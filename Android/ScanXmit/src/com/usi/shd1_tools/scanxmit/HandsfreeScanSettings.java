package com.usi.shd1_tools.scanxmit;

import java.util.ArrayList;
import java.util.Arrays;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.usi.shd1_tools.scannerlibrary.ScannerController;
import com.usi.shd1_tools.scannerlibrary.ScannerDecodedMessageListener;
import com.usi.shd1_tools.scannerlibrary.ScannerDecodedMessageObject;

public class HandsfreeScanSettings extends Activity {
	private Button btnOK;
	private Button btnCancel;
	private Button btnScan;
	private Button btnClear;
	private EditText txtInterleave;
	private EditText txtLoops;
	private EditText txtTimeout;
	private TextView txtBarcodes;
	private ArrayList<String> lstBarcodes;
	private boolean exitFlag = false;
	private ScannerController scanner;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_handsfree_scan_settings);
		btnCancel = (Button)findViewById(R.id.btnHandsfreeScanCancel);
		btnOK = (Button)findViewById(R.id.btnHandsfreeScanOK);
		btnScan = (Button)findViewById(R.id.btnHandsfreeScanAddBarcode);
		btnClear = (Button)findViewById(R.id.btnHandsfreeClearBarcodes);
		txtInterleave = (EditText)findViewById(R.id.txtHandsfreeScanInterleave);
		txtLoops = (EditText)findViewById(R.id.txtHandsfreeScanLoops);
		txtTimeout = (EditText)findViewById(R.id.txtHandsfreeScanTimeout);
		txtBarcodes = (TextView)findViewById(R.id.txtHandsfreeScanBarcodes);
		btnOK.setOnClickListener(btnClickedEventListener);
		btnCancel.setOnClickListener(btnClickedEventListener);
		btnScan.setOnClickListener(btnClickedEventListener);
		btnClear.setOnClickListener(btnClickedEventListener);
		Bundle bundle = this.getIntent().getExtras();
		String loops = String.valueOf(bundle.getInt(ScanXmit2.Key_HandsfreeScan_Looptimes));
		String interleave = String.valueOf(bundle.getInt(ScanXmit2.Key_HandsfreeScan_Interleave));
		String timeout = String.valueOf(bundle.getInt(ScanXmit2.Key_HandsfreeScan_Timeout));		
		String[] barcodes =  bundle.getStringArray(ScanXmit2.Key_HandsfreeScan_Barcodes);
		if(barcodes!=null){
			lstBarcodes =  new ArrayList<String>(Arrays.asList(barcodes));
		}
		else{
			lstBarcodes = new ArrayList<String>();
		}
		txtLoops.setText(loops);
		txtInterleave.setText(interleave);
		txtTimeout.setText(timeout);
		refreshBarcodes();
	}
	
	@Override
	protected void onResume() {
		super.onResume();
		//{{ Scanner enable
		scanner = new ScannerController();
		scanner.Enable();
		scanner.addScannerDecodedMessageListener(new ScannerDecodedMessageListener()
    	{
			@Override
			public void DecodeMessage(ScannerDecodedMessageObject arg0) {
				String decodeData = arg0.DecodedMessage();
				//Toast.makeText(getApplicationContext(), decodeData, Toast.LENGTH_SHORT).show();
				int position = lstBarcodes.indexOf(decodeData);
				//Toast.makeText(getApplicationContext(), String.valueOf(position), Toast.LENGTH_SHORT).show();
				if(position<0){
					lstBarcodes.add(decodeData);
					refreshBarcodes();
				}
			}		        		
    	});
		//}} scanner enable
	};

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.handsfree_scan_settings, menu);
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
	
	public OnClickListener btnClickedEventListener = new OnClickListener(){
		@Override
		public void onClick(View v) {
			// {{ btnOK
			if(v.equals(btnOK))
			{
				try{
					int looptimes = Integer.parseInt(txtLoops.getText().toString());
					int interval = Integer.parseInt(txtInterleave.getText().toString());
					int timeout = Integer.parseInt(txtTimeout.getText().toString());
					String[] barcodes = lstBarcodes.toArray(new String[0]);
					ScanXmit2.SetHandsfreeScanParameters(looptimes, interval, timeout, barcodes);
					exitFlag = true;
					onBackPressed();					
				}
				catch(Exception ex){
					Toast.makeText(getApplicationContext(), "The parameters are incorrect, please check and try again.", Toast.LENGTH_LONG).show();					
				}				
			}
			// }} btnOK
			// {{ btnCancel
			else if (v.equals(btnCancel))
			{
				exitFlag = true;
				onBackPressed();
			}
			// }} btnCancel
			// {{ btnScan
			else if (v.equals(btnScan))
			{
				 scanner.Scan(5000);				
			}
			// }} btnScan
			// {{ btnClear
			else if (v.equals(btnClear))
			{
				Builder bulder = new AlertDialog.Builder(HandsfreeScanSettings.this);
				bulder.setTitle("Clear all barcodes");
			    bulder.setMessage("Do you want to erase all the barcodes in the list?");
			    bulder.setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
			        public void onClick(DialogInterface dialog, int which) {
			        	lstBarcodes.clear();
			        	refreshBarcodes();
			            dialog.cancel();
			        }
			     });
			    bulder.setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
			        public void onClick(DialogInterface dialog, int which) {
			        	dialog.cancel();
			        }
			     });
			    bulder.setIcon(android.R.drawable.ic_dialog_alert);
			    AlertDialog alertdialog = bulder.create();
			    alertdialog.show();
			}
			// }} btnClear
		}		
	};
	
	private void refreshBarcodes(){		
		txtBarcodes.setText("");
		for(String str :lstBarcodes){
			txtBarcodes.append(str+"\n");
		}
	}
	
	@Override
	public void onBackPressed(){
		if(exitFlag)
		{
			scanner.Disable();
			scanner = null;
			super.onBackPressed();
		}
		exitFlag = false;
	}
}
