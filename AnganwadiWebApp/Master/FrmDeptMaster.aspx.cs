using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;


namespace ProjectManagement.Master
{
    public partial class FrmDeptMaster : System.Web.UI.Page
    {
        AnganwadiLib.Business.DepartmentBrConfig objDepartmentBrConfig = new AnganwadiLib.Business.DepartmentBrConfig();
        Int32 Inmode = 1;
        Int32 DepartmentId = 0;

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDepartmentRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Department Master";
                txtDepartmentCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                txtDepartmentCode.BackColor = backcolor;
                txtDepartmentCode.ForeColor = forecolor;
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    DepartmentId = 0;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    DepartmentId = Convert.ToInt32(Session["DepartmentId"]);
                    String str = "select var_department_dept from aoup_department_def where num_department_brid=" + Session["GrdLevel"] + " and num_department_deptid=" + DepartmentId;

                    DataTable TblDepartmentId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                    if (TblDepartmentId.Rows.Count > 0)
                    {
                        txtDepartmentCode.Text = (Session["DepartmentId"]).ToString();
                        txtDepartmentName.Text = TblDepartmentId.Rows[0]["var_department_dept"].ToString();
                    }
                }
            }
        }
        
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDepartmentName.Text == "")
            {
                MessageAlert("Department Name cannot be blank", "");
                return;
            }
            if (Request.QueryString["@"] == "1")
            {
                objDepartmentBrConfig.Mode = 1;
                objDepartmentBrConfig.DepartmentId = DepartmentId;
            }
            else
            {
                objDepartmentBrConfig.Mode = 2;
                objDepartmentBrConfig.DepartmentId = Convert.ToInt32(txtDepartmentCode.Text.Trim());
            }
            objDepartmentBrConfig.BrId = Convert.ToInt32(Session["GrdLevel"]);
            objDepartmentBrConfig.Department = txtDepartmentName.Text.Trim();
            objDepartmentBrConfig.Insert();

            if (objDepartmentBrConfig.ErrCode == -100)
            {
                MessageAlert(objDepartmentBrConfig.ErrMsg, "../Master/FrmDepartmentList.aspx");
                return;
            }
            else
            {
                MessageAlert(objDepartmentBrConfig.ErrMsg, "");
                return;
            }
        }

        protected void btnCancel_Cilck(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmDepartmentList.aspx");
            return;
        }
    }
}