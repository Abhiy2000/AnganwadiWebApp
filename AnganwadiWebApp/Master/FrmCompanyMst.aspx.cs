using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;
using System.Text.RegularExpressions;

namespace AnganwadiWebApp.Master
{
    public partial class FrmCompanyMst : System.Web.UI.Page
    {
        int Inmode = 0;

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmCompanyList.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "State Master";

                MstMethods.Dropdown.Fill(ddlBank, "aoup_bank_def order by trim(var_bank_bankname)", "var_bank_bankname", "num_bank_bankid", "", "");
                MstMethods.Dropdown.Fill(ddlProjectType, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "", "");

                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    txtBranchName.Enabled = false;
                    //txtBranchName.Text = "Admin Branch";
                    txtBranchName.Text = "Integrated Child development Scheme";
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    txtName.Enabled = false;
                    txtBranchName.Enabled = false;
                    filldata();
                }
                //RdoDiv.Checked = true;
                //ManageRadio();
            }
        }

        protected void RdoOrg_CheckedChanged(object sender, EventArgs e)
        {
            ManageRadio();
        }

        protected void RdoDiv_CheckedChanged(object sender, EventArgs e)
        {
            ManageRadio();
        }

        protected void RdoDistrict_CheckedChanged(object sender, EventArgs e)
        {
            ManageRadio();
        }

        protected void RdoCDPO_CheckedChanged(object sender, EventArgs e)
        {
            ManageRadio();
        }

        protected void RdoBeat_CheckedChanged(object sender, EventArgs e)
        {
            ManageRadio();
        }

        protected void ddlParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlParent.SelectedValue != "")
            {
                String str = "SELECT var_corporation_corpname corpname FROM aoup_corporation_mas where num_corporation_corpid = " + ddlParent.SelectedValue.ToString();

                DataTable TblName = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                txtName.Text = TblName.Rows[0]["corpname"].ToString();
            }
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBank.SelectedValue.ToString() == "" || ddlBank.SelectedValue.ToString() == "0")
            {
                ddlBankBranch.DataSource = "";
                ddlBankBranch.DataBind();
            }
            else
            {
                MstMethods.Dropdown.Fill(ddlBankBranch, "aoup_bankbranch_def", "var_bankbranch_branchname", "num_bankbranch_branchid", "num_bankbranch_bankid = " + ddlBank.SelectedValue, "");
            }
        }

        protected void LinkSerchBankBranch_OnClick(object sender, EventArgs e)
        {
            if (ddlBank.SelectedValue.ToString() == "" || ddlBank.SelectedValue.ToString() == "0")
            {
                MessageAlert("Select Bank first from the list", "");
                ddlBank.Focus();
                return;
            }

            if (txtIFSC.Text == "")
            {
                MessageAlert("IFSC code can not be blank", "");
                txtIFSC.Focus();
                return;
            }

            String str = "select var_bankbranch_branchname branchname,num_bankbranch_branchid branchid from aoup_bankbranch_def where num_bankbranch_bankid = " + ddlBank.SelectedValue.ToString() + " and ";
            str += "UPPER(var_bankbranch_ifsccode) = UPPER('" + txtIFSC.Text + "')";

            DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblResult.Rows.Count > 0)
            {
                ddlBankBranch.SelectedValue = TblResult.Rows[0]["branchid"].ToString();
                txtBankBranch.Text = TblResult.Rows[0]["branchname"].ToString();
            }
            else
            {
                ddlBankBranch.DataSource = "";
                ddlBankBranch.DataBind();
                txtBankBranch.Text = "";

                MessageAlert("Bank branch not found", "");
                txtIFSC.Focus();
                return;
            }
        }

        public void ManageRadio()
        {
            ddlParent.Enabled = false;

            txtBranchName.Enabled = true;
            txtBranchName.Text = "";

            txtName.Enabled = false;

            if (RdoOrg.Checked == true)
            {
                ddlParent.Enabled = false;

                txtBranchName.Enabled = false;
                txtBranchName.Text = "Admin Branch";

                txtName.Enabled = true;
            }

            else if (RdoDiv.Checked == true)
            {
                ddlParent.Enabled = true;
                txtName.Enabled = false;

                MstMethods.Dropdown.Fill(ddlParent, "aoup_corporation_mas", "var_corporation_corpname || ' - ' || var_corporation_branch", "num_corporation_corpid", "num_company_category=1", "");
            }

            else if (RdoDistrict.Checked == true)
            {
                ddlParent.Enabled = true;
                txtName.Enabled = false;

                MstMethods.Dropdown.Fill(ddlParent, "aoup_corporation_mas", "var_corporation_corpname || ' - ' || var_corporation_branch", "num_corporation_corpid", "num_company_category=2", "");
            }

            else if (RdoCDPO.Checked == true)
            {
                ddlParent.Enabled = true;
                txtName.Enabled = false;

                MstMethods.Dropdown.Fill(ddlParent, "aoup_corporation_mas", "var_corporation_corpname || ' - ' || var_corporation_branch", "num_corporation_corpid", "num_company_category=3", "");
            }

            else if (RdoBeat.Checked == true)
            {
                ddlParent.Enabled = true;
                txtName.Enabled = false;

                MstMethods.Dropdown.Fill(ddlParent, "aoup_corporation_mas", "var_corporation_corpname || ' - ' || var_corporation_branch", "num_corporation_corpid", "num_company_category=4", "");
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageAlert(" Name can not be blank", "");
                txtName.Focus();
                return;
            }
            if (txtBranchName.Text == "")
            {
                MessageAlert("Branch Name can not be blank", "");
                txtBranchName.Focus();
                return;
            }
            if (txtCode.Text == "")
            {
                MessageAlert("Code can not be blank", "");
                txtCode.Focus();
                return;
            }
            if (txtCode.Text.Length != 2)
            {
                MessageAlert("State Code should be 2 digit", "");
                txtCode.Focus();
                return;
            }

            if (txtAddress.Text == "")
            {
                MessageAlert("Address can not be blank", "");
                txtAddress.Focus();
                return;
            }

            if (txtEmailId.Text == "")
            {
                MessageAlert("Email ID can not be blank", "");
                return;
            }
            if (txtEmailId.Text != "")
            {
                string email = txtEmailId.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                }
                else
                {
                    MessageAlert(email + " is Invalid Email Address", "");
                    return;
                }
            }
            if (txtPhoneno.Text != "")
            {
                if (txtPhoneno.Text.Length != 10)
                {
                    MessageAlert("Phone No should be 10 digit", "");
                    return;
                }
            }


            AnganwadiLib.Business.CompanyMst objCom = new AnganwadiLib.Business.CompanyMst();

            objCom.Category = 1;

            if (ddlParent.SelectedValue.ToString() != "")
            {
                objCom.Parent = Convert.ToInt32(ddlParent.SelectedValue);
            }
            else
            {
                objCom.Parent = 0;
            }

            objCom.CompanyName = txtName.Text.Trim();
            objCom.BranchName = txtBranchName.Text.Trim();
            objCom.Code = txtCode.Text.Trim();
            objCom.Address = txtAddress.Text.Trim();

            if (txtPhoneno.Text != "" && txtPhoneno.Text != "0")
            {
                objCom.PhoneNumber = Convert.ToInt64(txtPhoneno.Text.Trim());
            }

            objCom.EmailAddress = txtEmailId.Text.Trim();

            if (ddlBankBranch.SelectedValue.ToString() != "" && ddlBankBranch.SelectedValue.ToString() != "0")
            {
                objCom.BankBranchId = Convert.ToInt32(ddlBankBranch.SelectedValue);
            }

            objCom.BankAccNo = txtAccountNo.Text.Trim();

            //if (ddlProjectType.SelectedValue.ToString() != "" && ddlProjectType.SelectedValue.ToString() != "0")
            //{
            //    objCom.ProjectTypeId = Convert.ToInt32(ddlProjectType.SelectedValue);
            //}
            objCom.ProjectTypeId = 0;
            objCom.OfficerNM = "";
            objCom.PinCode = 0;
            objCom.PrjCode = 0;
            objCom.DistCode = 0;
            if (Request.QueryString["@"] == "1")
            {
                objCom.Mode = 1;
                objCom.CorpId = 0;
            }
            else
            {
                objCom.Mode = 2;
                objCom.CorpId = Convert.ToInt32(Session["StateId"]);
            }

            objCom.UserId = Session["UserId"].ToString();

            objCom.Insert();

            if (objCom.ErrCode == -100)
            {
                MessageAlert(objCom.ErrMsg, "../Master/FrmCompanyList.aspx");
                return;
            }
            else
            {
                MessageAlert(objCom.ErrMsg, "");
                return;
            }
        }

        public void filldata()
        {
            String query = " select num_corporation_corpid corpid,var_company_compcode Code,var_corporation_corpname Name,var_corporation_branch Branch, ";
            query += " var_company_address address,var_projecttype_prjtype PrjType,num_company_branchid bankbranch,var_bankbranch_branchname branchnm,num_bankbranch_bankid bank, ";
            query += " var_company_phoneno phone,var_company_emailid email,var_company_accno accno,num_company_prjtypeid prjid from aoup_corporation_mas a ";
            query += " left join aoup_projecttype_def b on a.num_company_prjtypeid=b.num_projecttype_prjtypeid ";
            query += " left join aoup_bankbranch_def c on a.num_company_branchid=c.num_bankbranch_branchid ";
            query += " where num_corporation_parentid=0 and num_corporation_corpid= " + Session["StateId"];

            DataTable dtStateList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtStateList.Rows.Count > 0)
            {
                if (dtStateList.Rows[0]["Name"].ToString() != "" && dtStateList.Rows[0]["Name"].ToString() != null)
                {
                    txtName.Text = dtStateList.Rows[0]["Name"].ToString();
                }
                if (dtStateList.Rows[0]["Branch"].ToString() != "" && dtStateList.Rows[0]["Branch"].ToString() != null)
                {
                    txtBranchName.Text = dtStateList.Rows[0]["Branch"].ToString();
                }
                if (dtStateList.Rows[0]["Code"].ToString() != "" && dtStateList.Rows[0]["Code"].ToString() != null)
                {
                    txtCode.Text = dtStateList.Rows[0]["Code"].ToString();
                }
                if (dtStateList.Rows[0]["address"].ToString() != "" && dtStateList.Rows[0]["address"].ToString() != null)
                {
                    txtAddress.Text = dtStateList.Rows[0]["address"].ToString();
                }
                if (dtStateList.Rows[0]["phone"].ToString() != "" && dtStateList.Rows[0]["phone"].ToString() != null)
                {
                    txtPhoneno.Text = dtStateList.Rows[0]["phone"].ToString();
                }
                if (dtStateList.Rows[0]["email"].ToString() != "" && dtStateList.Rows[0]["email"].ToString() != null)
                {
                    txtEmailId.Text = dtStateList.Rows[0]["email"].ToString();
                }
                if (dtStateList.Rows[0]["bank"].ToString() != "" && dtStateList.Rows[0]["bank"].ToString() != null)
                {
                    ddlBank.SelectedValue = dtStateList.Rows[0]["bank"].ToString();
                    ddlBank_SelectedIndexChanged(null, null);
                }
                if (dtStateList.Rows[0]["bankbranch"].ToString() != "" && dtStateList.Rows[0]["bankbranch"].ToString() != null)
                {
                    ddlBankBranch.SelectedValue = dtStateList.Rows[0]["bankbranch"].ToString();
                }
                if (dtStateList.Rows[0]["branchnm"].ToString() != "" && dtStateList.Rows[0]["branchnm"].ToString() != null)
                {
                    txtBankBranch.Text = dtStateList.Rows[0]["branchnm"].ToString();
                }
                if (dtStateList.Rows[0]["accno"].ToString() != "" && dtStateList.Rows[0]["accno"].ToString() != null)
                {
                    txtAccountNo.Text = dtStateList.Rows[0]["accno"].ToString();
                }
                if (dtStateList.Rows[0]["prjid"].ToString() != "" && dtStateList.Rows[0]["prjid"].ToString() != null)
                {
                    ddlProjectType.SelectedValue = dtStateList.Rows[0]["prjid"].ToString();
                }
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmCompanyList.aspx");
        }
    }
}