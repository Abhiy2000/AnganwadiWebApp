using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using AnganwadiLib.Methods;
using System.Data;
using System.IO;
using System.Drawing;

namespace AnganwadiWebApp.Reports
{
    public partial class RptPaymentDeductions : System.Web.UI.Page
    {
        BoLastNo objLast = new BoLastNo();
        DateTime Days = System.DateTime.Now;
        int days = 0;
        Int32 CentralBill, StateBill, FixedBill;

        #region "MeesageAlert"
        public void MessageAlert(string message, string WindowsLocation)
        {
            lblPopupResponse.Text = "|| " + message + "||";
            popMsg.Show();

            if (WindowsLocation != "")
            {
                lblredirect.Text = WindowsLocation;
            }
            else
            {
                lblredirect.Text = "";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Payment Report";
            if (!IsPostBack)
            {
                // LoadGrid();
                //dtSalDate.Focus();
                PnlSerch.Visible = true;
                BindDropdrownlist();
                //------------10-03-18
                PnlBillNo.Visible = false;
                btnExport.Visible = false;
                //----------------------------------

                MstMethods.Dropdown.Fill(ddldesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "var_designation_flag='W'  order by trim(var_designation_desig)", "");
                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();

                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["BRCategory"]);

                if (BRCategory == 0)    //Admin
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                }
                if (BRCategory == 1)    //State
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                }

                else if (BRCategory == 2)   // Div
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");

                    ddlDivision.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDivision_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDistrict_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                }

                else if (BRCategory == 4)   // CDPO
                {
                    str = "select a.num_corporation_corpid cdpo, a.num_corporation_parentid dis, b.num_corporation_parentid div ";
                    str += "from aoup_corporation_mas a ";
                    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = Session["GrdLevel"].ToString();

                    ddlCDPO_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                }

