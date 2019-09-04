package com.usi.shd1_tools.scannerlibrary;

import java.util.EventListener;

public interface ScannerStateChangedEventListener extends EventListener{
	public void StateChangedMessage(ScannerStateChangedEventObject eventObj);
}
