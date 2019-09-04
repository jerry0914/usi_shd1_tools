package com.usi.shd1_tools.scannerlibrary;

import java.util.Date;
import java.util.Enumeration;
import java.util.Timer;
import java.util.TimerTask;
import java.util.Vector;

import android.R.integer;
import android.util.Log;

import com.symbol.scanning.ScanDataCollection;
import com.symbol.scanning.Scanner;
import com.symbol.scanning.Scanner.StatusListener;
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
import com.symbol.scanning.ScannerException;
import com.symbol.scanning.StatusData;
import com.symbol.scanning.Scanner.DataListener;
import com.symbol.scanning.StatusData.ScannerStates;
import com.usi.shd1_tools.commonlibrary.Logger;
import com.usi.shd1_tools.commonlibrary.Logger.Log_Level;

public class ScannerController {
	private Vector<ScannerStateChangedEventListener> statusChangeListeners = null;
	private Vector<ScannerDecodedMessageListener> dataDecodedListeners = null;
    private Scanner scanner;
    public boolean IsEnable(){
    	boolean isEnabled = false;
    	try{
    		isEnabled = scanner.isEnable();
    	}
    	catch (Exception ex){
    		isEnabled = false;
    	}
    	return isEnabled;
    }

    private final String Tag = "ScanController";
    private boolean isScanning = false;
    private ScannerStates _doNotUse_currentState = ScannerStates.DISABLED;
    public int DefaultEnableTimeout=5000;
    public ScannerConfig defaultScannerConfig = null;
    private boolean isListenerRegistered = false;
    public ScannerStates GetCurrentStates(){
    	return _doNotUse_currentState;  
    }
    
    private void setCurrentStates(ScannerStates state){
    	if(_doNotUse_currentState!=state)
    	{
    		_doNotUse_currentState = state;
    		Logger.WriteLog(Tag,"Scanner's state is changed, current state = "+state.toString(),Log_Level.Verbose);
    		sendStatesChangedEvent();
    	}    	
    }
    
    public ScannerController(){
    	scanner = new Scanner();	
    }
    
	private StatusListener scanStatusListener = new StatusListener(){
		@Override
		public void onStatus(StatusData arg0) {
			setCurrentStates(arg0.getState());
		}		  
	};
	
	private DataListener dataDecodedListener = new DataListener(){
		@Override
		public void onData(ScanDataCollection arg0) 
		{
			String data = "";
		    stopDecodeTimer();
			for(int i=0;i<arg0.DataList.size();i++)
			{
				data+=arg0.DataList.get(i).getData();
			}
			//data = data.trim();
			Logger.WriteLog(Tag,"Data is decoded, data = "+data,Log_Level.Information);
			sendDataDecodedEvent(data);
			isScanning = false;			
		}
	};
    
    public boolean Is_Scanning(){
    	return isScanning;
    }
    
    public boolean Enable(){
    	return Enable(DefaultEnableTimeout);
    }
    
    public boolean Enable(final int timeout_inMilliseconds){
    	boolean isTimeout = false;
    	int checkStatusInterval = (int) timeout_inMilliseconds/4;
    	Date startTime = new Date();    	
    	try
    	{
	    	if(scanner==null)
	    	{
	    		scanner = new Scanner();  	
	    	}
	        do{
	        	if(!IsEnable())
	        	{
	        		try
	        		{
	        			scanner.enable();
	        		}catch(Exception ex1){
	        		}
	        		if(!IsEnable())
	        		{
			        	try
			        	{	        		
			        		Thread.sleep(checkStatusInterval);
			        	}
			        	catch (Exception ex){	        		
			        	}
		        		isTimeout = ((new Date().getTime())-startTime.getTime()>timeout_inMilliseconds); 
	        		}
	        	}    	
	        }while(!IsEnable() && !isTimeout);
	        if(IsEnable())
	        {
	        	if(defaultScannerConfig==null)
	        	{
		        	//scanner.resetToDefault();
		        	ScannerConfig tempConfig = scanner.getConfig();
		        	tempConfig.scanParams.audioStreamType = ScannerConfig.AudioStreamType.RINGER;
			    	tempConfig.scanParams.decodeAudioFeedbackUri = "content://media/internal/audio/media/97";
		        	Log.i("AT_API", "***********Scanconfig set*************");			    	
		        	tempConfig.decoderParams.d2of5.length1 = 0;
			    	tempConfig.decoderParams.d2of5.length2 = 14;
			        defaultScannerConfig = tempConfig;
			        scanner.setConfig(defaultScannerConfig);
			    }
	        	if(!isListenerRegistered){
		        	scanner.addStatusListener(scanStatusListener);
				    scanner.addDataListener(dataDecodedListener);
				    isListenerRegistered = true;
	        	}
		        Logger.WriteLog(Tag,"Scanner is enabled successfully.",Log_Level.Information);
	        }
	        else
	        {
	        	Logger.WriteLog(Tag,"It's timeout while the scanner enabling, it's enabled unsuccessfully.",Log_Level.Warning,true);
	        }
    	}
    	catch(Exception ex)
    	{
    		Log.d("AT_Debug", ex.getMessage());
    		Logger.WriteLog(Tag,"There's a exception occur while the scanner enabling, exception message = "+ex.toString(),Log_Level.Error,true);
    	}    	
    	return IsEnable();
    }
    
