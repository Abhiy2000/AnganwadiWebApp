using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using AnganwadiLib.Methods;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmLICApproveMst : System.Web.UI.Page
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
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            if (!IsPostBack)
            {
                ViewState["Mode"] = "";
                LblGrdHead.Text = Session["LblGrdHead"].ToString();

                if (Request.QueryString["@"].ToString() == "2")
                {
                    txtotp.Visible = true;
                    lbl1.Visible = true;
                    lblverify.Visible = true;
                    ViewState["Mode"] = 2;
                }
                else if (Request.QueryString["@"].ToString() == "1")
                {
                    txtotp.Visible = false;
                    lbl1.Visible = false;
                    lblverify.Visible = false;
                    ViewState["Mode"] = 1;
                }
                else
                {
                    MessageAlert("Invalid mode of access", "");
                    return;
                }
                //MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");
                //      ddlAnganID.Focus();
                grdSavikaList_set();

                UserLevel = Session["brcategory"].ToString();


            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //------------------------------------------------OTP--------------------------------------------------- added by suresh on 01/07/2022
            //if (TxtMobNo.Text.Trim() == "")
            //{
            //    MessageAlert("Please enter mobile number before approval", "");
            //    return;
            //}
            //ObjLICStatus.UserID = Session["UserId"].ToString();
            //ObjLICStatus.Mobile = Convert.ToInt64(TxtMobNo.Text);
            //if (ViewState["Mode"].ToString() == "1")
            //{
            //    ObjLICStatus.mode = 1;
            //    ObjLICStatus.otp = null;
            //}
            //else if (ViewState["Mode"].ToString() == "2")
            //{
            //    if (txtotp.Text.Trim() == "")
            //    {
            //        MessageAlert("Please Enter OTP", "");
            //        return;
            //    }
            //    ObjLICStatus.mode = 2;
            //    ObjLICStatus.otp = txtotp.Text.Trim();
            //}
            //else
            //{
            //    MessageAlert("Invalid mode access", "");
            //    return;
            //}
            //ObjLICStatus.ApproveOTP();
            //if (ObjLICStatus.ErrorCode == -100)
            //{
            //    if (ViewState["Mode"].ToString() == "2")
            //    {
            //        CallProc();
            //        MessageAlert(ObjLICStatus.ErrorMessage, "../Transaction/FrmLICApprovalList.aspx"); //"../HomePage/FrmDashboard.aspx"
            //        return;
            //    }
            //    else
            //    {
            //        MessageAlert(ObjLICStatus.ErrorMessage, "../Transaction/FrmLICApproveMst.aspx?@=2"); //"../HomePage/FrmDashboard.aspx"
            //        return;
            //    }

            //}
            //else
            //{
            //    MessageAlert(ObjLICStatus.ErrorMessage, "");
            //    return;
            //}
            //---------------------------------------------------------------------------------------------------
            CallProc(); //existing 
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Transaction/FrmLICSevikaList.aspx");
        }


        public void CallProc()
        {
            string ApproveDoc = string.Empty;
            string RejectDoc = string.Empty;
            bool isValid = false;

            if (RdoStatus.SelectedItem.Value == "A")
            {
                isValid = true;
                ApproveDoc += Session["SevikaId"] + "$" + TxtReason.Text;
            }
            else if (RdoStatus.SelectedItem.Value == "R")
            {
                isValid = true;
                if (TxtReason.Text == "")
                {
                    MessageAlert("Please Fill Reject Reason", "");
                    TxtReason.Focus();
                    return;
                }
                else
                {
                    RejectDoc += Session["SevikaId"] + "$" + TxtReason.Text;
                }
            }

            if (isValid == false)
            {
                MessageAlert("Please choose Approve OR Reject", "");
                return;
            }

            ObjLICStatus.UserID = Session["UserId"].ToString();
            ObjLICStatus.SevikaID = Convert.ToInt32(Session["SevikaId"]);
            ObjLICStatus.Apprvstr = ApproveDoc;
            ObjLICStatus.Rejctstr = RejectDoc;
            ObjLICStatus.UpDateLICApproval();

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



        private void grdSavikaList_set()
        {
            #region "Load Grid"

            String query = "";
            String Flag = Session["Flag"].ToString();

            ////if (Flag == "I")
            ////{
            ////    query += " Select a.num_licsevika_sevikaid SevikaID,var_angnwadimst_angnname AnganwadiID, a.var_licsevika_name SevikaName, a.date_licsevika_birthdate Birthdate,a.var_licsevika_address Address,  ";
            ////    query += " a.num_licsevika_mobileno MobileNo, a.var_licsevika_phoneno PhoneNo,var_bank_bankname BankID, b.num_bankbranch_branchid BranchID,  b.var_bankbranch_ifsccode IFSCCode,   ";
            ////    query += " a.var_licsevika_panno PAN, a.var_licsevika_aadharno AadharNo,b.var_bankbranch_branchname branchnm, a.num_licsevika_branchid SeviBranchId,a.var_licsevika_desigcode DesigID,   ";
            ////    query += " a.var_licsevika_accno AccNo, a.var_licsevika_remark SeviRemark,a.var_licsevika_middlename MiddleName, a.var_licsevika_village Village, a.var_licsevika_pincode PinCode ";
            ////    query += " FROM aoup_LICsevika_def a  left join aoup_bankbranch_def b on a.num_licsevika_branchid=b.num_bankbranch_branchid ";
            ////    query += " left join aoup_bank_def g on b.num_bankbranch_bankid=g.num_bank_bankid ";
            ////    query += " inner join aoup_AngnwadiMst_def m on m.num_angnwadimst_angnid=num_licsevika_anganid ";
            ////    query += " where "; //num_licsevika_compid='" + Session["GrdLevel"].ToString() + "' and 
            ////    query += " num_licsevika_sevikaid='" + Session["SevikaId"] + "'";
            ////}
            ////else if (Flag == "E")
            ////{
            if (Flag == "I")
            {
                query += " select  num_lic_sevikaid SevikaID,var_angnwadimst_angnname AnganwadiID,var_sevikamaster_name SevikaName,date_sevikamaster_birthdate Birthdate,var_sevikamaster_address Address,  ";
                query += " num_sevikamaster_mobileno MobileNo,var_sevikamaster_phoneno PhoneNo,var_bank_bankname BankID,num_bankbranch_branchid BranchID,var_bankbranch_ifsccode IFSCCode,   ";
                query += " var_sevikamaster_panno PAN,var_sevikamaster_aadharno AadharNo,b.var_bankbranch_branchname branchnm,num_sevikamaster_branchid SeviBranchId,num_sevikamaster_desigid DesigID,   ";
                query += " var_sevikamaster_accno AccNo,var_sevikamaster_remark SeviRemark,var_sevikamaster_middlename MiddleName,var_sevikamaster_village Village,var_sevikamaster_pincode PinCode ";
                query += " FROM aoup_lic_def a  inner join aoup_sevikamaster_def s on num_sevikamaster_sevikaid=num_lic_sevikaid ";
                query += " left join aoup_bankbranch_def b on s.num_sevikamaster_branchid=b.num_bankbranch_branchid   ";
                query += " left join aoup_bank_def g on b.num_bankbranch_bankid=g.num_bank_bankid ";
                query += " inner join aoup_AngnwadiMst_def m on m.num_angnwadimst_angnid=num_sevakmaster_anganid ";
                query += " where "; //num_licsevika_compid='" + Session["GrdLevel"].ToString() + "' and 
                query += " num_lic_sevikaid='" + Session["SevikaId"] + "'";
            }
            else if (Flag == "E")
            {
                query += " SELECT num_lic_sevikaid sevikaid, var_angnwadimst_angnname anganwadiid, var_licsevika_name sevikaname, date_licsevika_birthdate birthdate, ";
                query += " var_licsevika_address address, num_licsevika_mobileno mobileno,var_licsevika_phoneno phoneno, var_bank_bankname bankid, num_bankbranch_branchid branchid,  ";
                query += " var_bankbranch_ifsccode ifsccode,var_licsevika_panno pan, var_licsevika_aadharno aadharno,b.var_bankbranch_branchname branchnm, num_licsevika_branchid sevibranchid, ";
                query += " var_licsevika_desigcode desigid, var_licsevika_accno accno, var_licsevika_remark seviremark,var_licsevika_middlename middlename, var_licsevika_village village,  ";
                query += " var_licsevika_pincode pincode  FROM aoup_lic_def a ";
                query += " INNER JOIN aoup_licsevika_def s ON num_licsevika_sevikaid = num_lic_sevikaid LEFT JOIN aoup_bankbranch_def b ON s.num_licsevika_branchid = b.num_bankbranch_branchid ";
                query += " LEFT JOIN aoup_bank_def g ON b.num_bankbranch_bankid = g.num_bank_bankid INNER JOIN aoup_angnwadimst_def m ON m.num_angnwadimst_angnid = num_licsevika_anganid ";
                query += " where num_lic_sevikaid='" + Session["SevikaId"] + "'";
            }
            //}

            DataTable dtSavikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtSavikaList.Rows.Count > 0)
            {
                if (dtSavikaList.Rows[0]["SevikaID"].ToString() != "" && dtSavikaList.Rows[0]["SevikaID"].ToString() != null)
                {
                    txtSevikaId.Text = dtSavikaList.Rows[0]["SevikaID"].ToString();
                }
                if (dtSavikaList.Rows[0]["AnganwadiID"].ToString() != "" && dtSavikaList.Rows[0]["AnganwadiID"].ToString() != null)
                {
                    txtAnganwadi.Text = dtSavikaList.Rows[0]["AnganwadiID"].ToString();
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
                    if (dtSavikaList.Rows[0]["DesigID"].ToString() == "M")
                    { rdbActive.SelectedValue = "H"; }
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

                //if (dtSavikaList.Rows[0]["DocImage"].ToString() != "" && dtSavikaList.Rows[0]["DocImage"].ToString() != null)
                //{
                //    Image ImgDoc = (Image)dtSavikaList.Rows[0].FindControl("ImgDoc");
                //    if (dtSavikaList.Rows[0]["DocImage"] != DBNull.Value)
                //    {
                //        Byte[] PropimageBytes = (Byte[])dtSavikaList.Rows[0]["DocImage"];
                //        ImgDoc.ImageUrl = "data:image;base64," + Convert.ToBase64String(PropimageBytes);
                //    }
                //}

            }
            #endregion
        }

        public void lnkDownload_Click(object sender, EventArgs e)
        {
            string Query = " Select img_lic_document DocImage from aoup_LIC_DEF where num_lic_sevikaid='" + Session["SevikaId"] + "' ";

            DataTable TblFile = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
            if (TblFile.Rows.Count > 0)
            {
                Byte[] DocImage = new Byte[0];
                if (TblFile.Rows[0]["DocImage"] != DBNull.Value)
                {
                    DocImage = (Byte[])(TblFile.Rows[0]["DocImage"]);
                    string base64String = Convert.ToBase64String(DocImage);
                    //Response.Clear();
                    //MemoryStream ms = new MemoryStream(DocImage);
                    //Response.ContentType = "Image/jpg";
                    //Response.AddHeader("content-disposition", "attachment;filename=Image_" + DateTime.Now.ToString("dd-MM-yy hh mm ss") + ".jpg");
                    //Response.Buffer = true;
                    //ms.WriteTo(Response.OutputStream);
                    //Response.End();


                    Response.Clear();
                    MemoryStream ms = new MemoryStream(DocImage);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Doc_" + System.DateTime.Now.ToString("ddmmyyyyhhMMss") + ".pdf");
                    Response.Buffer = true;
                    ms.WriteTo(Response.OutputStream);
                    Response.End();

                }
            }
        }
    }
}