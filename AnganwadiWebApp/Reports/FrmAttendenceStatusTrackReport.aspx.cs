using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AnganwadiWebApp.Reports
{
    public partial class FrmAttendenceStatusTrackReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();
                BindDataToGrid();
                lblError.Text = "";
            }
        }
        protected void BindDataToGrid()
        {
            String query = "select * from view_attend_tracking";
            DataTable dtTbl = new DataTable();
            dtTbl = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);
            if (dtTbl.Rows.Count > 0)
            {
                grdMonAttendance.DataSource = dtTbl;
                grdMonAttendance.DataBind();
                ViewState["TblMonAttendance"] = dtTbl;
                lblError.Text = "";
            }
            else
            {
                grdMonAttendance.DataSource = null;
                grdMonAttendance.DataBind();
                lblError.Text = "No Record Found";
            }
        }

        protected void grdMonAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMonAttendance.PageIndex = e.NewPageIndex;
            BindDataToGrid();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (grdMonAttendance.Rows.Count > 0)
            {
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                DataTable DtMonAttendance = (DataTable)ViewState["TblMonAttendance"];

                GridView1.DataSource = DtMonAttendance;
                GridView1.DataBind();

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=AttendenceStatusTrackReport" + System.DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                GridView1.RenderControl(htmlWrite);
                Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                Response.Write(stringWrite.ToString());
                Response.End();
            }
        }
    }
}