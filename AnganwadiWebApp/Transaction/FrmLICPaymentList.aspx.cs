using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmLICPaymentList : System.Web.UI.Page
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString(); // "Sevika LIC Document Upload"
            if (!IsPostBack)
            {
                Session["GrdLevel"].ToString();
                LoadGrid();
            }
        }

        protected void LoadGrid()
        {
               String Query = "";
                Query += " Select divname,distname,cdponame,bitbitname,num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo,  ";
                Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason ExitReason,num_lic_payscal LastSalary,num_lic_licamount ClaimAmount from aoup_LIC_DEF  ";
                Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid  ";
                Query += " inner join corpinfo on bitid=num_lic_compid ";
                Query += " where img_lic_document is not null and var_lic_licapprovby is not null and var_lic_licapprovflag='A' and var_lic_flag_payment is null  ";

                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    grdLICDEF.DataSource = dtSevikaList;
                    ViewState["CurrentTable"] = dtSevikaList;
                    grdLICDEF.DataBind();
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                    MessageAlert(" Record Not Found ", "");
                    return;
                }
        }

        protected void grdLICDEF_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowindex = grdLICDEF.SelectedRow.RowIndex;
            Session["SevikaId"] = Convert.ToInt32(grdLICDEF.Rows[rowindex].Cells[1].Text);
            Session["Reason"] = grdLICDEF.Rows[rowindex].Cells[3].Text;
            Session["Amount"] = Convert.ToInt32(grdLICDEF.Rows[rowindex].Cells[4].Text);
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "redirectToNewWindow();", true);
        }
        protected void grdLICDEF_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;

            }
        }
    }
}