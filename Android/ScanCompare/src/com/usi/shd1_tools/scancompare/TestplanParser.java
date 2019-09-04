package com.usi.shd1_tools.scancompare;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import android.app.Activity;
import android.content.*;
import android.content.SharedPreferences.Editor;
import android.widget.Toast;

import com.aspose.cells.*;
import com.usi.shd1_tools.commonlibrary.Logger;
import com.usi.shd1_tools.scannerlibrary.ScannerConfigSettingCommand;
import com.usi.shd1_tools.scannerlibrary.ScannerConfigSettingCommand.CommandCategory;

public class TestplanParser {
	public final static String Action_LoadTestPlanProgressUpdate = "com.usi.shd1_tools.TestplanParser.action.loadTpProgressUpdate";
	public final static String Action_LoadTestCaseUiUpdate = "com.usi.shd1_tools.TestplanParser.action.loadTcUiUpdate";
	public final static String ExtraHeader_Progress = "com.usi.shd1_tools.TestplanParser.extra.progress";
	public final static String ExtraHeader_Maximum = "com.usi.shd1_tools.TestplanParser.extra.maximum";
	public final static String ExtraHeader_tcLoadingFlag = "com.usi.shd1_tools.TestplanParser.extra.tcLoadingFlag";
	public final static String SharedPreferencesKey = "com.usi.shd1_tools.DataCompare.sharePreferences";
	public final static String SharedPreferencesName_TcIdColumn = "TcIdColnum";
	public final static String SharedPreferencesName_TcNameColumn = "TcNameColnum";
	public final static String SharedPreferencesName_TcProcedureColumn = "TcProcedureColnum";
	public final static String SharedPreferencesName_ExpectedResultColumn = "ExpectedResultColnum";
	public final static String SharedPreferencesName_ScanConfigSettingsCommandColumn = "ScanConfigSettingCommandColumn";
	public final static String SharedPreferencesName_DataStartRow = "DataStartRow";
	public final static String SharedPreferencesName_AutoDetectColumnFlag = "AutoDetectColumnFlag";
	public static int tcidColumn =15;
	public static int tcNameColumn = 1;
	public static int scanConfigSettingsCommandColumn = 3;
	public static int tcProcedureColumn = 4;
	public static int expectedResultColumn = 5;
	public static int dataStartRow = 15;

	private static String _testplanPath = "";
	private static Context _context;
	public static ArrayList<TestCase> TestcaseList = new ArrayList<TestCase>();
//	public static ArrayList<String> TestcaseList = new ArrayList<String>();
//	public static TestCase SelectedTestCase = null;
	private static Thread tdLoadTestPlan;
//	private static Thread tdLoadTestCase;
//	private static Thread tdExportScanConfig;
	private static boolean isTestPlanLoading = false;
	private static Context currentContext;
	public static boolean get_AutoDectedColumn_Flag(){
		boolean result = true;
		if(currentContext!=null){
			SharedPreferences mySharedPreference = currentContext.getSharedPreferences(TestplanParser.SharedPreferencesKey,Activity.MODE_PRIVATE);
			result = mySharedPreference.getBoolean(SharedPreferencesName_AutoDetectColumnFlag, true);
		}
		return result;
	}
	
	public static void set_AutoDectedColumn_Flag(boolean flag){
		SharedPreferences mySharedPreference = currentContext.getSharedPreferences(TestplanParser.SharedPreferencesKey,Activity.MODE_PRIVATE);
		Editor myEditor = mySharedPreference.edit();		
		myEditor.putBoolean(SharedPreferencesName_AutoDetectColumnFlag,flag);
		myEditor.commit();
	}
	
	public static void LoadTestPlan(final Context context, final String testplanPath){
		if(testplanPath!=null && testplanPath.length()>0)
		{
			if(!isTestPlanLoading)
			{
				if(get_AutoDectedColumn_Flag())
				{
					
				}
				else
				{
					loadSharedPreferences(context);
				}
				Toast.makeText(context, "Test plan is loading, this may take a few minutes, please wait...", Toast.LENGTH_LONG).show();
				_context = context;
				_testplanPath = testplanPath;
				tdLoadTestPlan = null;
				tdLoadTestPlan = new Thread(new Runnable() {			
					@Override
					public void run() {						
						loadTestPlan();
					}
				});
				isTestPlanLoading = true;
				tdLoadTestPlan.start();
			}
			else
			{			
				Toast.makeText(context, "Rejected, the loading task is still running.", Toast.LENGTH_SHORT).show();
			}	
		}
		else 
		{
			Toast.makeText(context, "Please select the test plan file first, please~~", Toast.LENGTH_SHORT).show();
		}
	}
	
