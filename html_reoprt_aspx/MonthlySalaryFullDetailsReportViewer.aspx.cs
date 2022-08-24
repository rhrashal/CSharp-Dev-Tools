using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayRollMS.DAL;
using System.Data;
using PayRollMS.BLL;
using System.Text;

namespace PayRollMS.Views.Report.Viewers
{
    public partial class MonthlySalaryFullDetailsReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string branchID = Request.QueryString["Branch"];
                    string depID = Request.QueryString["Dep"];
                    int year = int.Parse(Request.QueryString["year"]);
                    int month = int.Parse(Request.QueryString["month"]);
                    string monthName = Request.QueryString["MonthName"];

                    string PayType = Request.QueryString["PayType"];

                    if (branchID == "All")
                    {
                        lblShopName.Text = "Shop : " + branchID;
                    }
                    else
                    {
                        string branchName = new BRANCH_SETUP().Find(branchID).BRANCH_NAME;
                        branchID = decimal.Parse(branchID).ToString("0000");
                        lblShopName.Text = string.Format("Shop : {0} ,{1}", branchID, branchName);
                    }

                    DateTime dtStart = new DateTime(year, month, 1);
                    lblstartDate.Text = dtStart.ToLongDateString();

                    DateTime dtEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    lblEndDate.Text = dtEnd.ToLongDateString();

                    lblWorkDays.Text = new AttendanteTable().WorkDays(branchID, year, month).ToString();

                    lblMonthName.Text = monthName + " " + year.ToString();
                    lblTitle.Text = monthName + " " + year.ToString();
                    // bind table header for allowance reocord
                    List<ALLOWANCE_TYPE_SETUP> oATY = new ALLOWANCE_TYPE_SETUP().SelectAll();
                    List<ManualAllowanceType> mATY = new ManualAllowanceType().SelectAll();
                    List<ManualDeductionType> mDTY = new ManualDeductionType().SelectAll();

                    #region prepare TablHead
                    string head1 = "<th style=\"border-right:1px solid;\">&nbsp;</th> <th style=\"border-right:1px solid;\">&nbsp;</th>  <th style=\"border-right:1px solid;\">&nbsp;</th>  <th style=\"border-right:1px solid;\">&nbsp;</th> <th style=\"border-right:1px solid;\">&nbsp;</th><th style=\"border-right:1px solid;\">&nbsp;</th><th style=\"border-right:1px solid;\">&nbsp;</th>" +
                                    "<th colspan=\"" + (oATY.Count + 9).ToString() + "\" style=\"border-bottom:1px solid; border-right:1px solid;\" >Earnings</th> <th colspan=\"" + (mDTY.Count + 6).ToString() + "\"  style=\"border-bottom:1px solid; border-right:1px solid;\" >Deductions</th>" +
                                    "<th style=\"border-right:1px solid;\">&nbsp;</th> <th style=\"border-right:1px solid;\">&nbsp;</th> <th style=\"border-right:1px solid;\">&nbsp;</th> <th style=\"border-right:1px solid;\">&nbsp;</th> ";
                    LiteralHead1.Text = head1;

                    string head2 = "<th style=\"border-bottom:1px solid; border-right:1px solid;\">SL No.</th><th style=\"border-bottom:1px solid; border-right:1px solid;\">ID</th> <th style=\"border-bottom:1px solid; border-right:1px solid;\">Name</th> <th style=\"border-bottom:1px solid; border-right:1px solid;\">Designation </th> <th style=\"border-bottom:1px solid; border-right:1px solid;\"> DOJ </th> <th style=\"border-bottom:1px solid; border-right:1px solid;\"> DOC </th> <th style=\"border-bottom:1px solid; border-right:1px solid;\"> Present Month Gross </th> "; ;
                    foreach (ALLOWANCE_TYPE_SETUP oAT in oATY)
                    {
                        head2 += "<th style=\"border-bottom:1px solid; border-right:1px solid;\">" + oAT.ALOWANCE_TITLE + " " + oAT.PERCENTAGE + " % </th>";
                    }

