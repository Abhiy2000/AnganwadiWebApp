using AnganwadiLib.Business;
using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmApplicationVerifyMst : System.Web.UI.Page
    {
        double totalMarks = 0;
        double marks = 0;
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDepartmentRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Transaction" + " >> " + "Application Verification Master";

                if (Session["AppId"].ToString() != null)
                {
                    filldropdwon();
                    GetDetails();
                }
            }
        }
        protected void filldropdwon()
        {
            MstMethods.Dropdown.Fill(ddlAnganwadi, "tb_anganwadi_mas", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' and var_angnwadimst_active='Y' order by trim(var_angnwadimst_angnname)", "");
            MstMethods.Dropdown.Fill(ddlporition, "tb_porition_mas", "var_porition_poriname", "num_porition_poriid", " num_porition_compid ='" + Session["GrdLevel"].ToString() + "' and var_porition_active='Y' order by trim(var_porition_poriname)", "");
            bindcompany();
            bindgrid();
        }
        public void bindcompany()
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
        public void GetDetails()
        {
            string qry = " select COMPID, APPLIID, APPLINO, APPNAME, ADDRESS, PORTID, POSITION, ANGNID, ANGNNAME, EDUQUALID, ";
            qry += " EDUQULI_NAME, MARITALID, MARISTATUS_NAME, PROJID, PROJNAME, DIVID, DIVISION, DISTID, DISTRICT, ";
            qry += " DOB, AGE from vw_applidtls ";
            qry += " where COMPID = '" + Session["GrdLevel"].ToString() + "' and APPLIID = '"+ Session["AppId"].ToString() + "' ";

            DataTable tblgrdbind = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
            if (tblgrdbind.Rows.Count > 0)
            {
                Session["tblgrdbind"] = tblgrdbind;
                txtNameApp.Text = tblgrdbind.Rows[0]["APPNAME"].ToString();
                txtAddress.Text = tblgrdbind.Rows[0]["ADDRESS"].ToString();
                ddlDivision.SelectedValue = tblgrdbind.Rows[0]["DIVID"].ToString();
                ddlDivision_SelectedIndexChanged(null, null);
                ddlDistrict.SelectedValue = tblgrdbind.Rows[0]["DISTID"].ToString();
                ddlAnganwadi.SelectedValue = tblgrdbind.Rows[0]["ANGNID"].ToString();
                ddlporition.SelectedValue = tblgrdbind.Rows[0]["PORTID"].ToString();

                DateTime dob;
                if (DateTime.TryParse(tblgrdbind.Rows[0]["DOB"].ToString(), out dob))
                {
                    birthdt.Text = dob.ToString("dd/MM/yyyy");
                }
                else
                {
                    birthdt.Text = ""; // or show error
                }
                
                txtAge.Text = tblgrdbind.Rows[0]["AGE"].ToString();
                Disable();
            }
            else
            {
                MessageAlert("No Record Found","");
                return;
            }
        }
        public void bindgrid()
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
        private void SetInitialRowDocDtls(int NoRows)
        {
            ViewState["dtUploadMarks"] = null;

            DataTable dtUploadMarks = new DataTable();
            //DataRow dr = null;
            dtUploadMarks.Columns.Add(new DataColumn("Docid", typeof(Int64)));
            dtUploadMarks.Columns.Add(new DataColumn("Docname", typeof(string)));
            dtUploadMarks.Columns.Add(new DataColumn("Marks", typeof(string)));
            for (int i = 0; i < NoRows; i++)
            {
                DataRow dtrow = dtUploadMarks.NewRow();
                dtUploadMarks.Rows.Add(dtrow);
            }

            dtUploadMarks.AcceptChanges();
            ViewState["dtUploadMarks"] = dtUploadMarks;
        }
        public void SetUploadedFiles(GridView Grv)
        {
            DataTable dtTempDoc = (DataTable)ViewState["dtUploadMarks"];
            DataRow dr = null;
            if (dtTempDoc != null && Grv.Rows.Count > 0)
            {
                for (int rowindex = 0; rowindex < Grv.Rows.Count; rowindex++)
                {
                    int docid = Convert.ToInt32(Grv.Rows[rowindex].Cells[0].Text);
                    Label txtDocName = Grv.Rows[rowindex].FindControl("lblDocNames") as Label;
                    TextBox txtMarks = Grv.Rows[rowindex].FindControl("txtMarks") as TextBox;

                    dtTempDoc.Rows[rowindex]["Docid"] = docid;
                    dtTempDoc.Rows[rowindex]["Docname"] = txtDocName.Text;
                    dtTempDoc.Rows[rowindex]["Marks"] = txtMarks.Text;
                }

                ViewState["dtUploadMarks"] = dtTempDoc;

                SetPreviousDataDocGrd();
            }
        }
        private void SetPreviousDataDocGrd()
        {

            if (ViewState["dtUploadMarks"] != null)
            {
                DataTable dt = (DataTable)ViewState["dtUploadMarks"];
                if (dt.Rows.Count > 0 && grdDoc.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDoc.Rows.Count; i++)
                    {
                        TextBox txtMarks = grdDoc.Rows[i].FindControl("txtMarks") as TextBox;

                        dt.Rows[i]["Marks"] = txtMarks.Text;
                    }
                }
            }
        }
        public void Disable()
        {
            txtNameApp.Enabled = false;
            txtAddress.Enabled = false;
            ddlDivision.Enabled = false;
            ddlDistrict.Enabled = false;
            ddlAnganwadi.Enabled = false;
            ddlporition.Enabled = false;
            birthdt.DisableControl();
            txtAge.Enabled = false;
        }

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
                string qry = " select docname,blob_applidoc_doc,var_applidoc_extension from tb_applidoc_det ";
                qry += " inner join vw_doclist on docid = num_applidoc_docid ";
                qry += " where num_applidoc_appliid='" + Session["AppId"].ToString() + "' ";
                qry += " and num_applidoc_docid = '"+ hosappdoc_id + "' ";
                DataTable tblgrdbind = (DataTable)MstMethods.Query2DataTable.GetResult(qry);

                if (tblgrdbind.Rows.Count > 0)
                {
                    string extension = tblgrdbind.Rows[0]["var_applidoc_extension"].ToString();
                    string filename = tblgrdbind.Rows[0]["docname"].ToString().Replace(" ", "_").Replace("%20", "_") + extension;
                    byte[] fileData = (byte[])tblgrdbind.Rows[0]["blob_applidoc_doc"];

                    string mimeType = "application/octet-stream";
                    
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
                    string message = "Document Not Found";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", $"alert('{message}');", true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Document Not Found";
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowErrorMessage", $"alert('{errorMessage}');", true);
            }

        }

        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            if (txtRemark.Text == "" || txtRemark.Text == null)
            {
                MessageAlert("Please enter Remark", "");
                txtRemark.Focus();
                return;
            }

            BoONLApp obj = new BoONLApp();
            obj.UserId = Session["UserId"].ToString();
            obj.corpId = Convert.ToInt64(Session["GrdLevel"].ToString());
            obj.DivId = Convert.ToInt64(ddlDivision.SelectedValue.ToString());
            obj.DistId = Convert.ToInt64(ddlDistrict.SelectedValue.ToString());
            obj.ProjId = 0;
            obj.AppName = txtNameApp.Text.Trim().ToString();
            obj.Anganwadiid = Convert.ToInt64(ddlAnganwadi.SelectedValue.ToString());
            obj.Appaddress = txtAddress.Text.Trim();
            obj.poritid = Convert.ToInt64(ddlporition.SelectedValue.ToString());
            obj.dob = Convert.ToDateTime(birthdt.Value);
            obj.handicapeid = null;
            obj.disabilityage = 0;
            obj.disability = null;
            obj.age = Convert.ToInt64(txtAge.Text.Trim());
            obj.maritalid = 0;
            obj.eduQualid = 0;
            obj.aadharno = null;
            obj.panno = null;
            obj.religion = null;
            obj.cast = null;
            obj.subcast = null;
            obj.Mode = 2;
            obj.docverify = rdbDocVerify.SelectedValue;
            obj.authstatus = rdbAppliApproved.SelectedValue;
            obj.authremark = txtRemark.Text;
            obj.strMarks = Session["resultString"].ToString();
            obj.applid = Convert.ToInt64(Session["AppId"].ToString());

            obj.Insert();

            if (obj.ErrorCode == 9999)
            {
                MessageAlert(obj.ErrorMsg, "../Transaction/FrmApplicationVerifyList.aspx");
                return;
            }
            else
            {
                MessageAlert(obj.ErrorMsg, "../Transaction/FrmApplicationVerifyMst.aspx");
                return;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Transaction/FrmApplicationVerifyList.aspx");
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void txtMarks_TextChanged(object sender, EventArgs e)
        {
            List<string> marksList = new List<string>();

            // Loop through data rows
            foreach (GridViewRow row in grdDoc.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtMarks = row.FindControl("txtMarks") as TextBox;
                    if (txtMarks != null)
                    {
                        if (double.TryParse(txtMarks.Text, out marks))
                        {
                            totalMarks += marks;
                            marksList.Add(marks.ToString());
                        }
                    }
                }
            }

            // Update footer total
            string finalTotal = "";
            GridViewRow footerRow = grdDoc.FooterRow;
            if (footerRow != null)
            {
                Label lblTotalMarks = footerRow.FindControl("lblTotalMarks") as Label;
                if (lblTotalMarks != null)
                {
                    lblTotalMarks.Text = totalMarks.ToString();
                    finalTotal = lblTotalMarks.Text;
                }
            }

            // Combine into final string
            string resultString = string.Join("$", marksList) + "$" + finalTotal + "#";

            if (resultString != "")
            {
                resultString = resultString.Substring(0, resultString.Length - 1);
            }

            Session["resultString"] = resultString;
        }
    }
}