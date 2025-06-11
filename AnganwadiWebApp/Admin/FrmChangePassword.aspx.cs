using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;

namespace ProjectManagement.Admin
{
    public partial class FrmChangePassword : System.Web.UI.Page
    {
        AnganwadiLib.Business.Cls_Business_ChangePassword ObjChangePass = new AnganwadiLib.Business.Cls_Business_ChangePassword();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../FrmSessionLogOut.aspx?@=2");
            }
            if (!IsPostBack)
            {
                TxtUserName.Text = Session["UserId"].ToString();
                LblGrdHead.Text = "Admin" + " >> " + "Change Password";
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (TxtUserName.Text == "")
            {
                MessageAlert("User Name cannot be blank", "");
                return;
            }
            if (TxtOldPwd.Text == "")
            {
                MessageAlert("Old password cannot be blank", "");
                return;
            }
            if (TxtNewPwd.Text == "")
            {
                MessageAlert("New password cannot be blank", "");
                return;
            }
            if (TxtConfirmPwd.Text == "")
            {
                MessageAlert("Confirm password cannot be blank", "");
                return;
            }
            if (TxtOldPwd.Text.ToString().ToUpper().Trim() == TxtNewPwd.Text.ToString().ToUpper().Trim())
            {
                MessageAlert("Old Password & New Password can not be same", "");
                return;
            }
            if (TxtNewPwd.Text.ToString().ToUpper().Trim() != TxtConfirmPwd.Text.ToString().ToUpper().Trim())
            {
                MessageAlert("New Password & Confirm Password does not match", "");
                return;
            }
            if (Session["UserId"].ToString().ToUpper().Trim() == TxtNewPwd.Text.ToUpper().Trim())
            {
                MessageAlert("User Name & New Password can not be same", "");
                return;
            }
            if (TxtNewPwd.Text != "")
            {
                if (TxtNewPwd.Text.Length < 8)
                {
                    MessageAlert("New Password length can not be less than eight characters", "");
                    return;
                }
            }

            Boolean Flag;
            string Msg;

            MstMethods.PasswordStrength(TxtNewPwd.Text, out Flag, out Msg);

            if (Flag == true)
            {
            }
            else
            {
                MessageAlert(Msg, "");
                return;
            }

            string oldpwd = TxtOldPwd.Text;
            string newpwd = TxtNewPwd.Text;
            ObjChangePass.OldPassword = Convert.ToString(oldpwd);
            ObjChangePass.NewPassword = Convert.ToString(newpwd);
            ObjChangePass.UserId = Session["UserId"].ToString();

            ObjChangePass.UpDate();

            if (ObjChangePass.ErrorCode == 9999)
            {
                TxtOldPwd.Text = "";
                TxtNewPwd.Text = "";
                TxtConfirmPwd.Text = "";

                MessageAlert(ObjChangePass.ErrorMessage, "../HomePage/FrmDashboard.aspx");

                return;
            }
            else
            {
                MessageAlert(ObjChangePass.ErrorMessage, "");
                return;
            }
        }

        protected void btnCancel_Cilck(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx");
            return;
        }
    }
}
