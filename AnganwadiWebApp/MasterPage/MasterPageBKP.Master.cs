using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProjectManagement.MasterPage
{
    public partial class MasterPageBKP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserFullName"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            Page.Header.DataBind(); 
            if (!IsPostBack)
            {
                lblUserName.Text = Session["UserFullName"].ToString();
                //lblDeptName.Text = Session["branchname"].ToString();
                lblDesigName.Text = Session["desgname"].ToString();
                lblLastLogIn.Text = Session["lastlogin"].ToString();
                lblLastLogOut.Text = Session["lastlogout"].ToString();
                //lblTataHeader.Text = "Anganwadi Web Application";

                MenuBind();
            }

            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }

        public void MenuBind()
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

            DataTable TblMenus = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(MenuQuery);

            DataRow[] MainMenu = TblMenus.Select("ParentId = 0");

            Int32 CountSubMenu = 0;
            String LiteralText = null;

            LiteralText = "<nav class='navigation'><ul class='mainmenu'>";

            if (MainMenu.Length > 0)
            {
                foreach (DataRow MainMenuRow in MainMenu)
                {
                    String MainMenuPagePath = MainMenuRow["pagepath"].ToString();
                    String MainMenuMenuTitle = MainMenuRow["menutitle"].ToString();
                    String MainMenuId = MainMenuRow["menuid"].ToString();
                    String ParentId = MainMenuRow["parentid"].ToString();

                    LiteralText += String.Format(@"<li><a href=""{0}""> &#10070; &nbsp;{1}</a>", MainMenuPagePath, MainMenuMenuTitle);
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
                                LiteralText += String.Format(@"<ul class='submenu'><li><a href=""{0}"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{1}</a></li>", SubMenuPagePath, SubMenuPageTitle);
                                CountSubMenu++;
                            }
                            else
                            {
                                LiteralText += String.Format(@"<li><a href=""{0}"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{1}</a></li>", SubMenuPagePath, SubMenuPageTitle);
                            }
                            lit.Text = LiteralText;
                        }
                        LiteralText += "</ul>";
                        lit.Text += LiteralText;
                    }
                    LiteralText += "</li>";
                }
                LiteralText += "</ul></nav>";
                lit.Text = LiteralText;
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

        public LinkButton LnkSignoutE
        { get { return LnkSignout; } }

        public Literal litE
        { get { return lit; } }

        public HyperLink hrefDashE
        { get { return hrefDash; } }
    }
}