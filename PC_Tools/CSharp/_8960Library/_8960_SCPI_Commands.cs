using System;
using System.Collections.Generic;
using System.Text;
using NationalInstruments.NI4882;

namespace com.usi.shd1_tools._8960Library
{
    public class _8960_SCPI_Commands
    {
        public static readonly String SystemApplicationName_GSM_GPRS_WCDMA = "Fast Switch Lab App E";
        public static readonly String SystemApplicationName_TDSCDMA = "TD-SCDMA_GSM Fast Switch";

        public static String[] Connect = new String[] { "DISPlay:OPERator:MESSage1 \'Connected\'" };

        public static String[] SetAuto = new String[] { "CALL:FUNCtion:CONNection:TYPE AUTO" };

        public static String[] SetBLER = new String[] { "CALL:FUNCtion:CONNection:TYPE BLER" };

        public static String GetCallState = "CALL:STATus:STATe?";

        public static String GetDataState = "CALL:STATUS:DATA?";

        public static String[] Dial = new String[] { "CALL:ORIGinate" };

        public static String[] EndCall = new String[] { "CALL:END" };

        public static String[] Set_EGPRS_850 = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                   "CALL:OPER:MODE OFF",
                                                                                                   "CALL:BCH:SCEL EGPRS",
                                                                                                   "CALL:OPER:MODE CALL",
                                                                                                   "CALL:CELL:BAND GSM850"};