	private static void loadTestPlan(){
		TestcaseList.clear();
		int startRow = 0;
		int rowCount = 0;
		Workbook wb =null;
		Worksheet ws = null;
		try 
		{		
			wb = new Workbook(_testplanPath);
			ws = wb.getWorksheets().get(0);
			rowCount = ws.getCells().getRows().getCount();
			int testcases = rowCount - startRow;
			for(int index = dataStartRow ; index<=testcases ; index++)
			{	
				try {
					Row row = ws.getCells().getRows().get(index+startRow);
					Intent intent = new Intent();
					intent.setAction(Action_LoadTestPlanProgressUpdate);
					intent.putExtra(ExtraHeader_Progress, index);
					intent.putExtra(ExtraHeader_Maximum, testcases);				
					_context.sendBroadcast(intent);
					TestCase tc = parseTestCaseInfo(row);
					if(tc!=null)
					{
						TestcaseList.add(tc);
					}
				}
				catch (Exception ex) 
				{
					String msg = ex.getMessage();					
					ex.printStackTrace();
				} 
			}
		}
		 catch (Exception et) 
		{
			 //Logger.WriteLog("ERROR", "LoadTestPlan exception2 , message = "+ et.getMessage());
		     //Log.e("ATDebug","LoadTestPlan exception2 , message = "+ et.getMessage());
			 //Log.e("ATDebug","LoadTestPlan exception2 , StackTrace = "+ et.getStackTrace().toString());
		}
		finally
		{
			ws = null;
			wb = null;
			isTestPlanLoading = false;
		}
	}
	
//	private static String preloadTestCaseInfo(Row row){
//		String rtnValue = "";
//		if(row!=null)
//		{			
//			String tcName = row.getCellOrNull(tcNameColumn).getStringValue().trim();
//			String tcid = row.getCellOrNull(tcidColumn).getStringValue().trim();			
//			String regTCID = "\\s*(?i)VT\\d{3,}-\\d{4,}\\s*";
//			Pattern patternTCID = Pattern.compile(regTCID);
//			Matcher matchesTCID = patternTCID.matcher(tcid);
//			if(tcid!=null && matchesTCID.find())
//			{
//				rtnValue = tcName +" - "+ tcid;
//			}
//		}
//		return rtnValue;
//	}
	
	public static int getExcelColnumIndexWithCharactor(String colChar){
		int val= 0;
		int diff=0;
		for(int index=0;index<colChar.length();index++)
		{
			diff = colChar.charAt(index) - 'A'+1;
			if(diff>=0 && diff<=25)
			{
				for(int power=0;power<index ;power++)
				{
					diff = diff*26;
				}
				val+=diff;
			}
		}
		val = val-1;
		return val;
	}
	
	public static String getExcelColnumStringFormInt(int index){
		String result = "";
		int quotient =0,remainder=0;
		index = index+1;
		do{
			quotient = index/26;
			remainder = index%26;
			result = String.valueOf((char)((int)'A'+remainder-1))+result;
			index = quotient;
		}while(quotient>0);
		return result;
	}
	
//	public static void refreshSelectedTestCase(final int selectedIndex){
//		tdLoadTestCase = new Thread(new Runnable() {			
//			@Override
//			public void run() {	
//				SelectedTestCase = getSelectedTestCase(selectedIndex);
//				}
//			});
//	}
//		
//	private static TestCase getSelectedTestCase(int selectedIndex){
//		TestCase selectedTestCase = null;
//		Workbook wb = null;
//		Worksheet ws = null;
//		
//		try
//		{
//			wb = new Workbook(_testplanPath);
//			ws = wb.getWorksheets().get(0);
//			Row row = ws.getCells().getRows().get(selectedIndex+dataStartRow);
//			selectedTestCase = parseTestCaseInfo(row);
//			
//		}
//		catch(Exception ex){
//			selectedTestCase = null;
//		}
//		finally
//		{
//			ws = null;
//			wb = null;
//		}
//		return selectedTestCase;
//	}
		