                    foreach (ManualAllowanceType m in mATY)
                    {
                        head2 += "<th style=\"border-bottom:1px solid; border-right:1px solid;\">" + m.AllowanceTypeName + " </th>";
                    }

                    head2 += "<th  style=\"border-bottom:1px solid; border-right:1px solid;\">Base Salary</th>      <th style=\"border-bottom:1px solid; border-right:1px solid;\">Additional Bonus</th>     <th style=\"border-bottom:1px solid; border-right:1px solid;\">Other (Special Allowance)</th>  <th  style=\"border-bottom:1px solid; border-right:1px solid;\">Total</th> <th style=\"border-bottom:1px solid; border-right:1px solid;\">Other Deductions</th>  <th style=\"border-bottom:1px solid; border-right:1px solid;\">Tax</th> <th style=\"border-bottom:1px solid; border-right:1px solid;\">Abscent</th> <th style=\"border-bottom:1px solid; border-right:1px solid;\">Less work</th> <th style=\"border-bottom:1px solid; border-right:1px solid;\">PF 2.5%</th>";
                    foreach(ManualDeductionType m in mDTY)
                    {
                        head2 += "<th style=\"border-bottom:1px solid; border-right:1px solid;\">" + m.DeductionTypeName + " </th>";
                    }
                    head2 += "  <th style=\"border-bottom:1px solid; border-right:1px solid;\">Total</th>  <th style=\"border-bottom:1px solid; border-right:1px solid;\">Net Payable</th>  <th style=\"border-bottom:1px solid; border-right:1px solid;\"> Cash Pay </th> <th style=\"border-bottom:1px solid; border-right:1px solid;\"> Bank Pay </th>   <th style=\"border-bottom:1px solid; border-right:1px solid; \">Respective Sallary Account Number</th>  <th style=\"border-bottom:1px solid; \">Remarks</th> ";
                    
                    LiteralHead2.Text = head2;
                    #endregion

                    #region  prepare data

                    /// process heading data
                    DataTable ReportTable = new DataTable();
                    ReportTable.Columns.Add("SLNo.");
                    ReportTable.Columns.Add("ID");
                    ReportTable.Columns.Add("Name");
                    ReportTable.Columns.Add("Designation");
                    ReportTable.Columns.Add("DOJ");
                    ReportTable.Columns.Add("DOC");
                    ReportTable.Columns.Add("PresentMonthGross");

                    foreach (ALLOWANCE_TYPE_SETUP oAT in oATY)
                    {
                        ReportTable.Columns.Add(oAT.ALOWANCE_TITLE + "-" + oAT.ALID);
                    }
                    foreach (ManualAllowanceType m in mATY)
                    {
                        ReportTable.Columns.Add(m.AllowanceTypeName + "-" + m.ManualAllowanceTypeId);
                    }

                    ReportTable.Columns.Add("GrossSalary");
                    //ReportTable.Columns.Add("OverTime");
                    ReportTable.Columns.Add("OtherorBonus");
                    ReportTable.Columns.Add("OtherSpecialAllownace");
                    ReportTable.Columns.Add("TotalIncome");
                    ReportTable.Columns.Add("Other");
                    ReportTable.Columns.Add("Tax");
                    ReportTable.Columns.Add("Abscent");
                    ReportTable.Columns.Add("Late");
                    ReportTable.Columns.Add("PF");
                    foreach (ManualDeductionType m in mDTY)
                    {
                        ReportTable.Columns.Add(m.DeductionTypeName + "-" + m.ManualDeductionTypeId);
                    }
                    ReportTable.Columns.Add("TotalDeduction");
                    ReportTable.Columns.Add("NetPayable");
                    ReportTable.Columns.Add("CashPay");
                    ReportTable.Columns.Add("ForBankPayment");
                    ReportTable.Columns.Add("RespectiveSallaryAccount");
                    ReportTable.Columns.Add("Remarks");
                    // ReportTable.Columns.Add("Grade");

                    // process row data
                    int bid = 0;
                    int.TryParse(branchID, out bid);
                    int depid = 0;
                    int.TryParse(depID, out depid);

                    List<EMPLOYEE_SETUP> oEmpList = null;

