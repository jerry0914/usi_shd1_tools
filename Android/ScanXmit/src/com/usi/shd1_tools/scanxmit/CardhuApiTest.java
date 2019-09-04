package com.usi.shd1_tools.scanxmit;

import com.usi.shd1_tools.scanxmit.R.id;

import android.app.Activity;
import android.os.Bundle;
import android.os.Message;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.EditText;
import android.widget.ToggleButton;

public class CardhuApiTest extends Activity {
    private final static String TAG = "CardhuApiTest";

    private final static String INTENT_KEY_DISABLE = "com.android.key.service_settings";
    private final static String PDA_KEY_HOME = "pda_key_home";
    private final static String PDA_KEY_MENU = "pda_key_menu";
    private final static String PDA_KEY_BACK = "pda_key_back";
    private final static String PDA_KEY_DIS = "pda_key_dis";
    private final static String PDA_KEY_DIS_STRING = "pda_key_dis_string";

    private final static String INTENT_STATUSBAR = "com.android.service_settings";
    private final static String PDA_STATUSBAR = "pda_statusbar";

    
    private CheckBox ckbFullscreen,ckbStatusbar;
    private CheckBox ckbHome,ckbBack,ckbMenu;
    private ToggleButton tbtnKeycodesToggle;
    private EditText txtKeycodes;

    private Button btnOK,btnCancel;
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cardhu_api_test);
        btnOK = (Button)findViewById(R.id.btnApiTestOK);
        btnCancel = (Button)findViewById(R.id.btnApiTestCancel);
        ckbFullscreen = (CheckBox) findViewById(R.id.ckbApiTest_Fullscreen);
        ckbStatusbar = (CheckBox) findViewById(R.id.ckbApiTest_Statusbar);
        ckbHome = (CheckBox) findViewById(R.id.ckbApiTest_Home);
        ckbBack = (CheckBox) findViewById(R.id.ckbApiTest_Back);
        ckbMenu = (CheckBox) findViewById(R.id.ckbApiTest_Menu);        
        tbtnKeycodesToggle = (ToggleButton)findViewById(R.id.tbtnKeycodesToggle);
        txtKeycodes = (EditText)findViewById(id.txtApiTest_Keycodes);
        
        btnOK.setOnClickListener(btnOnClickedListener);
        btnCancel.setOnClickListener(btnOnClickedListener);
        
        Bundle bundle = this.getIntent().getExtras();
        ckbFullscreen.setChecked(bundle.getBoolean(ScanXmit2.Key_Fullscreen_FullScreen));
        ckbStatusbar.setChecked(bundle.getBoolean(ScanXmit2.Key_Fullscreen_StatusBar));
        ckbHome.setChecked(bundle.getBoolean(ScanXmit2.Key_Fullscreen_HomeKey));
        ckbBack.setChecked(bundle.getBoolean(ScanXmit2.Key_Fullscreen_BackKey));
        ckbMenu.setChecked(bundle.getBoolean(ScanXmit2.Key_Fullscreen_MenuKey));
        txtKeycodes.setText(bundle.getString(ScanXmit2.Key_Fullscreen_Keycodes));
        tbtnKeycodesToggle.setChecked(bundle.getBoolean(ScanXmit2.Key_Fullscreen_KeycodesEnabled));
        
        ckbFullscreen.setOnCheckedChangeListener(new OnCheckedChangeListener() {			
			@Override
			public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
				ckbStatusbar.setChecked(!isChecked);
				ckbHome.setChecked(!isChecked);
				ckbMenu.setChecked(!isChecked);
				ckbBack.setChecked(!isChecked);
			}
		});
    }
    
	public OnClickListener btnOnClickedListener = new OnClickListener(){
		@Override
		public void onClick(View v) {
			// {{ btnOK
			if(v.equals(btnOK))
			{
				Message msg = new Message();   
				msg.what = ScanXmit2.Identifier_FullscreenParameterChanged;  
				msg.obj = getSettingsString();        
        		ScanXmit2.hd_UiRefresh.sendMessage(msg);
				exitFlag = true;
				onBackPressed();				
			}
			// }} btnOK
			// {{ btnCancel
			else if (v.equals(btnCancel))
			{
				exitFlag = true;
				onBackPressed();
			}
			// }} btnCancel
			
		}		
	};
	
	private String getSettingsString(){
		String str ="";
		str+="fullscreen:"+String.valueOf(ckbFullscreen.isChecked())+";";
		str+="statusbar:"+String.valueOf(ckbStatusbar.isChecked())+";";
		str+="homekey:"+String.valueOf(ckbHome.isChecked())+";";
		str+="backkey:"+String.valueOf(ckbBack.isChecked())+";";
		str+="menukey:"+String.valueOf(ckbMenu.isChecked());
		String keycodes = txtKeycodes.getText().toString().trim();
		if(keycodes.length()>0){// && keycodes!=txtKeycodes.getHint().toString()){
			str+=";keycodes:"+txtKeycodes.getText().toString()+":"+String.valueOf(tbtnKeycodesToggle.isChecked());
		}
		return str;
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
}
