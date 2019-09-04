using System;
using System.Collections.Generic;

namespace com.usi.shd1_tools.TestGuide
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

        public enum NecessaryFile_Category { None = -1, Application = 0, Configuration = 1, Attachment = 2 };

        public struct NecessaryFile_Info
        {
            public NecessaryFile_Category Category;
            public String Name;
            public String FullPath;
            public String Destination;
        }

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
        public String ConfigAssociatedKeyword = "";

        /// <summary>
        /// Remember the folder name of config files
        /// </summary>
        public String ConfigFloderName = "";

        public String Description = "";

        public String Cycle_Group1 = "";

        public String TPSData = "";

        /// <summary>Configuration file list (Script files)
        /// 
        /// </summary>
        public List<NecessaryFile_Info> NecessaryFileList;
       
        public String ResultComment;

        public String FailSR;

        public int BlockReasonID = -1;

        public TestCase()
        {
            NecessaryFileList = new List<NecessaryFile_Info>();
        }
    }
}
