using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace AnganwadiWebApp.Admin
{
    public partial class FrmPayDetailsUploadLogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
               
            }
            pnlTxtLog.Visible = false;
            pnlDbLogs.Visible = false;
            if (Session["FileUploadLogType"] != null)
            {
                if (Session["FileUploadLogType"].ToString() == "TextFileUploadlogs")
                {
                    LoadUploadedFiles(GrdLogList);
                    pnlTxtLog.Visible = true;

                }
                else if (Session["FileUploadLogType"].ToString() == "DBFileUploadlogs")
                {
                    GetdBErrorLogs();
                    pnlDbLogs.Visible = true;
                }
            }
            
        }
        public void LoadUploadedFiles(GridView gv)
        {
            String Path = HttpContext.Current.Server.MapPath("//UploadFiles//Payment Details//Logs//");
            DataTable dtFiles = GetFilesInDirectory(Path, Session["UserId"].ToString());
            gv.DataSource = dtFiles;
            gv.DataBind();

            if (dtFiles.Rows.Count == 0)
            {
                lblHead.Text = "No Files Uploaded";
            }
        }

        public void GetdBErrorLogs()
        {
            string Query = "select  * from aoup_paydetails_errorlog";
            DataTable dtBranchList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
            if (dtBranchList.Rows.Count > 0)
            {              
                GrdDBLogs.DataSource = dtBranchList;
                GrdDBLogs.DataBind();
            }
            else
            {
                GrdDBLogs.DataSource = null;
                GrdDBLogs.DataBind();
                //MessageAlert("No record found", "");
            }

        }
        public DataTable GetFilesInDirectory(string sourcePath, String BrId)
        {
            System.Data.DataTable dtFiles = new System.Data.DataTable();
            if ((Directory.Exists(sourcePath)))
            {
                dtFiles.Columns.Add(new System.Data.DataColumn("Name"));
                dtFiles.Columns.Add(new System.Data.DataColumn("Link"));
                DirectoryInfo dir = new DirectoryInfo(sourcePath);
                String FilePathe = BrId + "*.log";
                foreach (FileInfo files in dir.GetFiles(FilePathe))
                {
                    System.Data.DataRow drFile = dtFiles.NewRow();
                    String FileName = files.Name;
                    FileName = FileName.Substring(5, FileName.Length - 5);
                    FileName = FileName.Substring(0, FileName.Length - 4);
                    drFile["Name"] = FileName;
                    drFile["Link"] = sourcePath + files.Name;

                    dtFiles.Rows.Add(drFile);
                }
            }
            return dtFiles;
        }

        protected void GrdLogList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;

            }
        }

        protected void GrdLogList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow row = GrdLogList.SelectedRow;
            String FilePath = row.Cells[2].Text;

            StreamReader sReader = new StreamReader(FilePath);
            String txtFile = "";

            while (sReader.Peek() >= 0)
            {
                txtFile += sReader.ReadLine() + "\r\n";
            }

            sReader.Close();

            TextBox1.Text = txtFile;

            lblHead.Text = row.Cells[1].Text;
        }        
    }
}