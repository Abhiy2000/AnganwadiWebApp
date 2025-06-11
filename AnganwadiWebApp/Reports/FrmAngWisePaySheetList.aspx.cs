using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using AnganwadiLib.Methods;
using System.IO;
using System.Drawing;

namespace AnganwadiWebApp.Reports
{
    public partial class FrmAngWisePaySheetList : System.Web.UI.Page
    {
        int days = 0;

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
            if (!IsPostBack)
            {
                PnlSerch.Visible = true;
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Anganwadi wise Pay Sheet";             
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

        //protected void GrdCompanyList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    
        //}

        public void LoadGrid()
        {
            //DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            //string lstdate = Lastdate.ToString("dd-MMM-yyyy");
            //DateTime Days = System.DateTime.Now;
            //Days = Convert.ToDateTime(txtFromdate.Value);
            ////DateTime FromDate = Convert.ToDateTime(txtFromdate.Value);
            //int month = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("MM")));
            //String month1 = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Days.Month);
            //int year = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("yyyy")));
            //days = DateTime.DaysInMonth(year, month);
            //String lstdate = Convert.ToString(days + "-" + month1 + "-" + year);

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            String qry = "SELECT divname Division,distname District,cdponame CDPO,bitbitname BIT,var_angnwadimst_angnname Anganwadi,num_salary_sevikaid sevikaid, var_sevikamaster_name SevikaName, ";
            qry += " var_sevikamaster_aadharno Aadhar,var_sevikamaster_cpsmscode PFMS_Code,var_sevikamaster_sevikacode SchemeSpecificCode,var_designation_desig Designation, ";
            qry += " nvl(num_salary_totaldays,0) TotalDays, nvl(num_salary_presentdays,0) Present, nvl(num_salary_absentdays,0) absent, nvl(num_salary_central,0) Central, ";
            qry += " nvl(num_salary_state,0) State, nvl(num_salary_fixed,0) Fixed, nvl(num_salary_allowance1,0) Allowance1,";
            qry += " nvl(num_salary_allowance2,0) allowance2, nvl(num_salary_deduct1,0) Deduction1, nvl(num_salary_deduct2,0) Deduction2,nvl(num_salary_add_tribal,0) PayTribal,nvl(num_salary_add_center,0) AddCenter,nvl(num_salary_add_state,0) AddState, nvl(num_salary_totalpaid,0) TotalWages";
            qry += " FROM aoup_salary_def inner join aoup_angnwadimst_def on num_salary_compid=num_angnwadimst_compid and num_salary_anganid =num_angnwadimst_angnid ";
            qry += " inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_salary_sevikaid ";
            qry += " inner join aoup_designation_def on num_designation_desigid=num_salary_desigid ";
            qry += " inner join corpinfo on num_salary_compid=bitid ";
            qry += " where stateid= '" + TblGetHoId.Rows[0]["hoid"].ToString() + "' and trunc(date_salary_date)='" + lstdate + "' and var_salary_authorisedby is not null ";

            if (ddlDivision.SelectedValue != "")
            {
                qry += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
            }
            if (ddlDistrict.SelectedValue != "")
            {
                qry += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            if (ddlCDPO.SelectedValue != "")
            {
                qry += " and cdpoid=" + Convert.ToInt32(ddlCDPO.SelectedValue);
            }
            if (ddlBeat.SelectedValue != "")
            {
                qry += " and bitid=" + Convert.ToInt32(ddlBeat.SelectedValue);
            }
            qry += " order by var_sevikamaster_name ";

            //qry += " where  num_salary_compid= '" + Convert.ToInt32(Session["GrdLevel"]) + "' and trunc(date_salary_billfrom) > = '" + Convert.ToDateTime(txtFromdate.Value).ToString("dd-MMM-yyyy") + "' ";
            //qry += " and num_salary_billno is not null ";
            //Query += " where trunc(date_dataupload_insdt) >= '" + FromDate.ToString("dd/MMM/yyyy") + "' and trunc(date_dataupload_insdt) <= '" + ToDate.ToString("dd/MMM/yyyy") + "'";

            DataTable tblAttendlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(qry);
            ViewState["tblAttendlist"] = tblAttendlist;

            if (tblAttendlist.Rows.Count > 0)
            {
                grdPaySheetRpt.DataSource = tblAttendlist;
                grdPaySheetRpt.DataBind();
                grdPaySheetRpt.Visible = true;
                btnExport.Visible = true;

                //Calculate Sum and display in Footer Row
                decimal total = Math.Round(tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("Central")));
                grdPaySheetRpt.FooterRow.Cells[14].Text = total.ToString("0");

                decimal total1 = Math.Round(tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("State")));
                grdPaySheetRpt.FooterRow.Cells[15].Text = total1.ToString("0");

                decimal total2 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("Fixed"));
                grdPaySheetRpt.FooterRow.Cells[16].Text = total2.ToString("0");

                decimal total3 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("Allowance1"));
                grdPaySheetRpt.FooterRow.Cells[17].Text = total3.ToString("0");

