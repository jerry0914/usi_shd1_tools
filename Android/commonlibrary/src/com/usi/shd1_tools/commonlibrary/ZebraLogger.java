package com.usi.shd1_tools.commonlibrary;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;

import android.text.format.Time;

public class ZebraLogger {
	private static ArrayList <String> logQueue =  new ArrayList <String>();
	private static Thread tdWriteLog;
	private static Time startTime;
	private static String logPath = "";
	private static boolean runFlag = false;
	private static Date dtPreviousWriteLogTime = new Date();
	private static boolean flag_WriteLogImmediately = false;
	private static final int checkLogQueueInterval = 5000;
	private static String _deviceID;
	public static void setLogPath(String path) {
		int extensionIndex = path.lastIndexOf('.');
		if(extensionIndex>0)
		{
			logPath = path.substring(0,extensionIndex);
		}
		else {
			logPath = path;
		}
		createNewFile(getRealLogPath());
	}
	
	public static String getLogPath() {
		return getRealLogPath();
	}

	private static String timeStamp = "";

	public static void Start(String LogPath,String deviceID) {
		//log_index = 0;
		_deviceID = deviceID;
		startTime = getCurrentTime();
		timeStamp = GetDateTimeString();
		setLogPath(LogPath);
		startLogger();
	}
	
	public static void Stop()
	{
		runFlag = false;
		try {
			Thread.sleep(checkLogQueueInterval);
			//tdWriteLog.interrupt();
			tdWriteLog = null;	
		} catch (InterruptedException e) {
			e.printStackTrace();
		}		
	}
		
	private static void startLogger()
	{
		runFlag = true;
		tdWriteLog = new Thread(new Runnable() 
		{				
			@Override
			public void run() 
			{
				FileWriter fw = null;
				try {
					fw = new FileWriter(getRealLogPath(),true);
				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
				while(runFlag && !tdWriteLog.isInterrupted())
				{
					Date dtNow = new Date();
					if(logQueue.size()>0 && 
					  ((dtNow.getTime() - dtPreviousWriteLogTime.getTime()>=checkLogQueueInterval) || flag_WriteLogImmediately))
					{
						while(logQueue.size()>0)
						{
							try {
								fw.append(logQueue.remove(0)+"\r\n");
							} catch (IOException e) {
								e.printStackTrace();
							}							
						}
						flag_WriteLogImmediately = false;
						dtPreviousWriteLogTime = new Date();
						try {
							Thread.sleep(500);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
				}
				try {
					fw.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
				runFlag = false;
			}
		});
		tdWriteLog.start();	
	}

	private static String getRealLogPath() {
		String path = "";
		path = logPath + _deviceID+"_"+timeStamp + ".scanstress.profiler.log.txt";
		return path;
	}

	private static boolean createNewFile(String filePath) {
		boolean result = false;
		File f = new File(filePath);
		File parent = f.getParentFile();
		if(!parent.exists() && !parent.mkdirs())
		{
			result = false;
		}
		else 
		{
			if(!f.exists())
			{
				try
				{
					result = f.createNewFile();
				}
				catch(IOException ioex)
				{
					result = false;
				}
			}
			else 
			{
				result = true;
			}
		}
		return result;
	}

	private static Time getCurrentTime()
	{
		Time time = new Time();
		time.setToNow();
		return time;
	}
	
	@SuppressWarnings("deprecation")
	public static String GetDateTimeString() 
	{
		String dateTime = "";
		Time currentTime = getCurrentTime();

		dateTime += String.format("%02d", currentTime.year);
		dateTime += String.format("%02d", currentTime.month+1);
		dateTime += String.format("%02d", currentTime.monthDay);
		dateTime += String.format("%02d", currentTime.hour);
		dateTime += String.format("%02d", currentTime.minute);
		dateTime += String.format("%02d", currentTime.second);
		return dateTime;
	}
	
	public static String getZebraTimeFormatString()
	{	
		Time currentTime = getCurrentTime();
		long diff = currentTime.toMillis(true) - startTime.toMillis(true);
		String strTime = "";
		strTime += String.format("%02d", (int)currentTime.hour)+":";
		strTime += String.format("%02d", (int)currentTime.minute)+":";
		strTime += String.format("%02d", (int)currentTime.second)+" | ";
		strTime+=String.format("%04d",(int)diff/(60*60*1000))+":";
		strTime += String.format("%02d", (int)(diff/(60*1000))%60)+ ":";		
		strTime += String.format("%02d", (int)(diff/1000)%60)+" | ";
		strTime += "0x0"+" | ";
		return strTime;
	}
	
	public static void WriteLog(String msg , boolean writeImmediately)
	{
		String myMsg = getZebraTimeFormatString()+msg;		
		logQueue.add(myMsg);
		flag_WriteLogImmediately = writeImmediately;
	}
	
	public static void WriteLog(String msg)
	{
		WriteLog(msg,false);
	}
}
