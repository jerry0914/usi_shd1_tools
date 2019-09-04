package com.usi.shd1_tools.scannerlibrary;

import java.util.EventObject;

public class ScannerDecodedMessageObject extends EventObject{
	private String myMessage = "";
	
	public ScannerDecodedMessageObject(Object source,String decodedMessage) 
	{			
		super(source);
		myMessage = decodedMessage; 		
	}
	
	public String DecodedMessage()
	{
		return 	myMessage;
	}

}
