package com.usi.shd1_tools.SocketConnection;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.AbstractMap;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.Vector;
import com.usi.shd1_tools.commonlibrary.Logger;

import android.util.Log;

public class Connection {
	public static final String HOST = "127.0.0.1";
	public final int LISTENING_PORT;
	public final int SENDING_PORT;
	private Thread td_socket_listening, td_socket_sending,
			tdProcessReceivingData;// ,tdSendData;
	private ServerSocket ssocket_listening, ssocket_sending;
	private Socket socket_listening, socket_sending;
	private int listenInterval = 500, receivingProcessInterval = 500;

	private boolean run_flag = false;
	// private List<String> sendDataList;
	private List<String> receivingDataList;

	public boolean isRunning() {
		return run_flag;
	}

	// private Vector<ConnectionStateChangedEventListener>
	// connectionChangedListeners = null;
	private Vector<DataReceivedEventListener> dataReceivedListeners = null;
	private Vector<DataSentEventListener> dataSentListeners = null;
	private Vector<DebugMessageEventListener> debugMessageEventListeners = null;

	public enum ConnectStates {
		Disconnected, Connected
	};

	public final static String KW_SequenceSpilter = "`#";
	public final static String KW_HeaderSpilter = "`@";
	public final static String KW_ParametersSpilter = "`;";
	public final static String KW_KeyValueSpilter = "`=";

	public Connection(String ip, int port) {
		LISTENING_PORT = port;
		SENDING_PORT = port + 1;
	}

	public void Open() {
		if (!run_flag) {
			run_flag = true;
			tdSocket_listening_start();
			tdSocket_sending_start();
		}
	}

	private void tdSocket_listening_start() {
		if (td_socket_listening != null && td_socket_listening.isAlive()) {
			sendDebugMessageEvent("Socket's already opened");
			Logger.WriteLog("Connection", "Socket's already opened");
			return;
		} else {
			Logger.WriteLog("Connection", "Socket is opening");
			if (td_socket_listening != null) {
				try {
					td_socket_listening.join();
					td_socket_listening = null;
				} catch (Exception ex) {
					sendDebugMessageEvent("Fail to initialize td_socket_listening,message="
							+ ex.getMessage());
					Logger.WriteLog("Connection",
							"Fail to initialize td_socket_listening,message="
									+ ex.getMessage());
				}
			}
			try {
				ssocket_listening = new ServerSocket(LISTENING_PORT);
			} catch (IOException e1) {
				Log.d("AT_Debug1", "create ssocket_listening object exception:"
						+ e1.getMessage());
				sendDebugMessageEvent("Waiting for listening socket connection...");
			}
			td_socket_listening = new Thread(new Runnable() {
				@Override
				public void run() {
					while (run_flag) {
						try {
							Logger.WriteLog("Connection",
									"Waiting for listening connection...");
							sendDebugMessageEvent("Waiting for listening socket connection...");
							socket_listening = ssocket_listening.accept();
							Logger.WriteLog("Connection", "listening socket connected");
							sendDebugMessageEvent("listening socket connected");
							receiveDate_listening();

						} catch (Exception e) {
							Logger.WriteLog("Connection",
									"Waiting for listening connection exception, message="
											+ e.getMessage());
						} finally {
							try {
								Thread.sleep(listenInterval);
							} catch (InterruptedException e) {
								e.printStackTrace();
							}
						}
					}
					sendDebugMessageEvent("td_socket_listening end");
				}
			});
			td_socket_listening.start();
			Logger.WriteLog("Connection", "td_socket_listening.start");
		}
	}

