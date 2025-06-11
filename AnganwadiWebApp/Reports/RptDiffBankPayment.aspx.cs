using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using System.IO;
using System.Drawing;

namespace AnganwadiWebApp.Reports
{
    public partial class RptDiffBankPayment : System.Web.UI.Page
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

                    //ddlDivision_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                   // ddlDistrict_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                }

                //else if (BRCategory == 4)   // CDPO
                //{
                //    str = "select a.num_corporation_corpid cdpo, a.num_corporation_parentid dis, b.num_corporation_parentid div ";
                //    str += "from aoup_corporation_mas a ";
                //    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                //    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                //    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                //    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                //    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                //    //MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                //    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                //    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                //    //ddlCDPO.SelectedValue = Session["GrdLevel"].ToString();

                //    //ddlCDPO_OnSelectedIndexChanged(sender, e);

                //    ddlDivision.Enabled = false;
                //    ddlDistrict.Enabled = false;
                //    //ddlCDPO.Enabled = false;
                //}

                //else if (BRCategory == 5)   // Beat
                //{
                //    str = "select a.num_corporation_corpid beat, a.num_corporation_parentid cdpo, b.num_corporation_parentid dis, c.num_corporation_parentid div ";
                //    str += "from aoup_corporation_mas a ";
                //    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                //    str += "inner join aoup_corporation_mas c on b.num_corporation_parentid = c.num_corporation_corpid ";
                //    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                //    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                //    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                //    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                //    //MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["cdpo"].ToString() + " order by branchname", "");
                //    //MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                //    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                //    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                //    //ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();
                //    //ddlBeat.SelectedValue = Session["GrdLevel"].ToString();

                //    //ddlBeat_OnSelectedIndexChanged(sender, e);

                //    ddlDivision.Enabled = false;
                //    ddlDistrict.Enabled = false;
                //    //ddlCDPO.Enabled = false;
                //    //ddlBeat.Enabled = false;

                //    PnlSerch.Visible = true;
                //}
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

        //protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    PnlSerch.Visible = false;

        //    ddlDistrict.DataSource = "";
        //    ddlDistrict.DataBind();

        //    //ddlCDPO.DataSource = "";
        //    //ddlCDPO.DataBind();

        //    //ddlBeat.DataSource = "";
        //    //ddlBeat.DataBind();

        //    if (ddlDivision.SelectedValue.ToString() != "")
        //    {
        //        MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
        //    }

        //    if (ddlDivision.SelectedValue.ToString() != "")
        //    {
        //        //Session["GrdLevel"] = ddlDivision.SelectedValue.ToString();
        //        PnlSerch.Visible = true;
        //        //BindDropdrownlist();
        //    }
        //    else
        //    {
        //        //PnlSerch.Visible = false;
        //    }
        //}

        //protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    PnlSerch.Visible = false;

        //    ddlCDPO.DataSource = "";
        //    ddlCDPO.DataBind();

        //    ddlBeat.DataSource = "";
        //    ddlBeat.DataBind();

        //    if (ddlDistrict.SelectedValue.ToString() != "")
        //    {
        //        MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
        //    }
        //    if (ddlDistrict.SelectedValue.ToString() != "")
        //    {
        //        //Session["GrdLevel"] = ddlDistrict.SelectedValue.ToString();
        //        PnlSerch.Visible = true;
        //        //BindDropdrownlist();
        //    }
        //}

        //protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    PnlSerch.Visible = false;

        //    ddlBeat.DataSource = "";
        //    ddlBeat.DataBind();

        //    if (ddlCDPO.SelectedValue.ToString() != "")
        //    {
        //        MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
        //    }
        //    if (ddlCDPO.SelectedValue.ToString() != "")
        //    {
        //        //Session["GrdLevel"] = ddlCDPO.SelectedValue.ToString();
        //        PnlSerch.Visible = true;
        //        //BindDropdrownlist();
        //    }
        //}

        //protected void ddlBeat_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlBeat.SelectedValue.ToString() != "")
        //    {
        //        //Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();
        //        PnlSerch.Visible = true;
        //        //BindDropdrownlist();
        //    }
        //}

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
            Query += " case when VAR_DESIGNATION_FLAG='W' THEN 'Incentive To AW' else 'Incentive To AH' END Purpose,num_salarydiff_presentdays PresentDays, num_salarydiff_absentdays AbsentDays,var_designation_desig Designation, ";      
            Query += " nvl(num_salarydiff_diffcentral,0) Central, nvl(num_salarydiff_diffstate,0) State,num_salarydiff_difffixed State_Fixed, ";
            Query += " NUM_SALARYDIFF_DIFFTOTALPAID Total_Amount, ";
            Query += " to_char((SELECT ADD_MONTHS((LAST_DAY('" + lstdate + "')+1),-1) FROM DUAL)) Payment_From_Date, ";
            Query += " to_char('" + lstdate + "') Payment_To_Date,divname DivisionName,distname District,cdpocode DDOCode,null AWName,var_sevikamaster_accno AccountNo,var_sevikamaster_aadharno AadharCardNo ";
            Query += " from aoup_salarydiff_def b inner join aoup_sevikamaster_def a on  a.num_sevikamaster_sevikaid=b.num_salarydiff_sevikaid ";
            Query += " inner join corpinfo c on b.num_salarydiff_compid=c.bitid inner Join aoup_designation_def d on b.num_salarydiff_desigid=d.num_designation_desigid ";
            Query += " where stateid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " and date_salarydiff_date='" + lstdate + "' ";
            if (ddlDivision.SelectedValue != "")
            {
                Query += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
            }
            if (ddlDistrict.SelectedValue != "")
            {
                Query += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            //-----------Radio Button Visible False----------------
            if (rbdNormal.Checked == true)
            {
                Query += " and num_salarydiff_presentdays > 0 "; //and var_sevikamaster_cpsmscode is not null 
            }
            if (rbdzeroPrsnt.Checked == true)
            {
                Query += " and num_salarydiff_presentdays=0 ";
            }
                Query += " and var_sevikamaster_cpsmscode is not null ";
            //-------------------10-03-18------
            Query += " and var_salarydiff_billnocentral = '" + CentralBill + "' ";
            Query += " and var_salarydiff_billnostate = '" + StateBill + "' ";
            Query += " and var_salarydiff_billnofixed = '" + FixedBill + "' ";
            //-----------------------------------------
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

            string billdet = " select var_salarydiff_billnocentral Central,var_salarydiff_billnostate State,var_salarydiff_billnofixed Fixed from aoup_salarydiff_def ";
            billdet += " left join corpinfo on num_salarydiff_compid=bitid ";
            billdet += " where stateid =" + TblGetHoId.Rows[0]["hoid"].ToString();
            if (ddlDivision.SelectedValue != "")
            {
                billdet += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
            }
            if (ddlDistrict.SelectedValue != "")
            {
                billdet += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            
                billdet += " and var_salarydiff_cpsmscode is not null ";
           
            billdet += " and trunc(date_salarydiff_date)='" + lstdate + "' ";//num_salary_compid=1559 and
            //billdet += " and var_salarydiff_billnocentral is not null  ";
            billdet += " and var_salarydiff_billnofixed is not null and var_salarydiff_billnocentral is not null and var_salarydiff_billnostate is not null ";
            billdet += " GROUP by var_salarydiff_billnocentral,var_salarydiff_billnostate,var_salarydiff_billnofixed ";

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