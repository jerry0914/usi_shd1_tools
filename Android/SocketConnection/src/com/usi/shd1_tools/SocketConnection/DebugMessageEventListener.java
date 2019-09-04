package com.usi.shd1_tools.SocketConnection;
import java.util.EventListener;

public interface DebugMessageEventListener extends EventListener {
	public void DebugMessageReceived(DebugMessageEventObject obj);
}
