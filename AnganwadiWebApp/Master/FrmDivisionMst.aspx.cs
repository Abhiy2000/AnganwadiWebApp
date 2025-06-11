using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;
using AnganwadiLib.Business;
using System.Text.RegularExpressions;

namespace AnganwadiWebApp.Master
{
    public partial class FrmDivisionMst : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDivisionRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Division Master";

                MstMethods.Dropdown.Fill(ddlstate, "aoup_corporation_mas", "distinct var_corporation_corpname", "num_corporation_corpid", "num_corporation_corpid=" + Session["GrdLevel"], "");
                MstMethods.Dropdown.Fill(ddlBank, "aoup_bank_def order by trim(var_bank_bankname)", "var_bank_bankname", "num_bank_bankid", "", "");
                MstMethods.Dropdown.Fill(ddlProjectType, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "", "");

                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    //txtName.Enabled = false;
                    //txtBranchName.Enabled = false;
                    filldata();
                }
                txtBranchName.Enabled = false;
                String comp = "select companyname from companyview where brid=" + Session["GrdLevel"];
                DataTable dtComp = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(comp);
                if (dtComp.Rows.Count > 0)
                {
                    txtBranchName.Text = dtComp.Rows[0]["companyname"].ToString();
                }
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
                MessageAlert("DDO Code can not be blank", "");
                txtCode.Focus();
                return;
            }
            if(txtCode.Text.Length != 10)
            {
                MessageAlert("DDO Code should be 10 digit", "");
                txtCode.Focus();
                return;
            }
            if (txtAddress.Text == "")
            {
                MessageAlert("Address can not be blank", "");
                txtAddress.Focus();
                return;
            }
            if (txtPinCode.Text != "")
            {
                if (txtPinCode.Text.Length != 6)
                {
                    MessageAlert("PIN Code Should be 6 digit", "");
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

            CompanyMst objCom = new CompanyMst();

            objCom.Category = 2;

            //if (ddlstate.SelectedValue.ToString() != "")
            //{
            objCom.Parent = Convert.ToInt32(Session["GrdLevel"]);
            //}
            //else
            //{
            //    objCom.Parent = 0;
            //}

            objCom.CompanyName = txtBranchName.Text.Trim();
            objCom.BranchName = txtName.Text.Trim();
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
            objCom.DistCode = 0;
            objCom.OfficerNM = "";
            if (txtPinCode.Text != "")
            {
                objCom.PinCode = Convert.ToInt32(txtPinCode.Text.Trim());
            }
            else
            {
                objCom.PinCode = 0;
            }
            objCom.PrjCode = 0;
            if (Request.QueryString["@"] == "1")
            {
                objCom.Mode = 1;
                objCom.CorpId = 0;
            }
            else
            {
                objCom.Mode = 2;
                objCom.CorpId = Convert.ToInt32(Session["DivisionId"]);
            }

            objCom.UserId = Session["UserId"].ToString();

            objCom.Insert();

            if (objCom.ErrCode == -100)
            {
                MessageAlert(objCom.ErrMsg, "../Master/FrmDivisionList.aspx");
                return;
            }
            else
            {
                MessageAlert(objCom.ErrMsg, "");
                return;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmDivisionList.aspx");
        }

        public void filldata()
        {
            String query = " select a.num_corporation_corpid corpid, d.num_corporation_corpid parentid,a.var_company_compcode Code,a.var_corporation_branch Name, ";
            query += " a.var_corporation_corpname Branch,a.var_company_address address,var_projecttype_prjtype PrjType,c.var_bankbranch_branchname branchnm,a.num_company_branchid bankbranch,num_bankbranch_bankid bank, ";
            query += " a.var_company_phoneno phone,a.var_company_emailid email,a.var_company_accno accno,a.num_company_prjtypeid prjid,a.num_company_pincode pin from aoup_corporation_mas a ";
            query += " left join aoup_projecttype_def b on a.num_company_prjtypeid=b.num_projecttype_prjtypeid ";
            query += " left join aoup_bankbranch_def c on a.num_company_branchid=c.num_bankbranch_branchid ";
            query += " left join aoup_corporation_mas d on a.num_corporation_corpid=d.num_corporation_parentid ";
            query += " where a.num_corporation_parentid=" + Session["GrdLevel"] + " and a.num_corporation_corpid= " + Session["DivisionId"];

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
                else
                {
                    txtCode.Text = "";
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
                if (dtStateList.Rows[0]["pin"].ToString() != "" && dtStateList.Rows[0]["pin"].ToString() != null)
                {
                    txtPinCode.Text = dtStateList.Rows[0]["pin"].ToString();
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
    }
}