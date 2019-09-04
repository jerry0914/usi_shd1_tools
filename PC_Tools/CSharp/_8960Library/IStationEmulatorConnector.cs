using System;

namespace com.usi.shd1_tools._8960Library
{
    public interface IStationEmulatorConnector
    {
        //EventHandler<StationEmulatorLiveLogMessageEventArgs> StationEmulatorLiveLogMessageEventHandler { get; set; }
        bool IsConnected
        {
            get;
        }
        void Write(String command);
        void Write(String[] commands);
        String Read();
        String Query(String commands);
        void Connect();
    }  
}
