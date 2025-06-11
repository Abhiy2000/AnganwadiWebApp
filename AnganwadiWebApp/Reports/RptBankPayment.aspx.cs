using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.IO;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Reports
{
    public partial class RptBankPayment : System.Web.UI.Page
    {
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Registerd Savika Report";
            if (!IsPostBack)
            {
                // LoadGrid();
                //dtSalDate.Focus();
                PnlSerch.Visible = true;
                //------------10-03-18
                PnlBillNo.Visible = false;
                btnExport.Visible = false;
                //----------------------------------
                BindDropdrownlist();
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

        protected void GrdBankPaymnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdBankPaymnt.SelectedRow;
            Session["SevikaId"] = row.Cells[1].Text;
        }

        protected void GrdBankPaymnt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrdBankPaymnt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdBankPaymnt.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

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

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                //Session["GrdLevel"] = ddlDivision.SelectedValue.ToString();
                PnlSerch.Visible = true;
                //BindDropdrownlist();
            }
            else
            {
                //PnlSerch.Visible = false;
            }
        }

        protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                //Session["GrdLevel"] = ddlDistrict.SelectedValue.ToString();
                PnlSerch.Visible = true;
                //BindDropdrownlist();
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
            }
            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                //Session["GrdLevel"] = ddlCDPO.SelectedValue.ToString();
                PnlSerch.Visible = true;
                //BindDropdrownlist();
            }
        }

        protected void ddlBeat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBeat.SelectedValue.ToString() != "")
            {
                //Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();
                PnlSerch.Visible = true;
                //BindDropdrownlist();
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered (Export To Excel Is not working)*/
        }
        #endregion

        protected void LoadGrid()
        {
            //if (dtSalDate.Text != "")
            //{

            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            String BITid = "";
            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            String Query = " select var_sevikamaster_cpsmscode PFMS_Beneficiary_ID,var_sevikamaster_sevikacode SchemeSpecificId,var_sevikamaster_name Beneficiary_Name, ";
            Query += " case when VAR_DESIGNATION_FLAG='W' THEN 'Incentive To AW' else 'Incentive To AH' END Purpose,num_salary_presentdays PresentDays, num_salary_absentdays AbsentDays,var_designation_desig Designation, ";
            //Query += " num_salary_central Central,num_salary_state State,num_salary_fixed State_Fixed, (num_salary_central+num_salary_state+num_salary_fixed) Total_Amount,";            
            Query += " nvl(num_salary_central,0)+nvl(num_salary_add_center,0) Central, nvl(num_salary_state,0)+nvl(num_salary_add_state,0)+nvl(num_salary_add_tribal,0) State,num_salary_fixed State_Fixed, ";
            Query += " nvl(num_salary_central,0)+nvl(num_salary_state,0)+nvl(num_salary_fixed,0)+nvl(num_salary_add_center,0)+nvl(num_salary_add_state,0)+nvl(num_salary_add_tribal,0) Total_Amount, ";
            //----------------------------------------newly added on 19/01/2023-------------------------------------------------------------------------------
            Query += " NVL(num_salary_deduct1, 0) + NVL(num_salary_deduct2, 0) Deduction_Amount,  NVL(((NVL(num_salary_central, 0) + NVL(num_salary_state, 0) + NVL(num_salary_fixed, 0) + NVL(num_salary_add_center, 0) + NVL(num_salary_add_state, 0) ";
            Query += "  + NVL(num_salary_add_tribal, 0)) - (NVL(num_salary_deduct1, 0) + NVL(num_salary_deduct2, 0))), 0) NetAmount, ";
            //------------------------------------------------------------------------------------------------------------------------------------------------

            Query += " to_char((SELECT ADD_MONTHS((LAST_DAY('" + lstdate + "')+1),-1) FROM DUAL)) Payment_From_Date, ";
            Query += " to_char('" + lstdate + "') Payment_To_Date,divname DivisionName,distname District,cdpocode DDOCode,null AWName,var_sevikamaster_accno AccountNo,var_sevikamaster_aadharno AadharCardNo ";
            /*Query += " from aoup_sevikamaster_def a inner join aoup_salary_def b on a.num_sevikamaster_compid=b.num_salary_compid ";
            Query += " and a.num_sevikamaster_sevikaid=b.num_salary_sevikaid inner join corpinfo c on a.num_sevikamaster_compid=c.bitid ";
            Query += " inner Join aoup_designation_def b on a.num_sevikamaster_desigid=b.num_designation_desigid ";*/
            Query += " from aoup_salary_def b inner join aoup_sevikamaster_def a on  a.num_sevikamaster_sevikaid=b.num_salary_sevikaid ";
            Query += " inner join corpinfo c on b.num_salary_compid=c.bitid inner Join aoup_designation_def d on b.num_salary_desigid=d.num_designation_desigid ";
            Query += " where stateid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " and date_salary_date='" + lstdate + "' ";
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
            //-----------Radio Button Visible False----------------
            if (rbdNormal.Checked == true)
            {
                Query += " and num_salary_presentdays > 0 "; //and var_sevikamaster_cpsmscode is not null 
            }
            if (rbdzeroPrsnt.Checked == true)
            {
                Query += " and num_salary_presentdays=0 ";
            }
            //if (rbdntReg.Checked == true)
            //{
            //    Query += " and var_sevikamaster_cpsmscode is null ";
            //}
            if (rbdRegistered.Checked == true)
            {
                Query += " and var_sevikamaster_cpsmscode is not null ";
            }
            if (rbdUnregistered.Checked == true)
            {
                Query += " and var_sevikamaster_cpsmscode is null ";
            }
            if (rbdAll.Checked == true)
            {
                Query += " and ( var_sevikamaster_cpsmscode is not null or var_sevikamaster_cpsmscode is null )";
            }
            //--------------------------------------------------

            //-------------------10-03-18------
            Query += " and var_salary_billnocentral = '" + CentralBill + "' ";
            Query += " and var_salary_billnostate = '" + StateBill + "' ";
            Query += " and var_salary_billnofixed = '" + FixedBill + "' ";
            //-----------------------------------------
            Query += " and var_salary_authorisedby is not null ";
            Query += " order by num_sevikamaster_sevikaid ";

            DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (dtSevikaList.Rows.Count > 0)
            {
                ExportToExcel(dtSevikaList, "Bank_Payment_Report_as_on_" + DateTime.Now);
            }
            else
            {
                MessageAlert(" Record not Found for selected Report Type", "");
                return;
            }
            //}
            //else
            //{
            //    MessageAlert(" Please Select Date ", "");
            //    return;
            //    dtSalDate.Focus();
            //}
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
            //ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 5).ToString(), "4"));//added for data need to remove after work done
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();

            ddlYear.SelectedItem.Text = System.DateTime.Now.Year.ToString();
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
            if (rbdRegistered.Checked == true)
            {
                billdet += " and var_salary_cpsmscode is not null ";
            }
            if (rbdUnregistered.Checked == true)
            {
                billdet += " and var_salary_cpsmscode is null ";
            }
            if (rbdAll.Checked == true)
            {
                billdet += " and ( var_salary_cpsmscode is null OR var_salary_cpsmscode is not null) ";
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
            if (ddlDivision.SelectedValue == "")
            {
                MessageAlert("Please Select Division", "");
                return;
            }
            BillNo();
        }
        //-----------------------------------------------
    }
}