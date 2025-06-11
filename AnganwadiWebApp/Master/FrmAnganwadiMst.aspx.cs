using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Business;
using AnganwadiLib.Methods;
using System.Text.RegularExpressions;

namespace AnganwadiWebApp.Master
{
    public partial class FrmAnganwadiMst : System.Web.UI.Page
    {
        BoAnganMst ObjAngan = new BoAnganMst();

        int Inmode = 0;
        Int32 AnganId = 0;

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmAnganwadiRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Aganwadi Master"; //
                string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
                DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);
                if (TblGetHoId.Rows.Count > 0)
                {
                    MstMethods.Dropdown.Fill(ddlprjType, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "num_projecttype_compid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " order by num_projecttype_prjtypeid ", "");
                }

                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    AnganId = 0;
                    rbdActive.Checked = true;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    filldata();
                }
            }
        }

        public void filldata()
        {
            AnganId = Convert.ToInt32(Session["AngnId"]);
            String str = "select num_angnwadimst_compid,num_angnwadimst_angnid,num_angnwadimst_prjtypeid,var_angnwadimst_angnname,var_angnwadimst_angncode,var_angnwadimst_address, ";
            str += " var_angnwadimst_email,num_angnwadimst_mobileno,var_angnwadimst_phoneno,var_angnwadimst_active,num_anganwadimst_pincode pin from aoup_angnwadimst_def where num_angnwadimst_angnid=" + AnganId;
            str += " and num_angnwadimst_compid=" + Session["GrdLevel"].ToString();

            DataTable tblAnganDet = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

            if (tblAnganDet.Rows.Count > 0)
            {
                txtAngnName.Text = tblAnganDet.Rows[0]["var_angnwadimst_angnname"].ToString();
                txtAngnCode.Text = tblAnganDet.Rows[0]["var_angnwadimst_angncode"].ToString();
                txtAddress.Text = tblAnganDet.Rows[0]["var_angnwadimst_address"].ToString();
                txtEmail.Text = tblAnganDet.Rows[0]["var_angnwadimst_email"].ToString();
                txtPinCode.Text = tblAnganDet.Rows[0]["pin"].ToString();
                txtMobNo.Text = tblAnganDet.Rows[0]["num_angnwadimst_mobileno"].ToString();
                txtPhoneNo.Text = tblAnganDet.Rows[0]["var_angnwadimst_phoneno"].ToString();
                //ddlprjType.SelectedValue = tblAnganDet.Rows[0]["num_angnwadimst_prjtypeid"].ToString();
                if (tblAnganDet.Rows[0]["var_angnwadimst_active"].ToString() == "A")
                {
                    rbdActive.Checked = true;
                }
                else
                {
                    rbdInActive.Checked = true;
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtAngnName.Text == "")
            {
                MessageAlert(" Please Enter Anganwadi Name ", "");
                txtAngnName.Focus();
                return;
            }

            if (txtAngnCode.Text == "")
            {
                MessageAlert(" Please Enter Anganwadi Code ", "");
                txtAngnCode.Focus();
                return;
            }

            if (txtAngnCode.Text.Length != 11)
            {
                MessageAlert(" Anganwadi Code should be 11 digit ", "");
                txtAngnCode.Focus();
                return;
            }

            if (txtAddress.Text == "")
            {
                MessageAlert(" Please Enter Anganwadi Address ", "");
                txtAddress.Focus();
                return;
            }

            if (txtAddress.Text.Length > 200)
            {
                MessageAlert(" Address Length can not be grater than 200 character ", "");
                txtAddress.Focus();
                return;
            }
            if (txtEmail.Text != "")
            {
                string email = txtEmail.Text;
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

            //if (txtEmail.Text == "")
            //{
            //    MessageAlert(" Please Enter Email ", "");
            //    txtEmail.Focus();
            //    return;
            //}

            //if (txtMobNo.Text == "")
            //{
            //    MessageAlert(" Please Enter Mobile No. ", "");
            //    txtMobNo.Focus();
            //    return;
            //}

            //if (txtPhoneNo.Text == "")
            //{
            //    MessageAlert(" Please Enter Phone No. ", "");
            //    txtPhoneNo.Focus();
            //    return;
            //}
            if (txtPinCode.Text != "")
            {
                if (txtPinCode.Text.Length != 6)
                {
                    MessageAlert(" PIN Code should be 6 digit", "");
                    return;
                }
            }
            if (txtMobNo.Text != "")
            {
                if (txtMobNo.Text.Length != 10)
                {
                    MessageAlert(" Mobile No should be 10 digit", "");
                    return;
                }
            }
            if (txtPhoneNo.Text != "")
            {
                if (txtPhoneNo.Text.Length != 10)
                {
                    MessageAlert(" Phone No should be 10 digit", "");
                    return;
                }
            }
            if (rbdActive.Checked == true)
            {
                CallProc();
            }
            else
            {
                popMsg1.Show();
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmAnganwadiList.aspx");
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            CallProc();
        }

        public void CallProc()
        {
            try
            {
                ObjAngan.BrId = Convert.ToInt32(Session["GrdLevel"]);
                ObjAngan.AngnID = Convert.ToInt32(Session["AngnId"]);
                if (ddlprjType.SelectedIndex != 0)
                {
                    ObjAngan.PrjTypeId = Convert.ToInt32(ddlprjType.SelectedValue);
                }
                else
                {
                    ObjAngan.PrjTypeId = 0;
                }
                ObjAngan.AngnName = txtAngnName.Text.Trim();
                ObjAngan.AngnCode = txtAngnCode.Text.Trim();
                ObjAngan.Address = txtAddress.Text.Trim();
                ObjAngan.Email = txtEmail.Text.Trim();
                if (txtPinCode.Text != "")
                {
                    ObjAngan.PinCode = Convert.ToInt32(txtPinCode.Text.Trim());
                }
                else
                {
                    ObjAngan.PinCode = 0;
                }

                if (txtMobNo.Text != "")
                {
                    ObjAngan.MobileNo = Convert.ToInt64(txtMobNo.Text.Trim());
                }
                else
                {
                    ObjAngan.MobileNo = 0;
                }

                if (txtPhoneNo.Text != "")
                {
                    ObjAngan.PhoneNo = txtPhoneNo.Text.Trim();
                }
                else
                {
                    ObjAngan.PhoneNo = null;
                }

                if (rbdActive.Checked == true)
                {
                    ObjAngan.Active = "A";
                }
                else
                {
                    ObjAngan.Active = "I";
                    //popMsg1.Show();
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>alert(' You are saving Anganwadi in InActive Mode ');</script>", false);
                }
                ObjAngan.UserId = Session["UserId"].ToString();
                if (Request.QueryString["@"] == "1")
                {
                    ObjAngan.Mode = 1;
                }
                else
                {
                    ObjAngan.Mode = 2;
                }
                ObjAngan.BoAnganMst_1();

                if (ObjAngan.ErrorCode == -100)
                {
                    MessageAlert(ObjAngan.ErrorMsg, "../Master/FrmAnganwadiList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(ObjAngan.ErrorMsg, "../Master/FrmAnganwadiMst.aspx");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }
    }
}