	private static TestCase parseTestCaseInfo(Row row){
		TestCase tc = null;
		if(row!=null)
		{			
			String tcProcedure = row.getCellOrNull(tcProcedureColumn).getStringValue().trim();
			String strDescription = "";
			String regDescription = "^\\[(\\s|\\S)*((?i)barcode(\\s*)#\\d+(\\S|\\s)*)\\]";
			Pattern patternDescription = Pattern.compile(regDescription);
			Matcher matchesDescription = patternDescription.matcher(tcProcedure);
			if(matchesDescription.find())
			{
				strDescription = matchesDescription.group(2);
			}
			String tcName = row.getCellOrNull(tcNameColumn).getStringValue().trim();
			String tcid = row.getCellOrNull(tcidColumn).getStringValue().trim();
			
			String expectedResult = row.getCellOrNull(expectedResultColumn).getStringValue();

			String regTCID = "\\s*(?i)VT\\d{3,}-\\d{4,}\\s*";
			Pattern patternTCID = Pattern.compile(regTCID);
			Matcher matchesTCID = patternTCID.matcher(tcid);
			if(tcid!=null && expectedResult!=null && matchesTCID.find())
			{
				tc = new TestCase(tcName,tcid);
				tc.Description = strDescription;
				tc.ExpectedResult = expectedResult;
				//{{ Get the scanner config setting commands
				if(scanConfigSettingsCommandColumn>=0)
				{
					String cfgSettingsString = row.getCellOrNull(scanConfigSettingsCommandColumn).getStringValue().trim();
					tc.ScanConfigSettingCommands = parseScanConfigSettingCommands(cfgSettingsString);
				}
				//}} Get the scanner config setting commands
			}
		}
		return tc;
	}
	
	private static ArrayList<ScannerConfigSettingCommand> parseScanConfigSettingCommands(String cmdText){
		ArrayList<ScannerConfigSettingCommand> scanConfigcommands = new ArrayList<ScannerConfigSettingCommand>();
		cmdText = cmdText.split("\\*")[0];
		CommandCategory cmdCategory = CommandCategory.Unknow;
		String[] tempTexts1 = cmdText.split("\\+");
		for(int i1=0;i1<tempTexts1.length;i1++)
		{
			String[] subCmdStrings = tempTexts1[i1].split(":");
			cmdCategory = CommandCategory.Unknow;
			String cmdName = "",param = "";
			//{{Get the command category
			if(subCmdStrings.length==2)
			{
				cmdCategory = CommandCategory.Toggle;
				cmdName = subCmdStrings[0];
				param = subCmdStrings[1];
			}
			else if(subCmdStrings.length==3)
			{
				if(subCmdStrings[0].equals("ReaderParams"))
				{
					cmdCategory = CommandCategory.ReaderParameter;
					cmdName = subCmdStrings[1];
					param = subCmdStrings[2];
				}
				else if(subCmdStrings[0].equals("Params")||subCmdStrings[0].equals("ScanParams"))
				{
					cmdCategory = CommandCategory.ScanParameter;
					cmdName = subCmdStrings[1];
					param = subCmdStrings[2];
				}
				else
				{
					cmdCategory = CommandCategory.DecodeParameter;
					cmdName = subCmdStrings[0]+"_"+subCmdStrings[1];
					param = subCmdStrings[2];
				}				
			}
			else
			{
				continue;
			}
			//}}Get the command category
			if(!cmdCategory.equals(CommandCategory.Unknow))
			{
				ScannerConfigSettingCommand cfgSettingCmd = new ScannerConfigSettingCommand(cmdCategory,cmdName);
				cfgSettingCmd.Parameters.add(param);
				scanConfigcommands.add(cfgSettingCmd);
			}			
		}		
		return scanConfigcommands;
	}
	
	public static int GetExcelColumnIndexByCharactors(String charactors){
		int result = 0;
		int diff = 0;
		charactors = charactors.toUpperCase();
		for(int index=0;index<charactors.length();index++)
		{
			diff = charactors.charAt(index)-'A'+1;
			for(int power=0;power<index;power++)
			{
				diff = diff*26;
			}
			result+=diff;
		}
		return result-1;
	}
	
	private static void loadSharedPreferences(final Context context){
		SharedPreferences mySharedPreference = context.getSharedPreferences(TestplanParser.SharedPreferencesKey,Activity.MODE_PRIVATE);
		String strTcidCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_TcIdColumn,"");
		TestplanParser.tcidColumn = TestplanParser.GetExcelColumnIndexByCharactors(strTcidCol);
		
