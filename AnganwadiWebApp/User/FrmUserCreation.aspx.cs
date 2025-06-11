using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using System.IO;
using System.Text.RegularExpressions;
using AnganwadiLib.Business;

namespace ProjectManagement.User
{
    public partial class FrmUserCreation : System.Web.UI.Page
    {
        AnganwadiLib.Business.User1 ObjUser = new AnganwadiLib.Business.User1();

        DataTable tblGetUserId = new DataTable();
        Int32 InMode = 0;

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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"User Creation";
                Session["ResetGrd"] = Session["GrdLevel"];
                string Getcompid = "select hoid from companyview where brid=" + Session["GrdLevel"];

                DataTable TblGetcompid = (DataTable)MstMethods.Query2DataTable.GetResult(Getcompid);

                if (TblGetcompid.Rows.Count > 0)
                {
                    MstMethods.Dropdown.Fill(ddlDept, "aoup_department_def", "var_department_dept", "num_department_deptid", "num_department_brid='" + TblGetcompid.Rows[0]["hoid"].ToString() + "'", "");
                }
                MstMethods.Dropdown.Fill(ddlDesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "", "");

                if (Request.QueryString["@"] == "1")
                {
                    InMode = 1;
                }

                if (Request.QueryString["@"] == "2")
                {
                    InMode = 2;
                    txtUserID.Enabled = false;
                    GetUserDetails();
                }
            }
        }

        public void GetUserDetails()
        {
            String str = " select var_usermst_userid userid,var_usermst_userfullname username,date_usermst_validfrom validfrom,date_usermst_validupto validupto,num_usermst_desgid desgid, ";
            str += " num_usermst_deptid deptid from aoup_usermst_def where var_usermst_userid ='" + Session["UserIdMst"].ToString() + "'";

            DataTable TblUserDet = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblUserDet.Rows.Count > 0)
            {
                if (TblUserDet.Rows[0]["userid"].ToString() != null && TblUserDet.Rows[0]["userid"].ToString() != "")
                {
                    txtUserID.Text = TblUserDet.Rows[0]["userid"].ToString();
                }

                if (TblUserDet.Rows[0]["username"].ToString() != null && TblUserDet.Rows[0]["username"].ToString() != "")
                {
                    txtUserName.Text = TblUserDet.Rows[0]["username"].ToString();
                }

                if (TblUserDet.Rows[0]["desgid"].ToString() != null && TblUserDet.Rows[0]["desgid"].ToString() != "")
                {
                    ddlDesg.SelectedValue = TblUserDet.Rows[0]["desgid"].ToString();
                }

                if (TblUserDet.Rows[0]["deptid"].ToString() != null && TblUserDet.Rows[0]["deptid"].ToString() != "")
                {
                    ddlDept.SelectedValue = TblUserDet.Rows[0]["deptid"].ToString();
                }
                if (TblUserDet.Rows[0]["validfrom"].ToString() != null && TblUserDet.Rows[0]["validfrom"].ToString() != "")
                {
                    ActiveFrmDt.Text = Convert.ToDateTime(TblUserDet.Rows[0]["validfrom"].ToString()).ToString("dd/MM/yyyy");
                }
                if (TblUserDet.Rows[0]["validupto"].ToString() != null && TblUserDet.Rows[0]["validupto"].ToString() != "")
                {
                    ActiveTillDt.Text = Convert.ToDateTime(TblUserDet.Rows[0]["validupto"].ToString()).ToString("dd/MM/yyyy");
                }

                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                ddlDept.Enabled = false;
                ddlDesg.Enabled = false;
            }
            else
            {
                MessageAlert("User details not found", "../User/FrmUserCreation.aspx");
                return;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "")
            {
                MessageAlert(" Enter User ID ", "");
                return;
            }
            if (txtUserName.Text == "")
            {
                MessageAlert(" Please Insert User Name ", "");
                return;
            }
            if (ddlDesg.SelectedIndex == 0)
            {
                MessageAlert(" Please Select Designation ", "");
                return;
            }
            if (ActiveFrmDt.Value.ToString() == "")
            {
                MessageAlert(" Please Select From date ", "");
                return;
            }
            if (ActiveFrmDt.Value.ToString() == "")
            {
                MessageAlert(" Please Select Till date ", "");
                return;
            }
            if (ddlDept.SelectedIndex == 0)
            {
                MessageAlert(" Please Select Department ", "");
                return;
            }

            if (Request.QueryString["@"] == "1")
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

                ObjUser.Mode = 1;
                ObjUser.UserId = txtUserID.Text.Trim();
                ObjUser.Password = txtPassword.Text.Trim();
            }
            else
            {
                ObjUser.Mode = 2;
                ObjUser.UserId = txtUserID.Text.Trim();
                ObjUser.Password = "";
            }
            ObjUser.Brid = Session["GrdLevel"].ToString();

            ObjUser.UserDepartment = Convert.ToInt64(ddlDept.SelectedValue);
            ObjUser.UserDesignation = Convert.ToInt64(ddlDesg.SelectedValue);
            ObjUser.UserName = txtUserName.Text.Trim();
            ObjUser.ValidFrom = Convert.ToDateTime(ActiveFrmDt.Value);
            ObjUser.ValidUpto = Convert.ToDateTime(ActiveTillDt.Value);

            ObjUser.Insert();

            if (ObjUser.ErrCode == 9999)
            {
                Session["GrdLevel"] = Session["ResetGrd"];
                MessageAlert(ObjUser.ErrMsg, "../User/FrmUserList.aspx");
                return;
            }
            else
            {
                MessageAlert(ObjUser.ErrMsg, "");
                return;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }
    }
}