using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using System.Drawing;
using System.IO;

namespace AnganwadiWebApp.Reports
{
    public partial class RptRegisterSevika : System.Web.UI.Page
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Registerd Savika Report";
            if (!IsPostBack)
            {
                //LoadGrid();
                String qry = " select var_designation_desig, num_designation_desigid from (select distinct(num_designation_desigid) num_designation_desigid,var_designation_desig from aoup_designation_def ";
                qry += " inner join aoup_payscal_def on num_payscal_desigid = num_designation_desigid ";
                qry += " where var_payscal_active='Y' and var_designation_flag in ('W','H')  order by trim(var_designation_desig)) ";

                MstMethods.Dropdown.Fill(ddlDesg, "", "var_designation_desig", "num_designation_desigid", "", qry);
                //MstMethods.Dropdown.Fill(ddlDesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "var_designation_flag in ('W','H')  order by trim(var_designation_desig)", "");
                Session["GrdLevel"].ToString();
            }
        }

        protected void GrdRegSevika_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdRegSevika.SelectedRow;
            Session["SevikaId"] = row.Cells[1].Text;
            Session["AnganId"] = row.Cells[2].Text;
        }

        protected void GrdRegSevika_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }

        #region "Export Excel"
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //if (ddlDesg.SelectedIndex != 0)
            //{
            LoadGrid();
            //}
            //else
            //{
            //    MessageAlert("Please select Designation", "");
            //    return;
            //}
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
            String GetBrId = " select brid from companyview where hoid=" + Session["GrdLevel"] + " and brcategory=5 ";

            DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(GetBrId);

            String Query = "";
            if (TblGetBrid.Rows.Count > 0)
            {
                String brid = "";
                for (int i = 0; i < TblGetBrid.Rows.Count; i++)
                {
                    brid += "'" + TblGetBrid.Rows[i]["brid"] + "',";
                }
                brid = brid.Remove(brid.Length - 1);

                Query += " select var_sevikamaster_name fullName,var_sevikamaster_name OffName, ";
                Query += " 'Female' gender,var_sevikamaster_address address1,null address2,null address3,distname district,statename state,'India' country,var_bank_bankname bank, ";
                Query += " var_bankbranch_ifsccode ifsc,var_sevikamaster_accno accno,var_sevikamaster_aadharno aadharNo,var_sevikamaster_pincode pincode, ";
                Query += " var_sevikamaster_sevikacode SchemeSpeciID,ROUND((num_payscal_wages)/100 * num_payscal_central) CenterShareAmt, ";
                Query += " ROUND((((num_payscal_wages)/100 * num_payscal_state)+num_payscal_fixed)) StateShareAmt from aoup_sevikamaster_def a ";
                Query += " left join aoup_bankbranch_def b on a.num_sevikamaster_branchid=b.num_bankbranch_branchid ";
                Query += " left join aoup_bank_def c on c.num_bank_bankid=b.num_bankbranch_bankid ";
                Query += " inner join corpinfo c on a.num_sevikamaster_compid=c.bitid ";
                Query += " left join aoup_payscal_def on num_payscal_payscalid = a.num_sevikamaster_payscalid ";
                Query += " inner join aoup_designation_def d on d.num_designation_desigid=a.num_sevikamaster_desigid ";
                Query += " where stateid=" + Session["GrdLevel"];//num_sevikamaster_compid in (" + brid + ")  ";
                if (ddlDesg.SelectedIndex != 0)
                {
                    Query += " and num_designation_desigid=" + ddlDesg.SelectedValue;
                }
                Query += " and var_sevikamaster_cpsmscode is null and var_payscal_active='Y' ";
                Query += " order by num_sevikamaster_sevikaid ";

                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    ExportToExcel(dtSevikaList, "Un-Registerd_Sevika_Report_as_on_" + DateTime.Now);
                }
                else
                {
                    GrdRegSevika.DataSource = null;
                    GrdRegSevika.DataBind();
                    MessageAlert(" Record Not Found ", "");
                    return;
                }
            }
        }

        protected void GrdRegSevika_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdRegSevika.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}