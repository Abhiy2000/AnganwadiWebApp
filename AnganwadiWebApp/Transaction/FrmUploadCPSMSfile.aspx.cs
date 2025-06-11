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

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmUploadCPSMSfile : System.Web.UI.Page
    {
        BoCPSMS objcpsms = new BoCPSMS();
        string str = "", FileName="";

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

        string DATA_LOG = "C://ICDS_log/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Registerd Savika Report";
            if (!IsPostBack)
            {
                btnUpdate.Visible = false;
            }
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (upldCPSMS.HasFile)
            {
                string strFileName = Path.GetFileName(upldCPSMS.PostedFile.FileName);
                string strExtension = Path.GetExtension(strFileName);

                write_log_error("FrmUploadCPSMSfile.aspx", "Error BtnUpload strFileName : " + strFileName);

                if (strExtension == ".xls" || strExtension == ".xlsx")
                {
                    FileName = Path.GetFileName(upldCPSMS.PostedFile.FileName);
                    ViewState["FileName"] = FileName;
                    string Extension = Path.GetExtension(upldCPSMS.PostedFile.FileName);
                    string FolderPath = Server.MapPath("../UploadFiles/" + FileName);
                    write_log_error("FrmUploadCPSMSfile.aspx", "Error BtnUpload FolderPath : " + FolderPath);

                    //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    //string FilePath = Server.MapPath(FolderPath);
                    upldCPSMS.SaveAs(FolderPath);
                    Import_To_Grid(FolderPath, Extension, "YES");
                    lblmsg.Text = "Upload status: File uploaded.";

                    write_log_error("FrmUploadCPSMSfile.aspx", "File Upload Success.");
                }
                else
                {
                    GrdList.Visible = false;
                    btnUpdate.Visible = false;
                    lblmsg.Text = "Upload status: only .xls and .xlsx file are allowed!";
                    return;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GrdList.Rows.Count; i++)
            {
                Label lblSevikaCode = (Label)GrdList.Rows[i].FindControl("lblSevikaCode");
                Label lblCPSMS = (Label)GrdList.Rows[i].FindControl("lblCPSMS");
                if (lblCPSMS.Text == "")
                {
                    MessageAlert(" PFMS Code can not be blank ", "");
                    return;
                }
                else if (lblCPSMS.Text.Length > 20)
                {
                    MessageAlert(" PFMS Code length can not greater than 20 ", "");
                    return;
                }
                str += lblSevikaCode.Text.Trim() + "#" + lblCPSMS.Text.Trim() + "$";
            }
            if (str != "")
            {
                str = str.Substring(0, str.Length - 1);
            }
            objcpsms.UserId = Session["UserId"].ToString();
            objcpsms.FileName = ViewState["FileName"].ToString();
            objcpsms.Str = str;
            objcpsms.BoCPSMS_1();
            if (objcpsms.ErrorCode == -100)
            {
                MessageAlert(objcpsms.ErrorMsg, "../Transaction/FrmUploadCPSMSfile.aspx");
                return;
            }
            else
            {
                MessageAlert(objcpsms.ErrorMsg, "");
                GrdList.Visible = false;
                btnUpdate.Visible = false;
                return;
            }
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn c in dt.Columns)
                {
                    c.ColumnName = String.Join("_", c.ColumnName.Split());
                }
            }
            connExcel.Close();

            //Bind Data to GridView
            // GrdList.Caption = Path.GetFileName(FilePath);
            GrdList.DataSource = dt;
            GrdList.DataBind();
            GrdList.Visible = true;
            btnUpdate.Visible = true;
        }

        protected void GrdList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {

                string path = Server.MapPath("UploadPFMSCodeFormat.xlsx");
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
                MessageAlert(ex.Message, "");
                return;
            }

        }

        public void write_log_error(string sourceObject, string description)
        {
            StreamWriter sw = null;

            try
            {
                DirectoryInfo dir = new DirectoryInfo(DATA_LOG);

                if (dir.Exists)
                {
                    string filePath = DATA_LOG + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = filePath + "Error.log";

                    sw = new StreamWriter(fileName, true);
                    DateTime dtNow = DateTime.Now;
                    sw.WriteLine("-----------------------------------------------------");
                    sw.WriteLine(dtNow.ToString("dd-MM-yyyy HH:mm:ss") + " " + sourceObject + " " + description);
                    sw.Flush();
                    sw.Dispose();
                }
            }

            catch (Exception Ex)
            {
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }
    }
}