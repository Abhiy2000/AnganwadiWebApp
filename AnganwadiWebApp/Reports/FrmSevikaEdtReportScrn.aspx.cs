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
    public partial class FrmSevikaEdtReportScrn : System.Web.UI.Page
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

                    //ddlBeat_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlCDPO.SelectedValue.Trim() == "" || ddlCDPO.SelectedValue == null)
            {
                MessageAlert("Please Select CDPO!", "");
                ddlCDPO.Focus();
                return;
            }
            //if (ddlBeat.SelectedValue.Trim() == "" || ddlBeat.SelectedValue == null)
            //{
            //    MessageAlert("Please Select BIT!", "");
            //    ddlBeat.Focus();
            //    return;
            //}

            if (DtFromDate.Text == "")
            {
                MessageAlert("Please Select From date!", "");
                return;
            }
            if (DtToDate.Text == "")
            {
                MessageAlert("Please Select To-date!", "");
                return;
            }
            if (DtFromDate.Text != "" && DtToDate.Text != "")
            {
                if (Convert.ToDateTime(DtFromDate.Value).Date > Convert.ToDateTime(DtToDate.Value).Date)
                {
                    MessageAlert("From Date must be less than of To-date!", "");
                    return;
                }
            }

            LoadGrid();
        }

        public void LoadGrid()
        {
            string fromDate = "", todate = "";
            fromDate = Convert.ToDateTime(DtFromDate.Value).ToString("dd/MMM/yyyy");
            todate= Convert.ToDateTime(DtToDate.Value).ToString("dd/MMM/yyyy");

            //string Gethoid = "select hoid from companyview where brid=" + Session["GrdLevel"];

            String qry = "select  divname, distname, cdponame, bitbitname ,  var_sevikamaster_name sevikaname, ";
            qry += " (case when var_hoappr_status = 'A' or var_dc_apprstatus ='A' then 'APPROVED' else 'REJECTED' end) status, ";
            qry += "  to_char(dtold_sevikaedit_dob, 'dd/MM/yyyy')  old_dob ";
            qry += " ,to_char(dtold_sevikaedit_doj, 'dd/MM/yyyy')  old_joindate ";
            qry += " ,num_sevikaedit_oldaadhar old_aadharno   ";
            qry += " , to_char(dtnew_sevikaedit_dob,'dd/MM/yyyy') new_dob ";
            qry += " ,to_char(dtnew_sevikaedit_doj, 'dd/MM/yyyy') new_joindate ";
            qry += " ,num_sevikaedit_newaadhar new_aadharno ";
            qry += " from aoup_sevikamaster_def  inner join aoup_sevikaedit ";
            qry += " on num_sevikaedit_sevikaid = num_sevikamaster_sevikaid ";
            qry += " inner  join corpinfo on bitid = num_sevikamaster_compid ";
            qry += " where ( var_hoappr_status = 'A' or var_dc_apprstatus='A') and ( trunc(dt_hoapprdt) between   '" + fromDate + "' and '" + todate + "' ";
            qry += " or TRUNC (dat_dc_apprdt) BETWEEN '" + fromDate + "' and '" + todate + "'  ) ";
            qry += " and   cdpoid = " + ddlCDPO.SelectedValue;
            if (ddlBeat.SelectedValue.Trim() != "" && ddlBeat.SelectedValue != null)
            {
                qry += "and bitid = " + ddlBeat.SelectedValue + " ";
            }

            DataTable tblAttendlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(qry);

            if (tblAttendlist!=null && tblAttendlist.Rows.Count > 0)
            {
                GrdSevikaEdtStatusRpt.DataSource = tblAttendlist;
                GrdSevikaEdtStatusRpt.DataBind();
                GrdSevikaEdtStatusRpt.Visible = true;
                btnExport.Visible = true;

            }
            else
            {
                GrdSevikaEdtStatusRpt.DataSource = tblAttendlist;
                GrdSevikaEdtStatusRpt.DataBind();
                btnExport.Visible = false;
            }
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {


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

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename= SevikaEditStatus_Rpt" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    GrdSevikaEdtStatusRpt.AllowPaging = false;
                    GrdSevikaEdtStatusRpt.HeaderRow.BackColor = System.Drawing.Color.White;

                    foreach (TableCell cell in GrdSevikaEdtStatusRpt.HeaderRow.Cells)
                    {
                        cell.BackColor = GrdSevikaEdtStatusRpt.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GrdSevikaEdtStatusRpt.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GrdSevikaEdtStatusRpt.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GrdSevikaEdtStatusRpt.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GrdSevikaEdtStatusRpt.RenderControl(hw);
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                    GrdSevikaEdtStatusRpt.Visible = false;
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