    public void Disable(){
    	if(scanner!=null)
    	{
    		scanner.removeStatusListener(scanStatusListener);
    		scanner.removeDataListener(dataDecodedListener);
    		isListenerRegistered = false;
    		try {
				scanner.disable();
			} catch (ScannerException e) {				
				Logger.WriteLog(Tag,"ScannerException occur while scanner.disable(), message="+e.getMessage() +".",Log_Level.Error,true);
			}
    	}
    	Logger.WriteLog(Tag,"Scanner is disabled.",Log_Level.Information,true);
    }
        
    public void Scan(int timeout){
    	if(!scanner.isEnable())
    	{
    		Enable();
    	}
    	if(!isScanning)
    	{
	    	startDecodeTimer(timeout);
	    	try {
			    isScanning = true;
			    Logger.WriteLog(Tag,"Scanning is triggered.",Log_Level.Verbose);
				scanner.read();
			} catch (ScannerException e) {			
				Logger.WriteLog(Tag,"ScannerException occur while scanner.read(), message="+e.getMessage() +".",Log_Level.Error,true);
			}
    	}
    }
    
    public void Scan(){
	    Scan(2000);    	
    }
    
    public void HandsfreeScan(int interleave) throws Exception{
    	isScanning = true;
    	try{
    		scanner.handsfreeRead(interleave);
    	}
    	catch(ScannerException ex){
    		Logger.WriteLog(Tag,"ScannerException occur while HandsfreeScan, message="+ex.getMessage() +".",Log_Level.Error,true);
    		scanner.cancelRead();
    		isScanning = false;
    		throw(ex);    		
    	}    	
    }
    
    public void CancelScan(){
    	stopDecodeTimer();
    	if(scanner!=null && isScanning)
    	{
	    	try {
	    		Logger.WriteLog(Tag,"Scanning is canceled.",Log_Level.Verbose);
				scanner.cancelRead();
			} catch (ScannerException e) {		
				Logger.WriteLog(Tag,"ScannerException occur while scanner.cancelRead, message="+e.getMessage() +".",Log_Level.Error,true);
			}
    	}
		isScanning = false;
    }
    
    private Timer decodeTimer;
    private void startDecodeTimer(int timeout){
    	decodeTimer = new Timer();
    	decodeTimer.schedule(new TimerTask() {			
			@Override
			public void run() {
				CancelScan();
			}
		},timeout);
    }
    
    private void stopDecodeTimer(){
    	if(decodeTimer!=null)
    	{
        	decodeTimer.cancel();
        	decodeTimer = null;
    	}
    }
        
    private void sendStatesChangedEvent(){
    	if(statusChangeListeners!=null && !statusChangeListeners.isEmpty())
		{
    		ScannerStateChangedEventObject eventObj = new  ScannerStateChangedEventObject(this,GetCurrentStates());
			Vector targets;
		      synchronized (this) {
		        targets = (Vector) statusChangeListeners.clone();
		      }
		      // walk through the listener list and
		      //   call the sunMoved method in each
		      Enumeration e = targets.elements();
		      while (e.hasMoreElements()) {
		    	ScannerStateChangedEventListener l = (ScannerStateChangedEventListener) e.nextElement();
		        l.StateChangedMessage(eventObj);
		      }
		}
    }
    
    private void sendDataDecodedEvent(String decodedMessage){
    	if(dataDecodedListeners!=null && !dataDecodedListeners.isEmpty())
		{
    		ScannerDecodedMessageObject eventObj = new  ScannerDecodedMessageObject(this,decodedMessage);
			Vector targets;
		      synchronized (this) {
		        targets = (Vector) dataDecodedListeners.clone();
		      }
		      // walk through the listener list and
		      //   call the sunMoved method in each
		      Enumeration e = targets.elements();
		      while (e.hasMoreElements()) {
		    	  ScannerDecodedMessageListener l = (ScannerDecodedMessageListener) e.nextElement();
		        l.DecodeMessage(eventObj);
		      }
		}
    }
  
