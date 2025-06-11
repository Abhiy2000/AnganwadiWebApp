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
    public partial class FrmLICDocUploadMst : System.Web.UI.Page
    {
        Cls_Business_LICStatus ObjLICStatus = new Cls_Business_LICStatus();
        int days = 0;
        DateTime doj;
        DataTable TblGetHOid = new DataTable();

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
            //String SevikaId = Session["SevikaID"].ToString();
            //MessageAlert("Current Page is under development ", "");
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            if (!IsPostBack)
            {

                LblGrdHead.Text = Session["LblGrdHead"].ToString();

                //MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");
                //ddlAnganID.Focus();

                if (Session["SevikaType"].ToString() == "External")
                {
                    grdExternalSavikaList_set();
                }
                else
                {
                    grdSavikaList_set();
                }


                UserLevel = Session["brcategory"].ToString();


            }
        }
        private void grdExternalSavikaList_set()
        {
            #region "Load Grid"

            String query = "";
            query += "  SELECT a.num_licsevika_sevikaid sevikaid, m.var_angnwadimst_angnname anganwadiid,a.var_licsevika_name sevikaname, a.date_licsevika_birthdate birthdate,  ";
            query += " a.var_licsevika_address address, a.num_licsevika_mobileno mobileno, a.var_licsevika_phoneno phoneno, var_bank_bankname bankid,  ";
            query += " b.num_bankbranch_branchid branchid, b.var_bankbranch_ifsccode ifsccode, a.var_licsevika_panno pan, a.var_licsevika_aadharno aadharno,  ";
            query += " b.var_bankbranch_branchname branchnm,a.num_licsevika_branchid sevibranchid, a.var_licsevika_desigcode desigid, a.var_licsevika_accno accno,   ";
            query += " a.var_licsevika_remark seviremark, a.var_licsevika_middlename middlename, a.var_licsevika_village village, a.var_licsevika_pincode pincode  ";
            query += " FROM aoup_licsevika_def a LEFT JOIN aoup_bankbranch_def b ON a.num_licsevika_branchid = b.num_bankbranch_branchid  ";
            query += " INNER JOIN aoup_angnwadimst_def m ON m.num_angnwadimst_angnid = a.num_licsevika_anganid  ";
            query += " LEFT JOIN aoup_bank_def g ON b.num_bankbranch_bankid = g.num_bank_bankid  ";
            query += " WHERE num_licsevika_sevikaid = '" + Session["SevikaID"] + "'";

            DataTable dtSavikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtSavikaList.Rows.Count > 0)
            {
                if (dtSavikaList.Rows[0]["SevikaID"].ToString() != "" && dtSavikaList.Rows[0]["SevikaID"].ToString() != null)
                {
                    txtSevikaId.Text = dtSavikaList.Rows[0]["SevikaID"].ToString();
                }
                if (dtSavikaList.Rows[0]["AnganwadiID"].ToString() != "" && dtSavikaList.Rows[0]["AnganwadiID"].ToString() != null)
                {
                    TxtAngwadiName.Text = dtSavikaList.Rows[0]["AnganwadiID"].ToString();
                }
                if (dtSavikaList.Rows[0]["SevikaName"].ToString() != "" && dtSavikaList.Rows[0]["SevikaName"].ToString() != null)
                {
                    txtName.Text = dtSavikaList.Rows[0]["SevikaName"].ToString();
                }

                if (dtSavikaList.Rows[0]["Birthdate"].ToString() != "" && dtSavikaList.Rows[0]["Birthdate"].ToString() != null)
                {
                    DtDob.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["Birthdate"]).ToString("dd/MM/yyyy");
                }

                if (dtSavikaList.Rows[0]["Address"].ToString() != "" && dtSavikaList.Rows[0]["Address"].ToString() != null)
                {
                    txtAddr.Text = dtSavikaList.Rows[0]["Address"].ToString();
                }
                if (dtSavikaList.Rows[0]["MobileNo"].ToString() != "" && dtSavikaList.Rows[0]["MobileNo"].ToString() != null)
                {
                    TxtMobNo.Text = dtSavikaList.Rows[0]["MobileNo"].ToString();
                }
                if (dtSavikaList.Rows[0]["PhoneNo"].ToString() != "" && dtSavikaList.Rows[0]["PhoneNo"].ToString() != null)
                {
                    txtphone.Text = dtSavikaList.Rows[0]["PhoneNo"].ToString();
                }

                if (dtSavikaList.Rows[0]["BankID"].ToString() != "" && dtSavikaList.Rows[0]["BankID"].ToString() != null)
                {
                    txtBank.Text = dtSavikaList.Rows[0]["BankID"].ToString();
                }

                if (dtSavikaList.Rows[0]["BranchID"].ToString() != "" && dtSavikaList.Rows[0]["BranchID"].ToString() != null)
                {
                    TxtBranch.Text = dtSavikaList.Rows[0]["branchnm"].ToString();
                }

                if (dtSavikaList.Rows[0]["AccNo"].ToString() != "" && dtSavikaList.Rows[0]["AccNo"].ToString() != null)
                {
                    TxtAccNO.Text = dtSavikaList.Rows[0]["AccNo"].ToString();
                }
                if (dtSavikaList.Rows[0]["SeviRemark"].ToString() != "" && dtSavikaList.Rows[0]["SeviRemark"].ToString() != null)
                {
                    TxtRemark.Text = dtSavikaList.Rows[0]["SeviRemark"].ToString();
                }
                if (dtSavikaList.Rows[0]["DesigID"].ToString() != "" && dtSavikaList.Rows[0]["DesigID"].ToString() != null)
                {
                    rdbActive.SelectedValue = dtSavikaList.Rows[0]["DesigID"].ToString();

                }

                if (dtSavikaList.Rows[0]["MiddleName"].ToString() != "" && dtSavikaList.Rows[0]["MiddleName"].ToString() != null)
                {
                    TxtMidName.Text = dtSavikaList.Rows[0]["MiddleName"].ToString();
                }
                if (dtSavikaList.Rows[0]["Village"].ToString() != "" && dtSavikaList.Rows[0]["Village"].ToString() != null)
                {
                    TxtVillage.Text = dtSavikaList.Rows[0]["Village"].ToString();
                }
                if (dtSavikaList.Rows[0]["PinCode"].ToString() != "" && dtSavikaList.Rows[0]["PinCode"].ToString() != null)
                {
                    TxtPinCode.Text = dtSavikaList.Rows[0]["PinCode"].ToString();
                }
                if (dtSavikaList.Rows[0]["IFSCCode"].ToString() != "" && dtSavikaList.Rows[0]["IFSCCode"].ToString() != null)
                {
                    txtIFSC.Text = dtSavikaList.Rows[0]["IFSCCode"].ToString();
                }

            }
            #endregion
        }
        private void grdSavikaList_set()
        {
            #region "Load Grid"

            String query = "";
            query += "  Select a.num_sevikamaster_sevikaid SevikaID,m.var_angnwadimst_angnname AnganwadiID, a.var_sevikamaster_name SevikaName,a.date_sevikamaster_birthdate Birthdate,  ";
            query += "  a.var_sevikamaster_address Address,   a.num_sevikamaster_mobileno MobileNo, a.var_sevikamaster_phoneno PhoneNo,var_bank_bankname BankID,  ";
            query += "  b.num_bankbranch_branchid BranchID,  b.var_bankbranch_ifsccode IFSCCode,    a.var_sevikamaster_panno PAN, a.var_sevikamaster_aadharno AadharNo,  ";
            query += "  b.var_bankbranch_branchname branchnm, a.num_sevikamaster_branchid SeviBranchId,a.num_sevikamaster_desigid DesigID,    a.var_sevikamaster_accno AccNo,  ";
            query += "  a.var_sevikamaster_remark SeviRemark,a.var_sevikamaster_middlename MiddleName, a.var_sevikamaster_village Village, a.var_sevikamaster_pincode PinCode,var_designation_flag  ";
            query += "  FROM aoup_sevikamaster_def a  left join aoup_bankbranch_def b on a.num_sevikamaster_branchid=b.num_bankbranch_branchid    ";
            query += " inner join aoup_AngnwadiMst_def m on m.num_angnwadimst_angnid=a.num_sevakmaster_anganid  ";
            query += " left join aoup_bank_def g on b.num_bankbranch_bankid=g.num_bank_bankid inner join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
            query += " where "; //num_licsevika_compid='" + Session["GrdLevel"].ToString() + "' and 
            query += " num_sevikamaster_sevikaid='" + Session["SevikaID"] + "'";


            DataTable dtSavikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtSavikaList.Rows.Count > 0)
            {
                if (dtSavikaList.Rows[0]["SevikaID"].ToString() != "" && dtSavikaList.Rows[0]["SevikaID"].ToString() != null)
                {
                    txtSevikaId.Text = dtSavikaList.Rows[0]["SevikaID"].ToString();
                }
                if (dtSavikaList.Rows[0]["AnganwadiID"].ToString() != "" && dtSavikaList.Rows[0]["AnganwadiID"].ToString() != null)
                {
                    TxtAngwadiName.Text = dtSavikaList.Rows[0]["AnganwadiID"].ToString();
                }
                if (dtSavikaList.Rows[0]["SevikaName"].ToString() != "" && dtSavikaList.Rows[0]["SevikaName"].ToString() != null)
                {
                    txtName.Text = dtSavikaList.Rows[0]["SevikaName"].ToString();
                }

                if (dtSavikaList.Rows[0]["Birthdate"].ToString() != "" && dtSavikaList.Rows[0]["Birthdate"].ToString() != null)
                {
                    DtDob.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["Birthdate"]).ToString("dd/MM/yyyy");
                }

                if (dtSavikaList.Rows[0]["Address"].ToString() != "" && dtSavikaList.Rows[0]["Address"].ToString() != null)
                {
                    txtAddr.Text = dtSavikaList.Rows[0]["Address"].ToString();
                }
                if (dtSavikaList.Rows[0]["MobileNo"].ToString() != "" && dtSavikaList.Rows[0]["MobileNo"].ToString() != null)
                {
                    TxtMobNo.Text = dtSavikaList.Rows[0]["MobileNo"].ToString();
                }
                if (dtSavikaList.Rows[0]["PhoneNo"].ToString() != "" && dtSavikaList.Rows[0]["PhoneNo"].ToString() != null)
                {
                    txtphone.Text = dtSavikaList.Rows[0]["PhoneNo"].ToString();
                }

                if (dtSavikaList.Rows[0]["BankID"].ToString() != "" && dtSavikaList.Rows[0]["BankID"].ToString() != null)
                {
                    txtBank.Text = dtSavikaList.Rows[0]["BankID"].ToString();
                }

                if (dtSavikaList.Rows[0]["BranchID"].ToString() != "" && dtSavikaList.Rows[0]["BranchID"].ToString() != null)
                {
                    TxtBranch.Text = dtSavikaList.Rows[0]["branchnm"].ToString();
                }

                if (dtSavikaList.Rows[0]["AccNo"].ToString() != "" && dtSavikaList.Rows[0]["AccNo"].ToString() != null)
                {
                    TxtAccNO.Text = dtSavikaList.Rows[0]["AccNo"].ToString();
                }
                if (dtSavikaList.Rows[0]["SeviRemark"].ToString() != "" && dtSavikaList.Rows[0]["SeviRemark"].ToString() != null)
                {
                    TxtRemark.Text = dtSavikaList.Rows[0]["SeviRemark"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_designation_flag"].ToString() != "" && dtSavikaList.Rows[0]["var_designation_flag"].ToString() != null)
                {
                    rdbActive.SelectedValue = dtSavikaList.Rows[0]["var_designation_flag"].ToString();

                }

                if (dtSavikaList.Rows[0]["MiddleName"].ToString() != "" && dtSavikaList.Rows[0]["MiddleName"].ToString() != null)
                {
                    TxtMidName.Text = dtSavikaList.Rows[0]["MiddleName"].ToString();
                }
                if (dtSavikaList.Rows[0]["Village"].ToString() != "" && dtSavikaList.Rows[0]["Village"].ToString() != null)
                {
                    TxtVillage.Text = dtSavikaList.Rows[0]["Village"].ToString();
                }
                if (dtSavikaList.Rows[0]["PinCode"].ToString() != "" && dtSavikaList.Rows[0]["PinCode"].ToString() != null)
                {
                    TxtPinCode.Text = dtSavikaList.Rows[0]["PinCode"].ToString();
                }
                if (dtSavikaList.Rows[0]["IFSCCode"].ToString() != "" && dtSavikaList.Rows[0]["IFSCCode"].ToString() != null)
                {
                    txtIFSC.Text = dtSavikaList.Rows[0]["IFSCCode"].ToString();
                }

            }
            #endregion
        }

        //protected void BtnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("window.open('Transaction/FrmLICDocUploadRef.aspx','_blank')");
        //}

    }
}