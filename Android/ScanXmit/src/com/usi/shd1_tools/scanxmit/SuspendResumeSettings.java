package com.usi.shd1_tools.scanxmit;

import com.usi.shd1_tools.commonlibrary.SystemUtility;
import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnLongClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class SuspendResumeSettings extends Activity {
	private EditText txtSuspendInterval;
	private EditText txtActiveInterval;
	private Button btnOK;
	private Button btnCancel;
	private Button btnDebug;
	private TextView tvActiveMs;
	private Context currentContext;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		Bundle bundle = this.getIntent().getExtras();
		currentContext = getApplicationContext();
		SystemUtility.SetWakeLock(currentContext, true);
		setContentView(R.layout.activity_suspend_resume_settings);
		txtSuspendInterval = (EditText)findViewById(R.id.numSuspendInterval);
		txtActiveInterval = (EditText)findViewById(R.id.numActiveInterval);
		tvActiveMs = (TextView)findViewById(R.id.txtViewActiveMs);
		tvActiveMs.setOnLongClickListener(new OnLongClickListener()
		{
			@Override
			public boolean onLongClick(View v) {
				int visibility = btnDebug.getVisibility();
				if(visibility!=View.VISIBLE){
					visibility = View.VISIBLE;
				}
				else{
					visibility = View.INVISIBLE;
				}
				btnDebug.setVisibility(visibility);
				return false;
			}
			
		});
		btnOK = (Button)findViewById(R.id.btnSetSuspendResumeOK);
		btnCancel = (Button)findViewById(R.id.btnSetSuspendResumeCancel);
		btnDebug = (Button)findViewById(R.id.btnSuspendResumeDebug);
		btnOK.setOnClickListener(btnClickedEventListener);
		btnCancel.setOnClickListener(btnClickedEventListener);
		btnDebug.setOnClickListener(btnClickedEventListener);
		int activeInterval = bundle.getInt(ScanXmit2.Key_SuspendResume_ActiveInterval);
		int suspendInterval = bundle.getInt(ScanXmit2.Key_SuspendResume_SuspendInterval);
		txtSuspendInterval.setText(String.valueOf(suspendInterval));
		txtActiveInterval.setText(String.valueOf(activeInterval));
	}
	
	public OnClickListener btnClickedEventListener = new OnClickListener(){
		@Override
		public void onClick(View v) {
			if(v.equals(btnOK))
			{
				int activeInterval = Integer.parseInt(txtActiveInterval.getText().toString());
				int suspendInterval = Integer.parseInt(txtSuspendInterval.getText().toString());
				ScanXmit2.SetSuspendResumeParameters(activeInterval, suspendInterval);
				exitFlag = true;
				onBackPressed();
			}
			else if (v.equals(btnCancel)){
				exitFlag = true;
				onBackPressed();
			}
			else if (v.equals(btnDebug)){
				runSuspendResumeTest();
			}
		}		
	};

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.suspend_resume_settings, menu);
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
	
	private boolean exitFlag = false;
	@Override
	public void onBackPressed(){
		if(exitFlag)
		{
			super.onBackPressed();
		}
		exitFlag = false;
	}
		
	//{{ For debug

	private void runSuspendResumeTest(){     	
		SystemUtility.SetDisplayTimeout(currentContext, 15000);
		final int activeInterval = Integer.parseInt(txtActiveInterval.getText().toString());
		final int suspendInterval = Integer.parseInt(txtSuspendInterval.getText().toString());
		Thread td = new Thread(new Runnable(){
			@Override
			public void run() {
				for(int i=0;i<5;i++){
					try{
						SystemUtility.SetWakeLock(currentContext, true);
						Log.d("AT_Debug","Set device wake up.");
						Thread.sleep(activeInterval);
						Log.d("AT_Debug","Set alarm manager and wait to wake up.");
						SystemUtility.setWakeUp_AlarmManager(currentContext,suspendInterval);
						SystemUtility.SetWakeLock(currentContext, false);
						Thread.sleep(suspendInterval);
						}catch(Exception ex){				
					}
				}
				SystemUtility.cancelWakeUp_AlarmManager(currentContext);
				SystemUtility.SetWakeLock(currentContext,false);
			}			
		});
		td.start();					
	}
	
	
	//}} For debug
}
