using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools.TestcasePackage;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class ucDutTelephonyAutomation : UserControl
    {
        private frmMain mainForm;
        private List<AdbDeviceInfomation> devList = new List<AdbDeviceInfomation>();
        AdbDeviceInfomation dev1;
        AdbDeviceInfomation dev2;
        Device device1, device2;
        //dutController dut1;
        //dutController dut2;
        //ProcedureProcessor_DUT procedure;
        delegate void delGenernal(object sender, object param1);
        public ucDutTelephonyAutomation(frmMain main)
        {
            mainForm = main;
            mainForm.deviceListChangedEventHandler+=new EventHandler<DeviceListChangedEventArgs>(deviceListChangedEventHandler);
            InitializeComponent();
            deviceStatusChanged();
        }
        #region Testcases' description
        private List<String> testcaseDescription = new List<String>(new String[]{
        @"DUT1 calls DUT2 and DUT1 hangs up. 
        Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).  
        1. DUT1 calls DUT2, and DUT2 picks up
        2. Check DUT2's call-status
        3. DUT1 hangs up.
        4. Check DUT2's call-status",
        
        @"DUT1 calls DUT2 and DUT2 hangs up .
        Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
        1. DUT1 calls DUT2, and DUT2 picks up
        2. Check DUT2's call-status
        3. DUT2 hangs up.
        4. Check DUT1's call-status",
        
        @"DUT2 calls DUT1 and DUT1 hangs up
        Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
        1. DUT2 calls DUT1, and DUT1 picks up
        2. Check DUT2's call-status
        3. DUT1 hangs up.
        4. Check DUT2's call-status",
        
        @"DUT2 calls DUT1, and DUT2 hangs up
        Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
        1. DUT2 calls DUT1, DUT1 picks up
        2. Check DUT1's call-status
        3. DUT2 hangs up.
        4. Check DUT1's call-status",

        @"DUT1 calls DUT2, and DUT1 cancels
        Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
        1. DUT1 calls DUT2
        2. Check DUT2's call-status
        3. DUT1 cancels the call.
        4. Check DUT2's call-status",

        @"DUT1 calls DUT2, and DUT2 rejects
        Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
        1. DUT1 calls DUT2
        2. Check DUT2's call-status
        3. DUT2 rejects the phone call.
        4. Check DUT1's call-status"
        });
        #endregion Testcases' description

        private void deviceListChangedEventHandler(object sender,DeviceListChangedEventArgs e)
        {
            devList = e.DeviceList;
            deviceStatusChanged();
        }

        bool dutAutoSelecting = false;
        delegate void delVoidNoParam();
        private void deviceStatusChanged()
        {
            if (this.InvokeRequired)
            {
                delVoidNoParam del = new delVoidNoParam(deviceStatusChanged);
                this.Invoke(del);
            }
            else
            {
                dutAutoSelecting = true;
                cmbDUT1.Items.Clear();
                cmbDUT2.Items.Clear();
                foreach (AdbDeviceInfomation dev in devList)
                {
                    cmbDUT1.Items.Add(dev.ID);
                    cmbDUT2.Items.Add(dev.ID);
                }
                if (dev1 != null)
                {
                    cmbDUT1.SelectedItem = dev1.ID;
                    if (dev1.ConnectingStatus.Equals("Connected"))
                    {
                        txtDut1ConnectStatus.BackColor = System.Drawing.Color.LightCoral;
                        txtDut1ConnectStatus.ForeColor = System.Drawing.Color.White;
                    }
                    else if (dev1.ConnectingStatus.Equals("Ready"))
                    {
                        txtDut1ConnectStatus.BackColor = System.Drawing.Color.Green;
                        txtDut1ConnectStatus.ForeColor = System.Drawing.Color.White;
                    }
                    else if (dev1.ConnectingStatus.Equals("Initializing"))
                    {
                        txtDut1ConnectStatus.BackColor = System.Drawing.Color.YellowGreen;
                        txtDut1ConnectStatus.ForeColor = System.Drawing.Color.White;
                    }
                }
                if (cmbDUT1.SelectedItem == null)
                {
                    dev1 = null;
                    txtDut1ConnectStatus.BackColor = System.Drawing.SystemColors.Control;
                    txtDut1ConnectStatus.Text = "";
                }
                if (dev2 != null)
                {
                    cmbDUT2.SelectedItem = dev2.ID;
                    if (dev2.ConnectingStatus.Equals("Connected"))
                    {
                        txtDut2ConnectStatus.BackColor = System.Drawing.Color.LightCoral;
                        txtDut2ConnectStatus.ForeColor = System.Drawing.Color.White;
                    }
                    else if (dev2.ConnectingStatus.Equals("Ready"))
                    {
                        txtDut2ConnectStatus.BackColor = System.Drawing.Color.Green;
                        txtDut2ConnectStatus.ForeColor = System.Drawing.Color.White;
                    }
                    else if (dev2.ConnectingStatus.Equals("Initializing"))
                    {
                        txtDut2ConnectStatus.BackColor = System.Drawing.Color.YellowGreen;
                        txtDut2ConnectStatus.ForeColor = System.Drawing.Color.White;
                    }
                }
                if (cmbDUT2.SelectedItem == null)
                {
                    dev2 = null;
                    txtDut2ConnectStatus.BackColor = System.Drawing.SystemColors.Control;
                    txtDut2ConnectStatus.Text = "";
                }
                dutAutoSelecting = false;
            }
        }

        private void cmbDUT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!dutAutoSelecting)
            {
                if (cmbDUT1.SelectedIndex >= 0)
                {
                    AdbDeviceInfomation devSelected = devList[cmbDUT1.SelectedIndex];
                    if (dev2 == null || !devSelected.ID.Equals(dev2.ID))
                    {
                        txtDut1PhoneState.Text = "Phone number???";
                        dev1 = devSelected;
                        dut1 = new dutController(dev1.ID);
                        dut1.RefreshPhoneNumberByAPI();
                        if (dut1.PhoneNumber != null && dut1.PhoneNumber.Length > 0)
                        {
                            txtDut1PhoneState.Text = dut1.PhoneNumber;
                        }
                    }
                    else
                    {
                        dev1 = null;
                        dutAutoSelecting = true;
                        cmbDUT1.SelectedItem = null;
                        dutAutoSelecting = false;
                        MessageBox.Show(devSelected.ID + " is already selected");
                        return;
                    }
                }
            }

            if (dev1 != null)
            {
                dut1.DutPhoneStateChangedEventHandler += new EventHandler<DutPhoneStateChangedEventArgs>(dutPhoneStateChangedEventHandler);
                txtDut1ConnectStatus.Text = dev1.ConnectingStatus;
                if (dev1.ConnectingStatus.Equals("Connected"))
                {
                    txtDut1ConnectStatus.BackColor = System.Drawing.Color.LightCoral;
                    txtDut1ConnectStatus.ForeColor = System.Drawing.Color.White;
                }
                else if (dev1.ConnectingStatus.Equals("Ready"))
                {
                    txtDut1ConnectStatus.BackColor = System.Drawing.Color.Green;
                    txtDut1ConnectStatus.ForeColor = System.Drawing.Color.White;
                }
                else if (dev1.ConnectingStatus.Equals("Initializing"))
                {
                    txtDut1ConnectStatus.BackColor = System.Drawing.Color.YellowGreen;
                    txtDut1ConnectStatus.ForeColor = System.Drawing.Color.White;
                }
            }
            else
            {
                txtDut1ConnectStatus.BackColor = System.Drawing.SystemColors.Control;
                txtDut1ConnectStatus.Text = "";
            }
        }

        private void cmbDUT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!dutAutoSelecting)
            {
                if (cmbDUT2.SelectedIndex >= 0)
                {
                    AdbDeviceInfomation devSelected = devList[cmbDUT2.SelectedIndex];
                    if (dev1 == null || !devSelected.ID.Equals(dev1.ID))
                    {
                        txtDut2PhoneState.Text = "Phone number???";
                        dev2 = devSelected;
                        dut2 = new dutController(dev2.ID);
                        dut2.RefreshPhoneNumberByAPI();
                        if (dut2.PhoneNumber != null && dut2.PhoneNumber.Length > 0)
                        {
                            txtDut2PhoneState.Text = dut2.PhoneNumber;
                        }
                    }
                    else
                    {
                        dev2 = null;
                        dutAutoSelecting = true;
                        cmbDUT2.SelectedItem = null;
                        dutAutoSelecting = false;
                        MessageBox.Show(devSelected.ID + " is already selected");
                        return;
                    }
                }
            }
            if (dev2 != null)
            {
                dut2.DutPhoneStateChangedEventHandler += new EventHandler<DutPhoneStateChangedEventArgs>(dutPhoneStateChangedEventHandler);
                txtDut2ConnectStatus.Text = dev2.ConnectingStatus;
                if (dev2.ConnectingStatus.Equals("Connected"))
                {
                    txtDut2ConnectStatus.BackColor = System.Drawing.Color.LightCoral;
                    txtDut2ConnectStatus.ForeColor = System.Drawing.Color.White;
                }
                else if (dev2.ConnectingStatus.Equals("Ready"))
                {
                    txtDut2ConnectStatus.BackColor = System.Drawing.Color.Green;
                    txtDut2ConnectStatus.ForeColor = System.Drawing.Color.White;
                }
                else if (dev2.ConnectingStatus.Equals("Initializing"))
                {
                    txtDut2ConnectStatus.BackColor = System.Drawing.Color.YellowGreen;
                    txtDut2ConnectStatus.ForeColor = System.Drawing.Color.White;
                }
            }
            else
            {
                txtDut2ConnectStatus.BackColor = System.Drawing.SystemColors.Control;
                txtDut2ConnectStatus.Text = "";
            }
        }

        private void lsvTestcases_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                txtTcDescripition.Text = testcaseDescription[e.ItemIndex];
            }
        }

        private void dutPhoneStateChangedEventHandler(object sender, DutPhoneStateChangedEventArgs pscea)
        {
            dutPhoneStateChanged(sender, pscea);
        }
        private void dutPhoneStateChanged(object sender, DutPhoneStateChangedEventArgs param1)
        {
            if (this.InvokeRequired)
            {
                delGenernal del = new delGenernal(dutPhoneStateChanged);
                this.Invoke(del, sender, param1);
            }
            else
            {
                dutController dut = sender as dutController;
                DutPhoneStateChangedEventArgs pscea = param1 as DutPhoneStateChangedEventArgs;
                TextBox txtBox;
                if (dut.Equals(dut1))
                {
                    txtBox = txtDut1PhoneState;
                }
                else
                {
                    txtBox = txtDut2PhoneState;
                }
                txtBox.Text = pscea.State.ToString();
                txtBox.ForeColor = System.Drawing.Color.White;
                switch (pscea.State)
                {
                    case dutController.DutPhoneState.Idle:
                        txtBox.BackColor = System.Drawing.Color.LimeGreen;
                        break;
                    case dutController.DutPhoneState.Rejected:
                        txtBox.BackColor = System.Drawing.Color.Purple;
                        break;
                    case dutController.DutPhoneState.Answered:
                    case dutController.DutPhoneState.Connected:
                        txtBox.BackColor = System.Drawing.Color.Crimson;
                        break;
                    case dutController.DutPhoneState.Dialing:
                        txtBox.BackColor = System.Drawing.Color.Orange;
                        break;
                    case dutController.DutPhoneState.EndCall:
                        txtBox.BackColor = System.Drawing.Color.Green;
                        break;
                    case dutController.DutPhoneState.Offhook:
                        txtBox.BackColor = System.Drawing.Color.Red;
                        break;
                    case dutController.DutPhoneState.Ringing:
                        txtBox.BackColor = System.Drawing.Color.OrangeRed;
                        break;
                    case dutController.DutPhoneState.Unknow:
                    default:
                        txtBox.BackColor = System.Drawing.Color.DarkGray;
                        break;

                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtDut1PhoneState.Text.Length == 0 || txtDut1PhoneState.Text == "Phone number???" ||
                txtDut2PhoneState.Text.Length == 0 || txtDut2PhoneState.Text == "Phone number???")
            {
                MessageBox.Show("Please enter the phone number first.");
            }
            else
            {
                if (procedure == null)
                {
                    procedure = new ProcedureProcessor_DUT(mainForm);
                }
                dut1.PhoneNumber = txtDut1PhoneState.Text;
                dut2.PhoneNumber = txtDut2PhoneState.Text;
                if (procedure.isRunning)
                {
                    procedure.Stop();
                    txtDut1PhoneState.ReadOnly = false;
                    txtDut2PhoneState.ReadOnly = false;
                }
                else
                {
                    procedure.Run(lsvTestcases.CheckedIndices, dut1, dut2);
                    txtDut1PhoneState.ReadOnly = true;
                    txtDut2PhoneState.ReadOnly = true;
                }
            }
        }

        private void txtDutPhoneState_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text == "Phone number???")
            {
                txt.Text = "";
            }
        }

        private void txtDutPhoneState_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text.Trim().Length == 0)
            {
                txt.Text = "Phone number???";
            }
        }

        private void txtDutPhoneState_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Equals("Phone number???"))
            {
                txtBox.ForeColor = Color.Crimson;
            }
            else
            {
                txtBox.ForeColor = Color.Black;
            }
        }
    }
}
