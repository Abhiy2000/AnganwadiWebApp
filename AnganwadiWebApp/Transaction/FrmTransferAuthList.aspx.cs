using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using AnganwadiLib.Methods;
using System.Data;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmTransferAuthList : System.Web.UI.Page
    {
        BoTransferSevika objTrfSev = new BoTransferSevika();

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

            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";           

            if (!IsPostBack)
            {
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

                    PnlGrid.Visible = true;
                }
            }
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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
                Session["GrdLevel"] = ddlCDPO.SelectedValue;
                LoadGrid();
            }
        }

        protected void GrdSevikaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdSevikaList.SelectedRow;
            Session["SevikaId"] = row.Cells[2].Text;
            Session["AadharNo"] = row.Cells[4].Text;
            Session["TransferDate"] = row.Cells[6].Text;
            Response.Redirect("../Transaction/FrmTransferAuth.aspx?@=2");
        }

        protected void GrdSevikaList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
            }
        }

        protected void GrdSevikaList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdSevikaList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        public void LoadGrid()
        {
            try
            {
                string GetHOid = "select hoid from companyview where brid=" + Session["GrdLevel"];

                DataTable TblGetHOid = (DataTable)MstMethods.Query2DataTable.GetResult(GetHOid);

                String str = "select a.bitbitname oldBIT,b.bitbitname NewBIT,c.var_angnwadimst_angnname OldAngan,d.var_angnwadimst_angnname NewAngan, ";
                str += " var_sevikamaster_name Name,num_sevikamaster_sevikaid,var_transfer_aadharno AadharNo,dat_transfer_transferdate from aoup_transfer_det inner join corpinfo a on num_transfer_oldbitid=a.bitid ";
                str += " inner join corpinfo b on num_transfer_newbitid=b.bitid left join aoup_angnwadimst_def c on c.num_angnwadimst_compid=num_transfer_oldbitid ";
                str += " and num_transfer_oldanganwadiid=c.num_angnwadimst_angnid left join aoup_angnwadimst_def d on d.num_angnwadimst_compid=num_transfer_newbitid ";
                str += " and num_transfer_newanganwadiid=d.num_angnwadimst_angnid left join aoup_sevikamaster_def e on e.num_sevikamaster_compid=num_transfer_oldbitid ";
                str += " and e.num_sevikamaster_sevikaid=num_transfer_sevikaid and e.var_sevikamaster_aadharno=var_transfer_aadharno ";
                str += " where var_transfer_authorizedby is null and var_transfer_status = 'P' ";
                str += " and b.stateid=" + TblGetHOid.Rows[0]["hoid"].ToString();
                str += " and b.cdpoid=" + Session["GrdLevel"];
                str += " order by dat_transfer_transferdate";

                DataTable dtInfo = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                if (dtInfo.Rows.Count > 0)
                {
                    PnlGrid.Visible = true;
                    GrdSevikaList.DataSource = dtInfo;
                    GrdSevikaList.DataBind();
                }
                else
                {
                    PnlGrid.Visible = false;
                    MessageAlert("Record Not Found", "");
                    return;
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