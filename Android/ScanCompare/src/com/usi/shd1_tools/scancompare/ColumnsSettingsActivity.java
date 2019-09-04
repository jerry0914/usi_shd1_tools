package com.usi.shd1_tools.scancompare;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class ColumnsSettingsActivity extends Activity {

	private MenuItem miTpSettings;	
	private Button btnOK;
	private Button btnCancel;
	private Button btnReload;
	private EditText txtTcIdCol;
	private EditText txtTcNameCol;
	private EditText txtTcProcedureCol;
	private EditText txtTcExpectedResultCol;
	private EditText txtStartRow;
	private EditText txtScanConfigSettingCmdCol;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_columns_settings);
		txtTcIdCol = (EditText)findViewById(R.id.txtTCIDCol);		
		txtTcNameCol = 	(EditText)findViewById(R.id.txtTCNameCol);
		txtTcProcedureCol = (EditText)findViewById(R.id.txtPrecedureCol);
		txtTcExpectedResultCol = (EditText)findViewById(R.id.txtExpectedResultCol);
		txtScanConfigSettingCmdCol = (EditText)findViewById(R.id.txtScanConfigCmdCol);
		txtStartRow = (EditText)findViewById(R.id.txtStartRow);
		btnOK = (Button)findViewById(R.id.btnColSetOK);
		btnCancel = (Button)findViewById(R.id.btnCancel);
		btnOK.setOnClickListener(new OnClickListener()
		{
			@Override
			public void onClick(View v) {
				saveSharedPreferences(getApplicationContext());
				onBackPressed();
		}});
		btnCancel.setOnClickListener(new OnClickListener(){
			@Override
			public void onClick(View v) {
				onBackPressed();
			}			
		});
		btnReload = (Button)findViewById(R.id.btnDebugLoadSharePreference);
		btnReload.setOnClickListener(new OnClickListener(){
			@Override
			public void onClick(View v) {
				loadSharedPreferences(getApplicationContext());
			}
			
		});

		this.setTitle("Column Settings");
		loadSharedPreferences(getApplicationContext());
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.	
		getMenuInflater().inflate(R.menu.columns_settings, menu);	
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
	
	private void loadSharedPreferences(final Context context){
		SharedPreferences mySharedPreference = super.getSharedPreferences(TestplanParser.SharedPreferencesKey,Activity.MODE_PRIVATE);
		
		String strTcidCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_TcIdColumn,"");
		TestplanParser.tcidColumn = TestplanParser.GetExcelColumnIndexByCharactors(strTcidCol);
		if(strTcidCol.length()>0)
		txtTcIdCol.setText(strTcidCol);
		
		String strTcNameCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_TcNameColumn,"");
		TestplanParser.tcNameColumn = TestplanParser.GetExcelColumnIndexByCharactors(strTcNameCol);
		if(strTcNameCol.length()>0)
		txtTcNameCol.setText(strTcNameCol);
		
		String strProcedureCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_TcProcedureColumn,"");
		TestplanParser.tcProcedureColumn = TestplanParser.GetExcelColumnIndexByCharactors(strProcedureCol);
		if(strProcedureCol.length()>0)
		txtTcProcedureCol.setText(strProcedureCol);

		String strExpectedResultCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_ExpectedResultColumn,"");
		TestplanParser.expectedResultColumn =  TestplanParser.GetExcelColumnIndexByCharactors(strExpectedResultCol);
		if(strExpectedResultCol.length()>0)
		txtTcExpectedResultCol.setText(strExpectedResultCol);
		
		String strScanConfigCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_ScanConfigSettingsCommandColumn,"");
		TestplanParser.scanConfigSettingsCommandColumn =  TestplanParser.GetExcelColumnIndexByCharactors(strScanConfigCol);
		if(strScanConfigCol.length()>0)
		txtScanConfigSettingCmdCol.setText(strScanConfigCol);
		
		TestplanParser.dataStartRow = mySharedPreference.getInt(TestplanParser.SharedPreferencesName_DataStartRow,TestplanParser.dataStartRow);		
		txtStartRow.setText(String.valueOf(TestplanParser.dataStartRow));
	}
	
	private void saveSharedPreferences(final Context context){
		try
		{
			TestplanParser.tcidColumn = TestplanParser.GetExcelColumnIndexByCharactors(txtTcIdCol.getText().toString());
			TestplanParser.tcNameColumn = TestplanParser.GetExcelColumnIndexByCharactors(txtTcNameCol.getText().toString());
			TestplanParser.tcProcedureColumn = TestplanParser.GetExcelColumnIndexByCharactors(txtTcProcedureCol.getText().toString());
			TestplanParser.expectedResultColumn = TestplanParser.GetExcelColumnIndexByCharactors(txtTcExpectedResultCol.getText().toString());
			TestplanParser.scanConfigSettingsCommandColumn = TestplanParser.GetExcelColumnIndexByCharactors(txtScanConfigSettingCmdCol.getText().toString());
			TestplanParser.dataStartRow = Integer.valueOf(txtStartRow.getText().toString());

			SharedPreferences mySharedPreference = super.getSharedPreferences(TestplanParser.SharedPreferencesKey,Activity.MODE_PRIVATE);
			Editor myEditor = mySharedPreference.edit();
			
			myEditor.putString(TestplanParser.SharedPreferencesName_TcIdColumn,txtTcIdCol.getText().toString());
			myEditor.putString(TestplanParser.SharedPreferencesName_TcNameColumn,txtTcNameCol.getText().toString());
			myEditor.putString(TestplanParser.SharedPreferencesName_TcProcedureColumn,txtTcProcedureCol.getText().toString());
			myEditor.putString(TestplanParser.SharedPreferencesName_ExpectedResultColumn,txtTcExpectedResultCol.getText().toString());
			myEditor.putString(TestplanParser.SharedPreferencesName_ScanConfigSettingsCommandColumn,txtScanConfigSettingCmdCol.getText().toString());
			myEditor.putInt(TestplanParser.SharedPreferencesName_DataStartRow,TestplanParser.dataStartRow);
			myEditor.putBoolean(TestplanParser.SharedPreferencesName_AutoDetectColumnFlag,false); //Manually modified, stop auto detect column settings.
			myEditor.commit();
		}
		catch(Exception ex)
		{
			Toast.makeText(context, ex.toString(), Toast.LENGTH_SHORT).show();
		}
	}
	//
}
