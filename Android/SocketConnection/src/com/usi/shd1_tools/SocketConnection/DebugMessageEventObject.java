package com.usi.shd1_tools.SocketConnection;

public class DebugMessageEventObject {
	public String Tag = "";
	public String DebugMsg = "";

	public DebugMessageEventObject(String tag, String msg) {
		Tag = tag;
		DebugMsg = msg;
	}
	
	public DebugMessageEventObject(String msg) {
		this("", msg);
	}

	@Override
	public String toString() {
		String str = "";
		if (Tag.length() == 0)
			str = DebugMsg;
		else {
			str = "[" + Tag + "] " + DebugMsg;
		}
		return str;
	}
}
