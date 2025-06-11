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
    public partial class FrmBITMstList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmBITRef.aspx");

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
                }
                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDistrict_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                }
                else if (BRCategory == 4)   // CDPO
                {
                    str = "select a.num_corporation_corpid cdpo, a.num_corporation_parentid dis, b.num_corporation_parentid div ";
                    str += "from aoup_corporation_mas a ";
                    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = Session["GrdLevel"].ToString();

                    ddlCDPO_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    PnlSerch.Visible = true;
                }
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (PnlSerch.Visible == true)
            {
                Session["DIV"] = ddlDivision.SelectedItem;
                Session["DIS"] = ddlDistrict.SelectedItem;
                LoadGrid();
            }

            else
            {
                grdBITList.DataSource = null;
                grdBITList.DataBind();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmBITMst.aspx?@=1");
        }

        protected void grdBITList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdBITList.SelectedRow;
            Session["BITId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmBITMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = " select num_corporation_corpid corpid,var_company_compcode Code,var_corporation_corpname Name,var_corporation_branch Branch, ";
            det += " var_company_address address,var_projecttype_prjtype PrjType from aoup_corporation_mas a left join aoup_projecttype_def b ";
            det += " on a.num_corporation_corpid=b.num_projecttype_compid and a.num_company_prjtypeid=b.num_projecttype_prjtypeid ";
            det += " where num_corporation_parentid=" + Session["GrdLevel"];
            det += " order by var_corporation_branch,var_company_compcode ";

            DataTable tblDivlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblDivlist.Rows.Count > 0)
            {
                grdBITList.DataSource = tblDivlist;
                grdBITList.DataBind();
                //lblrowCount.Text = "Total No. Of Records : " + grdBITList.Rows.Count.ToString();
                grdBITList.Visible = true;
            }
            else
            {
                grdBITList.Visible = false;
            }
        }

        protected void grdBITList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlCDPO.SelectedValue.ToString();
                PnlSerch.Visible = true;
            }

            else
            {
                PnlSerch.Visible = false;
            }
        }

        protected void grdBITList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBITList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}