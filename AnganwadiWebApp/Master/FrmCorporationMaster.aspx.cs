using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;

namespace ProjectManagement.Master
{
    public partial class FrmCorporationMaster : System.Web.UI.Page
    {
        AnganwadiLib.Business.CorporationMasterBrConfig objCorporationMasterBrConfig = new AnganwadiLib.Business.CorporationMasterBrConfig();

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
                LblGrdHead.Text = "Corporation Master";

                Session["GrdLevel"] = Session["brid"];
                MstMethods.Dropdown.Fill(ddlParentId, "aoup_corporation_mas", "var_corporation_corpname", "num_corporation_corpid", "num_corporation_corpid='" + Session["GrdLevel"] + "'", "");
                MstMethods.Dropdown.Fill(ddlClass, "aoup_corpclass_mas", "VAR_CORPCLASS_CLASSNAME", "NUM_CORPCLASS_CLASSID", "", "");
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlParentId.SelectedValue == "")
            {
                MessageAlert(" Please select Parent Id", "");
                ddlParentId.Focus();
                return;
            }
            if (txtCorporationName.Text == "")
            {
                MessageAlert("Corporation Name cannot be blank", "");
                return;
            }
            if (ddlClass.SelectedValue == "")
            {
                MessageAlert(" Please select Class", "");
                ddlParentId.Focus();
                return;
            }

            objCorporationMasterBrConfig.BrId = Convert.ToInt32(Session["GrdLevel"]);
            objCorporationMasterBrConfig.ParentID = Convert.ToInt32(ddlParentId.SelectedValue);
            objCorporationMasterBrConfig.CorporationName = txtCorporationName.Text.Trim();
            objCorporationMasterBrConfig.Class = ddlClass.SelectedValue;
            objCorporationMasterBrConfig.CorpBranch = "HO";
            objCorporationMasterBrConfig.UserId = Session["UserId"].ToString();
            objCorporationMasterBrConfig.Insert();

            if (objCorporationMasterBrConfig.ErrCode == 9999)
            {
                MessageAlert(objCorporationMasterBrConfig.ErrMsg, "../Home/FrmDashboard.aspx");
                return;
            }
            else
            {
                MessageAlert(objCorporationMasterBrConfig.ErrMsg, "");
                return;
            }
        }
    }
}