                    string query = @"SELECT DISTINCT EID,BID,DID,1 as JobType FROM dbo.SALLARY_SHEET_FULLTIME WHERE YEARS=" + year + " AND MONTHS=" + month + "  AND  (((BID=" + bid + ") and (" + bid + " <>0)) or (" + bid + @"=0))  AND (((DID=" + depid + ") and (" + depid + " <>0)) or (" + depid + @"=0)) 
                                    UNION ALL
                                    SELECT DISTINCT EID,BID,DID,2 AS JobType FROM dbo.SALLARY_SHEET_PARTTIME WHERE YEARS=" + year + " AND MONTHS=" + month + " AND (((BID=" + bid + ") and (" + bid + " <>0)) or (" + bid + "=0))  AND (((DID=" + depid + ") and (" + depid + " <>0)) or (" + depid + "=0))  ";

                    DataTable dtEmp = new TDAL().Select(query).Data;

                    oEmpList = new EMPLOYEE_SETUP().SelectAll();

                    //  List<GRADE_SETUP> gradelist = new GRADE_SETUP().SelectAll();

                    List<EMPLOYEE_SETUP> oEmpListFilterd = new List<EMPLOYEE_SETUP>();
                    for (int i = 0; i < dtEmp.Rows.Count; i++)
                    {
                        int empid = int.Parse(dtEmp.Rows[i]["EID"].ToString());
                        var emp = oEmpList.Find(m => m.EID == empid);
                        emp.BID = int.Parse(dtEmp.Rows[i]["BID"].ToString());
                        emp.DID = int.Parse(dtEmp.Rows[i]["DID"].ToString());
                        emp.JTID = int.Parse(dtEmp.Rows[i]["JobType"].ToString());

                        if (emp.PAYTYPE == PayType || PayType == "All")
                            oEmpListFilterd.Add(emp);
                    }



                    List<ReportData> OReportList = new List<ReportData>();