	private void tdSocket_sending_start() {
		if (td_socket_sending != null && td_socket_sending.isAlive()) {
			sendDebugMessageEvent("Socket's already opened");
			Logger.WriteLog("Connection", "Socket's already opened");
			return;
		} else {
			Logger.WriteLog("Connection", "Socket is opening");
			if (td_socket_sending != null) {
				try {
					td_socket_sending.join();
					td_socket_sending = null;
				} catch (Exception ex) {
					sendDebugMessageEvent("Fail to initialize td_socket_sending,message="
							+ ex.getMessage());
					Logger.WriteLog("Connection",
							"Fail to initialize td_socket_sending,message="
									+ ex.getMessage());
				}
			}
			try {
				ssocket_sending = new ServerSocket(SENDING_PORT);
			} catch (IOException e1) {
				Log.d("AT_Debug1", "create ssocket_sending object exception:"
						+ e1.getMessage());
			}
			td_socket_sending = new Thread(new Runnable() {
				@Override
				public void run() {
					while (run_flag) {
						try {
							Logger.WriteLog("Connection",
									"Waiting for sending socket connection...");
							sendDebugMessageEvent("Waiting for sending socket connection...");
							socket_sending = ssocket_sending.accept();
							Logger.WriteLog("Connection", "Sending socket connected");
							sendDebugMessageEvent("Sending socket connected");
						} catch (Exception e) {
							Logger.WriteLog("Connection",
									"Waiting for sending connection exception, message="
											+ e.getMessage());
							if (socket_sending != null) {
								try {
									socket_sending.close();
								} catch (IOException e1) {
									e1.printStackTrace();
								}
								socket_sending = null;
							}
						} finally {
							try {
								Thread.sleep(listenInterval);
							} catch (InterruptedException e) {
								e.printStackTrace();
							}
						}
					}
					sendDebugMessageEvent("td_socket_sending end");
				}
			});
			td_socket_sending.start();
			Logger.WriteLog("Connection", "td_socket_sending.start");
		}
	}

	private void tdProcessReceivingData_start() {
		if (tdProcessReceivingData == null) {
			tdProcessReceivingData = new Thread(new Runnable() {
				@Override
				public void run() {
					processReceivingData_runnable();
				}
			});
			tdProcessReceivingData.start();
			sendDebugMessageEvent("tdProcessReceivingData.start");
			Logger.WriteLog("Connection", "tdProcessReceivingData.start");
		}
	}

	public void Close() {
		// stopHeartbeatCheck();
		run_flag = false;
		try {

			if (ssocket_listening != null) {
				Logger.WriteLog("Connection", "ssocket_listening closing...");
				ssocket_listening.close();
				Logger.WriteLog("Connection", "ssocket_listening closed...");
				ssocket_listening = null;
			}

			if (ssocket_sending != null) {
				Logger.WriteLog("Connection", "ssocket_sending closing...");
				ssocket_sending.close();
				Logger.WriteLog("Connection", "ssocket_sending closed...");
				ssocket_sending = null;
			}
			
			if(socket_listening!=null){
				Logger.WriteLog("Connection", "socket_listening closing...");
				socket_listening.close();
				Logger.WriteLog("Connection", "socket_listening closed...");
				socket_listening = null;
			}
			
			if(socket_sending!=null){
				Logger.WriteLog("Connection", "socket_sending closing...");
				socket_sending.close();
				Logger.WriteLog("Connection", "socket_sending closed...");
				socket_sending = null;
			}

			if (td_socket_listening != null) {
				Logger.WriteLog("Connection", "td_socket_listening closing...");
				td_socket_listening.join();
				td_socket_listening = null;
				Logger.WriteLog("Connection", "td_socket_listening closed...");
			}

			if (td_socket_sending != null) {
				Logger.WriteLog("Connection", "td_socket_sending closing...");
				td_socket_sending.join();
				td_socket_sending = null;
				Logger.WriteLog("Connection", "td_socket_sending closed...");
			}

			if (tdProcessReceivingData != null) {
				Logger.WriteLog("Connection", "tdProcessReceivingData closed...");
				tdProcessReceivingData.join();
				tdProcessReceivingData = null;
				Logger.WriteLog("Connection", "tdProcessReceivingData closed...");
			}
			if (receivingDataList != null) {
				receivingDataList.clear();
			}
		} catch (InterruptedException e) {
			e.printStackTrace();
		} catch (Exception ex) {

		}
	}

