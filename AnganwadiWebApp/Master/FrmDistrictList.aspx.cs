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
    public partial class FrmDistrictList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDistrictRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                //LblGrdHead.Text = Session["LblGrdHead"].ToString();// "District List";//
                //LoadGrid();

                PnlSerch.Visible = false;
                MstMethods.Dropdown.Fill(ddlState, "companyview", "companyname", "brid", "", "");
                ddlState.Enabled = false;
                ddlState.SelectedValue = Session["GrdLevel"].ToString();

                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Anganwadi List";

                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();

                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["BRCategory"]);
                if (BRCategory == 0)    //Admin
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                }

                if (BRCategory == 1)    //State
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");                    
                }

                else if (BRCategory == 2)   // Div
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");

                    ddlDivision.SelectedValue = Session["GrdLevel"].ToString();
                    ddlDivision_OnSelectedIndexChanged(sender, e);
                    ddlDivision.Enabled = false;
                    PnlSerch.Visible = true;
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmDistrictMst.aspx?@=1");
        }

        protected void grdDistrictList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdDistrictList.SelectedRow;
            Session["DistrictId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmDistrictMst.aspx?@=2");
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlDivision.SelectedValue.ToString();
                PnlSerch.Visible = true;
            }

            else
            {
                PnlSerch.Visible = false;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (PnlSerch.Visible == true)
            {
                LoadGrid();
            }

            else
            {
                grdDistrictList.DataSource = null;
                grdDistrictList.DataBind();
            }
        }

        protected void grdDistrictList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }

        public void LoadGrid()
        {
            String det = " select num_corporation_corpid corpid,var_company_compcode Code,var_corporation_corpname Name,var_corporation_branch Branch, ";
            det += " var_company_address address,var_projecttype_prjtype PrjType from aoup_corporation_mas a left join aoup_projecttype_def b ";
            det += " on a.num_corporation_corpid=b.num_projecttype_compid and a.num_company_prjtypeid=b.num_projecttype_prjtypeid where num_corporation_parentid=" + Session["GrdLevel"];
            det += " order by var_corporation_branch ";

            DataTable tblDivlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblDivlist.Rows.Count > 0)
            {
                grdDistrictList.DataSource = tblDivlist;
                grdDistrictList.DataBind();
                //lblrowCount.Text = "Total No. Of Records : " + grdDistrictList.Rows.Count.ToString();
                grdDistrictList.Visible = true;
            }
            else
            {
                grdDistrictList.Visible = false;
            }
        }

        protected void grdDistrictList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDistrictList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}