                    int slno = 1;
                    foreach (EMPLOYEE_SETUP oEmp in oEmpListFilterd)
                    {

                        if (oEmp.vJobType == JOB_TYPE_SETUP.FULL_TIME)
                        {
                            SALLARY_SHEET_FULLTIME oSHF = new SALLARY_SHEET_FULLTIME().Find(year, month, oEmp.EID);
                            if (oSHF == null)
                            {
                                continue;
                            }
                            string[] DataCollection = new string[ReportTable.Columns.Count];
                            DataCollection[0] = slno.ToString();
                            DataCollection[1] = oEmp.CARD_NO;
                            DataCollection[2] = oEmp.EMP_NAME;
                            DataCollection[3] = oEmp.vDesignationName;
                            DataCollection[4] = oEmp.JOIN_DATE.Value.ToString("dd'/'MM'/'yyyy");
                            DataCollection[5] = oEmp.JOIN_DATE.Value.ToString("dd'/'MM'/'yyyy");
                            DataCollection[6] = oEmp.GRADE_AMOUNT.Value.ToString("0.##");

                            int pos = 7;
                            decimal totalAllowance = 0;
                            decimal totalDeduction = 0;
                            foreach (ALLOWANCE_TYPE_SETUP oAT in oATY)
                            {
                                SALLARY_SHEET_FULLTIME_DETAILS oDetails = oSHF.DetailsList.Find(m => m.ALID == oAT.ALID && m.SHFID == oSHF.SHFID);
                                if (oDetails != null)
                                {
                                    DataCollection[pos] = oDetails.AMOUNT.Value.ToString();
                                    totalAllowance += oDetails.AMOUNT.Value;
                                }
                                pos++;
                            }

                            foreach (ManualAllowanceType oAT in mATY)
                            {
                                SALLARY_SHEET_FULLTIME_DETAILS oDetails = oSHF.DetailsList.Find(m => m.ALID == oAT.ManualAllowanceTypeId && m.SHFID == oSHF.SHFID);
                                if (oDetails != null)
                                {
                                    DataCollection[pos] = oDetails.AMOUNT.Value.ToString();
                                    totalAllowance += oDetails.AMOUNT.Value;
                                }
                                else
                                {
                                    DataCollection[pos] = "0.00";
                                }
                                pos++;
                            }

                            DataCollection[pos] = oSHF.GROSS_SALARY.Value.ToString();
                            pos++;

                            //DataCollection[pos] = oSHF.OT_AMOUNT.Value.ToString();
                            //pos++;

                            DataCollection[pos] = oSHF.OTHER_BONUS.Value.ToString();
                            pos++;
                            DataCollection[pos] = oSHF.Additional_Allowance.Value.ToString();
                            pos++;
                            decimal totalIncome = (totalAllowance + oSHF.OT_AMOUNT.Value + oSHF.OTHER_BONUS.Value + oSHF.Additional_Allowance.Value + oSHF.TransportAllowance.Value /*+ oSHF.MedicalAllowance.Value*/);
                            DataCollection[pos] = totalIncome.ToString();
                            pos++;
                            DataCollection[pos] = oSHF.OTHER_DEDUCTION.Value.ToString();
                            pos++;
                            DataCollection[pos] = oSHF.TAX_AMOUNT.Value.ToString();
                            pos++;
                            DataCollection[pos] = oSHF.ABSCENT_AMOUNT.Value.ToString();
                            pos++;
                            DataCollection[pos] = oSHF.LATE_AMOUNT.Value.ToString();

                            pos++;
                            DataCollection[pos] = oSHF.CP_Fund.Value.ToString();

                            foreach (ManualDeductionType oAT in mDTY)
                            {
                                SALLARY_SHEET_FULLTIME_DETAILS oDetails = oSHF.DetailsList.Find(m => m.ALID == oAT.ManualDeductionTypeId && m.SHFID == oSHF.SHFID);
                                if (oDetails != null)
                                {
                                    DataCollection[pos] = oDetails.AMOUNT.Value.ToString();
                                    totalDeduction += oDetails.AMOUNT.Value;
                                }
                                else
                                {
                                    DataCollection[pos] = "0.00";
                                }
                                pos++;
                            }

                            totalDeduction += oSHF.TAX_AMOUNT.Value + oSHF.ABSCENT_AMOUNT.Value + oSHF.LATE_AMOUNT.Value + oSHF.OTHER_DEDUCTION.Value + oSHF.CP_Fund.Value;
                            pos++;
                            DataCollection[pos] = totalDeduction.ToString();

                            pos++;
                            DataCollection[pos] = oSHF.NET_AMOUNT.Value.ToString();

                            pos++;
                            DataCollection[pos] = oSHF.CashAmount.Value.ToString("0");

                            pos++;
                            DataCollection[pos] = oSHF.CardAmount.Value.ToString("0");
                            pos++;
                            DataCollection[pos] = oSHF.ACC_CODE;


                            ReportData oRD = new ReportData();
                            for (int i = 0; i < DataCollection.Length; i++)
                            {
                                if (i + 1 == DataCollection.Length)
                                {
                                    oRD.Data += "<td style=\"border-bottom:1px solid;\"> " + DataCollection[i] + " </td>";
                                }
                                else
                                {
                                    oRD.Data += "<td style=\"border-bottom:1px solid; border-right:1px solid;\"> " + DataCollection[i] + " </td>";
                                }
                            }
                            oRD.DataCollection = DataCollection;
                            OReportList.Add(oRD);
                            slno++;
                        }
                        else if (oEmp.vJobType == JOB_TYPE_SETUP.PART_TIME)
                        {
                            SALLARY_SHEET_PARTTIME oSHF = new SALLARY_SHEET_PARTTIME().Find(year, month, oEmp.EID);
                            if (oSHF == null)
                            {
                                continue;
                            }
                            string[] DataCollection = new string[ReportTable.Columns.Count];
                            DataCollection[0] = slno.ToString();
                            DataCollection[1] = oEmp.CARD_NO;
                            DataCollection[2] = oEmp.EMP_NAME;
                            DataCollection[3] = oEmp.vDesignationName;
                            DataCollection[4] = oEmp.JOIN_DATE.Value.ToString("dd'/'MM'/'yyyy");
                            DataCollection[5] = oEmp.JOIN_DATE.Value.ToString("dd'/'MM'/'yyyy");
                            DataCollection[6] = oEmp.GRADE_AMOUNT.Value.ToString("0.##");

                            int pos = 7;

                            decimal totalAllowance = 0;
                            foreach (ALLOWANCE_TYPE_SETUP oAT in oATY)
                            {
                                if (pos == 2)
                                {
                                    DataCollection[pos] = (oSHF.RATE_PER_HOUR.Value * oSHF.TOTAL_HOUR.Value).ToString("0");
                                    totalAllowance += decimal.Round((oSHF.RATE_PER_HOUR.Value * oSHF.TOTAL_HOUR.Value), 0);
                                }
                                else
                                {
                                    DataCollection[pos] = "0";
                                }
                                pos++;
                            }


                            DataCollection[pos] = totalAllowance.ToString("0");
                            pos++;

                            DataCollection[pos] = oSHF.OVER_TIME_AMNT.Value.ToString();
                            pos++;

                            DataCollection[pos] = "0";
                            pos++;

                            DataCollection[pos] = oSHF.Additional_Allowance.Value.ToString();
                            pos++;

                            decimal totalIncome = (totalAllowance + oSHF.OVER_TIME_AMNT.Value + oSHF.Additional_Allowance.Value);
                            DataCollection[pos] = totalIncome.ToString();
                            pos++;

                            DataCollection[pos] = oSHF.OTHER_DEDUCTION.Value.ToString();
                            pos++;

                            DataCollection[pos] = oEmp.TAX_AMOUNT.Value.ToString();
                            pos++;

                            DataCollection[pos] = "0";
                            pos++;

                            DataCollection[pos] = "0";
                            pos++;

                            DataCollection[pos] = "0"; //pf
                            decimal totalDeduction = (oEmp.TAX_AMOUNT.Value + oSHF.OTHER_DEDUCTION.Value);
                            pos++;

                            DataCollection[pos] = totalDeduction.ToString();
                            pos++;

                            DataCollection[pos] = (totalIncome - totalDeduction).ToString();
                            pos++;

                            DataCollection[pos] = ("0"); // cash pay
                            pos++;

                            DataCollection[pos] = (totalIncome - totalDeduction).ToString("0");
                            pos++;

                            DataCollection[pos] = oSHF.ACC_CODE;

                            pos++;

                            DataCollection[pos] = "";
                            ReportData oRD = new ReportData();
                            for (int i = 0; i < DataCollection.Length; i++)
                            {
                                if (i + 1 == DataCollection.Length)
                                {
                                    oRD.Data += "<td style=\"border-bottom:1px solid;\"> " + DataCollection[i] + " </td>";
                                }
                                else
                                {
                                    oRD.Data += "<td style=\"border-bottom:1px solid; border-right:1px solid;\"> " + DataCollection[i] + " </td>";
                                }

                            }
                            oRD.DataCollection = DataCollection;
                            OReportList.Add(oRD);
                            slno++;
                        }
                    }

