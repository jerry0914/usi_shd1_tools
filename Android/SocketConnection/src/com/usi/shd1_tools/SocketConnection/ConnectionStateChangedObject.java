package com.usi.shd1_tools.SocketConnection;

import com.usi.shd1_tools.SocketConnection.Connection.ConnectStates;

public class ConnectionStateChangedObject {
	public ConnectStates State = ConnectStates.Disconnected;

	public ConnectionStateChangedObject(ConnectStates state) {
		State = state;
	}

	public boolean IsConnected() {
		return State.equals(ConnectStates.Connected);
	}
}
