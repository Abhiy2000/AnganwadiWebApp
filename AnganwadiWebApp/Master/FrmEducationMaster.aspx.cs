using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Business;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmEducationMaster : System.Web.UI.Page
    {
        EducationBrConfig objEducationBrConfig = new EducationBrConfig();

        Int32 Inmode = 1;
        Int32 EducationId = 0;

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmEducationRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Qualification Master";// Session["LblGrdHead"].ToString();// 
                txtEducationCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                txtEducationCode.BackColor = backcolor;
                txtEducationCode.ForeColor = forecolor;
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    EducationId = 0;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    grdEducationList_set();
                    //EducationId = Convert.ToInt32(Session["EducationId"]);
                    //String str = "select var_Education_EducName from aoup_Education_def where  num_Education_Educid=" + EducationId;

                    //DataTable TblDepartmentId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                    //if (TblDepartmentId.Rows.Count > 0)
                    //{
                    //    txtEducationCode.Text = Session["EducationId"].ToString();
                    //    txtEducationName.Text = TblDepartmentId.Rows[0]["var_Education_EducName"].ToString();
                    //}
                }
            }
        }

        private void grdEducationList_set()
        {
            #region "Load Grid"

            String query = "select num_Education_Educid eduId,var_Education_EducName Eduname from aoup_Education_def where num_Education_Educid=" + Session["EduId"];
            query += " order by num_Education_Educid";

            // DataTable dtBankList = (DataTable)HRPayRollClass.Methods.MstMethods.Query2DataTable.GetResult(query);
            DataTable TblEduId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (TblEduId.Rows.Count > 0)
            {
                if (TblEduId.Rows[0]["eduId"].ToString() != "" && TblEduId.Rows[0]["eduId"].ToString() != null)
                {
                    txtEducationCode.Text = TblEduId.Rows[0]["eduId"].ToString();
                }
                if (TblEduId.Rows[0]["Eduname"].ToString() != "" && TblEduId.Rows[0]["Eduname"].ToString() != null)
                {
                    txtEducationName.Text = TblEduId.Rows[0]["Eduname"].ToString();
                }
            }
            #endregion
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtEducationName.Text == "")
            {
                MessageAlert("Qualification Name cannot be blank", "");
                return;
            }

            //objEducationBrConfig.BrId = Convert.ToInt32(Session["CompId"]);
            objEducationBrConfig.UserId = Session["UserId"].ToString();
            if (Request.QueryString["@"] == "1")
            {
                objEducationBrConfig.Mode = 1;
                objEducationBrConfig.EducationId = EducationId;
            }
            else
            {
                objEducationBrConfig.Mode = 2;
                objEducationBrConfig.EducationId = Convert.ToInt32(txtEducationCode.Text.Trim());
            }
            objEducationBrConfig.Education = txtEducationName.Text.Trim();
            objEducationBrConfig.Insert();

            if (objEducationBrConfig.ErrCode == -100)
            {
                MessageAlert(objEducationBrConfig.ErrMsg, "../Master/FrmEducationList.aspx");
                return;
            }
            else
            {
                MessageAlert(objEducationBrConfig.ErrMsg, "");
                return;
            }
        }

        protected void btnCancel_Cilck(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmEducationList.aspx");
            return;
        }
    }
}