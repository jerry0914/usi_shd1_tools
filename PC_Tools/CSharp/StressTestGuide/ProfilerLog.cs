using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace com.usi.shd1_tools.TestGuide
{
    public class ProfilerLog
    {
        public readonly String LogFullPath = "";
        private List<ProfilerLog_TestCase> testCaseList = new List<ProfilerLog_TestCase>();
        public List<ProfilerLog_TestCase> TestCaseList
        {
            get
            {
                return testCaseList;
            }
        }
        private DateTime logDateTime = DateTime.MaxValue;
        public DateTime StartTime
        {
            get
            {
                if (testCaseList.Count > 0)
                {
                    return testCaseList[0].StartTime;
                }
                else
                {
                    return DateTime.MaxValue;
                }
            }
        }
        public DateTime EndTime
        {
            get
            {
                DateTime returnValue = DateTime.MinValue;
                if (testCaseList.Count > 0)
                {
                    int index = testCaseList.Count-1;
                    for(;index>=0;index--)
                    {
                        if(!testCaseList[index].EndTime.Equals(DateTime.MinValue))
                        {
                            break;
                        }
                    }
                    if (index >= 0)
                    {
                        returnValue = testCaseList[index].EndTime;
                    }
                }
                return returnValue;
            }
        }
        public int ErrorCount
        {
            get
            {
                int count = 0;
                foreach (ProfilerLog_TestCase tc in testCaseList)
                {
                    count += tc.ErrorIndexList.Count;
                }
                return count;
            }
        }
        public int MessageCount
        {
            get
            {
                int count = 0;
                foreach (ProfilerLog_TestCase tc in testCaseList)
                {
                    count += tc.LogMessages.Count;
                }
                return count;
            }
        }
        private ProfilerLog_TestCase currentTestCase = null;
        private BatteryInfo currentBatteryInfo = null;
        private double batteryPercentageTemp = -1;
        public ProfilerLog(String path)
        {
            if (File.Exists(path))
            {
                LogFullPath = path;
            }
        }

        public bool Parse()
        {
            bool result = false;
            if (File.Exists(LogFullPath))
            {
                StreamReader sr = null;
                testCaseList.Clear();
                try
                {
                    sr = new StreamReader(LogFullPath);
                    while (!sr.EndOfStream)
                    {
                        ProfilerLogMsg newMsg = parseLog_OneLine(sr.ReadLine());
                        if (newMsg != null)
                        {
                            if (newMsg.Header.ToUpper().Contains("LOG OPTIONS"))
                            {
                                #region The keyword of creating a new test case.
                                ProfilerLog_TestCase newTestCase = new ProfilerLog_TestCase();
                                newTestCase.LOG_OPTIONS = newMsg.Message;
                                testCaseList.Add(newTestCase);
                                currentTestCase = newTestCase;
                                #endregion The keyword of creating a new test case.
                            }
                            else if (newMsg.Header.ToLower().Contains("script version"))
                            {
                                try
                                {
                                    currentTestCase.Version = newMsg.Header.Split(':')[1].Trim();
                                }
                                catch
                                {
                                }
                            }
                            else if (newMsg.Header.ToLower().Contains("script title"))
                            {
                                try
                                {
                                    currentTestCase.Title = newMsg.Header.Split(':')[1].Trim();
                                }
                                catch
                                {
                                }
                            }
                            #region Battery Infomation Related
                            else if (newMsg.Header.ToLower().Trim().Equals("battery module"))
                            {
                                #region AC Power Status, the keyword to create new BatteryInfo object
                                if (newMsg.Message.ToLower().Contains("ac power status"))
                                {
                                    BatteryInfo batInfo = new BatteryInfo();
                                    try
                                    {
                                        batInfo.AC_Status = newMsg.Message.Split('|')[1];
                                        batInfo.Time = newMsg.MsgTime;
                                    }
                                    catch
                                    {
                                    }
                                    currentBatteryInfo = batInfo;
                                }
                                #endregion  AC Power Status, the keyword to create new BatteryInfo object
                                #region Battery Life Percent
                                else if (newMsg.Message.ToLower().Contains("batterylifepercent"))
                                {
                                    try
                                    {
                                        currentBatteryInfo.Percentage = Convert.ToDouble(newMsg.Message.Split('|')[1]);
                                    }
                                    catch
                                    {

                                    }
                                }
                                #endregion Battery Life Percent
                                #region Voltage
                                else if (newMsg.Message.ToLower().Contains("batteryvoltage"))
                                {
                                    try
                                    {
                                        String strVoltage = newMsg.Message.Split('|')[1];
                                        strVoltage = strVoltage.Replace("mV", "").Trim();
                                        currentBatteryInfo.Voltage = Convert.ToInt32(strVoltage);
                                    }
                                    catch
                                    {

                                    }
                                }
                                #endregion Voltage
                                #region Temperature
                                else if (newMsg.Message.ToLower().Contains("batterytemperature"))
                                {
                                    try
                                    {
                                        String strTemperature = newMsg.Message.Split('|')[1];
                                        strTemperature = strTemperature.Replace("degrees Celsius", "").Trim();
                                        currentBatteryInfo.Temperature = Convert.ToDouble(strTemperature);
                                    }
                                    catch
                                    {

                                    }
                                }
                                #endregion Temperature
                                #region AverageBatteryCurrent,the key word of end section of the BatteryInfo
                                else if (newMsg.Message.ToLower().Contains("averagebatterycurrent"))
                                {
                                    try
                                    {
                                        String strCurrent = newMsg.Message.Split('|')[1];
                                        strCurrent = strCurrent.Replace("mA", "").Trim();
                                        currentBatteryInfo.AvgCurrent = Convert.ToInt32(strCurrent);
                                    }
                                    catch
                                    {

                                    }
                                    if (currentBatteryInfo.Percentage != batteryPercentageTemp)
                                    {
                                        currentTestCase.BatteryInfoList.Add(currentBatteryInfo);
                                        batteryPercentageTemp = currentBatteryInfo.Percentage;
                                    }
                                    else //Discard currentBatteryInfo if the percentage is not changed.
                                    {
                                        currentBatteryInfo = null;
                                    }
                                }
                                #endregion AverageBatteryCurrent,the key word of end section of the BatteryInfo
                            }
                            #endregion  Battery Infomation Related
                            currentTestCase.LogMessages.Add(newMsg);
                            if (newMsg.Level.Equals(ProfilerLogMsg.LogLevels.Error))
                            {
                                currentTestCase.ErrorIndexList.Add(currentTestCase.LogMessages.Count - 1);
                            }
                        }
                    }
                    result = true;
                }
                catch
                {

                }
                finally
                {

                    if (sr != null)
                    {
                        sr.Close();
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void RefreshIsMsgTimeout(int LimitLogInterval)
        {
            DateTime lastLogTime = DateTime.MaxValue;
            bool isTimout = false;
            foreach (ProfilerLog_TestCase tc in testCaseList)
            {
                foreach(ProfilerLogMsg msg in tc.LogMessages)
                {
                    if (isTimout)
                    {
                        msg.Timeout = true;
                    }
                    else
                    {
                        if (lastLogTime.Equals(DateTime.MaxValue))
                        {
                            lastLogTime = msg.MsgTime;
                        }
                        else
                        {
                            isTimout = msg.MsgTime.Subtract(lastLogTime).TotalMinutes > LimitLogInterval;
                            msg.Timeout = isTimout;
                            if (!isTimout)
                            {
                                lastLogTime = msg.MsgTime;
                            }
                        }
                    }
                }
            }
        }

        private ProfilerLogMsg parseLog_OneLine(String line)
        {
            ProfilerLogMsg lmsg = null;
            String reguex = @"(?<MsgTime>\d+:\d+:\d+)(\s*)\|(\s*)"+
                                         @"(?<ElapsedTime>\d+:\d+:\d+)(\s*)\|(\s*)"+
                                         @"(?<CategoryCode>0x[a-fA-F0-9]+)(\s*)\|(\s*)"+
                                         @"(?<Level>\S+)(\s*)\|(\s*)"+
                                         @"(?<Header>[^\|]+)" +
                                         @"((\s*)\|(\s*)(?<Message>.*)|)";            
            Regex rgx = new Regex(reguex);
            Match m = rgx.Match(line);
            if (m.Success)
            {
                try
                {
                    lmsg = new ProfilerLogMsg();
                    String msgTime = m.Groups["MsgTime"].Value;
                    String elapsedTime = m.Groups["ElapsedTime"].Value;
                    lmsg.CategoryCode = Convert.ToInt32(m.Groups["CategoryCode"].Value,16);
                    lmsg.Level = (ProfilerLogMsg.LogLevel)Enum.Parse(lmsg.Level.GetType(), m.Groups["Level"].Value);
                    lmsg.Header = m.Groups["Header"].Value;
                    lmsg.Message = m.Groups["Message"].Value;

                    #region refresh last logDateTime for the first time meet [Current Time]
                    if (logDateTime.Equals(DateTime.MaxValue))
                    {
                        Regex rgxCurrentTime = new Regex(@"(?i:current(\s*)time)\s*:\s*"+
                                                                                        @"(?<year>\d{4})\."+
                                                                                        @"(?<month>\d{2})\."+
                                                                                        @"(?<day>\d{2})\s*-\s*"+
                                                                                        @"(?<hour>\d{2})\."+
                                                                                        @"(?<minute>\d{2})\."+
                                                                                        @"(?<second>\d{2})");
                        Match m1 = rgxCurrentTime.Match(lmsg.Message);
                        if (m1.Success)
                        {
                            DateTime temp = new DateTime(Convert.ToInt32(m1.Groups["year"].Value),
                                                             Convert.ToInt32(m1.Groups["month"].Value),
                                                             Convert.ToInt32(m1.Groups["day"].Value),
                                                             Convert.ToInt32(m1.Groups["hour"].Value),
                                                             Convert.ToInt32(m1.Groups["minute"].Value),
                                                             Convert.ToInt32(m1.Groups["second"].Value));
                            #region Update the early logs before first [Current Time]

                            foreach (ProfilerLogMsg msg in currentTestCase.LogMessages)
                            {

                                DateTime correctTime = new DateTime(temp.Year,
                                                                                                     temp.Month,
                                                                                                     temp.Day,
                                                                                                     msg.MsgTime.Hour,
                                                                                                     msg.MsgTime.Minute,
                                                                                                     msg.MsgTime.Second);
                                msg.MsgTime = correctTime;
                                logDateTime = correctTime;
                            }
                            #endregion Update the early logs before first [Current Time]
                        }
                    }
                    #endregion refresh last logDateTime for the first time meet [Current Time]

                    #region combine the date and time of message log
                    Regex rgxMsgTime = new Regex(@"(?<hour>\d{2}):(?<minute>\d{2}):(?<second>\d{2})");
                    Match m2 = rgxMsgTime.Match(msgTime);
                    if(m2.Success)
                    {
                        DateTime msgDateTime = new DateTime(logDateTime.Year,
                                                                                                logDateTime.Month,
                                                                                                logDateTime.Day,
                                                                                                Convert.ToInt32(m2.Groups["hour"].Value),
                                                                                                Convert.ToInt32(m2.Groups["minute"].Value),
                                                                                                Convert.ToInt32(m2.Groups["second"].Value));
                        //If the newly logMsg time is less than order one, it means the day is increased.
                        if (!logDateTime.Equals(DateTime.MaxValue) && msgDateTime.Subtract(logDateTime).TotalSeconds < 0)
                        {
                            msgDateTime = msgDateTime.AddDays(1);
                            logDateTime = msgDateTime; // Update logDateTime to sync to last logMsg.
                        }
                        lmsg.MsgTime = msgDateTime;
                    }
                    #endregion combine the date and time of message log

                    #region elapsed time
                    Regex rgxElapsedTime = new Regex(@"(?<hours>\d{4}):(?<minutes>\d{2}):(?<seconds>\d{2})");
                    Match m3 = rgxElapsedTime.Match(elapsedTime);
                    if (m3.Success)
                    {
                        lmsg.ElapsedTime = new TimeSpan(Convert.ToInt32(m3.Groups["hours"].Value),
                                                                                       Convert.ToInt32(m3.Groups["minutes"].Value),
                                                                                       Convert.ToInt32(m3.Groups["seconds"].Value));
                    }
                    #endregion elapsed time
                }
                catch (Exception ex)
                {
                    lmsg = null;
                }
            }
            return lmsg;
        }
    }
}
