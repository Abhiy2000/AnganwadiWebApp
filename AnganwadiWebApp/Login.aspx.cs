using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Oracle.DataAccess.Client;
using AnganwadiLib.Data;
using System.IO;
using System.Globalization;

namespace ProjectManagement
{
    public partial class Login : System.Web.UI.Page
    {
        AnganwadiLib.Business.Login ObjLogIn = new AnganwadiLib.Business.Login();

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

        int line = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //
            //if()
            //{


            //}

            if (!IsPostBack)
            {
                //if (!Request.IsLocal && !Request.IsSecureConnection)
                //{
                //    String NewURL = "https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl;

                //    Response.Redirect(NewURL);
                //}
                //FillCaptcha(); // Comment for ICDS_TEST
                txtUserName.Focus();
                txtUserName.Text = "";
                txtPassword.Text = "";
                btnLogin.Focus();
                //this.txtPayConfCaptcha.Attributes.Add("onkeypress", "button_click(this,'" + this.btnLogin.ClientID + "')");               
            }

            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // DateTime dateTime = DateTime.ParseExact("31-12-2020 17:57:50", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                // Session["lastlogin"] = Convert.ToDateTime();
                if (txtUserName.Text == "")
                {
                    MessageAlert("User Id can not be blank", "");
                    txtUserName.Focus();
                    return;
                }

                if (txtPassword.Text == "")
                {
                    MessageAlert("Password can not be blank", "");
                    txtPassword.Focus();
                    return;
                }

                //if (Session["captcha"] != null) // Comment for ICDS_TEST
                //{
                //    if (txtPayConfCaptcha.Text != Session["captcha"].ToString())
                //    {
                //        MessageAlert("Please Enter Proper Captcha", "");
                //        txtPayConfCaptcha.Text = "";
                //        txtPayConfCaptcha.Focus();
                //        FillCaptcha();
                //        return;
                //    }
                //}
                line = 1;
                ObjLogIn.UserId = txtUserName.Text;
                ObjLogIn.Password = txtPassword.Text;

                ObjLogIn.UserLogin();
                line = 2;
                if (ObjLogIn.ErrCode == "-100")
                {
                    line = 3;
                    //txtPayConfCaptcha.Text = ""; // Comment for ICDS_TEST
                    //FillCaptcha(); // Comment for ICDS_TEST
                    line = 4;
                    Session["UserId"] = txtUserName.Text;
                    Session["CompId"] = ObjLogIn.CompId;
                    Session["brid"] = ObjLogIn.Brid;
                    Session["UserFullName"] = ObjLogIn.UserName;
                    Session["brcategory"] = ObjLogIn.brcategory;
                    line = 5;
                    if (ObjLogIn.LastLogin == "")
                    {
                        Session["lastlogin"] = "";
                    }
                    else
                    {
                        // Session["lastlogin"] = Convert.ToDateTime(ObjLogIn.LastLogin);\
                        Session["lastlogin"] = ObjLogIn.LastLogin;
                    }
                    if (ObjLogIn.LastLogout == "")
                    {
                        Session["lastlogout"] = "";
                    }
                    else
                    {
                        // Session["lastlogout"] = Convert.ToDateTime(ObjLogIn.LastLogout);
                        Session["lastlogout"] = ObjLogIn.LastLogout;
                    }
                    line = 6;
                    Session["lastchangepwd"] = ObjLogIn.Lastchangepwd;
                    Session["DesgId"] = ObjLogIn.Desgid;
                    Session["branchname"] = ObjLogIn.Branchname;
                    Session["companyname"] = ObjLogIn.Compname;
                    Session["desgname"] = ObjLogIn.DesgName;
                    line = 7;
                    Session["GrdLevel"] = ObjLogIn.CompId;

                    if (txtUserName.Text != "admin")
                    {
                        if (Session["lastchangepwd"].ToString() != "null")
                        {
                            DateTime PswdChngDate = Convert.ToDateTime(Session["lastchangepwd"]);

                            Int32 DayDiff = (Int32)DateAndTime.DateDiff(DateInterval.Day, PswdChngDate, DateTime.Now.Date, Microsoft.VisualBasic.FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);

                            if (DayDiff > 30)
                            {
                                Session["OldPassword"] = txtPassword.Text;
                                Response.Redirect("~/Admin/FrmResetPwdMst.aspx?@=2");
                                return;
                            }
                        }
                    }
                    line = 8;
                    // Comment for ICDS_TEST
                    //string str1 = "<script>window.open('HomePage/FrmDashboard.aspx', '', 'fullscreen=yes, toolbars=no, menubar=no, location=no, scrollbars=yes, resizable=no, status=yes, width=2000px, height=5000px');</script>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "str", str1);

                    // Response.Redirect("~/HomePage/FrmDashboard.aspx"); // UnComment for ICDS_TEST
                    // Response.Redirect("~/Master/FrmMenuList.aspx");
                    //('admin','ICDSAdmin','ICDS_HO_1','ICDS_HO_2')
                    if (txtUserName.Text == "admin" || txtUserName.Text == "ICDSAdmin" || txtUserName.Text == "ICDS_HO_1" || txtUserName.Text == "ICDS_HO_2")
                    {
                        Response.Redirect("~/HomePage/tablegraph.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/HomePage/FrmDashboard.aspx");
                    }
                }
                else
                {
                    MessageAlert(ObjLogIn.ErrMsg, "");
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(st.FrameCount - 1);
                // Get the line number from the stack frame
                var line1 = frame.GetFileLineNumber();
                MessageAlert(ex.Message.ToString() + "line:" + line + "c#line:" + line1, "");
            }
        }

        /* // Comment for ICDS_TEST
        protected void btnPayConfRefresh_Click(object sender, EventArgs e)
        {
            FillCaptcha();
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            FillCaptcha();
        }

        public void FillCaptcha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                Session["captcha"] = captcha.ToString();
                string ImageUrl = "~/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                imgPayConfCaptcha.ImageUrl = ImageUrl;
            }
            catch
            {
                throw;
            }
        }
        */
        protected void lnkCheckPaymentDetail_Click(object sender, EventArgs e)
        {
            Response.Write("<script>");
            Response.Write("window.open('Reports/FrmPayDetailReport.aspx','_blank')");
            Response.Write("</script>");
        }



    }
}