	/** Register a listener for MaafUtils_RunningMessageEvent */
	synchronized public void addScannerStateChangedeListener(ScannerStateChangedEventListener lsn) {
	    if (statusChangeListeners == null)
	    {
	    	statusChangeListeners = new Vector();
	    }
	    statusChangeListeners.addElement(lsn);
	}
	  
	/** Remove a listener for ScannerDecodedMessageEvent */
	synchronized public void removeScannerStateChangedeListener(ScannerStateChangedEventListener lsn) {
	    if (statusChangeListeners != null)
	    {
	    	statusChangeListeners.removeElement(lsn);
	    } 
	}   
       
	/** Register a listener for MaafUtils_RunningMessageEvent */
	synchronized public void addScannerDecodedMessageListener(ScannerDecodedMessageListener lsn) {
	    if (dataDecodedListeners == null)
	    {
	    	dataDecodedListeners = new Vector();
	    }
	    dataDecodedListeners.addElement(lsn);
	}
	
	/** Remove a listener for ScannerDecodedMessageEvent */
	synchronized public void removeScannerDecodedMessageListener(ScannerDecodedMessageListener lsn) {
	    if (dataDecodedListeners != null)
	    {
		    dataDecodedListeners.removeElement(lsn);
	    }
	}
	
	public ScannerConfig GetScannerConfig(){
		ScannerConfig config = null;
		try {
			config = scanner.getConfig();
		} catch (ScannerException e) {
			e.printStackTrace();
		}
		return config;
	}
	
	public void SetScannerConfig(ScannerConfig config){
    	try {    		
			scanner.setConfig(config);			
		} catch (ScannerException e) {			
			e.printStackTrace();
		}
    }
	    
	public void ResetToDefault(){
		if(scanner!=null)
		{
//			try {
//				//scanner.resetToDefault();
//		    	ScannerConfig tempConfig = new ScannerConfig();
//		    	tempConfig.scanParams.decodeAudioFeedbackUri = "content://media/internal/audio/media/94";
//		        scanner.setConfig(tempConfig);
//		        Logger.WriteLog(Logger.CurrentLogLevel.Information,Tag,"Scanner config reset to default.");
//			} catch (ScannerException e) {				
//				e.printStackTrace();
//			}
			try {
				scanner.setConfig(defaultScannerConfig);
				Log.i("AT_API", "***********Scanconfig reset to default*************");
			} catch (ScannerException e) {
			}
		}
	}	

	//{{Scan Parameters
	
	public ScannerConfig SetScanParameters_CODEID(ScannerConfig config, CodeIdType idType){
		config.scanParams.codeIdType = idType;
		Logger.WriteLog(Tag,"Set scanner config, CodeIdType = "+idType.toString(),Log_Level.Information);
		return config;
	}
	
	//}}Scan Parameters
	
