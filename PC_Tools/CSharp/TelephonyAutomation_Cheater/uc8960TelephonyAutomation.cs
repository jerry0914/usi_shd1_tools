using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools.TestcasePackage;
using com.usi.shd1_tools._8960Library;
using System.Threading;
using System.Reflection;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;


namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class uc8960TelephonyAutomation : UserControl
    {
        private frmMain mainForm;
        private String defaultTestCasesExcelPath = System.AppDomain.CurrentDomain.BaseDirectory + "WAN_AutomationTestCases.xlsx";
        private List<Wwan_TestCaseInfo> lstTC_GsmGprsWcdma = new List<Wwan_TestCaseInfo>();
        private List<Wwan_TestCaseInfo> lstTC_TdScdma = new List<Wwan_TestCaseInfo>();
        private List<Wwan_TestCaseInfo> lstTC_Handover = new List<Wwan_TestCaseInfo>();
        private List<Wwan_TestCaseInfo> lstTC_DebugTestcase = new List<Wwan_TestCaseInfo>();
        private StationEmulator_8960 se8960;
        private AdbDeviceInfomation currentDevice = null;
        private Thread tdDutStatus = null;
        private const int dutMonitorInterval = 4000;
        private bool dutStatusMonitor_Flag = false;
        private List<AdbDeviceInfomation> devList;
        private bool dutReady = false, atstReady = false;
        private String apiFunctionApkPath = System.AppDomain.CurrentDomain.BaseDirectory + "apk\\APIFunction.apk";
        private clsTelephony.CallStates dut1State = clsTelephony.CallStates.IDLE;
        private clsTelephony.CallStates previousDutPhoneState = clsTelephony.CallStates.IDLE;
        private ProcedureProcessor_8960 procedureProcessor = null;
        private ucDbg0001 uiDbg001 = null;
        private ucDbg0002 uiDbg002 = null;
        private ucDbg0003 uiDbg003 = null;
        public uc8960TelephonyAutomation(frmMain main)
        {
            InitializeComponent();
            #region Test Case Description
            List<Wwan_TestCaseInfo> totalTCs = TestCaseReader.GetTestCasesFromExcel(defaultTestCasesExcelPath, 0);
            if (totalTCs.Count > 0)
            {
                foreach (Wwan_TestCaseInfo tc in totalTCs)
                {
                    if(tc.CurrentFormat.Equals(Wwan_TestCaseInfo.Format.TD_SCDMA))
                    {
                        lstTC_TdScdma.Add(tc);
                    }
                    else
                    {
                        lstTC_GsmGprsWcdma.Add(tc);
                    }
                }
            }
            else
            {
                #region GSM/GPRS/WCDMA
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0001",
                                                                        "DUT calls station emulator and DUT hangs up (GSM 900)",
                                                                         "(1) Switch the wan band of station emulator to GSM 900" + "\r\n" +
                                                                         "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                         "(3) Make sure the DUT is attached to the station emulator.",
                                                                         "1. DUT Calls station emulator." + "\r\n" +
                                                                         "2. Emulator pick up." + "\r\n" +
                                                                         "3. Check the DUT & emulator status." + "\r\n" +
                                                                         "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                         "5. DUT hangs up." + "\r\n" +
                                                                         "6. Check the DUT & emulator status.",
                                                                         "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                         "2. Emulator call status is connected" + "\r\n" +
                                                                         "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                         "5. DUT call status is idle" + "\r\n" +
                                                                         "6. Both DUT and emulator status are idle.",
                                                                         200,
                                                                         200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0002",
                                                                       "DUT calls station emulator  and emulator hangs up (GSM 900)",
                                                                       "(1) Switch the wan band of station emulator to GSM 900" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm). " + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                        "1. DUT Calls station emulator." + "\r\n" +
                                                                        "2. Emulator pick up." + "\r\n" +
                                                                        "3. Check the DUT & emulator status." + "\r\n" +
                                                                        "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                        "5. Emulator hangs up." + "\r\n" +
                                                                        "6. Check the DUT & emulator status.",
                                                                        "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                        "2. Emulator call status is connected" + "\r\n" +
                                                                        "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                        "5. Emulator's call status is idle" + "\r\n" +
                                                                        "6. Both DUT and emulator status are idle.",
                                                                         200,
                                                                         200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0003",
                                                                         "Station emulator calls DUT and DUT hangs up (GSM 900)",
                                                                         "(1) Switch the wan band of station emulator to GSM 900" + "\r\n" +
                                                                         "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                         "(3) Make sure the DUT is attached to the station emulator.",
                                                                         "1. Station emulator calls DUT." + "\r\n" +
                                                                         "2. DUT pick up." + "\r\n" +
                                                                         "3. Check the DUT & emulator status." + "\r\n" +
                                                                         "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                         "5. DUT hangs up." + "\r\n" +
                                                                         "6. Check the DUT & emulator status.",
                                                                         "1. DUT status is ringing, emulator status is alerting." + "\r\n" +
                                                                         "2. DUT call status is connected" + "\r\n" +
                                                                         "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                         "5. DUT call status is idle" + "\r\n" +
                                                                         "6. Both DUT and emulator status are idle.",
                                                                         200,
                                                                         200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0004",
                                                                        "Station emulator calls DUT and emulator hangs up (GSM 900)",
                                                                        "(1) Switch the wan band of station emulator to GSM 900" + "\r\n" +
                                                                        "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                        "(3) Make sure the DUT is attached to the station emulator.",
                                                                        "1. Station emulator calls DUT." + "\r\n" +
                                                                        "2. DUT pick up." + "\r\n" +
                                                                        "3. Check the DUT & emulator status. " + "\r\n" +
                                                                        "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                        "5. Emulator hangs up." + "\r\n" +
                                                                        "6. Check the DUT & emulator status.",
                                                                        "1. DUT status is ringing, emulator status is alerting." + "\r\n" +
                                                                         "2. DUT call status is connected" + "\r\n" +
                                                                         "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                         "5. Emulator's call status is idle" + "\r\n" +
                                                                         "6. Both DUT and emulator status are idle",
                                                                         200,
                                                                         200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0005",
                                                                       "DUT calls station emulator and DUT hangs up (DCS 1800)",
                                                                       "(1) Switch the wan band of station emulator to DCS 1800" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm). " + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. DUT Calls station emulator." + "\r\n" +
                                                                       "2. Emulator pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status." + "\r\n" +
                                                                       "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                       "5. DUT hangs up." + "\r\n" +
                                                                       "6. Check the DUT & emulator status.",
                                                                       "1. DUT status is off-hook, emulator status is ringing. " + "\r\n" +
                                                                       "2. Emulator call status is connected" + "\r\n" +
                                                                       "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                       "5. DUT call status is idle" + "\r\n" +
                                                                       "6. Both DUT and emulator status are idle.",
                                                                       200,
                                                                       200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0006",
                                                                       "DUT calls station emulator  and emulator hangs up (DCS 1800)",
                                                                       "(1) Switch the wan band of station emulator to DCS 1800" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. Station emulator calls DUT." + "\r\n" +
                                                                       "2. DUT pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status. " + "\r\n" +
                                                                       "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                       "5. Emulator hangs up." + "\r\n" +
                                                                       "6. Check the DUT & emulator status.",
                                                                       "1. DUT status is ringing, emulator status is alerting." + "\r\n" +
                                                                       "2. DUT call status is connected" + "\r\n" +
                                                                       "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                       "5. Emulator's call status is idle" + "\r\n" +
                                                                       "6. Both DUT and emulator status are idle",
                                                                        200,
                                                                        200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0007",
                                                                       "Station emulator calls DUT and DUT hangs up (DCS 1800)",
                                                                       "(1) Switch the wan band of station emulator to DCS 1800" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. Station emulator calls DUT." + "\r\n" +
                                                                       "2. DUT pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status." + "\r\n" +
                                                                       "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                       "5. DUT hangs up." + "\r\n" +
                                                                       "6. Check the DUT & emulator status.",
                                                                       "1. DUT status is ringing, emulator status is alerting. " + "\r\n" +
                                                                       "2. DUT call status is connected" + "\r\n" +
                                                                       "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                       "5. DUT call status is idle" + "\r\n" +
                                                                       "6. Both DUT and emulator status are idle.",
                                                                        200,
                                                                        200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0008",
                                                                       "Station emulator calls DUT and emulator hangs up (DCS 1800)",
                                                                       "(1) Switch the wan band of station emulator to DCS 1800" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. Station emulator calls DUT." + "\r\n" +
                                                                       "2. DUT pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status. " + "\r\n" +
                                                                       "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                       "5. Emulator hangs up." + "\r\n" +
                                                                       "6. Check the DUT & emulator status.",
                                                                       "1. DUT status is ringing, emulator status is alerting. " + "\r\n" +
                                                                       "2. DUT call status is connected" + "\r\n" +
                                                                       "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                       "5. Emulator's call status is idle" + "\r\n" +
                                                                       "6. Both DUT and emulator status are idle.",
                                                                       200,
                                                                       200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0009",
                                                                       "DUT calls station emulator and DUT hangs up (UMTS 900)",
                                                                       "(1) Switch the wan band of station emulator to UMTS 900" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. DUT Calls station emulator." + "\r\n" +
                                                                       "2. Emulator pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status." + "\r\n" +
                                                                       "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                       "5. DUT hangs up." + "\r\n" +
                                                                       "6. Check the DUT & emulator status.",
                                                                       "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                       "2. Emulator call status is connected" + "\r\n" +
                                                                       "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                       "5. DUT call status is idle" + "\r\n" +
                                                                       "6. Both DUT and emulator status are idle.",
                                                                       200,
                                                                       200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0010",
                                                                       "DUT calls station emulator  and emulator hangs up UMTS 900)",
                                                                       "(1) Switch the wan band of station emulator to UMTS 900" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. DUT Calls station emulator." + "\r\n" +
                                                                       "2. Emulator pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status." + "\r\n" +
                                                                       "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                       "5. Emulator hangs up." + "\r\n" +
                                                                       "6. Check the DUT & emulator status.",
                                                                       "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                       "2. Emulator call status is connected" + "\r\n" +
                                                                       "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                       "5. Emulator's call status is idle" + "\r\n" +
                                                                       "6. Both DUT and emulator status are idle.",
                                                                       200,
                                                                       200));

                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0011",
                                                                      "DUT calls station emulator and DUT hangs up (UMTS 2100)",
                                                                      "(1) Switch the wan band of station emulator to UMTS 2100" + "\r\n" +
                                                                      "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                      "(3) Make sure the DUT is attached to the station emulator.",
                                                                      "1. DUT Calls station emulator." + "\r\n" +
                                                                      "2. Emulator pick up." + "\r\n" +
                                                                      "3. Check the DUT & emulator status." + "\r\n" +
                                                                      "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                      "5. DUT hangs up." + "\r\n" +
                                                                      "6. Check the DUT & emulator status.",
                                                                      "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                      "2. Emulator call status is connected" + "\r\n" +
                                                                      "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                      "5. DUT call status is idle" + "\r\n" +
                                                                      "6. Both DUT and emulator status are idle.",
                                                                      200,
                                                                      200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0012",
                                                                       "DUT calls station emulator  and emulator hangs up UMTS 2100)",
                                                                       "(1) Switch the wan band of station emulator to UMTS 2100" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. DUT Calls station emulator." + "\r\n" +
                                                                       "2. Emulator pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status." + "\r\n" +
                                                                       "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                       "5. Emulator hangs up." + "\r\n" +
                                                                       "6. Check the DUT & emulator status.",
                                                                       "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                       "2. Emulator call status is connected" + "\r\n" +
                                                                       "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                       "5. Emulator's call status is idle" + "\r\n" +
                                                                       "6. Both DUT and emulator status are idle.",
                                                                       200,
                                                                       200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0017",
                                                                       "Establish a phone connection for a long time.(GSM 900)",
                                                                       "(1) Switch the wan band of station emulator to GSM 900" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. DUT Calls station emulator." + "\r\n" +
                                                                       "2. Emulator pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status every 30 seconds, continuous this step for 60 minutes" + "\r\n" +
                                                                       "4. DUT hangs up." + "\r\n" +
                                                                       "5. Check the DUT & emulator status.",
                                                                       "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                        "2. Emulator call status is connected" + "\r\n" +
                                                                        "3. Both DUT and Emulator status are connected every time." + "\r\n" +
                                                                        "4. DUT call status is idle" + "\r\n" +
                                                                        "5. Both DUT and emulator status are idle.",
                                                                        1,
                                                                        1));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0018",
                                                                       "Establish a phone connection for a long time.(DCS 1800)",
                                                                       "(1) Switch the wan band of station emulator to DCS 1800" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. DUT Calls station emulator." + "\r\n" +
                                                                       "2. Emulator pick up." + "\r\n" +
                                                                       "3. Check the DUT & emulator status every 30 seconds, continuous this step for 60 minutes" + "\r\n" +
                                                                       "4. DUT hangs up." + "\r\n" +
                                                                       "5. Check the DUT & emulator status.",
                                                                       "1. DUT status is off-hook, emulator status is ringing." + "\r\n" +
                                                                        "2. Emulator call status is connected" + "\r\n" +
                                                                        "3. Both DUT and Emulator status are connected every time." + "\r\n" +
                                                                        "4. DUT call status is idle" + "\r\n" +
                                                                        "5. Both DUT and emulator status are idle.",
                                                                        1,
                                                                        1));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0101",
                                                                       "Station emulator establish a data connetion to DUT. (SIM1,UMTS 900)",
                                                                       "(1) Switch the wan band of station emulator to UMTS 900" + "\r\n" +
                                                                       "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                       "(3) Make sure the DUT is attached to the station emulator.",
                                                                       "1. Station emulator starts a original call." + "\r\n" +
                                                                       "2. Check the emulator status." + "\r\n" +
                                                                       "3. Check the emulator status again after 20 seconds later. " + "\r\n" +
                                                                       "4. Emulator ends call." + "\r\n" +
                                                                       "5. Check the emulator status." + "\r\n",
                                                                       "2.3. Emulator status is connected." + "\r\n" +
                                                                       "5. Emulator status is idle.",
                                                                       200,
                                                                       200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0102",
                                                                     "Station emulator establish a data connetion to DUT. (SIM1,UMTS 2100)",
                                                                     "(1) Switch the wan band of station emulator to UMTS 2100" + "\r\n" +
                                                                     "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                     "(3) Make sure the DUT is attached to the station emulator.",
                                                                     "1. Station emulator starts a original call." + "\r\n" +
                                                                     "2. Check the emulator status." + "\r\n" +
                                                                     "3. Check the emulator status again after 20 seconds later. " + "\r\n" +
                                                                     "4. Emulator ends call." + "\r\n" +
                                                                     "5. Check the emulator status." + "\r\n",
                                                                     "2.3. Emulator status is connected." + "\r\n" +
                                                                     "5. Emulator status is idle.",
                                                                     200,
                                                                     200));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0105",
                                                                   "Establish a data connection for a long time.(SIM1,UMTS 900)",
                                                                   "(1) Switch the wan band of station emulator to UMTS 900" + "\r\n" +
                                                                   "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                   "(3) Make sure the DUT is attached to the station emulator.",
                                                                   "1. Station emulator starts a original call." + "\r\n" +
                                                                   "2. Check the emulator status." + "\r\n" +
                                                                   "3. Check the emulator status every 30 seconds, continuous this step for 60 minutes" + "\r\n" +
                                                                   "4. Emulator ends call." + "\r\n" +
                                                                   "5. Check the emulator status." + "\r\n",
                                                                   "2. Emulator call status is connected" + "\r\n" +
                                                                   "3. Emulator status should be connected every time." + "\r\n" +
                                                                   "5. Emulator status is idle",
                                                                   1,
                                                                   1));
                lstTC_GsmGprsWcdma.Add(new Wwan_TestCaseInfo("WA0106",
                                                                   "Establish a data connection for a long time.(SIM1,UMTS 2100)",
                                                                   "(1) Switch the wan band of station emulator to UMTS 2100" + "\r\n" +
                                                                   "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                   "(3) Make sure the DUT is attached to the station emulator.",
                                                                   "1. Station emulator starts a original call." + "\r\n" +
                                                                   "2. Check the emulator status." + "\r\n" +
                                                                   "3. Check the emulator status every 30 seconds, continuous this step for 60 minutes" + "\r\n" +
                                                                   "4. Emulator ends call." + "\r\n" +
                                                                   "5. Check the emulator status." + "\r\n",
                                                                   "2. Emulator call status is connected" + "\r\n" +
                                                                   "3. Emulator status should be connected every time." + "\r\n" +
                                                                   "5. Emulator status is idle",
                                                                   1,
                                                                   1));
                #endregion GSM/GPRS/WCDMA
                #region TD-SCDMA
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0013",
                                                                      "DUT calls station emulator and DUT hangs up (TD-SCDMA B34)",
                                                                      "(1) Switch the wan band of station emulator to  TD-SCDMA B34" + "\r\n" +
                                                                      "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                      "(3) Make sure the DUT is attached to the station emulator.",
                                                                      "1. DUT Calls station emulator." + "\r\n" +
                                                                      "2. Emulator pick up." + "\r\n" +
                                                                      "3. Check the DUT & emulator status." + "\r\n" +
                                                                      "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                      "5. DUT hangs up." + "\r\n" +
                                                                      "6. Check the DUT & emulator status.",
                                                                      "1. DUT status is ringing, emulator status is alerting." + "\r\n" +
                                                                      "2. DUT call status is connected" + "\r\n" +
                                                                      "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                      "5. DUT call status is idle" + "\r\n" +
                                                                      "6. Both DUT and emulator status are idle.",
                                                                      200,
                                                                      200));
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0014",
                                                                    "DUT calls station emulator and emulator hangs up (TD-SCDMA B34)",
                                                                    "(1) Switch the wan band of station emulator to  TD-SCDMA B34" + "\r\n" +
                                                                    "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                    "(3) Make sure the DUT is attached to the station emulator.",
                                                                    "1. DUT Calls station emulator." + "\r\n" +
                                                                    "2. DUT pick up." + "\r\n" +
                                                                    "3. Check the DUT & emulator status." + "\r\n" +
                                                                    "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                    "5. Emulator hangs up." + "\r\n" +
                                                                    "6. Check the DUT & emulator status.",
                                                                    "1. DUT status is ringing, emulator status is alerting." + "\r\n" +
                                                                    "2. Emulator's call status is connected" + "\r\n" +
                                                                    "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                    "5. Emulator's call status is idle" + "\r\n" +
                                                                    "6. Both DUT and emulator status are idle",
                                                                    200,
                                                                    200));
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0015",
                                                                      "DUT calls station emulator and DUT hangs up (TD-SCDMA B39)",
                                                                      "(1) Switch the wan band of station emulator to  TD-SCDMA B39" + "\r\n" +
                                                                      "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                      "(3) Make sure the DUT is attached to the station emulator.",
                                                                      "1. DUT Calls station emulator." + "\r\n" +
                                                                      "2. Emulator pick up." + "\r\n" +
                                                                      "3. Check the DUT & emulator status." + "\r\n" +
                                                                      "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                      "5. DUT hangs up." + "\r\n" +
                                                                      "6. Check the DUT & emulator status.",
                                                                      "1. DUT status is ringing, emulator status is alerting." + "\r\n" +
                                                                      "2. DUT call status is connected" + "\r\n" +
                                                                      "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                      "5. DUT call status is idle" + "\r\n" +
                                                                      "6. Both DUT and emulator status are idle.",
                                                                      200,
                                                                      200));
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0016",
                                                                     "DUT calls station emulator and emulator hangs up (TD-SCDMA B39)",
                                                                     "(1) Switch the wan band of station emulator to  TD-SCDMA B39" + "\r\n" +
                                                                     "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                     "(3) Make sure the DUT is attached to the station emulator.",
                                                                     "1. DUT Calls station emulator." + "\r\n" +
                                                                     "2. DUT pick up." + "\r\n" +
                                                                     "3. Check the DUT & emulator status." + "\r\n" +
                                                                     "4. Check the DUT & emulator status again after 20 seconds later." + "\r\n" +
                                                                     "5. Emulator hangs up." + "\r\n" +
                                                                     "6. Check the DUT & emulator status.",
                                                                     "1. DUT status is ringing, emulator status is alerting." + "\r\n" +
                                                                     "2. Emulator's call status is connected" + "\r\n" +
                                                                     "3.4. Both DUT and Emulator status are connected." + "\r\n" +
                                                                     "5. Emulator's call status is idle" + "\r\n" +
                                                                     "6. Both DUT and emulator status are idle",
                                                                     200,
                                                                     200));
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0103",
                                                                      "Station emulator establish a data connetion to DUT. (SIM1,TD-SCDMA B34)",
                                                                      "(1) Switch the wan band of station emulator to TD-SCDMA B34" + "\r\n" +
                                                                      "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                      "(3) Make sure the DUT is attached to the station emulator.",
                                                                      "1. Station emulator starts a original call." + "\r\n" +
                                                                      "2. Check the emulator status." + "\r\n" +
                                                                      "3. Check the emulator status again after 20 seconds later. " + "\r\n" +
                                                                      "4. Emulator ends call." + "\r\n" +
                                                                      "5. Check the emulator status." + "\r\n",
                                                                      "2.3. Emulator status is connected." + "\r\n" +
                                                                      "5. Emulator status is idle.",
                                                                      200,
                                                                      200));
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0104",
                                                                     "Station emulator establish a data connetion to DUT. (SIM1,TD-SCDMA B39)",
                                                                     "(1) Switch the wan band of station emulator to TD-SCDMA B39" + "\r\n" +
                                                                     "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                     "(3) Make sure the DUT is attached to the station emulator.",
                                                                     "1. Station emulator starts a original call." + "\r\n" +
                                                                     "2. Check the emulator status." + "\r\n" +
                                                                     "3. Check the emulator status again after 20 seconds later. " + "\r\n" +
                                                                     "4. Emulator ends call." + "\r\n" +
                                                                     "5. Check the emulator status." + "\r\n",
                                                                     "2.3. Emulator status is connected." + "\r\n" +
                                                                     "5. Emulator status is idle.",
                                                                     200,
                                                                     200));
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0107",
                                                                   "Establish a data connection for a long time.(SIM1,TD-SCDMA B34)",
                                                                   "(1) Switch the wan band of station emulator to TD-SCDMA B34" + "\r\n" +
                                                                   "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                   "(3) Make sure the DUT is attached to the station emulator.",
                                                                   "1. Station emulator starts a original call." + "\r\n" +
                                                                   "2. Check the emulator status." + "\r\n" +
                                                                   "3. Check the emulator status every 30 seconds, continuous this step for 60 minutes" + "\r\n" +
                                                                   "4. Emulator ends call." + "\r\n" +
                                                                   "5. Check the emulator status." + "\r\n",
                                                                   "2. Emulator call status is connected" + "\r\n" +
                                                                   "3. Emulator status should be connected every time." + "\r\n" +
                                                                   "5. Emulator status is idle",
                                                                   1,
                                                                   1));
                lstTC_TdScdma.Add(new Wwan_TestCaseInfo("WA0108",
                                                                   "Establish a data connection for a long time.(TD-SCDMA B39)",
                                                                   "(1) Switch the wan band of station emulator to TD-SCDMA B39" + "\r\n" +
                                                                   "(2) Set the sufficient power level to (RSSI -50dbm)." + "\r\n" +
                                                                   "(3) Make sure the DUT is attached to the station emulator.",
                                                                   "1. Station emulator starts a original call." + "\r\n" +
                                                                   "2. Check the emulator status." + "\r\n" +
                                                                   "3. Check the emulator status every 30 seconds, continuous this step for 60 minutes" + "\r\n" +
                                                                   "4. Emulator ends call." + "\r\n" +
                                                                   "5. Check the emulator status." + "\r\n",
                                                                   "2. Emulator call status is connected" + "\r\n" +
                                                                   "3. Emulator status should be connected every time." + "\r\n" +
                                                                   "5. Emulator status is idle",
                                                                   1,
                                                                   1));

                #endregion TD-SCDMA
            }
            #region Handover Testcases
            Wwan_TestCaseInfo wa0201 = new Wwan_TestCaseInfo("WA0201",
                                                            "Adjust frequency and signal strength while phone calling (SIM1 - GSM 900)",
                                                            "(1) Switch the wan band of station emulator to GSM 900" + "\r\n" +
                                                            "(2) Make sure the DUT is attached to the station emulator.",
                                                            "1. DUT Calls station emulator,emulator pick up." + "\r\n" +
                                                            "2. Set traffic channel (Initial = minimun one of current band)" + "\r\n" +
                                                            "3. Decreace DUT signal strength from -82 db to -102 db for 10db/20seconds" + "\r\n" +
                                                            "4. Check the DUT & emulator status per 20 seconds." + "\r\n" +
                                                            "5. Increase traffic channel by 10 and repeat setp2 util the channal set to the maximun one." + "\r\n" +
                                                            "6. DUT hangs up.",
                                                            "1. Establish phone connection successfully" + "\r\n" +
                                                            "2-5. Phone state should always be CONNECTED." + "\r\n" +
                                                            "6. Both DUT and emulator status are idle.",
                                                            1,
                                                            1);
            wa0201.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM1;
            wa0201.CurrentFormat = Wwan_TestCaseInfo.Format.GSM;

            Wwan_TestCaseInfo wa0202 = new Wwan_TestCaseInfo("WA0202",
                                                        "Adjust frequency and signal strength while phone calling (SIM1 - DCS 1800)",
                                                        "(1) Switch the wan band of station emulator to DCS 1800" + "\r\n" +
                                                        "(2) Make sure the DUT is attached to the station emulator.",
                                                        "1. DUT Calls station emulator,emulator pick up." + "\r\n" +
                                                        "2. Set traffic channel (Initial = minimun one of current band)" + "\r\n" +
                                                        "3. Decreace DUT signal strength from -82 db to -102 db for 10db/20seconds" + "\r\n" +
                                                        "4. Check the DUT & emulator status per 20 seconds." + "\r\n" +
                                                        "5. Increase traffic channel by 10 and repeat setp2 util to the maximun one." + "\r\n" +
                                                        "6. DUT hangs up.",
                                                        "1. Establish phone connection successfully" + "\r\n" +
                                                        "2-5. Phone state should always be CONNECTED." + "\r\n" +
                                                        "6. Both DUT and emulator status are idle.",
                                                        1,
                                                        1);
            wa0202.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM1;
            wa0202.CurrentFormat = Wwan_TestCaseInfo.Format.WCDMA;

            Wwan_TestCaseInfo wa0203 = new Wwan_TestCaseInfo("WA0203",
                                                        "Adjust frequency and signal strength while phone calling (SIM1 - GSM 900 & DCS 1800)",
                                                        "(1) Make sure the DUT could be attached to the station emulator (DCS1800 & GSM 900).",
                                                        "1. DUT Calls station emulator,emulator pick up." + "\r\n" +
                                                        "2. Switch the cell band of emulator between GSM900 and DCS1800 ( Initial = GSM900)" + "\r\n" +
                                                        "3. Set traffic channel (Initial = minimun one of current band)" + "\r\n" +
                                                        "4. Decreace DUT signal strength from -82 db to -102 db for 10db/20seconds" + "\r\n" +
                                                        "5. Check the DUT & emulator status per 20 seconds." + "\r\n" +
                                                        "6. Increase traffic channel of current band by 10" + "\r\n" +
                                                        "7. Repeat setp2 util both GSM and DCS channel set to the maximun one." + "\r\n" +
                                                        "8. DUT hangs up.",
                                                        "1. Establish phone connection successfully" + "\r\n" +
                                                        "2-7. Phone state should always be CONNECTED." + "\r\n" +
                                                        "8. Both DUT and emulator status are idle.",
                                                        1,
                                                        1);
            wa0203.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM1;
            wa0203.CurrentFormat = Wwan_TestCaseInfo.Format.WCDMA;

            Wwan_TestCaseInfo wa0251 = new Wwan_TestCaseInfo("WA0251",
                                                "Adjust frequency and signal strength while phone calling (SIM2 - GSM 900)",
                                                "(1) Switch the wan band of station emulator to GSM 900" + "\r\n" +
                                                "(2) Make sure the DUT is attached to the station emulator.",
                                                "1. DUT Calls station emulator,emulator pick up." + "\r\n" +
                                                "2. Set traffic channel (Initial = minimun one of current band)" + "\r\n" +
                                                "3. Decreace DUT signal strength from -82 db to -102 db for 10db/20seconds" + "\r\n" +
                                                "4. Check the DUT & emulator status per 20 seconds." + "\r\n" +
                                                "5. Increase traffic channel by 10 and repeat setp2 util the channal set to the maximun one." + "\r\n" +
                                                "6. DUT hangs up.",
                                                "1. Establish phone connection successfully" + "\r\n" +
                                                "2-5. Phone state should always be CONNECTED." + "\r\n" +
                                                "6. Both DUT and emulator status are idle.",
                                                1,
                                                1);
            wa0251.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM2;
            wa0251.CurrentFormat = Wwan_TestCaseInfo.Format.GSM;

            Wwan_TestCaseInfo wa0252 = new Wwan_TestCaseInfo("WA0252",
                                            "Adjust frequency and signal strength while phone calling (SIM2 - DCS 1800)",
                                            "(1) Switch the wan band of station emulator to DCS 1800" + "\r\n" +
                                            "(2) Make sure the DUT is attached to the station emulator.",
                                            "1. DUT Calls station emulator,emulator pick up." + "\r\n" +
                                            "2. Set traffic channel (Initial = minimun one of current band)" + "\r\n" +
                                            "3. Decreace DUT signal strength from -82 db to -102 db for 10db/20seconds" + "\r\n" +
                                            "4. Check the DUT & emulator status per 20 seconds." + "\r\n" +
                                            "5. Increase traffic channel by 10 and repeat setp2 util to the maximun one." + "\r\n" +
                                            "6. DUT hangs up.",
                                            "1. Establish phone connection successfully" + "\r\n" +
                                            "2-5. Phone state should always be CONNECTED." + "\r\n" +
                                            "6. Both DUT and emulator status are idle.",
                                            1,
                                            1);
            wa0252.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM2;
            wa0252.CurrentFormat = Wwan_TestCaseInfo.Format.WCDMA;

            Wwan_TestCaseInfo wa0253 = new Wwan_TestCaseInfo("WA0253",
                                            "Adjust frequency and signal strength while phone calling (SIM2 - GSM 900 & DCS 1800)",
                                            "(1) Make sure the DUT could be attached to the station emulator (DCS1800 & GSM 900).",
                                            "1. DUT Calls station emulator,emulator pick up." + "\r\n" +
                                            "2. Switch the cell band of emulator between GSM900 and DCS1800 ( Initial = GSM900)" + "\r\n" +
                                            "3. Set traffic channel (Initial = minimun one of current band)" + "\r\n" +
                                            "4. Decreace DUT signal strength from -82 db to -102 db for 10db/20seconds" + "\r\n" +
                                            "5. Check the DUT & emulator status per 20 seconds." + "\r\n" +
                                            "6. Increase traffic channel of current band by 10" + "\r\n" +
                                            "7. Repeat setp2 util both GSM and DCS channel set to the maximun one." + "\r\n" +
                                            "8. DUT hangs up.",
                                            "1. Establish phone connection successfully" + "\r\n" +
                                            "2-7. Phone state should always be CONNECTED." + "\r\n" +
                                            "8. Both DUT and emulator status are idle.",
                                            1,
                                            1);
            wa0253.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM2;
            wa0253.CurrentFormat = Wwan_TestCaseInfo.Format.WCDMA;


            lstTC_Handover.Add(wa0201);
            lstTC_Handover.Add(wa0202);
            lstTC_Handover.Add(wa0203);
            lstTC_Handover.Add(wa0251);
            lstTC_Handover.Add(wa0252);
            lstTC_Handover.Add(wa0253);
            #endregion Handover Testcases
            #region DUT Debug Testcases
            #region DBG0001
            Wwan_TestCaseInfo dbg0001 = new Wwan_TestCaseInfo();
            dbg0001.TCID = "DBG0001";
            dbg0001.Name = "In/Out coverage test";
            dbg0001.Loop = 100;
            dbg0001.PassingCriteria = 100;            
            lstTC_DebugTestcase.Add(dbg0001);
            #endregion DBG0001
            #region DBG0002
            Wwan_TestCaseInfo dbg0002 = new Wwan_TestCaseInfo();
            dbg0002.TCID = "DBG0002";
            dbg0002.Name = "In/Out coverage - Static";
            dbg0002.Loop = 1;
            dbg0002.PassingCriteria = 1;
            lstTC_DebugTestcase.Add(dbg0002);
            #endregion DBG0002
            #region DBG0003
            Wwan_TestCaseInfo dbg0003= new Wwan_TestCaseInfo();
            dbg0003.TCID = "DBG0003";
            dbg0003.Name = "In/Out coverage - Dynamic";
            dbg0003.Loop = 1;
            dbg0003.PassingCriteria = 1;
            lstTC_DebugTestcase.Add(dbg0003);
            #endregion DBG0003
            #endregion DUT Debug Testcases
            #endregion Test Case Description
            mainForm = main;
            foreach (Wwan_TestCaseInfo tcinfo in lstTC_GsmGprsWcdma)
            {
                ListViewItem li = new ListViewItem();
                li.Text = tcinfo.TCID+" - " + tcinfo.Name;
                lsvTestcases_GsmGprsWcdma.Items.Add(li);
            }

            foreach (Wwan_TestCaseInfo tcinfo in lstTC_TdScdma)
            {
                ListViewItem li = new ListViewItem();
                li.Text = tcinfo.TCID + " - " + tcinfo.Name;
                lsvTestcasees_TdScdma.Items.Add(li);
            }
            foreach (Wwan_TestCaseInfo tcinfo in lstTC_Handover)
            {
                ListViewItem li = new ListViewItem();
                li.Text = tcinfo.TCID + " - " + tcinfo.Name;
                lsvTestcases_Handover.Items.Add(li);
            }
            startDutMonitor();
            splitContainer2.Panel2Collapsed = true;
            splitContainer2.Panel1Collapsed = false; //Hide I/O converage testcase settings
            ProcedureProcessor_8960.ReadTestCaseSettings();
        }
        
        private void startDutMonitor()
        {
            dutStatusMonitor_Flag = true;
            if (tdDutStatus != null)
            {
                stopDutMonitor();
            }
            tdDutStatus = new Thread(dutStatusMonitor_Runnable);
            tdDutStatus.Start();
        }

        private void stopDutMonitor()
        {
            dutStatusMonitor_Flag = false;
            if (tdDutStatus != null)
            {
                tdDutStatus.Interrupt();
                tdDutStatus = null;
            }
        }

        private void dutStatusMonitor_Runnable()
        {
            try
            {
                while (dutStatusMonitor_Flag)
                {
                    Thread.Sleep(dutMonitorInterval);
                    refreshDutStatus();
                }
            }
            catch (ThreadInterruptedException tiex)
            {
            }
        }

        private void testcasesRun(bool debugMode, bool infinityMode, bool catchMtkLog)
        {
            mainForm.ClearLiveLogMessage();
            List<Wwan_TestCaseInfo> lstSelectedTCs = new List<Wwan_TestCaseInfo>();
            if (procedureProcessor != null)
            {
                procedureProcessor.Dispose();
            }
            try
            {
                clsDevice clsDevice = new clsDevice(cmbDeviceList.Text);
                procedureProcessor = new ProcedureProcessor_8960(mainForm,clsDevice, se8960);//connector);
                procedureProcessor.CallStateChangedEventHandler += new EventHandler<CallStateChangedEventArgs>(DutPhoneStateChangedEventHandler);
                se8960.StationEmulatorStateChangedEventHandler += new EventHandler<StationEmulatorStateChangedEventArgs>(StationEmulatorStateChangedEventHandler);
                procedureProcessor.ProcedureProgressChangedEventHandler += new EventHandler<ProcedureProgressChangedEventArgs>(ProcedureProgressChangedEventHandler);
                procedureProcessor.TestResultUpdateEventHandler += new EventHandler<TestResultUpdateEventArgs>(TestResultUpdateEventHandler);
                procedureProcessor.RunningStateChangedEventHandler += new EventHandler<ProcedureProcessorRunningStateChangedEventArgs>(ProcedureProcessorRunningStateChagedEventHandler);
                procedureProcessor.DutSignalStengthChangedEventHandler += new EventHandler<DutSignalStrengthChangedEventArgs>(DutSignalStrengthChangedEventHandler);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            if (lsvTestcasees_TdScdma.CheckedItems.Count > 0)
            {
                foreach (Wwan_TestCaseInfo tc in lstTC_TdScdma)
                {
                    if(tc.IsSelected){
                        lstSelectedTCs.Add(tc);
                    }                    
                }
                //procedureProcessor.Start(lstTC_TdScdma, debugMode, infinityMode, catchMtkLog);
            }
            if (lsvTestcases_GsmGprsWcdma.CheckedItems.Count > 0)
            {
                //procedureProcessor.Start(lstTC_GsmGprsWcdma, debugMode, infinityMode, catchMtkLog);
                foreach (Wwan_TestCaseInfo tc in lstTC_GsmGprsWcdma)
                {
                    if (tc.IsSelected)
                    {
                        lstSelectedTCs.Add(tc);
                    }
                }            
            }
            if (lsvTestcases_Handover.CheckedItems.Count > 0)
            {
                //procedureProcessor.Start(lstTC_Handover, debugMode, infinityMode, catchMtkLog);
                foreach (Wwan_TestCaseInfo tc in lstTC_Handover)
                {
                    if (tc.IsSelected)
                    {
                        lstSelectedTCs.Add(tc);
                    }
                }
            }
            if (lsvTestCase_Debug.CheckedItems.Count > 0)
            {
                #region Testcase - Debug0001
                if (lstTC_DebugTestcase[0].IsSelected)
                {
                    try
                    {
                        Wwan_TestCaseInfo debug0001 = lstTC_DebugTestcase[0];
                        debug0001.ClearProperties();
                        debug0001.PassingCriteria = debug0001.Loop;
                        debug0001.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM2;
                        if (uiDbg001 != null)
                        {
                            debug0001.CurrentBand = uiDbg001.Band;
                            debug0001.CellChannel = uiDbg001.CellChannel;
                            debug0001.Loop = uiDbg001.Loop;
                            debug0001.SetProperty("G1_Enable", uiDbg001.G1_Enable);
                            debug0001.SetProperty("G1_CellPower1", uiDbg001.G1_CellPower1);
                            debug0001.SetProperty("G1_CellPower2", uiDbg001.G1_CellPower2);
                            debug0001.SetProperty("G1_CellPower3", uiDbg001.G1_CellPower3);
                            debug0001.SetProperty("G1_Delay1", uiDbg001.G1_Delay1);
                            debug0001.SetProperty("G1_Delay2", uiDbg001.G1_Delay2);
                            debug0001.SetProperty("G1_Delay3", uiDbg001.G1_Delay3);

                            debug0001.SetProperty("G2_Enable", uiDbg001.G2_Enable);
                            debug0001.SetProperty("G2_CellPower1", uiDbg001.G2_CellPower1);
                            debug0001.SetProperty("G2_CellPower2", uiDbg001.G2_CellPower2);
                            debug0001.SetProperty("G2_CellPower3", uiDbg001.G2_CellPower3);
                            debug0001.SetProperty("G2_Delay1", uiDbg001.G2_Delay1);
                            debug0001.SetProperty("G2_Delay2", uiDbg001.G2_Delay2);
                            debug0001.SetProperty("G2_Delay3", uiDbg001.G2_Delay3);

                            debug0001.SetProperty("G3_Enable", uiDbg001.G3_Enable);
                            debug0001.SetProperty("G3_CellPower1", uiDbg001.G3_CellPower1);
                            debug0001.SetProperty("G3_CellPower2", uiDbg001.G3_CellPower2);
                            debug0001.SetProperty("G3_CellPower3", uiDbg001.G3_CellPower3);
                            debug0001.SetProperty("G3_Delay1", uiDbg001.G3_Delay1);
                            debug0001.SetProperty("G3_Delay2", uiDbg001.G3_Delay2);
                            debug0001.SetProperty("G3_Delay3", uiDbg001.G3_Delay3);

                            debug0001.SetProperty("G4_Enable", uiDbg001.G4_Enable);
                            debug0001.SetProperty("G4_CellPower1", uiDbg001.G4_CellPower1);
                            debug0001.SetProperty("G4_CellPower2", uiDbg001.G4_CellPower2);
                            debug0001.SetProperty("G4_CellPower3", uiDbg001.G4_CellPower3);
                            debug0001.SetProperty("G4_CellPower4", uiDbg001.G4_CellPower4);
                            debug0001.SetProperty("G4_Delay1", uiDbg001.G4_Delay1);
                            debug0001.SetProperty("G4_Delay2", uiDbg001.G4_Delay2);
                            debug0001.SetProperty("G4_Delay3", uiDbg001.G4_Delay3);
                            debug0001.SetProperty("G4_Delay4", uiDbg001.G4_Delay4);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                    }
                }
                #endregion Testcase - Debug0001
                #region Testcase - Debug0002
                if (lstTC_DebugTestcase[1].IsSelected)
                {
                    try
                    {
                        Wwan_TestCaseInfo debug0002 = lstTC_DebugTestcase[1];
                        debug0002.ClearProperties();
                        debug0002.PassingCriteria = debug0002.Loop;
                        debug0002.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM2;
                        if (uiDbg002 != null)
                        {
                            debug0002.CurrentBand = uiDbg002.Band;

                            debug0002.SetProperty("BER_Interval", uiDbg002.BER_Interval);
                            debug0002.SetProperty("Channel_Duration", uiDbg002.Channel_Duration);
                            debug0002.SetProperty("Channels", uiDbg002.Channels);
                            debug0002.SetProperty("RSCP", uiDbg002.RSCP);
                            debug0002.SetProperty("RSCP_Inaccuracy", uiDbg002.RSCP_Inaccuracy);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                    }
                }
                #endregion Testcase - Debug0002
                #region Testcase - Debug0003
                if (lstTC_DebugTestcase[2].IsSelected)
                {
                    try
                    {
                        Wwan_TestCaseInfo debug0003 = lstTC_DebugTestcase[2];
                        debug0003.ClearProperties();
                        debug0003.PassingCriteria = debug0003.Loop;
                        debug0003.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM2;
                        if (uiDbg003 != null)
                        {
                            debug0003.CurrentBand = uiDbg003.Band;
                            debug0003.SetProperty("BER_Interval", uiDbg003.BER_Interval);
                            debug0003.SetProperty("Channel_Duration", uiDbg003.Channel_Duration);
                            debug0003.SetProperty("Channels", uiDbg003.Channels);
                            debug0003.SetProperty("RSCP_Init", uiDbg003.RSCP_Init);
                            debug0003.SetProperty("RSCP_Inaccuracy_Init", uiDbg003.RSCP_Inaccuracy_Init);
                            debug0003.SetProperty("RSCP_High", uiDbg003.RSCP_High);
                            debug0003.SetProperty("RSCP_Inaccuracy_High", uiDbg003.RSCP_Inaccuracy_High);
                            debug0003.SetProperty("RSCP_Low", uiDbg003.RSCP_Low);
                            debug0003.SetProperty("CyclesPerChannel", uiDbg003.CyclesPerChannel);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                    }
                }
                #endregion Testcase - Debug0003
                foreach (Wwan_TestCaseInfo tc in lstTC_DebugTestcase)
                {
                    if (tc.IsSelected)
                    {
                        lstSelectedTCs.Add(tc);
                    }
                }
            }
            procedureProcessor.Start(lstSelectedTCs, debugMode, infinityMode, catchMtkLog);
        }

        #region Custom Event Handler

        private void ProcedureProgressChangedEventHandler(object sender, ProcedureProgressChangedEventArgs ea)
        {
            mainForm.UpdateProgress(ea);
        }

        private void TestResultUpdateEventHandler(object sender, TestResultUpdateEventArgs ea)
        {
            mainForm.UpdateTestResult(ea);
        }

        #region DUT connecting state UI refresh
        private delegate void delVoidNoparam();
        private bool dutAutoSelectFlag = false;
        private void refreshDutStatus()
        {
            if (this.InvokeRequired)
            {                
                try
                {
                    delVoidNoparam del = new delVoidNoparam(refreshDutStatus);
                    this.Invoke(del);
                }
                catch (Exception iex)
                {
                }
            }
            else
            {
                devList = ADB_Process.GetDeivcesList();
                dutAutoSelectFlag = true;
                cmbDeviceList.Items.Clear();
                foreach (AdbDeviceInfomation dev in devList)
                {
                    cmbDeviceList.Items.Add(dev.ID);
                }
                if (devList.Count == 0)
                {
                    currentDevice = null;
                    btnDutStatus.Text = "No Device";
                    btnDutStatus.BackColor = System.Drawing.Color.Crimson;
                    btnDutStatus.ForeColor = System.Drawing.Color.White;
                    dutReady = false;
                    atstReady = false;
                }
                else
                {
                    if (devList.Count == 1)
                    {
                        cmbDeviceList.SelectedIndex = 0;
                        currentDevice = devList[0];
                    }
                    else
                    {
                        if (currentDevice != null && cmbDeviceList.Items.Contains(currentDevice.ID))
                        {
                            cmbDeviceList.SelectedItem = currentDevice.ID;
                        }
                        else
                        {
                            currentDevice = null;
                            btnDutStatus.Text = "Select a DUT";
                            btnDutStatus.BackColor = System.Drawing.Color.Orange;
                            btnDutStatus.ForeColor = System.Drawing.Color.White;
                        }
                    }
                }
                if (currentDevice != null)
                {
                    switch (currentDevice.ConnectingStatus)
                    {
                        case "Offline":
                            btnDutStatus.Text = "Offline";
                            btnDutStatus.BackColor = System.Drawing.Color.Gray;
                            btnDutStatus.ForeColor = System.Drawing.Color.White;
                            dutReady = false;
                            atstReady = false;
                            break;
                        case "Connected":
                            dutReady = true;
                            if (!atstReady)
                            {
                                atstReady = checkAtstPackageReady();
                            }
                            if (atstReady)
                            {
                                btnDutStatus.Text = "Ready";
                                btnDutStatus.BackColor = System.Drawing.Color.Green;
                                btnDutStatus.ForeColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                btnDutStatus.Text = "Initializing...";
                                btnDutStatus.BackColor = System.Drawing.Color.YellowGreen;
                                btnDutStatus.ForeColor = System.Drawing.Color.White;
                                btnDutStatus_Click(btnDutStatus, new EventArgs());
                            }
                            break;
                        default:
                            dutReady = false;
                            atstReady = false;
                            btnDutStatus.Text = currentDevice.ConnectingStatus;
                            btnDutStatus.BackColor = System.Drawing.SystemColors.Control;
                            btnDutStatus.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
                dutAutoSelectFlag = false;
            }
        }
        #endregion DUT connecting state UI refresh

        #region DUT state UI refresh
        private void DutPhoneStateChangedEventHandler(object sender, CallStateChangedEventArgs ea)
        {
            refreshDutPhoneState(ea.State);
        }
        private delegate void delRefreshDutPhoneState(clsTelephony.CallStates state);
        private void refreshDutPhoneState(clsTelephony.CallStates state)
        {
            if (this.InvokeRequired)
            {
                delRefreshDutPhoneState del = new delRefreshDutPhoneState(refreshDutPhoneState);
                try
                {
                    this.Invoke(del, state);
                }
                catch (InvalidOperationException iex)
                {
                }
            }
            else
            {
                dut1State = state;
                switch (state)
                {
                    case clsTelephony.CallStates.IDLE:
                        txtDutPhoneState.BackColor = Color.Orange;
                        break;
                    case clsTelephony.CallStates.RINGING:
                        txtDutPhoneState.BackColor = Color.LimeGreen;
                        break;
                    case clsTelephony.CallStates.OFFHOOK:
                        txtDutPhoneState.BackColor = Color.Green;
                        break;
                    
                //    case dutController.DutPhoneState.Ringing:
                //        txtDutPhoneState.BackColor = Color.LimeGreen;
                //    break;
                //case dutController.DutPhoneState.Offhook:
                //    if (previousDutPhoneState.Equals(dutController.DutPhoneState.Ringing))
                //    {
                //        dut1State = dutController.DutPhoneState.Answered;
                //    }
                //    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Answered))
                //    {
                //        dut1State = dutController.DutPhoneState.Answered;
                //    }
                //    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Dialing))
                //    {
                //        if (procedureProcessor != null &&
                //            se8960.CurrentCellState.Equals(StationEmulator_8960.CallState.Connected))                           
                //        {
                //            dut1State = dutController.DutPhoneState.Connected;
                //        }
                //    }
                //    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Connected))
                //    {
                //        dut1State = dutController.DutPhoneState.Connected;
                //    }
                //    else
                //    {
                //        dut1State = dutController.DutPhoneState.Dialing;
                //    }
                //    txtDutPhoneState.BackColor = Color.Green;
                //    break;
                //case dutController.DutPhoneState.Idle:
                //    if (previousDutPhoneState.Equals(dutController.DutPhoneState.Offhook) ||
                //        previousDutPhoneState.Equals(dutController.DutPhoneState.Answered) ||
                //        previousDutPhoneState.Equals(dutController.DutPhoneState.Connected) ||
                //        previousDutPhoneState.Equals(dutController.DutPhoneState.Dialing))
                //    {
                //        dut1State = dutController.DutPhoneState.EndCall;
                //        txtDutPhoneState.BackColor = Color.DarkGreen;
                //    }
                //    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Ringing))
                //    {
                //        dut1State = dutController.DutPhoneState.Rejected;
                //        txtDutPhoneState.BackColor = Color.Purple;
                //    }
                //    else
                //    {
                //        dut1State = dutController.DutPhoneState.Idle;
                //        txtDutPhoneState.BackColor = Color.Crimson;
                //    }
                //    break;
                default:
                    txtDutPhoneState.BackColor = Color.Gray;
                    dut1State = clsTelephony.CallStates.IDLE;
                    break;
                }
                previousDutPhoneState = dut1State;
                String DutPhoneStateText = dut1State.ToString();
                //DutPhoneStateText =  DutPhoneStateText.Contains("_")?DutPhoneStateText.Substring(DutPhoneStateText.IndexOf("_")+1):DutPhoneStateText;
                txtDutPhoneState.Text = DutPhoneStateText;
            }
        }

        private void DutSignalStrengthChangedEventHandler(object sender, DutSignalStrengthChangedEventArgs ea)
        {
            refreshDutSignalStrength(ea.Slot, ea.SignalStrength);
        }

        private delegate void delRefreshDutSignalStrength(Wwan_TestCaseInfo.SimSlot slot, int strength);
        private void refreshDutSignalStrength(Wwan_TestCaseInfo.SimSlot slot, int strength)
        {
            if (this.InvokeRequired)
            {
                delRefreshDutSignalStrength del = new delRefreshDutSignalStrength(refreshDutSignalStrength);
                this.Invoke(del, slot, strength);
            }
            else
            {
                txtSimSlot.Text = slot.ToString();
                txtSignalStrength.Text = strength + " db";
                if (strength > -999)
                {
                    txtSignalStrength.BackColor = Color.LimeGreen;
                }
                else
                {
                    txtSignalStrength.BackColor = Color.Red;
                }
            }
        }
        #endregion DUT state UI refresh

        #region StationEmulator state UI refresh

        private void StationEmulatorStateChangedEventHandler(object sender, StationEmulatorStateChangedEventArgs ea)
        {
            refreshStationEmulatorState(ea);
        }
        
        private delegate void delStationEmulatorDutPhoneState(StationEmulatorStateChangedEventArgs sesca);
        private void refreshStationEmulatorState(StationEmulatorStateChangedEventArgs sesca)//String band, double cellPower, ProcedureProcessor_8960.StationEmulatorState state)
        {
            if (this.InvokeRequired)
            {
                delStationEmulatorDutPhoneState del = new delStationEmulatorDutPhoneState(refreshStationEmulatorState);
                try
                {
                    this.Invoke(del, sesca);// band, cellPower, state);
                }
                catch (InvalidOperationException iex)
                {
                }
            }
            else
            {
                switch (sesca.Key)
                {
                    case StationEmulator_8960.KEYS_StateChangedEvent.Application:
                        break;
                    case StationEmulator_8960.KEYS_StateChangedEvent.Format:
                        break;
                    case  StationEmulator_8960.KEYS_StateChangedEvent.Band:
                        txtBand.Text = sesca.Value.ToString();
                        break;
                    case StationEmulator_8960.KEYS_StateChangedEvent.TrafficChannel:
                        txtSeChannel.Text = sesca.Value.ToString().TrimStart('+');
                        break;
                    case StationEmulator_8960.KEYS_StateChangedEvent.CellChannel:

                        break;
                    case StationEmulator_8960.KEYS_StateChangedEvent.CellPower:
                        txtSeCellPower.Text = sesca.Value.ToString() +" db"; 
                        break;
                    case StationEmulator_8960.KEYS_StateChangedEvent.CallState:
                        refreshStationEmulatorState((StationEmulator_8960.CallStates)sesca.Value);
                        break;
                    case StationEmulator_8960.KEYS_StateChangedEvent.None:
                    default:
                        break;                  
                }
            }
        }

        private void refreshStationEmulatorState(StationEmulator_8960.CallStates state)
        {
            switch (state)
            {
                case StationEmulator_8960.CallStates.Idle:
                    txtStationEmulatorState.BackColor = Color.Black;
                    break;
                case StationEmulator_8960.CallStates.Alerting:
                    txtStationEmulatorState.BackColor = Color.LimeGreen;
                    break;
                case StationEmulator_8960.CallStates.Connected:
                    txtStationEmulatorState.BackColor = Color.Green;
                    break;
                case StationEmulator_8960.CallStates.Disconnecting:
                    txtStationEmulatorState.BackColor = Color.Red;
                    break;
                case StationEmulator_8960.CallStates.SetupRequest:
                    txtStationEmulatorState.BackColor = Color.SeaGreen;
                    break;
                case StationEmulator_8960.CallStates.Proceeding:
                    txtStationEmulatorState.BackColor = Color.LightGoldenrodYellow;
                    break;
                case StationEmulator_8960.CallStates.Unknow:
                default:
                    txtStationEmulatorState.BackColor = Color.Gray;
                    break;
            }
            txtStationEmulatorState.Text = state.ToString();
        } 
        #endregion StationEmulator state UI refresh
        
        delegate void delVoidObject(object param);
        #region Procedure running state refresh
        private void ProcedureProcessorRunningStateChagedEventHandler(object sender, ProcedureProcessorRunningStateChangedEventArgs ea)
        {
            procedureProcessorRunningStateRefresh(ea);
        }

        private void procedureProcessorRunningStateRefresh(object procedureProcessorRunningStateChangedEventArgs)
        {
            if (this.InvokeRequired)
            {
                delVoidObject del = new delVoidObject(procedureProcessorRunningStateRefresh);
                this.Invoke(del, procedureProcessorRunningStateChangedEventArgs);
            }
            else
            {
                ProcedureProcessorRunningStateChangedEventArgs param = procedureProcessorRunningStateChangedEventArgs as ProcedureProcessorRunningStateChangedEventArgs;
                switch (param.State)
                {
                    case ProcedureProcessor_8960.RunningState.Running:
                        btnRun.BackColor = Color.Green;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        btnRun.Enabled = true;
                        btnPause.Enabled = true;
                        btnPause.Image = Properties.Resources.pause;
                        break;
                    case ProcedureProcessor_8960.RunningState.Pausing:
                        btnRun.BackColor = Color.Orange;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        btnPause.Enabled = false;
                        btnRun.Enabled = false;
                        break;
                    case ProcedureProcessor_8960.RunningState.Paused:
                        btnRun.BackColor = Color.OrangeRed;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        btnPause.Enabled = true;
                        btnPause.Image = Properties.Resources.resume;
                        break;
                    case ProcedureProcessor_8960.RunningState.Stopped:
                        btnRun.BackColor = Color.Crimson;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        btnPause.Enabled = false;
                        btnPause.Image = Properties.Resources.pause;
                        break;
                    case ProcedureProcessor_8960.RunningState.Stopping:
                        btnRun.BackColor = Color.PaleVioletRed;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        btnPause.Enabled = false;
                        btnPause.Image = Properties.Resources.pause;
                        break;
                    case ProcedureProcessor_8960.RunningState.Finished:
                        btnRun.BackColor = Color.MediumPurple;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        btnPause.Enabled = false;
                        btnPause.Image = Properties.Resources.pause;
                        break;
                    default:
                        btnRun.BackColor = SystemColors.Control;
                        btnRun.ForeColor = SystemColors.MenuHighlight;
                        btnRun.Text = "Run";
                        btnPause.Enabled = false;
                        btnPause.Image = Properties.Resources.pause;
                        break;
                }
            }
        }
        #endregion Procedure running state refresh

        #endregion Custom Event Handler

        private bool checkAtstPackageReady()
        {
            bool apkReady = false, pathReady = false;
            String adbArgument = "", adbReturn = "";
            #region check atst apk
            List<String> list = ADB_Process.GetPackagesList(currentDevice.ID, "com.asus.at");
            if (list.Count > 0)
            {
                apkReady = true;
            #endregion check atst apk
                #region check file path
                if (currentDevice != null && currentDevice.ID.Length > 0)
                {
                    adbArgument += "-s " + currentDevice.ID + " ";
                }
                adbArgument += "shell ls /sdcard/ATST/ToolInfo/";
                ADB_Process.RunAdbCommand(adbArgument, ref adbReturn);
                pathReady = !adbReturn.ToLower().Contains("no such file or directory");
                #endregion check file path
            }
            return apkReady & pathReady;
        }

        private void initializeAtstPackage()
        {
            String deviceHeader = "", adbArgument = "", adbReturn = "";
            if (currentDevice != null && currentDevice.ID.Length > 0)
            {
                deviceHeader = "-s " + currentDevice.ID + " ";
            }
            #region delete old files
            adbArgument = deviceHeader + "shell rm -r /sdcard/ATST";
            ADB_Process.RunAdbCommand(adbArgument);
            #endregion delete old files
            #region create file path
            adbArgument = deviceHeader + "shell mkdir -p /sdcard/ATST/ToolInfo";
            ADB_Process.RunAdbCommand(adbArgument);
            //adbArgument = deviceHeader + "shell mkdir -p /sdcard/ATST/Core";
            //ADB_Process.RunAdbCommand(adbArgument);
            adbArgument = deviceHeader + "shell mkdir -p /sdcard/ATST/Logs";
            ADB_Process.RunAdbCommand(adbArgument);
            #endregion create file path
            #region install apk
            adbArgument = deviceHeader + "install -r " + "\"" + apiFunctionApkPath + "\"";
            String rtn = "", error = "";
            ADB_Process.RunAdbCommand(adbArgument, ref rtn, ref error, true);
            #endregion install apk
        }

        #region UI events
        private void lsvTestcases_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = lsvTestcases_GsmGprsWcdma.GetItemAt(e.X, e.Y);
            if (li != null)
            {
                txtTcDescripition.Text = "";
                txtTcDescripition.Text += " [ Prerequisite ] : \r\n";
                txtTcDescripition.Text += "(0) Set System application to GSM/GPRS/WCDMA\r\n";
                txtTcDescripition.Text += lstTC_GsmGprsWcdma[li.Index].Prerequisite+"\r\n\r\n";
                txtTcDescripition.Text += " [ Procedure ] : \r\n";
                txtTcDescripition.Text += lstTC_GsmGprsWcdma[li.Index].Procedure + "\r\n\r\n";
                txtTcDescripition.Text += " [ Excepted Result ] (" + lstTC_GsmGprsWcdma[li.Index].PassingCriteria + " / " + lstTC_GsmGprsWcdma[li.Index].Loop + "): \r\n";
                txtTcDescripition.Text += lstTC_GsmGprsWcdma[li.Index].ExpectedResult+"\r\n\r\n";
            }
            else
            {
                txtTcDescripition.Text = "";
            }
        }

        private void lsvTestCasees_TdScdma_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = lsvTestcasees_TdScdma.GetItemAt(e.X, e.Y);
            if (li != null)
            {
                txtTcDescripition.Text = "";
                txtTcDescripition.Text += " [ Prerequisite ] : \r\n";
                txtTcDescripition.Text += "(0) Set System application to " + lstTC_TdScdma[li.Index].CurrentFormat + "\r\n";
                txtTcDescripition.Text += lstTC_TdScdma[li.Index].Prerequisite + "\r\n\r\n";
                txtTcDescripition.Text += " [ Procedure ] : \r\n";
                txtTcDescripition.Text += lstTC_TdScdma[li.Index].Procedure + "\r\n\r\n";
                txtTcDescripition.Text += " [ Excepted Result ] (" + lstTC_TdScdma[li.Index].PassingCriteria + " / " + lstTC_TdScdma[li.Index].Loop + "): \r\n";
                txtTcDescripition.Text += lstTC_TdScdma[li.Index].ExpectedResult + "\r\n\r\n";
            }
            else
            {
                txtTcDescripition.Text = "";
            }
        }

        private void lsvTestcases_Handover_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = lsvTestcases_Handover.GetItemAt(e.X, e.Y);
            if (li != null)
            {  
                txtTcDescripition.Text = "";
                txtTcDescripition.Text += " [ Prerequisite ] : \r\n";
                txtTcDescripition.Text += "(0) Set System application to "+lstTC_Handover[li.Index].CurrentFormat+"\r\n";
                txtTcDescripition.Text += lstTC_Handover[li.Index].Prerequisite + "\r\n\r\n";
                txtTcDescripition.Text += " [ Procedure ] : \r\n";
                txtTcDescripition.Text += lstTC_Handover[li.Index].Procedure + "\r\n\r\n";
                txtTcDescripition.Text += " [ Excepted Result ] (" + lstTC_Handover[li.Index].PassingCriteria + " / " + lstTC_Handover[li.Index].Loop + "): \r\n";
                txtTcDescripition.Text += lstTC_Handover[li.Index].ExpectedResult + "\r\n\r\n";
            }
            else
            {
                txtTcDescripition.Text = "";
            }
        }

        private void rdbConnector_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Name.Equals("rdbGpib"))
            {
                gbGpib.Visible = rdb.Checked;
                //checkGpibConnected();
            }
            else if (rdb.Name.Equals("rdbVisa"))
            {
                gbVisa.Visible = rdb.Checked;
                //checkVisaConnected();             
            }
        }      

        private void btnGpibConnect_Click(object sender, EventArgs e)
        {
            GPIB_Connector connector = null;
            //try
            //{
                //connector = new GPIB_Connector(Convert.ToInt16(numBoard.Value), Convert.ToByte(numGPIB1.Value), Convert.ToByte(numGPIB2.Value));
                //connector.Connect(); //auto connect when constructed.
                //if (connector.IsConnected)
                //{
                    //se8960 =  new StationEmulator_8960(connector);
                //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //if (connector != null && connector.IsConnected)
            //{
                btnGpibConnect.Text = "Connected";
                btnGpibConnect.BackColor = Color.Green;
                btnGpibConnect.ForeColor = Color.White;
            //}
            //else
            //{
            //    btnGpibConnect.Text = "Disconnected";
            //    btnGpibConnect.BackColor = Color.Crimson;
            //    btnGpibConnect.ForeColor = Color.White;
            //}
        }

        private void btnVisaConnect_Click(object sender, EventArgs e)
        {
            //VISA_Connector connector = null;
            //try
            //{
                
            //    connector = new VISA_Connector(txtVisaName.Text);
            //    connector.Connect();
            //    btnVisaConnect.Text = "Connected";
            //    btnVisaConnect.BackColor = Color.Green;
            //    btnVisaConnect.ForeColor = Color.White;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    btnVisaConnect.Text = "Disconnected";
            //    btnVisaConnect.BackColor = Color.Crimson;
            //    btnVisaConnect.ForeColor = Color.White;
            //}
        }

        private void btnStationEmulatorFuncTest_Click(object sender, EventArgs e)
        {
            if (se8960 == null)
            {
                MessageBox.Show("Establish a connection first.");
            }
            else
            {
                frmStationEmulatorFunctionTest.Display(se8960);//(GPIB_Connector)connector);
            }
        }

        private void cmbDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dutAutoSelectFlag)
            {
            }
            else
            {
                currentDevice = devList[cmbDeviceList.SelectedIndex];
            }
        }

        private void btnDutTest_Click(object sender, EventArgs e)
        {
            frmDutFunctionTest.Display(new clsDevice(cmbDeviceList.Text));
        }
        
        private void btnDutStatus_Click(object sender, EventArgs e)
        {
            if (dutReady && !atstReady)
            {
                this.Cursor = Cursors.WaitCursor;
                initializeAtstPackage();
                atstReady = checkAtstPackageReady();
                this.Cursor = Cursors.Default;
            }
        }      

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (procedureProcessor != null && procedureProcessor.CurrentRunningState.Equals(ProcedureProcessor_8960.RunningState.Running))
            {
                procedureProcessor.Stop();
            }
            else
            {
                if (lsvTestcases_GsmGprsWcdma.CheckedItems.Count > 0 ||
                    lsvTestcasees_TdScdma.CheckedItems.Count > 0 ||
                    lsvTestcases_Handover.CheckedItems.Count > 0 ||
                    lsvTestCase_Debug.CheckedItems.Count>0)
                {
                    mainForm.ClearLiveLogMessage();
                    if ((se8960 != null && currentDevice != null) || bForceStart_Flag)
                    {
                        testcasesRun(ckbDebug.Checked,ckbInfinityMode.Checked,ckbCatchMtkLog.Checked);
                    }
                    else
                    {
                        MessageBox.Show("Establish the connections with station emulator & DUT  first, please.");
                    }
                }                
                else
                {
                    MessageBox.Show("Choose the test case(s) to run.");
                }
            }
        }
                     
        private void lvTestCase_SizeChanged(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            lv.Columns[0].Width = lv.Width - (2 * lv.Bounds.Width);
        }

        bool warningMessage = true;
        private void lsvTestCasees_TdScdma_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if ((lsvTestcases_GsmGprsWcdma.CheckedItems.Count > 0) || 
                (lsvTestcases_Handover.CheckedItems.Count > 0) ||
                (lsvTestCase_Debug.CheckedItems.Count > 0))
            {
                e.NewValue = CheckState.Unchecked;
                if (warningMessage)
                {
                    MessageBox.Show("The test cases of \"TD-SCDMA\" & \"GSM/GPRS/WCDMA\" could not be selected at the same time");
                    warningMessage = false;
                }
            }
            else
            {
                lstTC_TdScdma[e.Index].IsSelected = e.NewValue.Equals(CheckState.Checked);
            }
        }

        private void lsvTestcases_GsmGprsWcdma_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lsvTestcasees_TdScdma.CheckedItems.Count > 0)
            {
                e.NewValue = CheckState.Unchecked;
                if (warningMessage)
                {
                    MessageBox.Show("The test cases of \"TD-SCDMA\" & \"GSM/GPRS/WCDMA\" could not be selected at the same time");
                    warningMessage = false;
                }
            }
            else
            {
                lstTC_GsmGprsWcdma[e.Index].IsSelected = e.NewValue.Equals(CheckState.Checked);
            }
        }

        private void lsvTestcases_BandFreqNStrength_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lsvTestcasees_TdScdma.CheckedItems.Count > 0)
            {
                e.NewValue = CheckState.Unchecked;
                if (warningMessage)
                {
                    MessageBox.Show("The test cases of \"TD-SCDMA\" & \"GSM/GPRS/WCDMA\" could not be selected at the same time");
                    warningMessage = false;
                }
            }
            else
            {
                lstTC_Handover[e.Index].IsSelected = e.NewValue.Equals(CheckState.Checked);
            }
        }
        private void btnInitialSettings_Click(object sender, EventArgs e)
        {
            //if (connector != null && connector.IsConnected() && procedureProcessor!=null)
            //{
            frmStationEmulatorInitialization.SetSettings(se8960, tpcTestcases.SelectedIndex, procedureProcessor);
            //}
            //else
            //{
            //    MessageBox.Show("Establish the connections with station emulator (8960) first, please.");
            //}
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            warningMessage = true;
            splitContainer2.Panel2Collapsed = true;
            splitContainer2.Panel1Collapsed = false; //Hide I/O converage testcase settings
        }

        private void txtDutPhoneState_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                try
                {
                    clsDevice clsDevice = new clsDevice(cmbDeviceList.Text);
                    String strCurrentState = clsDevice.Telephony.CallState.ToString();//ADB_Process.GetPhoneCallState(devList[cmbDeviceList.SelectedIndex].ID, 5000);
                    txtDutPhoneState.Text = strCurrentState;
                    txtDutPhoneState.BackColor = System.Drawing.Color.Black;
                    txtDutPhoneState.ForeColor = System.Drawing.Color.White;
                }
                catch
                {

                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(procedureProcessor.CurrentRunningState.Equals(ProcedureProcessor_8960.RunningState.Paused) ||
                procedureProcessor.CurrentRunningState.Equals(ProcedureProcessor_8960.RunningState.Pausing))
            {
                procedureProcessor.Pause(false);
            }
            else if(procedureProcessor.CurrentRunningState.Equals(ProcedureProcessor_8960.RunningState.Running))
            {
                procedureProcessor.Pause(true);
            }
        }

        String pw = "";
        private void txtTcDescripition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && !(e.KeyCode.Equals(Keys.RButton | Keys.ShiftKey)))
            {
                pw += e.KeyCode.ToString();
            }
            else
            {
                pw = "";
            }
            if (pw.Length > 3)
            {
                pw = "";
            }
            if (pw.ToLower().Contains("usi"))
            {
                gbxDebugTools.Visible = !gbxDebugTools.Visible;
                pw = "";
            }
        }

        private void btnDutSignal_Click(object sender, EventArgs e)
        {
            if (cmbDeviceList.Text.Length > 0)
            {
                frmDutSignal.Display(new clsDevice(cmbDeviceList.Text), se8960);
            }           
        }

        bool bForceStart_Flag = false;
        private void btnRun_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                bForceStart_Flag = true;
            }
        }

        private void btnRun_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                bForceStart_Flag = false;
            }
        }

        private void lsvTestCaseDebug_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lstTC_DebugTestcase.Count > e.Item.Index)
            {
                lstTC_DebugTestcase[e.Item.Index].IsSelected = e.Item.Checked;
            }
        }

        private void lsvTestCaseDebug_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = lsvTestCase_Debug.GetItemAt(e.X, e.Y);
            if (li != null)
            {
                splitContainer2.Panel2Collapsed = false;
                splitContainer2.Panel1Collapsed = true; //Show I/O converage testcase settings
                splitContainer2.Panel2.Controls.Clear();
                UserControl usc = null;
                if (li.Index == 0)
                {
                    if (uiDbg001 == null)
                    {
                        uiDbg001 = new ucDbg0001();
                    }
                    usc = uiDbg001;
                }

                else if (li.Index == 1)
                {
                    if (uiDbg002 == null)
                    {
                        uiDbg002 = new ucDbg0002();
                    }
                    usc = uiDbg002;
                }
                else if (li.Index == 2)
                {
                    if (uiDbg003 == null)
                    {
                        uiDbg003 = new ucDbg0003();
                    }
                    usc = uiDbg003;
                }
                if (usc != null)
                {
                    splitContainer2.Panel2.Controls.Add(usc);
                    usc.Dock = DockStyle.Fill;
                }
            }
        }
        
        private void lsvTestCase_Debug_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lsvTestcasees_TdScdma.CheckedItems.Count > 0)
            {
                e.NewValue = CheckState.Unchecked;
                if (warningMessage)
                {
                    MessageBox.Show("The test cases of \"TD-SCDMA\" & \"GSM/GPRS/WCDMA\" could not be selected at the same time");
                    warningMessage = false;
                }
            }
            else
            {
                lstTC_DebugTestcase[e.Index].IsSelected = e.NewValue.Equals(CheckState.Checked);
            }
        }
        #endregion UI events     


    }
}
