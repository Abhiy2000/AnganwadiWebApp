using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Reports
{
    public partial class RptPolicyUpdateRpt : System.Web.UI.Page
    {
        String Type = "";
        String StatusType = "";

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

            LblGrdHead.Text = "Reports >> Policy Update Report";//Session["LblGrdHead"].ToString();// "Savika List Report";

            if (!IsPostBack)
            {
                string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
                DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);
                if (TblGetHoId.Rows.Count > 0)
                {
                    MstMethods.Dropdown.Fill(ddlPrjType, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "num_projecttype_compid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " order by num_projecttype_prjtypeid ", "");
                }
                MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "'", "");

                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();
                // String str = "select brcategory, parentid from companyview where brid =13";
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
                }
            }
        }

        //protected void ddlDiv_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MstMethods.Dropdown.Fill(ddlDist, "companyview", "branchname", "brid", "parentid=" + ddlDiv.SelectedValue + "", "");
        //}

        //protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MstMethods.Dropdown.Fill(ddlCdpo, "companyview", "branchname", "brid", "parentid=" + ddlDist.SelectedValue + "", "");
        //}

        //protected void ddlCdpo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MstMethods.Dropdown.Fill(ddlBit, "companyview", "branchname", "brid", "parentid=" + ddlCdpo.SelectedValue + "", "");
        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    if (ddlDiv.SelectedValue == "")
        //    {
        //        MessageAlert(" Please Select Division ", "");
        //        ddlDiv.Focus();
        //        return;
        //    }
        //    if (ddlDist.SelectedValue == "")
        //    {
        //        MessageAlert(" Please Select District ", "");
        //        ddlDist.Focus();
        //        return;
        //    }
        //    LoadGrid();
        //}

        #region "Export Excel"
        protected void btnExport_Click1(object sender, EventArgs e)
        {
            String XlsName = "_PolicyUpdateReprt_" + DateTime.Now.ToString("dd-MM-yyyyHHmmss");
            ExportToExcel((DataTable)ViewState["dtlist"], XlsName);
        }

        public void ExportToExcel(DataTable Dt, String XlsName)
        {
            if (ddlDivision.SelectedValue == "")
            {
                MessageAlert("Select valid Division from the list", "");
                return;
            }

            String query = (String)GetQuery(ddlDivision.SelectedValue.ToString(), ddlDistrict.SelectedValue.ToString(), ddlCDPO.SelectedValue.ToString(),
            ddlBeat.SelectedValue.ToString(), ddlAnganID.SelectedValue.ToString(), ddlPrjType.SelectedValue.ToString(), Type, StatusType);

            DataTable dt = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            DateTime dd = DateTime.Now;
            string datetime = dd.Day + "" + dd.Month + "" + dd.Year + "" + dd.Hour + "" + dd.Minute + "" + dd.Second;

            StreamWriter sw = new StreamWriter(Server.MapPath("../ImageGarbage/") + "PolicyUpdateReprt_" + datetime + ".xls");
            try
            {
                //                DIVISION, DISTRICT, CDPONAME, DDOCODE, BITCODE, BIT, ANGANWADI, SEVIKACODE, 
                //SEVIKA_NAME, EXPERIENCE, DESIGNATION, PAYSCALE, AADHARNO, CPSMS_BENEFICIARY_CODE, 
                //SCHEME_SPECIFIC_CODE, STATUS, PMJJBYNO, PMSBYNO, DIVID, DISTID, CDPOID, 
                //BITID, ANGANID, CPSMSCODE

                int j = 0;
                String strheader = "Division" + Convert.ToChar(9)
                    + "District" + Convert.ToChar(9)
                    + "CDPO Name" + Convert.ToChar(9)
                    + "DDOCode" + Convert.ToChar(9)
                    + "BITCODE" + Convert.ToChar(9)
                    + "BIT" + Convert.ToChar(9)
                    + "ANGANWADI" + Convert.ToChar(9)
                    + "SEVIKA CODE" + Convert.ToChar(9)
                    + "SEVIKA NAME" + Convert.ToChar(9)
                    + "EXPERIENCE" + Convert.ToChar(9)
                    + "DESIGNATION" + Convert.ToChar(9)
                    + "PAY SCALE" + Convert.ToChar(9)
                    + "AADHAR NO" + Convert.ToChar(9)
                    + "CPSMS BENEFICIARY CODE" + Convert.ToChar(9)
                    + "SCHEME SPECIFIC CODE" + Convert.ToChar(9)
                    + "STATUS" + Convert.ToChar(9)
                    + "PMJJBYNO" + Convert.ToChar(9)
                    + "PMSBYNO" + Convert.ToChar(9);

                while (j < dt.Rows.Count)
                {
                    sw.WriteLine(strheader);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        j = j + 1;
                        string strrow = dt.Rows[i]["DIVISION"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["DISTRICT"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["CDPONAME"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["DDOCODE"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["BITCODE"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["BIT"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["ANGANWADI"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["SEVIKACODE"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["SEVIKA_NAME"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["EXPERIENCE"].ToString() + Convert.ToChar(9)
                             + dt.Rows[i]["DESIGNATION"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["PAYSCALE"].ToString() + Convert.ToChar(9)
                            + "'" + dt.Rows[i]["AADHARNO"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["CPSMS_BENEFICIARY_CODE"].ToString() + Convert.ToChar(9)
                            + "'" + dt.Rows[i]["SCHEME_SPECIFIC_CODE"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["STATUS"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["PMJJBYNO"].ToString() + Convert.ToChar(9)
                            + dt.Rows[i]["PMSBYNO"].ToString() + Convert.ToChar(9);
                        sw.WriteLine(strrow);
                    }
                }

                sw.Flush();
                sw.Close();
            }

            catch (Exception ex)
            {
                sw.Flush();
                sw.Close();
                Response.Write("Error : " + ex.Message.Trim());
                return;
            }
            Response.Redirect("../ImageGarbage/" + "PolicyUpdateReprt_" + datetime + ".xls");

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered (Export To Excel Is not working)*/
        }
        #endregion

        public static object GetQuery(String DivisionId, String DistrictId, String CDPOId, String BeatId, String AnganWadiId, String PrjTypeId, String Type, String StatusType)
        {
            //String qry = " SELECT divname Division,distname District,cdpocode DDOCode,bitcode BITCode,bitbitname BIT, b.var_angnwadimst_angnname Anganwadi,num_sevikamaster_sevikaid SevikaCode, ";
            //qry += " a.var_sevikamaster_name Sevika_Name,replace(var_sevikamaster_address,chr(10),'') Address,num_sevikamaster_mobileno MobileNo,to_char(date_sevikamaster_birthdate,'dd-MM-yyyy') DOB, ";
            //qry += " to_char(date_sevikamaster_joindate,'dd-MM-yyyy') JoinDate,to_char(date_sevikamaster_retiredate,'dd-MM-yyyy') Retirement_Date,var_experience_text Experience,";
            //qry += " d.var_designation_desig Designation,var_education_educname Education,var_maritstat_maritstat Martial_Status, ";
            //qry += " var_religion_religname Religion,null Caste,var_cast_castname Category,var_sevikamaster_accno BankAcNo,var_sevikamaster_cpsmscode CPSMS_Beneficiary_Code, ";
            //qry += " var_sevikamaster_sevikacode Scheme_Specific_Code,case when var_sevikamaster_active='Y' then 'Active' ELSE 'Inactive' end as Status ";
            //qry += " ,var_payscal_payscal PayScale, cdponame CDPONM,var_sevikamaster_aadharno AadharNo, var_sevikamaster_reason Reason ";//-----------24-04-18
            //qry += " FROM aoup_sevikamaster_def a left join aoup_AngnwadiMst_def b  on a.num_sevakmaster_anganid=b.num_angnwadimst_angnid and a.num_sevikamaster_compid=b.num_angnwadimst_compid  ";
            //qry += " left Join aoup_experience_def on num_experience_id=num_sevikamaster_experience "; //--- added on 08/09/18
            //qry += " left join aoup_designation_def d on num_sevikamaster_desigid=d.num_designation_desigid  left join corpinfo e on a.num_sevikamaster_compid=e.bitid  ";
            ////qry += " inner join aoup_projecttype_def c on c.num_projecttype_compid=e.stateid and b.num_angnwadimst_prjtypeid=c.num_projecttype_prjtypeid "; commented on 10-04-18
            //qry += " left Join aoup_education_def on  num_education_educid=num_sevikamaster_educid left join aoup_maritstat_def on num_maritstat_maritstatid=num_sevikamaster_maritstatid ";
            //qry += " left Join aoup_religion_def on num_religion_religid=num_sevikamaster_religid left join aoup_cast_def on num_cast_castid=num_sevikamaster_castid ";
            //qry += " left join aoup_corporation_mas on num_corporation_corpid=bitid ";
            //qry += " left join aoup_payscal_def on num_payscal_compid=stateid and num_payscal_payscalid=num_sevikamaster_payscalid ";//------------24-04-18
            //qry += " where e.divid=" + DivisionId + " ";

            string qry = "";
            qry += "select DIVISION, DISTRICT, CDPONAME, DDOCODE, BITCODE, BIT, ANGANWADI, SEVIKACODE, SEVIKA_NAME, EXPERIENCE, DESIGNATION, PAYSCALE, AADHARNO, CPSMS_BENEFICIARY_CODE, ";
            qry += " SCHEME_SPECIFIC_CODE, STATUS, PMJJBYNO, PMSBYNO, DIVID, DISTID, CDPOID, BITID, ANGANID, CPSMSCODE ";
            qry += " from view_policyupdtlist where divid='" + DivisionId + "' ";

            if (DistrictId != "")
            {
                qry += " and DISTID=" + DistrictId + " ";
            }

            if (CDPOId != "")
            {
                qry += " and CDPOID=" + CDPOId + " ";
            }

            if (BeatId != "")
            {
                qry += " and BITID=" + BeatId + " ";
            }

            if (AnganWadiId != "")
            {
                qry += " and ANGANID=" + AnganWadiId + " ";
            }
            //if (PrjTypeId != "")
            //{
            //    qry += "and num_projecttype_prjtypeid=" + PrjTypeId + " ";
            //}
            if (Type == "R")
            {
                qry += " and CPSMSCODE is not null ";
            }
            if (Type == "U")
            {
                qry += " and CPSMSCODE is null ";
            }
            return qry;
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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
                // MstMethods.Dropdown.Fill(ddlAnganID, "companyview", "branchname", "brid", " parentid = " + ddlBeat.SelectedValue.ToString() + " and brcategory = 5 order by branchname", "");
                //String qry = "  select var_angnwadimst_angnname,num_angnwadimst_angnid from aoup_angnwadimst_def a ";                
                //qry += " inner join companyview b on a.num_angnwadimst_compid=b.brid where a.num_angnwadimst_compid='"+ ddlBeat.SelectedValue.ToString()+ "'";

                MstMethods.Dropdown.Fill(ddlAnganID, "aoup_angnwadimst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid = " + ddlBeat.SelectedValue.ToString() + "", "");
            }
        }

        protected void ddlAnganID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAnganID.SelectedValue.ToString() != "")
            {
                Session["GrdLevel"] = ddlAnganID.SelectedValue.ToString();
            }
        }

        protected void GrdSevikaList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            //{
            //}
            //e.Row.Cells[0].Visible = true;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = true;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = true;
            //e.Row.Cells[5].Visible = true;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[8].Visible = true;
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = true;
            //e.Row.Cells[11].Visible = true;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
            //e.Row.Cells[15].Visible = false;
        }

        protected void GrdSevikaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = GrdSevikaList.SelectedRow;
            //Session["SevikaId"] = row.Cells[1].Text;
            //Response.Redirect("../Transaction/FrmSevikaMaster.aspx?@=2");
        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue == "")
            {
                MessageAlert("Select valid Division from the list", "");
                return;
            }

            Int64 recorCount = 0;

            String query = (String)GetQuery(ddlDivision.SelectedValue.ToString(), ddlDistrict.SelectedValue.ToString(), ddlCDPO.SelectedValue.ToString(),
                ddlBeat.SelectedValue.ToString(), ddlAnganID.SelectedValue.ToString(), ddlPrjType.SelectedValue.ToString(), Type, StatusType);

            GrdSevikaList.QueryStr = query;
            GrdSevikaList.NoOfRowsToShow = 100;
            GrdSevikaList.FillGridView(1, out recorCount);

            if (recorCount == 0)
            {
                MessageAlert(" Record Not Found ", "");
                return;
            }
        }
    }
}