                decimal total4 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("allowance2"));
                grdPaySheetRpt.FooterRow.Cells[18].Text = total4.ToString("0");

                decimal total5 = Math.Round(tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("Deduction1")));
                grdPaySheetRpt.FooterRow.Cells[19].Text = total5.ToString("0");

                decimal total6 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("Deduction2"));
                grdPaySheetRpt.FooterRow.Cells[20].Text = total6.ToString("0");

                decimal total7 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("PayTribal"));
                grdPaySheetRpt.FooterRow.Cells[21].Text = total7.ToString("0");

                decimal total8 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("AddCenter"));
                grdPaySheetRpt.FooterRow.Cells[22].Text = total8.ToString("0");

                decimal total9 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("AddState"));
                grdPaySheetRpt.FooterRow.Cells[23].Text = total9.ToString("0");

                decimal total10 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("TotalWages"));
                grdPaySheetRpt.FooterRow.Cells[24].Text = total10.ToString("0");

                tblAttendlist.Rows.Add(null, null, null, null, null, null, null, null, null, "Total", null, null, null, null, tblAttendlist.Compute("sum(Central)", string.Empty), tblAttendlist.Compute("sum(State)", string.Empty), tblAttendlist.Compute("sum(Fixed)", string.Empty), tblAttendlist.Compute("sum(Allowance1)", string.Empty), tblAttendlist.Compute("sum(allowance2)", string.Empty), tblAttendlist.Compute("sum(Deduction1)", string.Empty), tblAttendlist.Compute("sum(Deduction2)", string.Empty), tblAttendlist.Compute("sum(PayTribal)", string.Empty), tblAttendlist.Compute("sum(AddCenter)", string.Empty), tblAttendlist.Compute("sum(AddState)", string.Empty), tblAttendlist.Compute("sum(TotalWages)", string.Empty));
                ViewState["tblAttendlist"] = tblAttendlist;

                if (ddlDistrict.SelectedValue == "" && ddlCDPO.SelectedValue == "" && ddlBeat.SelectedValue == "")
                {
                    grdPaySheetRpt.Visible = false;
                    btnExport.Visible = false;
                    ExportToExcel((DataTable)ViewState["tblAttendlist"], "PaySheet_Report_as_on_" + DateTime.Now);
                }
            }
            else
            {
                MessageAlert(" Record Not Found ", "");
                grdPaySheetRpt.Visible = false;
                btnExport.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (txtFromdate.Text == "")
            //{
            //    MessageAlert("Please select from date", "");
            //    return;
            //}
            if (ddlMonth.SelectedValue == "")
            {
                MessageAlert(" Please Select Month ", "");
                return;
            }
            if (ddlYear.SelectedValue == "")
            {
                MessageAlert(" Please Select Year ", "");
                return;
            }

            LoadGrid();
        }

        protected void grdPaySheetRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[1].Visible = false;
                //e.Row.Cells[2].Visible = false;
                //e.Row.Cells[3].Visible = false;
                //e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
            }
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
            }

            else
            {
                //PnlSerch.Visible = false;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel((DataTable)ViewState["tblAttendlist"], "PaySheet_Report_as_on_" + DateTime.Now);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
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
            //ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 3).ToString(), "4"));
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();

            ddlYear.SelectedItem.Text = System.DateTime.Now.Year.ToString();
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

        protected void grdPaySheetRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPaySheetRpt.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}