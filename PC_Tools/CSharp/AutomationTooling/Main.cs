using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace AutomationTooling
{
    internal class Main
    {
        public IDevice[] Devices
        {
            get
            {
                return lstDevices.ToArray();
            }
        }
        private List<IDevice> lstDevices;        
        public Main()
        {

        }
        public void AddDevice(String platform, String device_id)
        {
            if (platform.ToLower().Equals("android"))
            {
                clsDevice newDev = new clsDevice(device_id);
                lstDevices.Add(newDev);
            }
        }

        public void RemoveDevice(String device_id)
        {
            foreach (IDevice device in lstDevices)
            {
                if (device.ID.Equals(device_id))
                {
                    lstDevices.Remove(device);
                }
            }
        }
    }
}
