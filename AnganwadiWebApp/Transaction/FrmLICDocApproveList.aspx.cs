using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using System.IO;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmLICDocApproveList : System.Web.UI.Page
    {
        Cls_Business_LICStatus ObjLICStatus = new Cls_Business_LICStatus();
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString(); // "Sevika LIC Document Upload"
            if (!IsPostBack)
            {
                // LoadGrid();
                Session["GrdLevel"].ToString();
                PnlSerch.Visible = false;
                PnlButton.Visible = false;

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
                    PnlButton.Visible = true;
                    PnlSerch.Visible = false;
                    BtnSubmit.Visible = false;
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

                    PnlButton.Visible = true;
                    PnlSerch.Visible = false;
                    BtnSubmit.Visible = false;
                }
            }
        }

        protected void LoadGrid()
        {

            String Query = "";

            Query += " Select divname,distname,cdponame,bitbitname,num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo,  ";
            Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason ExitReason,num_lic_payscal LastSalary,num_lic_licamount ClaimAmount,(case when img_lic_document is null then 'N' else 'Y' end) DocImage, '' OLDTSOFTWARENO,var_lic_flag_insert LicFlagType from aoup_LIC_DEF  ";
            Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid  ";
            Query += " inner join corpinfo on bitid=num_lic_compid ";
            Query += " where var_lic_flag_insert='I' and img_lic_document is not null and var_LIC_Approvby is null ";
              Query += " and num_lic_compid in (select brid from companyview where parentid=" + Session["GrdLevel"] + " and brcategory=5) ";

            if (ViewState["Action"] != null)
            {
                if (ViewState["Action"].ToString() == "Death")
                {
                    Query += " and  var_lic_flag='DT' ";
                }
                else if (ViewState["Action"].ToString() == "Retire")
                {
                    Query += " and  var_lic_flag='RT' ";
                }
                else if (ViewState["Action"].ToString() == "Resign")
                {
                    Query += " and  var_lic_flag In ('RS','TE') ";
                }

            }
            else
            {
                MessageAlert(" Record Not Found ", "");
                return;
            }

            DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (dtSevikaList.Rows.Count > 0)
            {
                grdLICDEF.DataSource = dtSevikaList;
                grdLICDEF.DataBind();
                BtnSubmit.Visible = true;
                PnlSerch.Visible = true;
                BtnSubmit.Enabled = false;
                chkTerms.Checked = false;


                foreach (GridViewRow grd in grdLICDEF.Rows)
                {
                    Label grdSevikaId = (Label)grd.FindControl("lblSevikaID");
                    for (int i = 0; i < dtSevikaList.Rows.Count; i++)
                    {
                        if (grdSevikaId.Text.Trim() == dtSevikaList.Rows[i]["SevikaID"].ToString().Trim())
                        {
                            if (dtSevikaList.Rows[i]["DocImage"].ToString() != "N")
                            {
                                (grd.FindControl("lnkBtnViewDoc") as LinkButton).Text = "View";
                            }
                            break;
                        }

                    }
                }
                ViewState["TblData"] = dtSevikaList;
            }
            else
            {
                grdLICDEF.DataSource = null;
                grdLICDEF.DataBind();
                BtnSubmit.Visible = false;
                PnlSerch.Visible = false;
                MessageAlert(" Record Not Found ", "");
                return;
            }

        }

        protected void ExternalLoadGrid()
        {

            String Query = "";

            Query += " SELECT divname, distname, cdponame, bitbitname, num_lic_compid bit, num_lic_sevikaid sevikaid, var_licsevika_name sevikaname, var_licsevika_aadharno aadharno, ";
            Query += " TRUNC (date_lic_exitdate)exitdate, var_licsevika_remark exitreason, num_lic_payscal lastsalary,num_lic_licamount claimamount, ";
            Query += " VAR_LICSEVIKA_OLDTSOFTWARENO OLDTSOFTWARENO, (case when img_lic_document is null then 'N' else 'Y' end) DocImage, '' OLDTSOFTWARENO,var_lic_flag_insert LicFlagType FROM aoup_lic_def ";
            Query += " INNER JOIN aoup_licsevika_def ON num_licsevika_sevikaid = num_lic_sevikaid ";
            Query += " INNER JOIN corpinfo ON bitid = num_lic_compid ";
            Query += " where img_lic_document is not null and var_LIC_Approvby is null ";
             Query += "  and num_lic_compid in (select brid from companyview where parentid=" + Session["GrdLevel"] + " and brcategory=5 ) ";

            if (ViewState["Action"] != null)
            {
                if (ViewState["Action"].ToString() == "External")
                {
                    Query += " and var_lic_flag_insert='E' ";
                }
            }
            else
            {
                MessageAlert(" Record Not Found ", "");
                return;
            }

            DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (dtSevikaList.Rows.Count > 0)
            {
                grdLICDEF.DataSource = dtSevikaList;
                grdLICDEF.DataBind();
                BtnSubmit.Visible = true;
                PnlSerch.Visible = true;
                BtnSubmit.Enabled = false;
                chkTerms.Checked = false;

                foreach (GridViewRow grd in grdLICDEF.Rows)
                {
                    Label grdSevikaId = (Label)grd.FindControl("lblSevikaID");
                    for (int i = 0; i < dtSevikaList.Rows.Count; i++)
                    {
                        if (grdSevikaId.Text.Trim() == dtSevikaList.Rows[i]["SevikaID"].ToString().Trim())
                        {
                            if (dtSevikaList.Rows[i]["DocImage"].ToString() != "N")
                            {
                                (grd.FindControl("lnkBtnViewDoc") as LinkButton).Text = "View";
                            }
                            break;
                        }

                    }

                }
                ViewState["TblData"] = dtSevikaList;
            }
            else
            {
                grdLICDEF.DataSource = null;
                grdLICDEF.DataBind();
                BtnSubmit.Visible = false;
                PnlSerch.Visible = false;
                MessageAlert(" Record Not Found ", "");
                return;
            }

        }

        public void rbtApproved_Changed(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            RadioButton rbtApproved = (RadioButton)grdLICDEF.Rows[index].FindControl("rbtApproved");
            RadioButton rbtReject = (RadioButton)grdLICDEF.Rows[index].FindControl("rbtReject");
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
            RadioButton rbtApproved = (RadioButton)grdLICDEF.Rows[index].FindControl("rbtApproved");
            RadioButton rbtReject = (RadioButton)grdLICDEF.Rows[index].FindControl("rbtReject");
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
            string ApproveDoc = string.Empty;
            string RejectDoc = string.Empty;
            bool isValid = false;
            foreach (GridViewRow row in grdLICDEF.Rows)
            {
                RadioButton rbtApproved = (RadioButton)row.FindControl("rbtApproved");
                RadioButton rbtReject = (RadioButton)row.FindControl("rbtReject");
                System.Web.UI.WebControls.Label lblSevikaID = (System.Web.UI.WebControls.Label)row.FindControl("lblSevikaID");
                System.Web.UI.WebControls.TextBox txtRejectReason = (System.Web.UI.WebControls.TextBox)row.FindControl("txtRejectReason");
                Label lblTypes = (Label)row.FindControl("lblFlagType");

                if (rbtApproved.Checked)
                {
                    isValid = true;
                    ApproveDoc += lblSevikaID.Text + "$" + txtRejectReason.Text.Trim() + "$" + lblTypes.Text.Trim() + "#";
                }
                else if (rbtReject.Checked)
                {
                    isValid = true;
                    if (txtRejectReason.Text == "")
                    {
                        MessageAlert("Please Fill Reject Reason", "");
                        txtRejectReason.Focus();
                        return;
                    }
                    else
                    {
                        RejectDoc += lblSevikaID.Text + "$" + txtRejectReason.Text.Trim() + "$" + lblTypes.Text.Trim() + "#";
                    }
                }
            }
            if (isValid == false)
            {
                MessageAlert("Please choose Approve OR Reject", "");
                return;
            }
            if (ApproveDoc.Length > 0)
            {
                ApproveDoc = ApproveDoc.Substring(0, ApproveDoc.Length - 1);
            }
            if (RejectDoc.Length > 0)
            {
                RejectDoc = RejectDoc.Substring(0, RejectDoc.Length - 1);
            }

            ObjLICStatus.UserID = Session["UserId"].ToString();
            ObjLICStatus.Apprvstr = ApproveDoc;
            ObjLICStatus.Rejctstr = RejectDoc;
            ObjLICStatus.UpDateLICStatus();

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
            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlCDPO.SelectedValue.ToString();
                //LoadGrid();
                PnlButton.Visible = true;
                PnlSerch.Visible = false;
                BtnSubmit.Visible = false;
            }
        }

        protected void CheckBoxRequired_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = chkTerms.Checked;
        }
        protected void chkTerms_CheckedChanged(object sender, EventArgs e)
        {
            //BtnSubmit.Enabled = chkTerms.Checked;
            if (chkTerms.Checked == true)
            {
                BtnSubmit.Enabled = true;
            }
            else
            {
                BtnSubmit.Enabled = false;
            }
        }

        protected void BtnDeath_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Death";
                LoadGrid();
                chkTerms_CheckedChanged(null, null);
            }
        }
        protected void BtnRetirement_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Retire";
                LoadGrid();
                chkTerms_CheckedChanged(null, null);
            }
        }
        protected void BtnResignation_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Resign";
                LoadGrid();
                chkTerms_CheckedChanged(null, null);
            }
        }
        protected void BtnExternal_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "External";
                ExternalLoadGrid();
                chkTerms_CheckedChanged(null, null);
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnDownload = (HiddenField)grdLICDEF.Rows[rindex].FindControl("hdnDownload");
                Label lblFlagType = (Label)grdLICDEF.Rows[rindex].FindControl("lblFlagType");
                if (hdnDownload.Value != "0")
                {
                    DownloadFile(Convert.ToInt32(hdnDownload.Value), lblFlagType.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }
        }
        public void DownloadFile(int SevikaID, string FlagType)
        {

            string Query = " Select img_lic_document DocImage from aoup_LIC_DEF ";
            Query += " where img_lic_document is not null and var_LIC_Approvby is null  ";
            Query += " and num_lic_sevikaid='" + SevikaID + "' and var_lic_flag_insert= '" + FlagType + "' ";

            DataTable TblFile = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
            if (TblFile.Rows.Count > 0)
            {
                Byte[] DocImage = new Byte[0];
                if (TblFile.Rows[0]["DocImage"] != DBNull.Value)
                {
                    DocImage = (Byte[])(TblFile.Rows[0]["DocImage"]);
                    string base64String = Convert.ToBase64String(DocImage);

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

        protected void lnkBtnViewDoc_Click(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                Label lblSevikaID = (Label)grdLICDEF.Rows[RowIndex].FindControl("lblSevikaID");
                string SevikaId = lblSevikaID.Text.Trim();

                Label lblFlagType = (Label)grdLICDEF.Rows[RowIndex].FindControl("lblFlagType");

                string Query = " Select img_lic_document DocImage from aoup_LIC_DEF ";
                Query += " where img_lic_document is not null and var_LIC_Approvby is null  ";
                Query += " and num_lic_sevikaid='" + SevikaId + "' and var_lic_flag_insert= '" + lblFlagType.Text.Trim() + "' ";

                DataTable dtDoc = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtDoc != null && dtDoc.Rows.Count > 0)
                {
                    byte[] bytes = (byte[])dtDoc.Rows[0]["DocImage"];
                    String fileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

                    String ExportPath = Server.MapPath("~\\ImageGarbage\\") + fileName;
                    string ExportPath1 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/") + "ImageGarbage/" + fileName;

                    WriteToFile(ExportPath, ref bytes);

                    string str1 = "window.open('" + ExportPath1 + "', '', 'fullscreen=No, toolbars=no, menubar=no, location=no, scrollbars=yes, resizable=no, status=yes, width=1200px, height=700px');";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", str1, true);
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

        protected void grdLICDEF_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[17].Visible = false;
            }

        }

        protected void grdLICDEF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLICDEF.PageIndex = e.NewPageIndex;
            DataTable tbl = ViewState["TblData"] as DataTable;

            grdLICDEF.DataSource = tbl;
            grdLICDEF.DataBind();
            foreach (GridViewRow grd in grdLICDEF.Rows)
            {
                Label grdSevikaId = (Label)grd.FindControl("lblSevikaID");
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    if (grdSevikaId.Text.Trim() == tbl.Rows[i]["SevikaID"].ToString().Trim())
                    {
                        if (tbl.Rows[i]["DocImage"].ToString() != "N")
                        {
                            (grd.FindControl("lnkBtnViewDoc") as LinkButton).Text = "View";
                        }
                        break;
                    }

                }

            }

        }
    }
}