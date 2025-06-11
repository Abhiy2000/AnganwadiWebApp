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
    public partial class FrmMonthlyAttendence : System.Web.UI.Page
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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Monthly Attendence"; //
                BindDropdrownlist();
                chkConfigFlag();
                PnlSerch.Visible = false;

                Session["ResetGrd"] = Session["GrdLevel"];

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
                    if (Convert.ToInt32(txtAbsentDays.Text) < 0 || (Convert.ToInt32(txtAbsentDays.Text) > Convert.ToInt32(lblTotalDays.Text)))
                    {
                        MessageAlert(" Please enter Proper Absent Days ", "");
                        return;
                    }

                    Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
               

                    //String[] PrsntDaysarr = hdnpresentday.Value.Split('$');

                    //foreach (string sr in PrsntDaysarr)
                    //{
                    //    if (Convert.ToInt32(sr.Split('~')[0]) == i)
                    //    {
                    //        lblPrsntDays.Text = sr.Split('~')[1].ToString();
                    //    }
                    //}

                    if (lblPrsntDays.Text == "")
                    {
                        MessageAlert(" Present Days Should not be Blank ", "");
                        return;
                    }
                                      

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

                    //if (txtAbsentDays.Enabled == true)
                    //{
                        VchPrepDtl += lblSevikaId.Text + "#" + lblPrsntDays.Text + "#" + txtAbsentDays.Text + "#" + txtAllow.Text + "#" + txtDeduct.Text + "$";
                    //}
                }
            }
            if (VchPrepDtl != "")
            {
                VchPrepDtl = VchPrepDtl.Substring(0, VchPrepDtl.Length - 1);

                objattend.UserId = Session["UserId"].ToString();
                objattend.CompID = Convert.ToInt32(Session["GrdLevel"]);
                objattend.SevikaID = 0;
                objattend.Date = Convert.ToString(lstdate); //"31-May-2023";
                objattend.AnganID = 0;

                if (ddlprjType.SelectedValue != "")
                {
                    objattend.PrjTypeId = Convert.ToInt32(ddlprjType.SelectedValue);
                }
                else
                {
                    objattend.PrjTypeId = 0;
                }

                if (grdMonAttendance.Rows.Count > 0)
                {
                    int rowscount = grdMonAttendance.Rows.Count;
                    int columnscount = grdMonAttendance.Columns.Count;
                    for (int i = 0; i < rowscount; i++)
                    {
                        TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
                        if (string.IsNullOrEmpty(txtAbsentDays.Text))
                        {
                            MessageAlert("कृपया सगळयाांची हजेरी भरावी.", "");
                            return;
                        }
                    }

                }

                objattend.TotalDays = days;
                objattend.ParamStr = VchPrepDtl;

                objattend.Mode = 1;

                objattend.BoMonAttend_1();

                if (objattend.ErrorCode == -100)
                {
                    hdnpresentday.Value = "";
                    Session["GrdLevel"] = Session["ResetGrd"];
                    //If Saved Successfully
                    String str = "हजेरी १००% भरण्यात आली आहे. कृपया DDO लॉगीन ने जाऊन हजेरी authorized करा.";
                    MessageAlert(str, "../Transaction/FrmMonthlyAttendence.aspx");
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
                MessageAlert("Attendence can not inserted again", "../HomePage/FrmDashboard.aspx");
                return;
            }
        }

        protected void txtAbsentDays_TextChanged(object sender, EventArgs e)
        {
            TableRow grdAddOnPlan = ((System.Web.UI.WebControls.TableRow)((GridViewRow)(((TextBox)(sender)).Parent.BindingContainer)));
            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.Parent.Parent;
            int rowIndex = row.RowIndex;

            //GridViewRow row = (sender as TextBox).Parent.Parent as GridViewRow;

            //int i = row.RowIndex;
            Label lblTotalDays = (Label)grdAddOnPlan.FindControl("lblTotalDays");
            //Label lblTotalDays = (Label)grdMonAttendance.Rows[i].FindControl("lblTotalDays");
            Label lblPrsntDays = (Label)grdAddOnPlan.FindControl("lblPrsntDays");
            // Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
            TextBox txtAbsentDays = (TextBox)grdAddOnPlan.FindControl("txtAbsentDays");
            //TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");

            if (txtAbsentDays.Text == "")
            {
                //lblPrsntDays.Text = "0";
                MessageAlert("Absent days can not be blank", "");
                grdMonAttendance.Focus();
                grdMonAttendance.Rows[rowIndex].Cells[7].Focus();
                return;
            }

            else
            {
                if (Convert.ToInt32(txtAbsentDays.Text) >= 0)
                {
                    if (Convert.ToInt32(txtAbsentDays.Text) > Convert.ToInt32(lblTotalDays.Text))
                    {
                        lblPrsntDays.Text = "";
                        txtAbsentDays.Text = "";

                        MessageAlert("Absent Days can not be greater than Total Days", "");                        
                        grdMonAttendance.Focus();
                        grdMonAttendance.Rows[rowIndex].Cells[7].Focus();
                        return;
                    }

                    lblPrsntDays.Text = (Convert.ToInt32(lblTotalDays.Text) - Convert.ToInt32(txtAbsentDays.Text)).ToString();

                    if (lblPrsntDays.Text == "0")
                    {
                        MessageAlert("तुम्ही शुन्य दिवस हजेरी भरत आहे. बरोबर असेल तर 'OK' वर Click करा. ", "");
                        grdMonAttendance.Focus();
                        grdMonAttendance.Rows[rowIndex].Cells[7].Focus();
                        return;
                    }
                 
                   // txtAbsentDays.Focus();
                    if (grdMonAttendance.Rows.Count-1 != rowIndex)
                    {
                        rowIndex = rowIndex + 1;
                        grdMonAttendance.Focus();
                        grdMonAttendance.Rows[rowIndex].Cells[7].Focus();
                    }
                }
                else
                {
                    MessageAlert(" Please Enter Proper Absent Days ", "");
                    return;
                }
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

        //public void LoadGrid()
        //{
        //    Int32 HOId = 0;
        //    String str = "";

        //    //str = " select num_sevikamaster_sevikaid sevikaid,num_sevikamaster_compid brid,num_sevakmaster_anganid anganid,num_sevikamaster_desigid desgid, ";
        //    //str += " var_designation_desig desgNM,num_sevikamaster_payscalid payid,var_sevikamaster_name sevikaNM,NULL Days from aoup_sevikamaster_def a left join aoup_designation_def b ";
        //    //str += " on a.num_sevikamaster_desigid=b.num_designation_desigid ";
        //    //if (ddlAngan.SelectedValue != "")
        //    //{
        //    //    str += " where num_sevikamaster_compid=" + Session["GrdLevel"].ToString() + " and num_sevakmaster_anganid= " + ddlAngan.SelectedValue;
        //    //}
        //    //else
        //    //{
        //    //str += " where num_sevikamaster_compid=" + Session["GrdLevel"].ToString();// +" and num_sevakmaster_anganid= " + Convert.ToInt32(Session["AngnId"]);
        //    //}
        //    //str += " order by num_sevikamaster_sevikaid";
        //    //str += "  select num_sevikamaster_sevikaid sevikaid,num_sevikamaster_compid brid,d.branchname BITNM,num_sevakmaster_anganid anganid,c.var_angnwadimst_angnname anganNM, ";
        //    //str += " num_sevikamaster_desigid desgid,var_designation_desig desgNM,num_sevikamaster_payscalid payid,var_sevikamaster_name sevikaNM,NULL Days ";
        //    //str += " from aoup_sevikamaster_def a left join aoup_designation_def b  on a.num_sevikamaster_desigid=b.num_designation_desigid ";
        //    //str += " left join aoup_angnwadimst_def c on a.num_sevakmaster_anganid=c.num_angnwadimst_angnid and a.num_sevikamaster_compid=c.num_angnwadimst_compid ";
        //    //str += " left Join companyview d on a.num_sevikamaster_compid=d.brid ";
        //    //str += " where num_sevikamaster_compid in (select brid from companyview where parentid=" + Session["GrdLevel"] + ") ";
        //    //str += " and num_angnwadimst_prjtypeid= " + ddlprjType.SelectedValue;
        //    //str += " order by num_sevikamaster_sevikaid ";

        //    str += " select num_sevikamaster_sevikaid sevikaid,num_sevikamaster_compid brid,d.branchname BITNM,num_sevakmaster_anganid anganid, ";
        //    str += " c.var_angnwadimst_angnname anganNM,  num_sevikamaster_desigid desgid,var_designation_desig desgNM,num_sevikamaster_payscalid payid, ";
        //    str += " var_sevikamaster_name sevikaNM,NULL Days  from aoup_sevikamaster_def a left join aoup_designation_def b on ";
        //    str += " a.num_sevikamaster_desigid=b.num_designation_desigid ";
        //    str += " left join aoup_angnwadimst_def c on a.num_sevakmaster_anganid=c.num_angnwadimst_angnid and a.num_sevikamaster_compid=c.num_angnwadimst_compid ";
        //    str += " left Join companyview d on a.num_sevikamaster_compid=d.brid where num_sevikamaster_compid =" + Session["GrdLevel"];
        //    str += " and var_sevikamaster_authorizedby is not null ";
        //    if (ddlprjType.SelectedIndex != 0)
        //    {
        //        str += " and num_angnwadimst_prjtypeid= " + ddlprjType.SelectedValue;
        //    }
        //    str += " order by num_sevikamaster_sevikaid ";

        //    DataTable dtFill = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

        //    if (dtFill.Rows.Count > 0)
        //    {
        //        DateTime Date = System.DateTime.Now;
        //        if (Request.QueryString["@"] == "1")
        //        {
        //            //  Date = Convert.ToDateTime(dtSalDate.Value);
        //        }
        //        else
        //        {
        //            // Date = Convert.ToDateTime(Session["SalDate"]);
        //        }

        //        //String mn = Date.Month.ToString();
        //        //String yy = Date.Year.ToString();
        //        //var lastDayOfMonth = DateTime.DaysInMonth(Date.Year, Date.Month);

        //        DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
        //        string lstdate = Lastdate.ToString("dd-MMM-yyyy");
        //        days = System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue));

        //        for (int i = 0; i < dtFill.Rows.Count; i++)
        //        {
        //            dtFill.Rows[i]["Days"] = days;
        //        }

        //        grdMonAttendance.DataSource = dtFill;
        //        grdMonAttendance.DataBind();
        //        lblrowCount.Visible = true;
        //        lblrowCount.Text = "Total No. Of Records : " + grdMonAttendance.Rows.Count.ToString();
        //        grdMonAttendance.Visible = true;
        //        btnInsert.Visible = true;
        //    }
        //    else
        //    {
        //        MessageAlert(" Record Not Found ", "");
        //        grdMonAttendance.DataSource = null;
        //        grdMonAttendance.Visible = false;
        //        lblrowCount.Visible = false;
        //        btnInsert.Visible = false;
        //    }
        //}

        //public void FillString()
        //{
        //    //String str = " select num_salary_presentdays presentdays,num_salary_absentdays absentdays,num_salary_allowance1 allowance1,num_salary_deduct1 deduct1 from aoup_salary_def ";
        //    //str += " where num_salary_compid in (select brid from companyview where parentid=" + Session["GrdLevel"] + ")  ";//and num_salary_prjtypeid=" + ddlprjType.SelectedValue;
        //    //str += " and num_salary_anganid=" + Session["AngnId"].ToString() + " and date_salary_date = '" + Session["SalDate"] + "'";// Convert.ToDateTime(dtSalDate.Value) + "' ";

        //    String str = " select num_salary_presentdays presentdays,num_salary_absentdays absentdays,num_salary_allowance1 allowance1,num_salary_deduct1 deduct1 ";
        //    str += " from aoup_salary_def where num_salary_compid=" + Session["GrdLevel"];
        //    str += " and num_salary_anganid=" + Session["AngnId"].ToString() + " and date_salary_date = '" + Session["SalDate"] + "'";

        //    DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(str);

        //    LoadGrid();
        //    for (int j = 0; j < dtstr.Rows.Count; j++)
        //    {
        //        for (int i = 0; i < dtstr.Rows.Count; i++)
        //        {
        //            Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
        //            TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
        //            TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");
        //            TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");

        //            lblPrsntDays.Text = dtstr.Rows[i]["presentdays"].ToString();
        //            txtAbsentDays.Text = dtstr.Rows[i]["absentdays"].ToString();
        //            txtAllow.Text = dtstr.Rows[i]["allowance1"].ToString();
        //            txtDeduct.Text = dtstr.Rows[i]["deduct1"].ToString();

        //            if (txtAbsentDays.Text == "")
        //            {
        //                txtAbsentDays.Enabled = true;
        //            }
        //            else
        //            {
        //                txtAbsentDays.Enabled = false;
        //            }
        //            if (txtAllow.Text == "")
        //            {
        //                txtAllow.Enabled = true;
        //            }
        //            else
        //            {
        //                txtAllow.Enabled = false;
        //            }
        //            if (txtDeduct.Text == "")
        //            {
        //                txtDeduct.Enabled = true;
        //            }
        //            else
        //            {
        //                txtDeduct.Enabled = false;
        //            }
        //        }
        //    }
        //}

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
            Qry += " num_salary_deduct1 deduct1,var_addpay_type LeaveFlag from aoup_sevikamaster_def a left join aoup_designation_def b on  a.num_sevikamaster_desigid=b.num_designation_desigid ";
            Qry += " left join aoup_angnwadimst_def c on a.num_sevakmaster_anganid=c.num_angnwadimst_angnid and a.num_sevikamaster_compid=c.num_angnwadimst_compid ";
            Qry += " left Join companyview d on a.num_sevikamaster_compid=d.brid ";
            Qry += " left join aoup_salary_def e on a.num_sevikamaster_compid=e.num_salary_compid and a.num_sevikamaster_sevikaid=e.num_salary_sevikaid ";
            Qry += " and date_salary_date = '" + dt.ToString("dd-MMM-yyyy") + "' left join aoup_addpay_def on NUM_ADDPAY_SEVIKAID=num_sevikamaster_sevikaid and ";
            Qry += " (date_addpay_fromdate<='" + lstdate + "' and date_addpay_todate>='" + lstdate + "') and var_addpay_status='A' ";
            Qry += " where num_sevikamaster_compid = " + Session["GrdLevel"] + " and var_sevikamaster_authorizedby is not null ";
            Qry += " and var_sevikamaster_active='Y' ";//' and var_sevikamaster_cpsmscode is not NULL ";
            Qry += " and date_sevikamaster_joindate <= '" + dt.ToString("dd-MMM-yyyy") + "' and date_sevikamaster_retiredate >='" + lstdate + "' and date_sevikamaster_retiredate <> '" + fstdate + "'";
            Qry += " and num_sevikamaster_sevikaid not in (select num_salary_sevikaid from aoup_salary_def where num_salary_compid=" + Session["GrdLevel"] + " and date_salary_date='" + dt.ToString("dd-MMM-yyyy") + "') ";
            Qry += " order by var_angnwadimst_angnname,var_sevikamaster_name ";

            DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(Qry);

            if (dtstr.Rows.Count > 0)
            {
                //dtstr.DefaultView.Sort = "sevikaid";

                //String QryM = " select * from Aoup_AddPay_Def where num_addpay_compid = " + Session["GrdLevel"] + " and var_addpay_type='M' and ";
                //QryM += "date_addpay_fromdate <= '" + dt.ToString("dd-MMM-yyyy") + "' and date_addpay_todate >='" + dt.ToString("dd-MMM-yyyy") + "' ";
                
                //DataTable dtstrM = (DataTable)MstMethods.Query2DataTable.GetResult(QryM);



                //for (int k = 0; k < dtstr.Rows.Count; k++)
                //{
                //    dtstr.Rows[k]["Days"] = days;
                //}

               
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

                        Label lblLeaveFlag = (Label)grdMonAttendance.Rows[i].FindControl("lblLeaveFlag");


                        //if (lblLeaveFlag.Text == "M")
                        if (dtstr.Rows[i]["LeaveFlag"].ToString() == "M")
                        {
                            // TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[0].FindControl("txtAbsentDays");
                            txtAbsentDays.Text = days.ToString();
                            txtAbsentDays.Enabled = false;
                            lblPrsntDays.Text = "0";
                            lblPrsntDays.Enabled = false;

                        }
                        else
                        {
                            lblPrsntDays.Text = dtstr.Rows[i]["presentdays"].ToString();
                            txtAbsentDays.Text = dtstr.Rows[i]["absentdays"].ToString();
                        }
                        
                        //if (txtAbsentDays.Text != "")
                        //{
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "GetSelectedRowRowindex(" + i + ")", true);
                        //}
                        txtAllow.Text = dtstr.Rows[i]["allowance1"].ToString();
                        txtDeduct.Text = dtstr.Rows[i]["deduct1"].ToString();
                        lblLeaveFlag.Text = dtstr.Rows[i]["LeaveFlag"].ToString();

                        if (txtAbsentDays.Text == "")
                        {
                            txtAbsentDays.Enabled = true;
                        }
                        else
                        {
                            txtAbsentDays.Enabled = false;
                        }

                       
                        if (ViewState["ConfigFlag"].ToString() == "Y") 
                        {
                            txtAllow.Enabled = true;
                            txtDeduct.Enabled = true;
                        }
                        else
                        {
                            txtAllow.Enabled = false;
                            //txtDeduct.Enabled = false;//comment  by babita on 15-jan-2020
                            txtDeduct.Enabled = true;
                        }

                        //if (txtAllow.Text == "")
                        //{
                        //    txtAllow.Enabled = true;
                        //}
                        //else
                        //{
                        //    txtAllow.Enabled = false;
                        //}
                        //if (txtDeduct.Text == "")
                        //{
                        //    txtDeduct.Enabled = true;
                        //}
                        //else
                        //{
                        //    txtDeduct.Enabled = false;
                        //} 

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "GetSelectedRowRowindex()", true);
                    }
                }

               // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "CallMyFunction", "GetSelectedRow()", true);
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

        //public Boolean ReadOnly
        //{
        //    get
        //    {
        //        return ViewState["ReadOnly"] == null ? false : Convert.ToBoolean(ViewState["ReadOnly"]);
        //    }
        //    set
        //    {
        //        ViewState["ReadOnly"] = value;
        //    }
        //}        

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
