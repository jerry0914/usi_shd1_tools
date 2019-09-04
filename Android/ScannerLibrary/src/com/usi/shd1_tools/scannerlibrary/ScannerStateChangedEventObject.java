package com.usi.shd1_tools.scannerlibrary;
import java.util.EventObject;
import com.symbol.scanning.StatusData.ScannerStates;

public class ScannerStateChangedEventObject extends EventObject{
	private ScannerStates currentState = ScannerStates.DISABLED;
	public ScannerStates getScannerStates()
	{
		return currentState;
	}
	public ScannerStateChangedEventObject(Object source,ScannerStates state) {		
		super(source);
		currentState = state;
	}
}
