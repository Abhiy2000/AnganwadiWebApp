using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmAdditionalPayList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmAdditionalPayRef.aspx");
            LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Aganwadi Master"; //
        }

        protected void grdAddtionalPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void grdAddtionalPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowindex = grdAddtionalPay.SelectedRow.RowIndex;
            Session["UniqueId"] = grdAddtionalPay.Rows[rowindex].Cells[0].Text.ToString();
            Session["AadharNo"] = txtAdharNo.Text!=""?txtAdharNo.Text:null;
            Response.Redirect("~/master/FrmAdditionalPayMst.aspx?@=2");
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/master/FrmAdditionalPayMst.aspx?@=1");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAdharNo.Text != "")
            {
                string str = " select c.divname,c.distname,c.cdponame,c.bitbitname,s.var_sevikamaster_name savikaName ,s.num_sevikamaster_sevikaid SevikaId,var_sevikamaster_cpsmscode schemeId,a.var_addpay_type pay_type, ";
                str += " a.date_addpay_fromdate fromdate,a.date_addpay_todate todate, a.num_addpay_uniqueid uniqueid  from  corpinfo c ";
                str += " inner join aoup_sevikamaster_def s on bitid=num_sevikamaster_compid  ";
                str += " inner join Aoup_AddPay_Def a on a.num_addpay_compid=s.num_sevikamaster_compid and a.num_addpay_sevikaid=s.num_sevikamaster_sevikaid ";
                str += " where var_sevikamaster_aadharno='" + txtAdharNo.Text + "' and var_addpay_status='A'";
                DataTable dtTbl = new DataTable();
                dtTbl = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                if (dtTbl.Rows.Count > 0)
                {
                    grdAddtionalPay.DataSource = dtTbl;
                    grdAddtionalPay.DataBind();
                    lblError.Text = "";
                }
                else
                {
                    grdAddtionalPay.DataSource = null;
                    grdAddtionalPay.DataBind();
                    lblError.Text = "No Record Found";
                }
            }
            else
            {
                MessageAlert("please Enter Aadhar Number","");
                return;
            }
        }
    }
}