package com.usi.shd1_tools.scanxmit;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.EditText;

public class DataTransferSettings extends Activity {
	private Button btnOK;
	private Button btnCancel;
	private EditText txtIp;
	private EditText txtPort;
	private EditText txtInterval;
	private CheckBox ckbFixedDataSize;
	private EditText txtDataLength;	
	public Handler settingsChangeHandler;
	//private static String configPath = "/sdcard/usi/shd1_tools/ScanXmit/defaultSettings.xml";
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_data_transfer_settings);
		btnOK = (Button)findViewById(R.id.btnSetSuspendResumeOK);
		btnCancel = (Button)findViewById(R.id.btnSetSuspendResumeCancel);
		btnOK.setOnClickListener(btnClickedEventListener);
		btnCancel.setOnClickListener(btnClickedEventListener);
		txtIp = (EditText)findViewById(R.id.txtDataTransfer_IP);
		txtPort = (EditText)findViewById(R.id.txtDataTransfer_Port);
		txtInterval = (EditText)findViewById(R.id.txtDataTransfer_SendInterval);
		ckbFixedDataSize = (CheckBox)findViewById(R.id.ckbDataTransfer_FixedDataSize);
		ckbFixedDataSize.setOnCheckedChangeListener(new OnCheckedChangeListener(){
			@Override
			public void onCheckedChanged(CompoundButton buttonView,	boolean isChecked) {
				txtDataLength.setEnabled(isChecked);
			}			
		});
		txtDataLength = (EditText)findViewById(R.id.txtDataTransfer_DataLength);
		Bundle bundle = this.getIntent().getExtras();
		String ip = bundle.getString(ScanXmit2.Key_DataTransfer_IP)==null?"":bundle.getString(ScanXmit2.Key_DataTransfer_IP);
		int port = bundle.getInt(ScanXmit2.Key_DataTransfer_Port);
		int interval = bundle.getInt(ScanXmit2.Key_DataTransfer_Interval);
		boolean fixedData = bundle.getBoolean(ScanXmit2.Key_DataTransfer_FixDataSize);
		int dataLen = bundle.getInt(ScanXmit2.Key_DataTransfer_DataLength);
		
		txtIp.setText(ip);
		txtPort.setText(String.valueOf(port));
		txtInterval.setText(String.valueOf(interval));
		ckbFixedDataSize.setChecked(fixedData);
		txtDataLength.setText(String.valueOf(dataLen));
		txtDataLength.setEnabled(ckbFixedDataSize.isChecked());
	}
	
	
	
	public OnClickListener btnClickedEventListener = new OnClickListener(){
		@Override
		public void onClick(View v) {
			if(v.equals(btnOK))
			{
				String ip = txtIp.getText().toString();
				int port = Integer.parseInt(txtPort.getText().toString());
				int interval = Integer.parseInt(txtInterval.getText().toString());
				boolean fixDataSize = ckbFixedDataSize.isChecked();
				int dataLength = Integer.parseInt(txtDataLength.getText().toString());
				ScanXmit2.SetDataTransferParameters(ip,port,interval,fixDataSize,dataLength);
				exitFlag = true;
				onBackPressed();
			}
			else if (v.equals(btnCancel))
			{
				exitFlag = true;
				onBackPressed();
			}			
		}		
	};
	
	private boolean exitFlag = false;
	@Override
	public void onBackPressed(){
		if(exitFlag)
		{
			super.onBackPressed();
		}
		exitFlag = false;
	}
	
	
		
//	@Override
//	public Bundle getNewParametersBoundle(){
//		Bundle bundle = new Bundle();
//		
//		bundle.putString(ScanXmit2.Key_DataTransfer_IP, txtIp.getText().toString());
//		
//		try{
//			bundle.putInt(ScanXmit2.Key_DataTransfer_Port, Integer.parseInt(txtPort.getText().toString()));
//		}
//		catch(Exception ex){}
//		
//		try{
//			bundle.putInt(ScanXmit2.Key_DataTransfer_Interval, Integer.parseInt(txtInterval.getText().toString()));
//		}
//		catch(Exception ex){}
//		
//		bundle.putBoolean(ScanXmit2.Key_DataTransfer_FixDataSize, ckbFixedDataSize.isChecked());
//
//		try{
//			bundle.putInt(ScanXmit2.Key_DataTransfer_DataLength, Integer.parseInt(txtDataLength.getText().toString()));
//		}
//		catch(Exception e1){}
//		return bundle;
//	}
}
