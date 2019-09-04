using System;
using System.Collections.Generic;
using System.Text;

namespace com.usi.shd1_tools.TestcasePackage
{
    public class TestCaseInfo
    {
        public String DocumentTitle = "";
        public String TCID = "";
        public String Name = "";
        public String Procedure = "";
        public String Prerequisite = "";
        public String Description = "";
        public String ExpectedResult = "";
        public int Loop = 0;
        public int PassingCriteria = 0;
        /// <summary>The test result enum list
        /// 
        /// </summary>
        public enum TestResults
        {
            None = 1 << 0,
            P = 1 << 1,
            I = 1 << 2,
            B = 1 << 3,
            F = 1 << 4
        };  //Bit of the test result in a integer.
        public TestResults TestResult = TestResults.None;
        public String ResultComment;
        public String FailSR;
        public int BlockReasonID = -1;
        public bool IsSelected = false;
        private Dictionary<String, object> propertiesDictionary = new Dictionary<string, object>();


        public TestCaseInfo()
        {
        }

        public TestCaseInfo(String tcid, String name, String prerequisite, String procedure, String expectedResult, int loop, int passingCriteria)
        {
            TCID = tcid;
            Name = name;
            Prerequisite = prerequisite;
            Procedure = procedure;
            ExpectedResult = expectedResult;
            Loop = loop;
            PassingCriteria = passingCriteria;
        }

        public void SetProperty(String Key, Object Value)
        {
            propertiesDictionary.Add(Key, Value);
        }

        public Object GetProperty(String Key)
        {
            if (propertiesDictionary.ContainsKey(Key))
            {
                return propertiesDictionary[Key];
            }
            else
            {
                return null;
            }
        }

        public void ClearProperties()
        {
            propertiesDictionary.Clear();
        }
    }
}
