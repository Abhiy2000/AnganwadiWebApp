using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using AnganwadiLib.Business;
using AnganwadiLib.Methods;

namespace ANCL_MRRGWEB.Transaction
{
    public partial class FrmInterviewSchedule : System.Web.UI.Page
    {
        BoInterviewschedule obj = new BoInterviewschedule();
        #region "MeesageAlert"
        public void MessageAlert(String Message, String WindowsLocation)
        {
            String str = "";

            str = "alert('|| " + Message + " ||');";

            if (WindowsLocation != "")
            {
                str += "window.location = '" + WindowsLocation + "';";
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, str, true);
            return;
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
                DtInterviewDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                LblGrdHead.Text = "Transaction" + " >> " + "Interview Schedule";
                filldropdwon();
                Disable();
            }
        }

        protected void filldropdwon()
        {
            MstMethods.Dropdown.Fill(ddlAnganwadi, "tb_anganwadi_mas", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' and var_angnwadimst_active='Y' order by trim(var_angnwadimst_angnname)", "");
            MstMethods.Dropdown.Fill(ddlporition, "tb_porition_mas", "var_porition_poriname", "num_porition_poriid", " num_porition_compid ='" + Session["GrdLevel"].ToString() + "' and var_porition_active='Y' order by trim(var_porition_poriname)", "");
            bindcompany();
            FillDetails();
        }
        public void bindcompany()
        {
            String str = "select brcategory, parentid from vw_companyview where brid = " + Session["GrdLevel"].ToString();
            DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);
            Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["BRCategory"]);
            if (BRCategory == 0)    //Admin
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
            }
            if (BRCategory == 1)    //State
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
            }
            else if (BRCategory == 2)   // Div
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                ddlDivision.SelectedValue = Session["GrdLevel"].ToString();
                ddlDivision_SelectedIndexChanged(null, null);
                ddlDivision.Enabled = false;
            }
            else if (BRCategory == 3)   // Dis
            {
                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");
                ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();
                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
            }

            else if (BRCategory == 4)   // CDPO
            {
                str = "select a.num_corporation_corpid cdpo, a.num_corporation_parentid dis, b.num_corporation_parentid div ";
                str += "from tb_corporation_mas a ";
                str += "inner join tb_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();

                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
            }

            else if (BRCategory == 5)   // Beat
            {
                str = "select a.num_corporation_corpid beat, a.num_corporation_parentid cdpo, b.num_corporation_parentid dis, c.num_corporation_parentid div ";
                str += "from tb_corporation_mas a ";
                str += "inner join tb_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                str += "inner join tb_corporation_mas c on b.num_corporation_parentid = c.num_corporation_corpid ";
                str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                MstMethods.Dropdown.Fill(ddlDivision, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");

                ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
            }
        }
        public void FillDetails()
        {
            String str = "";
            str += " SELECT num_slot_id slot, var_slot_tokenname || ' ' || var_slot_name time ";
            str += " FROM tb_slot_def ";
            str += " WHERE num_slot_id NOT IN ";
            str += " (SELECT num_appli_slotid ";
            str += " FROM tb_appli_mas ";
            str += " WHERE num_appli_slotid IS NOT NULL ";
            str += " AND TRUNC (date_appli_slotdate) = '" + Convert.ToDateTime(DtInterviewDt.Value).ToString("dd-MMM-yyyy") + "') ";
            str += " ORDER BY num_slot_id ";

            DataTable tblDept = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (tblDept.Rows.Count > 0)
            {
                GrdUserLevel.DataSource = tblDept;
                GrdUserLevel.DataBind();
            }
            else
            {
                GrdUserLevel.DataSource = null;
                GrdUserLevel.DataBind();
                MessageAlert("No record found", "");
            }
        }
        public void GetDetails()
        {
            string qry = " select COMPID, APPLIID, APPLINO, APPNAME, ADDRESS, PORTID, POSITION, ANGNID, ANGNNAME, EDUQUALID, ";
            qry += " EDUQULI_NAME, MARITALID, MARISTATUS_NAME, PROJID, PROJNAME, DIVID, DIVISION, DISTID, DISTRICT, ";
            qry += " DOB, AGE from vw_applidtls ";
            qry += " where COMPID = '" + Session["GrdLevel"].ToString() + "' and APPLINO = '" + txtApplino.Text + "' and AUTHSTATUS = 'Yes' and SLOTID is null ";

            DataTable tblgrdbind = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
            if (tblgrdbind.Rows.Count > 0)
            {
                Session["tblgrdbind"] = tblgrdbind;
                txtNameApp.Text = tblgrdbind.Rows[0]["APPNAME"].ToString();
                txtAddress.Text = tblgrdbind.Rows[0]["ADDRESS"].ToString();
                ddlDivision.SelectedValue = tblgrdbind.Rows[0]["DIVID"].ToString();
                ddlDivision_SelectedIndexChanged(null, null);
                ddlDistrict.SelectedValue = tblgrdbind.Rows[0]["DISTID"].ToString();
                ddlAnganwadi.SelectedValue = tblgrdbind.Rows[0]["ANGNID"].ToString();
                ddlporition.SelectedValue = tblgrdbind.Rows[0]["PORTID"].ToString();

                DateTime dob;
                if (DateTime.TryParse(tblgrdbind.Rows[0]["DOB"].ToString(), out dob))
                {
                    birthdt.Text = dob.ToString("dd/MM/yyyy");
                }
                else
                {
                    birthdt.Text = ""; // or show error
                }

                txtAge.Text = tblgrdbind.Rows[0]["AGE"].ToString();
            }
            else
            {
                String str = "";
                str += " SELECT count(num_appli_slotid) slotid ";
                str += " FROM tb_appli_mas ";
                str += " WHERE var_appli_applino = '"+ txtApplino.Text +"' AND num_appli_slotid IS NOT NULL ";

                DataTable tblSlot = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                if (Convert.ToInt64(tblSlot.Rows[0]["slotid"]) > 0)
                {
                    MessageAlert("Slot Already Booked For this Application", "");
                    return;
                }
                else
                {
                    MessageAlert("No Record Found", "");
                    return;
                }
            }
        }
        public void Disable()
        {
            txtNameApp.Enabled = false;
            txtAddress.Enabled = false;
            ddlDivision.Enabled = false;
            ddlDistrict.Enabled = false;
            ddlAnganwadi.Enabled = false;
            ddlporition.Enabled = false;
            birthdt.DisableControl();
            txtAge.Enabled = false;
        }

        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            if (DtInterviewDt.Value < DateTime.Now.Date)
            {
                MessageAlert(" || Interview Date Cannot be less than System Date ||", "");
                return;
            }
            
            int count = 0;

            foreach (GridViewRow row in GrdUserLevel.Rows)
            {
                CheckBox ch = (CheckBox)row.FindControl("ChkSlot");
                if (ch.Checked)
                {
                    count++;
                    if (count > 1)
                    {
                        MessageAlert("Please select only one slot", "");
                        return;
                    }
                }
            }

            String ChkSlotString = "";

            for (int i = 0; i < GrdUserLevel.Rows.Count; i++)
            {
                CheckBox ChkSlot = (CheckBox)GrdUserLevel.Rows[i].Cells[0].FindControl("ChkSlot");

                if (ChkSlot.Checked == true)
                {
                    ChkSlotString += GrdUserLevel.Rows[i].Cells[1].Text;
                }
            }

            if (ChkSlotString != "")
            {
                obj.SlotId = Convert.ToInt64(ChkSlotString);
            }
            else
            {
                MessageAlert("Please select Slot", "");
                return;
            }
            
            obj.UserId = Session["UserId"].ToString();
            obj.Applino = txtApplino.Text.Trim().ToString();
            obj.SlotDate = Convert.ToDateTime(DtInterviewDt.Value);
            obj.Compid = Session["GrdLevel"].ToString();

            obj.Insert();

            if (obj.ErrorCode == 9999)
            {
                MessageAlert(obj.ErrorMsg, "../Transaction/FrmInterviewSchedule.aspx");
                return;
            }
            else
            {
                MessageAlert(obj.ErrorMsg, "../Transaction/FrmInterviewSchedule.aspx");
                return;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx");
        }

        //public void GenerateReport()
        //{

        //    DataTable SlottReceipt = new DataTable();
        //    String ReportPath = "";
        //    String ExportPath = "";
        //    String PDFNAME = "Receipt" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
        //    String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
        //    var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

        //    ReportPath = Server.MapPath("~\\Reports\\CrtSlottReceipt.rpt");
        //    ExportPath = Server.MapPath("..\\ImageGarbage\\") + "Receipt" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

        //    String[] Parameter = new String[7];
        //    String[] ParameterVal = new String[7];

        //    Parameter[0] = "Corporation";
        //    Parameter[1] = "ReceiptNo";
        //    Parameter[2] = "Address";
        //    Parameter[3] = "MobileNo";
        //    Parameter[4] = "Subject";
        //    Parameter[5] = "RegistrationDate";
        //    Parameter[6] = "FormNo";



        //    ParameterVal[0] = Session["ReceiptOfficeName"].ToString();
        //    ParameterVal[1] = "REC/004"; ;
        //    ParameterVal[2] = txtaddress.Text;
        //    ParameterVal[3] = txtphoneno.Text;
        //    ParameterVal[4] = "Marriage Registration";
        //    ParameterVal[5] = DateTime.Now.ToString();
        //    ParameterVal[6] = txtformno.Text;


        //    SlottReceipt.TableName = "SlottReceipt";

        //    Byte[] btFile = Methods.MainModule.Report.ViewReport("", SlottReceipt, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");


        //    String path = "../ReportsForm/ViewPdf.aspx?ID=" + PDFNAME + "";
        //    if (File.Exists(finalPDF))
        //    {
        //        Response.Write("<script>");
        //        Response.Write("window.open('" + path + "','_blank')");
        //        Response.Write("</script>");
        //    }
        //    else
        //    {
        //        Response.Write("<script type='text/javascript'>");
        //        Response.Write("alert('PDF not created');");
        //        Response.Write("</script>");
        //    }


        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtApplino.Text == "")
            {
                MessageAlert("Application No can not be blank.", "");
                txtApplino.Focus();
                return;
            }

            GetDetails();
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }
    }
}