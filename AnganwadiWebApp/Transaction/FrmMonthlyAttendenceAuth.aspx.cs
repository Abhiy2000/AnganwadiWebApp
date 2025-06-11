using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;
using AnganwadiLib.Methods;
using System.Globalization;
using System.IO;
using System.Text;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmMonthlyAttendenceAuth : System.Web.UI.Page
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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();
                string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
                DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);
                if (TblGetHoId.Rows.Count > 0)
                {
                    MstMethods.Dropdown.Fill(ddlprjType, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "num_projecttype_compid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " order by num_projecttype_prjtypeid ", "");
                }

                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");

                dtSalDate.Text = Session["SalDate"].ToString();
                dtSalDate.DisableControl();
                //FillString();
                FillGrid(Convert.ToDateTime(Session["SalDate"]));
                Submit.Visible = false;
                CheckBox chkAll = (CheckBox)grdMonAttendance.HeaderRow.FindControl("chkAll");
                chkAll.Checked = true;
                chkAll_CheckedChanged(null, null);
            }
        }

        protected void ddlprjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdMonAttendance.Rows.Count; i++)
            {
                CheckBox chkAll = (CheckBox)grdMonAttendance.HeaderRow.FindControl("chkAll");
                CheckBox ChkCompany = (CheckBox)grdMonAttendance.Rows[i].FindControl("ChkCompany");
                Label lblSevikaId = (Label)grdMonAttendance.Rows[i].FindControl("lblSevikaId");
                Label lblSevkiaNm = (Label)grdMonAttendance.Rows[i].FindControl("lblSevkiaNm");
                Label lblbeat = (Label)grdMonAttendance.Rows[i].FindControl("lblbeat");
                Label lblanganwadi = (Label)grdMonAttendance.Rows[i].FindControl("lblanganwadi");
                Label lblDesg = (Label)grdMonAttendance.Rows[i].FindControl("lblDesg");
                Label lblTotalDays = (Label)grdMonAttendance.Rows[i].FindControl("lblTotalDays");
                TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
                Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
                TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");
                TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");
                Button ButtonUpdate = (Button)grdMonAttendance.Rows[i].FindControl("ButtonUpdate");

                if (chkAll.Checked)
                {
                    ChkCompany.Checked = true;
                }
                else
                {
                    ChkCompany.Checked = false;
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            DateTime Days = Convert.ToDateTime(dtSalDate.Value);
            int month = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("MM")));
            int year = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("yyyy")));
            days = DateTime.DaysInMonth(year, month);
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            String VchPrepDtl = "";

            Days = Convert.ToDateTime(Session["SalDate"]);
            int month = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("MM")));
            String month1 = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Days.Month);
            int year = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("yyyy")));
            days = DateTime.DaysInMonth(year, month);
            String lstdate = Convert.ToString(days + "-" + month1 + "-" + year);                    

            for (int i = 0; i < grdMonAttendance.Rows.Count; i++)
            {
                CheckBox ChkMenu = (CheckBox)grdMonAttendance.Rows[i].Cells[0].FindControl("ChkCompany");

                if (ChkMenu.Checked == true)
                {
                    Label lblSevikaId = (Label)grdMonAttendance.Rows[i].FindControl("lblSevikaId");
                    Label lblSevkiaNm = (Label)grdMonAttendance.Rows[i].FindControl("lblSevkiaNm");
                    Label lblDesg = (Label)grdMonAttendance.Rows[i].FindControl("lblDesg");
                    Label lblTotalDays = (Label)grdMonAttendance.Rows[i].FindControl("lblTotalDays");
                    TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
                    if (txtAbsentDays.Text == "")
                    {
                        MessageAlert(" Absent Days Can not be Blank ", "");
                        return;
                    }
                    else if (Convert.ToInt32(txtAbsentDays.Text) < 0 || (Convert.ToInt32(txtAbsentDays.Text) > Convert.ToInt32(lblTotalDays.Text)))
                    {
                        MessageAlert(" Please enter Proper Absent Days ", "");
                        return;
                    }
                    Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
                    if (lblPrsntDays.Text == "")
                    {
                        MessageAlert(" Present Days Should not be Blank ", "");
                        return;
                    }
                    if (lblPrsntDays.Text == "0")
                    {
                        popMsg1.Show();
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
                    VchPrepDtl += lblSevikaId.Text + "#" + lblPrsntDays.Text + "#" + txtAbsentDays.Text + "#" + txtAllow.Text + "#" + txtDeduct.Text + "$";
                }
            }
            if (VchPrepDtl != "")
            {
                VchPrepDtl = VchPrepDtl.Substring(0, VchPrepDtl.Length - 1);
            }
            else
            {
                MessageAlert("Please select atleast one record", "");
                return;
            }
            objattend.UserId = Session["UserId"].ToString();
            objattend.CompID = Convert.ToInt32(Session["GrdLevel"]);
            objattend.SevikaID = 0;
            objattend.Date = Convert.ToString(lstdate);
            objattend.PrjTypeId = 0;// Convert.ToInt32(ddlprjType.SelectedValue);
            objattend.TotalDays = days;
            objattend.ParamStr = VchPrepDtl;

            objattend.AuthorisedSalaryCal();

            if (objattend.ErrorCode == -100)
            {
                string str = "हजेरी १००% Authorized करण्यात आली आहे.";
                MessageAlert(str, "../Transaction/FrmMonthlyAttendenceAuthList.aspx");
                return;
            }
            else
            {
                MessageAlert(objattend.ErrorMsg, "");
                return;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/FrmMonthlyAttendenceAuthList.aspx");
        }

        protected void grdMonAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TextBox txt = e.Row.FindControl("txtAbsentDays") as TextBox;
            txt.TextChanged += new EventHandler(txtAbsentDays_TextChanged);
        }

        protected void grdMonAttendance_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[e.RowIndex].FindControl("txtAbsentDays");
            TextBox txtAllow = (TextBox)grdMonAttendance.Rows[e.RowIndex].FindControl("txtAllow");
            TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[e.RowIndex].FindControl("txtDeduct");
            TextBox txtTotalwages = (TextBox)grdMonAttendance.Rows[e.RowIndex].FindControl("txtTotalwages");
            txtAbsentDays.Enabled = true;
            txtAllow.Enabled = false;
            //txtDeduct.Enabled = false;
            txtDeduct.Enabled = true;
            txtTotalwages.Enabled = true;
        }

        /*public void LoadGrid()
        {
            //string GetBRId = " select brid from companyview where parentid=" + Session["GrdLevel"].ToString();

            //DataTable dtFillBrId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(GetBRId);
            //Int32 HOId = 0;
            String str, BrId = "";
            //if (dtFillBrId.Rows.Count > 0)
            //{
            //    foreach (DataRow dtRow in dtFillBrId.Rows)
            //    {
            //        foreach (DataColumn dc in dtFillBrId.Columns)
            //        {
            //            BrId += dtRow[dc].ToString() + ",";
            //        }
            //    }
            //    BrId = BrId.Remove(BrId.Length - 1);

            str = " select num_salary_presentdays presentdays, branchname brname, var_angnwadimst_angnname angnname, num_sevikamaster_sevikaid sevikaid,num_sevikamaster_compid brid,num_sevakmaster_anganid anganid,num_sevikamaster_desigid desgid, ";
            str += " var_designation_desig desgNM,num_sevikamaster_payscalid payid,var_sevikamaster_name sevikaNM,NULL Days from aoup_sevikamaster_def a left join aoup_designation_def b ";
            str += " on a.num_sevikamaster_desigid=b.num_designation_desigid ";
            str += " left join aoup_angnwadimst_def on num_angnwadimst_angnid=num_sevakmaster_anganid and num_sevikamaster_compid=num_angnwadimst_compid ";
            str += " left join companyview on brid=num_sevikamaster_compid ";
            str += " left join aoup_salary_def on num_salary_sevikaid=num_sevikamaster_sevikaid ";
            str += " where num_sevikamaster_compid =" + Session["GrdLevel"] + " ";
            str += " and var_sevikamaster_authorizedby is not null ";
            str += " and date_salary_date = '" + Session["SalDate"] + "' and var_salary_authorisedby is  null";
            str += " order by num_sevikamaster_sevikaid";

            DataTable dtFill = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

            if (dtFill.Rows.Count > 0)
            {
                DateTime Date = System.DateTime.Now;
                if (Request.QueryString["@"] == "1")
                {
                    Date = Convert.ToDateTime(dtSalDate.Value);
                }
                else
                {
                    Date = Convert.ToDateTime(Session["SalDate"]);
                }

                String mn = Date.Month.ToString();
                String yy = Date.Year.ToString();
                var lastDayOfMonth = DateTime.DaysInMonth(Date.Year, Date.Month);

                for (int i = 0; i < dtFill.Rows.Count; i++)
                {
                    dtFill.Rows[i]["Days"] = lastDayOfMonth;
                }

                grdMonAttendance.Visible = true;
                btnInsert.Visible = true;
                grdMonAttendance.DataSource = dtFill;
                grdMonAttendance.DataBind();
            }
            //}
        }

        public void FillString()
        {
            //string GetBRId = " select brid from companyview where parentid=" + Session["GrdLevel"].ToString();
            //DataTable dtFillBrId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(GetBRId);

            String str, BrId = "";

            //if (dtFillBrId.Rows.Count > 0)
            //{
            //    foreach (DataRow dtRow in dtFillBrId.Rows)
            //    {
            //        foreach (DataColumn dc in dtFillBrId.Columns)
            //        {
            //            BrId += dtRow[dc].ToString() + ",";
            //        }
            //    }

            //    BrId = BrId.Remove(BrId.Length - 1);

            str = " select num_salary_presentdays presentdays,num_salary_absentdays absentdays,num_salary_allowance1 allowance1,num_salary_deduct1 deduct1,((NUM_SALARY_CENTRAL+NUM_SALARY_STATE+NUM_SALARY_FIXED+num_salary_allowance1+num_salary_allowance2)-(num_salary_deduct1-num_salary_deduct2)) totalwages, ";
            str += " (num_payscal_wages * num_payscal_central)/100 central, (num_payscal_wages * num_payscal_state)/100 state, num_payscal_fixed fixed ";
            str += " from aoup_salary_def ";
            str += " inner join companyview on brid=num_salary_compid ";
            str += " inner join aoup_payscal_def on hoid = num_payscal_compid and num_salary_payscalid = num_payscal_payscalid ";
            str += " where num_salary_compid =" + Session["GrdLevel"] + " ";
            str += " and date_salary_date = '" + Session["SalDate"] + "' and var_salary_authorisedby is null order by num_salary_sevikaid";

            DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            LoadGrid();

            for (int i = 0; i < grdMonAttendance.Rows.Count; i++)
            {
                Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
                TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
                TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");
                TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");
                TextBox txtTotalwages = (TextBox)grdMonAttendance.Rows[i].FindControl("txtTotalwages");

                lblPrsntDays.Text = dtstr.Rows[i]["presentdays"].ToString();
                txtAbsentDays.Text = dtstr.Rows[i]["absentdays"].ToString();
                txtAllow.Text = dtstr.Rows[i]["allowance1"].ToString();
                txtDeduct.Text = dtstr.Rows[i]["deduct1"].ToString();
                txtTotalwages.Text = dtstr.Rows[i]["totalwages"].ToString();
                txtAbsentDays.Enabled = false;
                txtAllow.Enabled = false;
                txtDeduct.Enabled = false;
                txtTotalwages.Enabled = false;

                Label lblcentral = (Label)grdMonAttendance.Rows[i].FindControl("lblcentral");
                Label lblstate = (Label)grdMonAttendance.Rows[i].FindControl("lblstate");
                Label lblfixed = (Label)grdMonAttendance.Rows[i].FindControl("lblfixed");

                lblcentral.Text = dtstr.Rows[i]["central"].ToString();
                lblstate.Text = dtstr.Rows[i]["state"].ToString();
                lblfixed.Text = dtstr.Rows[i]["fixed"].ToString();
            }
            //}
        }*/

        protected void txtAbsentDays_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = (sender as TextBox).Parent.Parent as GridViewRow;

            CalculateWages(row.RowIndex);
        }

        protected void txtAllow_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = (sender as TextBox).Parent.Parent as GridViewRow;

            CalculateWages(row.RowIndex);
        }

        protected void txtDeduct_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = (sender as TextBox).Parent.Parent as GridViewRow;

            CalculateWages(row.RowIndex);
        }

        public void CalculateWages(Int32 RowIndex)
        {
            Int32 i = RowIndex;

            TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
            TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");
            TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");
            TextBox txtTotalwages = (TextBox)grdMonAttendance.Rows[i].FindControl("txtTotalwages");

            Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
            Label lblTotalDays = (Label)grdMonAttendance.Rows[i].FindControl("lblTotalDays");

            Label lblcentral = (Label)grdMonAttendance.Rows[i].FindControl("lblcentral");
            Label lblstate = (Label)grdMonAttendance.Rows[i].FindControl("lblstate");
            Label lblfixed = (Label)grdMonAttendance.Rows[i].FindControl("lblfixed");

            Label lblPayTribal = (Label)grdMonAttendance.Rows[i].FindControl("lblPayTribal");
            Label lblAddCenter = (Label)grdMonAttendance.Rows[i].FindControl("lblAddCenter");
            Label lblAddState = (Label)grdMonAttendance.Rows[i].FindControl("lblAddState");

            if (txtAbsentDays.Text == "")
            {
                lblPrsntDays.Text = "0";
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
                        return;
                    }

                    lblPrsntDays.Text = (Convert.ToInt32(lblTotalDays.Text) - Convert.ToInt32(txtAbsentDays.Text)).ToString();
                    txtAbsentDays.Focus();
                }

                else
                {
                    MessageAlert(" Please Enter Proper Absent Days ", "");
                    return;
                }
            }

            Double CentralAmt = Convert.ToDouble(lblcentral.Text);
            Double StateAmt = Convert.ToDouble(lblstate.Text);
            Double Fixed = Convert.ToDouble(lblfixed.Text);

            Double PayTribalAmt = Convert.ToDouble(lblPayTribal.Text);
            Double AddCenterAmt = Convert.ToDouble(lblAddCenter.Text);
            Double AddStateAmt = Convert.ToDouble(lblAddState.Text);

            Double Allow = 0;
            Double Deduct = 0;

            if (Convert.ToDouble(lblTotalDays.Text) != Convert.ToDouble(lblPrsntDays.Text))
            {
                try
                {
                    CentralAmt = (Convert.ToDouble(lblcentral.Text) / Convert.ToDouble(lblTotalDays.Text)) * Convert.ToDouble(lblPrsntDays.Text);
                }

                catch { }

                try
                {
                    StateAmt = (Convert.ToDouble(lblstate.Text) / Convert.ToDouble(lblTotalDays.Text)) * Convert.ToDouble(lblPrsntDays.Text);
                }

                catch { }

                if (lblfixed.Text != "")
                {
                    Fixed = (Convert.ToDouble(lblfixed.Text) / Convert.ToDouble(lblTotalDays.Text)) * Convert.ToDouble(lblPrsntDays.Text);
                }
            }

            if (txtAllow.Text != "")
            {
                Allow = Convert.ToDouble(txtAllow.Text);
            }

            if (txtDeduct.Text != "")
            {
                Deduct = Convert.ToDouble(txtDeduct.Text);
            }

            Double Totalwages = (CentralAmt + StateAmt + Fixed + Allow + PayTribalAmt + AddCenterAmt + AddStateAmt) - Deduct;

            txtTotalwages.Text = Math.Round(Totalwages).ToString();
        }

        public void FillGrid(DateTime dt)
        {
            String Qry = " select num_salary_presentdays presentdays, branchname brname, var_angnwadimst_angnname angnname, num_sevikamaster_sevikaid sevikaid, ";
            Qry += " num_sevikamaster_compid brid,num_sevakmaster_anganid anganid,num_sevikamaster_desigid desgid,  var_designation_desig desgNM, ";
            Qry += " num_sevikamaster_payscalid payid,var_sevikamaster_name sevikaNM,NULL Days,num_salary_presentdays presentdays,num_salary_absentdays absentdays, ";
            Qry += " num_salary_allowance1 allowance1,num_salary_deduct1 deduct1,nvl(num_salary_add_tribal,0) PayTribal,nvl(num_salary_add_center,0) AddCenter,nvl(num_salary_add_state,0) AddState, ";
            Qry += " ((NUM_SALARY_CENTRAL+NUM_SALARY_STATE+NUM_SALARY_FIXED+num_salary_allowance1+num_salary_allowance2+nvl(num_salary_add_tribal,0)+nvl(num_salary_add_center,0)+nvl(num_salary_add_state,0))-(num_salary_deduct1-num_salary_deduct2)) totalwages, ";
            Qry += " (num_payscal_wages * num_payscal_central)/100 central, (num_payscal_wages * num_payscal_state)/100 state, num_payscal_fixed fixed ";
            Qry += " from aoup_sevikamaster_def a left join aoup_designation_def b  on a.num_sevikamaster_desigid=b.num_designation_desigid ";
            Qry += " left join aoup_angnwadimst_def on num_angnwadimst_angnid=num_sevakmaster_anganid and num_sevikamaster_compid=num_angnwadimst_compid ";
            Qry += " left join companyview on brid=num_sevikamaster_compid left join aoup_salary_def e on a.num_sevikamaster_compid=e.num_salary_compid ";
            Qry += " and a.num_sevikamaster_sevikaid=e.num_salary_sevikaid inner join aoup_payscal_def on hoid = num_payscal_compid ";
            Qry += " and num_salary_payscalid = num_payscal_payscalid and date_salary_date = '" + dt.ToString("dd-MMM-yyyy") + "' ";
            Qry += " where num_sevikamaster_compid =" + Session["GrdLevel"] + " and var_sevikamaster_active='Y' and var_sevikamaster_authorizedby is not null ";
            Qry += " and var_salary_authorisedby is null and var_salary_billnofixed is null and var_salary_billnocentral is null and var_salary_billnostate is null order by num_sevikamaster_sevikaid ";

            DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(Qry);
           
            if (dtstr.Rows.Count > 0)
            {
                DateTime Date = System.DateTime.Now;
                if (Request.QueryString["@"] == "1")
                {
                    Date = Convert.ToDateTime(dtSalDate.Value);
                }
                else
                {
                    Date = Convert.ToDateTime(Session["SalDate"]);
                }

                String mn = Date.Month.ToString();
                String yy = Date.Year.ToString();
                var lastDayOfMonth = DateTime.DaysInMonth(Date.Year, Date.Month);

                for (int i = 0; i < dtstr.Rows.Count; i++)
                {
                    dtstr.Rows[i]["Days"] = lastDayOfMonth;
                }

                grdMonAttendance.Visible = true;
                btnInsert.Visible = true;
                grdMonAttendance.DataSource = dtstr;
                grdMonAttendance.DataBind();

                for (int i = 0; i < grdMonAttendance.Rows.Count; i++)
                {
                    Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
                    TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
                    TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");
                    TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");
                    TextBox txtTotalwages = (TextBox)grdMonAttendance.Rows[i].FindControl("txtTotalwages");

                    lblPrsntDays.Text = dtstr.Rows[i]["presentdays"].ToString();
                    txtAbsentDays.Text = dtstr.Rows[i]["absentdays"].ToString();
                    txtAllow.Text = dtstr.Rows[i]["allowance1"].ToString();
                    txtDeduct.Text = dtstr.Rows[i]["deduct1"].ToString();
                    txtTotalwages.Text = dtstr.Rows[i]["totalwages"].ToString();
                    txtAbsentDays.Enabled = false;
                    txtAllow.Enabled = false;
                    //txtDeduct.Enabled = false;
                    txtDeduct.Enabled = true;
                    txtTotalwages.Enabled = false;

                    Label lblcentral = (Label)grdMonAttendance.Rows[i].FindControl("lblcentral");
                    Label lblstate = (Label)grdMonAttendance.Rows[i].FindControl("lblstate");
                    Label lblfixed = (Label)grdMonAttendance.Rows[i].FindControl("lblfixed");

                    lblcentral.Text = dtstr.Rows[i]["central"].ToString();
                    lblstate.Text = dtstr.Rows[i]["state"].ToString();
                    lblfixed.Text = dtstr.Rows[i]["fixed"].ToString();

                    Label lblPayTribal = (Label)grdMonAttendance.Rows[i].FindControl("lblPayTribal");
                    Label lblAddCenter = (Label)grdMonAttendance.Rows[i].FindControl("lblAddCenter");
                    Label lblAddState =  (Label)grdMonAttendance.Rows[i].FindControl("lblAddState");

                    lblPayTribal.Text = dtstr.Rows[i]["PayTribal"].ToString();
                    lblAddCenter.Text = dtstr.Rows[i]["AddCenter"].ToString();
                    lblAddState.Text = dtstr.Rows[i]["AddState"].ToString();
                }

            }
            else
            {
                MessageAlert(" Record Not Found ", "");
                grdMonAttendance.DataSource = null;
                grdMonAttendance.Visible = false;
                btnInsert.Visible = false;
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            String VchPrepDtl = "";

            Days = Convert.ToDateTime(Session["SalDate"]);
            int month = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("MM")));
            String month1 = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Days.Month);
            int year = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("yyyy")));
            days = DateTime.DaysInMonth(year, month);
            String lstdate = Convert.ToString(days + "-" + month1 + "-" + year);

            for (int i = 0; i < grdMonAttendance.Rows.Count; i++)
            {
                CheckBox ChkMenu = (CheckBox)grdMonAttendance.Rows[i].Cells[0].FindControl("ChkCompany");

                if (ChkMenu.Checked == true)
                {
                    Label lblSevikaId = (Label)grdMonAttendance.Rows[i].FindControl("lblSevikaId");
                    Label lblSevkiaNm = (Label)grdMonAttendance.Rows[i].FindControl("lblSevkiaNm");
                    Label lblDesg = (Label)grdMonAttendance.Rows[i].FindControl("lblDesg");
                    Label lblTotalDays = (Label)grdMonAttendance.Rows[i].FindControl("lblTotalDays");
                    TextBox txtAbsentDays = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAbsentDays");
                 
                    Label lblPrsntDays = (Label)grdMonAttendance.Rows[i].FindControl("lblPrsntDays");
                 
                    TextBox txtAllow = (TextBox)grdMonAttendance.Rows[i].FindControl("txtAllow");
                 
                    TextBox txtDeduct = (TextBox)grdMonAttendance.Rows[i].FindControl("txtDeduct");
                 
                    VchPrepDtl += lblSevikaId.Text + "#" + lblPrsntDays.Text + "#" + txtAbsentDays.Text + "#" + txtAllow.Text + "#" + txtDeduct.Text + "$";
                }
            }
            if (VchPrepDtl != "")
            {
                VchPrepDtl = VchPrepDtl.Substring(0, VchPrepDtl.Length - 1);
            }
            else
            {
                MessageAlert("Please select atleast one record", "");
                return;
            }
            objattend.UserId = Session["UserId"].ToString();
            objattend.CompID = Convert.ToInt32(Session["GrdLevel"]);
            objattend.SevikaID = 0;
            objattend.Date = Convert.ToString(lstdate);
            objattend.PrjTypeId = 0;// Convert.ToInt32(ddlprjType.SelectedValue);
            objattend.TotalDays = days;
            objattend.ParamStr = VchPrepDtl;

            objattend.AuthorisedSalaryCal();

            if (objattend.ErrorCode == -100)
            {
                MessageAlert(objattend.ErrorMsg, "../Transaction/FrmMonthlyAttendenceAuthList.aspx");
                return;
            }
            else
            {
                MessageAlert(objattend.ErrorMsg, "");
                return;
            }
        }

    
    }
}