package com.usi.shd1_tools.AndroidAgent;

import android.annotation.SuppressLint;

@SuppressLint("SdCardPath")
public class StaticVariables {
	public final static String LogTag = "AT_Agent";
	public final static String LogFolder = "/sdcard/com/usi/shd1_tools/AndroidAgent/Logs/";
	protected static int AgentPort = 15000;
	
	public final static String Reply_Tag = "reply_tag";
	public final static String ReplyTag_Ok ="OK";
	public final static String ReplyTag_NotOk ="NG";
	public final static String ReplyTag_Error ="ERROR";	
	public final static String Reply_Result = "result";	
}
