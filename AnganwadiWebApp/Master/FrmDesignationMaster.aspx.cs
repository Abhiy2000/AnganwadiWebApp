using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Business;
using AnganwadiLib.Methods;

namespace ProjectManagement.Master
{
    public partial class FrmDesignationMaster : System.Web.UI.Page
    {
        DesignationBrConfig objDesignationBrConfig = new DesignationBrConfig();

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDesignationRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Designation Master";
                txtDesignationCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                txtDesignationCode.BackColor = backcolor;
                txtDesignationCode.ForeColor = forecolor;
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    DesignationId = 0;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    DesignationId = Convert.ToInt32(Session["DesignationId"]);
                    String str = "select var_designation_desig,var_designation_code,var_designation_flag from aoup_designation_def where num_designation_desigid=" + DesignationId;

                    DataTable TblDepartmentId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                    if (TblDepartmentId.Rows.Count > 0)
                    {
                        txtDesignationCode.Text = Session["DesignationId"].ToString();
                        txtDesignationName.Text = TblDepartmentId.Rows[0]["var_designation_desig"].ToString();
                        txtCode.Text = TblDepartmentId.Rows[0]["var_designation_code"].ToString();
                        if (TblDepartmentId.Rows[0]["var_designation_flag"].ToString() != "")
                        {
                            if (TblDepartmentId.Rows[0]["var_designation_flag"].ToString() == "W")
                            {
                                rbdWorker.Checked = true;
                            }
                            if (TblDepartmentId.Rows[0]["var_designation_flag"].ToString() == "H")
                            {
                                rbdHelper.Checked = true;
                            }
                            if (TblDepartmentId.Rows[0]["var_designation_flag"].ToString() == "O")
                            {
                                rbdOther.Checked = true;
                            }
                            if (TblDepartmentId.Rows[0]["var_designation_flag"].ToString() == "M")
                            {
                                rbdMini.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDesignationName.Text == "")
            {
                MessageAlert("Designation Name cannot be blank", "");
                return;
            }
            if (rbdHelper.Checked == false && rbdWorker.Checked == false && rbdOther.Checked == false && rbdMini.Checked==false)
            {
                MessageAlert("Please select Designation Type", "");
                return;
            }
            //objDesignationBrConfig.BrId = Convert.ToInt32(Session["GrdLevel"]);
            if (Request.QueryString["@"] == "1")
            {
                objDesignationBrConfig.Mode = 1;
                objDesignationBrConfig.DesignationId = DesignationId;
            }
            else
            {
                objDesignationBrConfig.Mode = 2;
                objDesignationBrConfig.DesignationId = Convert.ToInt32(txtDesignationCode.Text.Trim());
            }
            objDesignationBrConfig.Designation = txtDesignationName.Text.Trim();
            if (txtCode.Text != "")
            {
                objDesignationBrConfig.Code = txtCode.Text.Trim();
            }
            else
            {
                objDesignationBrConfig.Code = "";
            }

            if (rbdHelper.Checked == true)
            {
                objDesignationBrConfig.Flag = "H";
            }
            if (rbdOther.Checked == true)
            {
                objDesignationBrConfig.Flag = "O";
            }
            if (rbdWorker.Checked == true)
            {
                objDesignationBrConfig.Flag = "W";
            }
            if (rbdMini.Checked == true)
            {
                objDesignationBrConfig.Flag = "M";
            }
            objDesignationBrConfig.UserId = Session["UserId"].ToString();
            objDesignationBrConfig.Insert();

            if (objDesignationBrConfig.ErrCode == -100)
            {
                MessageAlert(objDesignationBrConfig.ErrMsg, "../Master/FrmDesignationList.aspx");
                return;
            }
            else
            {
                MessageAlert(objDesignationBrConfig.ErrMsg, "");
                return;
            }
        }

        protected void btnCancel_Cilck(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmDesignationList.aspx");
            return;
        }
    }
}