using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmLICPaymentMst : System.Web.UI.Page
    {
        BoLICPayment ObjLICStatus = new BoLICPayment();
        DataTable dt = new DataTable();

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
                txtSevikaId.Text = Session["SevikaId"].ToString();
                txtReason.Text = Session["Reason"].ToString();
                TxtClaimAmount.Text = Session["Amount"].ToString();

                gridVIEWData();
                grdLICDEF.DataSource = dt;
                grdLICDEF.DataBind();
                Session["Data"] = dt;
            }
          

        }

        private void gridVIEWData()
        {
            dt.Columns.Add(new DataColumn("AccountNo", typeof(string)));
            dt.Columns.Add(new DataColumn("UtrNo", typeof(string)));
            dt.Columns.Add(new DataColumn("PaymentDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Amount", typeof(double)));
        }

        protected void BtnAddList_Click(object sender, EventArgs e)
        {
            if (txtAccountNo.Text == "")
            {
                MessageAlert("Please Enter Account No", "");
                txtAccountNo.Focus();
                return;
            }
            if (TxtUtrNo.Text == "")
            {
                MessageAlert("Please Enter URN No", "");
                TxtUtrNo.Focus();
                return;
            }
            if (DtPaymentDate.Text == "")
            {
                MessageAlert("Please Select Payment Date", "");
                DtPaymentDate.Focus();
                return;
            }
            if (TxtAmount.Text == "")
            {
                MessageAlert("Please Enter Amount", "");
                TxtAmount.Focus();
                return;
            }

            if (Session["Data"] != null)
            dt = (DataTable)Session["Data"];
            
            DataRow dr = dt.NewRow();
            dr["AccountNo"] = txtAccountNo.Text;
            dr["UtrNo"] = TxtUtrNo.Text;
            dr["PaymentDate"] = Convert.ToDateTime(DtPaymentDate.Value).ToString("dd-MMM-yyyy");
            dr["Amount"] = TxtAmount.Text;
            dt.Rows.Add(dr);
            Session["Data"] = dt;
            grdLICDEF.DataSource = dt;
            grdLICDEF.DataBind();

            txtAccountNo.Text =null;
            TxtUtrNo.Text = null;
            TxtAmount.Text = null;
            DtPaymentDate.Text = null;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string Str = null;

            foreach (GridViewRow row in grdLICDEF.Rows)
            {
                string AccountNo = row.Cells[1].Text;
                string UtrNo = row.Cells[2].Text;
                DateTime DtPay = Convert.ToDateTime(row.Cells[3].Text);
                String DatePay = DtPay.ToString("dd-MMM-yyyy");
                double Amount = Convert.ToDouble(row.Cells[4].Text);

                Str += AccountNo + "~" + UtrNo + "~" + DatePay + "~" + Amount + "#";
            }

            if (Str.Length > 0)
            {
                Str = Str.Substring(0, Str.Length - 1);
            }

            ObjLICStatus.SevikaID = Convert.ToInt32(Session["SevikaId"].ToString());
            ObjLICStatus.UserID = Session["UserId"].ToString();
            ObjLICStatus.Str = Str;
            ObjLICStatus.InsertLICPayment();

            if (ObjLICStatus.ErrorCode == -100)
            {
                MessageAlert(ObjLICStatus.ErrorMessage, "../HomePage/FrmDashboard.aspx");
                return;
            }
            else
            {
                MessageAlert(ObjLICStatus.ErrorMessage, "");
                return;
            }

        }

        protected void grdLICDEF_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            dt = Session["Data"] as DataTable;
            dt.Rows.RemoveAt(e.RowIndex);
            dt.AcceptChanges();
            Session["Data"] = dt;
            grdLICDEF.DataSource = dt;
            grdLICDEF.DataBind();
        }
    }
}