                else if (BRCategory == 5)   // Beat
                {
                    str = "select a.num_corporation_corpid beat, a.num_corporation_parentid cdpo, b.num_corporation_parentid dis, c.num_corporation_parentid div ";
                    str += "from aoup_corporation_mas a ";
                    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                    str += "inner join aoup_corporation_mas c on b.num_corporation_parentid = c.num_corporation_corpid ";
                    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["cdpo"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();
                    ddlBeat.SelectedValue = Session["GrdLevel"].ToString();

                    ddlBeat_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                    PnlSerch.Visible = true;
                }
            }
        }

        protected void GrdPaymnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdPaymnt.SelectedRow;
            Session["SevikaId"] = row.Cells[1].Text;
        }

        protected void GrdPaymnt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrdPaymnt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdPaymnt.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //PnlSerch.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //PnlSerch.Visible = false;

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //PnlSerch.Visible = false;

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
            }
        }

        protected void ddlBeat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBeat.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();
                PnlSerch.Visible = true;
                //BindDropdrownlist();
            }

            else
            {
                //PnlSerch.Visible = false;
            }
        }

        #region "Export Excel"
        protected void btnExport_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        public void ExportToExcel(DataTable Dt, String XlsName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = Dt;
            GridView1.DataBind();

            GridView1.HeaderRow.Cells[0].Text = "Receiving Party Code";
            GridView1.HeaderRow.Cells[1].Text = "Receiving Party Name";
            GridView1.HeaderRow.Cells[2].Text = "Transaction Code";
            GridView1.HeaderRow.Cells[3].Text = "Transaction Key";
            GridView1.HeaderRow.Cells[4].Text = "Component Code";
            GridView1.HeaderRow.Cells[5].Text = "Expense Type";
            GridView1.HeaderRow.Cells[6].Text = "Amount";
            GridView1.HeaderRow.Cells[7].Text = "Remarks";
            GridView1.HeaderRow.Cells[8].Text = "Action Type";
            GridView1.HeaderRow.Cells[9].Text = "Account Number";
            GridView1.HeaderRow.Cells[10].Text = "Payment Method";
            GridView1.HeaderRow.Cells[11].Text = "Narration For PassBook";
            GridView1.HeaderRow.Cells[12].Text = "Salary Date";

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + XlsName + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                GridView1.HeaderRow.BackColor = Color.Aqua;

                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (cell.Text.Length > 10)
                        {
                            cell.CssClass = "textmode";
                        }

                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                    }
                }

                GridView1.RenderControl(hw);

                string style = @"<style> .textmode {  mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public void ExportToExcel_new(DataTable Dt, String XlsName)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Server.MapPath("../ImageGarbage/") + XlsName + ".xls");
                sw.WriteLine("CPSMS Beneficiary Code" + Convert.ToChar(9)
                                  + "Scheme Specific Id" + Convert.ToChar(9)
                                  + "Beneficiary Name" + Convert.ToChar(9)
                                  + "Purpose" + Convert.ToChar(9)
                                   + "Centre Share Payment Amount" + Convert.ToChar(9)
                                   + "State Share Payment Amount" + Convert.ToChar(9)
                                   + "Payment From Date" + Convert.ToChar(9)
                                   + "Payment To Date" + Convert.ToChar(9)
                                   );
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    sw.WriteLine(Dt.Rows[i]["CPSMS_Beneficiary_Code"].ToString() + Convert.ToChar(9)
                         + "'" + Dt.Rows[i]["Scheme_Specific_Id"].ToString() + Convert.ToChar(9)
                         + Dt.Rows[i]["Beneficiary_Name"].ToString() + Convert.ToChar(9)
                         + Dt.Rows[i]["Purpose"].ToString() + Convert.ToChar(9)
                         + Dt.Rows[i]["Centre_Share_Payment_Amount"].ToString() + Convert.ToChar(9)
                         + Dt.Rows[i]["State_Share_Payment_Amount"].ToString() + Convert.ToChar(9)
                         + Dt.Rows[i]["Payment_From_Date"].ToString() + Convert.ToChar(9)
                         + Dt.Rows[i]["Payment_To_Date"].ToString() + Convert.ToChar(9)
                        );
                }
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
            Response.Redirect("../ImageGarbage/" + XlsName + ".xls");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered (Export To Excel Is not working)*/
        }
        #endregion

        protected void LoadGrid()
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");
            String paydate = Lastdate.ToString("dd/MM/yyyy");

            //String Query = " select var_sevikamaster_cpsmscode CPSMS_Beneficiary_Code, CPSMSCODE Comp_CPSMSCODE, var_sevikamaster_sevikacode Scheme_Specific_Id,var_sevikamaster_name Beneficiary_Name, ";
            //Query += " case when VAR_DESIGNATION_FLAG='W' THEN 'Incentive To AW' when VAR_DESIGNATION_FLAG='M' THEN 'Incentive To AW' else 'Incentive To AH' END Purpose, ";
            //Query += " nvl(num_salary_central,0)+nvl(num_salary_add_center,0) Centre_Share_Payment_Amount, ";
            //Query += " nvl(num_salary_state,0)+nvl(num_salary_fixed,0)+nvl(num_salary_add_state,0)+nvl(num_salary_add_tribal,0) State_Share_Payment_Amount, ";
            ////Query += " num_salary_central Centre_Share_Payment_Amount, ";
            ////Query += " (num_salary_state+num_salary_fixed) State_Share_Payment_Amount, ";
            //Query += " to_char((SELECT ADD_MONTHS((LAST_DAY('" + lstdate + "')+1),-1) FROM DUAL),'dd/mm/yyyy') Payment_From_Date,to_char('" + paydate + "') Payment_To_Date ";
            //Query += " from  aoup_salary_def d inner join aoup_sevikamaster_def a on ";
            //Query += " a.num_sevikamaster_sevikaid= d.num_salary_sevikaid ";//and a.num_sevikamaster_payscalid=d.num_salary_payscalid
            //Query += " inner Join aoup_designation_def b on d.num_salary_desigid=b.num_designation_desigid ";
            //Query += " inner join corpinfo c on d.num_salary_compid=c.bitid ";

            String Query = "select receiving_party_code ,receiving_party_name,transaction_code,transaction_key,component_code,expense_type,amount,remarks,action_type,";
            Query += "account_number,payment_method,narrationforpassbook,TO_CHAR(date_salary_date,'dd/mm/yyyy')  from ("; //,stateid,divid,distid,cdpoid,bitid,num_salary_desigid,num_salary_absentdays
            Query += " select num_salary_sevikaid, var_sevikamaster_cpsmscode Receiving_Party_Code, var_sevikamaster_name Receiving_Party_Name,";
            Query += " 'GP' Transaction_Code, null Transaction_Key, 2 Component_Code, 'R' Expense_Type,";
            Query += " (num_salary_central + num_salary_state + num_salary_fixed + num_salary_allowance1 + num_salary_allowance2) Amount,  ";
            Query += " null Remarks, null Action_Type, null Account_Number, 'U' Payment_Method, null  NarrationForPassBook ,";
            Query += " divid,distid,cdpoid,bitid,num_salary_desigid, VAR_DESIGNATION_FLAG,var_sevikamaster_cpsmscode,num_salary_presentdays, num_salary_absentdays, ";
            Query += " var_salary_billnocentral, var_salary_billnostate, var_salary_billnofixed ,date_salary_date,stateid, ";
            Query += " case when VAR_DESIGNATION_FLAG='W' THEN 'Incentive To AW' when VAR_DESIGNATION_FLAG='M' THEN 'Incentive To AW' else 'Incentive To AH' END Purpose";
            Query += " from aoup_Salary_def ";
            Query += " inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_salary_sevikaid";
            Query += " inner join corpinfo  on  num_salary_compid= bitid";
            Query += " inner Join aoup_designation_def  on num_salary_desigid= num_designation_desigid ";
            Query += " where date_salary_date='" + lstdate + "'";
            Query += " union all";
            Query += " select num_salary_sevikaid, var_sevikamaster_cpsmscode Receiving_Party_Code, var_sevikamaster_name Receiving_Party_Name,";
            Query += " 'MSC' Transaction_Code, null Transaction_Key, 2 Component_Code, 'R' Expense_Type, (num_salary_deduct1 + num_salary_deduct2) Amount, ";
            Query += " null Remarks, null Action_Type, null Account_Number, 'U' Payment_Method, null  NarrationForPassBook ,";
            Query += " divid,distid,cdpoid,bitid,num_salary_desigid, VAR_DESIGNATION_FLAG,var_sevikamaster_cpsmscode,num_salary_presentdays, num_salary_absentdays,";
            Query += " var_salary_billnocentral, var_salary_billnostate, var_salary_billnofixed ,date_salary_date,stateid, ";
            Query += " case when VAR_DESIGNATION_FLAG='W' THEN 'Incentive To AW' when VAR_DESIGNATION_FLAG='M' THEN 'Incentive To AW' else 'Incentive To AH' END Purpose";
            Query += " from aoup_Salary_def ";
            Query += " inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_salary_sevikaid";
            Query += " inner join corpinfo  on  num_salary_compid= bitid";
            Query += " inner Join aoup_designation_def  on num_salary_desigid= num_designation_desigid ";
            Query += " where date_salary_date='" + lstdate + "')";

            Query += " where stateid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " and date_salary_date = '" + lstdate + "' ";
            Query += " and var_salary_billnofixed is not null and var_salary_billnocentral is not null and var_salary_billnostate is not null ";
            //Query += " and num_salary_presentdays > 0 ";

            if (ddlDivision.SelectedValue != "")
            {
                Query += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
            }
            if (ddlDistrict.SelectedValue != "")
            {
                Query += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            if (ddlCDPO.SelectedValue != "")
            {
                Query += " and cdpoid=" + Convert.ToInt32(ddlCDPO.SelectedValue);
            }
            if (ddlBeat.SelectedValue != "")
            {
                Query += " and bitid=" + Convert.ToInt32(ddlBeat.SelectedValue);
            }
            if (ddldesg.SelectedValue != "")
            {
                Query += " and num_salary_desigid=" + Convert.ToInt32(ddldesg.SelectedValue);
            }
            if (rbdWorker.Checked == true)
            {
                Query += " and VAR_DESIGNATION_FLAG='W' ";
            }
            if (rbdHelper.Checked == true)
            {
                Query += " and VAR_DESIGNATION_FLAG='H' ";
            }
            if (rbdMini.Checked == true)
            {
                Query += " and VAR_DESIGNATION_FLAG='M' ";
            }
            //-------------------10-03-18------
            Query += " and var_salary_billnocentral = '" + CentralBill + "' ";
            Query += " and var_salary_billnostate = '" + StateBill + "' ";
            Query += " and var_salary_billnofixed = '" + FixedBill + "' ";
            //-----------------------------------------

            if (rbdRegistered.Checked == true)
            {
                Query += " and var_sevikamaster_cpsmscode is not null and num_salary_presentdays > 0 ";
            }
            if (rbdUnregistered.Checked == true)
            {
                Query += " and var_sevikamaster_cpsmscode is null and num_salary_presentdays > 0 ";
            }
            if (rbdMaternity.Checked == true)
            {
                Query += " and num_salary_presentdays = 0 and num_salary_absentdays = 0 ";
            }
            Query += " order by receiving_party_name,transaction_code";

            DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (dtSevikaList.Rows.Count > 0)
            {
                objLast.UserId = Session["UserId"].ToString();
                objLast.Type = "PPA";
                objLast.Date = System.DateTime.Now;
                objLast.BoLastNo_1();

                if (objLast.ErrorCode == -100)
                {
                    ExportToExcel(dtSevikaList, "MH_WCD_" + DateTime.Now.ToString("ddMMyyyy") + "00" + objLast.LastNo);
                }
                else
                {
                    MessageAlert(objLast.ErrorMsg, "");
                    return;
                }
                //ExportToExcel_new(dtSevikaList, "MH_WCD_" + DateTime.Now.ToString("ddMMyyyy"));
            }
            else
            {
                MessageAlert(" Record not Found for selected Date", "");
                return;
            }
        }

        public void BindDropdrownlist()
        {
            ddlMonth.Items.Add(new ListItem("January", "1"));
            ddlMonth.Items.Add(new ListItem("February", "2"));
            ddlMonth.Items.Add(new ListItem("March", "3"));
            ddlMonth.Items.Add(new ListItem("April", "4"));
            ddlMonth.Items.Add(new ListItem("May", "5"));
            ddlMonth.Items.Add(new ListItem("June", "6"));
            ddlMonth.Items.Add(new ListItem("July", "7"));
            ddlMonth.Items.Add(new ListItem("August", "8"));
            ddlMonth.Items.Add(new ListItem("September", "9"));
            ddlMonth.Items.Add(new ListItem("October", "10"));
            ddlMonth.Items.Add(new ListItem("November", "11"));
            ddlMonth.Items.Add(new ListItem("December", "12"));

            ddlYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), "1"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), "2"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 2).ToString(), "3"));
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();

            ddlYear.SelectedItem.Text = System.DateTime.Now.Year.ToString();
        }

        protected void rbdWorker_CheckedChanged(object sender, EventArgs e)
        {
            MstMethods.Dropdown.Fill(ddldesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "var_designation_flag='W'  order by trim(var_designation_desig)", "");
        }

        protected void rbdHelper_CheckedChanged(object sender, EventArgs e)
        {
            MstMethods.Dropdown.Fill(ddldesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "var_designation_flag='H' order by trim(var_designation_desig)", "");
        }

        protected void rbdOther_CheckedChanged(object sender, EventArgs e)
        {
            MstMethods.Dropdown.Fill(ddldesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "var_designation_flag='O' order by trim(var_designation_desig)", "");
        }

        //------------10-03-18----------
        public void BillNo()
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            string billdet = " select var_salary_billnocentral Central,var_salary_billnostate State,var_salary_billnofixed Fixed from aoup_salary_def ";
            billdet += " left join corpinfo on num_salary_compid=bitid ";
            billdet += " left join aoup_designation_def on num_designation_desigid=num_salary_desigid ";
            billdet += " where stateid =" + TblGetHoId.Rows[0]["hoid"].ToString();
            if (ddlDivision.SelectedValue != "")
            {
                billdet += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
            }
            if (ddlDistrict.SelectedValue != "")
            {
                billdet += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            if (ddlCDPO.SelectedValue != "")
            {
                billdet += " and cdpoid=" + Convert.ToInt32(ddlCDPO.SelectedValue);
            }
            if (ddlBeat.SelectedValue != "")
            {
                billdet += " and bitid=" + Convert.ToInt32(ddlBeat.SelectedValue);
            }
            if (rbdWorker.Checked == true)
            {
                billdet += " and VAR_DESIGNATION_FLAG='W' ";
            }
            if (rbdHelper.Checked == true)
            {
                billdet += " and VAR_DESIGNATION_FLAG='H' ";
            }
            if (rbdMini.Checked == true)
            {
                billdet += " and VAR_DESIGNATION_FLAG='M' ";
            }
            if (rbdRegistered.Checked == true)
            {
                billdet += " and var_salary_cpsmscode is not null ";
            }
            if (rbdUnregistered.Checked == true)
            {
                billdet += " and var_salary_cpsmscode is null ";
            }
            if (rbdMaternity.Checked == true) //added on 24/07/19
            {
                billdet += " and num_salary_presentdays = 0 and num_salary_absentdays = 0  ";
            }
            billdet += " and trunc(date_salary_date)='" + lstdate + "' ";//num_salary_compid=1559 and
            billdet += " and var_salary_billnofixed is not null and var_salary_billnocentral is not null and var_salary_billnostate is not null ";
            billdet += " GROUP by var_salary_billnocentral,var_salary_billnostate,var_salary_billnofixed ";

            DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(billdet);

            if (dtstr.Rows.Count > 0)
            {
                grdBillno.DataSource = dtstr;
                grdBillno.DataBind();
                PnlBillNo.Visible = true;
            }
            else
            {
                grdBillno.DataSource = null;
                grdBillno.DataBind();
                PnlBillNo.Visible = false;
                MessageAlert("Record Not found", "");
                return;
            }
        }

        protected void grdBillno_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdBillno.SelectedRow;
            CentralBill = Convert.ToInt32(row.Cells[1].Text);
            StateBill = Convert.ToInt32(row.Cells[2].Text);
            FixedBill = Convert.ToInt32(row.Cells[3].Text);

            LoadGrid();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            BillNo();
        }
        //-----------------------------------------------
    }
}