using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.usi.shd1_tools.TestGuide
{
    public class BatteryInfo
    {
        public DateTime Time = DateTime.MinValue;
        public double Percentage = 0.0;
        public int Voltage = 0;
        public double Temperature = 0.0;
        public String AC_Status = "";
        public int AvgCurrent = 0;
    }
}
