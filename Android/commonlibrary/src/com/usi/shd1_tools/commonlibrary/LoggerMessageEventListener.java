package com.usi.shd1_tools.commonlibrary;
import java.util.EventListener;

public interface LoggerMessageEventListener extends EventListener{
	 public void messageArrived(LoggerMessageEventObject e);	
}
