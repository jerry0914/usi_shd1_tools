package com.usi.shd1_tools.SocketConnection;
import java.util.EventListener;

public interface DataReceivedEventListener extends EventListener {
	public void DateReceived(DataEventObject obj);
}
