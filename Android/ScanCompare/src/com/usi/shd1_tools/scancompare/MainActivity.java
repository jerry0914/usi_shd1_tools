package com.usi.shd1_tools.scancompare;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Timer;
import java.util.TimerTask;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.symbol.scanning.ScannerConfig;
import com.symbol.scanning.ScannerConfig.CheckDigit;
import com.symbol.scanning.ScannerConfig.CheckDigitScheme;
import com.symbol.scanning.ScannerConfig.CodeIdType;
import com.symbol.scanning.ScannerConfig.CouponReport;
import com.symbol.scanning.ScannerConfig.Inverse1DMode;
import com.symbol.scanning.ScannerConfig.Isbt128ContactMode;
import com.symbol.scanning.ScannerConfig.PickList;
import com.symbol.scanning.ScannerConfig.Preamble;
import com.symbol.scanning.ScannerConfig.SecurityLevel;
import com.symbol.scanning.ScannerConfig.SupplementalMode;
import com.symbol.scanning.ScannerConfig.UccLinkMode;
import com.symbol.scanning.ScannerConfig.VerifyCheckDigit;
import com.usi.shd1_tools.commonlibrary.Logger;
import com.usi.shd1_tools.commonlibrary.SystemUtility;
import com.usi.shd1_tools.scannerlibrary.*;
import com.usi.shd1_tools.scancompare.R.id;
import com.usi.shd1_tools.scannerlibrary.ScannerConfigSettingCommand;


public class MainActivity extends Activity {

//	private final String saveInstanceStateKey_TestCaseList = "com.usi.shd1_tools.scancompare.lstTestCases";
//	private final String saveInstanceStateKey_TestPlanName = "com.usi.shd1_tools.scancompare.testPlanName";
//	private final String saveInstanceStateKey_TestCaseSelectedIndex = "com.usi.shd1_tools.scancompare.testCaseSelectedIndex";
	private final String logPath = "/storage/sdcard0/usi/shd1_tools/ScanCompare/";
    private static ScannerController scanCol;

	public static EditText txtTpPath =null;
	public static ProgressBar prgbarTpLoading = null;
	public static TextView txtProgress = null;
	public static LinearLayout layoutProgress = null;

    private FileChosenReceiver fcReceiver;
    private UiDoEventReceiver uiDoEventReceiver;
    
    private static boolean isEnableDisableEachScan = false;
    public static String testplanPath = "";
    public static String strTcName;
    public static String strTcID;
    public static String strTcDescritption;
    private static String strDecodedResult = "";
    public static String strExpectedResult = "";
    private Timer decodeTimeOut = null;
    private Context currentContext = null;
    
    public static Button btnScan;
    public static Button btnNext;
    public static Button btnPrevious;
    public static Button btnLoadTestPlan;
    public static ProgressBar pbWaiting;
    
    public static Spinner spinnerTestCases;
    public static TextView txtTcName;
    public static TextView txtTcID;
    public static TextView txtTcDescription;
    public static TextView txtTcExpectedResult;
    public static TextView txtDecoded;
    private static boolean isDecodeListenerRegisted = false;
    //private static final String KEY_SIGNAL_STRENGTH = "signal_strength";
    private String[] asciiConvertTable=new String[]
    {												"",//"<NUL>", 
										    		"<SOH>", 
										    		"<STX>", 
										    		"<ETX>", 
										    		"<EOT>", 
										    		"<ENQ>", 
										    		"<ACK>", 
										    		"<BEL>", 
										    		"<BS>" , 
										    		"<HT>" ,
										    		"<LF>" , 
										    		"<VT>" , 
										    		"<FF>" , 
										    		"<CR>" , 
										    		"<SO>" , 
										    		"<SI>" , 
										    		"<DLE>", 
										    		"<DC1>", 
										    		"<DC2>", 
										    		"<DC3>", 
										    		"<DC4>", 
										    		"<NAK>", 
										    		"<SYN>", 
										    		"<ETB>", 
										    		"<CAN>", 
										    		"<EM>" ,
										    		"<SUB>", 
										    		"<ESC>", 
										    		"<FS>" , 
										    		"<GS>" , 
										    		"<RS>" , 
										    		"<US>" , 
										    		"",//" ",
										    		"",//"!",
										    		"",//""",
										    		"",//"#",
										    		"",//"$",
										    		"",//"%",
										    		"",//"&",
										    		"",//"'",
										    		"",//"(",
										    		"",//")",
										    		"",//"*",
										    		"",//"+",
										    		"",//",",
										    		"",//"-",
										    		"",//".",
										    		"",//"/",
										    		"",//"0",
										    		"",//"1",
										    		"",//"2",
										    		"",//"3",
										    		"",//"4",
										    		"",//"5",
										    		"",//"6",
										    		"",//"7",
										    		"",//"8",
										    		"",//"9",
										    		"",//":",
										    		"",//";",
										    		"",//"<",
										    		"",//"=",
										    		"",//">",
										    		"",//"?",
										    		"",//"@",
										    		"",//"A",
										    		"",//"B",
										    		"",//"C",
										    		"",//"D",
										    		"",//"E",
										    		"",//"F",
										    		"",//"G",
										    		"",//"H",
										    		"",//"I",
										    		"",//"J",
										    		"",//"K",
										    		"",//"L",
										    		"",//"M",
										    		"",//"N",
										    		"",//"O",
										    		"",//"P",
										    		"",//"Q",
										    		"",//"R",
										    		"",//"S",
										    		"",//"T",
										    		"",//"U",
										    		"",//"V",
										    		"",//"W",
										    		"",//"X",
										    		"",//"Y",
										    		"",//"Z",
										    		"",//"[",
										    		"",//"\",
										    		"",//"]",
										    		"",//"^",
										    		"",//"_",
										    		"",//"`",
										    		"",//"a",
										    		"",//"b",
										    		"",//"c",
										    		"",//"d",
										    		"",//"e",
										    		"",//"f",
										    		"",//"g",
										    		"",//"h",
										    		"",//"i",
										    		"",//"j",
										    		"",//"k",
										    		"",//"l",
										    		"",//"m",
										    		"",//"n",
										    		"",//"o",
										    		"",//"p",
										    		"",//"q",
										    		"",//"r",
										    		"",//"s",
										    		"",//"t",
										    		"",//"u",
										    		"",//"v",
										    		"",//"w",
										    		"",//"x",
										    		"",//"y",
										    		"",//"z",
										    		"",//"{",
										    		"",//"|",
										    		"",//"}",
										    		"",//"~",
										    		"<DEL>"
											    	};
   
