using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagement.Master
{
    public partial class FrmProjectMaster : System.Web.UI.Page
    {
        //     AnganwadiLib.Business.ProjectMasterBrConfig objProjectBrConfig = new AnganwadiLib.Business.ProjectMasterBrConfig();

        Int32 Inmode = 1;
        Int32 DesignationId = 0;

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

            if (!IsPostBack)
            {
                //txtDesignationCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                //txtDesignationCode.BackColor = backcolor;
                // txtDesignationCode.ForeColor = forecolor;
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    DesignationId = 0;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    //ProjectId = Convert.ToInt32(Session["ProjectId"]);
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtProjId.Text == "")
            {
                MessageAlert("Project Id cannot be blank", "");
                return;
            }
            if (txtReferenceNo.Text == "")
            {
                MessageAlert("Reference Name cannot be blank", "");
                return;
            }
            if (txtProjName.Text == "")
            {
                MessageAlert("Project Name cannot be blank", "");
                return;
            }
            if (ddlProjOwner.SelectedValue == "")
            {
                MessageAlert(" Please select Owner Name", "");
                ddlProjOwner.Focus();
                return;
            }
            else
            {
                //objProjectBrConfig.WorkingFor = Convert.ToInt64(ddlProjOwner.SelectedValue);
            }

            if (txtDate.Text == "")
            {
                MessageAlert("Date cannot be blank", "");
                return;
            }
            if (ddlDurComplete.SelectedValue == "")
            {
                MessageAlert(" Please select Duration required to complete", "");
                ddlDurComplete.Focus();
                return;
            }
            else
            {
                //objProjectBrConfig.WorkingFor = Convert.ToInt64(ddlDurComplete.SelectedValue);
            }

            if (txtMonitor.Text == "")
            {
                MessageAlert("Monitor Name cannot be blank", "");
                return;
            }

            if (ddlUserId.Text == "")
            {
                MessageAlert("UserId cannot be blank", "");
                return;
            }
            if (txtNoStages.Text == "")
            {
                MessageAlert("No of stages cannot be blank", "");
                return;
            }
            //objProjectBrConfig.ProjectId = txtProjId.Text;
            //objProjectBrConfig.Designation = txtReferenceNo.Text;
            //objProjectBrConfig.Designation = txtProjName.Text;
            //objProjectBrConfig.Designation = ddlProjOwner.Text;
            //objProjectBrConfig.Designation = txtDate.Text;
            //objProjectBrConfig.Designation = ddlDurComplete.Text;
            //objProjectBrConfig.Designation = txtMonitor.Text;
            //objProjectBrConfig.Designation = ddlUserId.Text;
            //objProjectBrConfig.Designation = txtNoStages.Text;
        }
    }
}