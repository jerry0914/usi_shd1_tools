package com.usi.shd1_tools.SocketConnection;
import java.util.EventListener;

public interface DataSentEventListener extends EventListener {
	public void DateSent(DataEventObject obj);
}
