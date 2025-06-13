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
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmONLAppForm : System.Web.UI.Page
    {
        #region "MeesageAlert"
        public void MessageAlert(String Message, String WindowsLocation)
        {
            String str = "";

            str = "alert('|| " + Message + " ||');";

            if (WindowsLocation != "")
            {
                str += "window.location = '" + WindowsLocation + "';";
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, str, true);
            return;
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
                filldropdwon();
                //hdnduedate.Value = DateTime.Today.Date.ToString("dd-MM-yyyy");
            }
            else
            {
                #region "Documents grid records to maintain after postback"
                //for subject grid record to maintain after postbackUplo
                if (IsPostBack && grdDoc.Rows.Count > 0)
                {

                    for (int i = 0; i < grdDoc.Rows.Count; i++)
                    {
                        FileUpload UploadedFileName = grdDoc.Rows[i].FindControl("FlDoc") as FileUpload;
                        if (UploadedFileName.PostedFile != null && UploadedFileName.PostedFile.FileName != "")
                        {

                            string extension = Path.GetExtension(UploadedFileName.PostedFile.FileName).ToUpper();
                            //Allow upto 5mb
                            if (UploadedFileName.FileContent.Length > 5242880)
                            {
                                MessageAlert("Document Size Should Be <5 mb", "");
                                return;
                            }
                            if (UploadedFileName.PostedFile.ContentType.ToUpper() != "IMAGE/PNG" && extension != ".JPEG" && extension != ".JPG" && extension != ".PNG" && extension != ".PDF")
                            {
                                MessageAlert("Document Should Be Accepatable In JPEG/JPG/Png/PDF Format Only", "");
                                return;
                            }

                            SetUploadedFiles(grdDoc);
                        }

                    }
                }
                #endregion
            }

        }
        protected void filldropdwon()
        {
            MstMethods.Dropdown.Fill(ddlAnganwadi, "tb_anganwadi_mas", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' and var_angnwadimst_active='Y' order by trim(var_angnwadimst_angnname)", "");
            MstMethods.Dropdown.Fill(ddlproj, "tb_project_mas", "var_project_projname", "num_project_projid", " num_project_compid ='" + Session["GrdLevel"].ToString() + "' and var_project_active='Y' order by trim(var_project_projname)", "");
            MstMethods.Dropdown.Fill(ddlporition, "tb_porition_mas", "var_porition_poriname", "num_porition_poriid", " num_porition_compid ='" + Session["GrdLevel"].ToString() + "' and var_porition_active='Y' order by trim(var_porition_poriname)", "");
            MstMethods.Dropdown.Fill(ddlMarStatus, "tb_Maristatus_mas", "var_Maristatus_name", "num_Maristatus_id", " num_Maristatus_compid ='" + Session["GrdLevel"].ToString() + "' and var_Maristatus_active='Y' order by trim(var_Maristatus_name)", "");
            MstMethods.Dropdown.Fill(ddlEduQuali, "tb_eduQuli_mas", "var_eduQuli_name", "num_eduQuli_id", " num_eduQuli_compid ='" + Session["GrdLevel"].ToString() + "' and var_eduQuli_active='Y' order by trim(var_eduQuli_name)", "");
            bindcompany();
            bindgrid();
        }
        protected void bindcompany()
        {
            String str = "select brcategory, parentid from vw_companyview where brid = " + Session["GrdLevel"].ToString();
            DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);
            Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["BRCategory"]);
            if (BRCategory == 0)    //Admin
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
            }
            if (BRCategory == 1)    //State
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
            }
            else if (BRCategory == 2)   // Div
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                ddlDivision.SelectedValue = Session["GrdLevel"].ToString();
                ddlDivision_SelectedIndexChanged(null, null);
                ddlDivision.Enabled = false;
            }
            else if (BRCategory == 3)   // Dis
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");
                ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();
                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
            }

            else if (BRCategory == 4)   // CDPO
            {
                str = "select a.num_corporation_corpid cdpo, a.num_corporation_parentid dis, b.num_corporation_parentid div ";
                str += "from tb_corporation_mas a ";
                str += "inner join tb_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();

                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
            }

            else if (BRCategory == 5)   // Beat
            {
                str = "select a.num_corporation_corpid beat, a.num_corporation_parentid cdpo, b.num_corporation_parentid dis, c.num_corporation_parentid div ";
                str += "from tb_corporation_mas a ";
                str += "inner join tb_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                str += "inner join tb_corporation_mas c on b.num_corporation_parentid = c.num_corporation_corpid ";
                str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");

                ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
            }
        }
        protected void bindgrid()
        {
            string qry = " select docid,docname from vw_doclist where comid='" + Session["GrdLevel"].ToString() + "' ";
            DataTable tblgrdbind = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
            if (tblgrdbind.Rows.Count > 0)
            {
                Session["dtdoc"] = tblgrdbind;
                grdDoc.Visible = true;
                grdDoc.DataSource = tblgrdbind;
                grdDoc.DataBind();
                SetInitialRowDocDtls(tblgrdbind.Rows.Count);
            }
            else
            {
                grdDoc.DataSource = null;
                grdDoc.DataBind();
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }
        protected void ddlproj_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx.aspx");
        }
        public Boolean Validation(String a)
        {
            if (Convert.ToString(a.Trim()) == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void validationcontrols()
        {
            if (ddlDivision.SelectedValue.ToString() == "0" || ddlDivision.SelectedValue.ToString() == "")
            {
                MessageAlert("Please Select Division", "");
                ddlDivision.Focus();
                return;
            }
            if (ddlDistrict.SelectedValue.ToString() == "0" || ddlDistrict.SelectedValue.ToString() == "")
            {
                MessageAlert("Please Select District", "");
                ddlDistrict.Focus();
                return;
            }
            if (ddlproj.SelectedValue.ToString() == "0" || ddlproj.SelectedValue.ToString() == "")
            {
                MessageAlert("Please Select Project", "");
                ddlproj.Focus();
                return;
            }
            if (Validation(txtNameApp.Text) == true)
            {
                MessageAlert("Please enter Name of Applicant", "");
                txtNameApp.Focus();
                return;
            }
            if (ddlAnganwadi.SelectedValue.ToString() == "0" || ddlAnganwadi.SelectedValue.ToString() == "")
            {
                MessageAlert("Please Select Anganwadi", "");
                ddlAnganwadi.Focus();
                return;
            }
            if (Validation(txtAddress.Text) == true)
            {
                MessageAlert("Please enter Address", "");
                txtAddress.Focus();
                return;
            }
            if (ddlporition.SelectedValue.ToString() == "0" || ddlporition.SelectedValue.ToString() == "")
            {
                MessageAlert("Please Select Porition", "");
                ddlporition.Focus();
                return;
            }
            if (rbdhandicapp.SelectedValue.ToString() == "0" || rbdhandicapp.SelectedValue.ToString() == "")
            {
                MessageAlert("Physically Handicapped cannot be blank", "");
                rbdhandicapp.Focus();
                return;
            }
            if (Validation(txtage.Text) == true)
            {
                MessageAlert("Age Cannot be blank", "");
                txtage.Focus();
                return;
            }
            if (ddlMarStatus.SelectedValue.ToString() == "0" || ddlMarStatus.SelectedValue.ToString() == "")
            {
                MessageAlert("Please Select Marital Status", "");
                ddlMarStatus.Focus();
                return;
            }
            if (ddlEduQuali.SelectedValue.ToString() == "0" || ddlEduQuali.SelectedValue.ToString() == "")
            {
                MessageAlert("Please Select Education Qualification", "");
                ddlEduQuali.Focus();
                return;
            }
            if (Validation(txtAadhar.Text) == true)
            {
                MessageAlert("Please enter Aadhar number", "");
                txtAadhar.Focus();
                return;
            }
            if (Validation(txtAadhar.Text) == false)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtAadhar.Text, @"^\d{12}$"))
                {
                    // Display error (optional)
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Aadhar number. It must be 12 digits.');", true);
                    return;
                }
            }
            if (Validation(txtPanNo.Text) == true)
            {
                MessageAlert("Please enter PAN number", "");
                txtPanNo.Focus();
                return;
            }
            if (Validation(txtReligion.Text) == true)
            {
                MessageAlert("Please enter Religion", "");
                txtReligion.Focus();
                return;
            }
            if (Validation(txtcast.Text) == true)
            {
                MessageAlert("Please enter Cast", "");
                txtcast.Focus();
                return;
            }
            if (Validation(txtSubcast.Text) == true)
            {
                MessageAlert("Please enter SubCast", "");
                txtSubcast.Focus();
                return;
            }
        }
        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            validationcontrols();
            string DocumentStr = "";
            Int64 count = 0;
            if (grdDoc.Rows.Count > 0)
            {

                for (int Rowcnt = 0; Rowcnt < grdDoc.Rows.Count; Rowcnt++)
                {
                    string DocId = grdDoc.Rows[Rowcnt].Cells[0].Text;
                    string DocName = grdDoc.Rows[Rowcnt].Cells[2].Text;

                    count++;
                    DocumentStr += DocId + "$";
                    Image image = grdDoc.Rows[Rowcnt].FindControl("UplImg") as Image;
                    if (!string.IsNullOrEmpty(image.ImageUrl))
                    {

                    }
                    else
                    {
                        MessageAlert("Please upload file", "");
                        return;
                    }

                }

                if (DocumentStr.Trim().Length > 0)
                {
                    DocumentStr = DocumentStr.Substring(0, DocumentStr.Length - 1);
                }
                else
                {
                    MessageAlert("Select Atleast One Documet", "");
                    return;
                }

                if (count != grdDoc.Rows.Count)
                {
                    MessageAlert("Select All Documets", "");
                    return;
                }
            }
            else
            {
                MessageAlert("Select Atleast One Documet", "");
                return;
            }
            BoONLApp obj = new BoONLApp();
            obj.UserId = Session["UserId"].ToString();
            obj.corpId = Convert.ToInt64(Session["GrdLevel"].ToString());
            obj.DivId = Convert.ToInt64(ddlDivision.SelectedValue.ToString());
            obj.DistId = Convert.ToInt64(ddlDistrict.SelectedValue.ToString());
            obj.ProjId = Convert.ToInt64(ddlproj.SelectedValue.ToString());
            obj.AppName = txtNameApp.Text.Trim().ToString();
            obj.Anganwadiid = Convert.ToInt64(ddlAnganwadi.SelectedValue.ToString());
            obj.Appaddress = txtAddress.Text.Trim();
            obj.poritid = Convert.ToInt64(ddlporition.SelectedValue.ToString());
            obj.dob = Convert.ToDateTime(birthdt.Value);
            obj.handicapeid = rbdhandicapp.SelectedValue.ToString();
            if (txthandicapAge.Text.ToString() != "")
            {
                obj.disabilityage = Convert.ToInt64(txthandicapAge.Text.Trim());
            }
            else
            {
                obj.disabilityage = 0;
            }
            if (txtDisability.Text.ToString() != "")
            {
                obj.disability = txtDisability.Text.Trim();
            }
            else
            {
                obj.disability = null;
            }
            obj.age = Convert.ToInt64(txtage.Text.Trim());
            obj.maritalid = Convert.ToInt64(ddlMarStatus.SelectedValue.ToString());
            obj.eduQualid = Convert.ToInt64(ddlEduQuali.SelectedValue.ToString());
            obj.aadharno = txtAadhar.Text.Trim();
            obj.panno = txtPanNo.Text.Trim().ToString();
            obj.religion = txtReligion.Text.Trim();
            obj.cast = txtcast.Text.Trim();
            obj.subcast = txtSubcast.Text.Trim();

            obj.Insert();

            if (obj.ErrorCode == 9999)
            {
                Session["Appno"] = obj.applicationid;
                DataTable dtdoc = (DataTable)ViewState["dtUploadsDoc"];
                if (dtdoc.Rows.Count > 0)
                {
                    try
                    {
                        UpdateDocments(dtdoc, DocumentStr, Convert.ToString(obj.applicationid));
                    }
                    catch (Exception EX)
                    {
                        MessageAlert("Unable To Upload Documnet", "");
                        return;
                    }
                }

                MessageAlert(obj.ErrorMsg, "../Transaction/FrmONLAppForm.aspx");
                return;
            }
            else
            {
                MessageAlert(obj.ErrorMsg, "../Transaction/FrmONLAppForm.aspx");
                return;
            }
        }

        #region "Document Upload"
        protected void grdDoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
        protected void grdDoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SetInitialRowDocDtls(int NoRows)
        {
            ViewState["dtUploadsDoc"] = null;

            DataTable dtUploadsDoc = new DataTable();
            //DataRow dr = null;
            dtUploadsDoc.Columns.Add(new DataColumn("Docid", typeof(Int64)));
            dtUploadsDoc.Columns.Add(new DataColumn("Docname", typeof(string)));
            dtUploadsDoc.Columns.Add(new DataColumn("Filename", typeof(string)));
            dtUploadsDoc.Columns.Add(new DataColumn("FileByts", typeof(byte[])));
            dtUploadsDoc.Columns.Add(new DataColumn("FileExtension", typeof(string)));
            for (int i = 0; i < NoRows; i++)
            {
                DataRow dtrow = dtUploadsDoc.NewRow();
                dtUploadsDoc.Rows.Add(dtrow);
            }

            dtUploadsDoc.AcceptChanges();
            ViewState["dtUploadsDoc"] = dtUploadsDoc;
        }
        public bool UpdateDocments(DataTable dtDoc, string OutDocStr, String appid)
        {
            string[] DocRows = OutDocStr.Split('$');
            bool UpdatedDoc = false;
            for (int i = 0; i < DocRows.Length; i++)
            {
                //for Positions 
                //0 : aoms_ServAppli_mas.num_servappli_id i.e internal-id
                //1 : aoms_ServAppliDoc_det.num_servapplidoc_id  i.e internal id of child table
                //2 :Doc ID 
                string[] DocValues = DocRows[i].Split('#');

                DataRow[] dr = dtDoc.Select("Docid = " + DocValues[0]);
                //var DocByts = (byte[])dr[0]["FileByts"];

                if (!(dr.Length > 0))
                {
                    continue;
                }

                AnganwadiLib.Data.GetCon con = new AnganwadiLib.Data.GetCon();

                OracleParameter oraParameter = new OracleParameter("oraParameter", OracleDbType.Blob);

                oraParameter.Value = (byte[])dr[0]["FileByts"];
                OracleCommand Cmd = new OracleCommand();

                string userid = Session["UserId"].ToString();
                string Query = "";
                Query += " insert into tb_appliDoc_det(num_appliDoc_id, num_appliDoc_appliid, num_appliDoc_docid, var_appliDoc_extension, blob_appliDoc_doc, var_appliDoc_insby, dat_appliDoc_insdate) ";
                Query += "     values(sq_applidoc.nextval,'" + appid + "','" + DocValues[0] + "','" + dr[0]["FileExtension"] + "',:oraParameter,'" + userid + "',sysdate) ";
                Cmd.CommandText = Query;

                Cmd.Connection = con.connection;
                Cmd.Parameters.Add(oraParameter);
                if (con.connection.State == ConnectionState.Open)
                {
                    con.CloseConn();
                }
                else
                {
                    con.OpenConn();
                }
                Int32 effectedRows = Cmd.ExecuteNonQuery();
                con.CloseConn();
                if (effectedRows > 0)
                {
                    UpdatedDoc = true;
                }
                else
                {
                    UpdatedDoc = false;
                }
            }
            return UpdatedDoc;
        }
        protected void grdDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PreviewFile")
            {

                LinkButton linkButton = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)linkButton.NamingContainer;
                int rowIndex = row.RowIndex;
                int applicationId = Convert.ToInt32(e.CommandArgument);
                PreviewFile(rowIndex, applicationId);
                //ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#myModal').modal('show');", true);
            }
        }
        private void PreviewFile(int rowIndex, int hosappdoc_id)
        {
            try
            {
                long hosappdoc_id_long = (long)hosappdoc_id;
                GridViewRow row = grdDoc.Rows[rowIndex];
                DataTable dt = (DataTable)ViewState["dtUploadsDoc"];
                DataRow fileDataRow = dt.AsEnumerable()
                    .FirstOrDefault(r => !r.IsNull("Docid") && r.Field<long>("Docid") == hosappdoc_id_long);

                if (fileDataRow != null)
                {
                    string filename = fileDataRow["Filename"].ToString();
                    byte[] fileData = (byte[])fileDataRow["FileByts"];

                    string mimeType = "application/octet-stream";
                    string extension = System.IO.Path.GetExtension(filename).ToLower();
                    switch (extension)
                    {
                        case ".pdf": mimeType = "application/pdf"; break;
                        case ".jpg": mimeType = "image/jpeg"; break;
                        case ".png": mimeType = "image/png"; break;
                        case ".doc": mimeType = "application/msword"; break;
                        case ".docx": mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; break;
                    }
                    string tempFilePath = Server.MapPath("~/ImageGarbage/" + filename);
                    System.IO.File.WriteAllBytes(tempFilePath, fileData);

                    string previewUrl = ResolveUrl("~/ImageGarbage/" + filename);
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenPreview", "window.open('" + previewUrl + "', '_blank');", true);
                }
                else
                {
                    string message = "Please upload document.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", $"alert('{message}');", true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Please upload document.";
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowErrorMessage", $"alert('{errorMessage}');", true);
            }

        }
        public void SetUploadedFiles(GridView Grv)
        {
            DataTable dtTempDoc = (DataTable)ViewState["dtUploadsDoc"];
            DataRow dr = null;
            if (dtTempDoc != null && Grv.Rows.Count > 0)
            {
                for (int rowindex = 0; rowindex < Grv.Rows.Count; rowindex++)
                {
                    int docid = Convert.ToInt32(Grv.Rows[rowindex].Cells[0].Text);
                    Label txtDocName = Grv.Rows[rowindex].FindControl("lblDocNames") as Label;
                    FileUpload UplFileName = Grv.Rows[rowindex].FindControl("FlDoc") as FileUpload;
                    string fileName = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss_ffff");
                    //UplFile_Path = "", saveFilePath = "";

                    if (UplFileName.PostedFile != null && UplFileName.PostedFile.FileName != "")
                    {
                        string extension = Path.GetExtension(UplFileName.PostedFile.FileName).ToUpper();
                        byte[] filebytes = UplFileName.FileBytes;

                        dtTempDoc.Rows[rowindex]["Filename"] = (UplFileName.PostedFile.FileName);
                        dtTempDoc.Rows[rowindex]["FileByts"] = filebytes;
                        dtTempDoc.Rows[rowindex]["FileExtension"] = extension;
                    }
                    dtTempDoc.Rows[rowindex]["Docid"] = docid;
                    dtTempDoc.Rows[rowindex]["Docname"] = txtDocName.Text;


                }

                ViewState["dtUploadsDoc"] = dtTempDoc;

                SetPreviousDataDocGrd();
            }
        }
        private void SetPreviousDataDocGrd()
        {

            if (ViewState["dtUploadsDoc"] != null)
            {
                DataTable dt = (DataTable)ViewState["dtUploadsDoc"];
                if (dt.Rows.Count > 0 && grdDoc.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDoc.Rows.Count; i++)
                    {
                        Image imgVal = (Image)grdDoc.Rows[i].FindControl("UplImg");
                        if (Convert.ToString(dt.Rows[i]["FileByts"]) != "" && Convert.ToString(dt.Rows[i]["FileByts"]) != null && dt.Rows[i]["FileExtension"].ToString() == ".PDF")
                        {
                            imgVal.ImageUrl = "../Images/Pdf.png";
                        }
                        else if (Convert.ToString(dt.Rows[i]["FileByts"]) != "" && Convert.ToString(dt.Rows[i]["FileByts"]) != null && dt.Rows[i]["FileExtension"].ToString() == ".JPG")
                        {
                            imgVal.ImageUrl = "../Images/Image.jpg";
                        }
                        else if (Convert.ToString(dt.Rows[i]["FileByts"]) != "" && Convert.ToString(dt.Rows[i]["FileByts"]) != null && dt.Rows[i]["FileExtension"].ToString() == ".JPEG")
                        {
                            imgVal.ImageUrl = "../Images/Image.jpg";
                        }
                        else if (Convert.ToString(dt.Rows[i]["FileByts"]) != "" && Convert.ToString(dt.Rows[i]["FileByts"]) != null && dt.Rows[i]["FileExtension"].ToString() == ".PNG")
                        {
                            imgVal.ImageUrl = "../Images/Image.jpg";
                        }
                    }
                }
            }
        }
        #endregion
    }
}