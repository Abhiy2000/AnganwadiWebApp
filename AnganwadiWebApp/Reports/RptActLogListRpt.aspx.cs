using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Reports
{
    public partial class RptActLogListRpt : System.Web.UI.Page
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika List Report";
            if (!IsPostBack)
            {

                //  MstMethods.Dropdown.Fill(ddlUser, "aoup_activity_log", "var_activity_userid", "var_activity_userid", "", "");
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                PnlSerch.Visible = false;

                //MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "'", "");

                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();
                // String str = "select brcategory, parentid from companyview where brid =13";
                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["BRCategory"]);

                if (BRCategory == 1)    //State
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                }

                else if (BRCategory == 2)   // Div
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");

                    ddlDivision.SelectedValue = Session["GrdLevel"].ToString();

                    //ddlDivision_OnSelectedIndexChanged(sender, e);
                    ddlDivision_SelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                    //ddlDistrict_OnSelectedIndexChanged(sender, e);
                    ddlDistrict_SelectedIndexChanged(sender, e);

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
                    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["cdpo"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = Session["GrdLevel"].ToString();

                    //ddlCDPO_OnSelectedIndexChanged(sender, e);
                    ddlCDPO_SelectedIndexChanged(sender, e);

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

                    // ddlBeat_OnSelectedIndexChanged(sender, e);
                    ddlBeat_SelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                    PnlSerch.Visible = true;
                }
            }

        }



        protected void GrdActivityList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GrdActivityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdActivityList.SelectedRow;
            Session["SevikaId"] = row.Cells[1].Text;
            //Response.Redirect("../Reports/FrmSevikaMaster.aspx?@=2");
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
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


                String qry = " select a.var_usermst_userid,num_usermst_brid from  aoup_usermst_def a ";
                qry += " left join aoup_activity_log b on a.var_usermst_userid=b.var_activity_userid ";
                // qry += "where a.num_usermst_brid = " + Session["GrdLevel"].ToString() + " ";
                MstMethods.Dropdown.Fill(ddlUser, "", "", "", " num_usermst_brid = " + ddlDivision.SelectedValue.ToString() + " ", qry);
            }


        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");

                String qry = " select a.var_usermst_userid,num_usermst_brid from  aoup_usermst_def a ";
                qry += " left join aoup_activity_log b on a.var_usermst_userid=b.var_activity_userid ";
                //qry += "where a.num_usermst_brid = " + Session["GrdLevel"].ToString() + " ";
                MstMethods.Dropdown.Fill(ddlUser, "", "", "", " num_usermst_brid = " + ddlDistrict.SelectedValue.ToString() + " ", qry);
            }
        }

        protected void ddlCDPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PnlSerch.Visible = false;

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
                //PnlSerch.Visible = true;
            }
        }

        protected void ddlBeat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = true;

            //if (ddlUser.SelectedValue.ToString() != "")
            //{
            //    Session["GrdLevel"] = ddlUser.SelectedValue.ToString();

            //    PnlSerch.Visible = true;
            //}

            //else
            //{
            //    PnlSerch.Visible = false;
            //}


        }

        protected void search_Click(object sender, EventArgs e)
        {
            LoadGrid();

        }

        public void LoadGrid()
        {
            //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            DateTime FromDate = Convert.ToDateTime(txtFromdate.Value);
            DateTime ToDate = Convert.ToDateTime(txtTodate.Value);

            //String qry = " SELECT divname,distname,cdponame,bitbitname,a.var_sevikamaster_name SevikaName,b.var_angnwadimst_angnname Anganwadi, ";
            //qry += " d.var_designation_desig Designation,var_sevikamaster_aadharno AadharNo, var_projecttype_prjtype Project,n.date_activity_usertime ActDateTime FROM aoup_sevikamaster_def a left join aoup_AngnwadiMst_def b ";
            //qry += " on a.num_sevakmaster_anganid=b.num_angnwadimst_angnid and a.num_sevikamaster_compid=b.num_angnwadimst_compid ";
            //qry += " left join aoup_projecttype_def c on  b.num_angnwadimst_prjtypeid=c.num_projecttype_prjtypeid ";
            //qry += " left join aoup_designation_def d on num_sevikamaster_desigid=d.num_designation_desigid ";
            //qry += " left join corpinfo e on a.num_sevikamaster_compid=e.bitid  ";
            //qry += " left join aoup_usermst_def m on m.num_usermst_brid = a.num_sevikamaster_compid ";
            //qry += " left join aoup_activity_log n on n.var_activity_userid =  m.var_usermst_userid ";


            String qry = " SELECT var_activity_userid,date_activity_usertime,var_activity_pagetitle,var_activity_activity,var_activity_details ";
            qry += " from companyview a inner join aoup_usermst_def b on b.num_usermst_brid = a.brid ";
            qry += " inner join aoup_activity_log c on c.var_activity_userid = b.var_usermst_userid ";
            //  qry += " where b.num_usermst_brid='" + Session["GrdLevel"] + "' ";

            // qry += " where trunc(date_activity_usertime) >= '" + FromDate.ToString("dd/MMM/yyyy") + "' and trunc(date_activity_usertime) <= '" + ToDate.ToString("dd/MMM/yyyy") + "'";
            // qry += " order by date_activity_usertime";
            //qry += " where a.num_sevikamaster_compid in (select bitid from corpinfo where divid=" + ddlDivision.SelectedValue + " and distid=" + ddlDistrict.SelectedValue + " ) ";
            //  qry += " where a.num_sevikamaster_compid='" + Session["GrdLevel"] + "' ";

            //if (ddlCDPO.SelectedValue != "")
            //{
            //    qry += " and cdpoid=" + ddlCDPO.SelectedValue + " ";
            //}
            //if (ddlBeat.SelectedValue != "")
            //{
            //    qry += " and bitid=" + ddlBeat.SelectedValue + " ";
            //}

            if (ddlUser.SelectedValue != "")
            {
                MstMethods.Dropdown.Fill(ddlUser, "aoup_activity_log", "var_activity_userid", "var_activity_userid", "", "");
            }
            else
            {
                if (txtFromdate.Text != "" && txtTodate.Text != "")
                {
                    qry += " and trunc(date_activity_usertime)>='" + FromDate.ToString("dd-MMM-yyyy") + "' and trunc(date_activity_usertime)<='" + ToDate.ToString("dd-MMM-yyyy") + "' ";
                }
            }

            DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(qry);


            if (dtSevikaList.Rows.Count > 0)
            {

                GrdActivityList.DataSource = dtSevikaList;
                GrdActivityList.DataBind();
                GrdActivityList.Visible = true;


            }
            else
            {
                GrdActivityList.DataSource = null;
                GrdActivityList.DataBind();
                GrdActivityList.Visible = false;
                MessageAlert("No record found", "");
            }
        }

        protected void ddlUser_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //MstMethods.Dropdown.Fill(ddlUser, "aoup_activity_log", "var_activity_userid", "var_activity_userid" , "", "");
            if (ddlUser.SelectedIndex != 0)
            {
                txtFromdate.DisableControl();
                txtTodate.DisableControl();
            }
        }
    }
}