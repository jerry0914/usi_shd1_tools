using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools._8960Library;
using System.Xml.Linq;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class frmStationEmulatorInitialization : Form
    {
        private static frmStationEmulatorInitialization me;
        StationEmulator_8960 se8960;
        ProcedureProcessor_8960 process_8960;
        //private int defaultDialTimeout = 10000;
        //private int cellModifyDelay = 75000;
        //private double defaultCellPower = -50;
        private String defaultConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + "FreqAmpTables\\default.xml";
        public frmStationEmulatorInitialization()
        {
            InitializeComponent();
        }
        private double[] frequencyArray;
        private double[] amplitudeArray;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedMode">0= GSM/GPRS/WCDMA , 1=TD-SCDMA</param>
        public static void SetSettings(
                                    StationEmulator_8960 se,//IStationEmulatorConnector stationConnector,
                                    int selectedMode,
                                    ProcedureProcessor_8960 process)
        {
            if (me == null || me.IsDisposed)
            {
                me = null;
                me = new frmStationEmulatorInitialization();
            }
            me.readConfig(me.defaultConfigPath);
            me.se8960 = se;
            //me.btnGSM.Enabled = selectedMode == 0 || selectedMode==2;
            //me.btnTD_SCDMA.Enabled = selectedMode == 1;
            me.process_8960 = process;
            me.numModifyDelay.Value = ProcedureProcessor_8960.cellModifyDelay;
            me.numDialTimeout.Value = ProcedureProcessor_8960.defaultDialTimeout;
            me.numCellPower.Value = (int)ProcedureProcessor_8960.defaultCellPower;
            me.numAdjustSignalStrengthCheckInterval.Value = ProcedureProcessor_8960.adjustSignalStrength_CheckSignalStrengthInterval;
            me.numAdjustSignalStrengthInaccuracy.Value = ProcedureProcessor_8960.adjustSignalStrength_Inaccuracy;
            me.numAdjustSignalStrengthModifyDealy.Value = ProcedureProcessor_8960.adjustSignalStrength_ModifyStrengthDelay;
            me.numAdjustSignalStrengthPassingCriteria.Value = ProcedureProcessor_8960.adjustSignalStrength_HitTargetPassingCriteria;
            me.numAdjustSignalStrengthRetryLimit.Value = ProcedureProcessor_8960.adjustSignalStrength_RetryLimit;
            me.numAdjustSignalStrengthTimeout.Value = ProcedureProcessor_8960.adjustSignalStrength_TimeoutInSeconds;
            me.Show();
        }

        private void btnGSM_Click(object sender, EventArgs e)
        {
            se8960.SwitchSystemApplication(_8960_SCPI_Commands.SystemApplicationName_GSM_GPRS_WCDMA);
            //((GPIB_Connector)connector).SwitchSystemApplication(_8960_SCPI_Commands.SystemApplicationName_GSM_GPRS_WCDMA);
        }

        private void btnTD_SCDMA_Click(object sender, EventArgs e)
        {
            se8960.SwitchSystemApplication(_8960_SCPI_Commands.SystemApplicationName_TDSCDMA);
            //((GPIB_Connector)connector).SwitchSystemApplication(_8960_SCPI_Commands.SystemApplicationName_TDSCDMA);
        }

        private void btnOtherAppSet_Click(object sender, EventArgs e)
        {
            if (txtSysAppName.Text.Length > 0)
            {
                se8960.SwitchSystemApplication(txtSysAppName.Text);
                //((GPIB_Connector)connector).SwitchSystemApplication(txtSysAppName.Text);
            }
            else
            {
                MessageBox.Show("Enter the System Application Name of 8960 first, please.");
            }
        }

        private void lvSignalAmplitudeTable_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = lvSignalAmplitudeTable.GetItemAt(e.X, e.Y);
            if (li != null)
            {
                ListViewItem.ListViewSubItem lsi = li.GetSubItemAt(e.X, e.Y);
                if (lsi != null)
                {
                    if (lsi.Text.Equals("+"))
                    {
                        if (frmAddFrequencyAmplitude.SetFrequencyAmplitude().Equals(DialogResult.OK))
                        {
                            ListViewItem liNew = new ListViewItem("x");
                            ListViewItem.ListViewSubItem lsiFreq = new ListViewItem.ListViewSubItem(li, frmAddFrequencyAmplitude.Frequency.ToString("0.00"));
                            ListViewItem.ListViewSubItem lsiAmp = new ListViewItem.ListViewSubItem(li, frmAddFrequencyAmplitude.Amplitude.ToString("0.00"));
                            liNew.SubItems.Add(lsiFreq);
                            liNew.SubItems.Add(lsiAmp);
                            int insertIndex = 0;
                            foreach (ListViewItem liExistItem in lvSignalAmplitudeTable.Items)
                            {
                                ListViewItem.ListViewSubItem lsiExistFreq;
                                try
                                {
                                    lsiExistFreq = liExistItem.SubItems[1];
                                }
                                catch
                                {
                                    break;
                                }
                                if (lsiExistFreq == null)
                                {
                                    break;
                                }
                                else
                                {
                                    double freq = Convert.ToDouble(lsiExistFreq.Text);
                                    if (freq > frmAddFrequencyAmplitude.Frequency)
                                    {
                                        break;
                                    }
                                }
                                insertIndex++;
                            }
                            lvSignalAmplitudeTable.Items.Insert(insertIndex, liNew);
                        }
                    }
                    else if (lsi.Text.Equals("x"))
                    {
                        lvSignalAmplitudeTable.Items.RemoveAt(li.Index);
                    }
                    else
                    {
                        try
                        {
                            double frequency = Convert.ToDouble(li.SubItems["chFrequency"].Text);
                            double amplitude = Convert.ToDouble(li.SubItems["chAmplitude"].Text);
                            if (frmAddFrequencyAmplitude.SetFrequencyAmplitude(frequency, amplitude).Equals(DialogResult.OK))
                            {
                                li.SubItems["chFrequency"].Text = frmAddFrequencyAmplitude.Frequency.ToString("0.0");
                                li.SubItems["chAmplitude"].Text = frmAddFrequencyAmplitude.Amplitude.ToString("0.0");
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        
        private void readConfig(String configPath)
        {
            try
            {
                XElement xeConfig = XElement.Load(configPath);
                if (xeConfig != null)
                {
                    lvSignalAmplitudeTable.Items.Clear();
                    foreach (XElement xeAmp in xeConfig.Elements("Amplitude"))
                    {
                        try
                        {
                            ListViewItem li = new ListViewItem("x");
                            double freq = Convert.ToDouble(xeAmp.Attribute("Frequency").Value);
                            double ampl = Convert.ToDouble(xeAmp.Value);
                            ListViewItem.ListViewSubItem lsiFreq = new ListViewItem.ListViewSubItem(li, freq.ToString("0.00"));
                            ListViewItem.ListViewSubItem lsiAmp = new ListViewItem.ListViewSubItem(li, ampl.ToString("0.00"));
                            li.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { lsiFreq, lsiAmp });
                            lvSignalAmplitudeTable.Items.Add(li);                        
                        }
                        catch
                        {
                        }
                    }
                    lvSignalAmplitudeTable.Items.Add(new ListViewItem("+"));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        private void saveConfig(String configPath)
        {
            XElement xeConfig = new XElement("root");
            foreach (ListViewItem li in lvSignalAmplitudeTable.Items)
            {
                if (li.SubItems.Count >= 3)
                {
                    XElement xeAmp = new XElement("Amplitude");
                    xeAmp.SetAttributeValue("Frequency", li.SubItems[1].Text);
                    xeAmp.Value = li.SubItems[2].Text;
                    xeConfig.Add(xeAmp);
                }
            }
            xeConfig.Save(configPath);
        }

        private void getFrequencyAndAmplitudeArray()
        {
            int dataLength = lvSignalAmplitudeTable.Items.Count-1;
            frequencyArray = new double[dataLength];
            amplitudeArray = new double[dataLength];
            for (int index = 0; index < dataLength; index++)
            {
                try
                {
                    frequencyArray[index] = Convert.ToDouble(lvSignalAmplitudeTable.Items[index].SubItems[1].Text);
                    amplitudeArray[index] = Convert.ToDouble(lvSignalAmplitudeTable.Items[index].SubItems[2].Text);
                }
                catch
                {
                }
            }
        }

        private void btnSignalAmpSet_Click(object sender, EventArgs e)
        {            
            if (se8960 != null)
            {
                getFrequencyAndAmplitudeArray();
                se8960.SetAmplitudeOffsets(frequencyArray, amplitudeArray);
                //((GPIB_Connector)connector).SetAmplitudeOffsets(frequencyArray, amplitudeArray);
            }
            saveConfig(defaultConfigPath);
            this.DialogResult = MessageBox.Show("Done");
        }

        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory+"FreqAmpTables\\";
            if (DialogResult.OK.Equals(openFileDialog1.ShowDialog()))
            {
                readConfig(openFileDialog1.FileName);
            }
        }

        private void btnSignalAmpSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "FreqAmpTables\\";
            if (DialogResult.OK.Equals(saveFileDialog1.ShowDialog()))
            {
                saveConfig(saveFileDialog1.FileName);
            }
        }

        private void btnSetTcSettings_Click(object sender, EventArgs e)
        {
            ProcedureProcessor_8960.defaultCellPower = Convert.ToDouble(numCellPower.Value);
            ProcedureProcessor_8960.cellModifyDelay = Convert.ToInt32(numModifyDelay.Value);
            ProcedureProcessor_8960.defaultDialTimeout = Convert.ToInt32(numDialTimeout.Value);
            // ====  for signal strength adjust
            ProcedureProcessor_8960.adjustSignalStrength_CheckSignalStrengthInterval = Convert.ToInt32(numAdjustSignalStrengthCheckInterval.Value);
            ProcedureProcessor_8960.adjustSignalStrength_Inaccuracy = Convert.ToInt32(numAdjustSignalStrengthInaccuracy.Value);
            ProcedureProcessor_8960.adjustSignalStrength_HitTargetPassingCriteria = Convert.ToInt32(numAdjustSignalStrengthPassingCriteria.Value);
            ProcedureProcessor_8960.adjustSignalStrength_RetryLimit = Convert.ToInt32(numAdjustSignalStrengthRetryLimit.Value);
            ProcedureProcessor_8960.adjustSignalStrength_TimeoutInSeconds = Convert.ToInt32(numAdjustSignalStrengthTimeout.Value);
            ProcedureProcessor_8960.adjustSignalStrength_ModifyStrengthDelay = Convert.ToInt32(numAdjustSignalStrengthModifyDealy.Value);
            SaveTestCaseSettings(ProcedureProcessor_8960.defaultCellPower, ProcedureProcessor_8960.cellModifyDelay, ProcedureProcessor_8960.defaultDialTimeout,
                                 ProcedureProcessor_8960.adjustSignalStrength_HitTargetPassingCriteria,
                                 ProcedureProcessor_8960.adjustSignalStrength_RetryLimit,
                                 ProcedureProcessor_8960.adjustSignalStrength_ModifyStrengthDelay,
                                 ProcedureProcessor_8960.adjustSignalStrength_Inaccuracy,
                                 ProcedureProcessor_8960.adjustSignalStrength_CheckSignalStrengthInterval,
                                 ProcedureProcessor_8960.adjustSignalStrength_TimeoutInSeconds);
            this.Close();
        }

        /*public void ReadTestCaseSettings()
        {
            XElement xeConfig = null;
            try
            {
                xeConfig = XElement.Load(ProcedureProcessor_8960.pathConfiguration);
                if (xeConfig != null)
                {
                    try
                    {
                        defaultCellPower = Convert.ToDouble(xeConfig.Element("DefaultCellPower").Value);
                        if(process_8960!=null)
                        {
                            process_8960.defaultCellPower = defaultCellPower;
                        }
                    }
                    catch { }
                    try
                    {
                        cellModifyDelay = Convert.ToInt32(xeConfig.Element("CellModifyDelay").Value);
                        if (process_8960 != null)
                        {
                            process_8960.cellModifyDelay = cellModifyDelay;
                        }
                    }
                    catch { }
                    try
                    {
                        defaultDialTimeout = Convert.ToInt32(xeConfig.Element("DefaultDialTimeout").Value);
                        if (process_8960 != null)
                        {
                            process_8960.defaultDialTimeout = defaultDialTimeout;
                        }
                    }
                    catch { }
                }
            }
            catch
            { }
        }*/

        public void SaveTestCaseSettings(double cellPower, int cellModifyDelay, int dialTimeout, 
                                         int adjust_PassingCriteria,
                                         int adjust_RetryLimit,
                                         int adjust_ModifyDelay,
                                         int adjust_Inaccuracy,          
                                         int adjust_CheckInterval,
                                         int adjust_Timeout)
        {
            XElement xeConfig = new XElement("config");
            XElement xeCellPower = new XElement("DefaultCellPower");
            xeCellPower.Value = cellPower.ToString("0.00");
            XElement xeCellModifyDelay = new XElement("CellModifyDelay");
            xeCellModifyDelay.Value = cellModifyDelay.ToString();
            XElement xeDefaultDialTimeout = new XElement("DefaultDialTimeout");
            xeDefaultDialTimeout.Value = dialTimeout.ToString();           
            xeConfig.Add(xeCellPower);
            xeConfig.Add(xeCellModifyDelay);
            xeConfig.Add(xeDefaultDialTimeout);
            XElement xeAdjustDutSignalStrengthParams = new XElement("AdjustDutSignalStrengthParams");
            
            XElement xeAdjustDutSignal_CheckInterval = new XElement("CheckInterval");
            xeAdjustDutSignal_CheckInterval.Value = adjust_CheckInterval.ToString();
            xeAdjustDutSignalStrengthParams.Add(xeAdjustDutSignal_CheckInterval);
            XElement xeAdjustDutSignal_Inaccuracy = new XElement("Inaccuracy");
            xeAdjustDutSignal_Inaccuracy.Value = adjust_Inaccuracy.ToString();
            xeAdjustDutSignalStrengthParams.Add(xeAdjustDutSignal_Inaccuracy);
            XElement xeAdjustDutSignal_HitTargetPassingCriteria = new XElement("HitTargetPassingCriteria");
            xeAdjustDutSignal_HitTargetPassingCriteria.Value = adjust_PassingCriteria.ToString();
            xeAdjustDutSignalStrengthParams.Add(xeAdjustDutSignal_HitTargetPassingCriteria);
            XElement xeAdjustDutSignal_RetryLimit = new XElement("RetryLimit");
            xeAdjustDutSignal_RetryLimit.Value = adjust_RetryLimit.ToString();
            xeAdjustDutSignalStrengthParams.Add(xeAdjustDutSignal_RetryLimit);
            XElement xeAdjustDutSignal_TimeoutInSeconds = new XElement("TimeoutInSeconds");
            xeAdjustDutSignal_TimeoutInSeconds.Value = adjust_Timeout.ToString();
            xeAdjustDutSignalStrengthParams.Add(xeAdjustDutSignal_TimeoutInSeconds);
            XElement xeAdjustDutSignal_ModifyStrengthDelay = new XElement("ModifyStrengthDelay");
            xeAdjustDutSignal_ModifyStrengthDelay.Value = adjust_ModifyDelay.ToString();
            xeAdjustDutSignalStrengthParams.Add(xeAdjustDutSignal_ModifyStrengthDelay);
            xeConfig.Add(xeAdjustDutSignalStrengthParams);
            xeConfig.Save(ProcedureProcessor_8960.pathConfiguration);
        }   
    }
}
