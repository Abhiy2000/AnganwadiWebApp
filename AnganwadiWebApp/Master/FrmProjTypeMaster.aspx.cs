using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AnganwadiWebApp.Master
{
    public partial class FrmProjTypeMaster : System.Web.UI.Page
    {
        AnganwadiLib.Business.ProjectBrConfig objProjectBrConfig = new AnganwadiLib.Business.ProjectBrConfig();

        Int32 Inmode = 1;
        Int32 ProjectId = 0;

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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "ProjectType Master";
                txtProjectCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                txtProjectCode.BackColor = backcolor;
                txtProjectCode.ForeColor = forecolor;
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    ProjectId = 0;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    grdProjectList_set();
                }
            }
        }

        private void grdProjectList_set()
        {
            #region "Load Grid"

            String query = "select num_ProjectType_PrjTypeid proId,var_ProjectType_PrjType ProName from aoup_ProjectType_def where num_ProjectType_PrjTypeid='" + Session["ProId"] + "' order by num_ProjectType_PrjTypeid";
           
            DataTable TblEduId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (TblEduId.Rows.Count > 0)
            {
                if (TblEduId.Rows[0]["proId"].ToString() != "" && TblEduId.Rows[0]["proId"].ToString() != null)
                {
                    txtProjectCode.Text = TblEduId.Rows[0]["proId"].ToString();
                }
                if (TblEduId.Rows[0]["ProName"].ToString() != "" && TblEduId.Rows[0]["ProName"].ToString() != null)
                {
                    txtProjectName.Text = TblEduId.Rows[0]["ProName"].ToString();
                }
            }
            #endregion
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtProjectName.Text == "")
            {
                MessageAlert("Project Name cannot be blank", "");
                return;
            }

            objProjectBrConfig.BrId = Convert.ToInt32(Session["GrdLevel"]);
            objProjectBrConfig.UserId= Session["UserId"].ToString();
            if (Request.QueryString["@"] == "1")
            {
                objProjectBrConfig.Mode = 1;
                objProjectBrConfig.ProjectId = ProjectId;
            }
            else
            {
                objProjectBrConfig.Mode = 2;
                objProjectBrConfig.ProjectId = Convert.ToInt32(txtProjectCode.Text.Trim());
            }
            objProjectBrConfig.Project = txtProjectName.Text.Trim();

            objProjectBrConfig.Insert();

            if (objProjectBrConfig.ErrCode == -100)
            {
                MessageAlert(objProjectBrConfig.ErrMsg, "../Master/FrmProjectTypeList.aspx");
                return;
            }
            else
            {
                MessageAlert(objProjectBrConfig.ErrMsg, "");
                return;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmProjectTypeList.aspx");
        }
    }
}