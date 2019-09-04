using System;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public class TestCase
    {
        /// <summary> Is the test case infomation edited  by user.
        ///
        /// </summary>
        public bool Edited = false;

        /// <summary>The order of test case list in the TRS file.
        /// 
        /// </summary>
        public int RowIndex = -1;

        /// <summary>The test result enum list
        /// 
        /// </summary>
        public enum EnumTestResult
        {
            None = 1 << 0,
            P = 1 << 1,
            I = 1 << 2,
            B = 1 << 3,
            F = 1 << 4
        };  //Bit of the test result in a integer.

        public EnumTestResult TestResult = EnumTestResult.None;

        /// <summary>
        /// TestCase name
        /// </summary>
        public String Name = "";
        /// <summary>TestCase ID (Format : VTxx-xxxx)
        /// 
        /// </summary>
        public String ID = "";
        /// <summary>
        /// The keyword releate to the configuation files(xml scripts)
        /// </summary>
        public String ConfigReleatedKeyword = "";

        /// <summary>
        /// Remember the folder name of config files
        /// </summary>
        public String ConfigFloderName = "";

        public String Description = "";

        public String TPSData = "";

        /// <summary>Configuration file list (Script files)
        /// 
        /// </summary>
        public List<String> ConfigFileList;

        public String ResultComment;

        public String FailSR;

        public int BlockReasonID = -1;

        public TestCase()
        {
            ConfigFileList = new List<string>();
        }
    }
}
