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
    public partial class FrmAuditTrail : System.Web.UI.Page
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
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();
            }
        }

        #region "Export Excel"
        protected void btnExport_Click(object sender, EventArgs e)
        {
            LoadData();
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

        public void LoadData()
        {
            if (dtFrmDate.Text == "")
            {
                MessageAlert("Please select from date", "");
                return;
            }
            if (dtToDate.Text == "")
            {
                MessageAlert("Please select from date", "");
                return;
            }
            DateTime frmdt = Convert.ToDateTime(dtFrmDate.Value);
            DateTime todt = Convert.ToDateTime(dtToDate.Value);
            try
            {
                String str = "select date_activity_usertime,var_activity_userid,var_activity_pagetitle,var_activity_activity,var_activity_details from aoup_activity_log ";
                str += " where trunc(date_activity_usertime) >= '" + frmdt.ToString("dd-MMM-yyyy")+ "' and trunc(date_activity_usertime) <='" + todt.ToString("dd-MMM-yyyy") + "'";
                if (txtUserId.Text != "")
                {
                    str += " and var_activity_userid='" + txtUserId.Text.Trim()+"'";
                }
                str += " order by date_activity_usertime desc";

                DataTable dtList = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                if (dtList.Rows.Count > 0)
                {
                    ExportToExcel(dtList, "Audit_Trail_Report_as_on_" + DateTime.Now);
                }
                else
                {
                    MessageAlert(" Record not Found for selected Date", "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }        
    }
}