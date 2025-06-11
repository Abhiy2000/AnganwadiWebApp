using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProjectManagement.Master
{
    public partial class FrmContractorMaster : System.Web.UI.Page
    {
        AnganwadiLib.Business.ConractorBrConfig objContractorBrConfig = new AnganwadiLib.Business.ConractorBrConfig();
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
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Contractor Master";
               // txtDepartmentCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                //txtDepartmentCode.BackColor = backcolor;
                //txtDepartmentCode.ForeColor = forecolor;
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    DepartmentId = 0;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                   // ContractId = Convert.ToInt32(Session["ContractId"]);
                   
                    String str = "select var_department_dept from aoup_department_def where  num_department_deptid=" + DepartmentId;

                    DataTable TblContractId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                    if (TblContractId.Rows.Count > 0)
                    {
                        //txtDepartmentCode.Text = (Session["DepartmentId"]).ToString();

                        txtContractName.Text = TblContractId.Rows[0]["VAR_CONTRACT_CONTRACTNAME"].ToString();
                        txtAddress.Text = TblContractId.Rows[0]["VAR_CONTRACT_ADDRESS"].ToString();
                        txtEmail.Text = TblContractId.Rows[0]["VAR_CONTRACT_EMAIL"].ToString();
                        txtPerson.Text = TblContractId.Rows[0]["VAR_CONTRACT_CONTACTPERSON"].ToString();
                    }
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtContractName.Text == "")
            {
                MessageAlert("Contract Name cannot be blank", "");
                return;
            }
            if (txtAddress.Text == "")
            {
                MessageAlert("Address  cannot be blank", "");
                return;
            }
            if (txtEmail.Text == "")
            {
                MessageAlert("Email Name cannot be blank", "");
                return;
            }
            if (txtPerson.Text == "")
            {
                MessageAlert("Person Name cannot be blank", "");
                return;
            }

            //objContractorBrConfig.BrId = Convert.ToInt32(Session["GrdLevel"]);
            if (Request.QueryString["@"] == "1")
            {
                objContractorBrConfig.Mode = 1;
              
            }
            objContractorBrConfig.BrId =Convert.ToInt32(Session["GrdLevel"]);
            objContractorBrConfig.ContractName = txtContractName.Text;
            objContractorBrConfig.Address = txtAddress.Text;
            objContractorBrConfig.Email = txtEmail.Text;
            objContractorBrConfig.ContactPerson = Convert.ToInt64(txtPerson.Text);
            objContractorBrConfig.UserId = Session["UserId"].ToString();
            objContractorBrConfig.Insert();

            if (objContractorBrConfig.ErrCode == -100)
            {
                MessageAlert(objContractorBrConfig.ErrMsg, "../HomePage/FrmDashboard.aspx");
                return;
            }
            else
            {
                MessageAlert(objContractorBrConfig.ErrMsg, "");
                return;
            }       
        }
    }
}