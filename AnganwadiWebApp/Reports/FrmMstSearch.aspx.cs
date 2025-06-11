using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.IO;
using System.Data;

namespace AnganwadiWebApp.Reports
{
    public partial class FrmMstSearch : System.Web.UI.Page
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
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            if (!IsPostBack)
            {
                BindDropdrownlist();
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Master Search";
                MstMethods.Dropdown.Fill(ddlDiv, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory=2 order by branchname", "");
                MstMethods.Dropdown.Fill(ddlPrjType, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "num_projecttype_compid=" + Session["GrdLevel"].ToString(), "");
                rbdAuth.Visible = false;
                rbdUnauth.Visible = false;
                rbdReg.Visible = false;
                rbdUnreg.Visible = false;
                rbdWrker.Visible = false;
                rbdHelper.Visible = false;
                lblsub.Visible = false;
                Label1.Visible = false;
                ddlPrjType.Enabled = false;
                lblaadhar.Visible = false;
                Label2.Visible = false;
                txtAadharNo.Visible = false;
            }
        }

        protected void ddlDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDiv.SelectedValue != "")
            {
                MstMethods.Dropdown.Fill(ddlDist, "companyview", "branchname", "brid", " parentid = " + ddlDiv.SelectedValue.ToString() + " order by branchname", "");
            }
            btnExport.Visible = false;
        }

        protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDist.SelectedValue != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDist.SelectedValue.ToString() + " order by branchname", "");
            }
            btnExport.Visible = false;
        }

        protected void ddlCDPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCDPO.SelectedValue != "")
            {
                MstMethods.Dropdown.Fill(ddlBIT, "companyview", "branchname", "brid", " parentid = " + ddlCDPO.SelectedValue.ToString() + " order by branchname", "");
            }
            btnExport.Visible = false;
        }

        protected void ddlBIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBIT.SelectedValue != "")
            {
                MstMethods.Dropdown.Fill(ddlAnganwadi, "aoup_angnwadimst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", "num_angnwadimst_compid=" + ddlBIT.SelectedValue.ToString(), "");
            }
            btnExport.Visible = false;
        }

        protected void ddlRptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRptType.SelectedValue != "")
            {
                if (ddlRptType.SelectedValue == "S")
                {
                    rbdAuth.Visible = true;
                    rbdUnauth.Visible = true;
                    rbdReg.Visible = true;
                    rbdUnreg.Visible = true;
                    rbdWrker.Visible = true;
                    rbdHelper.Visible = true;
                    lblsub.Visible = true;
                    Label1.Visible = true;
                    lblaadhar.Visible = true;
                    Label2.Visible = true;
                    txtAadharNo.Visible = true;
                }
                else
                {
                    rbdAuth.Visible = false;
                    rbdUnauth.Visible = false;
                    rbdReg.Visible = false;
                    rbdUnreg.Visible = false;
                    rbdWrker.Visible = false;
                    rbdHelper.Visible = false;
                    lblsub.Visible = false;
                    Label1.Visible = false;
                    lblaadhar.Visible = false;
                    Label2.Visible = false;
                    txtAadharNo.Visible = false;
                }

                if (ddlRptType.SelectedValue == "A" || ddlRptType.SelectedValue == "C")
                {
                    ddlPrjType.Enabled = true;
                }
                else
                {
                    ddlPrjType.SelectedIndex = 0;
                    ddlPrjType.Enabled = false;
                }
            }
            btnExport.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (ddlDiv.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Division ", "");
            //    return;
            //}
            //if (ddlRptType.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Report Type ", "");
            //    return;
            //}
            if (ddlRptType.SelectedValue == "S")
            {
                if (txtAadharNo.Text != "")
                {
                    if (txtAadharNo.Text.Length != 12)
                    {
                        MessageAlert("Aadhar no is Invalid", "");
                        return;
                    }
                }
            }
            else
            {
                if (ddlDiv.SelectedValue == "")
                {
                    MessageAlert(" Please Select Division ", "");
                    return;
                }
                if (ddlRptType.SelectedValue == "")
                {
                    MessageAlert(" Please Select Report Type ", "");
                    return;
                }
            }
            LoadGrid();
        }

        public void BindDropdrownlist()
        {
            ddlRptType.Items.Add(new ListItem("-- Select Option --", ""));
            ddlRptType.Items.Add(new ListItem("District", "D"));
            ddlRptType.Items.Add(new ListItem("CDPO", "C"));
            ddlRptType.Items.Add(new ListItem("BIT", "B"));
            ddlRptType.Items.Add(new ListItem("Anganwadi", "A"));
            ddlRptType.Items.Add(new ListItem("Sevika", "S"));
        }

        public void LoadGrid()
        {
            String query = "";
            Int64 recorCount = 0;

            if (ddlRptType.SelectedIndex != 0)
            {
                if (ddlRptType.SelectedValue == "D")
                {
                    //query += " select branchname,compcode from companyview where parentid=" + ddlDiv.SelectedValue + " order by brid ";
                    query += " select divname Division,b.branchname District,var_company_compcode District_Code,num_company_distcode DDOCode,var_company_address Address, ";
                    query += " var_company_phoneno Phone_No,var_company_emailid Email,num_company_pincode PIN,bankname Bank_Name,ifsccode IFSC,a.branchname Bank_Branch, ";
                    query += " ''''||(var_company_accno) Account_No from companyview b left join corpinfo on bitid=b.brid ";
                    query += " left join aoup_corporation_mas on bitid=num_corporation_corpid left join view_bank_branch a on a.branchid= num_company_branchid ";
                    query += " where parentid=" + ddlDiv.SelectedValue + " order by brid ";
                }
                else
                {
                    if (ddlRptType.SelectedValue == "C")
                    {
                        /*//query += " select distname District, cdponame CDPO from corpinfo left join aoup_projecttype_def on num_projecttype_compid=" + Session["GrdLevel"];
                        query += " select distinct divname Division, distname District,num_company_prjcode Project_Code,cdpocode DDOCode,cdponame Project_Name,var_projecttype_prjtype Project_Type, ";
                        query += " var_company_officernm CDPO_OfficerName,var_company_address Address,var_company_phoneno Phone_No,num_company_pincode PIN, ";
                        query += " var_company_emailid Email,bankname Bank_Name,ifsccode IFSC,a.branchname Bank_Branch,var_company_accno Account_No ";
                        query += " from corpinfo ";
                        query += " left join aoup_corporation_mas on cdpoid=num_corporation_corpid left join view_bank_branch a on a.branchid= num_company_branchid ";
                        query += " left join aoup_projecttype_def on num_projecttype_compid=" + Session["GrdLevel"];
                        query += " and num_company_prjtypeid=num_projecttype_prjtypeid ";
                        query += " where stateid = " + Session["GrdLevel"];
                        if (ddlPrjType.SelectedIndex != 0)
                        {
                            query += " and num_projecttype_prjtypeid=" + ddlPrjType.SelectedValue;
                        }
                        //query += " order by statename, divname, distname, cdponame ";*/
                        query += " select  Div.var_corporation_branch Division, Dist.var_corporation_branch District,CDPO.num_company_prjcode Project_Code, ";
                        query += " Cdpo.var_company_compcode DDOCode,CDPO.var_corporation_branch Project_Name, var_projecttype_prjtype Project_Type, ";
                        query += " CDPO.var_company_officernm CDPO_OfficerName,CDPO.var_company_address Address,CDPO.var_company_phoneno Phone_No,CDPO.num_company_pincode PIN, ";
                        query += " CDPO.var_company_emailid Email,bankname Bank_Name,ifsccode IFSC,a.branchname Bank_Branch,''''||(CDPO.var_company_accno) Account_No ";
                        query += " from aoup_corporation_mas inner JOIN aoup_corporation_mas Div ON aoup_corporation_mas.num_corporation_corpid = Div. num_corporation_parentid ";
                        query += " inner JOIN aoup_corporation_mas Dist ON Div.num_corporation_corpid = Dist.num_corporation_parentid ";
                        query += " inner JOIN aoup_corporation_mas CDPO ON Dist.num_corporation_corpid = CDPO.num_corporation_parentid ";
                        query += " left join view_bank_branch a on a.branchid= CDPO.num_company_branchid ";
                        query += " left join aoup_projecttype_def on num_projecttype_compid=11 and CDPO.num_company_prjtypeid=num_projecttype_prjtypeid ";
                        query += " where aoup_corporation_mas.num_corporation_corpid = " + Session["GrdLevel"];
                        if (ddlDiv.SelectedValue != "")
                        {
                            query += " and Div.num_corporation_corpid=" + ddlDiv.SelectedValue;
                        }
                        if (ddlDist.SelectedValue != "")
                        {
                            query += " and Dist.num_corporation_corpid =" + ddlDist.SelectedValue;
                        }
                        if (ddlPrjType.SelectedIndex != 0)
                        {
                            query += " and num_projecttype_prjtypeid=" + ddlPrjType.SelectedValue;
                        }
                    }
                    if (ddlRptType.SelectedValue == "B")
                    {
                        /*query += " select distname District, cdponame CDPO, bitbitname BIT ";
                        query += " from corpinfo where stateid = " + Session["GrdLevel"];*/
                        query += " select divname Division, distname District, cdponame CDPO,b.num_company_prjcode Project_Code,cdpocode DDO_Code,bitbitname BIT,bitcode BIT_Code, ";
                        query += " a.var_company_officernm Supervisor_Name,a.var_company_address Address,a.var_company_phoneno Phone_No, ";
                        query += " a.var_company_emailid Email,a.num_company_pincode PIN  from corpinfo left join aoup_corporation_mas a on bitid=a.num_corporation_corpid ";
                        query += " left join aoup_corporation_mas b on b.num_corporation_corpid=cdpoid ";
                        query += " where stateid = " + Session["GrdLevel"];
                        if (ddlDiv.SelectedValue != "")
                        {
                            query += " and divid=" + ddlDiv.SelectedValue;
                        }
                        if (ddlDist.SelectedValue != "")
                        {
                            query += " and distid =" + ddlDist.SelectedValue;
                        }
                        if (ddlCDPO.SelectedValue != "")
                        {
                            query += " and cdpoid =" + ddlCDPO.SelectedValue;
                        }
                    }
                    if (ddlRptType.SelectedValue == "A")
                    {
                        /*query += " select divname Division,distname District, var_company_compcode DDOCode,var_projecttype_prjtype Project_type,bitcode BITCode, ";
                        query += " bitbitname BIT, var_company_officernm Supervisor,var_angnwadimst_angncode Anganwadi_code,var_angnwadimst_angnname Anganwadi_name, ";
                        query += " var_angnwadimst_address Address, num_angnwadimst_mobileno Mobileno,var_angnwadimst_email email from aoup_angnwadimst_def a ";
                        query += " inner join corpinfo on bitid = num_angnwadimst_compid left join aoup_corporation_mas on num_corporation_corpid=bitid ";
                        query += " left join aoup_projecttype_def b on a.num_angnwadimst_prjtypeid=b.num_projecttype_prjtypeid ";
                        query += " and num_projecttype_compid = stateid where stateid =" + Session["GrdLevel"];*/
                        query += " select divname Division,distname District, cdponame CDPO,c.num_company_prjcode Project_Code,a.var_company_officernm Supervisor, ";
                        query += " cdpocode DDO_Code,bitcode BIT_Code,var_projecttype_prjtype Project_type,bitbitname BIT,var_angnwadimst_angnname Anganwadi_name,";
                        query += " var_angnwadimst_angncode Anganwadi_code, var_angnwadimst_address Address,var_angnwadimst_email email, num_anganwadimst_pincode PIN, ";
                        query += " num_angnwadimst_mobileno Mobile_No,var_angnwadimst_phoneno Phone_No, ";
                        query += " case WHEN var_angnwadimst_active='A' THEN 'Active' else 'Inactive' end Status from aoup_angnwadimst_def a ";
                        query += " inner join corpinfo on bitid = num_angnwadimst_compid left join aoup_corporation_mas a on bitid=a.num_corporation_corpid left join  aoup_corporation_mas c on c.num_corporation_corpid=cdpoid ";
                        query += " left join aoup_projecttype_def b on a.num_angnwadimst_prjtypeid=b.num_projecttype_prjtypeid  and num_projecttype_compid = stateid ";
                        query += " where stateid =" + Session["GrdLevel"];
                        if (ddlPrjType.SelectedValue != "")
                        {
                            query += " and num_angnwadimst_prjtypeid=" + ddlPrjType.SelectedValue;
                        }
                        if (ddlDiv.SelectedValue != "")
                        {
                            query += " and divid=" + ddlDiv.SelectedValue;
                        }
                        if (ddlDist.SelectedValue != "")
                        {
                            query += " and distid =" + ddlDist.SelectedValue;
                        }
                        if (ddlCDPO.SelectedValue != "")
                        {
                            query += " and cdpoid =" + ddlCDPO.SelectedValue;
                        }
                        if (ddlBIT.SelectedValue != "")
                        {
                            query += " and bitid =" + ddlBIT.SelectedValue;
                        }
                    }
                    if (ddlRptType.SelectedValue == "S")
                    {
                        /*query += " select divname Division,distname District, cdponame CDPO, bitbitname BIT,var_sevikamaster_sevikacode Sevika_code,var_sevikamaster_name Sevika_name, ";
                        query += " var_angnwadimst_angnname Anganwadi_name,var_projecttype_prjtype ProjectType, ";
                        query += " var_sevikamaster_cpsmscode CPSMS_code,var_designation_desig Designation from aoup_sevikamaster_def a ";
                        query += " left JOIN aoup_designation_def b on a.num_sevikamaster_desigid = b.num_designation_desigid ";
                        query += " left join aoup_angnwadimst_def c on a.num_sevikamaster_compid=c.num_angnwadimst_compid and a.num_sevakmaster_anganid= c.num_angnwadimst_angnid ";
                        query += " inner join corpinfo on bitid = a.num_sevikamaster_compid ";
                        query += " left join aoup_projecttype_def d on d.num_projecttype_compid=stateid and c.num_angnwadimst_prjtypeid=d.num_projecttype_prjtypeid ";
                        query += " where stateid =" + Session["GrdLevel"];*/
                        query += "  select divname Division,distname District, cdponame CDPO,s.num_company_prjcode Project_Code,cdpocode DDO_Code,bitcode BIT_Code,bitbitname BIT,var_angnwadimst_angnname Anganwadi_name, ";
                        query += " var_sevikamaster_name Sevika_name,var_sevikamaster_middlename Middle_Name,var_sevikamaster_address Address,var_sevikamaster_village Village, ";
                        query += " var_sevikamaster_pincode PIN,var_projecttype_prjtype Project_Type, ";
                        query += " var_sevikamaster_phoneno Phone_No,num_sevikamaster_mobileno Mobile_No,date_sevikamaster_birthdate DOB, ";
                        query += " var_sevikamaster_aadharno Aadhar_No,var_sevikamaster_panno Pan_No, var_sevikamaster_sevikacode Sevika_code,var_education_educname Education, ";
                        query += " var_designation_desig Designation,var_payscal_payscal PayScale, var_sevikamaster_cpsmscode CPSMS_code,date_sevikamaster_joindate DOJ,";
                        query += " var_sevikamaster_orderno Order_No,date_sevikamaster_orderdate Order_Date,date_sevikamaster_retiredate Retirement_Date,num_sevikamaster_experience Experience, ";
                        query += " bankname Bank_Name,ifsccode IFSC_Code,branchname Bank_Branch,''''||(var_sevikamaster_accno) Account_No, ";
                        query += " case when var_sevikamaster_active='Y' then 'Active' ELSE 'Inactive' end as Status,var_sevikamaster_reason Reason, ";
                        query += " var_religion_religname Religion,var_cast_castname Category,var_maritstat_maritstat Martial_Status, ";
                        query += " N1.var_sevikanominee_nom1name Nominee_1_Name,R1.var_relation_relation Nominee_1_Relation,N1.var_sevikanominee_nom1age Nominee_1_Age,N1.var_sevikanominee_nom1address Nominee_1_Address, ";
                        query += " N1.var_sevikanominee_nom2name Nominee_2_Name,R2.var_relation_relation Nominee_2_Relation,N1.var_sevikanominee_nom2age Nominee_2_Age,N1.var_sevikanominee_nom2address Nominee_2_Address ";
                        query += " from aoup_sevikamaster_def a left JOIN aoup_designation_def b on a.num_sevikamaster_desigid = b.num_designation_desigid ";
                        query += " left join aoup_angnwadimst_def c on a.num_sevikamaster_compid=c.num_angnwadimst_compid and a.num_sevakmaster_anganid= c.num_angnwadimst_angnid ";
                        query += " inner join corpinfo on bitid = a.num_sevikamaster_compid left join aoup_projecttype_def d on d.num_projecttype_compid=stateid and c.num_angnwadimst_prjtypeid=d.num_projecttype_prjtypeid ";
                        query += " left join aoup_corporation_mas e on e.num_corporation_corpid=a.num_sevikamaster_compid ";
                        query += " left join aoup_education_def f on f.num_education_educid=a.num_sevikamaster_educid ";
                        query += " left join aoup_payscal_def on num_payscal_compid=stateid and num_payscal_payscalid=num_sevikamaster_payscalid ";
                        query += " left join aoup_maritstat_def on num_maritstat_maritstatid=num_sevikamaster_maritstatid ";
                        query += " left Join aoup_religion_def on num_religion_religid=num_sevikamaster_religid ";
                        query += " left join aoup_cast_def on num_cast_castid=num_sevikamaster_castid ";
                        query += " left join aoup_sevikanominee_def N1 on n1.num_sevikanominee_compid=num_sevikamaster_compid and n1.num_sevikanominee_sevikaid=num_sevikamaster_sevikaid ";
                        query += " left join aoup_relation_def R1 on R1.num_relation_relationid=n1.var_sevikanominee_nom1relaid ";
                        query += " left join aoup_relation_def R2 on R2.num_relation_relationid=n1.var_sevikanominee_nom2relaid ";
                        query += " left join view_bank_branch on num_sevikamaster_branchid=branchid ";
                        query += " LEFT JOIN aoup_corporation_mas s  ON s.num_corporation_corpid = cdpoid ";
                        query += " where stateid =" + Session["GrdLevel"];
                        if (ddlDiv.SelectedValue != "")
                        {
                            query += " and divid=" + ddlDiv.SelectedValue;
                        }
                        if (ddlDist.SelectedValue != "")
                        {
                            query += " and distid =" + ddlDist.SelectedValue;
                        }
                        if (ddlAnganwadi.SelectedValue != "")
                        {
                            query += " and num_sevakmaster_anganid=" + ddlAnganwadi.SelectedValue;
                        }
                        if (ddlCDPO.SelectedValue != "")
                        {
                            query += " and cdpoid =" + ddlCDPO.SelectedValue;
                        }
                        if (ddlBIT.SelectedValue != "")
                        {
                            query += " and bitid =" + ddlBIT.SelectedValue;
                        }
                        if (rbdAuth.Checked == true)
                        {
                            query += " and date_sevikamaster_authdate is not null ";
                        }
                        if (rbdUnauth.Checked == true)
                        {
                            query += " and date_sevikamaster_authdate is null ";
                        }
                        if (rbdReg.Checked == true)
                        {
                            query += " and var_sevikamaster_cpsmscode is not null ";
                        }
                        if (rbdUnreg.Checked == true)
                        {
                            query += " and var_sevikamaster_cpsmscode is null ";
                        }
                        if (rbdWrker.Checked == true)
                        {
                            query += " and var_designation_flag='W' ";
                        }
                        if (rbdHelper.Checked == true)
                        {
                            query += " and var_designation_flag='H' ";
                        }
                        if (txtAadharNo.Text != "")
                        {
                            query += " and var_sevikamaster_aadharno='" + txtAadharNo.Text.Trim() + "'";
                        }
                    }
                    //query += " order by divid,distid,cdpoid,bitid ";
                }
            }
            ViewState["mstSearchQry"] = query;
            ViewState["RptType"] = ddlRptType.SelectedValue.ToString();
            GrdList.QueryStr = query;
            GrdList.NoOfRowsToShow = 100;
            GrdList.FillGridView(1, out recorCount);

            if (recorCount == 0)
            {
                MessageAlert(" Record Not Found ", "");
                btnExport.Visible = false;
                return;
            }
            else
            {
                btnExport.Visible = true;
            }
        }

        #region "Export Excel"
        protected void btnExport_Click(object sender, EventArgs e)
        {
            String XlsName = "MasterSearchList_" + DateTime.Now.ToString("dd-MM-yyyyHHmmss");
            ExportToExcel((DataTable)ViewState["dtlist"], XlsName);
        }

        public void ExportToExcel(DataTable Dt2, String XlsName)
        {
            String query = ViewState["mstSearchQry"].ToString();
            DataTable dt = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            DateTime dd = DateTime.Now;
            string datetime = dd.Day + "" + dd.Month + "" + dd.Year + "" + dd.Hour + "" + dd.Minute + "" + dd.Second;

            StreamWriter sw = new StreamWriter(Server.MapPath("../ImageGarbage/") + "MasterSearchList_" + datetime + ".xls");
            try
            {
                int j = 0;

                String strheader = "";
                String ColumnData = "";
                foreach (DataColumn column in dt.Columns)
                {
                    strheader += column.ColumnName + Convert.ToChar(9);
                }
                while (j < dt.Rows.Count)
                {
                    sw.WriteLine(strheader);

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            j = j + 1;
                            ColumnData += row[column].ToString() + Convert.ToChar(9);
                        }
                        sw.WriteLine(ColumnData);
                        ColumnData = "";
                    }
                }

                sw.Flush();
                sw.Close();
                btnExport.Visible = false;
            }
            catch (Exception ex)
            {
                sw.Flush();
                sw.Close();
                Response.Write("Error : " + ex.Message.Trim());
                return;
            }
            Response.Redirect("../ImageGarbage/" + "MasterSearchList_" + datetime + ".xls");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered (Export To Excel Is not working)*/
        }
        #endregion
    }
}