                    // total calculate , first and last 2 two column is string data so we escaped to calculate toal
                    decimal[] DataTotal = new decimal[ReportTable.Columns.Count];
                    foreach (ReportData oData in OReportList)
                    {
                        for (int i = 2; i < oData.DataCollection.Length - 2; i++)
                        {
                            decimal temp;
                            decimal.TryParse(oData.DataCollection[i], out temp);

                            DataTotal[i] += temp;
                        }
                    }

                    ReportData oRDTotal = new ReportData();
                    oRDTotal.Data = "<td>&nbsp;</td> <td style=\"border-right:1px solid;\" >Total</td>  ";
                    for (int i = 2; i < DataTotal.Length - 2; i++)
                    {
                        oRDTotal.Data += "<td style=\"border-right:1px solid;\"> " + DataTotal[i] + " </td>";
                    }

                    OReportList.Add(oRDTotal);

                    // end row total calculate

                    Repeater1.DataSource = OReportList;
                    Repeater1.DataBind();
                    //GridView1.DataSource = ReportTable;
                    //GridView1.DataBind();
                    #endregion

                }
                catch (Exception ex)
                {
                    lblMsgTop.Text = "An error occured when processig this record <br> " + ex.Message;
                }

            }
        } // end load


        public List<DEPARTMENT_SETUP> SelectDepByBranch(string branchID, string depID)
        {
            List<DEPARTMENT_SETUP> oDepList = new DEPARTMENT_SETUP().FindDepByBranch(branchID, depID);

            return oDepList;
        }


        public void PreviousUnUsed()
        {
            //List<ALLOWANCE_TYPE_SETUP> oATY = new ALLOWANCE_TYPE_SETUP().SelectAll();
            //RepAlHeader.DataSource = oATY;
            //RepAlHeader.DataBind();

            //// bind table header for deduction record
            //for (int i = 0; i < oATY.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        oATY[i].ALOWANCE_TITLE = "Salary Advance";
            //    }
            //    else if (i == 1)
            //    {
            //        oATY[i].ALOWANCE_TITLE = "Loan";
            //    }
            //    else if (i == 2)
            //    {
            //        oATY[i].ALOWANCE_TITLE = "Abscent";
            //    }
            //    else
            //    {
            //        oATY[i].ALOWANCE_TITLE = "";
            //    }
            //}
            //RepDedHeader.DataSource = oATY;
            //RepDedHeader.DataBind();

            //// create for branch group
            //List<BRANCH_SETUP> oBList = new BRANCH_SETUP().SelectAll(branchID);
            //RepBranch.DataSource = oBList;
            //RepBranch.DataBind();


            //decimal TotalAllowance = 0;
            //decimal TotalGross = 0;
            //decimal TotalNetAmount = 0;

            //foreach (RepeaterItem ri in RepBranch.Items)
            //{
            //    Repeater RepDepartment = ri.FindControl("RepDepartment") as Repeater;
            //    HiddenField hfBranchID = ri.FindControl("hfBranchID") as HiddenField;

            //    RepDepartment.DataSource = SelectDepByBranch(hfBranchID.Value, depID);
            //    RepDepartment.DataBind();

            //    Repeater RepAiDataBranchHeader = ri.FindControl("RepAiDataBranchHeader") as Repeater;
            //    RepAiDataBranchHeader.DataSource = oATY;
            //    RepAiDataBranchHeader.DataBind();

            //    // for summary data
            //    decimal depTotalAllowance = 0;
            //    decimal depTotalGross = 0;
            //    decimal depNetAmount = 0;

            //    // get each branch and depid employee reocrd
            //    foreach (RepeaterItem ri2 in RepDepartment.Items)
            //    {
            //        HiddenField HFDID = ri2.FindControl("HFDID") as HiddenField;

            //        #region employee data processing.....

            //        List<EMPLOYEE_SETUP> oSHFList = new EMPLOYEE_SETUP().SelectAllByBranchDepartment(int.Parse(hfBranchID.Value), int.Parse(HFDID.Value));
            //        Repeater RepEmployee = ri2.FindControl("RepEmployee") as Repeater;

            //        RepEmployee.DataSource = oSHFList;
            //        RepEmployee.DataBind();

            //        Repeater RepAiDataDepHeader = ri2.FindControl("RepAiDataDepHeader") as Repeater;
            //        RepAiDataDepHeader.DataSource = oATY;
            //        RepAiDataDepHeader.DataBind();

            //        #region ---
            //        // get allowance data according to empid , year month
            //        foreach (RepeaterItem ri3 in RepEmployee.Items)
            //        {
            //            HiddenField HFEID = ri3.FindControl("HFEID") as HiddenField;
            //            HiddenField HFJTID = ri3.FindControl("HFJTID") as HiddenField;

            //            #region full time
            //            if (HFJTID.Value == "1")
            //            {
            //                SALLARY_SHEET_FULLTIME oSHF = new SALLARY_SHEET_FULLTIME().Find(year, month, int.Parse(HFEID.Value));
            //                if (oSHF == null) { continue; }
            //                #region load allowance data
            //                if (oSHF != null && oSHF.DetailsList != null)
            //                {
            //                    List<SALLARY_SHEET_FULLTIME_DETAILS> ODetails = new List<SALLARY_SHEET_FULLTIME_DETAILS>();


            //                    for (int i = 0; i < oATY.Count; i++)
            //                    {
            //                        SALLARY_SHEET_FULLTIME_DETAILS oSFD = new SALLARY_SHEET_FULLTIME_DETAILS();
            //                        foreach (SALLARY_SHEET_FULLTIME_DETAILS oSSFD in oSHF.DetailsList)
            //                        {
            //                            if (oATY[i].ALID == oSSFD.ALID)
            //                            {
            //                                oSFD.ALID = oATY[i].ALID;
            //                                oSFD.AMOUNT = oSSFD.AMOUNT;
            //                                ODetails.Add(oSFD);
            //                            }
            //                        }
            //                    }

            //                    Repeater RepAlData = ri3.FindControl("RepAlData") as Repeater;
            //                    RepAlData.DataSource = ODetails;
            //                    RepAlData.DataBind();
            //                }
            //                #endregion

            //                Label lblOverTimeAmnt = ri3.FindControl("lblOverTimeAmnt") as Label;
            //                lblOverTimeAmnt.Text = oSHF.OT_AMOUNT.Value.ToString();

            //                Label lblTotalAllowance = ri3.FindControl("lblTotalAllowance") as Label;
            //                lblTotalAllowance.Text = oSHF.TotalAllownace.Value.ToString();

            //                decimal temp;
            //                decimal.TryParse(lblTotalAllowance.Text, out temp);
            //                depTotalAllowance += temp;

            //                Label lblGrossAllowance = ri3.FindControl("lblGrossAllowance") as Label;
            //                lblGrossAllowance.Text = oSHF.GROSS_SALARY.Value.ToString();

            //                decimal.TryParse(lblGrossAllowance.Text, out temp);
            //                depTotalGross += temp;

            //                Label lblNetAmnt = ri3.FindControl("lblNetAmnt") as Label;
            //                lblNetAmnt.Text = oSHF.NET_AMOUNT.Value.ToString();

            //                decimal.TryParse(lblNetAmnt.Text, out temp);
            //                depNetAmount += temp;

            //                // load deduction next  row data
            //                Label lblAdvanceAmnt = ri3.FindControl("lblAdvanceAmnt") as Label;
            //                lblAdvanceAmnt.Text = oSHF.AdvanceAmount.Value.ToString();

            //                Label lblLoanAmnt = ri3.FindControl("lblLoanAmnt") as Label;
            //                lblLoanAmnt.Text = oSHF.LoanAmount.Value.ToString();

            //                Label lblAbscentAmnt = ri3.FindControl("lblAbscentAmnt") as Label;
            //                lblAbscentAmnt.Text = oSHF.ABSCENT_AMOUNT.Value.ToString();

            //            }
            //            #endregion


            //            #region part time
            //            if (HFJTID.Value == "2")
            //            {
            //                SALLARY_SHEET_PARTTIME oSHF = new SALLARY_SHEET_PARTTIME().Find(year, month, int.Parse(HFEID.Value));
            //                if (oSHF == null) { continue; }
            //                #region load allowance data

            //                List<SALLARY_SHEET_FULLTIME_DETAILS> ODetails = new List<SALLARY_SHEET_FULLTIME_DETAILS>();


            //                for (int i = 0; i < oATY.Count; i++)
            //                {
            //                    SALLARY_SHEET_FULLTIME_DETAILS oSFD = new SALLARY_SHEET_FULLTIME_DETAILS();

            //                    if (oATY[i].ALOWANCE_TITLE == ALLOWANCE_TYPE_SETUP.TypeBasic)
            //                    {
            //                        oSFD.ALID = oATY[i].ALID;
            //                        oSFD.AMOUNT = oSHF.RATE_PER_HOUR.Value * oSHF.TOTAL_HOUR.Value;
            //                    }
            //                    ODetails.Add(oSFD);
            //                }

            //                Repeater RepAlData = ri3.FindControl("RepAlData") as Repeater;
            //                RepAlData.DataSource = ODetails;
            //                RepAlData.DataBind();

            //                #endregion

            //                Label lblOverTimeAmnt = ri3.FindControl("lblOverTimeAmnt") as Label;
            //                lblOverTimeAmnt.Text = oSHF.OVER_TIME_AMNT.Value.ToString();

            //                Label lblTotalAllowance = ri3.FindControl("lblTotalAllowance") as Label;
            //                lblTotalAllowance.Text = "0";

            //                decimal temp;
            //                decimal.TryParse(lblTotalAllowance.Text, out temp);
            //                depTotalAllowance += temp;

            //                Label lblGrossAllowance = ri3.FindControl("lblGrossAllowance") as Label;
            //                lblGrossAllowance.Text = (oSHF.RATE_PER_HOUR.Value * oSHF.TOTAL_HOUR.Value).ToString();

            //                decimal.TryParse(lblGrossAllowance.Text, out temp);
            //                depTotalGross += temp;

            //                Label lblNetAmnt = ri3.FindControl("lblNetAmnt") as Label;
            //                lblNetAmnt.Text = ((oSHF.RATE_PER_HOUR.Value * oSHF.TOTAL_HOUR.Value) + oSHF.OVER_TIME_AMNT.Value).ToString();

            //                decimal.TryParse(lblNetAmnt.Text, out temp);
            //                depNetAmount += temp;

            //                // load deduction next  row data
            //                Label lblAdvanceAmnt = ri3.FindControl("lblAdvanceAmnt") as Label;
            //                lblAdvanceAmnt.Text = "0";

            //                Label lblLoanAmnt = ri3.FindControl("lblLoanAmnt") as Label;
            //                lblLoanAmnt.Text = "0";

            //                Label lblAbscentAmnt = ri3.FindControl("lblAbscentAmnt") as Label;
            //                lblAbscentAmnt.Text = "0";

            //            }
            //            #endregion
            //        }
            //        #endregion

            //        #endregion

            //    }

            //    // for group summary row data 
            //    Repeater RepAlDatas = ri.FindControl("RepAlData") as Repeater;
            //    RepAlDatas.DataSource = oATY;
            //    RepAlDatas.DataBind();

            //    Label lblDepAlloTotal = ri.FindControl("lblDepAlloTotal") as Label;
            //    lblDepAlloTotal.Text = depTotalAllowance.ToString();

            //    Label lblDepGrossTotal = ri.FindControl("lblDepGrossTotal") as Label;
            //    lblDepGrossTotal.Text = depTotalGross.ToString();

            //    Label lblDepNetTotal = ri.FindControl("lblDepNetTotal") as Label;
            //    lblDepNetTotal.Text = depNetAmount.ToString();

            //    TotalAllowance += depTotalAllowance;
            //    TotalGross += depTotalGross;
            //    TotalNetAmount += depNetAmount;
            //}

            //RepAlData2.DataSource = oATY;
            //RepAlData2.DataBind();

            //lblAllowanceTotal.Text = TotalAllowance.ToString();
            //lblGrossTotal.Text = TotalGross.ToString();
            //lblNetTotal.Text = TotalNetAmount.ToString();
        }


        #region style
        string styles2 = @".tableStyle td 
                            {
                                width: 50px;
            
                            }
                             .tableStyle th 
                            {
                                width: 50px;
                            }
                            .tableStyle 
                            {
                                font-size:11px;
                                width:800px;
                                border:1px solid;
                            }
        
                            .tableStyle2
                            {
                                border:1px solid;
                            }";

        #endregion


        protected void btnExcel_Click(object sender, EventArgs e)
        {

            //var htmlContent = GetHtmlContent();
            var excelName = "TimeeCard_" + 2;
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.ContentEncoding = Encoding.GetEncoding("windows-1254");
            Response.Charset = "windows-1254"; //ISO-8859-13 ISO-8859-9  windows-1254
            Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}.xls", excelName));
            Response.ContentType = "application/ms-excel";
            //response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            string header = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<title></title>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1254\" />\n<style>\n " + styles2 + "</style>\n</head>\n<body>\n";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            sheet.RenderControl(htmlWrite);

            Response.Write(header + stringWrite.ToString());
            Response.End();
        }


    }
}