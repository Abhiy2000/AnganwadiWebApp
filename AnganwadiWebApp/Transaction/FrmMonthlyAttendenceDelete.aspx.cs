using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;
using AnganwadiLib.Business;
using System.Globalization;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmMonthlyAttendenceDelete : System.Web.UI.Page
    {
        BoMonAttend objattend = new BoMonAttend();

        int anganid = 0;
        int days = 0;
        DateTime Days = System.DateTime.Now;

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

            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Monthly Attendence Delete"; //
                BindDropdrownlist();
                chkConfigFlag();
                PnlSerch.Visible = false;

                string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
                DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);
                if (TblGetHoId.Rows.Count > 0)
                {
                    MstMethods.Dropdown.Fill(ddlprjType, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "num_projecttype_compid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " order by num_projecttype_prjtypeid ", "");
                }

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

                    PnlSerch.Visible = false;
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

                    PnlSerch.Visible = false;
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

                    PnlSerch.Visible = false;
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

                /*
                if (Request.QueryString["@"] == "1")
                {
                    Session["Mode"] = 1;
                }
                else
                {
                    Session["Mode"] = 2;

                    ddlprjType.SelectedValue = "";//Session["PrjType"].ToString();
                    ddlprjType_SelectedIndexChanged(null, null);
                    ddlAngan.SelectedValue = Session["AngnId"].ToString();
                    //dtSalDate.Text = Session["SalDate"].ToString();
                    //dtSalDate.DisableControl();
                    DateTime date = Convert.ToDateTime(Session["SalDate"]);
                    String month = date.Month.ToString();
                    String year = date.Year.ToString();
                    ddlMonth.Enabled = false;
                    ddlYear.Enabled = false;
                    ddlMonth.SelectedValue = month;
                    ddlYear.SelectedValue = year;
                    //FillString();
                    FillGrid(date);
                    Submit.Enabled = false;
                }*/
            }
        }

        protected void ddlprjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];

            //DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            //if (TblGetHoId.Rows.Count > 0)
            //{
            //    String qry = " Select var_angnwadimst_angnname,num_angnwadimst_angnid from aoup_angnwadimst_def where num_angnwadimst_compid=" + Session["GrdLevel"].ToString();
            //    qry += " and  num_angnwadimst_prjtypeid=" + ddlprjType.SelectedValue + " order by num_angnwadimst_angnid ";

            //    MstMethods.Dropdown.Fill(ddlAngan, "", "", "", "", qry);
            //}
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
                lblTotal.Text = "";
                lblrowCount.Text = "";
                lblUnauthCount.Text = "";
                lblUnauthSevika.Visible = false;
                grdMonAttendance.DataSource = null;
                grdMonAttendance.Visible = false;
                btnInsert.Visible = false;
                Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();
                String str = " select count(num_sevikamaster_sevikaid) TotalSevika from aoup_sevikamaster_def where num_sevikamaster_compid=" + Session["GrdLevel"];
                str += " and var_sevikamaster_active='Y' ";// and var_sevikamaster_cpsmscode is not NULL ";
                DataTable dtTotal = (DataTable)MstMethods.Query2DataTable.GetResult(str);
                lblTotal.Text = "Total No. of Sevika : " + dtTotal.Rows[0]["TotalSevika"].ToString();
                lblTotal.Visible = false;

                String str1 = " select count(num_sevikamaster_sevikaid) TotUnauthSevika from aoup_sevikamaster_def where num_sevikamaster_compid=" + Session["GrdLevel"];
                str1 += " and var_sevikamaster_active='Y' and var_sevikamaster_authorizedby is NULL ";
                DataTable dtTotal1 = (DataTable)MstMethods.Query2DataTable.GetResult(str1);
                lblUnauthCount.Text = "" + dtTotal1.Rows[0]["TotUnauthSevika"].ToString();
                lblUnauthCount.Visible = false;
                lblUnauthSevika.Visible = false;

                PnlSerch.Visible = true;
            }
            else
            {
                PnlSerch.Visible = false;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            if (Convert.ToInt32(ddlMonth.SelectedValue) > System.DateTime.Now.Month && Convert.ToInt32(ddlYear.SelectedItem.Text) >= System.DateTime.Now.Year)
            {
                MessageAlert(" You can not insert next month attendence", "");
                grdMonAttendance.Visible = false;
                btnInsert.Visible = false;
                lblrowCount.Visible = false;
                return;
            }

            //if (Request.QueryString["@"] == "1")
            //{
            //    String qry = " select * from aoup_salary_def where num_salary_compid=" + Session["GrdLevel"] + " and date_salary_date='" + lstdate + "'";

            //    DataTable dtFill = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(qry);

            //    if (dtFill.Rows.Count > 0)
            //    {
            //        //MessageAlert(" Salary Already Calculated for this month ", "");
            //        MessageAlert(" Attendence Already Inserted for this month ", "");
            //        return;
            //    }
            //}
            //LoadGrid();
            FillGrid(Lastdate);
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            String VchPrepDtl = "";

            if (Request.QueryString["@"] == "1")
            {
                //Days = Convert.ToDateTime(dtSalDate.Value);
            }

            else
            {
                Days = Convert.ToDateTime(Session["SalDate"]);
            }

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");
            days = System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue));

            for (int i = 0; i < grdMonAttendance.Rows.Count; i++)
            {
                Label lblSevikaId = (Label)grdMonAttendance.Rows[i].FindControl("lblSevikaId");
                Label lblSevkiaNm = (Label)grdMonAttendance.Rows[i].FindControl("lblSevkiaNm");
                Label lblDesg = (Label)grdMonAttendance.Rows[i].FindControl("lblDesg");
                Label lblTotalDays = (Label)grdMonAttendance.Rows[i].FindControl("lblTotalDays");
                TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");

                if (txtAbsentDays.Text != "")
                {

                    Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");

                    TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");

                    if (txtAllow.Text == "")
                    {
                        txtAllow.Text = "0";
                    }

                    TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");

                    if (txtDeduct.Text == "")
                    {
                        txtDeduct.Text = "0";
                    }

                    VchPrepDtl += lblSevikaId.Text + "#" + lblPrsntDays.Text + "#" + txtAbsentDays.Text + "#" + txtAllow.Text + "#" + txtDeduct.Text + "$";

                }
            }
            if (VchPrepDtl != "")
            {
                VchPrepDtl = VchPrepDtl.Substring(0, VchPrepDtl.Length - 1);

                objattend.UserId = Session["UserId"].ToString();
                objattend.CompID = Convert.ToInt32(Session["GrdLevel"]);
                objattend.SevikaID = 0;
                objattend.Date = Convert.ToString(lstdate);
                objattend.AnganID = 0;

                if (ddlprjType.SelectedValue != "")
                {
                    objattend.PrjTypeId = Convert.ToInt32(ddlprjType.SelectedValue);
                }
                else
                {
                    objattend.PrjTypeId = 0;
                }

                objattend.TotalDays = days;
                objattend.ParamStr = VchPrepDtl;

                objattend.Mode = 1;

                objattend.DeleteAttendance();

                if (objattend.ErrorCode == -100)
                {
                    MessageAlert(objattend.ErrorMsg, "../Transaction/FrmMonthlyAttendenceDeleteRef.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objattend.ErrorMsg, "");
                    return;
                }
            }
            else
            {
                MessageAlert("Attendence can not deleted", "../HomePage/FrmDashboard.aspx");
                return;
            }
        }

        protected void grdMonAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //TextBox txt = e.Row.FindControl("txtAbsentDays") as TextBox;
            //txt.TextChanged += new EventHandler(txtAbsentDays_TextChanged);
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[11].Visible = false;
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }

        public void BindDropdrownlist()
        {
            ddlMonth.Items.Add(new ListItem("January", "1"));
            ddlMonth.Items.Add(new ListItem("February", "2"));
            ddlMonth.Items.Add(new ListItem("March", "3"));
            ddlMonth.Items.Add(new ListItem("April", "4"));
            ddlMonth.Items.Add(new ListItem("May", "5"));
            ddlMonth.Items.Add(new ListItem("June", "6"));
            ddlMonth.Items.Add(new ListItem("July", "7"));
            ddlMonth.Items.Add(new ListItem("August", "8"));
            ddlMonth.Items.Add(new ListItem("September", "9"));
            ddlMonth.Items.Add(new ListItem("October", "10"));
            ddlMonth.Items.Add(new ListItem("November", "11"));
            ddlMonth.Items.Add(new ListItem("December", "12"));

            ddlYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), "1"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), "2"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 2).ToString(), "3"));
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();

            ddlYear.SelectedItem.Text = System.DateTime.Now.Year.ToString();
        }

        public void FillGrid(DateTime dt)
        {
            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            DateTime Firstdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), 01);
            string fstdate = Firstdate.ToString("dd-MMM-yyyy");

            days = System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue));

            if (lblUnauthCount.Text != "")
            {
                if (Convert.ToInt32(lblUnauthCount.Text) > 0)
                {
                    MessageAlert(" Can not insert Attendance due to sevika is unauthorized ", "");
                    return;
                }
            }
            String Qry = " select num_sevikamaster_sevikaid sevikaid,num_sevikamaster_compid brid,d.branchname BITNM,num_sevakmaster_anganid anganid, ";
            Qry += " c.var_angnwadimst_angnname anganNM,  num_sevikamaster_desigid desgid,var_designation_desig desgNM,num_sevikamaster_payscalid payid, ";
            Qry += " var_sevikamaster_name sevikaNM," + days + " Days,num_salary_presentdays presentdays,num_salary_absentdays absentdays,num_salary_allowance1 allowance1, ";
            Qry += " num_salary_deduct1 deduct1 from aoup_sevikamaster_def a left join aoup_designation_def b on  a.num_sevikamaster_desigid=b.num_designation_desigid ";
            Qry += " left join aoup_angnwadimst_def c on a.num_sevakmaster_anganid=c.num_angnwadimst_angnid and a.num_sevikamaster_compid=c.num_angnwadimst_compid ";
            Qry += " left Join companyview d on a.num_sevikamaster_compid=d.brid ";
            Qry += " left join aoup_salary_def e on a.num_sevikamaster_compid=e.num_salary_compid and a.num_sevikamaster_sevikaid=e.num_salary_sevikaid ";
            Qry += " and date_salary_date = '" + dt.ToString("dd-MMM-yyyy") + "' ";
            Qry += " where num_salary_compid = " + Session["GrdLevel"] + " and var_sevikamaster_authorizedby is not null ";
            Qry += " and var_sevikamaster_active='Y' AND var_salary_billnofixed IS NULL AND var_salary_billnocentral IS NULL AND var_salary_billnostate IS NULL";
            Qry += " order by date_salary_date ";

            DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(Qry);

            if (dtstr.Rows.Count > 0)
            {
                lblTotal.Visible = true;
                lblUnauthSevika.Visible = true;
                lblUnauthCount.Visible = true;
                grdMonAttendance.DataSource = dtstr;
                grdMonAttendance.DataBind();
                grdMonAttendance.Visible = true;
                lblrowCount.Visible = true;
                lblrowCount.Text = "Total No. Of Pending Sevika : " + dtstr.Rows.Count;
                grdMonAttendance.Visible = true;
                btnInsert.Visible = true;

                for (int j = 0; j < dtstr.Rows.Count; j++)
                {
                    for (int i = 0; i < grdMonAttendance.Rows.Count; i++)
                    {
                        Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
                        TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
                        TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");
                        TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");

                        txtAbsentDays.Text = dtstr.Rows[i]["absentdays"].ToString();
                        lblPrsntDays.Text = dtstr.Rows[i]["presentdays"].ToString();
                        txtAllow.Text = dtstr.Rows[i]["allowance1"].ToString();
                        txtDeduct.Text = dtstr.Rows[i]["deduct1"].ToString();
                    }
                }

            }
            else
            {
                MessageAlert(" Record Not Found ", "");
                grdMonAttendance.DataSource = null;
                grdMonAttendance.Visible = false;
                lblrowCount.Visible = false;
                btnInsert.Visible = false;
                lblUnauthSevika.Visible = false;
            }
        }

        private void chkConfigFlag()
        {
            String Qry = " select var_config_configflag configflag from aoup_config_def ";
            DataTable dtFlag = (DataTable)MstMethods.Query2DataTable.GetResult(Qry);

            if (dtFlag.Rows.Count > 0)
            {
                ViewState["ConfigFlag"] = dtFlag.Rows[0]["configflag"].ToString();
            }
        }


    }
}