    @Override
	public boolean onCreateOptionsMenu(Menu menu){
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.scan_compare, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item){
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			Intent intent = new Intent(MainActivity.this, ColumnsSettingsActivity.class);
		    startActivity(intent);
			return true;
		}
		else if(id == R.id.action_scanConfigExport){
			TestplanParser.ExportScanConfig(currentContext,"/sdcard/usi/shd1_tools/ScanCompare/");
			return true;
		}
			
		return super.onOptionsItemSelected(item);
	}

    @Override
    protected void onCreate(Bundle savedInstanceState){
		super.onCreate(savedInstanceState);
		//requestWindowFeature(Window.FEATURE_INDETERMINATE_PROGRESS);
		setContentView(R.layout.activity_main);
		Logger.Start(logPath,32);
		Logger.CurrentLogLevel = Logger.Log_Level.Verbose;
		currentContext = getApplicationContext();
		//btnReload = (Button)findViewById(R.id.btnReload);
		btnNext = (Button)findViewById(R.id.btnNext);
		btnPrevious = (Button)findViewById(R.id.btnPrevious);
		btnLoadTestPlan = (Button)findViewById(R.id.btnLoadTestPlan);
		txtTpPath = (EditText)findViewById(R.id.txtTpPath);
		prgbarTpLoading = (ProgressBar)findViewById(R.id.progressBar1);
		txtProgress = (TextView)findViewById(id.txtProgress);
		spinnerTestCases = (Spinner)findViewById(id.spinnerTestcases);
		layoutProgress = (LinearLayout)findViewById(id.linearLayoutProgressBar);
		txtTcName = (TextView)findViewById(id.txtTCNameCol);
		txtTcID = (TextView)findViewById(id.txtTcID);
		txtTcDescription = (TextView)findViewById(id.txtTcDescription);
		txtTcExpectedResult = (TextView)findViewById(id.txtExpectedResultCol);
		txtDecoded = (TextView)findViewById(id.txtDecoded);	
		btnScan = (Button)findViewById(id.btnScan);
		pbWaiting = (ProgressBar)findViewById(id.progressBar2);
		scanCol = new ScannerController();		
		//scannerEnable(true);
		btnScan.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {
				scan(false,3000);
				//updateScanResult("This message is for debug only");
			}
		});
		txtTpPath.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {
				Intent intent = new Intent(MainActivity.this, FileChooser.class);
			    startActivity(intent);		
			}
		});
		btnNext.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {
				int newIndex  = spinnerTestCases.getSelectedItemPosition()+1;
				{
					if(newIndex < spinnerTestCases.getCount())
					{
						spinnerTestCases.setSelection(newIndex);
					}
				}							
			}
		});
		btnPrevious.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {
				spinnerTestCases.setSelection(spinnerTestCases.getSelectedItemPosition()-1);			
			}
		});
		btnLoadTestPlan.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				TestplanParser.LoadTestPlan(currentContext,testplanPath);				
			}
		});
		try {
			spinnerTestCases.setOnItemSelectedListener(new OnItemSelectedListener(){
				@Override
				public void onItemSelected(AdapterView<?> arg0, View arg1,int position, long arg3) {
					loadTestCaseInfo(position);
				}
				@Override
				public void onNothingSelected(AdapterView<?> arg0) {
					Toast.makeText(MainActivity.this, "Nothing is selected.", Toast.LENGTH_SHORT).show();					
				}				
			});			
		} catch (Exception e){
			Log.e("ATDebug", "Set spinner onItemClickListener exception, message= "+e.getMessage());
			Log.e("ATDebug", "Set spinner onItemClickListener exception, stack trace = "+e.getStackTrace().toString());
		}
		
		fcReceiver =  new FileChosenReceiver();
		IntentFilter iFilger = new IntentFilter();
		iFilger.addAction(FileChooser.ACTION_FileChosen);
		registerReceiver(fcReceiver, iFilger);		
		uiDoEventReceiver = new UiDoEventReceiver();
		IntentFilter iFilger2 = new IntentFilter();
		iFilger2.addAction(TestplanParser.Action_LoadTestPlanProgressUpdate);
		iFilger2.addAction(TestplanParser.Action_LoadTestCaseUiUpdate);
		registerReceiver(uiDoEventReceiver, iFilger2);
		SystemUtility.StayAwake(getWindow(), true);
    }
       
    private void loadTestCaseInfo(int selectedIndex){
    	uiRefresh_testCaseLoading(true);    	
    	TestCase tcSelected = TestplanParser.TestcaseList.get(selectedIndex);
		if(tcSelected!=null)
		{	
			strDecodedResult = "";
			strTcID = tcSelected.ID;
			strTcName =tcSelected.Name;
			strTcDescritption = tcSelected.Description;
			strExpectedResult = tcSelected.ExpectedResult;
			//scanCol.ResetToDefault();
			ArrayList<ScannerConfigSettingCommand> scCmds = tcSelected.ScanConfigSettingCommands;
			Logger.WriteLog(Logger.Log_Level.Information, "TcInfo","TcName :"+tcSelected.Name+", TcID :"+tcSelected.ID+", ExpectedResult :"+tcSelected.ExpectedResult);																
			if(scCmds!=null && scCmds.size()>0){
				this.RunScannerConfigCommand(scCmds);
//				for(int i=0;i<scCmds.size();i++){
//					this.runScannerConfigCommand.get(i));
//				}
			}
		}
		uiRefresh_testCaseLoading(false);
    }

    private void uiRefresh_testCaseLoading(boolean loadingFlag){
        Intent intentBroadcast = new Intent();
    	intentBroadcast.setAction(TestplanParser.Action_LoadTestCaseUiUpdate);
    	intentBroadcast.putExtra(TestplanParser.ExtraHeader_tcLoadingFlag, loadingFlag);				
    	currentContext.sendBroadcast(intentBroadcast);
    }

