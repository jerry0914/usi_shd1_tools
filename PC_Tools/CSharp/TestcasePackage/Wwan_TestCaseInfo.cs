using System;
using System.Collections.Generic;
using System.Text;


namespace com.usi.shd1_tools.TestcasePackage
{
    public class Wwan_TestCaseInfo : TestCaseInfo
    {

        public enum SimSlot {None=-1,SIM1=0,SIM2=1};
        public enum RAT {GSM = 0, WCDMA = 1, TD_SCDMA = 2, LTE = 3 };
        public enum Band
        {
            GSM_850,
            GSM_900,
            GSM_PCS,
            DCS_1800,
            GSM_EGSM,
            UMTS_900,
            UMTS_2100,
            TD_SCDMA_34,
            TD_SCDMA_39,
            GPRS_850,
            GPRS_PCS,
            GPRS_DCS,
            GPRS_EGSM,
            EGPRS_850,
            EGPRS_PCS,
            EGPRS_DCS,
            EGPRS_EGSM,
        };
        public SimSlot CurrentSimSlot = SimSlot.SIM1;
        public RAT CurrentFormat = RAT.GSM;
        public Band CurrentBand = Band.GSM_900;
        public int CellChannel = 0;
        public int TrafficChannel = 0;       
        public Wwan_TestCaseInfo():base(){
            
        }
        public Wwan_TestCaseInfo(String tcid, String name, String prerequisite, String procedure, String expectedResult, int loop, int passingCriteria)
            : base(tcid, name, prerequisite, procedure, expectedResult, loop, passingCriteria)
        {

        }
    }
}
