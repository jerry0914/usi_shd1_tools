package com.usi.shd1_tools.AndroidAgent;

import java.util.Map;

import android.R.id;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.util.Log;

import com.usi.shd1_tools.SocketConnection.Connection;
import com.usi.shd1_tools.SocketConnection.DataReceivedEventListener;
import com.usi.shd1_tools.SocketConnection.DataEventObject;
import com.usi.shd1_tools.commonlibrary.Logger;
import com.usi.shd1_tools.commonlibrary.Logger.Log_Level;

public class MainService extends Service {
	public static Context MainServiceContext;
	private Connection agentConnection;
	private String agentIP = "127.0.0.1";

	@Override
	public void onCreate() {
		super.onCreate();
	}

	@Override
	public IBinder onBind(Intent intent) {
		return null;
	}

	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		super.onStartCommand(intent, flags, startId);
		Logger.LogcatTAG = StaticVariables.LogTag;
		Logger.Start(StaticVariables.LogFolder, 128);
		Log.d("AT_Agent", "LogFoler=" + Logger.getLogPath());
		Logger.WriteLog("Main",
				"com.usi.shd1_tools.AndroidAgent.MainService onCreate...");
		MainServiceContext = getApplicationContext();
		Logger.WriteLog("Main",
				"com.usi.shd1_tools.AndroidAgent.MainService onStartCommand...");
		MainServiceContext = getApplicationContext();
		agentConnection = new Connection(agentIP, StaticVariables.AgentPort);
		agentConnection.Open();
		agentConnection
				.setDataReceivedEventListener(new DataReceivedEventListener() {
					@Override
					public void DateReceived(DataEventObject obj) {
						processReceiveData(obj);
					}
				});
		return START_STICKY;
	}

	@Override
	public void onDestroy() {
		Logger.WriteLog("Main",
				"com.usi.shd1_tools.AndroidAgent.MainService onDestroy...");
		if (agentConnection != null) {
			agentConnection.Close();
		}
		Logger.Stop();
		super.onDestroy();
	}

	private void processReceiveData(DataEventObject obj) {
		String replyMsg = null;
		String sendMsg = null;
		// sequenceNo 1~1024 = request from DUT
		Logger.WriteLog(
				"Main",
				"MainService.processReceiveData:("
						+ String.valueOf(obj.SequenceNo) + ")[" + obj.Header
						+ "]" + obj.Data);
		if (obj.SequenceNo > 0 && obj.SequenceNo <= 1024) {
			try {
				Map mData = obj.TransferDataToPair(); // Transfer parameters to
														// Map data type
				switch (obj.Header) {

				// {{ System Functions
				case "GetTopActivity":
					replyMsg = SystemFunctions.GetTopActivity("pkg+cls");
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				case "CheckTopActivity":
					// Parameter=activity_name
					String activity_name = String.valueOf(mData
							.get("activity_name"));
					// Parameter=timeout
					String timeout = String.valueOf(mData.get("timeout"));
					replyMsg = SystemFunctions.CheckTopActivity(activity_name,
							timeout);
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				// }} System Functions

				// {{ WiFi Functions
				case "SetWiFiState":
					String state = String.valueOf(mData.get("state"));
					replyMsg = WifiFunctions.SetWiFiState(state);
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				case "GetWiFiState":
					replyMsg = WifiFunctions.GetWiFiState();
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				case "IsWiFiConnected":
					replyMsg = String.valueOf(WifiFunctions.IsWiFiConnected());
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				case "RemoveAllWiFiConfiguredNetworks":
					replyMsg = String.valueOf(WifiFunctions.RemoveAllWiFiConfiguredNetworks());
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				case "IsWiFiAPExist":
					String ap_name = String.valueOf(mData.get("ap_name"));
					replyMsg = String.valueOf(WifiFunctions.IsWiFiAPExist(ap_name));
					sendMsg = processFunctionReplyMessage(replyMsg);
				case "CreateWiFiApConfig":
					// This is a template
					String ssid = String.valueOf(mData.get("ssid"));
					String pwd = String.valueOf(mData.get("pwd"));
					String security_method = String.valueOf(mData.get("security_method"));
					replyMsg = WifiFunctions.CreateWiFiApConfig(ssid, pwd, security_method);
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				case "CreateWiFiApConfig_Enterprise":
					// This is a template
					ssid = String.valueOf(mData.get("ssid"));
					String id = String.valueOf(mData.get("id"));
					pwd = String.valueOf(mData.get("pwd"));
					String eap_method = String.valueOf(mData.get("eap_method"));
					String phase_2 = String.valueOf(mData.get("phase_2"));
					replyMsg = WifiFunctions.CreateWiFiApConfig_Enterprise(ssid, id, pwd, eap_method, phase_2);
					break;
				// }} WiFi Functions
					
				case "Template":
					// This is a template
					String arg1 = String.valueOf(mData.get("arg1"));
					// replyMsg = SystemFunctions.FUNCTION_NAME(arg1);
					sendMsg = processFunctionReplyMessage(replyMsg);
					break;
				default:
					break;
				}
				Logger.WriteLog("Main", "ReplyMsg=" + sendMsg);
				if (sendMsg != null) {
					agentConnection.sendData(obj.SequenceNo, obj.Header,
							sendMsg);
				}
			} catch (Exception ex) {
				Logger.WriteLog("Main",
						"processReceiveData exception:" + ex.getMessage(),
						Log_Level.Error);
			}
		}

		else {
			// Reserved;
		}
	}

	private String processFunctionReplyMessage(String replyMsg) {
		String sendMsg = "";
		// Reply NG or Error
		if (replyMsg == StaticVariables.ReplyTag_Error
				|| replyMsg == StaticVariables.ReplyTag_NotOk) {
			sendMsg = StaticVariables.Reply_Tag + Connection.KW_KeyValueSpilter
					+ replyMsg;
		}
		// Reply OK
		else {
			sendMsg = StaticVariables.Reply_Tag + Connection.KW_KeyValueSpilter
					+ StaticVariables.ReplyTag_Ok
					+ Connection.KW_ParametersSpilter;
			sendMsg += StaticVariables.Reply_Result
					+ Connection.KW_KeyValueSpilter + replyMsg;
		}
		return sendMsg;
	}
}
