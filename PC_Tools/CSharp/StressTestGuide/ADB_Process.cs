using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace com.usi.shd1_tools.TestGuide
{
    public class ADB_Process
    {
        public static readonly String adbPath = System.AppDomain.CurrentDomain.BaseDirectory + "adb\\adb.exe";
        public static readonly String workingDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "adb";

        public static void startADB()
        {
            Process psADB = new Process();
            try
            {
                psADB.StartInfo = new ProcessStartInfo(adbPath);
                psADB.StartInfo.WorkingDirectory = workingDirectory;
                psADB.StartInfo.Arguments = "start-server";
                psADB.StartInfo.CreateNoWindow = true;
                psADB.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                psADB.StartInfo.RedirectStandardOutput = true;
                psADB.StartInfo.UseShellExecute = false;
                psADB.Start();
            }
            catch
            {
            }
            finally
            {
                if (psADB != null)
                {
                    psADB.Close();
                }
            }
        }
        public static List<Device_Infomation> GetDeivcesList()
        {
            List<Device_Infomation> lstDeiviceList = new List<Device_Infomation>();
            Process psADB = new Process();
            //String adbPath = System.AppDomain.CurrentDomain.BaseDirectory + "adb\\adb.exe";
            try
            {
                psADB.StartInfo = new ProcessStartInfo(adbPath);
                psADB.StartInfo.WorkingDirectory = workingDirectory;
                psADB.StartInfo.Arguments = "devices";
                psADB.StartInfo.CreateNoWindow = true;
                psADB.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                psADB.StartInfo.RedirectStandardOutput = true;
                psADB.StartInfo.UseShellExecute = false;
                psADB.Start();
                String str = psADB.StandardOutput.ReadToEnd().TrimEnd(new char[] { '\r', '\n' }).Replace("\r\n", "\n");
                String[] strReturnInfos = str.Split('\n');
                foreach (String info in strReturnInfos)
                {
                    if (info.Contains("\t") && info.ToLower().Contains("device")) //get the online device
                    {
                        String id = info.Split('\t')[0];
                        String status = "Connected";
                        lstDeiviceList.Add(new Device_Infomation(id,status));
                    }
                    else if (info.Contains("\t") && info.ToLower().Contains("offline")) //get the offline device
                    {
                        String id = info.Split('\t')[0];
                        String status = "Offline";
                        lstDeiviceList.Add(new Device_Infomation(id, status));
                    }
                }
                psADB.WaitForExit();
            }
            catch
            {
            }
            finally
            {
                if (psADB != null)
                {
                    psADB.Close();
                }
            }
            return lstDeiviceList;
        }
        public static void ADB_Reboot()
        {
            int deviceCount = GetDeivcesList().Count();
            if (deviceCount <= 0)
            {
                MessageBox.Show("There is no device");
            }
            else if (deviceCount > 1)
            {
                MessageBox.Show("There are more than one devices.");
            }
            else
            {
                if (MessageBox.Show("Reboot the device?", "Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    Process psADB = new Process();
                    try
                    {
                        psADB.StartInfo = new ProcessStartInfo(adbPath);
                        psADB.StartInfo.WorkingDirectory = workingDirectory;
                        psADB.StartInfo.Arguments = "reboot";
                        psADB.StartInfo.CreateNoWindow = true;
                        psADB.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        psADB.StartInfo.RedirectStandardOutput = true;
                        psADB.StartInfo.UseShellExecute = false;
                        psADB.Start();
                    }
                    catch
                    {

                    }
                    finally
                    {
                        if (psADB != null)
                        {
                            psADB.Close();
                        }
                    }
                }
            }
        }

        public static int RunAdbCommand(String argument, ref String standardOutput, ref String standardError, bool waitForExit)
        {
            int exitCode = -1;
            Process psADB = new Process();
            try
            {
                psADB.StartInfo = new ProcessStartInfo(adbPath);
                psADB.StartInfo.WorkingDirectory = workingDirectory;
                psADB.StartInfo.Arguments = argument;
                psADB.StartInfo.CreateNoWindow = true;
                psADB.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                psADB.StartInfo.RedirectStandardOutput = true;
                psADB.StartInfo.RedirectStandardError = true;
                psADB.StartInfo.UseShellExecute = false;
                psADB.Start();
                if (waitForExit)
                {
                    psADB.WaitForExit();
                }
                if (standardOutput != null)
                {
                    standardOutput = psADB.StandardOutput.ReadToEnd();
                }
                if (standardError != null)
                {
                    standardError = psADB.StandardError.ReadToEnd();
                }
                exitCode = psADB.ExitCode;
            }
            catch
            {

            }
            finally
            {
                if (psADB != null)
                {
                    psADB.Close();
                }
            }
            return exitCode;
        }
        public static int RunAdbCommand(String argument, ref String standardOutput, ref String standardError)
        {
            return RunAdbCommand(argument, ref  standardOutput, ref  standardError, true);
        }

        public static int RunAdbCommand(String argument, ref String standardOuput, bool waitForExit)
        {
            String error = null;
            return RunAdbCommand(argument, ref standardOuput, ref error);
        }
        public static int RunAdbCommand(String argument, ref String standardOuput)
        {
            return RunAdbCommand( argument, ref  standardOuput,true);
        }

        public static int RunAdbCommand(String argument,bool waitForExit)
        {
            String output=null, error = null;
            return RunAdbCommand(argument, ref output, ref error, waitForExit);
        }
        public static int RunAdbCommand(String argument)
        {
            return RunAdbCommand(argument, true);
        }

        public static List<String> GetPackagesList()
        {
            return GetPackagesList("");
        }

        public static List<String> GetPackagesList(String keyword)
        {
            List<String> packages = new List<string>();
            String argument = "shell \"pm list packages\"";
            String returnedString = "";
            if (keyword != null && keyword.Length > 0)
            {
                argument = argument.TrimEnd('\"') + " | grep " + keyword +"\"";
            }

            Process psADB = new Process();
            try
            {
                psADB.StartInfo = new ProcessStartInfo(adbPath);
                psADB.StartInfo.WorkingDirectory = workingDirectory;
                psADB.StartInfo.Arguments = argument;
                psADB.StartInfo.CreateNoWindow = true;
                psADB.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                psADB.StartInfo.RedirectStandardOutput = true;
                psADB.StartInfo.UseShellExecute = false;
                psADB.Start();
                //psADB.WaitForExit();
                returnedString = psADB.StandardOutput.ReadToEnd();
                packages.AddRange(returnedString.Replace("\r","").TrimEnd('\n').Split('\n'));
                if (packages[packages.Count - 1].Length == 0)
                {
                    packages.RemoveAt(packages.Count - 1);
                }
            }
            catch
            {
            }
            finally
            {
                if (psADB != null)
                {
                    psADB.Close();
                }
            }

            return packages;
        }
    }
}