	//{{Toggle Commands
	public ScannerConfig SetDecoderParameters_AUSTRALIANPOSTAL_Enable(ScannerConfig config, boolean enable){
		return SetDecoderParameters_AUSPOSTAL_Enable(config,enable);
	}
	public ScannerConfig SetDecoderParameters_AUSPOSTAL_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.australianPostal.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, AustralianPostal_Enable = "+String.valueOf(enable),Log_Level.Information); 		
		return config;
	}
	 
	public ScannerConfig SetDecoderParameters_AZTEC_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.aztec.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Aztec_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
	}
	
	public ScannerConfig SetDecoderParameters_CHINESE2OF5_Enable(ScannerConfig config, boolean enable){
	    			
		config.decoderParams.chinese2of5.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Chinese2of5_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
	}
	 
	public ScannerConfig SetDecoderParameters_CODABAR_Enable(ScannerConfig config, boolean enable){    	
		config.decoderParams.codaBar.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, CodaBar_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
	
	public ScannerConfig SetDecoderParameters_CODE11_Enable(ScannerConfig config, boolean enable){
			
		config.decoderParams.code11.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code11_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
	
	public ScannerConfig SetDecoderParameters_CODE39_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.code39.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code39_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
	
	public ScannerConfig SetDecoderParameters_CODE93_Enable(ScannerConfig config, boolean enable){    	
		config.decoderParams.code93.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code93_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
	}
	
	public ScannerConfig SetDecoderParameters_CODE128_Enable(ScannerConfig config, boolean enable){    	
		config.decoderParams.code128.enabled = enable;
	    Logger.WriteLog(Tag,"Set scanner config, Code128_Enable = "+String.valueOf(enable),Log_Level.Information);	    		
		return config;
	}

	public ScannerConfig SetDecoderParameters_COMPOSITE_AB_Enable(ScannerConfig config, boolean enable){    	
		config.decoderParams.compositeAB.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, CompositeAB_Enable = "+String.valueOf(enable),Log_Level.Information);	    		
		return config;
	}
	
	public ScannerConfig SetDecoderParameters_COMPOSITE_C_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.compositeC.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, CompositeC_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
	
//	public ScannerConfig SetDecoderParameters_DUTCHPOSTAL_Enable(ScannerConfig config, boolean enable){
//		config.decoderParams.
//		Logger.WriteLog(Logger.CurrentLogLevel.Information,Tag,"Set scanner config, DUTCHPOSTAL_Enable = "+String.valueOf(enable));
//		return config;
//	}

    public ScannerConfig SetDecoderParameters_D2OF5_Enable(ScannerConfig config, boolean enable){	
    	config.decoderParams.d2of5.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, D2of5_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_DATAMATRIX_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.dataMatrix.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, DataMatrix_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;		
    }
    
    public ScannerConfig SetDecoderParameters_EAN13_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.ean13.enabled = enable;
	    Logger.WriteLog(Tag,"Set scanner config, EAN13_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_EAN8_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.ean8.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, EAN8_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
	
    public ScannerConfig SetDecoderParameters_RSS14_Enable(ScannerConfig config, boolean enable){
    	return SetDecoderParameters_GS1DATABAR_Enable(config, enable);    	
    }    
    public ScannerConfig SetDecoderParameters_GS1DATABAR_Enable(ScannerConfig config, boolean enable){    	
    	config.decoderParams.gs1Databar.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Gs1Databar_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_RSSEXP_Enable(ScannerConfig config, boolean enable){
    	return SetDecoderParameters_GS1DATABAREXP_Enable(config,enable);
    }    
    public ScannerConfig SetDecoderParameters_GS1DATABAREXP_Enable(ScannerConfig config, boolean enable){    	
    	config.decoderParams.gs1DatabarExp.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Gs1DatabarExp_Enable = "+String.valueOf(enable),Log_Level.Information);
	    return config;	
    }

    public ScannerConfig SetDecoderParameters_RSSLIM_Enable(ScannerConfig config, boolean enable){
    	return SetDecoderParameters_GS1DATABARLIM_Enable(config,enable);
    }
    public ScannerConfig SetDecoderParameters_GS1DATABARLIM_Enable(ScannerConfig config, boolean enable){    	
    	config.decoderParams.gs1DatabarLim.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Gs1DatabarLim_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_HANXIN_Enable(ScannerConfig config, boolean enable){    	
    	config.decoderParams.hanXin.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, HanXin_Enable = "+String.valueOf(enable),Log_Level.Information);	
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_I2OF5_Enable(ScannerConfig config, boolean enable){    
    	config.decoderParams.i2of5.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, I2of5_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    public ScannerConfig SetDecoderParameters_JAPPOSTAL_Enable(ScannerConfig config, boolean enable){
    	return SetDecoderParameters_JAPANESEPOSTAL_Enable(config,enable);
    }
    public ScannerConfig SetDecoderParameters_JAPANESEPOSTAL_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.japanesePostal.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, JapanesePostal_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_KOREAN_3OF5_Enable(ScannerConfig config, boolean enable){    
    	config.decoderParams.korean3of5.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Korean3of5_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MATRIX2OF5_Enable(ScannerConfig config, boolean enable){	
    	config.decoderParams.matrix2of5.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Matrix2of5_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MAXICODE_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.maxiCode.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, MaxiCode_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MICROPDF_Enable(ScannerConfig config, boolean enable){    	
    	config.decoderParams.microPDF.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, MicroPDF_Enable = "+String.valueOf(enable),Log_Level.Information);		
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MICROQR_Enable(ScannerConfig config, boolean enable){    
    	config.decoderParams.microQR.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, MicroQR_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.msi.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, MSIPLESSEY_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }

    public ScannerConfig SetDecoderParameters_PDF417_Enable(ScannerConfig config, boolean enable){    
    	config.decoderParams.pdf417.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Pdf417_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_QRCODE_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.qrCode.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, QrCode_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_TLC39_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.tlc39.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Tlc39_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_TRIOPTIC39_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.triOptic39.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, TriOptic39_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UKPOSTAL_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.ukPostal.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, UkPostal_Enable = "+String.valueOf(enable),Log_Level.Information);	    		
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UPCA_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.upca.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCA_Enable = "+String.valueOf(enable),Log_Level.Information);	    		
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UPCE0_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.upce0.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCE0_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UPCE1_Enable(ScannerConfig config, boolean enable){    	
    	config.decoderParams.upce1.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCE1_Enable = "+String.valueOf(enable),Log_Level.Information);	    		
		return config;
    }
    public ScannerConfig SetDecoderParameters_US4STATE_Enable(ScannerConfig config, boolean enable){
    	return SetDecoderParameters_US4_Enable(config,enable);
    }
    public ScannerConfig SetDecoderParameters_US4_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.us4State.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Us4_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_USPOSTNET_Enable(ScannerConfig config, boolean enable){
    	config.decoderParams.usPostNet.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, UsPostNet_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_USPLANET_Enable(ScannerConfig config, boolean enable){    
		config.decoderParams.usPlanet.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, UsPlanet_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
	public ScannerConfig SetDecoderParameters_US4STATEFICS_Enable(ScannerConfig config, boolean enable){
		config.decoderParams.us4StateFics.enabled = enable;
		Logger.WriteLog(Tag,"Set scanner config, Us4StateFics_Enable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
	
	//}}Toggle Commands
	
	//{{Decoder Parameters
	
	//{{Aztec
	
	 public ScannerConfig SetDecoderParameters_AZTEC_INVERSE(ScannerConfig config ,Inverse1DMode mode){
		config.decoderParams.aztec.inverse = mode;
		Logger.WriteLog(Tag,"Set scanner config, Codabar_MIN = "+String.valueOf(mode),Log_Level.Information);
		return config;
	 }
	 
	//}}Aztec
	 
    //{{Codabar
	
    public ScannerConfig SetDecoderParameters_CODABAR_MIN(ScannerConfig config ,int minLength){
		config.decoderParams.codaBar.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, Codabar_MIN = "+String.valueOf(minLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODABAR_MAX(ScannerConfig config ,int maxLength){
		config.decoderParams.codaBar.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, Codabar_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODABAR_CLSIEDITING(ScannerConfig config ,boolean enable){
		config.decoderParams.codaBar.clsiEditing = enable;
		Logger.WriteLog(Tag,"Set scanner config, Codabar_ClsiEditing = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODABAR_NOTISEDITING(ScannerConfig config ,boolean enable){
    	config.decoderParams.codaBar.notisEditing = enable;
		Logger.WriteLog(Tag,"Set scanner config, Codabar_NotisEditing = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    //}}Codabar
    
    //{{Code11
        
    public ScannerConfig SetDecoderParameters_CODE11_CHECKDIGITCOUNT(ScannerConfig config, VerifyCheckDigit vcd){
    	return SetDecoderParameters_CODE11_VERIFYCHECKDIGIT(config,vcd);
    }
    public ScannerConfig SetDecoderParameters_CODE11_VERIFYCHECKDIGIT(ScannerConfig config, VerifyCheckDigit vcd){
		config.decoderParams.code11.verifyCheckDigit=vcd;
		Logger.WriteLog(Tag,"Set scanner config, Code11_VERIFYCHECKDIGIT = "+String.valueOf(vcd.toString()),Log_Level.Information,true);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE11_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){
		config.decoderParams.code11.reportCheckDigit=enable;
		Logger.WriteLog(Tag,"Set scanner config, Code11_REPORTCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
       
    public ScannerConfig SetDecoderParameters_CODE11_MIN(ScannerConfig config, int minLength){
		config.decoderParams.code11.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, Code11_MIN = "+String.valueOf(minLength),Log_Level.Information);    	
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE11_MAX(ScannerConfig config, int maxLength){
    	config.decoderParams.code11.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, Code11_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;    		
    }
    //}}Code11
	
    //{{Code39
    
    public ScannerConfig SetDecoderParameters_CODE39_CODE32PREFIX(ScannerConfig config, boolean enable){
    	return SetDecoderParameters_CODE39_REPORTCODE32PREFIX(config,enable);
    }
    public ScannerConfig SetDecoderParameters_CODE39_REPORTCODE32PREFIX(ScannerConfig config, boolean enable){
		config.decoderParams.code39.reportCode32Prefix = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code39_REPORTCODE32PREFIX = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE39_FULLASCII(ScannerConfig config, boolean enable){
		config.decoderParams.code39.fullAscii = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code39_FULLASCII = "+String.valueOf(enable),Log_Level.Information);
		return config;	
    }
       
    public ScannerConfig SetDecoderParameters_CODE39_MIN(ScannerConfig config, int minLength){
    	config.decoderParams.code39.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, Code39_MIN = "+String.valueOf(minLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE39_MAX(ScannerConfig config, int maxLength){
    	config.decoderParams.code39.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, Code39_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_CODE39_CONVERTTOCODE32(ScannerConfig config, boolean enable){
    	config.decoderParams.code39.convertToCode32 = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code39_CONVERTTOCODE32 = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE39_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){
    	config.decoderParams.code39.reportCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code39_REPORTCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE39_VERIFYCHECKDIGIT(ScannerConfig config, boolean enable){
    	config.decoderParams.code39.verifyCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code39_VERIFYCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;	
    }  
    
    //}}Code39
    
    //{{Code93
    
    public ScannerConfig SetDecoderParameters_CODE93_MIN(ScannerConfig config, int minLength){
    	config.decoderParams.code93.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, Code93_MIN = "+String.valueOf(minLength),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_CODE93_MAX(ScannerConfig config, int maxLength){
    	config.decoderParams.code93.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, Code93_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;	
    }
    
    //}}Code93
    
	//{{Code128
        
    public ScannerConfig SetDecoderParameters_CODE128_ISBT128(ScannerConfig config, boolean enable){
    	config.decoderParams.code128.enableIsbt128=enable;
		Logger.WriteLog(Tag,"Set scanner config, Code128_ISBT128 = "+String.valueOf(enable),Log_Level.Information);
		return config;	
    }
                         
    public ScannerConfig SetDecoderParameters_CODE128_ISBT128CONCATMODE(ScannerConfig config, Isbt128ContactMode mode){
    	config.decoderParams.code128.isbt128ConcatMode=mode;
		Logger.WriteLog(Tag,"Set scanner config, Code128_ISBT128ConcatMode = "+String.valueOf(mode.toString()),Log_Level.Information,true);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE128_MIN(ScannerConfig config, int minLength){
		config.decoderParams.code128.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, Code128_MIN = "+String.valueOf(minLength),Log_Level.Information);    		
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_CODE128_MAX(ScannerConfig config, int maxLength){
    	config.decoderParams.code128.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, Code128_MAX = "+String.valueOf(maxLength));   	
		return config;
    }

    public ScannerConfig SetDecoderParameters_CODE128_CHECKISBTTABLE(ScannerConfig config, boolean enable){
    	config.decoderParams.code128.checkIsbtTable = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code128_CheckISBTTable = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }

    public ScannerConfig SetDecoderParameters_CODE128_EAN128(ScannerConfig config, boolean enable){
    	config.decoderParams.code128.enableEan128 = enable;
		Logger.WriteLog(Tag,"Set scanner config, Code128_EAN128 = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
            
    //}}Code128
    
    //{{D2of5
    
    public ScannerConfig SetDecoderParameters_D2OF5_MIN(ScannerConfig config, int minLength){
		config.decoderParams.d2of5.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, D2of5_MIN = "+String.valueOf(minLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_D2OF5_MAX(ScannerConfig config, int maxLength){
		config.decoderParams.d2of5.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, D2of5_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;
    }    
    //}}D2of5
    
    //{{DataMatrix
    
    public ScannerConfig SetDecoderParameters_DATAMATRIX_INVERSE(ScannerConfig config, Inverse1DMode mode){  	
    	config.decoderParams.dataMatrix.inverse = mode;
		Logger.WriteLog(Tag,"Set scanner config, DATAMATRIX_INVERSE = "+String.valueOf(mode),Log_Level.Information);
		return config;
    }
    
    //}}DataMatrix
    
    //{{I2of5
    public ScannerConfig SetDecoderParameters_I2OF5_MIN(ScannerConfig config, int minLength){
    	config.decoderParams.i2of5.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, I2of5_MIN = "+String.valueOf(minLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_I2OF5_MAX(ScannerConfig config, int maxLength){
    	config.decoderParams.i2of5.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, I2of5_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_I2OF5_VERIFYCHECKDIGIT(ScannerConfig config, VerifyCheckDigit chkDit){	
    	config.decoderParams.i2of5.verifyCheckDigit = chkDit;			
		Logger.WriteLog(Tag,"Set scanner config, I2of5_VERIFYCHECKDIGIT = "+String.valueOf(chkDit.toString()),Log_Level.Information,true);
		return config;
    }
        
    public ScannerConfig SetDecoderParameters_I2OF5_CONVERTTOEAN13(ScannerConfig config, boolean enable){
		config.decoderParams.i2of5.convertToEan13 = enable;
		Logger.WriteLog(Tag,"Set scanner config, I2of5_CONVERTTOEAN13 = "+String.valueOf(enable),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_I2OF5_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){
    	config.decoderParams.i2of5.reportCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, I2of5_REPORTCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    //}}
        
    //{{Matrix2of5
        
    public ScannerConfig SetDecoderParameters_MATRIX2OF5_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){  	
    	config.decoderParams.matrix2of5.reportCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, Matrix2of5_REPORTCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MATRIX2OF5_VERIFYCHECKDIGIT(ScannerConfig config, boolean enable){
    	config.decoderParams.matrix2of5.verifyCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, Matrix2of5_VERIFYCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MATRIX2OF5_MIN(ScannerConfig config, int minLength){
    	config.decoderParams.matrix2of5.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, Matrix2of5_MIN = "+String.valueOf(minLength),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_MATRIX2OF5_MAX(ScannerConfig config, int maxLength){
    	config.decoderParams.matrix2of5.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, Matrix2of5_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MATRIX2OF5_REDUNDANCY(ScannerConfig config, boolean enable){
		config.decoderParams.matrix2of5.redundancy = enable;
		Logger.WriteLog(Tag,"Set scanner config, Matrix2of5_REDUNDANCY = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    //}}Matrix2of5

    //{{MSI
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_CHECKDIGITCOUNT(ScannerConfig config, CheckDigit chkDgt){    	
    	return SetDecoderParameters_MSIPLESSEY_CHECKDIGITS(config,chkDgt);
    }
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_CHECKDIGITS(ScannerConfig config, CheckDigit chkDgt){
    	config.decoderParams.msi.checkDigits = chkDgt;
		Logger.WriteLog(Tag,"Set scanner config, MSIPLESSEY_CHECKDIGITS = "+String.valueOf(chkDgt.toString()),Log_Level.Information,true);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){
    	config.decoderParams.msi.reportCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, MSIPLESSEY_CHECKDIGITS = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_MIN(ScannerConfig config, int minLength){
    	config.decoderParams.msi.length1 = minLength;
		Logger.WriteLog(Tag,"Set scanner config, MSIPLESSEY_MIN = "+String.valueOf(minLength),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_MAX(ScannerConfig config, int maxLength){
    	config.decoderParams.msi.length2 = maxLength;
		Logger.WriteLog(Tag,"Set scanner config, MSIPLESSEY_MAX = "+String.valueOf(maxLength),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_SETCHECKDIGIT(ScannerConfig config, CheckDigit chkDit){
    	config.decoderParams.msi.checkDigits = chkDit;
		Logger.WriteLog(Tag,"Set scanner config, MSIPLESSEY_SETCHECKDIGIT = "+String.valueOf(chkDit.toString()),Log_Level.Information,true);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_MSIPLESSEY_CHECKDIGITSCHEME(ScannerConfig config, CheckDigitScheme scheme){
    	config.decoderParams.msi.checkdigitscheme = scheme;			
		Logger.WriteLog(Tag,"Set scanner config, MSIPLESSEY_CHECKDIGITSCHEME = "+String.valueOf(scheme.toString()),Log_Level.Information,true);
		return config;	
    }
    
    //}}MSI
            
    //{{UPCA
       
    public ScannerConfig SetDecoderParameters_UPCA_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){
		config.decoderParams.upca.reportCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCA_REPORTCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;	
    }
    
    public ScannerConfig SetDecoderParameters_UPCA_PREAMBLE(ScannerConfig config, Preamble preamble){
    	config.decoderParams.upca.preamble = preamble;
		Logger.WriteLog(Tag,"Set scanner config, UPCA_PREAMBLE = "+String.valueOf(preamble.toString()),Log_Level.Information,true);
		return config;	
    }
    //}}UPCA
        
    //{{UPCE0
        
    public ScannerConfig SetDecoderParameters_UPCE0_PREAMBLE(ScannerConfig config, Preamble preamble){
		config.decoderParams.upce0.preamble = preamble;    		
		Logger.WriteLog(Tag,"Set scanner config, UPCE0_PREAMBLE = "+String.valueOf(preamble.toString()),Log_Level.Information,true);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UPCE0_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){
		config.decoderParams.upce0.reportCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCE0_REPORTCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;
	}
    
    public ScannerConfig SetDecoderParameters_UPCE0_CONVERTTOUPCA(ScannerConfig config, boolean enable){
    	config.decoderParams.upce0.convertToUpca = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCE0_CONVERTTOUPCA = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    //}}UPCE0
    
    //{{UPCE1
     
    public ScannerConfig SetDecoderParameters_UPCE1_REPORTCHECKDIGIT(ScannerConfig config, boolean enable){    	
    	config.decoderParams.upce1.reportCheckDigit = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCE1_REPORTCHECKDIGIT = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UPCE1_CONVERTTOUPCA(ScannerConfig config, boolean enable){
    	config.decoderParams.upce1.convertToUpca = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCE1_CONVERTTOUPCA = "+String.valueOf(enable),Log_Level.Information);
		return config;		
    }
    
    public ScannerConfig SetDecoderParameters_UPCE1_PREAMBLE(ScannerConfig config, Preamble preamble){
    	config.decoderParams.upce1.preamble = preamble;
		Logger.WriteLog(Tag,"Set scanner config, UPCE1_PREAMBLE = "+String.valueOf(preamble.toString()),Log_Level.Information,true);
		return config;	
    }    
    //}}UPCE1
    
    //{{UPCEAN
    
    public ScannerConfig SetDecoderParameters_UPCEAN_SUPPLEMENTAL(ScannerConfig config, SupplementalMode mode){
		return SetDecoderParameters_UPCEAN_SUPPLEMENTALMODE(config,mode);
    }
    public ScannerConfig SetDecoderParameters_UPCEAN_SUPPLEMENTALMODE(ScannerConfig config, SupplementalMode mode){
    	config.decoderParams.upcEanParams.supplementalMode = mode;
		Logger.WriteLog(Tag,"Set scanner config, UPCEAN_SUPPLEMENTALMODE = "+String.valueOf(mode.toString()),Log_Level.Information,true);
		return config;
    }    

    public ScannerConfig SetDecoderParameters_UPCEAN_BOOKLAND(ScannerConfig config, boolean enable){
    	config.decoderParams.upcEanParams.booklandCode = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCEAN_BOOKLAND = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }  
    
    public ScannerConfig SetDecoderParameters_UPCEAN_COUPON(ScannerConfig config, boolean enable){    
    	config.decoderParams.upcEanParams.couponCode = enable;
		Logger.WriteLog(Tag,"Set scanner config, UPCEAN_COUPON = "+String.valueOf(enable),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UPCEAN_COUPONREPORTINGMODE(ScannerConfig config, CouponReport reportMode){    
    	config.decoderParams.upcEanParams.couponReport = reportMode;
		Logger.WriteLog(Tag,"Set scanner config, UPCEAN_COUPONREPORTINGMODE = "+String.valueOf(reportMode),Log_Level.Information);
		return config;
    }
    
    public ScannerConfig SetDecoderParameters_UPCEAN_SECURITYLEVEL(ScannerConfig config, SecurityLevel securityLv){    
    	config.decoderParams.upcEanParams.securityLevel = securityLv;
		Logger.WriteLog(Tag,"Set scanner config, UPCEAN_SECURITYLEVEL = "+String.valueOf(securityLv),Log_Level.Information);
		return config;
    }
    
    
        
    //}}UPCEAN

    //{{COMPOSITE_AB
    
    public ScannerConfig SetDecoderParameters_COMPOSITE_AB_UCCLINKMODE(ScannerConfig config, UccLinkMode mode){
		config.decoderParams.compositeAB.uccLinkMode = mode;
		Logger.WriteLog(Tag,"Set scanner config, COMPOSITE_AB_UCCLINKMODE = "+String.valueOf(mode),Log_Level.Information);
		return config;	
    }
    
    //}} COMPOSITE_AB
          
    //{{QRCode
    
    public ScannerConfig SetDecoderParameters_QRCODE_INVERSE(ScannerConfig config, Inverse1DMode mode){  	
    	config.decoderParams.qrCode.inverse = mode;
		Logger.WriteLog(Tag,"Set scanner config, QRCODE_INVERSE = "+String.valueOf(mode),Log_Level.Information);
		return config;
    }
    
    //}}QRCode
    
	//}}Decoder Parameter
    
    //{{Reader Parameter
    public ScannerConfig SetReaderParameters_PICKLIST(ScannerConfig config, PickList pick){
    	return SetReaderParameters_PICKLISTMODE(config,pick);
    }
    public ScannerConfig SetReaderParameters_PICKLISTMODE(ScannerConfig config, PickList pick){
		config.readerParams.readerSpecific.imagerSpecific.pickList = pick;
		Logger.WriteLog(Tag,"Set scanner config, CodeIdType = "+pick.toString(),Log_Level.Information);
		return config;
    }    
    
    public ScannerConfig SetReaderParameters_INVERSEID(ScannerConfig config, Inverse1DMode ivMode){
		//{{ imagerSpecific
		try
		{
			config.readerParams.readerSpecific.imagerSpecific.inverse1DMode = ivMode;
		}
		catch(Exception ex)
		{
			
		}
		//}} imagerSpecific
		//{{ laserSpecific
		try
		{
			config.readerParams.readerSpecific.laserSpecific.inverse1DMode = ivMode;
		}
		catch(Exception ex)
		{
			
		}
		//}} laserSpecific
		Logger.WriteLog(Tag,"Set scanner config, InverseMode = "+ivMode.toString(),Log_Level.Information);
		return config;
    }   
    //}}Reader Parameter
    
}


