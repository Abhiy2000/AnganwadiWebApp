using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;

namespace ANCL_MarketWeb.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {

        #region "cross site request forgery" 
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        protected void Page_Init(object sender, EventArgs e)
        {
            //First, check for the existence of the Anti-XSS cookie  
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            //If the CSRF cookie is found, parse the token from the cookie.  
            //Then, set the global page variable and view state user  
            //key. The global variable will be used to validate that it matches in the view state form field in the Page.PreLoad  
            //method.  
            if (requestCookie != null
            && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                //Set the global token variable so the cookie value can be  
                //validated against the value in the view state form field in  
                //the Page.PreLoad method.  
                _antiXsrfTokenValue = requestCookie.Value;
                //Set the view state user key, which will be validated by the  
                //framework during each request  
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            //If the CSRF cookie is not found, then this is a new session.  
            else
            {
                //Generate a new Anti-XSRF token  
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                //Set the view state user key, which will be validated by the  
                //framework during each request  
                Page.ViewStateUserKey = _antiXsrfTokenValue;
                //Create the non-persistent CSRF cookie  
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    //Set the HttpOnly property to prevent the cookie from  
                    //being accessed by client side script  
                    HttpOnly = true,
                    //Add the Anti-XSRF token to the cookie value  
                    Value = _antiXsrfTokenValue
                };
                //If we are using SSL, the cookie should be set to secure to  
                //prevent it from being sent over HTTP connections  
                if (FormsAuthentication.RequireSSL &&
                Request.IsSecureConnection)
                    responseCookie.Secure = true;
                //Add the CSRF cookie to the response  
                Response.Cookies.Set(responseCookie);
            }
            Page.PreLoad += master_Page_PreLoad;
        }
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            //During the initial page load, add the Anti-XSRF token and user  
            //name to the ViewState  
            if (!IsPostBack)
            {
                //Set Anti-XSRF token  
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                //If a user name is assigned, set the user name  
                ViewState[AntiXsrfUserNameKey] =
                Context.User.Identity.Name ?? String.Empty;
            }
            //During all subsequent post backs to the page, the token value from  
            //the cookie should be validated against the token in the view state  
            //form field. Additionally user name should be compared to the  
            //authenticated users name  
            else
            {
                //Validate the Anti-XSRF token  
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] !=
                (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of  Anti - XSRF token failed.");
                }
            }
        }
        #endregion
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
            if (Session["UserFullName"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            //*******For User Direct Url Access *********************//  
            //Done by Vaishnavi
            //Date : 26/06/2024     
            if (Request.UrlReferrer != null)
            {
                string referrerUrl = Request.UrlReferrer.ToString();
            }
            else
            {
                String AbsolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                string[] parts = AbsolutePath.Split('/');
                int count = parts.Length;
                string url = "/" + parts[count - 2] + "/" + parts[count - 1];
            }
            /************************End**************************/

            string PageUrl = this.Page.AppRelativeVirtualPath;
            String ErrorMsg = "";

            if (!IsPostBack)
            {
                GetLoginDetails();
                MenuAccess();
            }

            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
        }

        public void GetLoginDetails()
        {
            lblUserName.Text = Session["UserFullName"].ToString();
            lblDesigName.Text = Session["desgname"].ToString();
            lblLastLogIn.Text = Session["lastlogin"].ToString();
            lblLastLogOut.Text = Session["lastlogout"].ToString();
        }

        public void MenuAccess()
        {
            String MenuQuery = "";
            string query = "select hoid from companyview where brid=" + Session["CompId"].ToString();
            DataTable TblCompDet = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (TblCompDet.Rows.Count > 0)
            {
                MenuQuery = " select num_menumst_menuid menuid,var_menumst_menuname menutitle,num_menumst_parentid parentid,var_menumst_pagepath pagepath, ";
                MenuQuery += " num_menumst_orderby  orderby from aoup_menumst_def where num_menumst_parentid = 0 ";
                MenuQuery += " UNION select num_menumst_menuid menuid,var_menumst_menuname menutitle,num_menumst_parentid parentid,var_menumst_pagepath pagepath, ";
                MenuQuery += " num_menumst_orderby  orderby from aoup_menumst_def ";
                MenuQuery += " inner join aoup_MenuCompMst_def  on num_menucompmst_menuid=num_menumst_menuid and num_menucompmst_compid=" + TblCompDet.Rows[0]["hoid"].ToString();
                MenuQuery += " inner join aoup_MenuUsersMst_def  on num_menuusersmst_menuid=num_menumst_menuid and var_menuusersmst_userid='" + Session["UserId"].ToString() + "'  order by orderby";
            }

            DataTable TblMenus = new DataTable();

            if (Session["tblmenu"] == null)
            {
                TblMenus = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(MenuQuery);
                Session["tblmenu"] = TblMenus;
            }
            else
            {
                TblMenus = (DataTable)Session["tblmenu"];
            }

            if (TblMenus.Rows.Count > 0)
            {
                DataRow[] MainMenu = TblMenus.Select("ParentId = 0");

                Int32 CountSubMenu = 0;
                String LiteralText = null;

                LiteralText = "<ul class='nav'>";

                if (MainMenu.Length > 0)
                {
                    foreach (DataRow MainMenuRow in MainMenu)
                    {
                        String MainMenuPagePath = MainMenuRow["pagepath"].ToString();
                        String MainMenuMenuTitle = MainMenuRow["menutitle"].ToString();
                        String MainMenuId = MainMenuRow["menuid"].ToString();
                        String ParentId = MainMenuRow["parentid"].ToString();

                        if (MainMenuMenuTitle.Trim() == "Logout")
                        {
                            LiteralText += String.Format(@"<li class='dropdown Logoutcss'><a href=""#"" class = 'page-scroll'>&nbsp;{0}</a></li>", MainMenuMenuTitle);
                        }
                        else if (MainMenuMenuTitle.Trim() == "Home")
                        {
                            LiteralText += String.Format(@"<li class='dropdown'><a href='../HomePage/FrmDashboard.aspx'><span class=''><img src='../Images/HomeIconFinal.png' style='width: 22px;height: 22px;padding-bottom: 2px;' /></span> &nbsp;Home</a></li>", MainMenuMenuTitle);
                        }
                        else
                        {
                            LiteralText += String.Format(@"<li class='dropdown'><a href=""{0}""> &nbsp;{1}</a>", MainMenuPagePath, MainMenuMenuTitle);
                        }

                        lit.Text = LiteralText;

                        DataRow[] SubMenus = TblMenus.Select(String.Format("parentid = {0}", MainMenuId));

                        CountSubMenu = 0;

                        if (SubMenus.Length > 0)
                        {
                            foreach (DataRow SubMenuRow in SubMenus)
                            {
                                string SubMenuPagePath = SubMenuRow["pagepath"].ToString();
                                string SubMenuPageTitle = SubMenuRow["menutitle"].ToString();

                                if (CountSubMenu == 0)
                                {
                                    LiteralText += String.Format(@"<ul class='dropdown-menu'> <li class='dropdown'><a href=""{0}"" class = 'page-scroll'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{1}</a></li>", SubMenuPagePath, SubMenuPageTitle);
                                    CountSubMenu++;
                                }
                                else
                                {

                                    LiteralText += String.Format(@"<li class='dropdown'><a href=""{0}"" class = 'page-scroll'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{1}</a></li>", SubMenuPagePath, SubMenuPageTitle);

                                }
                                lit.Text = LiteralText;
                            }
                            LiteralText += "</ul>";
                            lit.Text += LiteralText;
                        }
                        LiteralText += "</li>";
                    }
                    LiteralText += "</ul>";
                    lit.Text = LiteralText;
                }
            }
        }

        protected void LnkSignout_Click(object sender, EventArgs e)
        {
            AnganwadiLib.Methods.MstMethods.LastLogOut.LastLogout(Session["UserId"].ToString());
            Session["UserName"] = null;
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            // Session.Abandon();
            Session.Contents.RemoveAll();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Redirect("~/FrmSessionLogOut.aspx?@=1");
        }

        public string GetIPAddress(HttpRequest request)
        {
            string ip;
            try
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    if (ip.IndexOf(",") > 0)
                    {
                        string[] ipRange = ip.Split(',');
                        int le = ipRange.Length - 1;
                        ip = ipRange[le];
                    }
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch { ip = null; }

            return ip;
        }
    }
}