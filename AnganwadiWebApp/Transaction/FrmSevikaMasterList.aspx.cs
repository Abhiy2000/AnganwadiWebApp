using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmSevikaMasterList : System.Web.UI.Page
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

            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";           

            if (!IsPostBack)
            {
                txtSevikaName.Focus();
                PnlSerch.Visible = false;

                MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");

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
                }

                else if (BRCategory == 5)   // Beat
                {
                    str = "select a.num_corporation_corpid beat, a.num_corporation_parentid cdpo, b.num_corporation_parentid dis, c.num_corporation_parentid div ";
                    str += "from aoup_corporation_mas a ";
                    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                    str += "inner join aoup_corporation_mas c on b.num_corporation_parentid = c.num_corporation_corpid ";
                    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["cdpo"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();
                    ddlBeat.SelectedValue = Session["GrdLevel"].ToString();

                    ddlBeat_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                    PnlSerch.Visible = true;
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["RejoinFlag"] = "N";
            Response.Redirect("../Transaction/FrmSevikaMaster.aspx?@=1");
        }

        protected void GrdSevikaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["RejoinFlag"] = "N";
            GridViewRow row = GrdSevikaList.SelectedRow;
            Session["SevikaId"] = row.Cells[2].Text;
            Response.Redirect("../Transaction/FrmSevikaMaster.aspx?@=2");
        }

        protected void GrdSevikaList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }
        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (txtAadharNo.Text != "")
            {
                if (txtAadharNo.Text.Length != 12)
                {
                    MessageAlert("Aadhar No is Invalid", "");
                    return;
                }
            }
            LoadGrid();
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

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

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
            }
        }

        protected void ddlBeat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBeat.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();

                PnlSerch.Visible = true;
                MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");
            }
            else
            {
                PnlSerch.Visible = false;
            }
        }

        protected void LoadGrid()
        {
            String Query = "SELECT a.num_sevikamaster_sevikaid,b.var_angnwadimst_angnname,a.num_sevakmaster_anganid, a.var_sevikamaster_name,a.var_sevikamaster_sevikacode, ";
            Query += " c.var_education_educname,a.date_sevikamaster_birthdate, a.var_sevikamaster_address,a.num_sevikamaster_mobileno,CASE WHEN   var_sevikamaster_active ='Y' THEN 'Active' ELSE 'Inactive' END var_sevikamaster_active, ";
            Query += " b.var_angnwadimst_angncode,d.var_designation_desig,e.var_payscal_payscal,f.var_bankbranch_branchname,g.var_bank_bankname ";
            Query += " FROM aoup_sevikamaster_def a left join aoup_AngnwadiMst_def b on a.num_sevakmaster_anganid=b.num_angnwadimst_angnid and a.num_sevikamaster_compid=b.num_angnwadimst_compid ";
            Query += " left join aoup_education_def c on a.num_sevikamaster_educid=c.num_education_educid ";
            Query += " left join aoup_designation_def d on num_sevikamaster_desigid=d.num_designation_desigid ";
            Query += " left join aoup_PayScal_def e on a.num_sevikamaster_payscalid=e.num_payscal_payscalid ";
            Query += " left join aoup_bankbranch_def f on a.num_sevikamaster_branchid=f.num_bankbranch_branchid ";
            Query += " left join aoup_bank_def g on f.num_bankbranch_bankid =g.num_bank_bankid ";
            Query += " where a.num_sevikamaster_compid='" + Session["GrdLevel"] + "' ";//;and num_sevikamaster_sevikaid=";
            if (txtSevikaName.Text != "")
            {
                Query += " and upper(var_sevikamaster_name) like upper('" + txtSevikaName.Text.Trim() + "%') ";
            }
            if (txtAadharNo.Text != "")
            {
                Query += " and var_sevikamaster_aadharno='" + txtAadharNo.Text.Trim() + "'";
            }
            if (ddlAnganID.SelectedValue != "")
            {
                Query += " and num_sevakmaster_anganid=" + ddlAnganID.SelectedValue;
            }
            Query += " order by num_sevikamaster_sevikaid ";

            DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (dtSevikaList.Rows.Count > 0)
            {
                Session["sevikaid"] = dtSevikaList.Rows[0]["num_sevikamaster_sevikaid"].ToString();
                GrdSevikaList.DataSource = dtSevikaList;
                GrdSevikaList.DataBind();
                lblrowCount.Text = "Total No. Of Records : " + dtSevikaList.Rows.Count.ToString();
            }
            else
            {
                GrdSevikaList.DataSource = null;
                GrdSevikaList.DataBind();
                MessageAlert("No record found", "");
            }
        }

        protected void GrdSevikaList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdSevikaList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void btnRejoinSevika_Click(object sender, EventArgs e)
        {
            Session["RejoinFlag"] = "Y";
            Response.Redirect("../Transaction/FrmSevikaMaster.aspx?@=1");
        }
    }
}