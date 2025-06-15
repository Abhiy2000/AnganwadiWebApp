using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmApplicationVerifyList : System.Web.UI.Page
    {
        #region "MeesageAlert"
        public void MessageAlert(String Message, String WindowsLocation)
        {
            String str = "";

            str = "alert('|| " + Message + " ||');";

            if (WindowsLocation != "")
            {
                str += "window.location = '" + WindowsLocation + "';";
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, str, true);
            return;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDepartmentRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();
                GetDetails();
            }
        }
        public void GetDetails()
        {
            string qry = " select COMPID, APPLIID, APPLINO, APPNAME, PORTID, POSITION, ANGNID, ANGNNAME, EDUQUALID, ";
            qry += " EDUQULI_NAME, MARITALID, MARISTATUS_NAME, PROJID, PROJNAME from vw_applidtls ";
            qry += " where COMPID = '" + Session["GrdLevel"].ToString() + "' and DOCVERIFY is null and AUTHSTATUS is null ";
            DataTable tblgrdbind = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
            if (tblgrdbind.Rows.Count > 0)
            {
                Session["tblgrdbind"] = tblgrdbind;
                GrdApplication.Visible = true;
                GrdApplication.DataSource = tblgrdbind;
                GrdApplication.DataBind();
            }
            else
            {
                GrdApplication.DataSource = null;
                GrdApplication.DataBind();
            }
        }

        protected void GrdApplication_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[7].Visible = false;
            }
        }

        protected void GrdApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdApplication.SelectedRow;
            Session["AppId"] = row.Cells[2].Text;
            Response.Redirect("../Transaction/FrmApplicationVerifyMst.aspx");
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx");
        }
    }
}