package com.usi.shd1_tools.logRecorder;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.util.Timer;
import java.util.TimerTask;

import com.usi.shd1_tools.logRecorder.R.id;
import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.EditText;
import android.widget.Toast;
import com.usi.shd1_tools.commonlibrary.*;

public class LogDisplayActivity extends Activity {
	private Button btnRunLogcat;
	private CheckBox ckbLogMonitor;
	private EditText txtLogcat;
	private String logcatPath = "/sdcard/usi/shd1_tools/LogRecorder/";
	private Thread tdLogcatMonitor;
	private Handler hd_UiRefreshHandler;
	private final int logcatMonitorInterval = 6000;
	private final int  maxLinesOfLogcatText = 1000;
	private int displayLinesCount = 0;
	private boolean monitor_flag = false;
	public static final int UI_LogcatTextClear = 0x370;
	public static final int UI_LogcatTextAppend = 0x371;

	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_log_display);        
        btnRunLogcat = (Button)findViewById(id.btnRunLogcat);
        ckbLogMonitor = (CheckBox)findViewById(id.ckbStartMonitor);
        txtLogcat = (EditText)findViewById(id.txtLogText);
        //txtLogcat.setEnabled(false);
        buttonStateRefresh();
        btnRunLogcat.setOnClickListener(new OnClickListener(){
			@Override
			public void onClick(View v) {
				
				if(Logger.IsLogcatRunning())
				{
					Logger.StopLogcat();
				}
				else
				{
					Logger.RunLogcat(logcatPath);
				}
				buttonStateRefresh();
			}
        });
        ckbLogMonitor.setOnCheckedChangeListener(new OnCheckedChangeListener(){

			@Override
			public void onCheckedChanged(CompoundButton buttonView,boolean isChecked) {
				if(isChecked)
				{
					startLogcatMonitor();
				}
				else
				{
					stopLogcatMonitor();
				}
			}
        	
        });
        
        hd_UiRefreshHandler = new Handler() {  
            public void handleMessage(Message msg) {   
                 switch (msg.what) 
                 {   
                      case UI_LogcatTextAppend:                      
                    	  logcatTextAppend(msg.obj);
                    	  txtLogcat.invalidate();  
                          break;
                      case UI_LogcatTextClear:
                    	  logcatTextClear();
                    	  txtLogcat.invalidate();
                      default:
                    	  break;
                 }   
                 super.handleMessage(msg);   
            }   
       };
    }
    
    private void buttonStateRefresh(){
    	 if(Logger.IsLogcatRunning())
         {
         	btnRunLogcat.setText("Running");
         	btnRunLogcat.setBackgroundColor(Color.GREEN);
         	btnRunLogcat.setTextColor(Color.WHITE);        	
         }
         else
         {
         	btnRunLogcat.setText("Start");
         	btnRunLogcat.setBackgroundColor(Color.GRAY);
         	btnRunLogcat.setTextColor(Color.BLACK);
         }
         ckbLogMonitor.setEnabled(Logger.IsLogcatRunning());
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.log_display, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    
    private void logcatTextClear(){
    	txtLogcat.setText("");
    }
    
    private void logcatTextAppend(Object strMsg){
    	String oldText = txtLogcat.getText().toString();
    	txtLogcat.append(strMsg.toString()+oldText);
    }
    
    private void startLogcatMonitor(){
    	if(!monitor_flag)
    	{
    		monitor_flag = true;
    		tdLogcatMonitor = new Thread(new Runnable()
        	{   		
    			public void run() 
    			{
    				while(monitor_flag)
    				{
    					Message msg1 = new Message();
    					msg1.what = UI_LogcatTextClear;
    					msg1.obj = "";
    					hd_UiRefreshHandler.sendMessage(msg1);
    					displayLinesCount = 0;
    					String logcatPath = Logger.getLogcatPath();
    					File f =  new File(logcatPath);
    					displayLinesCount = 0;
    					try 
    					{
    						if(f.exists())
    						{
    				    		FileReader fr;
    							fr = new FileReader(logcatPath);
    							BufferedReader br = new BufferedReader(fr);
    							String  strTemp = "";
    							while((strTemp = br.readLine())!=null && displayLinesCount<maxLinesOfLogcatText)							
    							{
    								displayLinesCount++;
    								Message msg2 = new Message();
    								msg2.what = UI_LogcatTextAppend;
    								msg2.obj = strTemp;
    								hd_UiRefreshHandler.sendMessage(msg2);
    								Thread.sleep(1);
    							}
    						} 
    					}
    					catch (Exception e) {
    					}
    					
    					try {
    						Thread.sleep(logcatMonitorInterval);
    					} catch (InterruptedException e) {
    						e.printStackTrace();
    					}    					
    				}				
    			}    		
        	});
    		
    		tdLogcatMonitor.start();
    	}    	
    }

    private void stopLogcatMonitor(){
    	monitor_flag = false;
    	if(tdLogcatMonitor!=null)
    	{
    		try {
				tdLogcatMonitor.join(logcatMonitorInterval);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
    	}
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
			//scannerEnable(false);
			continuallyBackkeyClickCount = 0;
			continuallyBackkeyClickTimer.cancel();
			if(backNoticeToast!=null)
			{
				backNoticeToast.cancel();
			}	
			stopLogcatMonitor();
			super.onBackPressed();
		}   	
    }    
}
