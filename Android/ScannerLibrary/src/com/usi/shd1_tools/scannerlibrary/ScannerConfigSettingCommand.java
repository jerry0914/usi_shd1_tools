package com.usi.shd1_tools.scannerlibrary;

import java.util.ArrayList;

public class ScannerConfigSettingCommand {
	public enum CommandCategory{Unknow,Toggle,ScanParameter,DecodeParameter,ReaderParameter};
	public CommandCategory Category = CommandCategory.Unknow;
	public String Command = "";
	public ArrayList<String>Parameters = new ArrayList<String>();
	public ScannerConfigSettingCommand(CommandCategory category,String command)
	{
		Category = category;
		Command = command;
	}
}