//	private void exportSanConfigParams(String outFilePath){
//		ArrayList<TestCase> aryTestCases = TestplanParser.getTestCases();
//		if(aryTestCases!=null && aryTestCases.size()>0)
//		{	
//			FileOutputStream outStream = null;
//			BufferedWriter writer = null;
//			String tcInfoWithScanCfgCommand = "";
//			try {
//				outStream = new FileOutputStream(outFilePath);
//				writer = new BufferedWriter(new OutputStreamWriter(outStream));	
//			} catch (FileNotFoundException e1) {
//				e1.printStackTrace();
//			}			
//
//			for(int i=0;i<aryTestCases.size();i++)
//			{
//				tcInfoWithScanCfgCommand = "";
//				TestCase currentTestCase = aryTestCases.get(i);
//				tcInfoWithScanCfgCommand += currentTestCase.Name+",";
//				tcInfoWithScanCfgCommand += currentTestCase.ID+",";
//				if(currentTestCase.ScanConfigSettingCommands.size()>0)
//				{
//					for(int j=0;j<currentTestCase.ScanConfigSettingCommands.size();j++)
//					{
//						ScannerConfigSettingCommand currentCfg =  currentTestCase.ScanConfigSettingCommands.get(j);
//						if(j==0)
//						{
//							tcInfoWithScanCfgCommand+="\"";
//						}
//						tcInfoWithScanCfgCommand+=currentCfg.Category.toString()+"-";
//						tcInfoWithScanCfgCommand+=currentCfg.Command+"=";
//						tcInfoWithScanCfgCommand+=currentCfg.Parameters.get(0);
//						tcInfoWithScanCfgCommand+="\n";
//						if(j==currentTestCase.ScanConfigSettingCommands.size()-1)
//						{
//							tcInfoWithScanCfgCommand = tcInfoWithScanCfgCommand.trim();
//							tcInfoWithScanCfgCommand+="\"";
//						}
//					}
//				}	
//				else
//				{
//					tcInfoWithScanCfgCommand+="None";
//				}
//				tcInfoWithScanCfgCommand = tcInfoWithScanCfgCommand.trim();
//				try 
//				{				
//					writer.write(tcInfoWithScanCfgCommand+"\n");
//				} 
//				catch (Exception e) {
//				e.printStackTrace();					
//				}
//			}
//			if(outStream!=null)
//			{
//				try {
//					outStream.close();
//				} catch (IOException e) {
//					e.printStackTrace();
//				}
//			}
//			if(writer!=null){
//				try {
//					writer.close();
//				} catch (IOException e) {
//					e.printStackTrace();
//				}
//			}
//		}
//	}
    
    private void scannerEnable(boolean enable_flag){
		if(enable_flag)
		{
	    	if(scanCol==null)
	    	{
	    		scanCol = new ScannerController();
	    	}
			try
			{
		    	if(!scanCol.IsEnable())
		    	{
					scanCol.Enable();
					if(!isDecodeListenerRegisted){
				    	scanCol.addScannerDecodedMessageListener(scannerDecodedListener);
						scanCol.addScannerStateChangedeListener(scanStateChangeListener);
						isDecodeListenerRegisted = true;
						Logger.WriteLog(Logger.CurrentLogLevel.Information,"Start", "Scanner is enabled.", true);						
					}
		    	}
	    	}
	    	catch(Exception esx)
	    	{
	    		Logger.WriteLog(Logger.CurrentLogLevel.Error,"Error", "Exception ocrror when scanner enabling, message = "+esx.toString(), true);
	    	}
		}
		else 
	    {
			if(scanCol!=null)
			{
				scanCol.removeScannerDecodedMessageListener(scannerDecodedListener);
				scanCol.removeScannerStateChangedeListener(scanStateChangeListener);
				scanCol.Disable();
			}
			//Log.d("ATDebug","Scanner disable");
	    	Logger.WriteLog(Logger.CurrentLogLevel.Information,"End", "Scanner is disabled.", true);
		} 
    }
          
    private ScannerDecodedMessageListener scannerDecodedListener = new ScannerDecodedMessageListener()
    {
    	@Override
		public void DecodeMessage(ScannerDecodedMessageObject arg0) 
		{
			strDecodedResult = arg0.DecodedMessage();
			//Toast.makeText(currentContext,strDecodedResult , Toast.LENGTH_SHORT).show();
			strDecodedResult = strDecodedResult.replace(String.valueOf((char)0), "");
			for(int i=1;i<=127;i++)
	    	{
				if(asciiConvertTable[i].length()>0)	
				{
					strDecodedResult = strDecodedResult.replace(String.valueOf((char)i), asciiConvertTable[i]);
				}
	    	}
			Logger.WriteLog(Logger.Log_Level.Debug,"TransferData","Transfer special charactor in decode data :" + strDecodedResult);
			updateScanResult(strDecodedResult);
			if(isEnableDisableEachScan)
			{
				scannerEnable(false);
			}
	    	Log.d("ATDebug","Scanner enable");
		}
	};
	
	private ScannerStateChangedEventListener scanStateChangeListener = new ScannerStateChangedEventListener()
	{
		public void StateChangedMessage(ScannerStateChangedEventObject arg0) {			
		}
		
	};
    
    private void scan(boolean isEnableDisable,int timeout){
    	if(!scanCol.IsEnable())
    	{
    		scannerEnable(true);
    	}
    	if(scanCol.Is_Scanning())
    	{
    		scanCol.CancelScan();
    	}
    	isEnableDisableEachScan =  isEnableDisable;
    	decodeTimeOut = new Timer();
		decodeTimeOut.schedule(new timeStop(), timeout);
    	strDecodedResult = "";
    	scanCol.Scan();
    }
    
    private class timeStop extends TimerTask{
	    public void run()
	    {	    	
			decodeTimeOut.cancel();
			scanCol.CancelScan();
			if(isEnableDisableEachScan)
	    	{
	    		scannerEnable(false);
	    	}	    	
	    }
	}
      
    private void updateScanResult(String result){
    	//gvTestCaseInfo = null;
    	txtDecoded.setText(result);
    	if(strExpectedResult.length()>0)
    	{
    		String[] expectedStrings = strExpectedResult.split(" OR ");
    		boolean isPass= false;
    		for(int i=0;i<expectedStrings.length;i++)
    		{
    			if(expectedStrings[i].equals(strDecodedResult))
    			{
    				isPass = true;
    				break;
    			}
    		}    			
	    	if(isPass)
	    	{
	    		txtDecoded.setTextColor(Color.GREEN);
	    		Toast.makeText(currentContext, "PASS", Toast.LENGTH_SHORT).show();
 	    		spinnerTestCases.setSelection(spinnerTestCases.getSelectedItemPosition()+1);
	    	}
	    	else 
	    	{
	    		txtDecoded.setTextColor(Color.RED);
	    		Toast.makeText(currentContext, "FAIL", Toast.LENGTH_SHORT).show();
			}
    	}
    }
    
    private void RunScannerConfigCommand(ArrayList<ScannerConfigSettingCommand> cmds){
    	if(scanCol!=null)
    	{
    		//scanCol.ResetToDefault();
    		ScannerConfig currentConfig = scanCol.GetScannerConfig();
    		for(int i=0;i<cmds.size();i++)
    		{
	    		ScannerConfigSettingCommand cmd = cmds.get(i);
		    	Class scannerClass = scanCol.getClass();
		    	Class[] args = null;
		    	Method method = null;
		    	Object[] objParams = null;
		    	String methodNameFullName = "";
		    	String methodName_Header = "";
		    	String methodName_Middle = "";
		    	String methodName_Rear = "";
		    	switch(cmd.Category){    	
		    		case Toggle:
		    			methodName_Header = "SetDecoderParameters";
		    			methodName_Middle = cmd.Command.toUpperCase();
		    			methodName_Rear = "Enable";
		    			//{{ Get parameter types of Toggle command
		    			args = new Class[2];
		    			objParams = new Object[2];
		    			args[0] = ScannerConfig.class;
		    			objParams[0] = currentConfig;
		    			args[1] = boolean.class;
		    			objParams[1] = (Boolean)(cmd.Parameters.get(0).toUpperCase().contains("ENABLE") || cmd.Parameters.get(0).toUpperCase().contains("TRUE"))?true:false;
		    			//}} Get parameter types of Toggle command
		    			break;
		    		case DecodeParameter:
		    			methodName_Header = "SetDecoderParameters";
		    			methodName_Middle = cmd.Command.toUpperCase();;
		    			methodName_Rear = "";
		    			//{{ Get parameter types of DecodeParameter command
		    			if(methodName_Middle.contains("Min".toUpperCase())||
		    			   methodName_Middle.contains("Max".toUpperCase()))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = int.class;
			    			objParams[1] = Integer.valueOf(cmd.Parameters.get(0));
		    			}
		    			else if(methodName_Middle.contains("VERIFYCHECKDIGIT")){
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
		    				if(methodName_Middle.contains("CODE11") ||
		    				   methodName_Middle.contains("I2OF5"))
		    				{
		    					args[1] = VerifyCheckDigit.class;
		    					VerifyCheckDigit param0 = VerifyCheckDigit.NO;
		    					String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
		    					if(strParam0.equals("NO")||
		    					   strParam0.equals("NONE"))
		    					{
		    						param0 = VerifyCheckDigit.NO;
		    					}
		    					else if(strParam0.equals("ONE") ||
		    							strParam0.equals("1") ||
		    							strParam0.equals("USS"))
		    					{
		    						param0 = VerifyCheckDigit.ONE;
		    					}
		    					else if(strParam0.equals("TWO") ||
		    							strParam0.equals("2") ||
		    							strParam0.equals("OPCC"))
		    					{
		    						param0 = VerifyCheckDigit.TWO;		    						
		    					}	    					
		    					objParams[1] = param0;
		    				}
		    				else
		    				{
				    			args[1] = boolean.class;
				    			objParams[1] = (Boolean)(cmd.Parameters.get(0).toUpperCase().equals("ENABLE")|| cmd.Parameters.get(0).toUpperCase().equals("TRUE"))?true:false;
		    				}   				
		    			}	    			                 
		    			else if (methodName_Middle.contains("ISBT128CONCATMODE".toUpperCase()))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;		    				
			    			args[1] = Isbt128ContactMode.class;
			    			String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
			    			if(strParam0.equals("ALWAYS"))
			    			{
			    				objParams[1] = Isbt128ContactMode.ALWAYS;
			    			}
			    			else if(strParam0.equals("AUTO"))
			    			{
			    				objParams[1] = Isbt128ContactMode.AUTO;
			    			}
			    			else if(strParam0.equals("NEVER") ||
			    					strParam0.equals("NONE"))
			    			{
			    				objParams[1] = Isbt128ContactMode.NEVER;
			    			}
			    			
		    			}
		    			else if (methodName_Middle.contains("CouponReportingMode".toUpperCase()))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = CouponReport.class;
			    			CouponReport report =  CouponReport.OLD;
			    			if(cmd.Parameters.get(0).toUpperCase().equals("OLD"))
			    			{
			    				 report =  CouponReport.OLD;
			    			}
			    			else if(cmd.Parameters.get(0).toUpperCase().equals("NEW"))
			    			{
			    				report =  CouponReport.NEW;
			    			}
			    			else if(cmd.Parameters.get(0).toUpperCase().equals("BOTH"))
			    			{
			    				report =  CouponReport.BOTH;
			    			}
			    			objParams[1] = report;
		    			}
		    			else if (methodName_Middle.contains("ReportCheckDigit".toUpperCase()) ||
		    					 methodName_Middle.contains("Editing".toUpperCase())||
		    					 methodName_Middle.contains("FullAsci".toUpperCase()) ||
		    					 methodName_Middle.contains("ReportCode32Prefi".toUpperCase())||
		    					 methodName_Middle.contains("ConvertToCode32".toUpperCase()) ||
		    					 methodName_Middle.contains("Redundancy".toUpperCase()) ||
		    					 methodName_Middle.contains("ConvertToUPCA".toUpperCase()) ||
		    					 methodName_Middle.contains("Coupon".toUpperCase()) ||
		    					 methodName_Middle.contains("Bookland".toUpperCase())||
		    					 methodName_Middle.contains("ISBT128".toUpperCase())||
		    					 methodName_Middle.contains("Code128_EAN128".toUpperCase())||
		    					 methodName_Middle.contains("Code128_CheckISBTTable".toUpperCase())||
		    					 methodName_Middle.contains("ConvertToEAN13".toUpperCase()))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = boolean.class;
	    					objParams[1] = (Boolean)(cmd.Parameters.get(0).toUpperCase().equals("ENABLE")|| cmd.Parameters.get(0).toUpperCase().equals("TRUE"))?true:false;
		    			}	    			
		    			else if (methodName_Middle.contains("SupplementalMode".toUpperCase()))	    				
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = SupplementalMode.class;
			    			String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
			    			SupplementalMode param0 = SupplementalMode.AUTO;
			    			if(strParam0.equals("NO") || strParam0.equals("NONE"))
	    					{
	    						param0 = SupplementalMode.NO;
	    					}
	    					else if(strParam0.equals("ALWAYS"))
	    					{
	    						param0 = SupplementalMode.ALWAYS;
	    					}
	    					else if(strParam0.equals("AUTO"))
	    					{
	    						param0 =  SupplementalMode.AUTO;
	    					}
	    					else if(strParam0.equals("S_378_379"))
	    					{
	    						param0 =  SupplementalMode.S_378_379;
	    					}
	    					else if(strParam0.equals("S_414_419_434_439"))
	    					{
	    						param0 =  SupplementalMode.S_414_419_434_439;
	    					}
	    					else if(strParam0.equals("S_977"))
	    					{
	    						param0 =  SupplementalMode.S_977;
	    					}
	    					else if(strParam0.equals("S_978_979"))
	    					{
	    						param0 =  SupplementalMode.S_978_979;
	    					}
	    					else if(strParam0.equals("SMART"))
	    					{
	    						param0 =  SupplementalMode.SMART;
	    					}
			    			objParams[1] = param0;
		    			}
		    			else if (methodName_Middle.contains("Preamble".toUpperCase()))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = Preamble.class;
			    			String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
			    			Preamble param0 = Preamble.NONE;
			    			if(strParam0.startsWith("NO"))
	    					{
	    						param0 = Preamble.NONE;
	    					}
	    					else if(strParam0.startsWith("SYS"))
	    					{
	    						param0 = Preamble.SYS_CHAR;
	    					}
	    					else if(strParam0.startsWith("COUNTRY"))
	    					{
	    						param0 =  Preamble.COUNTRY_AND_SYS_CHAR;
	    					}
			    			objParams[1] = param0;
		    			}
		    			else if (methodName_Middle.contains("CheckDigitScheme".toUpperCase())){
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
		    				args[1] = CheckDigitScheme.class;
		    				CheckDigitScheme param0 = CheckDigitScheme.MOD_10_10;
	    					String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
	    					if(strParam0.contains("MOD11")||strParam0.contains("MOD_11"))
							{
	    						param0 = CheckDigitScheme.MOD_11_10;
							}
	    					else
	    					{
	    						param0 = CheckDigitScheme.MOD_10_10;
	    					}    					  
	    					objParams[1] = param0;
	    				}
		    			else if (methodName_Middle.equals("CheckDigit".toUpperCase()) ||
		    					methodName_Middle.contains("CheckDigitCount".toUpperCase())){
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
		    				args[1] = CheckDigit.class;
		    				CheckDigit param0 = CheckDigit.ONE;
	    					String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
	    					if(strParam0.equals("1") || strParam0.equals("ONE"))
							{
	    						param0 = CheckDigit.ONE;
							}
	    					else if(strParam0.equals("2") || strParam0.equals("TWO"))
	    					{
	    						param0 = CheckDigit.TWO;
	    					}    					  
	    					objParams[1] = param0;	    				
		    			}		    			
		    			else if (methodName_Middle.contains("INVERSE"))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = Inverse1DMode.class;
			    			Inverse1DMode param0 = Inverse1DMode.AUTO;
			    			String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
	    					if(strParam0.equals("AUTO"))
							{
	    						param0 = Inverse1DMode.AUTO;
							}
	    					else if (strParam0.equals("ENABLE"))
	    					{
	    						param0 = Inverse1DMode.ENABLED;
	    					}
	    					else
	    					{
	    						param0 = Inverse1DMode.DISABLED;
	    					}
	    					objParams[1] = param0;			    			
		    			}
		    			
		    			else if(methodName_Middle.contains("SECURITYLEVEL"))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = SecurityLevel.class;
			    			SecurityLevel sLv = SecurityLevel.LEVEL_0;
			    			String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
			    			if(		strParam0.contains("1")||
			    					strParam0.contains("ONE"))
		    			   {
		    				   sLv = SecurityLevel.LEVEL_1;
		    			   }
			    			else if(strParam0.contains("2")||
			    					strParam0.contains("TWO"))
		    			   {
		    				   sLv = SecurityLevel.LEVEL_2;
		    			   }
			    			else if(strParam0.contains("3")||
			    					strParam0.contains("THREE"))
		    			   {
		    				   sLv = SecurityLevel.LEVEL_3;
		    			   }
			    			else if(strParam0.contains("0")||
				    			    strParam0.contains("ZERO") ||
				    			    strParam0.contains("NONE"))
		    			   {
		    				   sLv = SecurityLevel.LEVEL_0;
		    			   }
		    			}
		    			else if (methodName_Middle.contains("UCCLINKMODE"))
		    			{
		    				args = new Class[2];
			    			objParams = new Object[2];
			    			args[0] = ScannerConfig.class;
			    			objParams[0] = currentConfig;
			    			args[1] = UccLinkMode.class;
			    			UccLinkMode param0 = UccLinkMode.ALWAYS_LINKED;
			    			String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
			    			if(strParam0.contains("ALWAYS"))
			    			{
			    				param0 = UccLinkMode.ALWAYS_LINKED;
			    			}
			    			else if(strParam0.contains("AUTO"))
			    			{
			    				param0 = UccLinkMode.AUTO_DISCRIMINATE;
			    			}
			    			else if(strParam0.contains("IGNORED"))
			    			{
			    				param0 = UccLinkMode.LINK_FLAG_IGNORED;
			    			}
			    			objParams[1] = param0;
		    			}
		    			//}} Get parameter types of DecodeParameter command
		    			break;
		    		case ScanParameter:
		    			methodName_Header = "SetScanParameters";
		    			methodName_Middle = cmd.Command.toUpperCase();
		    			methodName_Rear = "";		    			
		    			//{{ Get parameter types of ScanParameter command	
		    			switch (methodName_Middle)
		    			{
		    				case "CODEID":
		    					args = new Class[2];
				    			objParams = new Object[2];
				    			args[0] = ScannerConfig.class;
				    			objParams[0] = currentConfig;
		    	    			args[1] = CodeIdType.class;
		    	    			CodeIdType param0 = CodeIdType.NONE;
		    	    			String strParam0 = cmd.Parameters.get(0).trim().toUpperCase();
				    			if(strParam0.startsWith("NO"))
		    					{
		    						param0 = CodeIdType.NONE;
		    					}
				    			else if (strParam0.equals("AIM"))
				    			{
				    				param0 = CodeIdType.AIM;
				    			}
				    			else if (strParam0.equals("SYMBOL"))
				    			{
				    				param0 = CodeIdType.SYMBOL;
				    			}
				    			objParams[1] = param0;
				    		break;
		    			}
		    			//}} Get parameter types of ScanParameter command
		    			break;
	    		case ReaderParameter:
	    			methodName_Header = "SetReaderParameters";
	    			methodName_Middle = cmd.Command.toUpperCase();;
	    			methodName_Rear = "";
	    			//{{ Get parameter types of ReaderParameter command
	    			switch (methodName_Middle)
	    			{
	    			case "PICKLIST":
    				case "PICKLISTMODE":
    					args = new Class[2];
		    			objParams = new Object[2];
		    			args[0] = ScannerConfig.class;
		    			objParams[0] = currentConfig;		    			
		    			args[1] = PickList.class;
		    			objParams[1] = (PickList)((cmd.Parameters.get(0).toUpperCase().equals("ENABLE") || 
		    									   cmd.Parameters.get(0).toUpperCase().equals("TRUE") ||
		    									   cmd.Parameters.get(0).toUpperCase().equals("HARDWARE") ||
		    									   cmd.Parameters.get(0).toUpperCase().equals("SOFTWARE") 
		    									  )?PickList.ENABLED:PickList.DISABLED);
    					break;
    				case "INVERSEID":
    					args = new Class[2];
		    			objParams = new Object[2];
		    			args[0] = ScannerConfig.class;
		    			objParams[0] = currentConfig;
    					args[1] = Inverse1DMode.class;
    					Inverse1DMode ivMode = Inverse1DMode.AUTO;
    					switch(cmd.Parameters.get(0).toUpperCase())
    					{
    						case "AUTO":
    							ivMode = Inverse1DMode.AUTO;
    							break;
    						case "ENABLE":
    						case "TRUE":
    							ivMode = Inverse1DMode.ENABLED;
    							break;
    						default:
    							ivMode = Inverse1DMode.DISABLED;
    							break;
    					}
    					objParams[1] = ivMode;
    					break;
	    			}
	    		}
    			//}} Get parameter types of ReaderParamet command
    			methodNameFullName = methodName_Header+"_"+methodName_Middle+(methodName_Rear.length()>0?("_"+methodName_Rear):"");
    			try{
    				method = scannerClass.getMethod(methodNameFullName, args);	    		
    			}
		    	catch (NoSuchMethodException nsmEx){
		    		method = null;
		    	}
		    	if(method!=null){
		    		try {
						currentConfig = (ScannerConfig) method.invoke(scanCol, objParams);
					} catch (IllegalAccessException | IllegalArgumentException
							| InvocationTargetException e) {
						e.printStackTrace();
					}
		    	}
		    	else
		    	{
		    		Logger.WriteLog(Logger.Log_Level.Warning,"Undefine", "Can't fine the scan config command, message ="+ methodNameFullName);
		    	}
		    }
    		scanCol.SetScannerConfig(currentConfig);
    	}
    }
    
    @Override
    protected void onRestart(){
		super.onRestart();
    }
    
    @Override
    protected void onResume(){
    	scannerEnable(true);
    	super.onResume();
    }
    
    @Override
    protected void onPause(){
    	super.onPause();
    }
    
    @Override
    protected void onStop(){    	
    	super.onStop();
    }
    
    @Override
    protected void onDestroy(){
    	SystemUtility.StayAwake(getWindow(), false);
    	unregisterReceiver(fcReceiver);
    	unregisterReceiver(uiDoEventReceiver);
    	Logger.Stop();
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
			scannerEnable(false);
			continuallyBackkeyClickCount = 0;
			continuallyBackkeyClickTimer.cancel();
			if(backNoticeToast!=null)
			{
				backNoticeToast.cancel();
			}	
			super.onBackPressed();
		}   	
    } 
}

