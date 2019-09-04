package com.usi.shd1_tools.scanxmit;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.Timer;
import java.util.TimerTask;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.Toast;
import android.provider.Settings.Secure;

import com.usi.shd1_tools.commonlibrary.*;
import com.usi.shd1_tools.commonlibrary.Logger.Log_Level;
import com.usi.shd1_tools.scannerlibrary.ScannerController;
import com.usi.shd1_tools.scannerlibrary.ScannerDecodedMessageListener;
import com.usi.shd1_tools.scannerlibrary.ScannerDecodedMessageObject;
import com.usi.shd1_tools.scannerlibrary.ScannerStateChangedEventListener;
import com.usi.shd1_tools.scannerlibrary.ScannerStateChangedEventObject;


public class ScanXmit2 extends Activity {
	private static final String TITLE = "ScanXmit";
	private static final String VERSION = "V1.1";
	protected static final int Identifier_ProgressUpdate = 0x101;
	protected static final int Identifier_DecodeCounterUpdate = 0x102;
	protected static final int Identifier_DecodeContentUpdate = 0x103;
	protected static final int Identifier_SendDataUpdateCounter = 0x104;
	protected static final int Identifier_FullscreenParameterChanged = 0x105;
	private static final String logPath = "/storage/sdcard0/usi/shd1_tools/ScanXmit/";
	private static final String zebraLogPath = "/storage/sdcard0/usi/shd1_tools/ScanXmit/zebra_";

	public static final String Key_TriggerScan_Looptimes = "com.usi.shd1_tools/ScanXmit.TriggerScan_Parameters.Looptimes";
	public static final String Key_TriggerScan_Interval = "com.usi.shd1_tools/ScanXmit.TriggerScan_Parameters.Interval";
	public static final String Key_TriggerScan_Timeout = "com.usi.shd1_tools/ScanXmit.TriggerScan_Parameters.Timeout";
	public static final String Key_TriggerScan_ScannerToggle = "com.usi.shd1_tools/ScanXmit.TriggerScan_Parameters.ScannerToggle";
	
	public static final String Key_HandsfreeScan_Looptimes = "com.usi.shd1_tools/ScanXmit.HandsfreeScan_Parameters.Looptimes";
	public static final String Key_HandsfreeScan_Interleave = "com.usi.shd1_tools/ScanXmit.HandsfreeScan_Parameters.Interleave";
	public static final String Key_HandsfreeScan_Barcodes = "com.usi.shd1_tools/ScanXmit.HandsfreeScan_Parameters.Barcodes";
	public static final String Key_HandsfreeScan_Timeout = "com.usi.shd1_tools/ScanXmit.HandsfreeScan_Parameters.Timeout";
	
    public static final String Key_DataTransfer_IP = "com.usi.shd1_tools/ScanXmit.DataTransger_Parameters.IP";
    public static final String Key_DataTransfer_Port = "com.usi.shd1_tools/ScanXmit.DataTransger_Parameters.Port";
    public static final String Key_DataTransfer_Interval = "com.usi.shd1_tools/ScanXmit.DataTransger_Parameters.Interval";
    public static final String Key_DataTransfer_FixDataSize = "com.usi.shd1_tools/ScanXmit.DataTransger_Parameters.FixDataSize";
    public static final String Key_DataTransfer_DataLength = "com.usi.shd1_tools/ScanXmit.DataTransger_Parameters.DataLength";

    public static final String Key_SuspendResume_ActiveInterval = "com.usi.shd1_tools/ScanXmit.SuspendResume_Parameters.ActiveInterval";
    public static final String Key_SuspendResume_SuspendInterval = "com.usi.shd1_tools/ScanXmit.SuspendResume_Parameters.SuspendInterval";
   
    public static final String Key_Fullscreen_FullScreen = "com.usi.shd1_tools/ScanXmit.Fullscreen_Parameters.FullScreen";
    public static final String Key_Fullscreen_StatusBar = "com.usi.shd1_tools/ScanXmit.Fullscreen_Parameters.StatusBar";
    public static final String Key_Fullscreen_HomeKey = "com.usi.shd1_tools/ScanXmit.Fullscreen_Parameters.HomeKey";
    public static final String Key_Fullscreen_BackKey = "com.usi.shd1_tools/ScanXmit.Fullscreen_Parameters.BackKey";
    public static final String Key_Fullscreen_MenuKey = "com.usi.shd1_tools/ScanXmit.Fullscreen_Parameters.MenuKey";
    public static final String Key_Fullscreen_Keycodes = "com.usi.shd1_tools/ScanXmit.Fullscreen_Parameters.Keycodes";
    public static final String Key_Fullscreen_KeycodesEnabled = "com.usi.shd1_tools/ScanXmit.Fullscreen_Parameters.KeycodesEnabled";
    
	private static final int logSizePre_FileInMB = 128;
	private static final int memoryMonitor_Interval = 600*1000;
	// {{ for full screen test
    private final static String INTENT_KEY_DISABLE = "com.android.key.service_settings";
    private final static String PDA_KEY_HOME = "pda_key_home";
    private final static String PDA_KEY_MENU = "pda_key_menu";
    private final static String PDA_KEY_BACK = "pda_key_back";
    private final static String PDA_KEY_DIS = "pda_key_dis";
    private final static String PDA_KEY_DIS_STRING = "pda_key_dis_string";

    private final static String INTENT_STATUSBAR = "com.android.service_settings";
    private final static String PDA_STATUSBAR = "pda_statusbar";
    // }} for full screen test
	
	private static Window currentWindow;
	private Context currentContext;
	private CheckBox ckbScanEnable;
	private CheckBox ckbDataTransferEnable;
	private CheckBox ckbSuspendResumeEnable;
	private TextView txtDecodeResult;
	private TextView txtProcedure;
	private Thread tdScan_Task;
	private Thread tdMemoryMonitor;
	private Thread mainThread;
	private Button btnStart;
	private Button btnScanTest;
	private Button btnHandsfreeTest;
	private Button btnDataTransferSettings;
	private Button btnSuspendResumeSettings;
	private Button btnTriggerScanSettings;
	private Button btnHandsfreeScanSettings;
	private RadioButton rdbTriggerScan;
	private RadioButton rdbHandsfreeScan;
	private TextView txtPass;
	private TextView txtFail;
	private TextView txtND;

	private boolean isDecoded = false;
	private boolean isDecode_timeout = false;
	private boolean scanTask_flag = false;
	private boolean memoryMonitor_flag = false;
	
	private String decodeGoldenSample = "";
	private String decodeData = "";
	private int loopTime = 0;
	private int loopCount = 0;
	private int passCount =0;
	private int failCOunt = 0;
	private int ndCount=0;
	private Date triggerTime;

