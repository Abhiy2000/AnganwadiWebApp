using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmCheckPaymentDetail : System.Web.UI.Page
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
                if (Convert.ToInt32(txtAdharNumber.Text.Trim()) == Convert.ToInt32(txtConfirmAdharNumber.Text))
                {

                    String query = "";
                    query += "select * from (SELECT var_sevikamaster_name savikaName,  to_char(a.date_salary_date,'MON') || ' ' || Extract (Year  from a.date_salary_date)  Salary_For,";
                    query += " date_salary_date Credited_Date, a.var_salary_cpsmscode cpsmscode, null as Bank_Name , null as Accno, a.num_salary_totalpaid  totalpaid";
                    query += " FROM aoup_salary_def a inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_salary_sevikaid";
                    query += " where VAR_SEVIKAMASTER_AADHARNO ='" + txtAdharNumber.Text.Trim() +"'  ";
                    query += " order by  date_salary_date desc) WHERE ROWNUM <= 12";

                    DataTable dtTblPayDetail = new DataTable();
                    dtTblPayDetail = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);
                    if (dtTblPayDetail.Rows.Count > 0)
                    {
                        Label1.Visible = true;
                        lnkBtnDownload.Visible = true;
                        txtSavikaName.Visible = true;
                        lblSavikaName.Visible = true;
                        txtSavikaName.Text=dtTblPayDetail.Rows[0]["savikaName"].ToString();
                        grdMonAttendance.DataSource = dtTblPayDetail;
                        grdMonAttendance.DataBind();
                    }
                    else
                    {
                        MessageAlert("No Record Found", "");
                        return;
                    }
                }
            }
            else
            {
                MessageAlert("Please Enter Adhar number and verify it","");
                return;
            }

        }


        protected void lnkBtnDownload_Click(object sender, EventArgs e)
        {

        }
    }
}