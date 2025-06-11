using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using AnganwadiLib.Methods;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using AnganwadiLib.Business;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Admin
{
    public partial class FrmPayDetailsUpload : System.Web.UI.Page
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Registerd Savika Report";
            if (!IsPostBack)
            {
                divLog.Visible = false;
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {

                string path = Server.MapPath("PayDetails.xlsx");
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/vnd.xls";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                else
                {
                    Response.Write("This file does not exist.");
                }
            }
            catch (Exception ex)
            {
               // MessageAlert(ex.Message, "");
                return;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String PathTrns = Server.MapPath("~\\UploadFiles\\Payment Details") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");
            String SuccessPathTrns = Server.MapPath("~\\UploadFiles\\Payment Details\\Success\\") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");
            String FailedPathTrns = Server.MapPath("~\\UploadFiles\\Payment Details\\Failed\\") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");
            String logPathTrns = Server.MapPath("~\\UploadFiles\\Payment Details\\Logs\\") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");


            //String PathTrns = Server.MapPath("//UploadFiles//Payment Details") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");
            //String SuccessPathTrns = Server.MapPath("//UploadFiles//Payment Details//Success//") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");
            //String FailedPathTrns = Server.MapPath("//UploadFiles//Payment Details//Failed//") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");
            //String logPathTrns = Server.MapPath("//UploadFiles//Payment Details//Logs//") + "\\" + Session["UserId"].ToString() + "_Payment_Details_" + System.DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt");

            if (!FileUpload.HasFile)
            {
               // MessageAlert("Please select valid file to upload", "");
                return;
            }
            try
            {

               

                FileUpload.SaveAs(PathTrns + ".txt");
                string Result = "";
                String DataSource = (String)MstMethods.DataSource.GetDateSource();
                String UserId = (String)MstMethods.DataSource.GetUserId();
                String Password = (String)MstMethods.DataSource.GetPassword();
                MstMethods.DataUpload.DeleteBeforeUploadFile(0, "aoup_paydetails_rawdef", "");
                Result = Convert.ToString(MstMethods.DataUpload.Upload(PathTrns + ".txt", logPathTrns + "_Ctrl.txt", logPathTrns + ".log", "", DataSource, UserId, Password, "aoup_paydetails_rawdef"));

                if (Result != "0")
                {
                    StreamReader sReader = new StreamReader(PathTrns + ".log");
                    String txtFile = "";

                    while (sReader.Peek() >= 0)
                    {
                        txtFile += sReader.ReadLine() + "\r\n";
                    }

                    sReader.Close();

                    ArrayList arr = new ArrayList(txtFile.Split(new string[] { "Record " }, StringSplitOptions.None));

                    txtFile = "=======================================\r\n  Unable to upload data. Check below log file.\r\n=======================================\r\n\r\n";

                    for (int i = 0; i < arr.Count; i++)
                    {
                        if (i > 0)
                        {
                            txtFile += "Record " + arr[i].ToString();
                        }
                    }

                    txtLog.Text = txtFile;
                    txtLog.Visible = true;
                    divLog.Visible = true;
                    return;
                }
                else if (Result == "0")
                {
                    Int32 ErrCode = 0;
                    String ErrMsg = "";
                    MstMethods.DataUpload.TempToFinale("aoup_paydetails_uploadins",0, Session["UserId"].ToString(), out ErrCode, out ErrMsg);
                    if (ErrCode == -100)
                    {
                        File.Move(PathTrns+".txt", SuccessPathTrns+".txt");
                       MessageAlert(ErrMsg, "");
                        return;
                    }
                    else
                    {
                        File.Move(PathTrns+".txt", FailedPathTrns+".txt");
                       MessageAlert(ErrMsg, "");
                        return;
                    }

                }
           
            }
            catch (Exception ex)
            {
                File.Move(PathTrns + ".txt", FailedPathTrns + ".txt");
              //  MessageAlert(ex.Message, "");
                return;
            }

        }

        protected void LinkLog_Click(object sender, EventArgs e)
        {
            Session["FileUploadLogType"] = "TextFileUploadlogs";
            string str = "window.open('../Admin/FrmPayDetailsUploadLogs.aspx', '', 'toolbars=no, menubar=no, location=left, scrollbars=yes, resizable=no, status=yes, width=1000px, heights=200');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, str, true);
            return;
        }
        protected void LinkDBLog_Click(object sender, EventArgs e)
        {
            Session["FileUploadLogType"] = "DBFileUploadlogs";
            string str = "window.open('../Admin/FrmPayDetailsUploadLogs.aspx', '', 'toolbars=no, menubar=no, location=left, scrollbars=yes, resizable=no, status=yes, width=1000px, heights=200');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, str, true);
            return;
        }
    }
}