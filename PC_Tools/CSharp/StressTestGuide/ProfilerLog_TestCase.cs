using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.usi.shd1_tools.TestGuide
{
    public class ProfilerLog_TestCase
    {
        public String LOG_OPTIONS = "";
        public String Title = "";
        public String Version = "";
        public DateTime StartTime
        {
            get
            {
                if (LogMessages.Count > 0 && !LogMessages[0].Timeout)
                {
                    return LogMessages[0].MsgTime;
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
                if (LogMessages.Count > 0)
                {
                    int index = LogMessages.Count - 1;
                    for (; index >= 0; index--)
                    {
                        if (!LogMessages[index].Timeout)
                        {
                            break;
                        }
                    }
                    if (index >= 0)
                    {
                        returnValue = LogMessages[index].MsgTime;
                    }
                }
                return returnValue;
            }
        }
        public List<ProfilerLogMsg> LogMessages = new List<ProfilerLogMsg>();
        /// <summary>Record the index of error messages to search them quickly.
        /// 
        /// </summary>
        public List<int> ErrorIndexList = new List<int>();
        public List<BatteryInfo> BatteryInfoList = new List<BatteryInfo>();
    }
}