    protected static Handler hd_UiRefresh = null;
    private ScannerController scanCol = null;
	private boolean isEnableDisableEachScan = false;
	private static final int scannerEnableDisableTimeout = 15000;
	public static int scannerEnableDelay = 50;
	public static int scannerDisableDelay = 50;
	private String deviceID = "";

	private TextView txtSendResult;
	private int sendPassCount = 0;
	private int sendFailCount = 0;
	private int sendCounter = 0;
	private int sendSize = 0;
	private boolean isDecodeListenerRegisted = false;
	// {{ Memory monitor	
	private void startMemoryMonitor(){
    	memoryMonitor_flag = true;
    	if(tdMemoryMonitor!=null){
    		tdMemoryMonitor.interrupt();
    		tdMemoryMonitor = null;
    	}
    	 
    	tdMemoryMonitor = new Thread(new Runnable(){
			@Override
			public void run(){
				try
				{
					while(!tdMemoryMonitor.isInterrupted() && memoryMonitor_flag){
						try {
							long availableMemory = SystemUtility.GetAvailableMemory(ScanXmit2.this);
							android.os.Debug.MemoryInfo memInfo = SystemUtility.GetApplicationMemoryInfo(ScanXmit2.this, "com.usi.shd1_tools.scan_stress");
							if(availableMemory>0){
								Logger.WriteLog("SystemInfo", "System available memory = "+String.format("%.2f",(double)availableMemory/((double)1024*1024))+" MB",Logger.Log_Level.Information);
							}
							if(memInfo!=null){
								Logger.WriteLog( "SystemInfo","TotalPrivateDirty of scan stress = "+String.format("%.2f",(double)memInfo.getTotalPrivateDirty()/(double)1024)+" KB",Logger.Log_Level.Information);
								Logger.WriteLog( "SystemInfo","TotalPss of scan stress = "+String.format("%.2f",(double)memInfo.getTotalPss()/(double)1024)+" KB",Logger.Log_Level.Information);
								Logger.WriteLog( "SystemInfo","TotalSharedDirty of scan stress = "+String.format("%.2f",(double)memInfo.getTotalSharedDirty()/(double)1024)+" KB",Logger.Log_Level.Information);
							}
							Thread.sleep(memoryMonitor_Interval);
						} 
						catch (Exception e){
						}
					}
				} 
				catch (Exception ex) {
				}			
			}
		});
    	tdMemoryMonitor.start();
    }
	
	private void stopMemoryMonitor(){
    	memoryMonitor_flag = false;
    	if(tdMemoryMonitor!=null)
    	{
    		try {
    			tdMemoryMonitor.join(1000);
    			tdMemoryMonitor.interrupt();
			} catch (Exception e) {
			}    		
    		tdMemoryMonitor = null;
    	}
    }
	
	// }} Memory monitor
	
	// {{ for data transfer
	
	private Transfer transfer;
	private Thread tranThread;
	private boolean dataTransfer_flag = false;
	//private boolean isTransfering = false;
	private static String dataTransfer_Ip = "192.168.2.201";
	private static int dataTransfer_Port = 1000;
	private static int dataTransfer_SendInterval = 30000;
	private static boolean dataTransfer_FixDataSize = false;
	private static int dataTransfer_FixedDataLength = 1024;
	
	public  static void SetDataTransferParameters(String ip,int port, int interval,boolean fixDataSize, int dataLength){
		dataTransfer_Ip = ip;
		dataTransfer_Port = port;
		dataTransfer_SendInterval = interval;
		dataTransfer_FixDataSize = fixDataSize;
		dataTransfer_FixedDataLength = dataLength;		
	}
	
	private void dataTransfer_Start(String serverip, int serverport, int interval, boolean chkSize){
    	Log.i("Transfer", "startTranThread");
    	dataTransfer_flag = true;
    	final int sendDataInterval = interval;
    	final boolean isCheckSize = chkSize;
    	final String _ip = serverip;
    	final int _port = serverport;
    	if(isCheckSize){
    		sendSize = dataTransfer_FixedDataLength;
    	} else {
    		sendSize = 0;
    	}
    	if(tranThread != null){
    		tranThread.interrupt();
    		tranThread = null;
    	}
    	tranThread = new Thread(new Runnable() {
			@Override
			public void run() {
				try{					
					while(!tranThread.isInterrupted() && dataTransfer_flag){						
						if(suspended_flag){ // && !isTransfering){
							// {{ Waiting for device resume
		        			try {
		        				Thread.sleep(500);
							} catch (InterruptedException e) {
									e.printStackTrace();
							}
		        			// }} Waiting for device resume
						}					
						else{
							//isTransfering=true;
							try {
								Thread.sleep(sendDataInterval);								
								if (connectServer(_ip, _port)){
									String result = "ERROR";
									if (isCheckSize && (sendSize != 0)) { 
										result = transfer.SendDataToServer(transfer.Data, sendSize);
									} else {
										result = transfer.SendDataToServer(transfer.Data);
									}
									Message msg = new Message();   
									msg.what = ScanXmit2.Identifier_SendDataUpdateCounter;  
									msg.obj = result;        
					        		ScanXmit2.hd_UiRefresh.sendMessage(msg);
					        		transfer.disconnect();
								}
								else{
									Message msg = new Message();   
									msg.what = ScanXmit2.Identifier_SendDataUpdateCounter;  
									msg.obj = "Connect error";        
					        		ScanXmit2.hd_UiRefresh.sendMessage(msg);
								}
							} 
							catch (Exception e){
							}
							//isTransfering=false;
						}
					}
					dataTransfer_flag = false;
				} 
				catch (Exception ex) {
					Logger.WriteLog("Error", "tranThread exception, message = "+ex.getMessage(),Logger.Log_Level.Information);
				}			
			}
		});
    	tranThread.start();
	}

	private void dataTransfer_Stop(){
    	Log.i("Transfer", "stopTranThread");
    	dataTransfer_flag = false;
    	if(tranThread!=null){
    		try {
    			tranThread.join(1000);				
			} catch (Exception e) {

			}
    		tranThread.interrupt();
    		tranThread = null;
    	}	
	}
	
	// }} for data transfer
	
	// {{ for suspend / resume
	
	private boolean showNoticeMsgForSuspendResume = true;
	private Thread tdSuspendResume;
	private boolean suspendResume_flag = false;
	private boolean suspended_flag = false;
	private Date dtSuspendStartTime;
	private Date dtActiveStartTime;	
	private static int suspendResume_ActiveInterval = 60000;
	private static int suspendResume_SuspendInterval = 60000;
	public static void SetSuspendResumeParameters(int activeInterval,int suspendInterval){
		suspendResume_ActiveInterval = activeInterval;
		suspendResume_SuspendInterval = suspendInterval;
	}
	