	private String receiveDate_listening() {
		DataInputStream in = null;
		String input = "";
		try {
			BufferedReader br = new BufferedReader(new InputStreamReader(
					socket_listening.getInputStream()));

			input = br.readLine();
			if (input != null) {
				Logger.WriteLog("Connection", "Receive data = " + input);
				sendDebugMessageEvent("Receive data = " + input);
				processReveivingData(input);
			}
		} catch (Exception e) {
			Logger.WriteLog("Connection", "Receive message exception:" + e.getMessage());
			e.printStackTrace();
		} finally {
			if (in != null)
				try {
					in.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
		}
		return input;
	}

	private void processReveivingData(String receivingData) {
		if (receivingDataList == null) {
			receivingDataList = new ArrayList<String>();
		}
		sendDebugMessageEvent("Add to receivedDataList:" + receivingData);
		receivingDataList.add(receivingData);
		tdProcessReceivingData_start();
	}

	private void processReceivingData_runnable() {
		sendDebugMessageEvent("processReceivingData_runnable start");
		Logger.WriteLog("Connection", "processReceivingData_runnable start");
		int sequenceNo = -1;
		String header = "";
		String data = "";
		while (run_flag) {
			while (receivingDataList != null && !receivingDataList.isEmpty()) {
				String receivingData = receivingDataList.remove(0);
				sendDebugMessageEvent("processReceivingData_runnable = "
						+ receivingData);
				Logger.WriteLog("Connection", "processReceivingData_runnable = "
						+ receivingData);
				if (receivingData.contains(KW_SequenceSpilter)) {
					String[] strs = receivingData.split(KW_SequenceSpilter);
					sequenceNo = Integer.parseInt(strs[0]);
					receivingData = strs[1];
				}
				if (receivingData.contains(KW_HeaderSpilter)) {
					String[] subStrs = receivingData.split(KW_HeaderSpilter);
					header = subStrs[0];
					data = subStrs[1];
					sendDataReceivedEvent(sequenceNo, header, data);
				} else {
					header = receivingData;
					sendDataReceivedEvent(sequenceNo, header, data);
				}
				try {
					Thread.sleep(receivingProcessInterval);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
			try {
				Thread.sleep(receivingProcessInterval);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

	@SuppressWarnings("unchecked")
	private void sendDataReceivedEvent(int sequenceNo, String header,
			Object data) {
		if (dataReceivedListeners != null && !dataReceivedListeners.isEmpty()) {
			DataEventObject obj = new DataEventObject(sequenceNo, header, data);
			Vector<DataReceivedEventListener> targets;
			synchronized (this) {
				targets = (Vector<DataReceivedEventListener>) dataReceivedListeners
						.clone();
			}
			// walk through the listener list and
			// call the sunMoved method in each
			Enumeration<DataReceivedEventListener> e = targets.elements();
			Logger.WriteLog("Connection",
					"sendDataReceivedEvent,send data=" + obj.toString());
			while (e.hasMoreElements()) {
				DataReceivedEventListener l = (DataReceivedEventListener) e
						.nextElement();
				l.DateReceived(obj);
			}

		}
	}

	@SuppressWarnings("unchecked")
	private void sendDataSentEvent(String str) {
		int sequenceNo = -1;
		String header = "", data = "";
		if (dataSentListeners != null && !dataSentListeners.isEmpty()) {
			if (str.contains(KW_SequenceSpilter)) {
				String[] strs = str.split(KW_SequenceSpilter);
				sequenceNo = Integer.parseInt(strs[0]);
				str = strs[1];
			}
			if (str.contains(KW_HeaderSpilter)) {
				String[] subStrs = str.split(KW_HeaderSpilter);
				header = subStrs[0];
				data = subStrs[1];
			} else {
				data = str;
			}
			DataEventObject obj = new DataEventObject(sequenceNo, header, data);
			Vector<DataSentEventListener> targets;
			synchronized (this) {
				targets = (Vector<DataSentEventListener>) dataSentListeners
						.clone();
			}
			// walk through the listener list and
			// call the sunMoved method in each
			Enumeration<DataSentEventListener> e = targets.elements();
			while (e.hasMoreElements()) {

				DataSentEventListener l = (DataSentEventListener) e
						.nextElement();
				l.DateSent(obj);
			}
		}
	}

	@SuppressWarnings("unchecked")
	private void sendDebugMessageEvent(String DebugMsg) {
		if (debugMessageEventListeners != null
				&& !debugMessageEventListeners.isEmpty()) {
			DebugMessageEventObject obj = new DebugMessageEventObject(DebugMsg);
			Vector<DebugMessageEventListener> targets;
			synchronized (this) {
				targets = (Vector<DebugMessageEventListener>) debugMessageEventListeners
						.clone();
			}
			// walk through the listener list and
			// call the sunMoved method in each
			Enumeration<DebugMessageEventListener> e = targets.elements();
			while (e.hasMoreElements()) {

				DebugMessageEventListener l = (DebugMessageEventListener) e
						.nextElement();
				l.DebugMessageReceived(obj);
			}
		}
	}

	public void sendData(int sequenceNo, String header, String msg) {
		String strData = String.valueOf(sequenceNo) + KW_SequenceSpilter
				+ header + KW_HeaderSpilter + msg;
		sendDebugMessageEvent("sendData,raw string=" + strData);
		__sendDataToClient(strData);
	}

	public void sendData(int sequenceNo, String header,
			List<AbstractMap.SimpleEntry<String, Object>> keyValueList) {
		String data = "";
		while (keyValueList.size() > 0) {
			AbstractMap.SimpleEntry<String, Object> entery = keyValueList
					.get(0);
			data += String.valueOf(entery.getKey()) + KW_KeyValueSpilter
					+ String.valueOf(entery.getValue());
			data += KW_ParametersSpilter;
			keyValueList.remove(0);
		}
		sendData(sequenceNo, header, data);
	}

	private void __sendDataToClient(String data) {
		try {
			if (socket_sending != null) {
				OutputStream ops = socket_sending.getOutputStream();
				// bw = new BufferedWriter(new OutputStreamWriter(
				// clientSoc.getOutputStream()));
				BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(
						ops));
				Logger.WriteLog("Connection", "Send message:" + data);
				bw.write(data);
				Logger.WriteLog("Connection", "Send message, flush()");
				bw.flush();
				sendDataSentEvent(data);
			}
		} catch (Exception ex) {

		} finally {

		}
	}

	synchronized public void setDataReceivedEventListener(
			DataReceivedEventListener listener) {
		if (dataReceivedListeners == null) {
			dataReceivedListeners = new Vector<DataReceivedEventListener>();
		}
		if (!dataReceivedListeners.contains(listener)) {
			dataReceivedListeners.add(listener);
		}
	}

	synchronized public void setDataSentEventListener(
			DataSentEventListener listener) {
		if (dataSentListeners == null) {
			dataSentListeners = new Vector<DataSentEventListener>();
		}
		if (!dataSentListeners.contains(listener)) {
			dataSentListeners.add(listener);
		}
	}

	synchronized public void setDebugEventListener(
			DebugMessageEventListener listener) {
		if (debugMessageEventListeners == null) {
			debugMessageEventListeners = new Vector<DebugMessageEventListener>();
		}
		if (!debugMessageEventListeners.contains(listener)) {
			debugMessageEventListeners.add(listener);
		}
	}

	synchronized public void removeDataReceivedEventListener(
			DataReceivedEventListener listener) {
		if (dataReceivedListeners != null) {
			dataReceivedListeners.remove(listener);
		}
	}

	synchronized public void removeDataSentEventListener(
			DataSentEventListener listener) {
		if (dataSentListeners != null) {
			dataSentListeners.remove(listener);
		}
	}

	synchronized public void removeDebugMessageEventListener(
			DebugMessageEventListener listener) {
		if (debugMessageEventListeners != null) {
			debugMessageEventListeners.remove(listener);
		}
	}

}