        public static String[] Set_EGPRS_DCS = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                     "CALL:OPER:MODE OFF",
                                                                                                     "CALL:BCH:SCEL EGPRS",
                                                                                                     "CALL:OPER:MODE CALL",
                                                                                                     "CALL:CELL:BAND DCS"};

        public static String[] Set_EGPRS_EGSM = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                         "CALL:OPER:MODE OFF",
                                                                                                        "CALL:BCH:SCEL EGPRS",
                                                                                                        "CALL:OPER:MODE CALL",
                                                                                                        "CALL:CELL:BAND EGSM"};

        public static String[] Set_EGPRS_PCS = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                         "CALL:OPER:MODE OFF",
                                                                                                        "CALL:BCH:SCEL EGPRS",
                                                                                                        "CALL:OPER:MODE CALL",
                                                                                                        "CALL:CELL:BAND PCS"};

        public static String[] Set_GPRS_850 = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GPRS",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND GSM850",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GPRS_DCS = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GPRS",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND DCS",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GPRS_EGSM = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GPRS",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND EGSM",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GPRS_PCS = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GPRS",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND PCS",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GSM_850 = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GSM",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND GSM850",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GSM_900 = new String[]{"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GSM",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND PGSM",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GSM_DCS = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GSM",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND DCS",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GSM_EGSM = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GSM",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND EGSM",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] Set_GSM_PCS = new String[] {"SYSTem:APPLication:FORMat 'GSM/GPRS'",
                                                                                                  "CALL:OPER:MODE OFF",
                                                                                                  "CALL:BCH:SCEL GSM",
                                                                                                  "CALL:OPER:MODE CALL",
                                                                                                  "CALL:CELL:BAND PCS",
                                                                                                  "CALL:FUNCtion:CONNection:TYPE AUTO"};

        public static String[] SetHSDPA = new String[]{"SYSTem:APPLication:FORMat 'WCDMA'",
                                                                                         "CALL:SERVice:RBTest:RABHSDParmc12",
                                                                                         "CALL:HSDPa:SERVice:RBTest:CNDomain PSDomain"};

        public static String[] SetWCDMA = new String[]{"SYSTem:APPLication:FORMat 'WCDMA'",
                                                                                             "CALL:SERVice:RBTest:RAB RMC12",
                                                                                            "CALL:HSDPa:SERVice:RBTest:CNDomain PSDomain"};

        public static String[] SetUMTS900 = new String[] { "SYSTem:APPLication:FORMat 'WCDMA'", 
                                                                                                "CALL:OPER:MODE OFF",
                                                                                                "CALL:CHAN 3012",  // WCDMA Band 8 = 900MHZ , DL Channel = 2937 ~ 3088
                                                                                                 "CALL:OPER:MODE CALL" };

        public static String[] SetUMTS2100 = new String[] {"SYSTem:APPLication:FORMat 'WCDMA'", 
                                                                                                "CALL:OPER:MODE OFF",
                                                                                                "CALL:CHAN 10700",  // WCDMA Band 1 = 2100 MHZ , DL Channel = 10562 ~ 10838
                                                                                                 "CALL:OPER:MODE CALL" };



        public static String[] SwitchSystemApplication(String AppName)
        {
            String[] rtnValue = new String[] { "SYSTem:APPLication:SELect:NAME \'" + AppName + "\'" };
            return rtnValue;
        }

        /// <summary>
        /// TD-SCDMA B34 = 2010 MHz - 2025 MHz (Channel # 10054 ~10075)
        /// </summary>
        public static String[] SetTD_SCDMA34 = new String[] {  "SYSTem:APPLication:FORMat 'TD-SCDMA'",
                                                                                                        "CALL:OPER:MODE OFF",
                                                                                                        "CALL:CHAN 10060",
                                                                                                        "CALL:OPER:MODE CALL" };

        /// <summary>
        ///  TD-SCDMA B34 = 1880 MHz - 1920 MHz (Channel # 9404 ~ 9596
        /// </summary>
        public static String[] SetTD_SCDMA39 = new String[] {  "SYSTem:APPLication:FORMat 'TD-SCDMA'",
                                                                                                        "CALL:OPER:MODE OFF",
                                                                                                        "CALL:CHAN 9504",
                                                                                                        "CALL:OPER:MODE CALL"};

        public static String[] SetCellPower(double cellPower)
        {
            return new String[] { "CALL:CELL:POWer " + cellPower.ToString("0.00") };
        }

        public static String[] SetAmplitudeOffsets(double[] frequences, double[] offsets)
        {
            String[] rtnValue = new String[] { "", "" };
            rtnValue[0] += "SYSTem:CORRection:SFRequency ";
            foreach (double freq in frequences)
            {
                rtnValue[0] += freq.ToString("0.00") + " MHZ,";
            }
            rtnValue[0] = rtnValue[0].TrimEnd(',');
            rtnValue[1] += "SYSTem:CORRection:SGAin ";
            foreach (double offset in offsets)
            {
                rtnValue[1] += offset.ToString("0.00") + ",";
            }
            rtnValue[1] = rtnValue[1].TrimEnd(',');
            return rtnValue;
        }

        public static String[] SetChannel(int channel)
        {
            return new String[] {
                                                 "CALL:OPER:MODE OFF",
                                                 "CALL:CHAN "+channel,
                                                 "CALL:OPER:MODE CALL"};
        }


        public static String[] SetTrafficChannel(int channel)
        {
            return new String[] {
                                                 //"CALL:OPER:MODE OFF",
                                                 "CALL:TCH "+channel};
            //"CALL:OPER:MODE CALL"};
        }

        public static String SetTrafficBand(String band)
        {
            return "CALL:TCH:BAND " + band;
        }

        public static String[] SetCellOff = {"CALL:OPER:MODE OFF"
                                            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">1000 to 999000</param>
        /// <param name="isContinuous"></param>
        /// <param name="timeout_InSeconds">1 to 999</param>
        /// <returns></returns>
        public static String[] Init_BER(int count, bool isContinuous, int timeout_InSeconds)
        {
            List<String> cmds = new List<string>();
            count = count < 1000 ? 1000 : count;
            count = count > 999000 ? 999000 : count;
			cmds.Add("SETup:WBERror:COUNt "+count.ToString());
            if (timeout_InSeconds > 0)
            {
                timeout_InSeconds = timeout_InSeconds < 1 ? 1 : timeout_InSeconds;
                timeout_InSeconds = timeout_InSeconds > 999 ? 999 : timeout_InSeconds;
                cmds.Add("SETup:WBERror:TIMeout:STATe ON");
                cmds.Add("SETup:WBERror:TIMeout "+timeout_InSeconds +"S");
            }
            else
            {
                cmds.Add("SETup:WBERror:TIMeout:STATe OFF");
            }
            cmds.Add("SETup:WBERror:CONTinuous "+(isContinuous?"ON":"OFF"));
            cmds.Add("INITiate:WBERror");
            return cmds.ToArray();
        }

        public static String GetBERR = "FETCh:WBERror:RATio?";
    }
}
