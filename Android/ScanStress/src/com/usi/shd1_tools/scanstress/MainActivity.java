package com.usi.shd1_tools.scanstress;

import java.util.Date;
import java.util.Timer;
import java.util.TimerTask;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.os.PowerManager;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.view.View.OnLongClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.symbol.scanning.ScanDataCollection;
import com.symbol.scanning.Scanner;
import com.symbol.scanning.Scanner.StatusListener;
import com.symbol.scanning.ScannerException;
import com.symbol.scanning.StatusData;
import com.symbol.scanning.Scanner.DataListener;
import com.symbol.scanning.StatusData.ScannerStates;
import com.usi.shd1_tools.scanstress.R.id;
import com.usi.shd1_tools.commonlibrary.*;
import android.provider.Settings.Secure;

public class MainActivity extends Activity {

	protected static final int ProgressUpdateIdentifier = 0x101;
	protected static final int DecodeCounterUpdateIdentifier = 0x102;
	protected static final int DecodeContentUpdateIdentifier = 0x103;
	private static final String logPath = "/storage/sdcard0/usi/shd1_tools/ScanStress/";
	private static final String zebraLogPath = "/storage/sdcard0/usi/shd1_tools/ScanStress/zebra_";
    public static final String Key_ScanEnableSettings = "com.usi.shd1_tools.ScanStress/ScanEnableDelay";
    public static final String Key_ScanDisableSettings = "com.usi.shd1_tools.ScanStress/ScanDisableDelay";
	private static final int logSizePre_FileInMB = 128;
	private static final int memoryMonitor_Interval = 600*1000;
	public final String contentProviderURL = "content://com.usi.shd1_tools/ScanStress";
	private final Logger.Log_Level currentLogLevel = Logger.Log_Level.Verbose;
	
	private Context currentContext;
	private TextView txtDecodeResult;
	private EditText txtLoopTime;
	private EditText txtInterval;
	private EditText txtTimeout;
	private TextView txtProcedure;
	private Thread tdScan_Task;
	private Thread tdMemoryMonitor;
	private Thread mainThread;
	private Button btnStart;
	private Button btnScanTest;
	private Button btnDebug;
	private Button btnEnable;
	private Button btnDisable;
	private TextView txtPass;
	private TextView txtFail;
	private TextView txtND;
	private CheckBox chkEnableDisable;
	private boolean isDecoded = false;
	private boolean isDecode_timeout = false;
	private boolean scan_flag = false;
	private boolean memoryMonitor_flag = false;
	private String decodeGoldenSample = "";
	private String decodeData = "";
	private int loopTime = 0;
	private int loopCount = 0;
	private int passCount =0;
	private int failCOunt = 0;
	private int ndCount=0;
	private Date triggerTime;
    private Handler hd_UiRefresh ;
    private Scanner scanner;
	private boolean isEnableDisableEachScan = false;
	private static final int scannerEnableDisableTimeout = 10000;
	private static final int scannerEnableDisableDelay = 500;
	private static ScannerStates currentScanState = ScannerStates.DISABLED;
	private String deviceID = ""; 	

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        currentContext = getApplicationContext();
        mainThread = Thread.currentThread();
        txtDecodeResult = (TextView)findViewById(id.txtDecodeResult);
    	txtDecodeResult.setEnabled(false);
        txtLoopTime = (EditText)findViewById(id.intLoop);
        txtInterval = (EditText)findViewById(id.intInterval);
        txtTimeout = (EditText)findViewById(id.intTimeout);
        btnScanTest = (Button)findViewById(id.btnTestScan);
        btnStart = (Button)findViewById(id.btnStart);
        btnDebug = (Button)findViewById(id.btnTestActivity);
        btnEnable = (Button)findViewById(id.btnEnable);
        btnDisable = (Button)findViewById(id.btnDisable);
        txtProcedure = (TextView)findViewById(id.txtProgress);
        txtPass = (TextView)findViewById(id.txtPass);
        txtFail = (TextView)findViewById(id.txtFail);
        txtND= (TextView)findViewById(id.txtND);
        chkEnableDisable = (CheckBox)findViewById(id.chkEnableDisable);
        deviceID = Secure.getString(currentContext.getContentResolver(),Secure.ANDROID_ID);
        scanner= new Scanner();
        scannerEnable(true,scannerEnableDisableTimeout);
       
