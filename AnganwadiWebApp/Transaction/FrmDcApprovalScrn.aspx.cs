using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;
using AnganwadiLib.Methods;
using System.IO;
using AnganwadiLib.Data;
using Oracle.DataAccess.Client;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmDcApprovalScrn : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            //LblGrdHead.Text = Session["LblGrdHead"].ToString(); // "Sevika LIC Document Upload"
            if (!IsPostBack)
            {
                // LoadGrid();
                Session["GrdLevel"].ToString();
                PnlSerch.Visible = false;
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

                    //PnlSerch.Visible = false;
                    //BtnSubmit.Visible = false;
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

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;

                    //PnlSerch.Visible = false;
                    //BtnSubmit.Visible = false;
                }
            }
            /*  if (IsPostBack)
              {
                  #region "Assign posted file data to table"
                  if (IsPostBack && GridCdpoScrn.Rows.Count > 0)
                  {

                      for (int i = 0; i < GridCdpoScrn.Rows.Count; i++)
                      {
                          FileUpload UploadedFileName = GridCdpoScrn.Rows[i].FindControl("flupldDCdoc") as FileUpload;
                          RadioButtonList RdbList = GridCdpoScrn.Rows[i].FindControl("RdbStatus") as RadioButtonList;

                          if (UploadedFileName.PostedFile != null && UploadedFileName.PostedFile.FileName != "")
                          {
                              if (RdbList.SelectedValue.Trim() != "")
                              {
                                  string extension = Path.GetExtension(UploadedFileName.PostedFile.FileName).ToUpper();
                                  if (extension != ".JPEG" && extension != ".PNG" && extension != ".PDF" && extension != ".JPG")
                                  {
                                      ErrMsg = "Document Should Be Accepatable In JPEG/JPG/Png/PDF Format Only";
                                      return;
                                  }
                                  //Allow upto 3mb
                                  if (UploadedFileName.FileContent.Length > 3145728)
                                  {
                                      MessageAlert("Document Size Should Be <3 mb", "");
                                      return;
                                  }


                                  SetUploadedFiles(GridCdpoScrn);
                                  break;
                              }
                              else
                              {
                                  MessageAlert("Select Atleast one Record", "");
                                  return;
                              }
                          }

                      }
                  }
                  #endregion

              }*/
        }
        public void SetUploadedFiles(GridView Grv)
        {
            DataTable dtTempDoc = (DataTable)ViewState["tblDcDocs"];
            //DataRow dr = null;
            if (dtTempDoc != null && Grv.Rows.Count > 0)
            {
                for (int rowindex = 0; rowindex < Grv.Rows.Count; rowindex++)
                {
                    string SevikaEditId = Grv.Rows[rowindex].Cells[0].Text.Trim();
                    TextBox txtRemark = Grv.Rows[rowindex].FindControl("txtRemark") as TextBox;
                    Label flName = Grv.Rows[rowindex].FindControl("lblFileName") as Label;

                    RadioButtonList RdStatus = Grv.Rows[rowindex].FindControl("RdbStatus") as RadioButtonList;

                    FileUpload UplFileName = Grv.Rows[rowindex].FindControl("flupldDCdoc") as FileUpload;

                    if (UplFileName.PostedFile != null && UplFileName.PostedFile.FileName != "" && flName.Text.Trim() == "")
                    {
                        string extension = Path.GetExtension(UplFileName.PostedFile.FileName).ToUpper();
                        byte[] filebytes = UplFileName.FileBytes;

                        dtTempDoc.Rows[rowindex]["DpoDoc"] = filebytes;
                        dtTempDoc.Rows[rowindex]["FileName"] = UplFileName.PostedFile.FileName.ToString();
                    }
                    dtTempDoc.Rows[rowindex]["SevikaEditId"] = SevikaEditId;
                    dtTempDoc.Rows[rowindex]["DpoStatus"] = RdStatus.SelectedValue;
                    dtTempDoc.Rows[rowindex]["DCRemark"] = txtRemark.Text;


                }

                ViewState["tblDcDocs"] = dtTempDoc;

                SetPreviousDataDocGrd();
            }
        }
        private void SetPreviousDataDocGrd()
        {

            if (ViewState["tblDcDocs"] != null)
            {
                DataTable dt = (DataTable)ViewState["tblDcDocs"];
                if (dt.Rows.Count > 0 && GridCdpoScrn.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        for (int i = 0; i < GridCdpoScrn.Rows.Count; i++)
                        {
                            string EditId = GridCdpoScrn.Rows[i].Cells[0].Text.Trim();

                            if (EditId == dt.Rows[j]["SevikaEditId"].ToString())
                            {
                                Label flName = GridCdpoScrn.Rows[i].FindControl("lblFileName") as Label;

                                TextBox txtRemark = GridCdpoScrn.Rows[i].FindControl("txtRemark") as TextBox;
                                RadioButtonList RdStatus = GridCdpoScrn.Rows[i].FindControl("RdbStatus") as RadioButtonList;

                                txtRemark.Text = dt.Rows[j]["DCRemark"].ToString();
                                RdStatus.SelectedValue = dt.Rows[j]["DpoStatus"].ToString().Trim();
                                flName.Text = dt.Rows[j]["FileName"].ToString().Trim();
                                break;
                            }
                        }
                    }

                }
            }
        }
        protected void InitialRow()
        {
            ViewState["tblDcDocs"] = null;

            DataTable dt = new DataTable();
            dt.Columns.Add("SevikaEditId", typeof(Int64));
            dt.Columns.Add("DCStatus", typeof(string));
            dt.Columns.Add("DCRemark", typeof(string));
            dt.Columns.Add("DCDoc", typeof(byte[]));
            dt.Columns.Add("FileType", typeof(string));

            //DataRow drnew = null;
            //for (int i = 0; i < Rows; i++)
            //{
            //    drnew = dt.NewRow();
            //    dt.Rows.Add(drnew);
            //}
            dt.AcceptChanges();
            ViewState["tblDcDocs"] = dt;

        }
        protected void LoadGrid()
        {


            String query = "select  num_sevikaedit_id sevikaedit_id, divid, distid, cdpoid, ";
            query += " num_sevikamaster_sevikaid sevikaid, var_sevikamaster_name sevikaname, ";
            query += " to_char(dtold_sevikaedit_dob,'dd/MM/yyyy') old_dob, to_char(dtold_sevikaedit_doj,'dd/MM/yyyy')     old_joindate,  ";
            query += " num_sevikaedit_oldaadhar old_aadharno, to_char(dtnew_sevikaedit_dob ,'dd/MM/yyyy')   new_dob,  ";
            query += " to_char(dtnew_sevikaedit_doj,'dd/MM/yyyy') new_joindate, num_sevikaedit_newaadhar  new_aadharno,  ";
            query += " VAR_DC_APPRSTATUS dc_status,  VAR_DC_APPREMARK  dc_remark,  ";
            query += " (case when nvl(dbms_lob.getlength(BLOB_DOCDOB_NEW),0) =0 then 'N' else 'Y' end  ) doc_dob, ";
            query += " (case when nvl(dbms_lob.getlength(BLOB_DOCDOJ_NEW), 0) = 0 then 'N' else 'Y' end) doc_doj, ";
            query += " (case when nvl(dbms_lob.getlength(BLOB_DOCAADHAR_NEW), 0) = 0 then 'N' else 'Y' end) doc_Aadhar, ";
            query += " (case when nvl(dbms_lob.getlength(BLOB_DC_DOC), 0) = 0 then 'N' else 'Y' end) doc_dpo, ";
            query += " num_sevikamaster_compid comp_id,var_docdob_type dob_type,var_docdoj_type doj_type,var_docaadhar_type aadhar_type ,var_dcdoc_type dc_doctype  from aoup_sevikaedit  inner join  aoup_sevikamaster_def ";
            query += " on num_sevikaedit_sevikaid = num_sevikamaster_sevikaid  ";
            query += " inner join corpinfo on bitid = num_sevikaedit_compid and cdpoid = num_sevikaedit_cdpoid  ";
            query += " inner join aoup_sevikaedit_doctypes on  num_sevikaedtdoc_editid = num_sevikaedit_id ";

            query += " where var_sevikaedit_dpostatus='A'  and VAR_DC_APPRSTATUS IS NULL  ";
          //  query += " and(dtnew_sevikaedit_dob is not null or dtnew_sevikaedit_doj is not null) and  num_sevikaedit_cdpoid = " + ddlCDPO.SelectedValue + " and num_sevikaedit_distrid = " + ddlDistrict.SelectedValue + " ";
            query += " and num_sevikaedit_cdpoid = " + ddlCDPO.SelectedValue + " and num_sevikaedit_distrid = " + ddlDistrict.SelectedValue + " ";
         
            DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(query);

            ViewState["tblDcDtls"] = null;
            if (TblGetBrid != null && TblGetBrid.Rows.Count > 0)
            {
                GridCdpoScrn.DataSource = TblGetBrid;
                GridCdpoScrn.DataBind();
                PnlSerch.Visible = true;
                BtnSubmit.Visible = true;


                ViewState["tblDcDtls"] = TblGetBrid;

                InitialRow();

                for (int i = 0; i < TblGetBrid.Rows.Count; i++)
                {

                    for (int j = 0; j < GridCdpoScrn.Rows.Count; j++)
                    {
                        string SevikaEdtID = GridCdpoScrn.Rows[j].Cells[0].Text.ToString();
                        LinkButton lnkDoB = (GridCdpoScrn.Rows[j].FindControl("lnkDownloadDob") as LinkButton);
                        LinkButton lnkDoJ = (GridCdpoScrn.Rows[j].FindControl("lnkDownloadDoj") as LinkButton);
                        LinkButton lnkAadhar = (GridCdpoScrn.Rows[j].FindControl("lnkDownloadAadhar") as LinkButton);

                        RadioButtonList RdbStatus = (GridCdpoScrn.Rows[j].FindControl("RdbStatus") as RadioButtonList);
                        if (SevikaEdtID.Trim() == TblGetBrid.Rows[i]["sevikaedit_id"].ToString().Trim())
                        {
                            if (TblGetBrid.Rows[i]["dc_status"] != null && TblGetBrid.Rows[i]["dc_status"].ToString().Trim() != "")
                                RdbStatus.SelectedValue = TblGetBrid.Rows[i]["dc_status"].ToString().Trim();
                            if (TblGetBrid.Rows[i]["doc_dob"].ToString().Trim() == "Y")
                                lnkDoB.Text = "Download";
                            if (TblGetBrid.Rows[i]["doc_doj"].ToString().Trim() == "Y")
                                lnkDoJ.Text = "Download";
                            if (TblGetBrid.Rows[i]["doc_Aadhar"].ToString().Trim() == "Y")
                                lnkAadhar.Text = "Download";

                            break;
                        }

                    }

                }
            }
            else
            {
                PnlSerch.Visible = false;
                BtnSubmit.Visible = false;
                MessageAlert("No data found", "");
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable TmpDpo = ViewState["tblDcDocs"] as DataTable;
            TmpDpo.Rows.Clear();
            TmpDpo.AcceptChanges();
            ViewState["tblDcDocs"] = TmpDpo;


            string Strrecords = string.Empty;
            foreach (GridViewRow row in GridCdpoScrn.Rows)
            {
                RadioButtonList RdbBtns = (RadioButtonList)row.FindControl("RdbStatus");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                string SevikaEdtId = row.Cells[0].Text.Trim();

                if (RdbBtns.SelectedValue != "")
                {
                    if (txtRemark.Text.Trim() == "")
                    {
                        MessageAlert("Please Enter Remark", "");
                        txtRemark.Focus();
                        return;
                    }
                    FileUpload flUpdDpo = (FileUpload)row.FindControl("flupldDCdoc");


                    if (RdbBtns.SelectedValue.Trim() == "A")
                    {
                        if (flUpdDpo.HasFile != true)
                        {
                            MessageAlert("Upload Document!", "");
                            flUpdDpo.Focus();
                            return;
                        }
                        if (flUpdDpo.HasFile == true)
                        {
                            if (ValidateFileSize(flUpdDpo, SevikaEdtId) == false)
                            {
                                TmpDpo.Rows.Clear();
                                TmpDpo.AcceptChanges();
                                ViewState["tblDcDocs"] = TmpDpo;
                                MessageAlert(ErrMsg, "");
                                flUpdDpo.Focus();
                                return;
                            }
                        }
                    }
                    else  // for Reject flag doc upload is optional 
                    {

                        if (flUpdDpo.HasFile == true || flUpdDpo.HasFile == false)
                        {
                            if (ValidateFileSize(flUpdDpo, SevikaEdtId) == false)
                            {
                                TmpDpo.Rows.Clear();
                                TmpDpo.AcceptChanges();
                                ViewState["tblDcDocs"] = TmpDpo;
                                MessageAlert(ErrMsg, "");
                                flUpdDpo.Focus();
                                return;
                            }
                        }
                    }

                    Strrecords += SevikaEdtId + "$" + RdbBtns.SelectedValue + "$" + txtRemark.Text + "#";

                }

            }

            if (Strrecords.Length > 0 && Strrecords.Trim() != "")
            {
                Strrecords = Strrecords.Substring(0, Strrecords.Length - 1);
            }
            else
            {
                MessageAlert("Select Atleast One Record!", "");
                return;
            }

            BoSevikaEdit objData = new BoSevikaEdit();
            objData.userid = Session["UserId"].ToString();
            objData.FlagMode = "DC";
            objData.StrCDPO = Strrecords;

            //this is comman insert method for pages
            // 1: .Transaction/FrmDpoDcmst.aspx
            // 2: .Transaction/FrmCDPOmodifyScreen.aspx

              objData.InsertCdpo();

            if (objData.ErrorCode == -100)
            {
                UploadBlobImg();
                MessageAlert(objData.ErrorMessage, "../Transaction/FrmDcApprovalScrn.aspx");
                return;
            }
            else
            {
                MessageAlert(objData.ErrorMessage, "");
                return;
            }

        }
        public bool ValidateFileSize(FileUpload files, string SevikaEdtId)
        {
            Boolean flag = true;
            int TblIndexNo = 0;
            string extension = "";
            DataTable dtDoc = ViewState["tblDcDocs"] as DataTable;
            if (dtDoc == null)
            {
                DataRow drnew = dtDoc.NewRow();
                dtDoc.Rows.Add(drnew);
            }
            else
            {
                DataRow drnew = dtDoc.NewRow();
                dtDoc.Rows.Add(drnew);
                TblIndexNo = (dtDoc.Rows.Count - 1);
            }

            if (files.PostedFile != null && files.PostedFile.FileName != "")
            {
                extension = Path.GetExtension(files.PostedFile.FileName).ToUpper();
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

                dtDoc.Rows[TblIndexNo]["DCDoc"] = (byte[])files.FileBytes;
                dtDoc.Rows[TblIndexNo]["FileType"] = extension;
                dtDoc.Rows[TblIndexNo]["SevikaEditId"] = SevikaEdtId;
                dtDoc.AcceptChanges();

                ViewState["tblDcDocs"] = dtDoc;
            }
            else
            {
                //dtDoc.Rows[TblIndexNo]["DpoDoc"] = "";
                dtDoc.Rows[TblIndexNo]["FileType"] = "";
                dtDoc.Rows[TblIndexNo]["SevikaEditId"] = SevikaEdtId;
            }

            return flag;
        }

        public void UploadBlobImg()
        {

            #region update file extentions
            DataTable dtDoc = ViewState["tblDcDocs"] as DataTable;

            for (int i = 0; i < dtDoc.Rows.Count; i++)
            {
                // taking project id 
                string qry = "select nvl(num_sevikaedit_projid,0) prjid from aoup_sevikaedit  where num_sevikaedit_id=" + dtDoc.Rows[i]["SevikaEditId"].ToString() + " ";
                DataTable tblProjectId = (DataTable)MstMethods.Query2DataTable.GetResult(qry);


                // end here

                GetCon con = new GetCon();
                string fileType = dtDoc.Rows[i]["FileType"].ToString().Trim();

           
                String QryExtension = "update aoup_sevikaedit_doctypes set  var_DCdoc_type='" + fileType + "'  ";
                QryExtension += " where num_sevikaedtdoc_editid = " + dtDoc.Rows[i]["SevikaEditId"].ToString() + " ";


                OracleCommand Cmd = new OracleCommand(QryExtension, con.connection);
                Cmd.CommandType = CommandType.Text;
                con.OpenConn();
                int ExntCount = Cmd.ExecuteNonQuery();
                con.CloseConn();

                if (tblProjectId.Rows[0]["prjid"].ToString() == "1")
                {
                    String QryDpoDoc = "update aoup_sevikaedit_doctypes set  var_dpodoc_type='" + fileType + "'  ";
                    QryDpoDoc += " where num_sevikaedtdoc_editid = " + dtDoc.Rows[i]["SevikaEditId"].ToString() + " ";

                    OracleCommand Cmddpo = new OracleCommand(QryDpoDoc, con.connection);
                    Cmddpo.CommandType = CommandType.Text;
                    con.OpenConn();
                    int ExntCountdpo = Cmddpo.ExecuteNonQuery();
                    con.CloseConn();
                }


                if (ExntCount > 0)
                {
                    con.OpenConn();
                    //GetCon con = new GetCon();
                    String Query = "update aoup_sevikaedit set  BLOB_DC_DOC = :BlobDpoDocuments     where var_sevikaedit_dpostatus='A' and num_sevikaedit_id = " + dtDoc.Rows[i]["SevikaEditId"].ToString() + " ";

                    OracleParameter oraParameter = new OracleParameter("BlobDpoDocuments", OracleDbType.Blob);
                    oraParameter.Value = (dtDoc.Rows[i]["DCDoc"].ToString().Length == 0 ? null : (byte[])dtDoc.Rows[i]["DCDoc"]);

                    OracleCommand Cmd1 = new OracleCommand(Query, con.connection);
                    Cmd1.Parameters.Add(oraParameter);

                    //con.OpenConn();
                    int a = Cmd1.ExecuteNonQuery();
                    con.CloseConn();
                    if (a>0)
                    {
                        con.OpenConn();
                        //GetCon con = new GetCon();
                        String QueryDpoDocument = "update aoup_sevikaedit set  blob_sevikaedit_dpodoc = :BlobDpoDocuments     where var_sevikaedit_cdpostatus='A' and num_sevikaedit_id = " + dtDoc.Rows[i]["SevikaEditId"].ToString() + " ";

                        OracleParameter oraParameterDpoDoc = new OracleParameter("BlobDpoDocuments", OracleDbType.Blob);
                        oraParameterDpoDoc.Value = (dtDoc.Rows[i]["DCDoc"].ToString().Length == 0 ? null : (byte[])dtDoc.Rows[i]["DCDoc"]);

                        OracleCommand CmdDpodoc = new OracleCommand(QueryDpoDocument, con.connection);
                        CmdDpodoc.Parameters.Add(oraParameterDpoDoc);

                        //con.OpenConn();
                        int cntDpodoc = CmdDpodoc.ExecuteNonQuery();
                        con.CloseConn();
                    }
                }
                else
                {
                    MessageAlert("Unable to Process Documents!", "");
                    return;
                }
            }


            #endregion
        }
        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

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

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;
            BtnSubmit.Visible = false;
            if (ddlDistrict.SelectedValue == null || ddlDistrict.SelectedValue == "")
            {
                MessageAlert("Select District!", "");
                return;
            }
            if (ddlCDPO.SelectedValue.ToString() != "" && ddlCDPO.SelectedValue != null)
            {
                LoadGrid();
            }
        }

        //Download and view code resides here 
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                string SeviEditId = GridCdpoScrn.Rows[rindex].Cells[0].Text.ToString();
                LinkButton lnkDownloads = ((LinkButton)(sender));
                string CmdType = lnkDownloads.CommandArgument;

                if (SeviEditId.Trim() != "0")
                {
                    DownloadFile(Convert.ToInt32(SeviEditId), CmdType.ToUpper());
                }
            }
            catch (Exception ex)
            {
                MessageAlert("Unable To Download File!", "");
            }
        }
        public void DownloadFile(int SevikaEdtID, string DownloadType)
        {
            string Query = "";
            if (DownloadType == "BIRTHDATE")
            {
                Query += " select BLOB_DOCDOB_NEW  DocImage,var_docdob_type Filetype  from aoup_sevikaedit inner join aoup_sevikaedit_doctypes on  num_sevikaedtdoc_editid =num_sevikaedit_id where num_sevikaedit_id=" + SevikaEdtID;
            }
            if (DownloadType == "JOINDATE")
            {
                Query += " select BLOB_DOCDOJ_NEW  DocImage ,var_docdoj_type Filetype  from aoup_sevikaedit inner join aoup_sevikaedit_doctypes on  num_sevikaedtdoc_editid =num_sevikaedit_id where num_sevikaedit_id=" + SevikaEdtID;
            }
            if (DownloadType == "AADHAR")
            {
                Query += " select BLOB_DOCAADHAR_NEW  DocImage ,var_docaadhar_type Filetype  from aoup_sevikaedit inner join aoup_sevikaedit_doctypes on  num_sevikaedtdoc_editid =num_sevikaedit_id where num_sevikaedit_id=" + SevikaEdtID;
            }

            DataTable TblFile = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
            if (TblFile.Rows.Count > 0)
            {
                Byte[] DocImage = new Byte[0];
                if (TblFile.Rows[0]["DocImage"] != DBNull.Value)
                {
                    DocImage = (Byte[])(TblFile.Rows[0]["DocImage"]);

                    // string base64String = Convert.ToBase64String(DocImage);
                    string ExtensionType = "", StrContype = "";
                    if (TblFile.Rows[0]["Filetype"].ToString().ToUpper().Contains(".PDF") == true)
                    {
                        ExtensionType = TblFile.Rows[0]["Filetype"].ToString();
                        StrContype = "application/pdf";
                    }
                    else
                    {
                        StrContype = "image/jpeg";
                        ExtensionType = TblFile.Rows[0]["Filetype"].ToString();
                    }
                    Response.Clear();
                    MemoryStream ms = new MemoryStream(DocImage);
                    Response.ContentType = StrContype;
                    Response.AddHeader("content-disposition", "attachment;filename=Doc_" + DownloadType + System.DateTime.Now.ToString("ddmmyyyyhhMMss") + ExtensionType);
                    Response.Buffer = true;
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                }
            }
        }

        //protected void lnkBtnViewDoc_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dtTbl = ViewState["CurrentTable"] as DataTable;

        //        if (ViewState["CurrentTable"] == null)
        //        {
        //            MessageAlert("Invalid Document Data", "");
        //            return;
        //        }
        //        else
        //        {
        //            int RowIndex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
        //            Label lblSevikaID = (Label)GridCdpoScrn.Rows[RowIndex].FindControl("lblSevikaID");
        //            string Contidion = "sevikaid='" + lblSevikaID.Text + "'";

        //            DataRow objdatarow = dtTbl.Select(Contidion).FirstOrDefault();

        //            if (objdatarow != null)
        //            {
        //                byte[] bytes = (byte[])objdatarow["DocImage"];
        //                String fileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

        //                String ExportPath = Server.MapPath("~\\ImageGarbage\\") + fileName;
        //                string ExportPath1 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/") + "ImageGarbage/" + fileName;

        //                WriteToFile(ExportPath, ref bytes);

        //                string str1 = "window.open('" + ExportPath1 + "', '', 'fullscreen=No, toolbars=no, menubar=no, location=no, scrollbars=yes, resizable=no, status=yes, width=1200px, height=700px');";
        //                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", str1, true);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageAlert("Something Went Wrong: " + ex.Message, "");
        //        return;
        //    }
        //}

        protected void WriteToFile(string GetFilePath, ref byte[] ImageBuffer)
        {
            String FilePath = GetFilePath;
            int counter = 0;
            String FilePathValue = "", TempFilePathValue = "";

            for (int i = 1; i < FilePath.Length; i++)
            {
                if (counter == 0 || counter == 1)
                {
                    TempFilePathValue = FilePath.Substring(FilePath.Length - i, 1) + TempFilePathValue;
                    if (counter == 0)
                    {
                        FilePathValue = FilePath.Substring(FilePath.Length - i, 1) + FilePathValue;
                    }
                }
                if (FilePath.Substring(FilePath.Length - i, 1) == "\\")
                {
                    counter = counter + 1;
                }
            }

            String ReplaceFilePath = FilePath.Replace(TempFilePathValue, "\\ImageGarbage");

            FileStream newFile = new FileStream(ReplaceFilePath + FilePathValue, FileMode.Create);
            ViewState["strPath"] = FilePathValue.TrimStart('\\');
            ViewState["ImagePath"] = ReplaceFilePath + FilePathValue;
            newFile.Write(ImageBuffer, 0, ImageBuffer.Length);
            newFile.Close();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/tablegraph.aspx");
        }

        protected void GridCdpoScrn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCdpoScrn.PageIndex = e.NewPageIndex;
            DataTable dtDetails = ViewState["tblDcDtls"] as DataTable;
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                GridCdpoScrn.DataSource = dtDetails;
                GridCdpoScrn.DataBind();

                for (int i = 0; i < dtDetails.Rows.Count; i++)
                {
                    for (int j = 0; j < GridCdpoScrn.Rows.Count; j++)
                    {
                        string SevikaEdtID = GridCdpoScrn.Rows[j].Cells[0].Text.ToString();
                        LinkButton lnkDoB = (GridCdpoScrn.Rows[j].FindControl("lnkDownloadDob") as LinkButton);
                        LinkButton lnkDoJ = (GridCdpoScrn.Rows[j].FindControl("lnkDownloadDoj") as LinkButton);
                        LinkButton lnkAadhar = (GridCdpoScrn.Rows[j].FindControl("lnkDownloadAadhar") as LinkButton);

                        RadioButtonList RdbStatus = (GridCdpoScrn.Rows[j].FindControl("RdbStatus") as RadioButtonList);
                        if (SevikaEdtID.Trim() == dtDetails.Rows[i]["sevikaedit_id"].ToString().Trim())
                        {
                            if (dtDetails.Rows[i]["dc_status"] != null && dtDetails.Rows[i]["dc_status"].ToString().Trim() != "")
                                RdbStatus.SelectedValue = dtDetails.Rows[i]["dc_status"].ToString().Trim();
                            if (dtDetails.Rows[i]["doc_dob"].ToString().Trim() == "Y")
                                lnkDoB.Text = "Download";
                            if (dtDetails.Rows[i]["doc_doj"].ToString().Trim() == "Y")
                                lnkDoJ.Text = "Download";
                            if (dtDetails.Rows[i]["doc_Aadhar"].ToString().Trim() == "Y")
                                lnkAadhar.Text = "Download";

                            break;
                        }

                    }

                }

            }
        }

        protected void GridCdpoScrn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
}