class FileChosenReceiver extends BroadcastReceiver{
	@Override
	public void onReceive(Context context, Intent intent){
		String action = intent.getAction();
		if(action.equals(FileChooser.ACTION_FileChosen))
		{
			String fullpath = intent.getStringExtra(FileChooser.EXTRA_ChosenFileFullPath);
			MainActivity.testplanPath = fullpath;
			MainActivity.txtTpPath.setText(intent.getStringExtra(FileChooser.EXTRA_ChosenFileName));			
		    Log.d("ATDebug","Test plan selected : "+fullpath);
			Intent intentBroadcast = new Intent();
			intentBroadcast.setAction(TestplanParser.Action_LoadTestPlanProgressUpdate);
			intentBroadcast.putExtra(TestplanParser.ExtraHeader_Progress, 0);
			intentBroadcast.putExtra(TestplanParser.ExtraHeader_Maximum, 1);				
			context.sendBroadcast(intentBroadcast);
			MainActivity.btnLoadTestPlan.setEnabled(true);
			TestplanParser.autoDetectColumn(context,MainActivity.testplanPath);
		    //TestplanParser.LoadTestPlan(context,fullpath);
		}	
	}
}

class UiDoEventReceiver extends BroadcastReceiver{
	@Override
	public void onReceive(Context context, Intent intent) {
		String action = intent.getAction();
		if(action.equals(TestplanParser.Action_LoadTestPlanProgressUpdate))
		{
			int progress = intent.getIntExtra(TestplanParser.ExtraHeader_Progress, 0);
			int max = intent.getIntExtra(TestplanParser.ExtraHeader_Maximum, 0);
			if(progress>0 && max >0)
			{
				MainActivity.layoutProgress.setVisibility(android.view.View.VISIBLE);
				MainActivity.prgbarTpLoading.setMax(max);
				MainActivity.prgbarTpLoading.setProgress(progress);
				double progressPercentage = (double)progress/(double)max;				
				MainActivity.txtProgress.setText(String.format("%.01f", progressPercentage*100)+" %");				
				MainActivity.txtTpPath.setEnabled(false);
				MainActivity.spinnerTestCases.setEnabled(false);
				MainActivity.btnScan.setEnabled(false);
				MainActivity.btnNext.setEnabled(false);
				MainActivity.btnPrevious.setEnabled(false);
				MainActivity.btnLoadTestPlan.setEnabled(false);
			}
			if(progress>=max && max>0)
			{
				try {
					Thread.sleep(1000);
				} catch (InterruptedException e) {					
					e.printStackTrace();
				}
				MainActivity.layoutProgress.setVisibility(android.view.View.INVISIBLE);
				MainActivity.btnScan.setEnabled(true);
				MainActivity.spinnerTestCases.setEnabled(true);
				MainActivity.btnNext.setEnabled(true);
				MainActivity.btnPrevious.setEnabled(true);
				MainActivity.txtTpPath.setEnabled(true);
				MainActivity.btnLoadTestPlan.setEnabled(true);
				Toast.makeText(context, "Finished. "+TestplanParser.TestcaseList.size() + " test cases have been loaded.", Toast.LENGTH_SHORT).show();
				showTestCases(context);
			}			
		}
		else if (action.equals(TestplanParser.Action_LoadTestCaseUiUpdate))
		{
			boolean loadingFlag = intent.getBooleanExtra(TestplanParser.ExtraHeader_tcLoadingFlag,false);
			if(loadingFlag)
			{
				MainActivity.txtTcName.setText("");
				MainActivity.txtTcID.setText("");
				MainActivity.txtTcDescription.setText("");
				MainActivity.txtTcExpectedResult.setText("");			
				MainActivity.txtDecoded.setText("");
				MainActivity.pbWaiting.setVisibility(View.VISIBLE);				
			}
			else
			{
				MainActivity.txtTcName.setText(MainActivity.strTcName);
				MainActivity.txtTcID.setText(MainActivity.strTcID);
				MainActivity.txtTcDescription.setText(MainActivity.strTcDescritption);
				MainActivity.txtTcExpectedResult.setText(MainActivity.strExpectedResult);
				MainActivity.pbWaiting.setVisibility(View.INVISIBLE);
			}
			MainActivity.btnScan.setEnabled(!loadingFlag);
			MainActivity.btnNext.setEnabled(!loadingFlag);
			MainActivity.btnPrevious.setEnabled(!loadingFlag);
			MainActivity.btnLoadTestPlan.setEnabled(!loadingFlag);
		}
	}
	
	private void showTestCases(final Context context){
		int tcCount = TestplanParser.TestcaseList.size();
		String[] tcs = new String[tcCount];
		for(int i=0;i<tcCount;i++)
		{
			TestCase tc = TestplanParser.TestcaseList.get(i);
			if(tc!=null)
			{
				tcs[i] = tc.Name + " - " + tc.ID;
			}
		}
		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(context,android.R.layout.simple_spinner_item,tcs);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);		
		MainActivity.spinnerTestCases.setAdapter(dataAdapter);
	}
	
}

