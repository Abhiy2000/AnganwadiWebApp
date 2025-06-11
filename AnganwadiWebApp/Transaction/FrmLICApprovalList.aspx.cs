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
    public partial class FrmLICApprovalList : System.Web.UI.Page
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
               
                Session["GrdLevel"].ToString();
                //LoadGrid();
                PnlSerch.Visible = false;

            }
        }

        protected void LoadGrid()
        {
            String GetBrId = " select brid from companyview where hoid=" + Session["GrdLevel"] + "  ";

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

                Query += " Select divname,distname,cdponame,bitbitname,num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo,  ";
                Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason ExitReason,num_lic_payscal LastSalary,num_lic_licamount ClaimAmount,var_lic_flag_insert Flag, img_lic_document DocImage, '' OLDTSOFTWARENO  from aoup_LIC_DEF  ";
                Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid  ";
                Query += " inner join corpinfo on bitid=num_lic_compid ";
                Query += " where var_lic_flag_insert='I' and img_lic_document is not null and var_lic_hoapprovby is not null and var_lic_licapprovby is null ";
                Query += "  and var_lic_hoapprovflag='A' ";

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
                    //else if (ViewState["Action"].ToString() == "External")
                    //{
                    //    Query += " and var_lic_flag_insert='E' ";
                    //}
                }
               
                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    grdLICDEF.DataSource = dtSevikaList;
                    grdLICDEF.DataBind();
                    //BtnSubmit.Visible = true;
                    PnlSerch.Visible = true;

                    for (int i = 0; i < dtSevikaList.Rows.Count; i++)
                    {
                        if (dtSevikaList.Rows[i]["DocImage"] != DBNull.Value)
                        {
                            Byte[] imageBytes = (Byte[])dtSevikaList.Rows[i]["DocImage"];
                            (grdLICDEF.Rows[i].FindControl("lnkBtnViewDoc") as LinkButton).Text = "View";
                        }
                    }

                    ViewState["CurrentTable"] = dtSevikaList;
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                   // BtnSubmit.Visible = false;
                    PnlSerch.Visible = false;
                    MessageAlert(" Record Not Found ", "");
                    return;
                }
            }
        }

        protected void ExternalLoadGrid()
        {
            String GetBrId = " select brid from companyview where hoid=" + Session["GrdLevel"] + "  ";

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

                //Query += " Select divname,distname,cdponame,bitbitname,num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo,  ";
                //Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason ExitReason,num_lic_payscal LastSalary,num_lic_licamount ClaimAmount,var_lic_flag_insert Flag  from aoup_LIC_DEF  ";
                //Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid  ";
                //Query += " inner join corpinfo on bitid=num_lic_compid ";
                //Query += " where img_lic_document is not null and var_lic_hoapprovby is not null and var_lic_licapprovby is null ";
                //Query += "  and var_lic_hoapprovflag='A' ";

                Query += " SELECT divname, distname, cdponame, bitbitname, num_lic_compid bit, num_lic_sevikaid sevikaid, var_licsevika_name sevikaname,var_licsevika_aadharno aadharno, ";
                Query += " TRUNC(date_lic_exitdate) exitdate,var_licsevika_remark exitreason, num_lic_payscal lastsalary,num_lic_licamount claimamount,var_lic_flag_insert Flag, ";
                Query += " img_lic_document DocImage,VAR_LICSEVIKA_OLDTSOFTWARENO OLDTSOFTWARENO from aoup_LIC_DEF ";
                Query += " INNER JOIN aoup_licsevika_def ON num_licsevika_sevikaid = num_lic_sevikaid ";
                Query += " INNER JOIN corpinfo ON bitid = num_lic_compid ";
                Query += " where img_lic_document is not null and var_lic_hoapprovby is not null and var_lic_licapprovby is null ";
                Query += " and var_lic_hoapprovflag='A' ";

                if (ViewState["Action"] != null)
                {
                    if (ViewState["Action"].ToString() == "External")
                    {
                        Query += " and var_lic_flag_insert='E' ";
                    }
                }

                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    grdLICDEF.DataSource = dtSevikaList;
                    grdLICDEF.DataBind();
                    //BtnSubmit.Visible = true;
                    PnlSerch.Visible = true;

                    for (int i = 0; i < dtSevikaList.Rows.Count; i++)
                    {
                        if (dtSevikaList.Rows[i]["DocImage"] != DBNull.Value)
                        {
                            Byte[] imageBytes = (Byte[])dtSevikaList.Rows[i]["DocImage"];
                            (grdLICDEF.Rows[i].FindControl("lnkBtnViewDoc") as LinkButton).Text = "View";
                        }
                    }

                    ViewState["CurrentTable"] = dtSevikaList;
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                    // BtnSubmit.Visible = false;
                    PnlSerch.Visible = false;
                    MessageAlert(" Record Not Found ", "");
                    return;
                }
            }
        }

        protected void BtnDeath_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Death";
                LoadGrid();
            }
        }
        protected void BtnRetirement_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Retire";
                LoadGrid();
            }
        }
        protected void BtnResignation_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "Resign";
                LoadGrid();
            }
        }
        protected void BtnExternal_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ViewState["Action"] = "External";
                ExternalLoadGrid();
            }
        }


        protected void grdLICDEF_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowindex = grdLICDEF.SelectedRow.RowIndex;
         
            Session["SevikaId"] = (grdLICDEF.Rows[rowindex].FindControl("lblSevikaID") as Label).Text; 
            // Session["SevikaId"] = Convert.ToInt32(grdLICDEF.Rows[rowindex].Cells[5].Text);
            Session["ImageDoc"] = (Image)grdLICDEF.Rows[rowindex].Cells[10].FindControl("ImgDoc");
            Session["Flag"]= grdLICDEF.Rows[rowindex].Cells[13].Text;
            
            //Response.Redirect("../Transaction/FrmLICApproval.aspx");
            //Response.Redirect("../Transaction/FrmLICApproveMst.aspx");
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "redirectToNewWindow();", true);
        }
        protected void grdLICDEF_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            //{
            //    e.Row.Cells[11].Visible = false;

            //}
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnDownload = (HiddenField)grdLICDEF.Rows[rindex].FindControl("hdnDownload");
                if (hdnDownload.Value != "0")
                {
                    DownloadFile(Convert.ToInt32(hdnDownload.Value));
                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }
        }
        public void DownloadFile(int SevikaID)
        {
            string Query = " Select img_lic_document DocImage from aoup_LIC_DEF ";
            Query += " where img_lic_document is not null and  var_lic_hoapprovby is not null and var_lic_licapprovby is null  ";
            Query += " and var_lic_hoapprovflag='A' and num_lic_sevikaid='" + SevikaID + "' ";

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
                    Label lblSevikaID = (Label)grdLICDEF.Rows[RowIndex].FindControl("lblSevikaID");
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
    }
}