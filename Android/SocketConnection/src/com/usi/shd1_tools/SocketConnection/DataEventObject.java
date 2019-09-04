package com.usi.shd1_tools.SocketConnection;

import java.util.HashMap;
import java.util.Map;

public class DataEventObject {
	public int SequenceNo = -1;
	public String Header = "";
	public Object Data = "";

	public DataEventObject(int sequenceNo, String header, Object data) {
		SequenceNo = sequenceNo;
		Header = header;
		Data = data;
	}

	@Override
	public String toString() {
		String str = "";
		if (SequenceNo > 0) {
			str += "(" + (String.valueOf(SequenceNo)) + ")";
		}
		if (Header.length() == 0)
			str = String.valueOf(Data);
		else {
			str = "[" + Header + "] " + String.valueOf(Data);
		}
		return str;
	}

	public Map<String, Object> TransferDataToPair() {
		Map<String, Object> map = new HashMap<String, Object>();
		if (Data != null) {
			String strData = String.valueOf(Data);
			int argIndex = 1;
			String[] strParams;
			if (strData.contains(Connection.KW_ParametersSpilter)) {
				strParams = strData.split(Connection.KW_ParametersSpilter);
			} else {
				strParams = new String[] { strData };
			}
			for (String strParam : strParams) {
				if (strParam.contains(Connection.KW_KeyValueSpilter)) {
					String[] strPair = strParam.split(
							Connection.KW_KeyValueSpilter, 2);
					map.put(strPair[0], strPair[1]);
				} else {
					map.put("arg_" + String.valueOf(argIndex), strParam);
				}
			}
		}
		return map;
	}
}
