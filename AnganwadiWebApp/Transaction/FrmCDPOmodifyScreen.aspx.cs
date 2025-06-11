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

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmCDPOmodifyScreen : System.Web.UI.Page
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
        }
        protected void LoadGrid()
        {
            String query = "select  num_sevikaedit_id sevikaedit_id, divid, distid, cdpoid, ";
            query += " num_sevikamaster_sevikaid sevikaid, var_sevikamaster_name sevikaname, ";
            query += " to_char(dtold_sevikaedit_dob,'dd/MM/yyyy') old_dob, to_char(dtold_sevikaedit_doj,'dd/MM/yyyy')     old_joindate,  ";
            query += " num_sevikaedit_oldaadhar old_aadharno, to_char(dtnew_sevikaedit_dob ,'dd/MM/yyyy')   new_dob,  ";
            query += " to_char(dtnew_sevikaedit_doj,'dd/MM/yyyy') new_joindate, num_sevikaedit_newaadhar  new_aadharno,  ";
            query += " var_sevikaedit_cdpostatus cdpo_status, var_sevikaedit_cdporemark   cdpo_remark,  ";
            query += " (case when nvl(dbms_lob.getlength(BLOB_DOCDOB_NEW),0) =0 then 'N' else 'Y' end  ) doc_dob, ";
            query += " (case when nvl(dbms_lob.getlength(BLOB_DOCDOJ_NEW), 0) = 0 then 'N' else 'Y' end) doc_doj, ";
            query += " (case when nvl(dbms_lob.getlength(BLOB_DOCAADHAR_NEW), 0) = 0 then 'N' else 'Y' end) doc_Aadhar, ";
            query += " num_sevikamaster_compid comp_id,var_docdob_type dob_type,var_docdoj_type doj_type,var_docaadhar_type aadhar_type  from aoup_sevikaedit  inner join  aoup_sevikamaster_def ";
            query += " on num_sevikaedit_sevikaid = num_sevikamaster_sevikaid  ";
            query += " inner join corpinfo on bitid = num_sevikaedit_compid and cdpoid = num_sevikaedit_cdpoid  ";
            query += " inner join aoup_sevikaedit_doctypes on  num_sevikaedtdoc_editid = num_sevikaedit_id ";
            query += " where var_sevikaedit_cdpostatus is null  and  num_sevikaedit_cdpoid = " + ddlCDPO.SelectedValue + " and num_sevikaedit_distrid = " + ddlDistrict.SelectedValue + " ";

            //added to check whether all the remarks fields are empty 
            query += " and var_sevikaedit_cdporemark is null  and var_sevikaedit_dporemark is null and var_hoappr_remark is null and var_dc_appremark is null ";
            DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(query);

            ViewState["TblCDpoDtls"] = null;
            if (TblGetBrid != null && TblGetBrid.Rows.Count > 0)
            {
                GridCdpoScrn.DataSource = TblGetBrid;
                GridCdpoScrn.DataBind();
                PnlSerch.Visible = true;
                BtnSubmit.Visible = true;

                ViewState["TblCDpoDtls"] = TblGetBrid;
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
                            if (TblGetBrid.Rows[i]["cdpo_status"] != null && TblGetBrid.Rows[i]["cdpo_status"].ToString().Trim() != "")
                                RdbStatus.SelectedValue = TblGetBrid.Rows[i]["cdpo_status"].ToString().Trim();
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
                MessageAlert("No data found","");
               
            }

        }
        public void rbtApproved_Changed(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            RadioButton rbtApproved = (RadioButton)GridCdpoScrn.Rows[index].FindControl("rbtApproved");
            RadioButton rbtReject = (RadioButton)GridCdpoScrn.Rows[index].FindControl("rbtReject");
            if (rbtApproved.Checked)
            {
                rbtReject.Checked = false;
                // rbtReject.Enabled = false;
            }
            else
            {
                rbtReject.Enabled = true;
            }
        }
        public void rbtReject_Changed(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            RadioButton rbtApproved = (RadioButton)GridCdpoScrn.Rows[index].FindControl("rbtApproved");
            RadioButton rbtReject = (RadioButton)GridCdpoScrn.Rows[index].FindControl("rbtReject");
            if (rbtReject.Checked)
            {
                rbtApproved.Checked = false;
                //rbtApproved.Enabled = false;
            }
            else
            {
                rbtApproved.Enabled = true;
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string Strrecords = "";
            foreach (GridViewRow row in GridCdpoScrn.Rows)
            {
                RadioButtonList RdbBtns = (RadioButtonList)row.FindControl("RdbStatus");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                string SevikaEdtId = row.Cells[0].Text.Trim();
                if (RdbBtns.SelectedValue!="" && RdbBtns.SelectedValue !=null)
                {
                    if (txtRemark.Text.Trim()=="")
                    {
                        MessageAlert("Please Enter Remark", "");
                        return;
                    }
                    else
                    {
                        Strrecords += SevikaEdtId + "$" + RdbBtns.SelectedValue + "$" + txtRemark.Text.Trim() + "#";
                    }
                }
   
            }

            if (Strrecords.Length > 0 && Strrecords.Trim()!="")
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
            objData.FlagMode = "CDPO";
            objData.StrCDPO = Strrecords;

            objData.InsertCdpo();

            if (objData.ErrorCode == -100)
            {
                MessageAlert(objData.ErrorMessage, "../Transaction/FrmCDPOmodifyScreen.aspx");
                return;
            }
            else
            {
                MessageAlert(objData.ErrorMessage, "");
                return;
            }

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
                    string ExtensionType = "",StrContype="";
                    if (TblFile.Rows[0]["Filetype"].ToString().ToUpper().Contains(".PDF")==true)
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

        protected void lnkBtnViewDoc_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTbl = ViewState["CurrentTable"] as DataTable;

                if (ViewState["CurrentTable"] == null)
                {
                    MessageAlert("Invalid Document Data", "");
                    return;
                }
                else
                {
                    int RowIndex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                    Label lblSevikaID = (Label)GridCdpoScrn.Rows[RowIndex].FindControl("lblSevikaID");
                    string Contidion = "sevikaid='" + lblSevikaID.Text + "'";

                    DataRow objdatarow = dtTbl.Select(Contidion).FirstOrDefault();

                    if (objdatarow != null)
                    {
                        byte[] bytes = (byte[])objdatarow["DocImage"];
                        String fileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

                        String ExportPath = Server.MapPath("~\\ImageGarbage\\") + fileName;
                        string ExportPath1 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/") + "ImageGarbage/" + fileName;

                        WriteToFile(ExportPath, ref bytes);

                        string str1 = "window.open('" + ExportPath1 + "', '', 'fullscreen=No, toolbars=no, menubar=no, location=no, scrollbars=yes, resizable=no, status=yes, width=1200px, height=700px');";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", str1, true);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageAlert("Something Went Wrong: " + ex.Message, "");
                return;
            }
        }

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
            DataTable dtDetails = ViewState["TblCDpoDtls"] as DataTable;
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
                            if (dtDetails.Rows[i]["cdpo_status"] != null && dtDetails.Rows[i]["cdpo_status"].ToString().Trim() != "")
                                RdbStatus.SelectedValue = dtDetails.Rows[i]["cdpo_status"].ToString().Trim();
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