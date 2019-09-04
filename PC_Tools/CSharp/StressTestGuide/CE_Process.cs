using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OpenNETCF.Desktop.Communication;
using System.IO;

namespace com.usi.shd1_tools.TestGuide
{
    class CE_Process
    {
        //public static readonly String ceCopyPath = System.AppDomain.CurrentDomain.BaseDirectory + "ce_tool\\cecopy.exe";
        //public static readonly String workingDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "ce_tool";
        private static RAPI rapi = new RAPI();
       // public static int CE_Copy(String sourcePath, String destinationPath)
        //{
        //    //dev:/Application/
        //    int exitcode = -1;
        //    Process psCE_Copy = new Process();
        //    //String adbPath = System.AppDomain.CurrentDomain.BaseDirectory + "adb\\adb.exe";
        //    try
        //    {
        //        psCE_Copy.StartInfo = new ProcessStartInfo(ceCopyPath);
        //        psCE_Copy.StartInfo.WorkingDirectory = workingDirectory;
        //        psCE_Copy.StartInfo.Arguments = "\"" + sourcePath + " \" dev:" + destinationPath;
        //        psCE_Copy.StartInfo.CreateNoWindow = true;
        //        psCE_Copy.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //        psCE_Copy.StartInfo.RedirectStandardOutput = true;
        //        psCE_Copy.StartInfo.UseShellExecute = false;
        //        psCE_Copy.Start();
        //        psCE_Copy.WaitForExit();
        //        exitcode = psCE_Copy.ExitCode;
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (psCE_Copy != null)
        //        {
        //            psCE_Copy.Close();
        //        }
        //    }
        //    return exitcode;
        //}

        public static bool Connected
        {
            get
            {
                if (rapi != null)
                {
                    return rapi.DevicePresent;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void GetFileFormDevice(String SourcePath, String DestinationPath)
        {
            GetFileFormDevice(SourcePath, DestinationPath, true);
        }

        public static void GetFileFormDevice(String SourcePath, String DestinationPath, bool Overwrite)
        {
            if (rapi.DevicePresent)
            {
                if (!rapi.Connected)
                {
                    rapi.Connect();
                }
                FileAttributes attr = File.GetAttributes(DestinationPath);
                if ((attr & FileAttributes.Directory).Equals(FileAttributes.Directory))   //DestinationPath is a directory
                {
                    DestinationPath = Path.Combine(DestinationPath , Path.GetFileName(SourcePath));
                }

                rapi.CopyFileFromDevice(DestinationPath, SourcePath, Overwrite);
                if (rapi != null && rapi.Connected)
                {
                    rapi.Disconnect();
                }
            }
        }

        
        public static void CopyFileToDevice(String SourcePath, String DestinationPath, bool Overwrite)
        {
            try
            {
                if (rapi.DevicePresent)
                {
                    if (!rapi.Connected)
                    {
                        rapi.Connect();
                    }
                    try
                    {
                        RAPI.RAPIFileAttributes rattr = rapi.GetDeviceFileAttributes(DestinationPath);
                        if ((rattr & RAPI.RAPIFileAttributes.Directory).Equals(RAPI.RAPIFileAttributes.Directory))   //if destination path exist and it's a directory
                        {
                            FileAttributes attr = File.GetAttributes(SourcePath);
                            if (!(attr & FileAttributes.Directory).Equals(FileAttributes.Directory)) //if source path is not a directory.
                            {
                                DestinationPath = DestinationPath.TrimEnd('\\') + '\\' + Path.GetFileName(SourcePath); //Append the file name after the destination directory 
                            }
                        }
                    }
                    catch
                    {
                        //if destination path is a non-existing file, it's a normal situation, do nothing.
                    }
                    rapi.CopyFileToDevice(SourcePath, DestinationPath, Overwrite);
                    if (rapi != null && rapi.Connected)
                    {
                        rapi.Disconnect();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + " ; \r\n" + ex.StackTrace);
            }
        }

        public static List<String> GetFileList(String sourceFolder, String searchingCriteria)
        {
            List<String> lstFiles = new List<string>();
            if (rapi.DevicePresent)
            {
                if (!rapi.Connected)
                {
                    rapi.Connect();
                }
                IEnumerable<FileInformation> files = rapi.EnumerateFiles(Path.Combine(sourceFolder, searchingCriteria));
                foreach (var file in files)
                {
                    lstFiles.Add(Path.Combine(sourceFolder, file.FileName));
                }
                if (rapi != null && rapi.Connected)
                {
                    rapi.Disconnect();
                }
            }
            return lstFiles;
        }

        public static SYSTEM_INFO GetSystemInfo()
        {
            SYSTEM_INFO sys_info;
            rapi.GetDeviceSystemInfo(out sys_info);
            return sys_info;
        }

        //public static extern IntPtr CeCreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);  
        //        private static void GetFileFromDevice(String SourcePath, String DestinationPath, bool Overwrite)
        //        {
        //            this.CheckConnection();  
        //    IntPtr zero = IntPtr.Zero;  
        //    int lpNumberOfbytesRead = 0;  
        //    byte[] lpBuffer = new byte[0x1000];  
        //    zero = CeCreateFile(DestinationPath, 0x80000000, 0, 0, 3, 0x80, 0);  
        //    if (((int) zero) == -1)  
        //    {  
        //        throw new RAPIException("Could not open remote file");  
        //    }  
        //    FileStream stream = new FileStream(SourcePath, Overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write);  
        //    CeReadFile(zero, lpBuffer, 0x1000, ref lpNumberOfbytesRead, 0);  
        //    while (lpNumberOfbytesRead > 0)  
        //    {  
        //        stream.Write(lpBuffer, 0, lpNumberOfbytesRead);  
        //        if (!Convert.ToBoolean(CeReadFile(zero, lpBuffer, 0x1000, ref lpNumberOfbytesRead, 0)))  
        //        {  
        //            CeCloseHandle(zero);  
        //            stream.Close();  
        //            throw new RAPIException("Failed to read device data");  
        //        }  
        //    }  
        //    CeCloseHandle(zero);  
        //    stream.Flush();  
        //    stream.Close();  
        //}
    }
}
