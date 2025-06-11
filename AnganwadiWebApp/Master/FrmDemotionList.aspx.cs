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
    public partial class FrmDemotionList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDemotionRef.aspx");
            LblGrdHead.Text = Session["LblGrdHead"].ToString();
        }

       
        protected void grdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowindex = grdList.SelectedRow.RowIndex;
            //Session["EduName"] = grdList.Rows[rowindex].Cells[9].Text.ToString();
            Int32 EduId = Convert.ToInt32(grdList.Rows[rowindex].Cells[11].Text);
            //if (EduId==7)
            //{
            //    Session["EduId"] = 1;
            //}
            //else
            //{
                Session["EduId"] = EduId;
           // }
            Session["AadharNo"] = txtAdharNo.Text != "" ? txtAdharNo.Text : null;
            Response.Redirect("~/Master/FrmDemotionMst.aspx?@=2");
        }
        protected void grdList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[11].Visible = false;
                
            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Master/FrmDemotionMst.aspx?@=1");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAdharNo.Text != "")
            {
                string str = " SELECT  c.divname, c.distname, c.cdponame, c.bitbitname,s.var_sevikamaster_name savikaName,date_sevikamaster_joindate joindate, ";
                str += " var_experience_text Exp,var_designation_desig desig,var_payscal_payscal payscal,var_education_educname educname,num_education_educid eduid  ";
                str += " FROM corpinfo c  INNER JOIN aoup_sevikamaster_def s ON bitid = num_sevikamaster_compid ";
                str += " inner join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
                str += " inner join aoup_payscal_def on num_payscal_payscalid=num_sevikamaster_payscalid ";
                str += " inner join aoup_education_def on num_education_educid=num_sevikamaster_educid ";
                str += " inner join aoup_experience_def on num_experience_id=num_sevikamaster_experience ";
                str += " WHERE  var_sevikamaster_aadharno = '" + txtAdharNo.Text + "' and var_payscal_active='Y' ";

                DataTable dtTbl = new DataTable();
                dtTbl = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                if (dtTbl.Rows.Count > 0)
                {
                    grdList.DataSource = dtTbl;
                    grdList.DataBind();
                    lblError.Text = "";
                }
                else
                {
                    grdList.DataSource = null;
                    grdList.DataBind();
                    lblError.Text = "No Record Found";
                }
            }
            else
            {
                MessageAlert("please Enter Aadhar Number", "");
                return;
            }
        }
    }
}