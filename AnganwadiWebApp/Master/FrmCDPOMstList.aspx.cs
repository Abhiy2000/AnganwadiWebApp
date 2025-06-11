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
    public partial class FrmCDPOMstList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmCDPORef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                //LoadGrid();
                //LblGrdHead.Text = Session["LblGrdHead"].ToString();// "District List";//
                //if (Session["MasterType"].ToString() == "C")
                //{
                //    btnNew.Visible = false;
                //}
                //else
                //{
                //    btnNew.Visible = true;
                //}

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
                    PnlSerch.Visible = true;
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
                    //MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    //ddlCDPO.SelectedValue = Session["GrdLevel"].ToString();

                    //ddlCDPO_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    //ddlCDPO.Enabled = false;
                    PnlSerch.Visible = true;
                }
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (PnlSerch.Visible == true)
            {
                Session["DIV"] = ddlDivision.SelectedItem;
                if (Session["MasterType"].ToString() == "C")
                {
                    btnNew.Visible = false;
                }
                else
                {
                    btnNew.Visible = true;
                }
                LoadGrid();
            }

            else
            {
                grdCDPOList.DataSource = null;
                grdCDPOList.DataBind();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmCDPOMst.aspx?@=1");
        }

        protected void grdCDPOList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdCDPOList.SelectedRow;
            Session["CDPOId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmCDPOMst.aspx?@=2");
        }

        protected void grdCDPOList_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlDistrict.SelectedValue.ToString();
                PnlSerch.Visible = true;
            }

            else
            {
                PnlSerch.Visible = false;
            }
        }

        public void LoadGrid()
        {
            //String det = " select distinct(num_corporation_corpid) corpid,var_company_compcode Code,var_corporation_corpname Name,var_corporation_branch Branch, ";
            //det += " var_company_address address,var_projecttype_prjtype PrjType from aoup_corporation_mas a left join aoup_projecttype_def b ";
            //det += " on a.num_company_prjtypeid=b.num_projecttype_prjtypeid ";
            String det="  select distinct(num_corporation_corpid) corpid,var_company_compcode Code,var_corporation_corpname Name,var_corporation_branch Branch, ";
            det += " var_company_address address from aoup_corporation_mas a ";
            if (Session["MasterType"].ToString() == "C")
            {
                det += " where num_corporation_corpid=" + Session["GrdLevel"];
            }
            else
            {
                det += " where num_corporation_parentid=" + Session["GrdLevel"];
            }
            det += " and num_company_category=4 order by var_corporation_branch ";

            DataTable tblDivlist = (DataTable)MstMethods.Query2DataTable.GetResult(det);

            if (tblDivlist.Rows.Count > 0)
            {
                grdCDPOList.DataSource = tblDivlist;
                grdCDPOList.DataBind();
                //lblrowCount.Text = "Total No. Of Records : " + grdCDPOList.Rows.Count.ToString();
                grdCDPOList.Visible = true;
            }
            else
            {
                grdCDPOList.Visible = false;
            }
        }

        protected void grdCDPOList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCDPOList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}