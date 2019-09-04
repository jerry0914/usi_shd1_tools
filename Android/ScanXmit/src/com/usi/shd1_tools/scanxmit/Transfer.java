package com.usi.shd1_tools.scanxmit;

import java.io.InputStream;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.Socket;
import com.usi.shd1_tools.commonlibrary.Logger;
import android.os.StrictMode;
import android.util.Log;

public class Transfer {

	private static String TAG = "Transfer";

	private static final int SOCKET_TIMEOUT = 5000;
	private Socket mSocket = null;
	private InputStream mInput = null;
	private PrintWriter mPrintWriter = null;

	public String Data = "";
	private String msgFromServer = "";

	public Transfer() {
	}

	public void SaveData(String _data) {
		Data = Data + _data;
	}

	private void ClearData() {
		Data = "";
	}

	private String CheckSize(String _msg, int _size) {
		int realSize = _msg.length();
		if(realSize < _size) {
			for (int a = realSize; a < _size; a++) {
				_msg = _msg + "x";
			}
		}
		else if(realSize > _size) {
			_msg = _msg.substring(0, _size);
		}
		return _msg;
	}

	public boolean ConnectToServer(String ip, int port) {
		if (android.os.Build.VERSION.SDK_INT > 9) {
		    StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
		    StrictMode.setThreadPolicy(policy);
		}
		if (null != mSocket && mSocket.isConnected()) {
			Logger.WriteLog( "Info","Try to connect to "+ip+":"+port+", but had already connected to it.",Logger.Log_Level.Information,true);
            return false;
        }
		Socket mSocket = null;
		try {
			mSocket = new Socket();
			mSocket.connect(new InetSocketAddress(ip, port), SOCKET_TIMEOUT);
			mInput = mSocket.getInputStream();
			mPrintWriter = new PrintWriter(mSocket.getOutputStream(), true);
		} catch(Exception e) {
			Log.e(TAG, e.toString());
			Logger.WriteLog("Error","Connect to "+ip+":"+port+" unsuccessfully, message = "+e.getMessage(),Logger.Log_Level.Information,true);
			return false;
		}
		Logger.WriteLog("Action","Connect to "+ip+":"+port+" successfully",Logger.Log_Level.Information,true);
		return true;
	}

	public String SendDataToServer(String msg) {
		try {
			mPrintWriter.print(msg);
			mPrintWriter.flush();
			msgFromServer = "";
			ClearData();
			byte[] rebyte = new byte[1448];
			int dataLength = 0;
			do
			{
				dataLength = mInput.read(rebyte);
				msgFromServer += new String(new String(rebyte, 0, dataLength));
			}while(dataLength>=1448);
			Logger.WriteLog( "CheckPoint","Send data to server successfully, data = " + msg,Logger.Log_Level.Information);
			Logger.WriteLog( "Info","Receive data from server, length = "+msgFromServer.length()+ ", message = " + msgFromServer ,Logger.Log_Level.Verbose,true);
		} catch(Exception e) {
			Log.e(TAG, e.toString());
			msgFromServer = "ERROR";
			Logger.WriteLog( "CheckPoint","Send data to server unsuccessfully, message = "+e.getMessage(),Logger.Log_Level.Information,true);
		}
		return msgFromServer;
	}

	public String SendDataToServer(String msg, int size) {
		String data = CheckSize(msg, size);
		return SendDataToServer(data);
	}

	public boolean disconnect() {
		try {
			if (mSocket != null) 
				mSocket.close();
			if(mInput != null) 
				mInput.close();
			if(mPrintWriter != null) 
				mPrintWriter.close();
			Logger.WriteLog( "Action","Disconnect to server successfully.",Logger.Log_Level.Information);
			
		} catch (Exception e) {
			Log.e(TAG, e.toString());
			Logger.WriteLog( "Error","Disconnect to server unsuccessfully, message = "+e.getMessage(),Logger.Log_Level.Information,true);
			return false;
		}
		return true;
	}
}
