using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using System.IO;

namespace AnganwadiWebApp.Reports
{
    public partial class ProjTypeSummaryRefList : System.Web.UI.Page
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
                PnlSerch.Visible = false;
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Anganwadi wise Pay Sheet";

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFromdate.Text == "")
            {
                MessageAlert("Please select date", "");
                return;
            }
            DateTime FromDate = Convert.ToDateTime(txtFromdate.Value);

            LoadGrid();
        }

        public void LoadGrid()
        {
            DateTime FromDate = Convert.ToDateTime(txtFromdate.Value);
            string Gethoid = "select hoid from companyview where brid=" + Session["GrdLevel"];

            DataTable tblHoid = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Gethoid);

            String qry = " SELECT var_projecttype_prjtype ProjectType, count(num_salary_sevikaid) SevikaCount, ";
            qry += " sum(num_salary_central) Central, sum(num_salary_state) State, sum(num_salary_totalpaid) TotalSalary ";
            qry += " FROM aoup_salary_def";
            qry += " left join aoup_projecttype_def on num_projecttype_compid =num_salary_compid and num_projecttype_prjtypeid =num_salary_prjtypeid";
            qry += " left join companyview on num_salary_compid=brid";
            qry += " where hoid = '" + Convert.ToInt32(tblHoid.Rows[0]["hoid"].ToString()) + "' and trunc(date_salary_date)='" + FromDate.ToString("dd-MMM-yyyy") + "' ";
            qry += " group by var_projecttype_prjtype";

            //String qry1 = " SELECT var_projecttype_prjtype ProjectType, count(num_salary_sevikaid) SevikaCount, ";
            //qry1 += " sum(num_salary_central) Central, sum(num_salary_state) State, sum(num_salary_totalpaid) TotalSalary ,date_salary_date ";
            //qry1 += " FROM aoup_salary_def inner join aoup_projecttype_def on  num_projecttype_prjtypeid =num_salary_prjtypeid ";
            //qry1 += " where num_salary_compid=" + Session["GrdLevel"] + " and trunc(date_salary_date)='" + FromDate.ToString("dd/MMM/yyyy") + "' ";
            //qry1 += " group by var_projecttype_prjtype,date_salary_date ";

            DataTable tblAttendlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(qry);

            if (tblAttendlist.Rows.Count > 0)
            {
                grdTypSummaryRpt.DataSource = tblAttendlist;
                grdTypSummaryRpt.DataBind();
                grdTypSummaryRpt.Visible = true;
                btnExport.Visible = true;
                /*decimal total = Math.Round(tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("SevikaCount")));
                grdTypSummaryRpt.FooterRow.Cells[1].Text = total.ToString("0");

                decimal total1 = Math.Round(tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("Central")));
                grdTypSummaryRpt.FooterRow.Cells[2].Text = total1.ToString("0");

                decimal total2 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("State"));
                grdTypSummaryRpt.FooterRow.Cells[3].Text = total2.ToString("0");

                decimal total3 = tblAttendlist.AsEnumerable().Sum(row => row.Field<decimal>("TotalSalary"));
                grdTypSummaryRpt.FooterRow.Cells[4].Text = total3.ToString("0");*/
            }
            else
            {
                MessageAlert(" Record Not Found ", "");
                grdTypSummaryRpt.Visible = false;
                btnExport.Visible = false;
            }
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
                PnlSerch.Visible = false;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=" + LblGrdHead.Text + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    grdTypSummaryRpt.AllowPaging = false;
                    grdTypSummaryRpt.HeaderRow.BackColor = System.Drawing.Color.White;

                    foreach (TableCell cell in grdTypSummaryRpt.HeaderRow.Cells)
                    {
                        cell.BackColor = grdTypSummaryRpt.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grdTypSummaryRpt.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grdTypSummaryRpt.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grdTypSummaryRpt.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grdTypSummaryRpt.RenderControl(hw);
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                    grdTypSummaryRpt.Visible = false;
                    btnExport.Visible = false;
                }
            }
            catch (System.Threading.ThreadAbortException lException)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}