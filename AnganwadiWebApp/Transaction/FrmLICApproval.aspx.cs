using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using AnganwadiLib.Business;
using System.IO;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmLICApproval : System.Web.UI.Page
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
                LoadGrid();

            }
        }

        protected void LoadGrid()
        {
            
                String Query = "";
                Query += " Select divname,distname,cdponame,bitbitname,num_lic_compid Bit,num_lic_sevikaid SevikaID,var_sevikamaster_name SevikaName,var_sevikamaster_aadharno AadharNo,  ";
                Query += " trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason ExitReason,img_lic_document DocImage,num_lic_payscal LastSalary,num_lic_licamount ClaimAmount from aoup_LIC_DEF  ";
                Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid  ";
                Query += " inner join corpinfo on bitid=num_lic_compid ";
                Query += " where img_lic_document is not null and var_lic_hoapprovby is not null and var_lic_licapprovby is null ";
                Query += " and num_lic_sevikaid='" + Convert.ToInt32(Session["SevikaId"]) + "'";

                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    grdLICDEF.DataSource = dtSevikaList;
                    ViewState["CurrentTable"] = dtSevikaList;
                    grdLICDEF.DataBind();
                    BtnSubmit.Visible = true;

                    for (int i = 0; i < dtSevikaList.Rows.Count; i++)
                    {
                        Image ImgDoc = (Image)grdLICDEF.Rows[i].FindControl("ImgDoc");
                        if (dtSevikaList.Rows[i]["DocImage"] != DBNull.Value)
                        {
                            Byte[] PropimageBytes = (Byte[])dtSevikaList.Rows[i]["DocImage"];
                            ImgDoc.ImageUrl = "data:image;base64," + Convert.ToBase64String(PropimageBytes);
                        }
                    }
                }
                else
                {
                    grdLICDEF.DataSource = null;
                    grdLICDEF.DataBind();
                    BtnSubmit.Visible = false;
                    MessageAlert(" Record Not Found ", "../HomePage/FrmDashboard.aspx");
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

                if (rbtApproved.Checked)
                {
                    isValid = true;
                    ApproveDoc += lblSevikaID.Text + "$" + txtRejectReason.Text + "#";
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
                        RejectDoc += lblSevikaID.Text + "$" + txtRejectReason.Text + "#";
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
            ObjLICStatus.SevikaID = Convert.ToInt32(Session["SevikaId"]);
            ObjLICStatus.Apprvstr = ApproveDoc;
            ObjLICStatus.Rejctstr = RejectDoc;
            ObjLICStatus.UpDateLICApproval();
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

        protected void lnkbtnDownload_Click(object sender, EventArgs e)
        {
            
            LinkButton btn = sender as LinkButton;
            GridViewRow gvrow = btn.NamingContainer as GridViewRow;
            //string filePath = "..ImageGarbage\\" + grdLICDEF.DataKeys[gvrow.RowIndex].Value.ToString();
            //Response.ContentType = "image/jpg";
            //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            //Response.TransmitFile(Server.MapPath(filePath));
            //Response.End();

            System.Net.WebClient client = new System.Net.WebClient();
            String ExportPath = Server.MapPath("..\\ImageGarbage\\LICSevika_12112020001200_download.jpg");
            string filePath = "..\\ImageGarbage\\LICSevika_12112020001200_download.jpg";
            String FileName = Convert.ToInt32(Session["SevikaId"])+"_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpg";
            if (Session["ImageDoc"] != "")
            {
                Byte[] buffer = client.DownloadData(ExportPath);

                if (buffer != null)
                {

                    FileStream fs = null;
                    fs = File.Open(ExportPath, FileMode.Open);
                    byte[] btFile = new byte[fs.Length];
                    fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    Response.ContentType = "image/jpg";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                    Response.TransmitFile(Server.MapPath(filePath));
                    Response.End();


                }
            }

        }
    }
}