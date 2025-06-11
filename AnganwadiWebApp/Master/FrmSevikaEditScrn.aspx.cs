using AnganwadiLib.Business;
using AnganwadiLib.Data;
using AnganwadiLib.Methods;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Master
{
    public partial class FrmSevikaEditScrn : System.Web.UI.Page
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

        string ErrMsg = "";
        //BoSevikaEdit
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmSevikaEditScrnRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            LblGrdHead.Text = "Master" + " >> " + "Sevika Edit Screen";// Session["LblGrdHead"].ToString();
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
                }
                if (Request.QueryString["@"] == "1")
                {
                    InitialRow();
                }
                if (Request.QueryString["@"] == "2")
                {
                    if (Session["SevikaIdUpdt"] != null)
                    {
                        txtSevikaId.Text = Session["SevikaIdUpdt"].ToString();
                        txtSevikaId.ReadOnly = true;
                        BtnSearchSevika_Click(null, null);
                        BtnSearchSevika.Enabled = false;

                    }
                }
              
            }
        }
        protected void InitialRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SevikaId", typeof(Int64));
            dt.Columns.Add("CompId", typeof(Int64));
            dt.Columns.Add("AadharDoc", typeof(byte[]));
            dt.Columns.Add("DobDoc", typeof(byte[]));
            dt.Columns.Add("DateJoinDoc", typeof(byte[]));

            dt.Columns.Add("AddharDocType", typeof(string));
            dt.Columns.Add("DOBDocType", typeof(string));
            dt.Columns.Add("DOJDocType", typeof(string));

            DataRow drw = dt.NewRow();
            drw["AadharDoc"] = null;
            drw["DobDoc"] = null;
            drw["DateJoinDoc"] = null;

            dt.Rows.Add(drw);
            dt.AcceptChanges();
            ViewState["TblDoc"] = dt;

        }
        protected void LoadGrid()
        {
            String query = "Select  distinct  divname,distname,cdponame,divid, distid,cdpoid, num_sevikamaster_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo,    ";
            query += "   to_char(date_sevikamaster_birthdate,'dd/MM/yyyy') SevikaBOD,(case when var_sevikamaster_active='Y' Then 'Active' Else 'InActive' end) status, var_sevikamaster_address address,var_sevikamaster_remark remark ";
            query += "  ,to_char(date_sevikamaster_joindate,'dd/MM/yyyy') DtJoining ,num_sevikamaster_compid comp_id , ";
            query += " dtnew_sevikaedit_dob sevikaedit_dob, dtnew_sevikaedit_doj sevikaedit_doj,num_sevikaedit_newaadhar sevikaedit_newaadhar from aoup_sevikamaster_def ";
            query += "   inner join aoup_angnwadimst_def  on num_angnwadimst_angnid = num_sevakmaster_anganid inner ";
            query += "   join corpinfo on bitid = num_sevikamaster_compid ";
            query += "   left join aoup_sevikaedit on  num_sevikaedit_sevikaid =num_sevikamaster_sevikaid ";
            query += "  where num_sevikamaster_sevikaid = '" + txtSevikaId.Text.Trim() + "' ";
            //0 is reserved for admin that can view all the records 

            if (Session["CompId"].ToString() != "0")
            {
                query += " and  num_sevikamaster_compid=" + Session["CompId"].ToString();
            }

            DataTable dtSevikaEdit = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);
            //DataTable dtSevikaEdit = null;

            if (dtSevikaEdit != null && dtSevikaEdit.Rows.Count > 0)
            {
                lblSevikaName.Text = dtSevikaEdit.Rows[0]["SevikaName"].ToString();
                DtDobOld.Text = dtSevikaEdit.Rows[0]["SevikaBOD"].ToString();
                DtDobOld.DisableControl();
                lblStatus.Text = dtSevikaEdit.Rows[0]["status"].ToString();
                lblSevikaAadhar.Text = dtSevikaEdit.Rows[0]["AadharNo"].ToString();
                DtDateJoinOld.Text = dtSevikaEdit.Rows[0]["DtJoining"].ToString();
                DtDateJoinOld.DisableControl();

                lblDivName.Text = dtSevikaEdit.Rows[0]["divname"].ToString();
                ViewState["DivId"] = dtSevikaEdit.Rows[0]["divid"].ToString();

                lblDistName.Text = dtSevikaEdit.Rows[0]["distname"].ToString();
                ViewState["DistId"] = dtSevikaEdit.Rows[0]["distid"].ToString();

                lblCdpo.Text = dtSevikaEdit.Rows[0]["cdponame"].ToString();
                ViewState["CdpoId"] = dtSevikaEdit.Rows[0]["cdpoid"].ToString();

                lblAddress.Text = dtSevikaEdit.Rows[0]["address"].ToString();
                ViewState["comp_id"] = dtSevikaEdit.Rows[0]["comp_id"].ToString();
                //sevikaedit_dob,sevikaedit_doj,sevikaedit_newaadhar
                if (dtSevikaEdit.Rows[0]["sevikaedit_dob"].ToString().Trim() != "")
                {
                    DtDobNew.Text = Convert.ToDateTime(dtSevikaEdit.Rows[0]["sevikaedit_dob"]).ToString("dd-MM-yyyy");
                    ChkDobNew.Checked = true;
                }
                if (dtSevikaEdit.Rows[0]["sevikaedit_doj"].ToString().Trim() != "")
                {
                    DtDateOfJoinNew.Text = Convert.ToDateTime(dtSevikaEdit.Rows[0]["sevikaedit_doj"]).ToString("dd-MM-yyyy");
                    chkDtJoinNew.Checked = true;
                }
                if (dtSevikaEdit.Rows[0]["sevikaedit_newaadhar"].ToString().Trim() != "")
                {
                    txtAadharNoNew.Text = Convert.ToString(dtSevikaEdit.Rows[0]["sevikaedit_newaadhar"]);
                    chkAadharNew.Checked = true;
                }

                if (Request.QueryString["@"].Trim() == "2")
                {
                    if (Session["SevikaIdUpdt"] != null)
                    {
                        fill_DocDtls();
                    }
                }
            }
            else
            {
                MessageAlert("Record Not Found!", "");
            }
        }
        public void fill_DocDtls()
        {
            String query = "select num_sevikaedit_id SevikaId , num_sevikaedit_compid CompId , blob_docaadhar_new AadharDoc , blob_docdob_new  DobDoc , ";
            query += " blob_docdoj_new DateJoinDoc, var_docaadhar_type AddharDocType , var_docdob_type DOBDocType, var_docdoj_type DOJDocType ";
            query += " from aoup_sevikaedit inner join aoup_sevikaedit_doctypes on num_sevikaedit_id = num_sevikaedtdoc_editid ";
            query += " where num_sevikaedit_sevikaid =" + Session["SevikaIdUpdt"].ToString() + " and num_sevikaedit_id=" + Session["SevikaEdtIdLst"].ToString();

            DataTable dtDoc = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);
            if (dtDoc != null && dtDoc.Rows.Count > 0)
            {
                ViewState["TblDoc"] = dtDoc;
            }
            else
            {
                InitialRow();
            }
        }

        protected void BtnSearchSevika_Click(object sender, EventArgs e)
        {
            if (txtSevikaId.Text.Trim() != "")
            {
                LoadGrid();
            }

        }

        protected void BtnRest_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["@"].Trim() == "1")
            {
                Response.Redirect("../Master/FrmSevikaEditScrn.aspx?@=1");
            }
            else
            {
                Response.Redirect("../Master/FrmSevikaEditScrn.aspx?@=2");
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDoc = ViewState["TblDoc"] as DataTable;
                dtDoc.Rows[0]["SevikaId"] = txtSevikaId.Text.Trim();
                dtDoc.Rows[0]["CompId"] = ViewState["comp_id"].ToString().Trim();

                #region  "Validation Part "
                if (ChkDobNew.Checked != true && chkAadharNew.Checked != true && chkDtJoinNew.Checked != true)
                {
                    MessageAlert("Select atleast One checkbox!", "");
                    return;
                }
                if (ChkDobNew.Checked == true && chkDtJoinNew.Checked == true)
                {
                    if (DtDobNew.Text == "")
                    {
                        MessageAlert("Select Sevika DOB!", "");
                        DtDobNew.Focus();
                        return;
                    }
                    if (DtDateOfJoinNew.Text == "")
                    {
                        MessageAlert("Select Sevika Joining Date!", "");
                        DtDateOfJoinNew.Focus();
                        return;
                    }
                    if (Convert.ToDateTime(DtDobNew.Value).Date >= Convert.ToDateTime(DtDateOfJoinNew.Value).Date)
                    {
                        MessageAlert("DOB must be less than of Joining Date!", "");
                        DtDobNew.Focus();
                        return;
                    }
                }


                if (ChkDobNew.Checked == true)
                {
                    if (DtDobNew.Text == "")
                    {
                        MessageAlert("Select Sevika DOB!", "");
                        DtDobNew.Focus();
                        return;
                    }

                    if (flUpdDOB.HasFile != true)
                    {
                        MessageAlert("Upload Document for Sevika DOB!", "");
                        flUpdDOB.Focus();
                        return;
                    }
                    else
                    {
                        if (ValidateFileSize(flUpdDOB, "DOB") == false)
                        {
                            MessageAlert(ErrMsg, "");
                            flUpdDOB.Focus();
                            return;
                        }
                    }


                }
                if (chkDtJoinNew.Checked == true)
                {
                    if (DtDateOfJoinNew.Text == "")
                    {
                        MessageAlert("Select Sevika DOB!", "");
                        DtDateOfJoinNew.Focus();
                        return;
                    }
                    if (flUpdDateJoin.HasFile != true)
                    {
                        MessageAlert("Upload Document for Sevika Date of Joining!", "");
                        flUpdDateJoin.Focus();
                        return;
                    }
                    else
                    {
                        if (ValidateFileSize(flUpdDateJoin, "DOJ") == false)
                        {
                            MessageAlert(ErrMsg, "");
                            flUpdDateJoin.Focus();
                            return;
                        }
                    }
                }
                if (chkAadharNew.Checked == true)
                {
                    if (txtAadharNoNew.Text.Trim() == "")
                    {
                        MessageAlert("Enter Aadhar No", "");
                        txtAadharNoNew.Focus();
                        return;
                    }
                    if (txtAadharNoNew.Text != "")
                    {
                        if (txtAadharNoNew.Text.Length != 12)
                        {
                            MessageAlert("Aadhar No should have 12 digits", "");
                            txtAadharNoNew.Focus();
                            return;
                        }
                        else
                        {
                            MstMethods.aadharcard aad = new MstMethods.aadharcard();
                            bool isValidnumber = MstMethods.aadharcard.validateVerhoeff(txtAadharNoNew.Text.Trim());
                            //aadharcard.validateVerhoeff("num");

                            if (isValidnumber != true)
                            {
                                MessageAlert("Aadhar No is Invalid", "");
                                txtAadharNoNew.Focus();
                                return;
                            }

                        }
                    }
                    if (flUpdAadhar.HasFile != true)
                    {
                        MessageAlert("Upload Document for Sevika Aadhar No.!", "");
                        flUpdAadhar.Focus();
                        return;
                    }
                    else
                    {
                        if (ValidateFileSize(flUpdAadhar, "AADHAR") == false)
                        {
                            MessageAlert(ErrMsg, "");
                            flUpdAadhar.Focus();
                            return;
                        }
                    }
                }

                #endregion

                BoSevikaEdit objData = new BoSevikaEdit();
                objData.userid = Session["UserId"].ToString();
                objData.comp_id = Convert.ToInt32(ViewState["comp_id"].ToString());
                objData.sevika_cdpoid = Convert.ToInt64(ViewState["CdpoId"].ToString());
                objData.sevika_distrid = Convert.ToInt64(ViewState["DistId"].ToString());

                objData.sevika_divid = Convert.ToInt64(ViewState["DivId"].ToString());

                objData.sevikaDob_old = Convert.ToDateTime(DtDobOld.Value);
                objData.sevikaDoJ_old = Convert.ToDateTime(DtDateJoinOld.Value);
                objData.sevikaAadharNo_old = Convert.ToInt64(lblSevikaAadhar.Text);

                if (ChkDobNew.Checked == true)
                    objData.sevikaDob_new = Convert.ToDateTime(DtDobNew.Value);
                if (chkDtJoinNew.Checked == true)
                    objData.sevikaDoJ_new = Convert.ToDateTime(DtDateOfJoinNew.Value);
                if (chkAadharNew.Checked == true)
                    objData.sevikaAadharNo_new = Convert.ToInt64(txtAadharNoNew.Text.Trim());

                objData.sevikaid = Convert.ToInt64(txtSevikaId.Text.Trim());
                if (Request.QueryString["@"].Trim() == "2")
                {
                    objData.In_Mode = 2;
                    objData.SevikaEdtId = Convert.ToInt32(Session["SevikaEdtIdLst"]);
                }
                else
                {
                    objData.In_Mode = 1;
                    objData.SevikaEdtId = 0;
                }

                objData.Insert();

                if (objData.ErrorCode == -100 && objData.SevikaEdtId != 0)
                {

                    UploadBlobImg(objData.SevikaEdtId);

                    MessageAlert(objData.ErrorMessage, "../Transaction/FrmSevikaEdtListScrn.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objData.ErrorMessage, "");
                    return;
                }

            }
            catch (Exception Ex)
            {
                MessageAlert("Something Went Wrong!", "");
            }

        }
        public bool ValidateFileSize(FileUpload files, string type)
        {
            Boolean flag = true;
            DataTable dtDoc = ViewState["TblDoc"] as DataTable;
            if (files.PostedFile != null && files.PostedFile.FileName != "")
            {
                string extension = Path.GetExtension(files.PostedFile.FileName).ToUpper();
                //Allow upto 5mb
                if (extension != ".JPEG" && extension != ".PNG" && extension != ".PDF" && extension != ".JPG")
                {
                    ErrMsg = "Document Should Be Accepatable In JPEG/JPG/Png/PDF Format Only";
                    return flag = false;
                }
                if (files.FileContent.Length > 3145728)
                {
                    ErrMsg = "Document Size Should Be <3 mb";
                    return flag = false;
                }
                if (type == "DOB")
                {
                    dtDoc.Rows[0]["DobDoc"] = (byte[])files.FileBytes;
                    dtDoc.Rows[0]["DOBDocType"] = extension;
                    dtDoc.AcceptChanges();
                }
                if (type == "DOJ")
                {
                    dtDoc.Rows[0]["DateJoinDoc"] = (byte[])files.FileBytes;
                    dtDoc.Rows[0]["DOJDocType"] = extension;
                    dtDoc.AcceptChanges();
                }
                if (type == "AADHAR")
                {
                    dtDoc.Rows[0]["AadharDoc"] = (byte[])files.FileBytes;
                    dtDoc.Rows[0]["AddharDocType"] = extension;
                    dtDoc.AcceptChanges();
                }
                ViewState["TblDoc"] = dtDoc;
            }

            return flag;
        }
        public void UploadBlobImg(int SevikaEdit_id)
        {
            //var DobDocByts = dtDoc.AsEnumerable().Where(row => Convert.ToInt32(row["SevikaId"]) == txtSevikaId.Text.Trim()).Select(row => row.Field<byte[]>("DobDoc"));
            DataTable dtDoc = ViewState["TblDoc"] as DataTable;
            DataRow[] dr = dtDoc.Select("SevikaId = " + txtSevikaId.Text.Trim());

            #region update fileextentions
            GetCon con = new GetCon();
            String QryExtension = "update aoup_sevikaedit_doctypes set  var_docdob_type='" + dtDoc.Rows[0]["DOBDocType"].ToString() + "',var_docdoj_type='" + dtDoc.Rows[0]["DOJDocType"].ToString() + "',var_docaadhar_type='" + dtDoc.Rows[0]["AddharDocType"].ToString() + "'  ";
            QryExtension += " where num_sevikaedtdoc_editid = " + SevikaEdit_id + " ";

            OracleCommand Cmd = new OracleCommand(QryExtension, con.connection);
            Cmd.CommandType = CommandType.Text;
            con.OpenConn();
            int ExntCount = Cmd.ExecuteNonQuery();
            con.CloseConn();
            //con.CloseConn();
            if (ExntCount > 0)
            {
                con.OpenConn();
                //GetCon con = new GetCon();
                String Query = "update aoup_sevikaedit set  blob_docdob_new = :BlobDoB ,blob_docdoj_new = :BlobDoJ ,blob_docaadhar_new = :BlobAadhar    where num_sevikaedit_id = " + SevikaEdit_id + " ";

                OracleParameter oraParameter = new OracleParameter("BlobDoB", OracleDbType.Blob);
                oraParameter.Value = (dr[0]["DobDoc"].ToString().Length == 0 ? null : (byte[])dr[0]["DobDoc"]);

                OracleParameter oraParameter1 = new OracleParameter("BlobDoJ", OracleDbType.Blob);
                oraParameter1.Value = (dr[0]["DateJoinDoc"].ToString().Length == 0 ? null : (byte[])dr[0]["DateJoinDoc"]);

                OracleParameter oraParameter2 = new OracleParameter("BlobAadhar", OracleDbType.Blob);
                oraParameter2.Value = (dr[0]["AadharDoc"].ToString().Length == 0 ? null : (byte[])dr[0]["AadharDoc"]);

                OracleCommand Cmd1 = new OracleCommand(Query, con.connection);
                Cmd1.Parameters.Add(oraParameter);
                Cmd1.Parameters.Add(oraParameter1);
                Cmd1.Parameters.Add(oraParameter2);

                //con.OpenConn();
                int a = Cmd1.ExecuteNonQuery();
                con.CloseConn();
            }
            else
            {
                MessageAlert("Unable to Process Documents!", "");
                return;
            }

            #endregion

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Transaction/FrmSevikaEdtListScrn.aspx");
        }
    }
}