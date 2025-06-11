using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Reports
{
    public partial class RptLICStatusReport : System.Web.UI.Page
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
            if (DtFromDate.Text == "")
            {
                MessageAlert("Please Select From Date", "");
                DtFromDate.Focus();
                return;
            }
            if (DtToDate.Text == "")
            {
                MessageAlert("Please Select To Date", "");
                DtToDate.Focus();
                return;
            }
            if (DtToDate.Value < DtFromDate.Value)
            {
                MessageAlert("From Date can not be greater than to date", "");
                return;
            }

            DateTime FrmDt = Convert.ToDateTime(DtFromDate.Value);
            String FromDate = FrmDt.ToString("dd-MMM-yyyy");
            DateTime ToDt = Convert.ToDateTime(DtToDate.Value);
            String ExitToDate = ToDt.ToString("dd-MMM-yyyy");

            if (rdbSevika.SelectedValue == "I")
            {
                string Query = " Select divname Division,distname District,cdponame CDPO,bitbitname BIT,num_lic_sevikaid SevikaID, ";
                Query += " var_sevikamaster_name SevikaName,trunc(date_lic_exitdate) ExitDate,var_sevikamaster_reason Remark,num_lic_licamount Amount , ";
                Query += " CASE WHEN img_lic_document is not null THEN 'Y' ELSE ' ' END Document, ";
                Query += " case when var_lic_approvflag='A' then 'Y' else ' ' end CDPO_Approval, ";
                Query += " CASE WHEN var_lic_dpoapprovflag = 'A' THEN 'Y' ELSE ' ' END DPO_approval, ";
                Query += " case when var_lic_hoapprovflag='A' then 'Y' else ' ' end  HO_Approval, ";
                Query += " case when var_lic_licapprovflag='A' then 'Y' else ' ' end  LIC_Approval, ";
                Query += " case when var_lic_flag_payment='P' then 'Y' else ' ' end  LIC_Payment,'' OLDTSOFTWARENO from aoup_LIC_DEF  ";
                Query += " inner join aoup_sevikamaster_def  on num_sevikamaster_sevikaid=num_lic_sevikaid  inner join corpinfo on bitid=num_lic_compid  ";
                Query += " where var_lic_flag_insert='I' and trunc(date_lic_exitdate) >='" + FromDate + "' and trunc(date_lic_exitdate) <='" + ExitToDate + "' ";
                if (ddlDivision.SelectedValue != "")
                {
                    Query += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
                }
                if (ddlDistrict.SelectedValue != "")
                {
                    Query += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
                }
                if (ddlCDPO.SelectedValue != "")
                {
                    Query += " and cdpoid=" + Convert.ToInt32(ddlCDPO.SelectedValue);
                }
                if (ddlBeat.SelectedValue != "")
                {
                    Query += " and bitid=" + Convert.ToInt32(ddlBeat.SelectedValue);
                }
                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
                if (dtSevikaList.Rows.Count > 0)
                {
                    ViewState["SevikaList"] = dtSevikaList;
                    if (rdbExport.SelectedValue == "P")
                    {
                        GenerateReport(dtSevikaList);
                    }
                    if (rdbExport.SelectedValue == "E")
                    {
                        GenerateExcel(dtSevikaList);
                    }
                }
                else
                {
                    MessageAlert(" Record Not Found ", "");
                    return;
                }
            }
            if (rdbSevika.SelectedValue == "E")
            {
                string Query2 = " Select divname Division,distname District,cdponame CDPO,bitbitname BIT,num_lic_sevikaid sevikaid, var_licsevika_name sevikaname,";
                Query2 += " trunc(date_lic_exitdate) ExitDate,var_licsevika_remark Remark,num_lic_licamount Amount , ";
                Query2 += " CASE WHEN img_lic_document is not null THEN 'Y' ELSE ' ' END Document, ";
                Query2 += " case when var_lic_approvflag='A' then 'Y' else ' ' end CDPO_Approval, ";
                Query2 += " CASE WHEN var_lic_dpoapprovflag = 'A' THEN 'Y' ELSE ' ' END DPO_approval, ";
                Query2 += " case when var_lic_hoapprovflag='A' then 'Y' else ' ' end  HO_Approval, ";
                Query2 += " case when var_lic_licapprovflag='A' then 'Y' else ' ' end  LIC_Approval, ";
                Query2 += " case when var_lic_flag_payment='P' then 'Y' else ' ' end  LIC_Payment,VAR_LICSEVIKA_OLDTSOFTWARENO OLDTSOFTWARENO from aoup_LIC_DEF  ";
                Query2 += " inner join aoup_licsevika_def  on num_licsevika_sevikaid=num_lic_sevikaid  inner join corpinfo on bitid=num_lic_compid  ";
                Query2 += " where  var_lic_flag_insert='E' and trunc(date_lic_exitdate) >='" + FromDate + "' and trunc(date_lic_exitdate) <='" + ExitToDate + "' ";
                if (ddlDivision.SelectedValue != "")
                {
                    Query2 += " and divid=" + Convert.ToInt32(ddlDivision.SelectedValue);
                }
                if (ddlDistrict.SelectedValue != "")
                {
                    Query2 += " and distid=" + Convert.ToInt32(ddlDistrict.SelectedValue);
                }
                if (ddlCDPO.SelectedValue != "")
                {
                    Query2 += " and cdpoid=" + Convert.ToInt32(ddlCDPO.SelectedValue);
                }
                if (ddlBeat.SelectedValue != "")
                {
                    Query2 += " and bitid=" + Convert.ToInt32(ddlBeat.SelectedValue);
                }
                DataTable dtSevikaList2 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query2);
                if (dtSevikaList2.Rows.Count > 0)
                {
                    ViewState["SevikaList"] = dtSevikaList2;
                    if (rdbExport.SelectedValue == "P")
                    {
                        GenerateReport(dtSevikaList2);
                    }
                    if (rdbExport.SelectedValue == "E")
                    {
                        GenerateExcel(dtSevikaList2);
                    }
                }
                else
                {
                    MessageAlert(" Record Not Found ", "");
                    return;
                }
            }

        }
        public void GenerateReport(DataTable tblLicStatus)
        {

            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "LIC_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtLICStatusReport.rpt");
            ExportPath = Server.MapPath("..\\ImageGarbage\\") + PDFNAME;

            String[] Parameter = new String[2];
            String[] ParameterVal = new String[2];
            Parameter[0] = "From Date";
            Parameter[1] = "To Date";

            ParameterVal[0] = DtFromDate.Value.ToString();
            ParameterVal[1] = DtToDate.Value.ToString();

            Byte[] btFile = AnganwadiLib.Methods.MstMethods.Report.ViewReport("", tblLicStatus, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");

            Response.AddHeader("Content-disposition", "attachment; filename=" + PDFNAME);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(btFile);
            Response.TransmitFile(ExportPath);
            //Response.End();
        }

        protected void GenerateExcel(DataTable tblLicStatus)
        {
            if (tblLicStatus.Rows.Count > 0)
            {
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                DataTable dtExcel = (DataTable)ViewState["SevikaList"];

                GridView1.DataSource = dtExcel;
                GridView1.DataBind();

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=LICStatus_Rpt" + System.DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                GridView1.RenderControl(htmlWrite);
                Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                Response.Write(stringWrite.ToString());
                Response.End();

            }
        }



    }
}