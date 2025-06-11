using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmDiffBillGenerationMst : System.Web.UI.Page
    {
        BoBillGen objBillGen = new BoBillGen();

        int days = 0;

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
                BindDropdrownlist();
                btnGenBill.Visible = false;

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

                    //ddlDivision_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                   // MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                }
            }
        }

        //protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlDistrict.DataSource = "";
        //    ddlDistrict.DataBind();

        //    if (ddlDivision.SelectedValue.ToString() != "")
        //    {
        //        MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
        //    }
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        public void LoadGrid()
        {
            /*if (ddlDivision.SelectedValue == "")
            {
                MessageAlert("Please select Division", "");
                return;
            }*/

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            try
            {
                String det = " SELECT branchname District, count(num_salarydiff_sevikaid) SevikaCount, ";
                if (rbdCentral.Checked == true)
                {
                    det += " sum(num_salarydiff_newcentral) Amount ";
                }
                if (rbdState.Checked == true)
                {
                    det += " sum(num_salarydiff_newstate) Amount ";
                }
                //if (rbdFixed.Checked == true)
                //{
                //    det += " sum(num_salarydiff_newfixed) Amount ";
                //}
                det += " FROM aoup_salarydiff_def left join corpinfo on bitid=num_salarydiff_compid left join companyview on distid=brid ";
                det += " left join aoup_designation_def on num_designation_desigid=num_salarydiff_desigid ";
                det += " left Join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_salarydiff_sevikaid ";
                det += " where stateid=" + TblGetHoId.Rows[0]["hoid"].ToString();
                if (ddlDivision.SelectedValue != "")
                {
                    det += " and parentid = " + ddlDivision.SelectedValue + " and brcategory=3 ";
                }
                det += " and trunc(date_salarydiff_date)='" + lstdate + "' ";
                if (ddlDistrict.SelectedValue != "")
                {
                    det += " and brid=" + ddlDistrict.SelectedValue;
                }
                if (rbdBoth.Checked == false)
                {
                    if (rbdWorker.Checked == true && rbdCentral.Checked == true)
                    {
                        det += " and var_designation_flag='W' and var_salarydiff_billnocentral is null ";
                    }
                    if (rbdWorker.Checked == true && rbdState.Checked == true)
                    {
                        det += " and var_designation_flag='W' and var_salarydiff_billnostate is null ";
                    }
                    if (rbdWorker.Checked == true) //&& rbdFixed.Checked == true
                    {
                        det += " and var_designation_flag='W' and var_salarydiff_billnofixed is null ";
                    }
                    if (rbdHelper.Checked == true && rbdCentral.Checked == true)
                    {
                        det += " and var_designation_flag='H' and var_salarydiff_billnocentral is null ";
                    }
                    if (rbdHelper.Checked == true && rbdState.Checked == true)
                    {
                        det += " and var_designation_flag='H' and var_salarydiff_billnostate is null ";
                    }
                    if (rbdHelper.Checked == true )//&& rbdFixed.Checked == true
                    {
                        det += " and var_designation_flag='H' and var_salarydiff_billnofixed is null ";
                    }
                }
                else
                {
                    if (rbdCentral.Checked == true)
                    {
                        det += " and var_salarydiff_billnocentral is null ";
                    }
                    if (rbdState.Checked == true)
                    {
                        det += " and var_salarydiff_billnostate is null ";
                    }
                    //if (rbdFixed.Checked == true)
                    //{
                    //    det += " and var_salarydiff_billnofixed is null ";
                    //}
                }
                det += " and num_salarydiff_presentdays > 0 "; //----------10-03-18
                //if (rbdRegistered.Checked == true)
                //{
                    det += " and var_sevikamaster_cpsmscode is not null and var_salarydiff_cpsmscode is not null ";
                //}
                //if (rbdUnregistered.Checked == true)
                //{
                //    det += " and var_sevikamaster_cpsmscode is null and var_salarydiff_cpsmscode is null ";
                //}
                det += " and var_sevikamaster_authorizedby is not null  ";//and var_salary_authorisedby is not null
                det += " group by branchname order by branchname";

                DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(det);

                if (dtstr.Rows.Count > 0)
                {
                    grdDet.DataSource = dtstr;
                    grdDet.DataBind();
                    grdDet.Visible = true;
                    btnGenBill.Visible = true;

                    decimal total = Math.Round(dtstr.AsEnumerable().Sum(row => row.Field<decimal>("SevikaCount")));
                    grdDet.FooterRow.Cells[1].Text = total.ToString("0");

                    decimal total1 = Math.Round(dtstr.AsEnumerable().Sum(row => row.Field<decimal>("Amount")));
                    grdDet.FooterRow.Cells[2].Text = total1.ToString("0");
                }
                else
                {
                    grdDet.DataSource = null;
                    grdDet.Visible = false;
                    btnGenBill.Visible = false;
                    MessageAlert("Record Not found", "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
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

        protected void btnGenBill_Click(object sender, EventArgs e)
        {
            if (dtBillDate.Text == "")
            {
                MessageAlert("Please select Bill Date", "");
                return;
            }
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            objBillGen.UserId = Session["UserId"].ToString();
            objBillGen.CompID = Convert.ToInt32(TblGetHoId.Rows[0]["hoid"].ToString());
            if (ddlDivision.SelectedValue != "")
            {
                if (ddlDistrict.SelectedValue != "")
                {
                    objBillGen.CompID = Convert.ToInt32(ddlDistrict.SelectedValue);
                }
                else
                {
                    objBillGen.CompID = Convert.ToInt32(ddlDivision.SelectedValue);
                }
            }
            objBillGen.SalDate = Convert.ToDateTime(lstdate);
            objBillGen.BillDate = Convert.ToDateTime(dtBillDate.Value);
            if (rbdBoth.Checked == true)
            {
                objBillGen.DesgType = "HW";
            }
            if (rbdWorker.Checked == true)
            {
                objBillGen.DesgType = "W";
            }
            if (rbdHelper.Checked == true)
            {
                objBillGen.DesgType = "H";
            }
            if (rbdCentral.Checked == true)
            {
                objBillGen.SalSlab = "C";
            }
            if (rbdState.Checked == true)
            {
                objBillGen.SalSlab = "S";
            }
            //if (rbdFixed.Checked == true)
            //{
            //    objBillGen.SalSlab = "F";
            //}
            //if (rbdRegistered.Checked == true)
            //{
                objBillGen.SevikaType = "R";
            //}
            //if (rbdUnregistered.Checked == true)
            //{
            //    objBillGen.SevikaType = "U";
            //}
            objBillGen.ParentId = Convert.ToInt32(TblGetHoId.Rows[0]["hoid"].ToString());

            objBillGen.BoBillGen_2();

            if (objBillGen.ErrorCode == -100)
            {
                MessageAlert(objBillGen.ErrorMsg, "../Transaction/FrmDiffBillGenerationMst.aspx");
                return;
            }
            else
            {
                MessageAlert(objBillGen.ErrorMsg, "");
                return;
            }
        }
    }
}