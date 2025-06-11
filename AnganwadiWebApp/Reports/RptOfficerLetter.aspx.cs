using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Globalization;
using AnganwadiLib.Methods;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Reports
{
    public partial class RptOfficerLetter : System.Web.UI.Page
    {
        BoLastNo objLast = new BoLastNo();
        DateTime Days = System.DateTime.Now;
        int days = 0;
        Int32 CentralBill, StateBill, FixedBill;
        string CentralWrds = "";
        string StateWrds = "";
        string FixedWrds = "";
        string TotalPaidWrds = "";

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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();
            if (!IsPostBack)
            {
                // LoadGrid();
                //dtSalDate.Focus();
                PnlSerch.Visible = true;
                BindDropdrownlist();

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

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();

                    ddlDivision.Enabled = false;

                }

                else if (BRCategory == 4)   // CDPO
                {
                    str = "select a.num_corporation_corpid cdpo, a.num_corporation_parentid dis, b.num_corporation_parentid div ";
                    str += "from aoup_corporation_mas a ";
                    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();

                    ddlDivision.Enabled = false;
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

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDivision.Enabled = false;

                    PnlSerch.Visible = true;
                }
            }
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


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            string billdet = " select count(num_salary_sevikaid) SevikaCount, sum(nvl(num_salary_central,0)) Central,sum(nvl(num_salary_state,0)) State, ";
            billdet += " sum(nvl(num_salary_fixed,0)) Fixed, sum(nvl(num_salary_totalpaid,0)) TotalPaid ";
            billdet += " from aoup_salary_def  ";
            billdet += " left join corpinfo on num_salary_compid=bitid  ";
            billdet += " where var_salary_billnofixed is not null and var_salary_billnocentral is not null  ";
            billdet += " and var_salary_billnostate is not null ";
            billdet += " and stateid =" + TblGetHoId.Rows[0]["hoid"].ToString();
            if (ddlDivision.SelectedValue != "")
            {
                billdet += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
            }
            billdet += " and trunc(date_salary_date)='" + lstdate + "' ";

            DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(billdet);

            if (dtstr.Rows.Count > 0)
            {
                if (dtstr.Rows[0]["SevikaCount"].ToString() != "0")
                {
                    Int64 Central = Convert.ToInt64(dtstr.Rows[0]["Central"].ToString());
                    CentralWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(Central);

                    Int64 State = Convert.ToInt64(dtstr.Rows[0]["State"].ToString());
                    StateWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(State);

                    Int64 Fixed = Convert.ToInt64(dtstr.Rows[0]["Fixed"].ToString());
                    FixedWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(Fixed);

                    Int64 TotalPaid = Convert.ToInt64(dtstr.Rows[0]["TotalPaid"].ToString());
                    TotalPaidWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(TotalPaid);

                    //Decimal Central = Convert.ToDecimal(dtstr.Rows[0]["Central"].ToString());
                    //CentralWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(Central);

                    //Decimal State = Convert.ToDecimal(dtstr.Rows[0]["State"].ToString());
                    //StateWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(State);

                    //Decimal Fixed = Convert.ToDecimal(dtstr.Rows[0]["Fixed"].ToString());
                    //FixedWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(Fixed);

                    //Decimal TotalPaid = Convert.ToDecimal(dtstr.Rows[0]["TotalPaid"].ToString());
                    //TotalPaidWrds = AnganwadiLib.Methods.MstMethods.funNumToWordConvert(TotalPaid);

                    GenerateReport(dtstr);
                    ViewState["CurrentTable"] = dtstr;
                }
                else
                {
                    MessageAlert("Record Not found", "");
                    return;
                }
            }
            else
            {
                MessageAlert("Record Not found", "");
                return;
            }
        }

        public void GenerateReport(DataTable tblOfficeLetter)
        {
            string month = ddlMonth.SelectedItem.Text.Trim();
            string yr = ddlYear.SelectedItem.Text.Trim();
            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "OfficerLetter_as_on_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtLLetter.rpt");
            ExportPath = Server.MapPath("..\\ImageGarbage\\") + PDFNAME;

            String[] Parameter = new String[7];
            String[] ParameterVal = new String[7];

            Parameter[0] = "SelDate";
            Parameter[1] = "UserId";
            Parameter[2] = "Division";
            Parameter[3] = "CenterAmt";
            Parameter[4] = "StateAmt";
            Parameter[5] = "FixedAmt";
            Parameter[6] = "TotAmt";

            ParameterVal[0] = month + '-' + yr;
            ParameterVal[1] = Session["UserId"].ToString();
            ParameterVal[2] = ddlDivision.SelectedItem.Text;
            ParameterVal[3] = CentralWrds;
            ParameterVal[4] = StateWrds;
            ParameterVal[5] = FixedWrds;
            ParameterVal[6] = TotalPaidWrds;


            tblOfficeLetter.TableName = "tblOfficeLetter";

            Byte[] btFile = AnganwadiLib.Methods.MstMethods.Report.ViewReport("", tblOfficeLetter, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");

            Response.AddHeader("Content-disposition", "attachment; filename=" + PDFNAME);

            //Response.ContentType = "application/octet-stream";
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(btFile);
            Response.TransmitFile(ExportPath);
            //Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered (Export To Excel Is not working)*/
        }


    }

}