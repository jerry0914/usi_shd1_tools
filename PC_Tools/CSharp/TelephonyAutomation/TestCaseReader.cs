using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using com.usi.shd1_tools.TestcasePackage;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    class TestCaseReader
    {
        private static int testCasesStartRow = 3;
        private static int tcidColumn = 0;
        private static int nameColumn = 1;
        private static int prerequisiteColumn = 2;
        private static int procedureColumn = 3;
        private static int expectedResultColumn = 4;
        private static int loopAndPassingCriteriaColumn = 5;
        //public List<CommonLibrary.TestCaseInfo> TestCaseList = new List<CommonLibrary.TestCaseInfo>();
        public static List<Wwan_TestCaseInfo> GetTestCasesFromExcel(String testCaseListExcelPath, int workSheetIndex)
        {
            List<Wwan_TestCaseInfo> lstTCs = new List<Wwan_TestCaseInfo>();
            int currentRow = testCasesStartRow;
            Workbook workBook1;
            Worksheet workSheet1;
            try
            {
                workBook1 = new Workbook(testCaseListExcelPath);
                workSheet1 = workBook1.Worksheets[workSheetIndex];
                for (; currentRow < workSheet1.Cells.Rows.Count; currentRow++)
                {
                    try
                    {
                        Wwan_TestCaseInfo tcNew = new Wwan_TestCaseInfo();
                        Cell cellTcid = workSheet1.Cells.Rows[currentRow].GetCellOrNull(tcidColumn);
                        Cell cellName = workSheet1.Cells.Rows[currentRow].GetCellOrNull(nameColumn);
                        Cell cellPrerequisite = workSheet1.Cells.Rows[currentRow].GetCellOrNull(prerequisiteColumn);
                        Cell cellProcedure= workSheet1.Cells.Rows[currentRow].GetCellOrNull(procedureColumn);
                        Cell cellExpectedResult = workSheet1.Cells.Rows[currentRow].GetCellOrNull(expectedResultColumn);
                        Cell cellLoopAndCriteria = workSheet1.Cells.Rows[currentRow].GetCellOrNull(loopAndPassingCriteriaColumn);
                        tcNew.TCID = cellTcid==null?"NULL":cellTcid.StringValue;
                        tcNew.Name = cellName == null ? "NULL" : cellName.StringValue;
                        if(tcNew.Name.Contains("SIM2"))
                        {
                            tcNew.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM2;
                        }
                        else
                        {
                            tcNew.CurrentSimSlot = Wwan_TestCaseInfo.SimSlot.SIM1;
                        }

                        if (tcNew.Name.Contains("TD-SCDMA"))
                        {
                            tcNew.CurrentFormat = Wwan_TestCaseInfo.RAT.TD_SCDMA;
                        }
                        else if(tcNew.Name.Contains("UMTS")||tcNew.Name.Contains("WCDMA"))
                        {
                            tcNew.CurrentFormat = Wwan_TestCaseInfo.RAT.WCDMA;
                        }
                        else if (tcNew.Name.Contains("LTE") || tcNew.Name.Contains("4G"))
                        {
                            tcNew.CurrentFormat = Wwan_TestCaseInfo.RAT.LTE;
                        }
                        else
                        {
                            tcNew.CurrentFormat = Wwan_TestCaseInfo.RAT.GSM;
                        }

                        tcNew.Prerequisite = cellPrerequisite == null ? "NULL" : cellPrerequisite.StringValue;
                        tcNew.Procedure = cellProcedure == null ? "NULL" : cellProcedure.StringValue;
                        tcNew.ExpectedResult = cellExpectedResult == null ? "NULL" : cellExpectedResult.StringValue;
                        String temp = cellLoopAndCriteria == null ? "" : cellLoopAndCriteria.StringValue;
                        String[] loopCri = temp.Split('/');
                        if (loopCri.Length == 2)
                        {
                            tcNew.PassingCriteria = Convert.ToInt32(loopCri[0].Trim());
                            tcNew.Loop = Convert.ToInt32(loopCri[1].Trim());
                        }
                        lstTCs.Add(tcNew);
                    }
                    catch
                    {
                        // Ignore the error row and read the next one.
                    }
                }
            }
            catch
            {

            }
            return lstTCs;
        }
    }
}