		String strTcNameCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_TcNameColumn,"");
		TestplanParser.tcNameColumn = TestplanParser.GetExcelColumnIndexByCharactors(strTcNameCol);
		
		String strProcedureCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_TcProcedureColumn,"");
		TestplanParser.tcProcedureColumn = TestplanParser.GetExcelColumnIndexByCharactors(strProcedureCol);

		String strExpectedResultCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_ExpectedResultColumn,"");
		TestplanParser.expectedResultColumn =  TestplanParser.GetExcelColumnIndexByCharactors(strExpectedResultCol);
		
		String strScanConfigCol = mySharedPreference.getString(TestplanParser.SharedPreferencesName_ScanConfigSettingsCommandColumn,"");
		TestplanParser.scanConfigSettingsCommandColumn =  TestplanParser.GetExcelColumnIndexByCharactors(strScanConfigCol);
		
		TestplanParser.dataStartRow = mySharedPreference.getInt(TestplanParser.SharedPreferencesName_DataStartRow,TestplanParser.dataStartRow);		
	}
	
	private static void saveSharedPreferences(final Context context){
		SharedPreferences mySharedPreference = context.getSharedPreferences(TestplanParser.SharedPreferencesKey,Activity.MODE_PRIVATE);
		Editor myEditor = mySharedPreference.edit();		
		myEditor.putString(SharedPreferencesName_TcIdColumn,getExcelColnumStringFormInt(tcidColumn));
		myEditor.putString(SharedPreferencesName_TcNameColumn,getExcelColnumStringFormInt(tcNameColumn));
		myEditor.putString(SharedPreferencesName_TcProcedureColumn,getExcelColnumStringFormInt(tcProcedureColumn));
		myEditor.putString(SharedPreferencesName_ExpectedResultColumn,getExcelColnumStringFormInt(expectedResultColumn));
		myEditor.putString(SharedPreferencesName_ScanConfigSettingsCommandColumn,getExcelColnumStringFormInt(scanConfigSettingsCommandColumn));
		myEditor.putInt(SharedPreferencesName_DataStartRow,TestplanParser.dataStartRow);
		myEditor.putBoolean(SharedPreferencesName_AutoDetectColumnFlag,true);
		myEditor.commit();
	}
	
	public static void ExportScanConfig(Context context, String exportFolder){
//		File file = new File(exportFolder+"ScanConfig.csv");
//		String path = file.getCanonicalPath();
//		String fs = System.getProperty("file.separator");
//		String fileName = path + fs + "myFile.txt";
		FileWriter fileWriter = null;
		String filePath = exportFolder+"ScanConfig.csv";		
		try
		{
			if(TestcaseList.size()>0)
			{
				File f = new File(filePath);
				if(f.exists())
				{
					f.delete();
				}
				Logger.CreateNewFile(filePath);
				fileWriter = new FileWriter(filePath, true);
				for(int index =0; index<TestcaseList.size();index++)
				{
					TestCase tc =null;
					String configCmd = "\"";
					try
					{
						tc = TestcaseList.get(index);
						if(tc!=null)
						{
							if(tc.ScanConfigSettingCommands!=null)
							{
								for(int j=0;j<tc.ScanConfigSettingCommands.size();j++)
								{
									configCmd += tc.ScanConfigSettingCommands.get(j).Category.toString()+" - " + 
												 tc.ScanConfigSettingCommands.get(j).Command+" : "+ 
											     tc.ScanConfigSettingCommands.get(j).Parameters.get(0) + "\n";
								}
							}
							configCmd+= "\"";
							fileWriter.append(tc.Name+","+tc.ID+","+configCmd+"\n");					
							fileWriter.flush();
						}
						else
						{
							Thread.sleep(1000);
						}
					}
					catch(Exception inEx)
					{
						fileWriter.append(index+"exception, message = "+inEx.toString()+"\n");
						//Ignore exception and continue
					}
				}
				Toast.makeText(context, "Export scan config to "+ filePath, Toast.LENGTH_SHORT).show();
			}
			else{
				Toast.makeText(context, "Fail to export scan config", Toast.LENGTH_SHORT).show();
			}
		}
		catch(Exception ex)
		{
			Toast.makeText(context, "Fail to export scan config, exception message  = "+ex.toString(), Toast.LENGTH_SHORT).show();
		}		
		finally 
		{
			try 
			{
				if(fileWriter!=null)
				{
					fileWriter.close();
				}
			} catch (IOException e) 
			{
			}
		}		
	}

	@SuppressWarnings("deprecation")
	public static void autoDetectColumn(Context context,final String testplanPath){
		currentContext = context;
		if(get_AutoDectedColumn_Flag()){
			try{
				Workbook wbook = new Workbook(testplanPath);
				Worksheet wsheet = wbook.getWorksheets().get(0);
				Row titleRow  = wsheet.getCells().getRow(dataStartRow-1);
				for(int index =0; index<50;index++){
					Cell cell = titleRow.getCellOrNull(index);
					if(cell!=null){
						String strTitle = cell.getStringValue().toLowerCase();
						if(strTitle.contains("name")){
							tcNameColumn = index;
						}
						else if(strTitle.equals("parameters")){
							scanConfigSettingsCommandColumn = index;
						}
						else if(strTitle.contains("test id")){
							tcidColumn = index;
						}
						else if(strTitle.equals("procedure")){
							tcProcedureColumn = index;
						}
						else if(strTitle.contains("expected")){
							expectedResultColumn = index;
						}
					}
				}
				saveSharedPreferences(currentContext);				
			}
			catch(Exception ex){	
			}
		}
	}
}
