using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Admin
{
    public partial class FrmResetPwdMst : System.Web.UI.Page
    {
        BoResetPwd ObjResetPwd = new BoResetPwd();

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
            if (Session["UserFullName"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Admin" + " >> " + "Reset Password";//Session["LblGrdHead"].ToString();//"User Creation";
                if (Request.QueryString["@"] == "2")
                {
                    Session["AccessUserId"] = Session["UserId"];
                    MessageAlert(" Your current password has expired.<br/> Please change it before proceeding. ", "");
                    //Master.LnkSignoutE.Enabled = false;
                    //Master.litE.Visible = false;
                    //Master.hrefDashE.Enabled = false;
                    BtnCancel.Visible = false;
                }
                GetUserDetails();
            }
        }

        public void GetUserDetails()
        {
            String str = " select var_usermst_userid userid,var_usermst_userfullname username,date_usermst_validfrom validfrom,date_usermst_validupto validupto,num_usermst_desgid desgid, ";
            str += " var_designation_desig desg,var_department_dept dept, ";
            str += " num_usermst_deptid deptid from aoup_usermst_def ";
            str += " LEFT join aoup_designation_def on num_designation_desigid =num_usermst_desgid left join aoup_department_def on num_department_deptid=num_usermst_deptid ";
            str += " where var_usermst_userid ='" + Session["AccessUserId"].ToString() + "'";

            DataTable TblUserDet = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblUserDet.Rows.Count > 0)
            {
                if (TblUserDet.Rows[0]["userid"].ToString() != null && TblUserDet.Rows[0]["userid"].ToString() != "")
                {
                    lblUserId.Text = TblUserDet.Rows[0]["userid"].ToString();
                }

                if (TblUserDet.Rows[0]["username"].ToString() != null && TblUserDet.Rows[0]["username"].ToString() != "")
                {
                    lblUserName.Text = TblUserDet.Rows[0]["username"].ToString();
                }

                if (TblUserDet.Rows[0]["desg"].ToString() != null && TblUserDet.Rows[0]["desg"].ToString() != "")
                {
                    lblDesg.Text = TblUserDet.Rows[0]["desg"].ToString();
                }

                if (TblUserDet.Rows[0]["dept"].ToString() != null && TblUserDet.Rows[0]["dept"].ToString() != "")
                {
                    lblDept.Text = TblUserDet.Rows[0]["dept"].ToString();
                }
            }
            else
            {
                MessageAlert("User details not found", "../Admin/FrmResetPwdList.aspx");
                return;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                MessageAlert("Password can not be blank.", "");
                txtPassword.Focus();
                return;
            }

            if (txtConfirmPassword.Text == "")
            {
                MessageAlert("Confirm Password can not be blank.", "");
                txtConfirmPassword.Focus();
                return;
            }

            if (txtPassword.Text.ToString() != txtConfirmPassword.Text.ToString())
            {
                MessageAlert("Password & Confirm password should be same", "");
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != "")
            {
                if (txtPassword.Text.Length < 8)
                {
                    MessageAlert("Password length can not be less than eight characters", "");
                    return;
                }
            }

            Boolean Flag;
            string Msg;

            MstMethods.PasswordStrength(txtPassword.Text, out Flag, out Msg);

            if (Flag == true)
            {
            }
            else
            {
                MessageAlert(Msg, "");
                return;
            }

            ObjResetPwd.UserId = lblUserId.Text.Trim();
            ObjResetPwd.NewPassword = txtPassword.Text.Trim();
            ObjResetPwd.Insby = Session["UserId"].ToString();
            ObjResetPwd.Remark = txtRemark.Text.Trim();

            ObjResetPwd.BoResetPwd_1();

            if (ObjResetPwd.ErrCode == 9999)
            {
                MessageAlert(ObjResetPwd.ErrMsg, "../HomePage/FrmDashboard.aspx");
                return;
            }
            else
            {
                MessageAlert(ObjResetPwd.ErrMsg, "");
                return;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }
    }
}