        hd_UiRefresh = new Handler() {
            public void handleMessage(Message msg) {   
                 switch (msg.what) {   
                      case MainActivity.ProgressUpdateIdentifier: 
                    	  updateProgress(msg.obj.toString());
                    	  txtProcedure.invalidate();  
                           break;
                      case MainActivity.DecodeCounterUpdateIdentifier:
                    	  updateDecodeCounter();
                    	  break;
                      case MainActivity.DecodeContentUpdateIdentifier:
                    	  updateDecodeContent(msg.obj.toString());
                    	  break;                 }   
                 super.handleMessage(msg);   
            }   
       };

        btnScanTest.setOnLongClickListener(new OnLongClickListener() {
			@Override
			public boolean onLongClick(View v) {
				return false;
			}
		});
        btnScanTest.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Scan_Once(true);
				updateDecodeCounter();
			}
		});
        btnStart.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) 
			{
				txtLoopTime.setEnabled(scan_flag);
				txtInterval.setEnabled(scan_flag);
				chkEnableDisable.setEnabled(scan_flag);
				txtTimeout.setEnabled(scan_flag);
				if(scan_flag)
				{
					Toast.makeText(currentContext, "Scan stress is stopping.", Toast.LENGTH_SHORT).show();
					btnStart.setText("Start");
					btnStart.setBackgroundColor(Color.GREEN);
					btnStart.setTextColor(Color.WHITE);
					btnStart.setEnabled(false);
					Logger.WriteLog(Logger.Log_Level.Information,"Stopped","Scan stress task is stopped by user.",true );
					ScanTask_Stop();
					btnStart.setEnabled(true);					
				}
				else 
				{	
					Toast.makeText(currentContext, "Scan stress is starting.", Toast.LENGTH_SHORT).show();
					btnStart.setText("Stop");
					btnStart.setBackgroundColor(Color.RED);
					btnStart.setTextColor(Color.WHITE);
					btnStart.setEnabled(false);
					Logger.WriteLog(Logger.Log_Level.Information,"Stopped","Scan stress task is starting.",true );
					ScanTask_Start(Integer.parseInt(txtLoopTime.getText().toString()),Integer.parseInt(txtInterval.getText().toString()),Long.parseLong(txtTimeout.getText().toString()),chkEnableDisable.isChecked());			
					btnStart.setEnabled(true);
				}
			}				
		});
        btnDebug.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Scan_Once(true);				
			}
		});

        PowerManager pm = (PowerManager) getSystemService(Context.POWER_SERVICE);
    	//============For Debug only=============
        //startMemoryMonitor();
    	//=======================================
    }
    
    private void Scan_Once(boolean isEnableDisable){
    	isDecoded = false;
    	isDecode_timeout = false;
    	decodeData = "";
    	isEnableDisableEachScan = isEnableDisable;
    	if(!scanner.isEnable())
    	{
    		scannerEnable(true,scannerEnableDisableTimeout);
    	}
    	triggerTime = new Date();
    	Logger.WriteLog("Action","Scanner is triggered.");
	    try {
			scanner.read();
		} catch (ScannerException e) {
			e.printStackTrace();
		}    	
    }
     
    private boolean scannerEnable(boolean enable,int timeout){
    	final int _timeout = timeout;
    	int checkStatusInterval = (int) _timeout/4;
    	boolean result = false;
    	Logger.WriteLog("Action", "Try to "+((enable)?"enable":"disable")+" the scanner.");
    	if(scanner!=null)
    	{    		
			if(enable && !scanner.isEnable())
			{
				try{
		        Date startTime = new Date();		        
		        scanner.addDataListener(new DataListener() {			
					@Override
					public void onData(ScanDataCollection arg0) 
					{
						isDecoded = true;						
						String data = "";
						for(int i=0;i<arg0.DataList.size();i++)
						{
							data+=arg0.DataList.get(i).getData()+"\n";
						}
						//data = data.trim();
						if(decodeGoldenSample.length()==0)
						{
							decodeGoldenSample = data;
						}
						decodeData = data;
						Logger.WriteLog("Action","Scanner is decoded, decode data = "+ decodeData);
						if(mainThread!=null && Thread.currentThread().equals(mainThread))
						{
							updateDecodeContent(decodeData);
						}
						else 
						{
							Message msg = new Message();
							msg.what = MainActivity.DecodeContentUpdateIdentifier;
							msg.obj = data;
							hd_UiRefresh.handleMessage(msg);
						}
					}
				});
		        scanner.addStatusListener(new StatusListener() {					
					@Override
					public void onStatus(StatusData arg0) {
						currentScanState = arg0.getState();
						Logger.WriteLog("Info", "Scanner status changed :"+currentScanState.toString());
					}
				});
		        scanner.enable();
		        //Check enable state 
		        Date currentTime;
		        do {		        	
		        	currentTime = new Date();
		        	result = scanner.isEnable();
		        	if(result)
		        	{
		        		break;
		        	}
		        	try {
						Thread.sleep(checkStatusInterval);
					} catch (InterruptedException e) {
						e.printStackTrace();
					}
				}while ((currentTime.getTime()-startTime.getTime())<=_timeout & !result);
		        if(result)
		        {
			        try {
						Thread.sleep(scannerEnableDisableDelay);
					} catch (InterruptedException e) {
						e.printStackTrace();
					}
		        }
		    	Logger.WriteLog(Logger.Log_Level.Information,"CheckPoint", "Change scanner state, result = "+((result)?"PASS":"FAIL"),true);
				}
				catch(Exception ex)
				{
				
				}
			}		
			else if(!enable && scanner.isEnable())
			{
		        Date startTime = new Date();
				Date currentTime = new Date();
				try {
					scanner.disable();
				} catch (ScannerException e1) {
					e1.printStackTrace();
				}
			        do {		        	
			        	currentTime = new Date();
			        	result = !scanner.isEnable();
			        	if(result)
			        	{
			        		break;
			        	}
			        	try {
							Thread.sleep(checkStatusInterval);
						} catch (InterruptedException e) {
						}
					}while ((currentTime.getTime()-startTime.getTime())<=_timeout & !result);
			        if(result)
			        {
				        try {
							Thread.sleep(scannerEnableDisableTimeout);
						} catch (InterruptedException e) {
						}
			        }
			    	Logger.WriteLog(Logger.Log_Level.Information,"CheckPoint", "Change scanner state, result = "+((result)?"PASS":"FAIL"),true);
			}
			else 
			{
				result = true;
		    	Logger.WriteLog("Info", "Scanner is already "+((enable)?"enable":"disable"));
			}  		
    	}
    	else
    	{    		
    		result = false;
    		Logger.WriteLog(Logger.Log_Level.Information,"Error", "Scanner is null, please initialize the scanner first.",true);
    	}  	
    	return result;
    }
    
    private void dataReset(){
    	loopCount =0;
    	loopTime = 0;
    	passCount =0;
    	failCOunt = 0;
    	ndCount=0;
    	decodeGoldenSample = "";
    	decodeData = "";
    	txtDecodeResult.setText("");
    }
    
    private void ScanTask_Start(int loop,final long decode_interval,final long decode_timeout,final boolean isEnableDisable){
    	if(!scan_flag)
    	{
    		SystemUtility.StayAwake(getWindow(), true);
    		dataReset();
    		Logger.CurrentLogLevel = currentLogLevel;
    		Logger.Start(logPath,logSizePre_FileInMB);
    		ZebraLogger.Start(zebraLogPath,deviceID);
    		Logger.RunLogcat(logPath);
    		updateDecodeCounter();    		
        	scan_flag = true;
        	loopTime = loop;
        	if(tdScan_Task!=null)
        	{
        		try {
            		tdScan_Task.join(1000);					
				} catch (Exception e) {

				}
        		tdScan_Task = null;
        	}        	
        	tdScan_Task = new Thread(new Runnable() {
        		@Override
				public void run() {
					long real_interval = decode_interval;
					Logger.WriteLog(Logger.Log_Level.Information,"Info","#### Scan Stress Test start. ####",true);
					ZebraLogger.WriteLog("Profiler Version | 0.0");
					ZebraLogger.WriteLog("MC36 Scan Stress | V1.1");					
					ZebraLogger.WriteLog("Product | Cardhu"	,true);
		        	while(loopCount<loopTime && scan_flag && !tdScan_Task.isInterrupted()){
		            	//Count down to start the scanning task
		        		if(loopCount==0){
		        			Logger.WriteLog("Action","Scanner is initializing");
		        	    	if(scanner!=null){
		        	    		Message m1 = new Message();
		        	    		m1.what = MainActivity.ProgressUpdateIdentifier;
		        	    		m1.obj = "Scanner's initializing...";
		        	    		 MainActivity.this.hd_UiRefresh.sendMessage(m1);
		        	    		scannerEnable(false,scannerEnableDisableTimeout);
		        	    		scannerEnable(true,scannerEnableDisableTimeout);
		        	    	}
		        			for(int i=3;i>0;i--){
		        				Message msg1 = new Message();
		        				msg1.what = MainActivity.ProgressUpdateIdentifier;
		        				msg1.obj = i +" seconds to start";            
			    	            MainActivity.this.hd_UiRefresh.sendMessage(msg1);   
			        			try {
				        			Thread.sleep(1000);
								} 
			        			catch (Exception e) {									
								}
		        			}
		        		}
		        		loopCount++;
		        		long decodeTime = 0;
		        		Logger.WriteLog(Logger.Log_Level.Information,"Info","==== Loop "+loopCount+ " start. ====",true);
		        		ZebraLogger.WriteLog("ScannerTask | Current Cycle | "+loopCount,true);
		        		Message message = new Message();   
	                    message.what = MainActivity.ProgressUpdateIdentifier;
	                    message.obj = loopCount+" / "+loopTime;            
	    	            MainActivity.this.hd_UiRefresh.sendMessage(message);
		        		try {
		        			Scan_Once(isEnableDisable);
		        			while( !isDecode_timeout && !isDecoded){	
		        				decodeTime = (new Date()).getTime()-triggerTime.getTime();
		        				isDecode_timeout = decodeTime>decode_timeout;
		    			    	Thread.sleep(50);
		    				}
		        			Logger.WriteLog(Logger.Log_Level.Verbose, "Detail", "isDecode_timeout = "+ String.valueOf(isDecode_timeout)+", isDecoded = "+String.valueOf(isDecoded)+", decode time = "+String.valueOf(decodeTime)+" ms");
		        			//eventWaitDecode.waitOne();
		        			getScanResult_Once();
			        		Message msg2 = new Message();
			        		msg2.what = MainActivity.DecodeCounterUpdateIdentifier;          
		    	            MainActivity.this.hd_UiRefresh.sendMessage(msg2);    	        			
						} 
		        		catch (Exception e){
						}
		        		Logger.WriteLog("Info","==== Loop "+loopCount+ " end. ====");
		        		real_interval = decode_interval - (new Date().getTime()-triggerTime.getTime());
		        		Logger.WriteLog(Logger.Log_Level.Verbose, "Decode", "Interval after decoded = "+real_interval);
		        		if(real_interval>0){
			        		try {
								Thread.sleep(real_interval);
							}
			        		catch (InterruptedException e){							
							}
		        		}
		        	}
		        	stopMemoryMonitor();
		        	ZebraLogger.WriteLog("Report | ScannerTask | P: "+passCount+" F: "+failCOunt+" ND : "+ndCount+"/"+loopCount,true);
		        	Logger.WriteLog(Logger.Log_Level.Information,"Info","#### Scan Stress Test end. ####",true);
		        	scan_flag = false;
		        	ZebraLogger.Stop();
		        	Logger.Stop();
		        	Logger.StopLogcat();
				}
			});
        	tdScan_Task.start();
            startMemoryMonitor();
    	}
    	else 
    	{
    		Toast.makeText(MainActivity.this,"Scanning task is still running...", Toast.LENGTH_LONG).show();
		}
    }
   
    private void ScanTask_Stop(){
    	stopMemoryMonitor();
    	scan_flag = false;
    	if(tdScan_Task!=null)
    	{
    		try {
        		tdScan_Task.join(1000);
        		tdScan_Task.interrupt();
        		tdScan_Task = null;
			} catch (Exception e) {
			}
    	}
    	ZebraLogger.Stop();
    	Logger.Stop();
    	Logger.StopLogcat();
    }
    	    
    private void stopMemoryMonitor(){
    	memoryMonitor_flag = false;
    	if(tdMemoryMonitor!=null)
    	{
    		try {
    			tdMemoryMonitor.join(1000);
				
			} catch (Exception e) {
			}
    		tdMemoryMonitor.interrupt();
    		tdMemoryMonitor = null;
    	}
    }
    
    private void getScanResult_Once(){
    	if(isDecoded)
    	{
    		Logger.WriteLog("Decode","Decoded data = "+decodeData);
    		if(decodeData.compareTo(decodeGoldenSample)==0)
    		{
    			passCount++;
    			Logger.WriteLog(Logger.Log_Level.Information,"CheckPoint", "Decode result : PASS",true);
    		}   
    		else 
    		{
    			failCOunt++;
    			Logger.WriteLog("Description", "Expected result = " +decodeGoldenSample+" ; decoded data = "+decodeData);
    			Logger.WriteLog(Logger.Log_Level.Information,"CheckPoint", "Decode result : FAIL",true);
    		}		
    	}
    	else 
    	{
    		if(isDecode_timeout)
    		{
    			ndCount++;
    			Logger.WriteLog(Logger.Log_Level.Information,"CheckPoint", "Decode result : ND",true);
    			if(scanner!=null)
    			{
    				try {
						scanner.cancelRead();
					} catch (ScannerException e) {
						e.printStackTrace();
					}
    			}
    		}
    	}
		if(isEnableDisableEachScan)
		{
			scannerEnable(false,scannerEnableDisableTimeout);
		}
    }
    
    private void updateProgress(String msg){
    	txtProcedure.setText(msg);
    	if(loopCount>=loopTime)
    	{
    		btnStart.setText("Finished!");
    		btnStart.setBackgroundColor(Color.rgb(255, 32, 192));
    		txtLoopTime.setEnabled(true);
    		txtInterval.setEnabled(true);
    		txtTimeout.setEnabled(true);
    		chkEnableDisable.setEnabled(true);
    	}  	
	}
    
    private void updateDecodeCounter(){
		txtPass.setText(String.valueOf(passCount));
		txtFail.setText(String.valueOf(failCOunt));
		txtND.setText(String.valueOf(ndCount));
	}
    
    private void updateDecodeContent(String content){
    	int maxLines = 5;
    	String text = content+"\n"+txtDecodeResult.getText().toString();
    	int currentLines = text.split("\n").length;
    	if(currentLines>maxLines)
    	{
    		text = text.substring(0,text.lastIndexOf("\n")); 		
    	}
    	txtDecodeResult.setText(text);
    }
	    
    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item){
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    
    private void startMemoryMonitor(){  
    	//============For Debug only=============    	
    	//Logger.Initialize(logPath, logSizePre_FileInMB);    	
    	//=======================================    	
    	memoryMonitor_flag = true;
    	if(tdMemoryMonitor!=null)
    	{
    		tdMemoryMonitor.interrupt();
    		tdMemoryMonitor = null;
    	}
    	tdMemoryMonitor = new Thread(new Runnable() {
			@Override
			public void run() 
			{
				try
				{
					while(!tdMemoryMonitor.isInterrupted() && memoryMonitor_flag){
						try{
							long availableMemory = SystemUtility.GetAvailableMemory(MainActivity.this);
							android.os.Debug.MemoryInfo memInfo = SystemUtility.GetApplicationMemoryInfo(MainActivity.this, "com.usi.shd1_tools.scan_stress");
							if(availableMemory>0){
								Logger.WriteLog(Logger.Log_Level.Debug, "SystemInfo", "System available memory = "+String.format("%.2f",(double)availableMemory/((double)1024*1024))+" MB");
							}
							if(memInfo!=null){
								Logger.WriteLog(Logger.Log_Level.Debug, "SystemInfo","TotalPrivateDirty of scan stress = "+String.format("%.2f",(double)memInfo.getTotalPrivateDirty()/(double)1024)+" KB");
								Logger.WriteLog(Logger.Log_Level.Debug, "SystemInfo","TotalPss of scan stress = "+String.format("%.2f",(double)memInfo.getTotalPss()/(double)1024)+" KB");
								Logger.WriteLog(Logger.Log_Level.Debug, "SystemInfo","TotalSharedDirty of scan stress = "+String.format("%.2f",(double)memInfo.getTotalSharedDirty()/(double)1024)+" KB");
							}
							Thread.sleep(memoryMonitor_Interval);
						} 
						catch (Exception e) 
						{
							// TODO: handle exception
						}
					}
				} 
				catch (Exception ex) {
				}			
			}
		});
    	tdMemoryMonitor.start();
    }
    
	protected void onPause(){
		//scannerEnable(false, scannerEnableDisableTimeout);
		//stayAwake(false);
		super.onPause();
	}
	
	protected void onRestart(){
		//scannerEnable(true, scannerEnableDisableTimeout);
		//stayAwake(true);
		super.onRestart();
	}
	    
    @Override
    protected void onDestroy(){
    	scan_flag = false;
    	memoryMonitor_flag = false;
    	SystemUtility.StayAwake(getWindow(), false);
    	if(tdScan_Task!=null)
    	{
    		tdScan_Task.interrupt();
    	}
//    	if(tdScan_Trigger!=null)
//    	{
//    		tdScan_Trigger.interrupt();
//    	}
    	stopMemoryMonitor();
    	scannerEnable(false, scannerEnableDisableTimeout);
    	ZebraLogger.Stop();
    	Logger.Stop();
    	Logger.StopLogcat();
    	super.onDestroy();
    }
    
    private int continuallyBackkeyClickCount = 0;
    Toast backNoticeToast;
    private Timer continuallyBackkeyClickTimer;
    @Override
    public void onBackPressed(){
    	if(continuallyBackkeyClickTimer!=null)
    	{
    		continuallyBackkeyClickTimer.cancel();
    	}
    	continuallyBackkeyClickTimer = new Timer();
		continuallyBackkeyClickTimer.schedule(new TimerTask() {			
			@Override
			public void run() {
				continuallyBackkeyClickCount = 0;
				continuallyBackkeyClickTimer.cancel();
			}
		},3000);
		continuallyBackkeyClickCount++;		
		if(continuallyBackkeyClickCount==1)
		{
			backNoticeToast = Toast.makeText(this,"Click back-key again to exit.",Toast.LENGTH_SHORT);
			backNoticeToast.show();
		}
		else if(continuallyBackkeyClickCount>=2)
		{
			continuallyBackkeyClickCount = 0;
			continuallyBackkeyClickTimer.cancel();
			if(backNoticeToast!=null)
			{
				backNoticeToast.cancel();
			}
			super.onBackPressed();
		}    	
    }
    
//    	if(awake)
//    	{
//    		getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
//    	}
//    	else {
//    		getWindow().clearFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);  		
//		}    
//    }
}
