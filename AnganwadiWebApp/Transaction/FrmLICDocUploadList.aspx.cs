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
    public partial class FrmLICDocUploadList : System.Web.UI.Page
    {
        Cls_Business_SavikaLIC objSavikaLIC = new Cls_Business_SavikaLIC();
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
                ViewState["Action"] = "";

                //LoadGrid();
                Session["GrdLevel"].ToString();

                PnlSerch.Visible = false;
                PnlButton.Visible = false;

                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();

                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["brcategory"]);

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
                    MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();
                    ddlBeat.SelectedValue = Session["GrdLevel"].ToString();

                    ddlBeat_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                    PnlButton.Visible = true;
                    PnlSerch.Visible = false;
                    BtnSubmit.Visible = false;
                }

            }
            if (IsPostBack)
            {
                FileUploadSaveImage();

            }
        }

        public void FileUploadSaveImage()
        {
            DataTable dt = new DataTable();

            if (ViewState["Action"] != null)
            {
                if (ViewState["Action"].ToString() == "Death")
                {
                    if (ViewState["CurrentTableD"] != null)
                    {
                        dt = (DataTable)ViewState["CurrentTableD"];
                    }
                }
                else if (ViewState["Action"].ToString() == "Retire")
                {
                    if (ViewState["CurrentTableR"] != null)
                    {
                        dt = (DataTable)ViewState["CurrentTableR"];
                    }
                }
                else if (ViewState["Action"].ToString() == "Resign")
                {
                    if (ViewState["CurrentTableRg"] != null)
                    {
                        dt = (DataTable)ViewState["CurrentTableRg"];
                    }

                }
                else if (ViewState["Action"].ToString() == "External")
                {
                    if (ViewState["CurrentTableEx"] != null)
                    {
                        dt = (DataTable)ViewState["CurrentTableEx"];
                    }

                }

            }
            else
            {
                MessageAlert(" Record Not Found ", "");
                return;
            }


            int count = 0;
            foreach (GridViewRow dr in grdLICDEF.Rows)
            {
                count++;
                FileUpload FileUploadDoc2 = (FileUpload)dr.FindControl("FileUploadDocPDF");
                System.Web.UI.WebControls.Image ImgDoc = (System.Web.UI.WebControls.Image)dr.FindControl("ImgDoc");
                System.Web.UI.WebControls.Image ImgDocPDF1 = (System.Web.UI.WebControls.Image)dr.FindControl("ImgDocPDF1");
                if (IsPostBack && FileUploadDoc2.PostedFile != null)
                {
                    if (FileUploadDoc2.PostedFile.FileName.Length > 0)
                    {
                        if (FileUploadDoc2.PostedFile.ContentType == "application/pdf")
                        {

                        }

                        else
                        {
                            MessageAlert("Only PDF file allowed", "");
                            return;
                        }
                        HttpPostedFile PFileThumb = FileUploadDoc2.PostedFile;
                        int lenthdoc = PFileThumb.ContentLength;
                        int KB = (lenthdoc / 2048000) + 1;

                        if (KB > 300)
                        {
                            MessageAlert("Image size of Documnet can not be more than 2 MB", "");
                            return;
                        }

                        Byte[] PropimageBytes = new byte[lenthdoc];
                        PFileThumb.InputStream.Read(PropimageBytes, 0, lenthdoc);

                        String strFilenameoc = Path.GetFileName(PFileThumb.FileName);
                        strFilenameoc = count + "_" + System.DateTime.Now.Date.ToString("ddMMyyyymmhhss") + "_" + strFilenameoc;
                        String filePath = Server.MapPath("..\\ImageGarbage\\") + strFilenameoc;
                        FileUploadDoc2.SaveAs(filePath);
                        ImgDoc.Visible = false;
                        ImgDoc.ImageUrl = "..\\ImageGarbage\\" + strFilenameoc;
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows[count - 1]["ImageByte"] = PropimageBytes;
                        }
                        ImgDocPDF1.ImageUrl = "..\\Images\\pdf.png";
                        ImgDocPDF1.Visible = true;
                    }
                }

            }

            if (ViewState["Action"].ToString() == "Death")
            {
                if (ViewState["CurrentTableD"] != null)
                {
                    ViewState["CurrentTableD"] = dt;
                }
            }
            else if (ViewState["Action"].ToString() == "Retire")
            {
                if (ViewState["CurrentTableR"] != null)
                {
                    ViewState["CurrentTableR"] = dt;
                }
            }
            else if (ViewState["Action"].ToString() == "Resign")
            {
                if (ViewState["CurrentTableRg"] != null)
                {
                    ViewState["CurrentTableRg"] = dt;
                }

            }
            else if (ViewState["Action"].ToString() == "External")
            {
                if (ViewState["CurrentTableEx"] != null)
                {
                    ViewState["CurrentTableEx"] = dt;
                }

            }


        }


        //protected void LoadGrid()
        //{
        //    String GetBrId = " select brid from companyview where brid=" + Session["GrdLevel"] + " and brcategory=5 ";

        //    DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(GetBrId);

        //    String Query = "";
        //    if (TblGetBrid.Rows.Count > 0)
        //    {
        //        String brid = "";
        //        for (int i = 0; i < TblGetBrid.Rows.Count; i++)
        //        {
        //            brid += "'" + TblGetBrid.Rows[i]["brid"] + "',";
        //        }
        //        brid = brid.Remove(brid.Length - 1);

        //        Query += " Select num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo, ";
        //        Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason Reason from aoup_LIC_DEF ";
        //        Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid ";
        //       // Query += " where img_lic_document is null ";

        //        DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

        //        if (dtSevikaList.Rows.Count > 0)
        //        {
        //            dtSevikaList.Columns.Add(new DataColumn("ImageByte", typeof(Byte[])));
        //            grdLICDEF.DataSource = dtSevikaList;
        //            ViewState["CurrentTable"] = dtSevikaList;
        //            grdLICDEF.DataBind();
        //            PnlSerch.Visible = true;
        //            BtnSubmit.Visible = true;
        //        }
        //        else
        //        {
        //            grdLICDEF.DataSource = null;
        //            grdLICDEF.DataBind();
        //            PnlSerch.Visible = false;
        //            BtnSubmit.Visible = false;
        //            MessageAlert(" Record Not Found ", "../HomePage/FrmDashboard.aspx");
        //            return;
        //        }
        //    }
        //}
        protected void CheckBoxRequired_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = chkTerms.Checked;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            string DocIds = string.Empty;
            string SevikaId = string.Empty;
            foreach (GridViewRow row in grdLICDEF.Rows)
            {
                System.Web.UI.WebControls.Image DocImg = (System.Web.UI.WebControls.Image)row.FindControl("ImgDoc");
                System.Web.UI.WebControls.Label lblSevikaID = (System.Web.UI.WebControls.Label)row.FindControl("lblSevikaID");
                System.Web.UI.WebControls.TextBox txtRemark = (System.Web.UI.WebControls.TextBox)row.FindControl("txtRemark");

                //if (DocImg.ImageUrl == "../Images/defaultpreview.jpg")
                //{
                //    //DocImg.ImageUrl = ResolveUrl(this.im);
                //    //MessageAlert("|| Please Select File. ||", "");
                //    //DocImg.Focus();
                //    //return;

                // }

                lblSevikaID.Text = row.Cells[1].Text;
                DocIds += row.Cells[0].Text + "~" + lblSevikaID.Text + "~" + txtRemark.Text + "#";
            }

            if (DocIds.Length == 0)
            {
                MessageAlert("|| Please Upload Document. ||", "");
                grdLICDEF.Focus();
                return;

            }

            //for (Int32 i = 0; i < grdLICDEF.Rows.Count; i++)
            //{
            //    System.Web.UI.WebControls.Image ImgDocPDF1 = (System.Web.UI.WebControls.Image)grdLICDEF.Rows[i].FindControl("ImgDocPDF1");

            //    FileInfo F1 = new FileInfo(Server.MapPath(ImgDocPDF1.ImageUrl));


            //    if (ImgDocPDF1.ImageUrl == "")
            //    {
            //        MessageAlert("Please upload document for Sevika - " + grdLICDEF.Rows[i].Cells[2].Text, "");
            //        return;
            //    }
            //}

            objSavikaLIC.UserID = Session["UserId"].ToString();
            //objSavikaLIC.CompID = Convert.ToInt32(Session["GrdLevel"].ToString());
            DocIds = DocIds.Substring(0, DocIds.Length - 1);
            objSavikaLIC.Str = DocIds;

            objSavikaLIC.UpDateLIC();

            if (objSavikaLIC.ErrorCode == -100)
            {

                if (ViewState["CurrentTableR"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTableR"];
                    if (dt.Rows.Count > 0)
                    {
                        AnganwadiLib.Methods.MstMethods.UpdateDocDetails(dt);
                    }
                }
                if (ViewState["CurrentTableD"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTableD"];
                    if (dt.Rows.Count > 0)
                    {
                        AnganwadiLib.Methods.MstMethods.UpdateDocDetails(dt);
                    }
                }
                if (ViewState["CurrentTableRg"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTableRg"];
                    if (dt.Rows.Count > 0)
                    {
                        AnganwadiLib.Methods.MstMethods.UpdateDocDetails(dt);
                    }
                }
                if (ViewState["CurrentTableEx"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTableEx"];
                    if (dt.Rows.Count > 0)
                    {
                        AnganwadiLib.Methods.MstMethods.UpdateDocDetails(dt);
                    }
                }


                MessageAlert(objSavikaLIC.ErrorMessage, "../Transaction/FrmLICDocUploadList.aspx");
            }
            else
            {
                BtnSubmit.Visible = false;
                MessageAlert(objSavikaLIC.ErrorMessage, "");
                return;
            }

        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;
            PnlButton.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;
            PnlButton.Visible = false;

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;
            PnlButton.Visible = false;

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
            }
        }

        protected void ddlBeat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBeat.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();
                //LoadSearch();
                PnlButton.Visible = true;
                PnlSerch.Visible = false;
                BtnSubmit.Visible = false;
            }
        }


        //protected void LoadSearch()
        //{
        //    if (IsPostBack)
        //    {
        //        LoadGrid();
        //        DataTable dt = new DataTable();
        //        if (ViewState["CurrentTable"] != null)
        //        {
        //            dt = (DataTable)ViewState["CurrentTable"];
        //        }
        //        int count = 0;
        //        foreach (GridViewRow dr in grdLICDEF.Rows)
        //        {
        //            count++;
        //            FileUpload FileUploadDoc = (FileUpload)dr.FindControl("FileUploadDoc");
        //            System.Web.UI.WebControls.Image ImgDoc = (System.Web.UI.WebControls.Image)dr.FindControl("ImgDoc");

        //            if (IsPostBack && FileUploadDoc.PostedFile != null)
        //            {
        //                if (FileUploadDoc.PostedFile.FileName.Length > 0)
        //                {

        //                    HttpPostedFile PFileThumb = FileUploadDoc.PostedFile;
        //                    int lenthThumb = PFileThumb.ContentLength;
        //                    int KB = (lenthThumb / 2048000) + 1;

        //                    if (KB > 300)
        //                    {
        //                        MessageAlert("Image size of document can not be more than 2MB", "");
        //                        return;
        //                    }
        //                    Byte[] PropimageBytes = new byte[lenthThumb];
        //                    PFileThumb.InputStream.Read(PropimageBytes, 0, lenthThumb);

        //                    String strFilenameoc = Path.GetFileName(PFileThumb.FileName);
        //                    strFilenameoc = count + "_" + System.DateTime.Now.Date.ToString("ddMMyyyymmhhss") + "_" + strFilenameoc;
        //                    String filePath = Server.MapPath("..\\ImageGarbage\\") + strFilenameoc;
        //                    FileUploadDoc.SaveAs(filePath);
        //                    ImgDoc.Visible = true;
        //                    ImgDoc.ImageUrl = "..\\ImageGarbage\\" + strFilenameoc;
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        dt.Rows[count - 1]["ImageByte"] = PropimageBytes;
        //                    }
        //                }
        //            }

        //        }
        //        ViewState["CurrentTable"] = dt;
        //    }
        //}
        protected void LoadGridDeath()
        {
            String GetBrId = " select brid from companyview where brid=" + Session["GrdLevel"] + " and brcategory=5 ";

            DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(GetBrId);

            String Query = "";
            if (TblGetBrid.Rows.Count > 0)
            {
                String brid = "";
                for (int i = 0; i < TblGetBrid.Rows.Count; i++)
                {
                    brid += "'" + TblGetBrid.Rows[i]["brid"] + "',";
                }
                brid = brid.Remove(brid.Length - 1);

                Query += " Select num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo, ";
                Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason Reason,var_lic_licapprovreason RejectReason, ";
                Query += " var_lic_approvflag,var_lic_dpoapprovflag,var_lic_hoapprovflag,var_lic_licapprovflag  FROM aoup_lic_def ";
                Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid ";
                Query += " where var_lic_flag='DT' and var_lic_flag_insert = 'I'  and img_lic_document is null and num_lic_compid in (" + brid + ") ";
                // Query += " and var_sevikamaster_authorizedby is not null and date_sevikamaster_authdate is not null ";
                //Query += " and var_lic_licapprovby is not null and date_lic_licapprovdate is not null ";
                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    dtSevikaList.Columns.Add(new DataColumn("ImageByte", typeof(Byte[])));
                    grdLICDEF.DataSource = dtSevikaList;
                    ViewState["CurrentTableD"] = dtSevikaList;
                    grdLICDEF.DataBind();

                    for (int i = 0; i < dtSevikaList.Rows.Count; i++)
                    {
                        if (dtSevikaList.Rows[i]["var_lic_approvflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList.Rows[i]["var_lic_dpoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList.Rows[i]["var_lic_hoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList.Rows[i]["var_lic_licapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else { grdLICDEF.Columns[6].Visible = false; }
                    }
                    PnlSerch.Visible = true;
                    BtnSubmit.Visible = true;
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                    PnlSerch.Visible = false;
                    BtnSubmit.Visible = false;
                    MessageAlert(" Record Not Found ", "../HomePage/FrmDashboard.aspx");
                    return;
                }
            }
        }
        protected void LoadGridRetirement()
        {
            String GetBrId = " select brid from companyview where brid=" + Session["GrdLevel"] + " and brcategory=5 ";

            DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(GetBrId);

            String Query = "";
            if (TblGetBrid.Rows.Count > 0)
            {
                String brid = "";
                for (int i = 0; i < TblGetBrid.Rows.Count; i++)
                {
                    brid += "'" + TblGetBrid.Rows[i]["brid"] + "',";
                }
                brid = brid.Remove(brid.Length - 1);

                Query += " Select num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo, ";
                Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason Reason,var_lic_licapprovreason RejectReason, ";
                Query += " var_lic_approvflag,var_lic_dpoapprovflag,var_lic_hoapprovflag,var_lic_licapprovflag  FROM aoup_lic_def ";
                Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid ";
                Query += " where var_lic_flag='RT' and var_lic_flag_insert = 'I' and img_lic_document is null and  num_lic_compid in (" + brid + ") ";
                Query += " and var_sevikamaster_authorizedby is not null and date_sevikamaster_authdate is not null ";
                //Query += " and var_lic_licapprovby is not null and date_lic_licapprovdate is not null ";

                DataTable dtSevikaList2 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList2.Rows.Count > 0)
                {
                    dtSevikaList2.Columns.Add(new DataColumn("ImageByte", typeof(Byte[])));
                    grdLICDEF.DataSource = dtSevikaList2;
                    ViewState["CurrentTableR"] = dtSevikaList2;
                    grdLICDEF.DataBind();
                    grdLICDEF.DataBind();

                    for (int i = 0; i < dtSevikaList2.Rows.Count; i++)
                    {
                        if (dtSevikaList2.Rows[i]["var_lic_approvflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList2.Rows[i]["var_lic_dpoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList2.Rows[i]["var_lic_hoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList2.Rows[i]["var_lic_licapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else { grdLICDEF.Columns[6].Visible = false; }
                    }

                    PnlSerch.Visible = true;
                    BtnSubmit.Visible = true;
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                    PnlSerch.Visible = false;
                    BtnSubmit.Visible = false;
                    MessageAlert(" Record Not Found ", "../HomePage/FrmDashboard.aspx");
                    return;
                }
            }
        }
        protected void LoadGridResignation()
        {
            String GetBrId = " select brid from companyview where brid=" + Session["GrdLevel"] + " and brcategory=5 ";

            DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(GetBrId);

            String Query = "";
            if (TblGetBrid.Rows.Count > 0)
            {
                String brid = "";
                for (int i = 0; i < TblGetBrid.Rows.Count; i++)
                {
                    brid += "'" + TblGetBrid.Rows[i]["brid"] + "',";
                }
                brid = brid.Remove(brid.Length - 1);

                Query += " Select num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo, ";
                Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason Reason,var_lic_licapprovreason RejectReason, ";
                Query += " var_lic_approvflag,var_lic_dpoapprovflag,var_lic_hoapprovflag,var_lic_licapprovflag  FROM aoup_lic_def ";
                Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid ";
                Query += " where var_lic_flag_insert = 'I' and var_lic_flag In ('RS','TE')  and img_lic_document is null and  num_lic_compid in (" + brid + ") ";
                Query += " and var_sevikamaster_authorizedby is not null and date_sevikamaster_authdate is not null ";
                //Query += " and var_lic_licapprovby is not null and date_lic_licapprovdate is not null ";

                DataTable dtSevikaList3 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList3.Rows.Count > 0)
                {
                    dtSevikaList3.Columns.Add(new DataColumn("ImageByte", typeof(Byte[])));
                    grdLICDEF.DataSource = dtSevikaList3;
                    ViewState["CurrentTableRg"] = dtSevikaList3;
                    grdLICDEF.DataBind();
                    grdLICDEF.DataBind();

                    for (int i = 0; i < dtSevikaList3.Rows.Count; i++)
                    {
                        if (dtSevikaList3.Rows[i]["var_lic_approvflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList3.Rows[i]["var_lic_dpoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList3.Rows[i]["var_lic_hoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList3.Rows[i]["var_lic_licapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else { grdLICDEF.Columns[6].Visible = false; }
                    }
                    PnlSerch.Visible = true;
                    BtnSubmit.Visible = true;
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                    PnlSerch.Visible = false;
                    BtnSubmit.Visible = false;
                    MessageAlert(" Record Not Found ", "../HomePage/FrmDashboard.aspx");
                    return;
                }
            }
        }
        protected void LoadExternal()
        {
            String GetBrId = " select brid from companyview where brid=" + Session["GrdLevel"] + " and brcategory=5 ";

            DataTable TblGetBrid = (DataTable)MstMethods.Query2DataTable.GetResult(GetBrId);

            String Query = "";
            if (TblGetBrid.Rows.Count > 0)
            {
                String brid = "";
                for (int i = 0; i < TblGetBrid.Rows.Count; i++)
                {
                    brid += "'" + TblGetBrid.Rows[i]["brid"] + "',";
                }
                brid = brid.Remove(brid.Length - 1);

                //Query += " Select num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo, ";
                //Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason Reason from aoup_LIC_DEF ";
                //Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid ";
                //Query += " where var_lic_flag_insert='E'  and img_lic_document is null and  num_lic_compid in (" + brid + ") ";
                //Query += " and var_sevikamaster_authorizedby is not null and date_sevikamaster_authdate is not null ";
                ////Query += " and var_lic_licapprovby is not null and date_lic_licapprovdate is not null ";

                Query += " SELECT num_lic_compid bit, num_lic_sevikaid SevikaID, var_licsevika_name SevikaName, var_licsevika_aadharno AadharNo, ";
                Query += " TRUNC (date_lic_exitdate) ExitDate, var_licsevika_remark Reason,var_lic_licapprovreason RejectReason, ";
                Query += " var_lic_approvflag,var_lic_dpoapprovflag,var_lic_hoapprovflag,var_lic_licapprovflag  FROM aoup_lic_def ";
                Query += " INNER JOIN aoup_licsevika_def ON num_licsevika_sevikaid = num_lic_sevikaid ";
                Query += " WHERE var_lic_flag_insert = 'E' AND img_lic_document IS NULL  AND num_lic_compid IN (" + brid + ") ";

                DataTable dtSevikaList3 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList3.Rows.Count > 0)
                {
                    dtSevikaList3.Columns.Add(new DataColumn("ImageByte", typeof(Byte[])));
                    grdLICDEF.DataSource = dtSevikaList3;
                    ViewState["CurrentTableEx"] = dtSevikaList3;
                    grdLICDEF.DataBind();

                    for (int i = 0; i < dtSevikaList3.Rows.Count; i++)
                    {
                        if (dtSevikaList3.Rows[i]["var_lic_approvflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList3.Rows[i]["var_lic_dpoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList3.Rows[i]["var_lic_hoapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else if (dtSevikaList3.Rows[i]["var_lic_licapprovflag"].ToString() == "R")
                        {
                            grdLICDEF.Columns[6].Visible = true;
                        }
                        else { grdLICDEF.Columns[6].Visible = false; }
                    }
                    PnlSerch.Visible = true;
                    BtnSubmit.Visible = true;
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                    PnlSerch.Visible = false;
                    BtnSubmit.Visible = false;
                    MessageAlert(" Record Not Found ", "../HomePage/FrmDashboard.aspx");
                    return;
                }
            }
        }
        protected void BtnDeath_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Death";
                LoadGridDeath();
                FileUploadSaveImage();

                //DataTable dt = new DataTable();
                //if (ViewState["CurrentTableD"] != null)
                //{
                //    dt = (DataTable)ViewState["CurrentTableD"];
                //}
                //int count = 0;
                //foreach (GridViewRow dr in grdLICDEF.Rows)
                //{
                //    count++;
                //    FileUpload FileUploadDoc1 = (FileUpload)dr.FindControl("FileUploadDoc");
                //    System.Web.UI.WebControls.Image ImgDoc = (System.Web.UI.WebControls.Image)dr.FindControl("ImgDoc");

                //    if (IsPostBack && FileUploadDoc1.PostedFile != null)
                //    {
                //        if (FileUploadDoc1.PostedFile.FileName.Length > 0)
                //        {

                //            HttpPostedFile PFileThumb = FileUploadDoc1.PostedFile;
                //            int lenthThumb = PFileThumb.ContentLength;
                //            int KB = (lenthThumb / 2048000) + 1;

                //            if (KB > 300)
                //            {
                //                MessageAlert("Image size of document can not be more than 2MB", "");
                //                return;
                //            }
                //            Byte[] PropimageBytes = new byte[lenthThumb];
                //            PFileThumb.InputStream.Read(PropimageBytes, 0, lenthThumb);

                //            String strFilenameoc = Path.GetFileName(PFileThumb.FileName);
                //            strFilenameoc = count + "_" + System.DateTime.Now.Date.ToString("ddMMyyyymmhhss") + "_" + strFilenameoc;
                //            String filePath = Server.MapPath("..\\ImageGarbage\\") + strFilenameoc;
                //            FileUploadDoc1.SaveAs(filePath);
                //            ImgDoc.Visible = true;
                //            ImgDoc.ImageUrl = "..\\ImageGarbage\\" + strFilenameoc;
                //            if (dt.Rows.Count > 0)
                //            {
                //                dt.Rows[count - 1]["ImageByte"] = PropimageBytes;
                //            }
                //        }
                //    }

                //}
                //ViewState["CurrentTableD"] = dt;
            }
            PnlSerch.Visible = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Enabled = false;
            chkTerms_CheckedChanged(null, null);
        }
        protected void BtnRetirement_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Retire";
                LoadGridRetirement();
                FileUploadSaveImage();
                //DataTable dt = new DataTable();
                //if (ViewState["CurrentTableR"] != null)
                //{
                //    dt = (DataTable)ViewState["CurrentTableR"];
                //}
                //int count = 0;
                //foreach (GridViewRow dr in grdLICDEF.Rows)
                //{
                //    count++;
                //    FileUpload FileUploadDoc2 = (FileUpload)dr.FindControl("FileUploadDoc");
                //    System.Web.UI.WebControls.Image ImgDoc = (System.Web.UI.WebControls.Image)dr.FindControl("ImgDoc");

                //    if (IsPostBack && FileUploadDoc2.PostedFile != null)
                //    {
                //        if (FileUploadDoc2.PostedFile.FileName.Length > 0)
                //        {

                //            HttpPostedFile PFileThumb = FileUploadDoc2.PostedFile;
                //            int lenthThumb = PFileThumb.ContentLength;
                //            int KB = (lenthThumb / 2048000) + 1;

                //            if (KB > 300)
                //            {
                //                MessageAlert("Image size of document can not be more than 2MB", "");
                //                return;
                //            }
                //            Byte[] PropimageBytes = new byte[lenthThumb];
                //            PFileThumb.InputStream.Read(PropimageBytes, 0, lenthThumb);

                //            String strFilenameoc = Path.GetFileName(PFileThumb.FileName);
                //            strFilenameoc = count + "_" + System.DateTime.Now.Date.ToString("ddMMyyyymmhhss") + "_" + strFilenameoc;
                //            String filePath = Server.MapPath("..\\ImageGarbage\\") + strFilenameoc;
                //            FileUploadDoc2.SaveAs(filePath);
                //            ImgDoc.Visible = true;
                //            ImgDoc.ImageUrl = "..\\ImageGarbage\\" + strFilenameoc;
                //            if (dt.Rows.Count > 0)
                //            {
                //                dt.Rows[count - 1]["ImageByte"] = PropimageBytes;
                //            }
                //        }
                //    }

                //}
                //ViewState["CurrentTableR"] = dt;
            }
            PnlSerch.Visible = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Enabled = false;
            chkTerms_CheckedChanged(null, null);

        }
        protected void BtnResignation_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Resign";
                LoadGridResignation();
                FileUploadSaveImage();

                //DataTable dt = new DataTable();
                //if (ViewState["CurrentTableRg"] != null)
                //{
                //    dt = (DataTable)ViewState["CurrentTableRg"];
                //}
                //int count = 0;
                //foreach (GridViewRow dr in grdLICDEF.Rows)
                //{
                //    count++;
                //    FileUpload FileUploadDoc3 = (FileUpload)dr.FindControl("FileUploadDoc");
                //    System.Web.UI.WebControls.Image ImgDoc = (System.Web.UI.WebControls.Image)dr.FindControl("ImgDoc");

                //    if (IsPostBack && FileUploadDoc3.PostedFile != null)
                //    {
                //        if (FileUploadDoc3.PostedFile.FileName.Length > 0)
                //        {

                //            HttpPostedFile PFileThumb = FileUploadDoc3.PostedFile;
                //            int lenthThumb = PFileThumb.ContentLength;
                //            int KB = (lenthThumb / 2048000) + 1;

                //            if (KB > 300)
                //            {
                //                MessageAlert("Image size of document can not be more than 2MB", "");
                //                return;
                //            }
                //            Byte[] PropimageBytes = new byte[lenthThumb];
                //            PFileThumb.InputStream.Read(PropimageBytes, 0, lenthThumb);

                //            String strFilenameoc = Path.GetFileName(PFileThumb.FileName);
                //            strFilenameoc = count + "_" + System.DateTime.Now.Date.ToString("ddMMyyyymmhhss") + "_" + strFilenameoc;
                //            String filePath = Server.MapPath("..\\ImageGarbage\\") + strFilenameoc;
                //            FileUploadDoc3.SaveAs(filePath);
                //            ImgDoc.Visible = true;
                //            ImgDoc.ImageUrl = "..\\ImageGarbage\\" + strFilenameoc;
                //            if (dt.Rows.Count > 0)
                //            {
                //                dt.Rows[count - 1]["ImageByte"] = PropimageBytes;
                //            }
                //        }
                //    }

                //}
                //ViewState["CurrentTableRg"] = dt;
            }
            PnlSerch.Visible = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Enabled = false;
            chkTerms_CheckedChanged(null, null);
        }
        protected void BtnExternal_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "External";
                LoadExternal();
                // FileUploadSaveImage();
            }
            PnlSerch.Visible = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Enabled = false;
            chkTerms_CheckedChanged(null, null);
        }
        protected void grdLICDEF_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdLICDEF.SelectedRow;
            Session["SevikaID"] = row.Cells[1].Text;

            if (ViewState["Action"] != null)
            {
                if (ViewState["Action"].ToString() == "External")
                {
                    Session["SevikaType"] = "External";
                }
                else
                { Session["SevikaType"] = "Internal"; }
            }
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "redirectToNewWindow();", true);
            //Response.Redirect("../Transaction/FrmLICDocUploadMst.aspx");
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

        //protected void lnkViewDetails_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = grdLICDEF.SelectedRow;
        //    Session["SevikaID"] = row.Cells[1].Text;
        //    Response.Write("<script>");
        //    Response.Write("window.open('../Transaction/FrmLICDocUploadMst.aspx?SevikaID='" + Session["SevikaID"] + "'','_blank')");
        //    Response.Write("</script>");
        //}
    }
}