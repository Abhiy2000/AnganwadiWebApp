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
    public partial class FrmDiffBillGenerationList : System.Web.UI.Page
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
            //LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";
            if (!IsPostBack)
            {
                //LoadGrid();
                PnlSerch.Visible = false;

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
                    //MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    //ddlCDPO.SelectedValue = Session["GrdLevel"].ToString();

                    //ddlCDPO_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    //ddlCDPO.Enabled = false;
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
                    //MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["cdpo"].ToString() + " order by branchname", "");
                    //MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    //ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();
                    //ddlBeat.SelectedValue = Session["GrdLevel"].ToString();

                    //ddlBeat_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    //ddlCDPO.Enabled = false;
                    //ddlBeat.Enabled = false;

                    PnlSerch.Visible = true;
                }
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
                GrdBillGenList.DataSource = null;
                GrdBillGenList.DataBind();
            }
        }

        protected void GrdBillGenList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            //e.Row.Cells[6].Visible = false;
        }

        protected void GrdBillGenList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdBillGenList.SelectedRow;
            Session["SalDate"] = row.Cells[3].Text;
            //Session["PrjType"] = row.Cells[4].Text;
            Session["AngnId"] = row.Cells[6].Text;
            Response.Redirect("../Transaction/FrmDiffBillGenerationMst.aspx?@=2");
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            //ddlCDPO.DataSource = "";
            //ddlCDPO.DataBind();

            //ddlBeat.DataSource = "";
            //ddlBeat.DataBind();

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

            //ddlCDPO.DataSource = "";
            //ddlCDPO.DataBind();

            //ddlBeat.DataSource = "";
            //ddlBeat.DataBind();

            //if (ddlDistrict.SelectedValue.ToString() != "")
            //{
            //    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            //}
        }

        /*protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
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
            }

            else
            {
                PnlSerch.Visible = false;
            }
        }*/

        protected void LoadGrid()
        {
            //string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            //DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);
            //if (TblGetHoId.Rows.Count > 0)
            //{
            //String Query = " SELECT num_salary_prjtypeid prjid, var_projecttype_prjtype prjtype,num_salary_anganid angnid, var_angnwadimst_angnname angnwdi, ";
            //Query += " date_salary_date saldate, COUNT (num_salary_sevikaid) empcount FROM aoup_salary_def a LEFT JOIN aoup_angnwadimst_def b ";
            //Query += " ON b.num_angnwadimst_compid=a.num_salary_compid and a.num_salary_anganid = b.num_angnwadimst_angnid ";
            //Query += " LEFT JOIN aoup_projecttype_def c ON  a.num_salary_prjtypeid = c.num_projecttype_prjtypeid WHERE num_salary_compid = " + Session["GrdLevel"];
            //Query += " AND date_salary_billgendate IS NULL and num_projecttype_compid=" + TblGetHoId.Rows[0]["hoid"].ToString();
            //Query += " GROUP BY num_salary_prjtypeid, var_projecttype_prjtype, num_salary_anganid,var_angnwadimst_angnname, date_salary_date ORDER BY date_salary_date ";

            String Query = " SELECT num_salary_prjtypeid prjid, num_salary_anganid angnid, var_angnwadimst_angnname angnwdi, ";
            Query += " date_salary_date saldate, COUNT (num_salary_sevikaid) empcount FROM aoup_salary_def a ";
            Query += " LEFT JOIN aoup_angnwadimst_def b  ON b.num_angnwadimst_compid=a.num_salary_compid and a.num_salary_anganid = b.num_angnwadimst_angnid ";
            Query += " WHERE num_salary_compid in(select bitid from corpinfo where distid=" + Session["GrdLevel"] + ") and var_salary_billnofixed is null  and var_salary_billnocentral is null and var_salary_billnostate is null and var_salary_authorisedby is not null ";
            Query += " GROUP BY num_salary_prjtypeid,num_salary_anganid,var_angnwadimst_angnname, date_salary_date ";
            Query += " ORDER BY date_salary_date ";

            DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (dtSevikaList.Rows.Count > 0)
            {
                GrdBillGenList.DataSource = dtSevikaList;
                GrdBillGenList.DataBind();
            }
            else
            {
                GrdBillGenList.DataSource = null;
                GrdBillGenList.DataBind();
            }
            //}
        }
    }
}