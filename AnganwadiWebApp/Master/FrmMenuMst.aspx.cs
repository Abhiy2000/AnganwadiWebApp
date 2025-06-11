using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace ProjectManagement.Master
{
    public partial class FrmMenuMst : System.Web.UI.Page
    {
        AnganwadiLib.Business.Menu ObjMenu = new AnganwadiLib.Business.Menu();

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmMenuMstRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Menu Master";

                AnganwadiLib.Methods.MstMethods.Dropdown.Fill(DdlParent, "aoup_menumst_def", "var_menumst_menuname", "num_menumst_menuid", "num_menumst_parentid=0 ", "");
                String Query = "select num_corporation_corpid Brid,var_corporation_corpname BrName from aoup_corporation_mas where num_corporation_parentid=0 or num_corporation_parentid= 1";
                if (Request.QueryString["@"] == "1")
                {
                    DataTable TblCompList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
                    if (TblCompList.Rows.Count > 0)
                    {
                        GrdCompanyList.DataSource = TblCompList;
                        GrdCompanyList.DataBind();
                    }
                }
                else
                {
                    if (!IsPostBack)
                    {
                        if (Session["MenuId"].ToString() == "" || Session["MenuId"].ToString() == null)
                        {
                            Response.Redirect("../Admin/FrmMenuList.aspx");
                        }
                        else
                        {
                            try
                            {
                                ObjMenu.Find(Convert.ToInt32(Session["MenuId"].ToString()));
                                txtpagetitle.Text = ObjMenu.PageTitle;
                                if (ObjMenu.ParentId != null && ObjMenu.ParentId != 0)
                                {
                                    DdlParent.SelectedValue = ObjMenu.ParentId.ToString();
                                }
                                txtPagePath.Text = ObjMenu.PagePath;
                                String PageType = ObjMenu.PageType;

                                ObjMenu.GetBranchList();
                                GrdCompanyList.DataSource = ObjMenu.TblBranchList;
                                GrdCompanyList.DataBind();

                                String Query1 = "select num_menucompmst_compid from aoup_MenuCompMst_def  where num_menucompmst_menuid=" + Convert.ToInt32(Session["MenuId"].ToString());
                                DataTable TblChkbranch = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query1);
                                for (int i = 0; i < TblChkbranch.Rows.Count; i++)
                                {
                                    for (int l = 0; l < ObjMenu.TblBranchList.Rows.Count; l++)
                                    {
                                        //CheckBox ChkMenu = (CheckBox)GrdCompanyList.Rows[i].Cells[0].FindControl("ChkCompany");
                                        if (TblChkbranch.Rows[i]["num_menucompmst_compid"].ToString() == ObjMenu.TblBranchList.Rows[l]["Brid"].ToString())
                                        {
                                            CheckBox ChkMenu = (CheckBox)GrdCompanyList.Rows[l].Cells[0].FindControl("ChkCompany");
                                            ChkMenu.Checked = true;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageAlert(ex.Message, "");
                                return;
                            }
                        }
                    }
                }
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtpagetitle.Text == "")
            {
                MessageAlert("Page Title can not be blank", "");
                return;
            }

            if (txtPagePath.Text == "")
            {
                MessageAlert("Page Path can not be blank", "");
                return;
            }
            String AccessString = "";
            ObjMenu.ParentId = Convert.ToInt32(DdlParent.SelectedValue);
            ObjMenu.PageTitle = txtpagetitle.Text.Trim();
            ObjMenu.PagePath = txtPagePath.Text.Trim();
            ObjMenu.MenuId = 0;
            for (int i = 0; i < GrdCompanyList.Rows.Count; i++)
            {
                CheckBox ChkMenu = (CheckBox)GrdCompanyList.Rows[i].Cells[0].FindControl("ChkCompany");
                if (ChkMenu.Checked == true)
                {
                    AccessString += GrdCompanyList.Rows[i].Cells[1].Text + "#";
                }
            }

            if (AccessString != "")
            {
                AccessString = AccessString.Remove(AccessString.Length - 1);
            }

            ObjMenu.AccessString = AccessString;
            ObjMenu.InsBy = Session["UserId"].ToString();
            if (Request.QueryString["@"] == "1")
            {
                ObjMenu.Mode = 1;
            }

            else
            {
                ObjMenu.MenuId = Convert.ToInt32(Session["MenuId"].ToString());
                ObjMenu.Mode = 2;
            }
            ObjMenu.Insert();

            if (ObjMenu.ErrorCode == -100)
            {
                MessageAlert(ObjMenu.ErrorMessage, "../Master/FrmMenuList.aspx");
                return;
            }
            else
            {
                MessageAlert(ObjMenu.ErrorMessage, "");
                return;
            }
        }
    }
}