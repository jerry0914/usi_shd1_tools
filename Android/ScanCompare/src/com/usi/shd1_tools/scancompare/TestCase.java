package com.usi.shd1_tools.scancompare;
import java.util.ArrayList;

import android.os.*;
import com.usi.shd1_tools.scannerlibrary.ScannerConfigSettingCommand;

public class TestCase extends Object implements Parcelable{
	public String Name = "";
	public String ID = "";
	public String ExpectedResult = "";
	public String Description = "";
	public String ScanResult = "";
	public ArrayList<ScannerConfigSettingCommand> ScanConfigSettingCommands = new ArrayList<ScannerConfigSettingCommand>();
	public TestCase(){		
	}
	
	public TestCase(String name,String tcid){
		Name = name;
		ID=tcid;
	}
	@Override
	public int describeContents(){
		return 0;
	}
	@Override
	public void writeToParcel(Parcel dest, int flags) {		
	}
	
}
