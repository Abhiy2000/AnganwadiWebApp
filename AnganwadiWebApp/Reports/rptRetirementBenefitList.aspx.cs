using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;
using System.Drawing;
using System.IO;

namespace AnganwadiWebApp.Reports
{
    public partial class rptRetirementBenefitList : System.Web.UI.Page
    {
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Payment Report";
            if (!IsPostBack)
            {
              //  PnlSerch.Visible = true;

                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();

                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["BRCategory"]);

                if (BRCategory == 0)    //Admin
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                }
                if (BRCategory == 1)    //State
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");
                }

                else if (BRCategory == 2)   // Div
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " and brcategory = 2 order by branchname", "");

                    ddlDivision.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDivision_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDistrict_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                }

                else if (BRCategory == 4)   // CDPO
                {
                    str = "select a.num_corporation_corpid cdpo, a.num_corporation_parentid dis, b.num_corporation_parentid div ";
                    str += "from aoup_corporation_mas a ";
                    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = Session["GrdLevel"].ToString();

                    ddlCDPO_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                }

                else if (BRCategory == 5)   // Beat
                {
                    str = "select a.num_corporation_corpid beat, a.num_corporation_parentid cdpo, b.num_corporation_parentid dis, c.num_corporation_parentid div ";
                    str += "from aoup_corporation_mas a ";
                    str += "inner join aoup_corporation_mas b on a.num_corporation_parentid = b.num_corporation_corpid ";
                    str += "inner join aoup_corporation_mas c on b.num_corporation_parentid = c.num_corporation_corpid ";
                    str += "where a.num_corporation_corpid = " + Session["GrdLevel"].ToString() + " ";

                    TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["div"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["dis"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["cdpo"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();
                    ddlBeat.SelectedValue = Session["GrdLevel"].ToString();

                    ddlBeat_OnSelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                   // PnlSerch.Visible = true;
                }
            }
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //PnlSerch.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //PnlSerch.Visible = false;

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //PnlSerch.Visible = false;

            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
            }
        }

        protected void ddlBeat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBeat.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();
               // PnlSerch.Visible = true;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dtFrmDate.Text == "")
            {
                MessageAlert("Please select From Date", "");
                return;
            }
            if (dtToDate.Text == "")
            {
                MessageAlert("Please select To Date", "");
                return;
            }
            if (dtToDate.Value < dtFrmDate.Value)
            {
                MessageAlert("From Date can not be greater than to date", "");
                return;
            }
            LoadGrid();
        }

        protected void LoadGrid()
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            DateTime frmdt = Convert.ToDateTime(dtFrmDate.Value);
            DateTime todt = Convert.ToDateTime(dtToDate.Value);

            if (rbtType.SelectedValue == "0")
            {
                String det1 = "select * from view_death_claims ";
                det1 += " where stateid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " and ExitDate >= '" + frmdt.ToString("dd-MMM-yyyy") + "' and ExitDate <= '" + todt.ToString("dd-MMM-yyyy") + "'";
                det1 += " and UPPER(causeexit) = UPPER('" + rbtType.SelectedItem.Text + "') ";
                if (ddlDivision.SelectedValue != "")
                {
                    det1 += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
                }
                if (ddlDistrict.SelectedValue != "")
                {
                    det1 += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
                }
                if (ddlCDPO.SelectedValue != "")
                {
                    det1 += " and cdpoid=" + Convert.ToInt32(ddlCDPO.SelectedValue);
                }
                if (ddlBeat.SelectedValue != "")
                {
                    det1 += " and bitid=" + Convert.ToInt32(ddlBeat.SelectedValue);
                }
                DataTable dtDeathList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det1);

                if (dtDeathList.Rows.Count > 0)
                {
                    GenerateDeathReport(dtDeathList);
                    ViewState["dtDeathList"] = dtDeathList;
                }
                else
                {
                    MessageAlert(" Record not Found for selected Date", "");
                    return;
                }
            }

            if (rbtType.SelectedValue == "1")
            {
                String det = "select * from view_retirement_resignation ";
                det += " where stateid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " and ExitDate >= '" + frmdt.ToString("dd-MMM-yyyy") + "' and ExitDate <= '" + todt.ToString("dd-MMM-yyyy") + "'";
                det += " and UPPER(causeexit) not in('DEATH') ";
                if (ddlDivision.SelectedValue != "")
                {
                    det += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
                }
                if (ddlDistrict.SelectedValue != "")
                {
                    det += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
                }
                if (ddlCDPO.SelectedValue != "")
                {
                    det += " and cdpoid=" + Convert.ToInt32(ddlCDPO.SelectedValue);
                }
                if (ddlBeat.SelectedValue != "")
                {
                    det += " and bitid=" + Convert.ToInt32(ddlBeat.SelectedValue);
                }
                DataTable dtList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

                if (dtList.Rows.Count > 0)
                {
                    GenerateReport(dtList);
                    ViewState["dtList"] = dtList;
                }
                else
                {
                    MessageAlert(" Record not Found for selected Date", "");
                    return;
                }
            }
        }

        public void GenerateDeathReport(DataTable tblDeath)
        {
            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "Retirement_Death_Claims_Rpt_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtDeathClaim.rpt");
            ExportPath = Server.MapPath("..\\ImageGarbage\\") + PDFNAME;

            String[] Parameter = new String[2];
            String[] ParameterVal = new String[2];

            Parameter[0] = "FrmDate";
            Parameter[1] = "ToDate";

            ParameterVal[0] = dtFrmDate.Text;
            ParameterVal[1] = dtToDate.Text;

            tblDeath.TableName = "tblDeath";

            Byte[] btFile = AnganwadiLib.Methods.MstMethods.Report.ViewReport("", tblDeath, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");

            Response.AddHeader("Content-disposition", "attachment; filename=" + PDFNAME);

            //Response.ContentType = "application/octet-stream";
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(btFile);
            Response.TransmitFile(ExportPath);
            //Response.End();
        }

        public void GenerateReport(DataTable tblRetirement)
        {
            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "Retirement_Resignation_Claims_Rpt_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtRetirementClaim.rpt");
            ExportPath = Server.MapPath("..\\ImageGarbage\\") + PDFNAME;

            String[] Parameter = new String[2];
            String[] ParameterVal = new String[2];

            Parameter[0] = "FrmDate";
            Parameter[1] = "ToDate";

            ParameterVal[0] = dtFrmDate.Text;
            ParameterVal[1] = dtToDate.Text;

            tblRetirement.TableName = "tblRetirement";

            Byte[] btFile = AnganwadiLib.Methods.MstMethods.Report.ViewReport("", tblRetirement, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");

            Response.AddHeader("Content-disposition", "attachment; filename=" + PDFNAME);

            //Response.ContentType = "application/octet-stream";
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(btFile);
            Response.TransmitFile(ExportPath);
            //Response.End();
        }

    }
}