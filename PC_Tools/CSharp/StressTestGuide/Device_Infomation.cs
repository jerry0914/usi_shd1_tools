using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.usi.shd1_tools.TestGuide
{
    public class Device_Infomation
    {
        public String ID = "";
        public String ConnectingStatus = "";
        public Device_Infomation(String id, String connectingStatus)
        {
            ID = id;
            ConnectingStatus = connectingStatus;
        }
    }
}
