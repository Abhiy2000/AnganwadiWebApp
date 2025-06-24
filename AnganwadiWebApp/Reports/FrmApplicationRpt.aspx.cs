using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Reports
{
    public partial class FrmApplicationRpt : System.Web.UI.Page
    {
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
                LblGrdHead.Text = "Reports" + " >> " + "Application List";
                filldropdwon();
            }
        }
        protected void filldropdwon()
        {
            MstMethods.Dropdown.Fill(ddlAnganwadi, "tb_anganwadi_mas", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' and var_angnwadimst_active='Y' order by trim(var_angnwadimst_angnname)", "");
            MstMethods.Dropdown.Fill(ddlproj, "tb_project_mas", "var_project_projname", "num_project_projid", " num_project_compid ='" + Session["GrdLevel"].ToString() + "' and var_project_active='Y' order by trim(var_project_projname)", "");
            MstMethods.Dropdown.Fill(ddlMarStatus, "tb_Maristatus_mas", "var_Maristatus_name", "num_Maristatus_id", " num_Maristatus_compid ='" + Session["GrdLevel"].ToString() + "' and var_Maristatus_active='Y' order by trim(var_Maristatus_name)", "");
            MstMethods.Dropdown.Fill(ddlEduQuali, "tb_eduQuli_mas", "var_eduQuli_name", "num_eduQuli_id", " num_eduQuli_compid ='" + Session["GrdLevel"].ToString() + "' and var_eduQuli_active='Y' order by trim(var_eduQuli_name)", "");
            MstMethods.Dropdown.FillAll(ddlStatus, "tb_status", "var_status_name", "num_status_id", "", "");
            ddlStatus.Items.RemoveAt(0);
            bindcompany();
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
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "vw_companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedItem.Text == "-- Select Option --" && ddlproj.SelectedItem.Text == "-- Select Option --"
                && ddlAnganwadi.SelectedItem.Text == "-- Select Option --" && txtReligion.Text == "" && ddlEduQuali.SelectedItem.Text == "-- Select Option --"
                && txtcast.Text == "" && ddlMarStatus.SelectedItem.Text != "-- Select Option --" && txtDisability.Text == "")
            {
                MessageAlert(" || Please Select Atleast One Input ||", "");
                return;
            }

            GetDetails();
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx");
        }
        public void GetDetails()
        {
            string qry = " select COMPID, APPLIID, APPLINO, APPNAME, ADDRESS, PORTID, POSITION, ANGNID, ANGNNAME, EDUQUALID, ";
            qry += " EDUQULI_NAME, MARITALID, MARISTATUS_NAME, PROJID, PROJNAME, DIVID, DIVISION, DISTID, DISTRICT, ";
            qry += " DOB, AGE, AADHARNO, PANNO, SUBCAST, AUTHREMARK, AUTHBY, AUTHDT from vw_applidtls ";
            qry += " where COMPID = '" + Session["GrdLevel"].ToString() + "' ";

            if (ddlDivision.SelectedItem.Text != "-- Select Option --")
            {
                qry += " and DIVID = '" + ddlDivision.SelectedValue.ToString() + "' ";
            }

            if (ddlDistrict.SelectedItem.Text != "-- Select Option --")
            {
                qry += " and DISTID = '" + ddlDistrict.SelectedValue.ToString() + "' ";
            }

            if (ddlproj.SelectedItem.Text != "-- Select Option --")
            {
                qry += " and PROJID = '" + ddlproj.SelectedValue.ToString() + "' ";
            }

            if (ddlAnganwadi.SelectedItem.Text != "-- Select Option --")
            {
                qry += " and ANGNID = '" + ddlAnganwadi.SelectedValue.ToString() + "' ";
            }

            if (txtReligion.Text != "")
            {
                qry += " and RELIGION = '" + txtReligion.Text + "' ";
            }

            if (ddlEduQuali.SelectedItem.Text != "-- Select Option --")
            {
                qry += " and EDUQUALID = '" + ddlEduQuali.SelectedValue.ToString() + "' ";
            }

            if (txtcast.Text != "")
            {
                qry += " and CAST = '" + txtcast.Text + "' ";
            }

            if (ddlMarStatus.SelectedItem.Text != "-- Select Option --")
            {
                qry += " and MARITALID = '" + ddlMarStatus.SelectedValue.ToString() + "' ";
            }

            if (txtDisability.Text != "")
            {
                qry += " and DISABILITY = '" + txtDisability.Text + "' ";
            }

            if (ddlStatus.SelectedValue != "-1")
            {
                qry += " and AUTHSTATUS = '" + ddlStatus.SelectedValue.ToString() + "' ";
            }

            DataTable tblgrdbind = (DataTable)MstMethods.Query2DataTable.GetResult(qry);

            if (tblgrdbind.Rows.Count > 0)
            {
                BtnExportToExcel.Visible = true;
                BtnExportToPdf.Visible = true;
                ViewState["CurrentTable"] = tblgrdbind;
                GrdUserLevel.DataSource = tblgrdbind;
                GrdUserLevel.DataBind();
            }
            else
            {
                GrdUserLevel.DataSource = null;
                GrdUserLevel.DataBind();
                MessageAlert("No record found", "");
            }
        }

        protected void GrdUserLevel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void BtnExportToPdf_Click(object sender, EventArgs e)
        {
            GenerateReport((DataTable)ViewState["CurrentTable"]);
        }
        public void GenerateReport(DataTable tblAbstract)
        {

            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "Application_List_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtApplicationRpt.rpt");
            ExportPath = Server.MapPath("..\\ImageGarbage\\") + PDFNAME;

            String[] Parameter = new String[0];
            String[] ParameterVal = new String[0];


            Byte[] btFile = AnganwadiLib.Methods.MstMethods.Report.ViewReport("", tblAbstract, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");

            Response.AddHeader("Content-disposition", "attachment; filename=" + PDFNAME);

            //Response.ContentType = "application/octet-stream";
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(btFile);
            Response.TransmitFile(ExportPath);
            //Response.End();
        }

        protected void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel((DataTable)ViewState["CurrentTable"], "ApplicationRpt_" + DateTime.Now);
        }
        public void ExportToExcel(DataTable Dt, String XlsName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = Dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + XlsName + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                GridView1.HeaderRow.BackColor = Color.Aqua;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (cell.Text.Length > 10)
                        {

                            cell.CssClass = "textmode";
                        }

                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                    }
                }

                GridView1.RenderControl(hw);

                string style = @"<style> .textmode {  mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered (Export To Excel Is not working)*/
        }
    }
}