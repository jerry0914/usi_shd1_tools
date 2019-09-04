package com.usi.shd1_tools.scannerlibrary;

import java.util.EventListener;

public interface ScannerDecodedMessageListener extends EventListener{
	public void DecodeMessage(ScannerDecodedMessageObject obj);
}