	private void suspendResume_Start(final int activeInterval,final int suspendInterval){
		try {
	    	if(tdSuspendResume!=null){
		    	suspendResume_flag = false;
		    	tdSuspendResume.join();
		    	tdSuspendResume = null;
	    	}
	    	suspendResume_flag = true;
	    	suspended_flag = false;
	    	SystemUtility.StayAwake(currentWindow, false);
	    	SystemUtility.SetWakeLock(currentContext, true);
	    	tdSuspendResume= new Thread(new Runnable(){
	    		@Override
				public void run() {
	    			dtActiveStartTime = new Date();
	    			dtSuspendStartTime = new Date();
	    			while(suspendResume_flag && !tdSuspendResume.interrupted()){
	    				if(suspended_flag){
	    					if((new Date().getTime()-dtSuspendStartTime.getTime())>=suspendInterval){	    						
	    						Logger.WriteLog("Action","--- Set device to wake up. ---");
	    						SystemUtility.SetWakeLock(currentContext, true);
	    						if(SystemUtility.IsScreenOn(currentContext)){
	    							suspended_flag = false;
		    						dtActiveStartTime = new Date();
	    						}		    					
		    				}
	    				}
	    				else{
	    					if((new Date().getTime()-dtActiveStartTime.getTime())>=activeInterval){
	    						Logger.WriteLog("Action","--- Set device to suspend. ---");
	    						SystemUtility.setWakeUp_AlarmManager(currentContext,suspendInterval-500);
	    						SystemUtility.SetWakeLock(currentContext,false);
		    					if(!SystemUtility.IsScreenOn(currentContext)){
		    						suspended_flag = true;
			    					dtSuspendStartTime = new Date();
	    						}
		    				}    					
	    				}
	    				try{
							Thread.sleep(500);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
	    			}
	    			suspendResume_flag = false;
	    		}
	    	});		    	
	    	tdSuspendResume.start();
		    }
			catch (InterruptedException e) {
				e.printStackTrace();
			}
	}
	    
    private void suspendResume_Stop(){
    	if(tdSuspendResume!=null){
	    	suspendResume_flag = false;
	    	try {
				tdSuspendResume.join();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
	    	tdSuspendResume = null;
    	}
    	//StayAwake(true);
    }
    
	// }} for suspend / resume
	
    // {{ for scan task
    
    private static int triggerScan_looptime = 5000;
	private static int triggerScan_interval = 5000;
	private static int triggerScan_timeout = 2000;
	private static boolean triggerScan_scanner_toggle = false;
	
	public static void SetTriggerScanParameters(int looptimes,int interval,int timeout,boolean toggleFlag){
		triggerScan_looptime = looptimes;
		triggerScan_interval = interval;
		triggerScan_timeout = timeout;
		triggerScan_scanner_toggle = toggleFlag;
	}
    
    private void scanTask_Start(){   	
    	if(!scanTask_flag){
    		dataReset();
    		updateDecodeCounter();
    		Logger.Start(logPath,logSizePre_FileInMB);
    		//Logger.CurrentLogLevel = Logger.Log_Level.Verbose;
    		ZebraLogger.Start(zebraLogPath,deviceID);
    		Logger.RunLogcat(logPath);
        	scanTask_flag = true;
        	loopTime = triggerScan_looptime;
        	if(tdScan_Task!=null){
        		try {
            		tdScan_Task.join(1000);					
				} catch (Exception e) {
				}
        		tdScan_Task = null;
        	}        	
        	tdScan_Task = new Thread(new Runnable() {				
				@Override
				public void run() {
					Logger.WriteLog("Info","#### Scan Stress Test start. ####",Logger.Log_Level.Information,true);
					ZebraLogger.WriteLog("Profiler Version | 0.0");
					ZebraLogger.WriteLog("MC36 "+TITLE+" | "+VERSION);					
					ZebraLogger.WriteLog("Product | Cardhu",true);					
		        	while(loopCount<loopTime && scanTask_flag && !tdScan_Task.isInterrupted()){
		        		if(suspended_flag){ // && !isScanning){
							// {{ Waiting for device resume
		        			try {
		        				Thread.sleep(500);
							} catch (InterruptedException e) {
									e.printStackTrace();
							}
		        			// }} Waiting for device resume
						}
		        		else{
		        			//isScanning = true;
			            	// {{Scanner initialize
			        		if(loopCount==0){			        			
			        			Logger.WriteLog("Action","Scanner is initializing");			        			
			        	    	if(scanCol!=null){
			        	    		Message m1 = new Message();
			        	    		m1.what = ScanXmit2.Identifier_ProgressUpdate;
			        	    		m1.obj = "Scanner's initializing...";
			        	    		ScanXmit2.hd_UiRefresh.sendMessage(m1);
			        	    	}
			        	    	scannerEnable(true,scannerEnableDisableTimeout);
			        	    	
			        			for(int i=3;i>0;i--){
			        				Message msg1 = new Message();
			        				msg1.what = ScanXmit2.Identifier_ProgressUpdate;
			        				msg1.obj = i +" seconds to start";            
			        				ScanXmit2.hd_UiRefresh.sendMessage(msg1);   
				        			try {
					        			Thread.sleep(1000);
									} 
				        			catch (Exception e) {									
									}
			        			}
			        		}
			        		// }}Scanner initialize
			        		loopCount++;
			        		Logger.WriteLog("Info","==== Loop "+loopCount+ " start. ====");
			        		ZebraLogger.WriteLog("ScannerTask | Current Cycle | "+loopCount);
			        		Message message = new Message();   
		                    message.what = ScanXmit2.Identifier_ProgressUpdate;
		                    message.obj = loopCount+" / "+loopTime;            
		                    ScanXmit2.hd_UiRefresh.sendMessage(message);
			        		try {
			        			triggerScan_Once(triggerScan_scanner_toggle);
			        			while( !isDecode_timeout && !isDecoded)
			    				{
			        				isDecode_timeout = (new Date().getTime()-triggerTime.getTime())>triggerScan_timeout;
			    			    	Thread.sleep(50);
			    				}        			
			        			//eventWaitDecode.waitOne();
			        			triggerScanResult_Once();
				        		Message msg2 = new Message();   
				        		msg2.what = ScanXmit2.Identifier_DecodeCounterUpdate;          
				        		ScanXmit2.hd_UiRefresh.sendMessage(msg2);
							} 
			        		catch (Exception e) {
							}
			        		Logger.WriteLog("Info","==== Loop "+loopCount+ " end. ====");
			        		try {
								Thread.sleep(triggerScan_interval);
							} catch (InterruptedException e) {
								e.printStackTrace();
							}
			        		//isScanning = false;
		        		}
		        	}
		        	dataTransfer_Stop();
		        	suspendResume_Stop();
		        	stopMemoryMonitor();
		        	transfer.disconnect();
		        	ZebraLogger.WriteLog("Report | ScannerTask | P: "+passCount+" F: "+failCOunt+" ND : "+ndCount+"/"+loopCount,true);
		        	Logger.WriteLog("Info","#### Scan Stress Test end. ####",Logger.Log_Level.Information,true);
		        	scanTask_flag = false;
		        	ZebraLogger.Stop();
		        	Logger.Stop();
		        	Logger.StopLogcat();
				}
			});
        	tdScan_Task.start();
            startMemoryMonitor();
    	}
    	else{
    		Toast.makeText(ScanXmit2.this,"Scanning task is still running...", Toast.LENGTH_LONG).show();
		}
    }

    private void scanTask_Stop(){
    	stopMemoryMonitor();
    	scanTask_flag = false;
    	if(tdScan_Task!=null){
    		try {
        		tdScan_Task.join(1000);				
			} catch (Exception e){
			}
    		tdScan_Task.interrupt();
    		tdScan_Task = null;
    	}    	
    	ZebraLogger.Stop();
    	Logger.Stop();
    }
    
    // }} for scan task
        
    // {{ for hand-free scan task
    private static int handsfree_timeout = 120000;
	private static int handsfree_interleave = 0;
	private static int handsfree_looptimes = 2000;
	private static ArrayList<String> handsfree_barcodes;
	private static boolean handsfree_flag = false;
	private Timer tmrHandsfreeScan_timeout;
    
	public static void SetHandsfreeScanParameters(int looptimes,int interval,int timeout,String[]barcodes){
		handsfree_looptimes = looptimes;
		handsfree_interleave = interval;
		if(handsfree_barcodes!=null){
			handsfree_barcodes.clear();
			handsfree_barcodes = null;
		}
		handsfree_barcodes = new ArrayList<String>(Arrays.asList(barcodes));
		handsfree_timeout = timeout;
	}
	
	private void handsfreeScan_Start(){
		if(!handsfree_flag){
    		dataReset();
    		updateDecodeCounter();
    		scannerEnable(true, scannerEnableDisableTimeout);
    		Logger.Start(logPath,logSizePre_FileInMB);    		
    		ZebraLogger.Start(zebraLogPath,deviceID);
    		Logger.RunLogcat(logPath);
        	handsfree_flag = true;
        	loopTime = handsfree_looptimes;
        	Logger.WriteLog("Info","#### Scan Stress Test start. ####",Logger.Log_Level.Information,true);
			ZebraLogger.WriteLog("Profiler Version | 0.0");
			ZebraLogger.WriteLog("MC36 "+TITLE+" | "+VERSION);					
			ZebraLogger.WriteLog("Product | Cardhu",true);
			try{
				scanCol.HandsfreeScan(handsfree_interleave);
				resetHandsfreeDecodeTimer();				
			}
			catch(Exception ex){
				Logger.WriteLog("handsfree","Handsfree scan exception occured, testing is interrupted.",Logger.Log_Level.Error,true);
			}
		}
		Toast.makeText(getApplicationContext(), "HandsfreeScan started~!", Toast.LENGTH_LONG).show();
	}
	
	private void resetHandsfreeDecodeTimer(){
		if(handsfree_timeout>0){
			if(tmrHandsfreeScan_timeout!=null){
				tmrHandsfreeScan_timeout.cancel();
				tmrHandsfreeScan_timeout = null;
			}
			tmrHandsfreeScan_timeout = new Timer();
			tmrHandsfreeScan_timeout.schedule(new TimerTask() {			
				@Override
				public void run() {
					Logger.WriteLog("Failed", "Handsfree decode timeout, end the handsfree scanning task!!",Log_Level.Error,true );
					tmrHandsfreeScan_timeout.cancel();	
					btnStart.setText("FAILED");
					btnStart.setBackgroundColor(Color.MAGENTA);
					btnStart.setTextColor(Color.WHITE);
					btnStart.setEnabled(false);
					Stop();
					btnStart.setEnabled(true);							
				}
			},handsfree_timeout);
		}
	}
	
    private void handsfreeScanResult_Once(){
    	if(isDecoded){
    		Logger.WriteLog("Decode","Decoded data = "+decodeData);
    		int index = handsfree_barcodes.indexOf(decodeData);
    		if(index<0){
    			failCOunt++;
    			Logger.WriteLog("Description","decoded data = "+decodeData+" is not in the expected barcodes list");
    			Logger.WriteLog("CheckPoint", "Decode result : FAIL",Logger.Log_Level.Information,true);
    		}
    		else{
    			passCount++;
    			Logger.WriteLog("CheckPoint", "Decode result : PASS",Logger.Log_Level.Information,true);   			
    		}
    		updateDecodeCounter();
//    		Message msg2 = new Message();   
//    		msg2.what = ScanXmit2.Identifier_DecodeCounterUpdate;          
//    		ScanXmit2.this.hd_UiRefresh.sendMessage(msg2);
    	}
    	isDecoded = false;
    }
	
    private void handsfreeScan_Stop(){
    	if(tmrHandsfreeScan_timeout!=null){
    		tmrHandsfreeScan_timeout.cancel();
    	}
    	scanCol.CancelScan();
    	try {
			Thread.sleep(5000);
		} catch (InterruptedException e) {
		}
    	scanCol.Disable();    	
    	//scannerEnable(false, scannerEnableDisableTimeout);
    	dataTransfer_Stop();
    	suspendResume_Stop();
    	stopMemoryMonitor();
    	transfer.disconnect();
    	handsfree_flag = false;
    	ZebraLogger.Stop();
    	Logger.Stop();
    	Logger.StopLogcat();
    }
        
    
    // }} for hand-free scan task
    
    // {{basic functions
    private void triggerScan_Once(boolean isEnableDisable){
    	isDecoded = false;
    	isDecode_timeout = false;
    	decodeData = "";
    	isEnableDisableEachScan = isEnableDisable;
    	if(!scanCol.IsEnable()){
    		scannerEnable(true,scannerEnableDisableTimeout);    		
    	}
    	if(scanCol.IsEnable()){
			triggerTime = new Date();
	    	scanCol.Scan(); 			
		}
    }
    
    private void triggerScanResult_Once(){
    	if(isDecoded){
    		Logger.WriteLog("Decode","Decoded data = "+decodeData);
    		if(decodeData.compareTo(decodeGoldenSample)==0){
    			passCount++;
    			Logger.WriteLog("CheckPoint", "Decode result : PASS",Logger.Log_Level.Information,true);
    		}   
    		else{
    			failCOunt++;
    			Logger.WriteLog("Description", "Expected result = " +decodeGoldenSample+" ; decoded data = "+decodeData);
    			Logger.WriteLog("CheckPoint", "Decode result : FAIL",Logger.Log_Level.Information,true);
    		}		
    	}
    	else{
    		if(isDecode_timeout){
    			ndCount++;
    			Logger.WriteLog("CheckPoint", "Decode result : ND",Logger.Log_Level.Information,true);
    			if(scanCol!=null){
    				scanCol.CancelScan();    				
    			}
    		}
    	}
		if(isEnableDisableEachScan && scanCol.IsEnable()){
			scannerEnable(false,scannerEnableDisableTimeout);
		}    
    }
            
    private boolean scannerEnable(boolean enable,int timeout){
    	final int _timeout = timeout;
    	int checkStatusInterval = (int) _timeout/4;
    	boolean result = false;
    	Logger.WriteLog("Action", "Try to "+((enable)?"enable":"disable")+" the scanCol.");
    	if(scanCol!=null){
    		// {{ to enable scanner
			if(enable && !scanCol.IsEnable())
			{
		        Date startTime = new Date();
		        Date currentTime = new Date();
		        do
		        {		        	
		        	currentTime = new Date();
		        	if(result = scanCol.Enable())
		        	{
		        		if(!isDecodeListenerRegisted){
				        	scanCol.addScannerDecodedMessageListener(new ScannerDecodedMessageListener()
				        	{
								@Override
								public void DecodeMessage(
										ScannerDecodedMessageObject arg0) {
									isDecoded = true;
									decodeData = arg0.DecodedMessage();
									if(handsfree_flag){
										loopCount++;
										resetHandsfreeDecodeTimer();
										Logger.WriteLog("Info","==== Loop "+loopCount+ " start. ====");
						        		ZebraLogger.WriteLog("ScannerTask | Current Cycle | "+loopCount);
//						        		Message message = new Message();   
//					                    message.what = ScanXmit2.Identifier_ProgressUpdate;
//					                    message.obj = loopCount+" / "+loopTime;
//					                    ScanXmit2.this.hd_UiRefresh.sendMessage(message);
						        		updateProgress(loopCount+" / "+loopTime);
					                    handsfreeScanResult_Once();
					                    Logger.WriteLog("Info","==== Loop "+loopCount+ " end. ====");
					                    if(loopCount>=loopTime){
					                    	ZebraLogger.WriteLog("Report | ScannerTask | P: "+passCount+" F: "+failCOunt+" ND : "+ndCount+"/"+loopCount,true);
					                    	Logger.WriteLog("Info","#### Scan Stress Test end. ####",Logger.Log_Level.Information,true);
					                    	handsfreeScan_Stop();
					                    }
									}
									else{
										if(decodeGoldenSample.length()==0 & scanTask_flag)
										{
											decodeGoldenSample = decodeData;
										}
										//updateProgress(loopCount+" / "+loopTime);
									}
									if(mainThread!=null && Thread.currentThread().equals(mainThread))
									{
										updateDecodeContent(decodeData);
									}
									else
									{
										Message msg = new Message();
										msg.what = ScanXmit2.Identifier_DecodeContentUpdate;
										msg.obj = decodeData;
										hd_UiRefresh.handleMessage(msg);
									}							
								}		        		
				        	});
				        	scanCol.addScannerStateChangedeListener(new ScannerStateChangedEventListener(){
								@Override
								public void StateChangedMessage(
										ScannerStateChangedEventObject arg0) {
		//							currentScanState = arg0.getState();
		//							Logger.WriteLog("Info", "Scanner status changed :"+currentScanState.toString());
								}	        		
				        	});
				        	isDecodeListenerRegisted = true;
		        		}
				        try {
							Thread.sleep(scannerEnableDelay);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
				        break;
			        }
			        else{
			        	try {
							Thread.sleep(checkStatusInterval);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
			    }while ((currentTime.getTime()-startTime.getTime())<=_timeout && !result);
			    Logger.WriteLog("CheckPoint", "Change scanCol state, result = "+((result)?"PASS":"FAIL"));		    	
			}
			// }} to enable scanner
			
			// {{ to disable scanner
			else if(!enable && scanCol.IsEnable()){
		        scanCol.Disable();
		        isDecodeListenerRegisted = false;
				result = true;
			    Logger.WriteLog("CheckPoint", "Change scanCol state, result = "+((result)?"PASS":"FAIL"));
			}
			// }} to disable
			
			// {{ No change
			else{
				result = true;
		    	Logger.WriteLog("Info", "Scanner is already "+((enable)?"enable":"disable"));
			}
			// }} No change
    	}
    	else{    		
    		result = false;
    		Logger.WriteLog("Error", "Scanner is null, please initialize the scanCol first.");
    	}  	
    	return result;
    }
    
    private boolean connectServer(String serverIp, int serverPort) {
    	boolean connectResult = false;
    	connectResult = transfer.ConnectToServer(serverIp, serverPort);
    	return connectResult;
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
    	sendPassCount = 0;
    	sendFailCount = 0;
    	sendCounter = 0;
    	txtSendResult.setText("0 / 0 / 0");
    }
    
    private void updateDecodeContent(String content){
    	transfer.SaveData(content);
    	txtDecodeResult.setText(content);
    }
    // }}basic functions
    
    // {{ UI control
    private void updateSendResultCounter(String result){
    	sendCounter++;
    	if (result.equals("ERROR") || result.equals("Connect error")) {
    		sendFailCount++;
    		Logger.WriteLog("CheckPoint","Send data result = Fail",Logger.Log_Level.Information,true);
    	} else {
    		sendPassCount++;
    		Logger.WriteLog("CheckPoint","Send data result = Pass",Logger.Log_Level.Information,true);
    	}
    	Log.i("Transfer", "From server: " + result);
    	txtSendResult.setText(sendPassCount + " / " + sendFailCount + " / " + sendCounter);
    	Logger.WriteLog("Result","Send data result counter Pass = "+sendPassCount+", Fail = "+sendFailCount,Logger.Log_Level.Information,true);
	}
    
    private OnClickListener viewOnclickListener = new OnClickListener(){
		@Override
		public void onClick(View v) {
			if(v.equals(btnDataTransferSettings)){
				Intent intent = new Intent(ScanXmit2.this, DataTransferSettings.class);
				Bundle bundle = new Bundle();
	            bundle.putString(Key_DataTransfer_IP,dataTransfer_Ip);
	            bundle.putInt(Key_DataTransfer_Port, dataTransfer_Port);
	            bundle.putInt(Key_DataTransfer_Interval, dataTransfer_SendInterval);
	            bundle.putBoolean(Key_DataTransfer_FixDataSize, dataTransfer_FixDataSize);
	            bundle.putInt(Key_DataTransfer_DataLength, dataTransfer_FixedDataLength);
	            intent.putExtras(bundle);
	            startActivity(intent);
			}
			else if (v.equals(btnSuspendResumeSettings)){
				SystemUtility.StayAwake(currentWindow, false);
				 SystemUtility.SetWakeLock(currentContext, false);
				Intent intent = new Intent(ScanXmit2.this, SuspendResumeSettings.class);
				Bundle bundle = new Bundle();
				bundle.putInt(Key_SuspendResume_ActiveInterval, suspendResume_ActiveInterval);
				bundle.putInt(Key_SuspendResume_SuspendInterval, suspendResume_SuspendInterval);				
				intent.putExtras(bundle);
		        startActivity(intent);
			}
			else if(v.equals(btnTriggerScanSettings)){
					Intent intent = new Intent(ScanXmit2.this, TriggerScanSettings.class);
					Bundle bundle = new Bundle();
		            bundle.putInt(Key_TriggerScan_Looptimes,triggerScan_looptime);
		            bundle.putInt(Key_TriggerScan_Interval, triggerScan_interval);
		            bundle.putInt(Key_TriggerScan_Timeout, triggerScan_timeout);
		            bundle.putBoolean(Key_TriggerScan_ScannerToggle, triggerScan_scanner_toggle);
		            intent.putExtras(bundle);
		            startActivity(intent);
			}
			else if(v.equals(btnHandsfreeScanSettings)){
				Intent intent = new Intent(ScanXmit2.this,HandsfreeScanSettings.class);
				Bundle bundle = new Bundle();
				bundle.putInt(Key_HandsfreeScan_Looptimes,handsfree_looptimes);
				bundle.putInt(Key_HandsfreeScan_Interleave,handsfree_interleave);
				bundle.putInt(Key_HandsfreeScan_Timeout,handsfree_timeout);
				if(handsfree_barcodes!=null){
					String[] barcodes = handsfree_barcodes.toArray(new String[0]);
					bundle.putStringArray(Key_HandsfreeScan_Barcodes,barcodes);
				}
				intent.putExtras(bundle);
	            startActivity(intent);
			}
			else if (v.equals(btnStart)){
				boolean running = scanTask_flag | dataTransfer_flag | suspendResume_flag | handsfree_flag;
		    	if(running){
		    		Toast.makeText(currentContext, "Scan stress is stopping.", Toast.LENGTH_SHORT).show();
					btnStart.setText("Start");
					btnStart.setBackgroundColor(Color.GREEN);
					btnStart.setTextColor(Color.WHITE);
					btnStart.setEnabled(false);
					Stop();
					btnStart.setEnabled(true);
				}
				else {
					btnStart.setText("Stop");
					btnStart.setBackgroundColor(Color.RED);
					btnStart.setTextColor(Color.WHITE);	
					btnStart.setEnabled(false);
					Start();
					btnStart.setEnabled(true);
		    	}
		    	running = scanTask_flag | dataTransfer_flag | suspendResume_flag | handsfree_flag;
		    	taskRunningUiLock(!running);
			}
			else if (v.equals(btnHandsfreeTest)){
				if(handsfree_flag){
					handsfreeScan_Stop();
				}
				else{					
					scannerEnable(true, scannerEnableDisableTimeout);
					try {
						scanCol.HandsfreeScan(0);
					} catch (Exception e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}					
				}
			}
		}   	
    };
    
    private void updateDecodeCounter() {
		txtPass.setText(String.valueOf(passCount));
		txtFail.setText(String.valueOf(failCOunt));
		txtND.setText(String.valueOf(ndCount));
	}
    
    private OnCheckedChangeListener viewOnCheckedChangeListener = new OnCheckedChangeListener(){
		@Override
		public void onCheckedChanged(CompoundButton buttonView,boolean isChecked) {
			if(buttonView.equals(ckbDataTransferEnable)) {
				btnDataTransferSettings.setEnabled(isChecked);
			}			
			else if(buttonView.equals(ckbSuspendResumeEnable)){
				btnSuspendResumeSettings.setEnabled(isChecked);
				if(isChecked && showNoticeMsgForSuspendResume){
					Builder bulder = new AlertDialog.Builder(ScanXmit2.this);
					bulder.setTitle("Suspend / Resume");
				    bulder.setMessage("Please check the prerequsite :\n" +
				    		    	  " (1) Security->Screen lock = Disalbe\n" +
				    		    	  " (2) Display->Sleep = 15 sec\n" +
				    		    	  " (3) (Optional) Developer->Stay awake = Disalbe");
				    bulder.setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
				        public void onClick(DialogInterface dialog, int which) { 
				            dialog.cancel();
				        }
				     });
//				    bulder.setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
//				        public void onClick(DialogInterface dialog, int which) { 
//				        	 dialog.cancel();
//				        }
//				     });
				    bulder.setIcon(android.R.drawable.ic_dialog_alert);
				    AlertDialog alert11 = bulder.create();
				    alert11.show();
					showNoticeMsgForSuspendResume = false;
				}
			}
			else if(buttonView.equals(rdbHandsfreeScan)){
				btnHandsfreeScanSettings.setEnabled(rdbHandsfreeScan.isChecked());
			}
			else if(buttonView.equals(rdbTriggerScan)){
				btnTriggerScanSettings.setEnabled(rdbTriggerScan.isChecked());
			}
		}
    	
    };
    
    private void updateProgress(String msg){		
    	txtProcedure.setText(msg);
    	if(loopCount>=loopTime){
    		btnStart.setText("Finished!");
    		btnStart.setBackgroundColor(Color.rgb(255, 32, 192));
    		taskRunningUiLock(true);    		
    	}
	}
    
    private void taskRunningUiLock(boolean enabled){
    	ckbScanEnable.setEnabled(enabled);
    	ckbDataTransferEnable.setEnabled(enabled);
    	ckbSuspendResumeEnable.setEnabled(enabled);	    	
    	btnDataTransferSettings.setEnabled(enabled & ckbDataTransferEnable.isChecked());
    	btnSuspendResumeSettings.setEnabled(enabled & ckbSuspendResumeEnable.isChecked());
    	rdbTriggerScan.setEnabled(enabled);
    	rdbHandsfreeScan.setEnabled(enabled);
    	btnTriggerScanSettings.setEnabled(enabled & rdbTriggerScan.isChecked());
    	btnHandsfreeScanSettings.setEnabled(enabled & rdbHandsfreeScan.isChecked());
    	btnScanTest.setEnabled(enabled);
    	btnHandsfreeTest.setEnabled(enabled);
    }
    
    // }} UI control
    
    // {{ for full screen test
    
    private boolean fullscreen_isFullScreen = false;
    private boolean fullscreen_statusBarEnabled = true;
    private boolean fullscreen_homeKeyEnabled = true;
    private boolean fullscreen_backKeyEnabled = true;
    private boolean fullscreen_menuKeyEnabled = true;
    private String fullscreen_keycodes = "";
    private boolean fullscreen_keycodesEnabled = false;
   
    
    private void setFullscreen(boolean enable){
    	if(enable){
    		 getWindow().clearFlags(WindowManager.LayoutParams.FLAG_FORCE_NOT_FULLSCREEN);
             getWindow().addFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN);
    	}
    	else{
    		getWindow().clearFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN);
            getWindow().addFlags(WindowManager.LayoutParams.FLAG_FORCE_NOT_FULLSCREEN);
    	}
    	fullscreen_isFullScreen = enable;
    }
    
    private void setStatusbar(boolean enable){
    	if(enable){
    		Intent mIntent = new Intent();
            mIntent.putExtra(PDA_STATUSBAR, "on");
            mIntent.setAction(INTENT_STATUSBAR);
            sendBroadcast(mIntent);
    	}
    	else{
    		 Intent mIntent = new Intent();
             mIntent.putExtra(PDA_STATUSBAR, "off");
             mIntent.setAction(INTENT_STATUSBAR);
             sendBroadcast(mIntent);
    	}
    	fullscreen_statusBarEnabled = enable;
    }
    
    private void setKey_Home(boolean enable){
    	if(enable){
    		sendDisKeyIntent(PDA_KEY_HOME, "off","");
    	}
    	else{
    		sendDisKeyIntent(PDA_KEY_HOME, "on","");    		
    	}
    	fullscreen_homeKeyEnabled = enable;
    }
    
    private void setKey_Back(boolean enable){
    	if(enable){
    		sendDisKeyIntent(PDA_KEY_BACK, "off","");
    	}
    	else{
    		sendDisKeyIntent(PDA_KEY_BACK, "on","");    		
    	}
    	fullscreen_backKeyEnabled = enable;
    }
    
    private void setKey_Menu(boolean enable){
    	if(enable){
    		sendDisKeyIntent(PDA_KEY_MENU, "off","");
    	}
    	else{
    		sendDisKeyIntent(PDA_KEY_MENU, "on","");
    	}
    	fullscreen_menuKeyEnabled = enable;
    }
    
    private void setKeys_enable_disable(String keycodes, boolean enable){
    	if(enable){
    		sendDisKeyIntent(PDA_KEY_DIS, "off",keycodes);
    	}
    	else{
    		sendDisKeyIntent(PDA_KEY_DIS, "on",keycodes);
    	}
    	fullscreen_keycodes = keycodes;
    	fullscreen_keycodesEnabled = enable;
    }
    
    
    
    // }} for full screen test
    
    private void Start(){
    	if(ckbScanEnable.isChecked()){
    		if(rdbHandsfreeScan.isChecked()){
    			handsfreeScan_Start();
    		}
    		else{
    			scanTask_Start();
    		}
    	}
    	if(ckbDataTransferEnable.isChecked()){
			dataTransfer_Start(dataTransfer_Ip, 
					   		   dataTransfer_Port,
					   		   dataTransfer_SendInterval,
					   		   dataTransfer_FixDataSize);
		}
    	if(ckbSuspendResumeEnable.isChecked()){
    		suspendResume_Start(suspendResume_ActiveInterval,suspendResume_SuspendInterval);
    	}    	
    }
    
    private void Stop(){
    	if(handsfree_flag){
    		handsfreeScan_Stop();
    	}
    	else{
    		scanTask_Stop();
    	}
    	dataTransfer_Stop();
    	suspendResume_Stop();
    }
    
    @Override
    protected void onCreate(Bundle savedInstanceState){
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        currentContext = getApplicationContext();
        currentWindow = getWindow();
        //SystemUtility.StayAwake(currentWindow, true);
        SystemUtility.SetWakeLock(currentContext, true);
        mainThread = Thread.currentThread();
        txtDecodeResult = (TextView)findViewById(R.id.txtDecodeResult);
        txtDecodeResult.setMaxLines(100);
        txtProcedure = (TextView)findViewById(R.id.txtProgress);
        txtPass = (TextView)findViewById(R.id.txtPass);
        txtFail = (TextView)findViewById(R.id.txtFail);
        txtND= (TextView)findViewById(R.id.txtND);
        deviceID = Secure.getString(currentContext.getContentResolver(),Secure.ANDROID_ID);
        scanCol= new ScannerController();
		transfer = new Transfer();
		txtSendResult = (TextView)findViewById(R.id.sendResult);
        if(hd_UiRefresh==null){
			hd_UiRefresh = new Handler() {
		        public void handleMessage(Message msg) {   
		             switch (msg.what) {   
		                  case ScanXmit2.Identifier_ProgressUpdate: 
		                	  updateProgress(msg.obj.toString());
		                	  txtProcedure.invalidate();
		                       break;
		                  case ScanXmit2.Identifier_DecodeCounterUpdate:
		                	  updateDecodeCounter();
		                	  break;
		                  case ScanXmit2.Identifier_DecodeContentUpdate:
		                	  updateDecodeContent(msg.obj.toString());
		                	  break;
		                  case ScanXmit2.Identifier_SendDataUpdateCounter:
		                	  updateSendResultCounter(msg.obj.toString());
		                	  break;
		                  case ScanXmit2.Identifier_FullscreenParameterChanged:
		                	  executeFullscreenSettings(msg.obj.toString());
		                	  break;
		             }
		             super.handleMessage(msg);
		        }   
		   };
        }
        btnStart = (Button)findViewById(R.id.btnStart);
        btnScanTest = (Button)findViewById(R.id.btnScanTest);
        btnTriggerScanSettings = (Button)findViewById(R.id.btnTriggerScanSettings);
        btnHandsfreeScanSettings = (Button)findViewById(R.id.btnHandsfreeScanSettings);
        btnStart.setOnClickListener(viewOnclickListener);
        rdbHandsfreeScan = (RadioButton)findViewById(R.id.rdbHandsfreeScan);
        rdbTriggerScan = (RadioButton)findViewById(R.id.rdbTriggerScan);        
        btnScanTest.setOnClickListener(new OnClickListener(){
			@Override
			public void onClick(View v) {
				triggerScan_Once(true);			
			}
		});
        btnDataTransferSettings = (Button)findViewById(R.id.btnDataTransferSettings);
        btnSuspendResumeSettings = (Button)findViewById(R.id.btnSuspendResumeSettings);
        btnHandsfreeTest =  (Button)findViewById(R.id.btnHandsfreeTest);
        btnDataTransferSettings.setOnClickListener(viewOnclickListener);
        btnSuspendResumeSettings.setOnClickListener(viewOnclickListener);
        ckbScanEnable = (CheckBox)findViewById(R.id.ckbScan);
        ckbDataTransferEnable = (CheckBox)findViewById(R.id.ckbDataTransfer);
        ckbSuspendResumeEnable = (CheckBox)findViewById(R.id.ckbSuspendResume);
        ckbScanEnable.setOnCheckedChangeListener(viewOnCheckedChangeListener);
        ckbDataTransferEnable.setOnCheckedChangeListener(viewOnCheckedChangeListener);
        ckbSuspendResumeEnable.setOnCheckedChangeListener(viewOnCheckedChangeListener);
        btnTriggerScanSettings.setOnClickListener(viewOnclickListener);
        btnHandsfreeScanSettings.setOnClickListener(viewOnclickListener);
        rdbHandsfreeScan.setOnCheckedChangeListener(viewOnCheckedChangeListener);
        rdbTriggerScan.setOnCheckedChangeListener(viewOnCheckedChangeListener);
        btnHandsfreeTest.setOnClickListener(viewOnclickListener);
    }
   
    @Override
    protected void onDestroy(){
    	scanTask_flag = false;
    	memoryMonitor_flag = false;
    	SystemUtility.StayAwake(currentWindow, false);
    	if(tdScan_Task!=null){
    		tdScan_Task.interrupt();
    	}
    	stopMemoryMonitor();
    	scannerEnable(false, scannerEnableDisableTimeout);
    	ZebraLogger.Stop();
    	Logger.Stop();
    	Stop();
    	super.onDestroy();
    }
    
    private int continuallyBackkeyClickCount = 0;
    Toast backNoticeToast;
    private Timer continuallyBackkeyClickTimer;
    @Override
    public void onBackPressed(){
    	if(continuallyBackkeyClickTimer!=null){
    		continuallyBackkeyClickTimer.cancel();
    	}
    	continuallyBackkeyClickTimer = new Timer();
		continuallyBackkeyClickTimer.schedule(new TimerTask(){
			@Override
			public void run() {
				continuallyBackkeyClickCount = 0;
				continuallyBackkeyClickTimer.cancel();
			}
		},3000);
		continuallyBackkeyClickCount++;		
		if(continuallyBackkeyClickCount==1){
			backNoticeToast = Toast.makeText(this,"Click back-key again to exit.",Toast.LENGTH_SHORT);
			backNoticeToast.show();
		}
		else if(continuallyBackkeyClickCount>=2){
			continuallyBackkeyClickCount = 0;
			continuallyBackkeyClickTimer.cancel();
			if(backNoticeToast!=null)
			{
				backNoticeToast.cancel();
			}
			super.onBackPressed();
		}
    }
    
    @Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.scan_xmit, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.itemApiTest) {
			Intent intent = new Intent(ScanXmit2.this, CardhuApiTest.class);
			Bundle bundle = new Bundle();
            bundle.putBoolean(Key_Fullscreen_FullScreen,fullscreen_isFullScreen);
            bundle.putBoolean(Key_Fullscreen_StatusBar,fullscreen_statusBarEnabled);
            bundle.putBoolean(Key_Fullscreen_HomeKey,fullscreen_homeKeyEnabled);
            bundle.putBoolean(Key_Fullscreen_BackKey,fullscreen_backKeyEnabled);
            bundle.putBoolean(Key_Fullscreen_MenuKey,fullscreen_menuKeyEnabled);
            bundle.putString(Key_Fullscreen_Keycodes,fullscreen_keycodes);
            bundle.putBoolean(Key_Fullscreen_KeycodesEnabled,fullscreen_keycodesEnabled);
            intent.putExtras(bundle);
            startActivity(intent);
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	@Override
    protected void onPause() {
        super.onPause();
        Intent mIntent = new Intent();
        mIntent.putExtra(PDA_STATUSBAR, "on");
        mIntent.setAction(INTENT_STATUSBAR);
        sendBroadcast(mIntent);
    }
	
    private void sendDisKeyIntent(String tag, String value,String keycodes) {
        Intent mIntent = new Intent();
        mIntent.putExtra(tag, value);
        if (tag.equals(PDA_KEY_DIS)) {
            mIntent.putExtra(PDA_KEY_DIS_STRING, keycodes);
        }
        mIntent.setAction(INTENT_KEY_DISABLE);
        sendBroadcast(mIntent);
    }
    
    private void executeFullscreenSettings(String str){
    	String[] subStrs = str.split(";");
    	for(String subStr:subStrs){
    		String[] strPair = subStr.split(":");
    		if(strPair.length>=2){
    			switch (strPair[0]) {
				case "fullscreen":
					setFullscreen(Boolean.parseBoolean(strPair[1]));
					break;
				case "statusbar":
					setStatusbar(Boolean.parseBoolean(strPair[1]));
					break;
				case "homekey":
					setKey_Home(Boolean.parseBoolean(strPair[1]));						
				case "backkey":
					setKey_Back(Boolean.parseBoolean(strPair[1]));
					break;
				case "menukey":
					setKey_Menu(Boolean.parseBoolean(strPair[1]));
					break;
				case "keycodes":
					Builder bulder = new AlertDialog.Builder(ScanXmit2.this);
					bulder.setTitle("Info");
				    bulder.setMessage("Keycodes:"+strPair[1]+"; "+strPair[2]);
				    bulder.setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
				        public void onClick(DialogInterface dialog, int which) { 
				            dialog.cancel();
				        }
				     });
				    bulder.setIcon(android.R.drawable.ic_dialog_alert);
				    AlertDialog alert11 = bulder.create();
				    alert11.show();
					setKeys_enable_disable(strPair[1], Boolean.parseBoolean(strPair[2]));
					
					break;
				default:
					break;
				}
    		}
    	}
    }
        
//    private void setAlarmManagerToWakeUp(int sleepInterval){
//    	Calendar cal = Calendar.kgetInstance();
//	    cal.add(Calendar.MILLISECOND, sleepInterval);	 
//	    Intent intent = new Intent(this, WakeupReceiver.class);
//	    intent.putExtra("action", "wakeup");	 
//	    PendingIntent pi = PendingIntent.getBroadcast(this, 1, intent, PendingIntent.FLAG_ONE_SHOT);	         
//	    AlarmManager am = (AlarmManager) getSystemService(Context.ALARM_SERVICE);
//	    am.set(AlarmManager.RTC_WAKEUP, cal.getTimeInMillis(), pi);
//    }
}


