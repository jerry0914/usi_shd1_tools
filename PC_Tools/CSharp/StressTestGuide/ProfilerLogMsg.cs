using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.usi.shd1_tools.TestGuide
{
    public class ProfilerLogMsg
    {
        public enum LogLevel
        {
            None = 0,
            Error = 1,
            Reserved_1 = 2,
            Info = 3,
            Reserved_2 = 4,
            Debug = 5
        };
        public DateTime MsgTime = DateTime.MinValue;
        public TimeSpan ElapsedTime = TimeSpan.Zero;
        public Int32 CategoryCode = 0;
        public LogLevel Level = LogLevels.Info;
        public String Header = "";
        public String Message = "";
        /// <summary>If the log message is not continual , set the timeout flag on to express it's a unavailable message.
        /// 
        /// </summary>
        public bool Timeout = false;

        public override string ToString()
        {
            return MsgTime.ToString("yyyy/MM/dd_HH:mm:ss") + " | " +
                        ((int)ElapsedTime.TotalHours).ToString("0000") + ":" + ElapsedTime.Minutes.ToString("00") + ":"  +ElapsedTime.Seconds.ToString("00") + " | " +
                        "0x"+CategoryCode.ToString("x8") + " | " +
                        Enum.GetName(Level.GetType(), Level) + " | " +
                        Header + " | " +
                        Message;
        }
    }
}
