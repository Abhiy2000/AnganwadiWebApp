using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Master
{
    public partial class FrmAttendenceAccess : System.Web.UI.Page
    {
        BoAttendenceAccess objAttend = new BoAttendenceAccess();

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmAttendenceAccessRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();

                LoadGrid();
                SetGrid();
            }
        }

        public void LoadGrid()
        {
            String str = "select branchname,brid from companyview where parentid=" + Session["GrdLevel"] + " and brcategory=2 order by branchname";

            DataTable TblDivDet = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblDivDet.Rows.Count > 0)
            {
                ViewState["TblDivDet"] = TblDivDet;
                GrdDivList.DataSource = TblDivDet;
                GrdDivList.DataBind();
            }
        }

        public void SetGrid()
        {
            DataTable dt = (DataTable)ViewState["TblDivDet"];
            if (dt != null)
            {
                //String str = "select divname,divid from corpinfo left join aoup_usermst_def on num_usermst_brid=bitid ";
                //str += " inner join aoup_menuusersmst_def on var_menuusersmst_userid=var_usermst_userid and num_menuusersmst_menuid=11 ";
                //str += " where stateid=" + Session["GrdLevel"] + " group by divname,divid order by divname";
                /*
                String str = " select branchname,brid from companyview left join corpinfo on divid=brid left join aoup_usermst_def on (num_usermst_brid=bitid or num_usermst_brid=cdpoid) ";
                str += " inner join aoup_menuusersmst_def on var_menuusersmst_userid=var_usermst_userid and num_menuusersmst_menuid=11 ";
                str += " where stateid=" + Session["GrdLevel"] + " and brcategory=2 group by branchname,brid order by branchname ";*/

                String str = " SELECT distinct divid brid,divname from corpinfo inner join aoup_usermst_def on bitid = num_usermst_brid ";
                str += " inner join aoup_menuusersmst_def on var_usermst_userid=var_menuusersmst_userid and num_menuusersmst_menuid=11 ";
                str += " where stateid=" + Session["GrdLevel"];
                str += " union ";
                str += " SELECT distinct divid,divname from corpinfo inner join aoup_usermst_def on cdpoid = num_usermst_brid ";
                str += " inner join aoup_menuusersmst_def on var_usermst_userid=var_menuusersmst_userid and num_menuusersmst_menuid=11 ";
                str += " where stateid=" + Session["GrdLevel"];

                DataTable Tblmenu = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                if (Tblmenu.Rows.Count > 0)
                {
                    for (int i = 0; i < Tblmenu.Rows.Count; i++)
                    {
                        for (int l = 0; l < dt.Rows.Count; l++)
                        {
                            if (Tblmenu.Rows[i]["brid"].ToString() == dt.Rows[l]["brid"].ToString())
                            {
                                CheckBox ChkProcess = (CheckBox)GrdDivList.Rows[l].Cells[1].FindControl("ChkProcess");
                                ChkProcess.Checked = true;
                            }
                        }
                    }
                }

                String str1 = " SELECT distinct divid brid,divname from corpinfo inner join aoup_usermst_def on bitid = num_usermst_brid ";
                str1 += " inner join aoup_menuusersmst_def on var_usermst_userid=var_menuusersmst_userid and num_menuusersmst_menuid=42 ";
                str1 += " where stateid=" + Session["GrdLevel"];
                str1 += " union ";
                str1 += " SELECT distinct divid,divname from corpinfo inner join aoup_usermst_def on cdpoid = num_usermst_brid ";
                str1 += " inner join aoup_menuusersmst_def on var_usermst_userid=var_menuusersmst_userid and num_menuusersmst_menuid=42 ";
                str1 += " where stateid=" + Session["GrdLevel"];

                DataTable Tblmenu1 = (DataTable)MstMethods.Query2DataTable.GetResult(str1);

                if (Tblmenu1.Rows.Count > 0)
                {
                    for (int i = 0; i < Tblmenu1.Rows.Count; i++)
                    {
                        for (int l = 0; l < dt.Rows.Count; l++)
                        {
                            if (Tblmenu1.Rows[i]["brid"].ToString() == dt.Rows[l]["brid"].ToString())
                            {
                                CheckBox ChkProcess1 = (CheckBox)GrdDivList.Rows[l].Cells[3].FindControl("ChkProcess1");
                                ChkProcess1.Checked = true;
                            }
                        }
                    }
                }

                String str2 = " SELECT distinct divid brid,divname from corpinfo inner join aoup_usermst_def on bitid = num_usermst_brid ";
                str2 += " inner join aoup_menuusersmst_def on var_usermst_userid=var_menuusersmst_userid and num_menuusersmst_menuid=5 ";
                str2 += " where stateid=" + Session["GrdLevel"];
                str2 += " union ";
                str2 += " SELECT distinct divid,divname from corpinfo inner join aoup_usermst_def on cdpoid = num_usermst_brid ";
                str2 += " inner join aoup_menuusersmst_def on var_usermst_userid=var_menuusersmst_userid and num_menuusersmst_menuid=5 ";
                str2 += " where stateid=" + Session["GrdLevel"];

                DataTable Tblmenu2 = (DataTable)MstMethods.Query2DataTable.GetResult(str2);

                if (Tblmenu2.Rows.Count > 0)
                {
                    for (int i = 0; i < Tblmenu2.Rows.Count; i++)
                    {
                        for (int l = 0; l < dt.Rows.Count; l++)
                        {
                            if (Tblmenu2.Rows[i]["brid"].ToString() == dt.Rows[l]["brid"].ToString())
                            {
                                CheckBox ChkProcess2 = (CheckBox)GrdDivList.Rows[l].Cells[3].FindControl("ChkProcess2");
                                ChkProcess2.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objAttend.UserId = Session["UserId"].ToString();
            objAttend.StateId = Convert.ToInt32(Session["GrdLevel"]);

            String AccessString = "";
            String AccessString1 = "";
            String AccessString2 = "";
            for (int i = 0; i < GrdDivList.Rows.Count; i++)
            {
                CheckBox ChkMenu = (CheckBox)GrdDivList.Rows[i].Cells[1].FindControl("ChkProcess");
                CheckBox ChkProcess1 = (CheckBox)GrdDivList.Rows[i].Cells[1].FindControl("ChkProcess1");
                CheckBox ChkProcess2 = (CheckBox)GrdDivList.Rows[i].Cells[1].FindControl("ChkProcess2");
                if (ChkMenu.Checked == true)
                {
                    AccessString += GrdDivList.Rows[i].Cells[2].Text + "#";
                }
                if (ChkProcess1.Checked == true)
                {
                    AccessString1 += GrdDivList.Rows[i].Cells[2].Text + "#";
                }
                if (ChkProcess2.Checked == true)
                {
                    AccessString2 += GrdDivList.Rows[i].Cells[2].Text + "#";
                }
            }

            if (AccessString != "")
            {
                objAttend.Str = AccessString.Remove(AccessString.Length - 1);
            }
            else
            {
                objAttend.Str = "";
            }
            if (AccessString1 != "")
            {
                objAttend.Str1 = AccessString1.Remove(AccessString1.Length - 1);
            }
            else
            {
                objAttend.Str1 = "";
            }
            if (AccessString2 != "")
            {
                objAttend.Str2 = AccessString2.Remove(AccessString2.Length - 1);
            }
            else
            {
                objAttend.Str2 = "";
            }

            objAttend.BoAttendenceAccess_1();

            if (objAttend.ErrorCode == -100)
            {
                MessageAlert(objAttend.ErrorMsg, "../HomePage/FrmDashboard.aspx");
                return;
            }
            else
            {
                MessageAlert(objAttend.ErrorMsg, "");
                return;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx");
        }

        protected void GrdDivList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}