package com.usi.shd1_tools.scanxmit;

import android.R.integer;
import android.app.Activity;
import android.os.Bundle;
import android.text.Editable;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.Checkable;
import android.widget.EditText;
import android.widget.Toast;

public class TriggerScanSettings extends Activity {

	private boolean exitFlag = false;
	private Button btnOK;
	private Button btnCancel;
	private EditText txtLoopTimes;
	private EditText txtInterval;
	private EditText txtTimeout;
	private CheckBox ckbScannerToggle;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_trigger_scan_settings);
		btnOK = (Button)findViewById(R.id.btnTriggerScanOK);
		btnCancel = (Button)findViewById(R.id.btnTriggerScanCancel);
		btnOK.setOnClickListener(viewOnClickListener);
		btnCancel.setOnClickListener(viewOnClickListener);
		txtLoopTimes = (EditText)findViewById(R.id.txtTriggerScan_LoopTimes);
		txtInterval = (EditText)findViewById(R.id.txtTriggerScanInterval);
		txtTimeout = (EditText)findViewById(R.id.txtTriggerScanTimeout);
		ckbScannerToggle = (CheckBox)findViewById(R.id.ckbTriggerScanToggle);
		
		Bundle bundle = this.getIntent().getExtras();
		String looptimes = String.valueOf(bundle.getInt(ScanXmit2.Key_TriggerScan_Looptimes));
		txtLoopTimes.setText(looptimes);
		String interval = String.valueOf(bundle.getInt(ScanXmit2.Key_TriggerScan_Interval));
		txtInterval.setText(interval);
		String timeout = String.valueOf(bundle.getInt(ScanXmit2.Key_TriggerScan_Timeout));
		txtTimeout.setText(timeout);
		boolean toggle_flag = bundle.getBoolean(ScanXmit2.Key_TriggerScan_ScannerToggle);
		ckbScannerToggle.setChecked(toggle_flag);
	}
	
	private OnClickListener viewOnClickListener = new OnClickListener() {		
			@Override
			public void onClick(View v) {
				if(v.equals(btnOK)){
					boolean valCheck = true;
					int looptimes=-1,interval=-1,timeout=-1;
					boolean toggleFlag = false;
					try{
						looptimes = Integer.parseInt(txtLoopTimes.getText().toString());
						interval = Integer.parseInt(txtInterval.getText().toString());
						timeout = Integer.parseInt(txtTimeout.getText().toString());
						toggleFlag = ckbScannerToggle.isChecked();
					}
					catch(Exception ex){
						valCheck = false;
					}
					if(valCheck){
						ScanXmit2.SetTriggerScanParameters(looptimes, interval, timeout, toggleFlag);
						exitFlag = true;
						onBackPressed();
					}
					else{
						Toast.makeText(getApplicationContext(),"The input variables is incorrect, please check again.", Toast.LENGTH_LONG).show();
					}
				}
				else if(v.equals(btnCancel)){
					exitFlag = true;
					onBackPressed();
				}				
			}
		};
		

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.trigger_scan_settings, menu);
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
	
	@Override
	public void onBackPressed(){
		if(exitFlag)
		{
			super.onBackPressed();
		}
		exitFlag = false;
	}
}
