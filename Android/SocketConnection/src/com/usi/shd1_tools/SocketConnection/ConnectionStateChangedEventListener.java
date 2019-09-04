package com.usi.shd1_tools.SocketConnection;
import java.util.EventListener;

public interface ConnectionStateChangedEventListener extends EventListener {
	public void ConnectionStateChanged(ConnectionStateChangedObject obj);
}
