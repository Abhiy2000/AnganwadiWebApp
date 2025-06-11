using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Reports
{
    public partial class RptDeductionReport : System.Web.UI.Page
    {
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Deduction Report";
            //dtSalDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                // LoadGrid();
                //dtSalDate.Focus();
                PnlSerch.Visible = true;
                btnExport.Visible = false;
                divDetails.Visible = false;
                divSummery.Visible = false;
                //----------------------------------
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

                    ddlDivision_SelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDistrict_SelectedIndexChanged(sender, e);

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

                    ddlCDPO_SelectedIndexChanged(sender, e);

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

                    ddlBeat_SelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                    PnlSerch.Visible = true;
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (rbdDetail.Checked == true)
            {
                if (ViewState["dtDetails"] != null)
                {
                    ExportToExcel((DataTable)ViewState["dtDetails"], "Deduction_Details_Report" + DateTime.Now.ToString("ddMMyyyy"));
                }
                else
                {
                    MessageAlert("Something went Wrong", "");
                    return;
                }
            }
            else if (rbdSummery.Checked == true)
            {
                if (ViewState["dtSummery"] != null)
                {
                    ExportToExcel((DataTable)ViewState["dtSummery"], "Deduction_Summery_Report" + DateTime.Now.ToString("ddMMyyyy"));
                }
                else
                {
                    MessageAlert("Something went Wrong", "");
                    return;
                }
            }
        }
        protected void GrdDeductionReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdDeductionReport.PageIndex = e.NewPageIndex;
            GetData();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue == "")
            {
                MessageAlert("Please Select Division", "");
                return;
            }
            if (dtSalDate.Value.ToString() == "")
            {
                MessageAlert("Please Select Month Date", "");
                return;
            }
            GetData();
        }

        protected void ddlBeat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBeat.SelectedValue.ToString() != "")
            {
                PnlSerch.Visible = true;
            }
        }

        protected void ddlCDPO_SelectedIndexChanged(object sender, EventArgs e)
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
                PnlSerch.Visible = true;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
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
                PnlSerch.Visible = true;
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
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
                PnlSerch.Visible = true;
            }
            else
            {
            }
        }

        public void GetData()
        {
            String Query1 = " select parentid from companyview where brid='" + ddlDivision.SelectedValue.ToString() + "' ";
            DataTable tblState = (DataTable)MstMethods.Query2DataTable.GetResult(Query1);
            Session["StateID"] = null;
            if (tblState.Rows.Count > 0)
            {
                Session["StateID"] = tblState.Rows[0]["parentid"].ToString();
            }
            DateTime SalDate = Convert.ToDateTime(dtSalDate.Value);
            var lastDayOfMonth = DateTime.DaysInMonth(SalDate.Year, SalDate.Month);
            string Salarydate = (lastDayOfMonth.ToString() + "-" + SalDate.ToString("MMM") + "-" + SalDate.Year.ToString());
            if (rbdDetail.Checked == true)
            {
                String Query = "";
                Query += " select divname, distname, cdponame, cdpocode, var_sevikamaster_name Beneficiary,var_sevikamaster_aadharno aadharno, ";
                Query += " num_salary_deduct1 Amount ";
                Query += "From aoup_salary_def ";
                Query += "  inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid = num_salary_sevikaid ";
                Query += "  inner join corpinfo on bitid = num_salary_compid ";
                Query += " where 1=1 ";
                if (Session["StateID"] != null)
                {
                    Query += " and stateid='" + Session["StateID"] + "' ";
                }
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
                Query += " and date_salary_date = '" + Salarydate + "' and num_salary_deduct1> 0 ";
                Query += "   order by divid,distid,cdpoid,bitid,var_sevikamaster_name ";
                //Convert.ToDateTime(Salarydate).ToString("dd-MMM-yyyy")
                DataTable dtDetails = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
                if (dtDetails.Rows.Count > 0)
                {
                    divDetails.Visible = true;
                    divSummery.Visible = false;
                    GrdDeductionReport.Visible = true;
                    GrdSummery.Visible = false;
                    btnExport.Visible = true;
                    ViewState["dtDetails"] = dtDetails;
                    GrdDeductionReport.DataSource = dtDetails;
                    GrdDeductionReport.DataBind();
                }
                else
                {
                    divDetails.Visible = false;
                    divSummery.Visible = false;
                    GrdDeductionReport.Visible = false;
                    GrdSummery.Visible = false;
                    btnExport.Visible = false;
                    MessageAlert("Record Not Found", "");
                    return;
                }
            }
            else if (rbdSummery.Checked == true)
            {
                String Query = "";
                Query += " select divid, divname, distid, distname, cdpoid, cdponame, cdpocode, sum(num_salary_deduct1)Amount ";
                Query += "  From aoup_salary_def ";
                Query += " inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid = num_salary_sevikaid ";
                Query += "   inner join corpinfo on bitid = num_salary_compid ";
                Query += " where 1=1 ";
                if (Session["StateID"] != null)
                {
                    Query += " and stateid='" + Session["StateID"] + "' ";
                }
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
                Query += " and date_salary_date = '" + Salarydate + "' and num_salary_deduct1 > 0 ";
                Query += "  group by divid, divname, distid, distname, cdpoid, cdponame, cdpocode ";
                Query += "   order by divid, distid, cdpoid ";
                DataTable dtDetails = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
                if (dtDetails.Rows.Count > 0)
                {
                    divDetails.Visible = false;
                    divSummery.Visible = true;
                    GrdDeductionReport.Visible = false;
                    GrdSummery.Visible = true;
                    ViewState["dtSummery"] = dtDetails;
                    GrdSummery.DataSource = dtDetails;
                    GrdSummery.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    divDetails.Visible = false;
                    divSummery.Visible = false;
                    GrdDeductionReport.Visible = false;
                    GrdSummery.Visible = false;
                    btnExport.Visible = false;
                    MessageAlert("Record Not Found", "");
                    return;
                }
            }
            else
            {
                Session["StateID"] = null;
            }
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
    }
}