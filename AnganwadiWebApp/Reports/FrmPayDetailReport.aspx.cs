using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Drawing;

namespace AnganwadiWebApp.Reports
{
    public partial class FrmPayDetailReport : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                // LblGrdHead.Text = Session["LblGrdHead"].ToString();
                FillCaptcha();
                lblSavikaName.Visible = false;
                txtSavikaName.Visible = false;
                lnkBtnDownload.Visible = false;
                Label1.Visible = false;
            }
        }
        public void FillCaptcha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                Session["captcha"] = captcha.ToString();
                string ImageUrl = "~/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                imgPayConfCaptcha.ImageUrl = ImageUrl;
            }
            catch
            {
                throw;
            }
        }

        protected void btnPayConfRefresh_Click(object sender, EventArgs e)
        {
            FillCaptcha();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtAdharNumber.Text != "" && txtConfirmAdharNumber.Text != "")
            {
                if (Convert.ToInt64(txtAdharNumber.Text.Trim()) == Convert.ToInt64(txtConfirmAdharNumber.Text))
                {

                    String query = "";
                    query += "select * from (SELECT  var_sevikamaster_name savikaName, to_char(a.date_salary_date,'MON') || ' ' || Extract (Year from a.date_salary_date)  Salary_For,";
                    query += " date_paydtls_paydate Credited_Date, var_paydtls_schemespecificid2 cpsmscode, var_paydtls_bankname_bankinn as Bank_Name ,";
                    query += " var_paydtls_accnumasperbank1 as Accno, num_paydtls_totalamount totalpaid,var_paydtls_creditstatuspb1 Status FROM aoup_salary_def a ";
                    query += " inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_salary_sevikaid   ";
                    query += " left join aoup_paydetails_def on var_paydtls_craadhaarnumpb=var_sevikamaster_aadharno and date_salary_date=date_paydtls_todate "; 
                    query += " where VAR_SEVIKAMASTER_AADHARNO ='" + txtAdharNumber.Text.Trim() + "'  order by  date_salary_date desc) WHERE ROWNUM <= 12";

                    //query += "select * from (SELECT  var_sevikamaster_name savikaName, to_char(date_paydtls_paydate,'MON') || ' ' || Extract (Year from date_paydtls_paydate)  Salary_For,";
                    //query += " date_paydtls_paydate Credited_Date, var_paydtls_schemespecificid2 cpsmscode, var_paydtls_bankname_bankinn as Bank_Name ,";
                    //query += " var_paydtls_accnumasperbank1 as Accno, num_paydtls_totalamount totalpaid,var_paydtls_creditstatuspb1 Status FROM aoup_paydetails_def ";
                    //query += " inner join aoup_sevikamaster_def on var_sevikamaster_aadharno=var_paydtls_craadhaarnumpb   ";
                    //query += " where VAR_SEVIKAMASTER_AADHARNO ='" + txtAdharNumber.Text.Trim() + "'  order by  date_paydtls_paydate desc) WHERE ROWNUM <= 12";


                    //query += "select * from (SELECT var_sevikamaster_name savikaName,  to_char(a.date_salary_date,'MON') || ' ' || Extract (Year  from a.date_salary_date)  Salary_For,";
                    //query += " date_salary_date Credited_Date, a.var_salary_cpsmscode cpsmscode, null as Bank_Name , null as Accno, a.num_salary_totalpaid  totalpaid";
                    //query += " FROM aoup_salary_def a inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_salary_sevikaid";
                    //query += " where VAR_SEVIKAMASTER_AADHARNO ='" + txtAdharNumber.Text.Trim() + "'  ";
                    //query += " order by  date_salary_date desc) WHERE ROWNUM <= 12";

                   

                    DataTable dtTblPayDetail = new DataTable();
                    dtTblPayDetail = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);
                    if (dtTblPayDetail.Rows.Count > 0)
                    {
                        Label1.Visible = true;
                        lnkBtnDownload.Visible = true;
                        trSevika.Visible = true;
                        grdMonAttendance.Visible = true;
                        txtSavikaName.Visible = true;
                        lblSavikaName.Visible = true;
                        txtSavikaName.Text = dtTblPayDetail.Rows[0]["savikaName"].ToString();
                        grdMonAttendance.DataSource = dtTblPayDetail;
                        grdMonAttendance.DataBind();
                        ViewState["CurrentTable"] = dtTblPayDetail;
                    }
                    else
                    {
                        MessageAlert("No Record Found", "");
                        trSevika.Visible = false;
                        lnkBtnDownload.Visible = false;
                        grdMonAttendance.Visible = false;
                        return;
                    }
                }
                else
                {
                    MessageAlert("Aadhar number and Confirm Aadhar Number not match", "");
                    trSevika.Visible = false;
                    lnkBtnDownload.Visible = false;
                    grdMonAttendance.Visible = false;
                    return;
                }
            }
            else
            {
                MessageAlert("Please Enter Aadhar number", "");
                trSevika.Visible = false;
                lnkBtnDownload.Visible = false;
                grdMonAttendance.Visible = false;
                return;
            }


            if (txtPayConfCaptcha.Text != Session["captcha"].ToString())
            {
                MessageAlert("Please Enter Proper Captcha", "");
                txtPayConfCaptcha.Text = "";
                txtPayConfCaptcha.Focus();
                FillCaptcha();
                trSevika.Visible = false;
                lnkBtnDownload.Visible = false;
                grdMonAttendance.Visible = false;
                return;
            }

        }

        protected void grdMonAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var status = (Label)e.Row.FindControl("lblStatus");
                if (status.Text == "Credit Success")
                {
                    // e.Row.Cells[2].BackColor = Color.Blue;
                    e.Row.Cells[6].ForeColor = Color.Green;
                    e.Row.Cells[6].Font.Bold = true;
                }
                else if (status.Text == "Credit Failed")
                {
                    e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.Cells[6].Font.Bold = true;
                }
                else
                {
                    e.Row.Cells[6].ForeColor = Color.Black;
                    e.Row.Cells[6].Font.Bold = true;
                }
            }

        }


        protected void lnkBtnDownload_Click(object sender, EventArgs e)
        {
            GenerateReport((DataTable)ViewState["CurrentTable"]);
        }
        public void GenerateReport(DataTable tblAbstract)
        {

            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "PaymentDetail_as_on_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtPayDetailReport.rpt");
            ExportPath = Server.MapPath("..\\ImageGarbage\\") + PDFNAME;

            String[] Parameter = new String[0];
            String[] ParameterVal = new String[0];


            Byte[] btFile = AnganwadiLib.Methods.MstMethods.Report.ViewReport("", tblAbstract, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");

            Response.AddHeader("Content-disposition", "attachment; filename=" + PDFNAME);

            //Response.ContentType = "application/octet-stream";
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(btFile);
            Response.TransmitFile(ExportPath);
            //Response.End();
        }
    }
}