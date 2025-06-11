using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Reports
{
    public partial class RptLICReportDownload : System.Web.UI.Page
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

                    ddlDivision_SelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                }

                else if (BRCategory == 3)   // Dis
                {
                    MstMethods.Dropdown.Fill(ddlDivision, "companyview", "branchname", "brid", " brid = " + TblResult.Rows[0]["parentid"].ToString() + " order by branchname", "");
                    MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " brid = " + Session["GrdLevel"].ToString() + " order by branchname", "");

                    ddlDivision.SelectedValue = TblResult.Rows[0]["parentid"].ToString();
                    ddlDistrict.SelectedValue = Session["GrdLevel"].ToString();

                    ddlDistrict_SelectedIndexChanged(sender, e);

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

                    ddlCDPO_SelectedIndexChanged(sender, e);

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

                    ddlBeat_SelectedIndexChanged(sender, e);

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;
                    ddlBeat.Enabled = false;

                    // PnlSerch.Visible = true;
                }
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
            String ToDate = ToDt.ToString("dd-MMM-yyyy");
            String Query = " ";
            if (rdbSevika.SelectedValue == "I" && rdbstatus.SelectedValue == "DT")
            {
                //Internal Death
                Query += " select divname,distname,cdpocode,cdponame,bitcode,bitbitname,var_sevikamaster_name sevika_name, var_designation_desig designation, var_designation_flag ShortDesignation, ";
                Query += " var_sevikamaster_cpsmscode cpsmscode,var_sevikamaster_aadharno aadharno, date_sevikamaster_birthdate birthdate, date_sevikamaster_joindate joindate,date_lic_exitdate exitdate, ";
                Query += " num_lic_serviceyears serviceyears, var_lic_flag ExitReason,num_lic_payscal payscal, num_lic_licamount amount, ";
                Query += " var_sevikanominee_nom1name Nominee1, num_sevikanominee_nom1ratio, n1.bankname, n1.branchname, n1.ifsccode , num_sevikanominee_nom1accno, ";
                Query += " var_sevikanominee_nom2name Nominee2, num_sevikanominee_nom2ratio, n2.bankname, n2.branchname, n2.ifsccode , num_sevikanominee_nom2accno, ";
                Query += " var_sevikanominee_nom3name Nominee3, num_sevikanominee_nom3ratio, n3.bankname, n3.branchname, n3.ifsccode , num_sevikanominee_nom3accno,date_lic_insdate Submission_Date ";
                Query += " from aoup_LIC_DEF ";
                Query += " inner join aoup_sevikamaster_def on num_lic_compid=num_sevikamaster_compid and num_sevikamaster_sevikaid=num_lic_sevikaid ";
                Query += " inner join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
                Query += " inner join corpinfo on bitid=num_lic_compid ";
                Query += " left join aoup_sevikanominee_def on num_sevikanominee_compid = num_lic_compid and num_sevikanominee_sevikaid=num_lic_sevikaid ";
                Query += " left join view_bank_branch n1 on n1.branchid=num_sevikanominee_nom1brid ";
                Query += " left join view_bank_branch n2 on n2.branchid=num_sevikanominee_nom2brid ";
                Query += " left join view_bank_branch n3 on n3.branchid=num_sevikanominee_nom3brid ";
                Query += " where var_lic_flag_insert='I' and var_lic_flag = 'DT' ";

            }
            else if (rdbSevika.SelectedValue == "I" && rdbstatus.SelectedValue == "OT")
            {
                //Internal Other
                Query += " select  divname,distname,cdpocode,cdponame,bitcode,bitbitname,var_sevikamaster_name sevika_name,var_designation_desig designation, var_designation_flag ShortDesignation, ";
                Query += " var_sevikamaster_cpsmscode cpsmscode, var_sevikamaster_aadharno aadharno, date_sevikamaster_birthdate birthdate, date_sevikamaster_joindate joindate, ";
                Query += " date_lic_exitdate exitdate, num_lic_serviceyears serviceyears, var_lic_flag ExitReason, num_lic_payscal payscal, bankname, branchname, ";
                Query += " var_sevikamaster_accno accno,ifsccode,num_lic_licamount amount, date_lic_insdate Submission_Date";
                Query += " from aoup_LIC_DEF ";
                Query += " inner join aoup_sevikamaster_def on num_lic_compid=num_sevikamaster_compid and num_sevikamaster_sevikaid=num_lic_sevikaid ";
                Query += " inner join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
                Query += " inner join corpinfo on bitid=num_lic_compid ";
                Query += " inner join view_bank_branch on branchid=num_sevikamaster_branchid ";
                Query += " where var_lic_flag_insert='I' ";
                Query += " and  var_lic_flag in ('RS','RT','TE') ";
            }
            else if (rdbSevika.SelectedValue == "E" && rdbstatus.SelectedValue == "DT")
            {
                //External Death
                Query += " select  divname,distname,cdpocode,cdponame,bitcode,bitbitname,var_licsevika_name sevika_name, var_licsevika_desigcode designation,var_licsevika_oldtsoftwareno PFMS_Number,var_licsevika_aadharno aadharno, ";
                Query += "  date_licsevika_birthdate birthdate, date_licsevika_joindate joindate,date_lic_exitdate exitdate, ";
                Query += " num_lic_serviceyears serviceyears, var_lic_flag ExitReason,num_lic_payscal payscal, num_lic_licamount amount, ";
                Query += " var_licsevikanomi_nom1name Nominee1, num_licsevikanomi_nom1ratio, n1.bankname, n1.branchname, n1.ifsccode , num_licsevikanomi_nom1accno, ";
                Query += " var_licsevikanomi_nom2name Nominee2, num_licsevikanomi_nom2ratio, n2.bankname, n2.branchname, n2.ifsccode , num_licsevikanomi_nom2accno, ";
                Query += " var_licsevikanomi_nom3name Nominee3, num_licsevikanomi_nom3ratio, n3.bankname, n3.branchname, n3.ifsccode , num_licsevikanomi_nom3accno,date_licsevika_insdate Submission_Date ";
                Query += " from aoup_LIC_DEF ";
                Query += " inner join aoup_licsevika_def on num_lic_compid=num_licsevika_compid and num_licsevika_sevikaid=num_lic_sevikaid ";
                // Query += " --inner join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
                Query += " inner join corpinfo on bitid=num_lic_compid ";
                Query += " left join aoup_licsevikanomi_def  on num_licsevikanomi_compid = num_lic_compid and num_licsevikanomi_sevikaid=num_lic_sevikaid ";
                Query += " left join view_bank_branch n1 on n1.branchid=num_licsevikanomi_nom1brid ";
                Query += " left join view_bank_branch n2 on n2.branchid=num_licsevikanomi_nom2brid ";
                Query += " left join view_bank_branch n3 on n3.branchid=num_licsevikanomi_nom3brid ";
                Query += " where var_lic_flag_insert='E' and var_lic_flag = 'DT' ";
            }
            else if (rdbSevika.SelectedValue == "E" && rdbstatus.SelectedValue == "OT")
            {
                //External Other

                Query += " select divname, distname,cdpocode,cdponame,bitcode,bitbitname,var_licsevika_name sevika_name, var_licsevika_desigcode designation,var_licsevika_oldtsoftwareno PFMS_Number,var_licsevika_aadharno aadharno, ";
                Query += "   date_licsevika_birthdate birthdate, date_licsevika_joindate joindate,date_lic_exitdate exitdate, ";
                Query += " num_lic_serviceyears serviceyears, var_lic_flag ExitReason,num_lic_payscal payscal, num_lic_licamount amount, ";
                Query += " n.bankname Sevika_bankname,  n.branchname sevika_branchname,var_licsevika_accno AccountNo,n.ifsccode sevika_ifsccode, ";//
                Query += " var_licsevikanomi_nom1name Nominee1, num_licsevikanomi_nom1ratio, n1.bankname, n1.branchname, n1.ifsccode , num_licsevikanomi_nom1accno,  ";
                Query += " var_licsevikanomi_nom2name Nominee2, num_licsevikanomi_nom2ratio, n2.bankname, n2.branchname, n2.ifsccode , num_licsevikanomi_nom2accno,  ";
                Query += " var_licsevikanomi_nom3name Nominee3, num_licsevikanomi_nom3ratio, n3.bankname, n3.branchname, n3.ifsccode , num_licsevikanomi_nom3accno,date_licsevika_insdate Submission_Date ";
                Query += " from aoup_LIC_DEF  ";
                Query += " inner join aoup_licsevika_def on num_lic_compid=num_licsevika_compid and num_licsevika_sevikaid=num_lic_sevikaid ";
                //Query += " --inner join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
                Query += " inner join corpinfo on bitid=num_lic_compid ";
                Query += " left join aoup_licsevikanomi_def  on num_licsevikanomi_compid = num_lic_compid and num_licsevikanomi_sevikaid=num_lic_sevikaid ";
                Query += " left join view_bank_branch n on n.branchid=num_licsevika_branchid ";
                Query += " left join view_bank_branch n1 on n1.branchid=num_licsevikanomi_nom1brid ";
                Query += " left join view_bank_branch n2 on n2.branchid=num_licsevikanomi_nom2brid ";
                Query += " left join view_bank_branch n3 on n3.branchid=num_licsevikanomi_nom3brid ";
                Query += " where var_lic_flag_insert='E' and var_lic_flag in ('RS','RT','TE') ";
            }
            else
            {
                MessageAlert(" please choose valid Selections ", "");
                return;
            }
            Query += " and trunc(date_lic_exitdate) between '" + FromDate + "' and '" + ToDate + "'";
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
                if (rdbdownload.SelectedValue == "P")
                {
                    GenerateReport(dtSevikaList);
                }
                else
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

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlCDPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBeat.DataSource = "";
            ddlBeat.DataBind();

            if (ddlCDPO.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlBeat, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
            }
        }

        protected void ddlBeat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBeat.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlBeat.SelectedValue.ToString();
                // PnlSerch.Visible = true;
            }
        }

        public void GenerateExcel(DataTable tblLicStatus)
        {
            DataTable dt = tblLicStatus;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (rdbSevika.SelectedValue == "I" && rdbstatus.SelectedValue == "DT")
                {
                    dt.Rows[i]["num_sevikanominee_nom1accno"] = dt.Rows[i]["num_sevikanominee_nom1accno"] + "'";
                    dt.Rows[i]["num_sevikanominee_nom2accno"] = dt.Rows[i]["num_sevikanominee_nom2accno"] + "'";
                    dt.Rows[i]["num_sevikanominee_nom3accno"] = dt.Rows[i]["num_sevikanominee_nom3accno"] + "'";
                    dt.AcceptChanges();
                }
                if (rdbSevika.SelectedValue == "I" && rdbstatus.SelectedValue == "OT")
                {
                    dt.Rows[i]["accno"] = dt.Rows[i]["accno"] + "'";
                    dt.AcceptChanges();
                }
                if (rdbSevika.SelectedValue == "E" && rdbstatus.SelectedValue == "DT")
                {
                    dt.Rows[i]["num_licsevikanomi_nom1accno"] = dt.Rows[i]["num_licsevikanomi_nom1accno"] + "'";
                    dt.Rows[i]["num_licsevikanomi_nom2accno"] = dt.Rows[i]["num_licsevikanomi_nom2accno"] + "'";
                    dt.Rows[i]["num_licsevikanomi_nom3accno"] = dt.Rows[i]["num_licsevikanomi_nom3accno"] + "'";
                    dt.AcceptChanges();
                }
                if (rdbSevika.SelectedValue == "E" && rdbstatus.SelectedValue == "OT")
                {
                    dt.Rows[i]["AccountNo"] = dt.Rows[i]["AccountNo"] + "'";
                    dt.Rows[i]["num_licsevikanomi_nom1accno"] = dt.Rows[i]["num_licsevikanomi_nom1accno"] + "'";
                    dt.Rows[i]["num_licsevikanomi_nom2accno"] = dt.Rows[i]["num_licsevikanomi_nom2accno"] + "'";
                    dt.Rows[i]["num_licsevikanomi_nom3accno"] = dt.Rows[i]["num_licsevikanomi_nom3accno"] + "'";
                    dt.AcceptChanges();
                }
            }
            if (dt.Rows.Count > 0)
            {
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                DataTable dtExcel = dt; //(DataTable)ViewState["SevikaList"];

                GridView1.DataSource = dtExcel;
                GridView1.DataBind();

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=LICSReportDownloadRpt" + System.DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
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
        public void GenerateReport(DataTable tblLicStatus)
        {

            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "LIC_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtLICDownloadReport.rpt");
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
    }
}
