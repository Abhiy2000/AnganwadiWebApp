﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;
using System.IO;
using System.Drawing;

namespace AnganwadiWebApp.Reports
{
    public partial class RptDiffAbstractReport : System.Web.UI.Page
    {
        Int32 CentralBill, StateBill, FixedBill;

        #region "MessageAlert"
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
                GrdAbstrct.Visible = false;

                //------------10-03-18
                PnlBillNo.Visible = false;
                btnExport.Visible = false;
                //----------------------------------

                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Master Search";
                MstMethods.Dropdown.Fill(ddlDiv, "companyview", "branchname", "brid", " parentid = " + Session["GrdLevel"].ToString() + " and brcategory=2 order by branchname", "");
                MstMethods.Dropdown.Fill(ddldesg, "aoup_designation_def order by trim(var_designation_desig)", "var_designation_desig", "num_designation_desigid", "", "");
            }
        }

        public void LoadGrid()
        {
            if (ddlDiv.SelectedIndex == 0)
            {
                MessageAlert("Please select Division", "");
                return;
            }
            if (ddldesg.SelectedIndex == 0)
            {
                MessageAlert("Please select Designation", "");
                return;
            }

            try
            {
                DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
                string lstdate = Lastdate.ToString("dd-MMM-yyyy");

                String str = " select divname Division,distname District,var_designation_desig Designation,var_payscal_payscal PayScale, ";
                str += " num_salarydiff_presentdays No_of_Present_Days, count(num_salarydiff_sevikaid) No_of_Beneficiaries, ";
                if (rbd60.Checked == true)
                {
                    str += " round((num_payscal_wages* num_payscal_central/100)/num_salarydiff_totaldays,2) Wage_Rate_Per_Day, ";
                    str += " round(num_salarydiff_presentdays * (num_payscal_wages* num_payscal_central/100)/num_salarydiff_totaldays,0) * count(num_salarydiff_sevikaid) Total ";
                }
                if (rbd40.Checked == true)
                {
                    str += " round((num_payscal_wages* num_payscal_state/100)/num_salarydiff_totaldays,2) Wage_Rate_Per_Day, ";
                    str += " round(num_salarydiff_presentdays * (num_payscal_wages* num_payscal_state/100)/num_salarydiff_totaldays,0) * count(num_salarydiff_sevikaid) Total ";
                }
                
                str += " from aoup_salarydiff_def inner join aoup_sevikamaster_def on  num_sevikamaster_sevikaid=num_salarydiff_sevikaid ";
                str += " inner join corpinfo on bitid=num_salarydiff_compid inner join aoup_designation_def on num_designation_desigid=num_salarydiff_desigid ";
                str += " left join aoup_payscal_def on num_payscal_payscalid=num_salarydiff_payscalid ";
                str += " where date_salarydiff_date='" + lstdate + "'";
                str += " and divid=" + ddlDiv.SelectedValue + "  ";//and var_salary_authorisedby is not null
                if (ddldesg.SelectedIndex != 0)
                {
                    str += " and num_salarydiff_desigid=" + ddldesg.SelectedValue;
                }
                
                str += " and num_salarydiff_presentdays > 0 ";
                if (CentralBill.ToString() != "")
                {
                    str += " and var_salarydiff_billnocentral = '" + CentralBill + "' ";
                }
                if (StateBill.ToString() != "")
                {
                    str += " and var_salarydiff_billnostate = '" + StateBill + "' ";
                }
                if (FixedBill.ToString() != "")
                {
                    str += " and var_salarydiff_billnofixed = '" + FixedBill + "' ";
                }
               
                if (rbdRegistered.Checked == true)
                {
                    str += " and var_sevikamaster_cpsmscode is not null ";
                }
                if (rbdUnregistered.Checked == true)
                {
                    str += " and var_sevikamaster_cpsmscode is null ";
                }
                str += " group by divname,distname,var_designation_desig,var_payscal_payscal,num_payscal_wages,";
                if (rbd60.Checked == true)
                {
                    str += " num_payscal_central, ";
                }
                if (rbd40.Checked == true)
                {
                    str += " num_payscal_state, ";
                }
                
                str += " num_salarydiff_totaldays,num_salarydiff_presentdays ";
                str += " order by var_payscal_payscal,num_salarydiff_presentdays";

                DataTable dtabstract = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                if (dtabstract.Rows.Count > 0)
                {
                    GenerateReport(dtabstract);
                    ViewState["dtabstract"] = dtabstract;
                    // ExportToExcel(dtabstract, "Abstract_Report_as_on_" + DateTime.Now);
                }
               
                else
                {
                    GrdAbstrct.Visible = false;
                    MessageAlert(" Record not Found for selected Date", "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }

        public void LoadGridXls()
        {
            if (ddlDiv.SelectedIndex == 0)
            {
                MessageAlert("Please select Division", "");
                return;
            }
            if (ddldesg.SelectedIndex == 0)
            {
                MessageAlert("Please select Designation", "");
                return;
            }

            try
            {
                DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
                string lstdate = Lastdate.ToString("dd-MMM-yyyy");

                String str = " select divname Division,distname District,var_designation_desig Designation,var_payscal_payscal PayScale, ";
                str += " num_salarydiff_presentdays No_of_Present_Days, count(num_salarydiff_sevikaid) No_of_Beneficiaries, ";
                if (rbd60.Checked == true)
                {
                    str += " round((num_payscal_wages* num_payscal_central/100)/num_salarydiff_totaldays,2) Wage_Rate_Per_Day, ";
                    str += " round(num_salarydiff_presentdays * (num_payscal_wages* num_payscal_central/100)/num_salarydiff_totaldays,0) * count(num_salarydiff_sevikaid) Total ";
                }
                if (rbd40.Checked == true)
                {
                    str += " round((num_payscal_wages* num_payscal_state/100)/num_salarydiff_totaldays,2) Wage_Rate_Per_Day, ";
                    str += " round(num_salarydiff_presentdays * (num_payscal_wages* num_payscal_state/100)/num_salarydiff_totaldays,0) * count(num_salarydiff_sevikaid) Total ";
                }
                str += " from aoup_salarydiff_def inner join aoup_sevikamaster_def on  num_sevikamaster_sevikaid=num_salarydiff_sevikaid ";
                str += " inner join corpinfo on bitid=num_salarydiff_compid inner join aoup_designation_def on num_designation_desigid=num_salarydiff_desigid ";
                str += " left join aoup_payscal_def on num_payscal_payscalid=num_salarydiff_payscalid ";
                str += " where date_salarydiff_date='" + lstdate + "' "; //and var_sevikamaster_cpsmscode is not null
                str += " and divid=" + ddlDiv.SelectedValue + "  "; //and var_salary_authorisedby is not null
                if (ddldesg.SelectedIndex != 0)
                {
                    str += " and num_salarydiff_desigid=" + ddldesg.SelectedValue;
                }
               
                str += " and num_salarydiff_presentdays > 0 ";
                if (CentralBill.ToString() != "")
                {
                    str += " and var_salarydiff_billnocentral = '" + CentralBill + "' ";
                }
                if (StateBill.ToString() != "")
                {
                    str += " and var_salarydiff_billnostate = '" + StateBill + "' ";
                }
                if (FixedBill.ToString() != "")
                {
                    str += " and var_salarydiff_billnofixed = '" + FixedBill + "' ";
                }
                //--------------------------------------------------
                if (rbdRegistered.Checked == true)
                {
                    str += " and var_sevikamaster_cpsmscode is not null ";
                }
                if (rbdUnregistered.Checked == true)
                {
                    str += " and var_sevikamaster_cpsmscode is null ";
                }
                str += " group by divname,distname,var_designation_desig,var_payscal_payscal,num_payscal_wages,";
                if (rbd60.Checked == true)
                {
                    str += " num_payscal_central, ";
                }
                if (rbd40.Checked == true)
                {
                    str += " num_payscal_state, ";
                }
                
                str += " num_salarydiff_totaldays,num_salarydiff_presentdays ";
                str += " order by var_payscal_payscal,num_salarydiff_presentdays";

                DataTable dtabstract = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                if (dtabstract.Rows.Count > 0)
                {
                    ViewState["dtabstract"] = dtabstract;
                    ExportToExcel(dtabstract, "Abstract_Report_as_on_" + DateTime.Now);
                }
                
                else
                {
                    GrdAbstrct.Visible = false;
                    MessageAlert(" Record not Found for selected Date", "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }
        public void BindDropdrownlist()
        {
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("January", "1"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("February", "2"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("March", "3"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("April", "4"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("May", "5"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("June", "6"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("July", "7"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("August", "8"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("September", "9"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("October", "10"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("November", "11"));
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("December", "12"));

            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(DateTime.Now.Year.ToString(), "1"));
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem((DateTime.Now.Year - 1).ToString(), "2"));
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem((DateTime.Now.Year - 2).ToString(), "3"));
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();

            ddlYear.SelectedItem.Text = System.DateTime.Now.Year.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (ddlDiv.SelectedIndex == 0)
            //{
            //    MessageAlert("Please select Division", "");
            //    return;
            //}
            //if (ddldesg.SelectedIndex == 0)
            //{
            //    MessageAlert("Please select Designation", "");
            //    return;
            //}

            //LoadGrid();
        }

        #region "ExportExcel"
        protected void btnExport_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
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

        #endregion

        
        public void BillNo()
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            DateTime Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            string lstdate = Lastdate.ToString("dd-MMM-yyyy");

            string billdet = " select var_salarydiff_billnocentral Central,var_salarydiff_billnostate State,var_salarydiff_billnofixed Fixed from aoup_salarydiff_def ";
            billdet += " left join corpinfo on num_salarydiff_compid=bitid ";
            billdet += " where stateid =" + TblGetHoId.Rows[0]["hoid"].ToString();
            if (ddlDiv.SelectedValue != "")
            {
                billdet += " and divid=" + Convert.ToInt32(ddlDiv.SelectedValue);
            }
            billdet += " and trunc(date_salarydiff_date)='" + lstdate + "' ";//num_salary_compid=1559 and
            if (rbd40.Checked == true)
            {
                billdet += " and var_salarydiff_billnostate is not null ";
            }
            if (rbd60.Checked == true)
            {
                billdet += "  and var_salarydiff_billnocentral is not null ";
            }
           
            billdet += " and num_salarydiff_desigid=" + ddldesg.SelectedValue;
            if (rbdRegistered.Checked == true)
            {
                billdet += " and var_salarydiff_cpsmscode is not null ";
            }
            if (rbdUnregistered.Checked == true)
            {
                billdet += " and var_salarydiff_cpsmscode is null ";
            }
            billdet += " GROUP by var_salarydiff_billnocentral,var_salarydiff_billnostate,var_salarydiff_billnofixed ";

            DataTable dtstr = (DataTable)MstMethods.Query2DataTable.GetResult(billdet);

            if (dtstr.Rows.Count > 0)
            {
                grdBillno.DataSource = dtstr;
                grdBillno.DataBind();
                PnlBillNo.Visible = true;
            }
            else
            {
                grdBillno.DataSource = null;
                grdBillno.DataBind();
                PnlBillNo.Visible = false;
                MessageAlert("Record Not found", "");
                return;
            }
        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
            HiddenField hdnCentral = ((HiddenField)grdBillno.Rows[rindex].FindControl("hdnCentral"));
            HiddenField hdnState = ((HiddenField)grdBillno.Rows[rindex].FindControl("hdnState"));
            HiddenField hdnFixed = ((HiddenField)grdBillno.Rows[rindex].FindControl("hdnFixed"));

            CentralBill = Convert.ToInt32(hdnCentral.Value);
            StateBill = Convert.ToInt32(hdnState.Value);
            FixedBill = Convert.ToInt32(hdnFixed.Value);
            LoadGrid();
        }

        protected void lnkExl_Click(object sender, EventArgs e)
        {
            int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
            HiddenField hdnCentral1 = ((HiddenField)grdBillno.Rows[rindex].FindControl("hdnCentral1"));
            HiddenField hdnState2 = ((HiddenField)grdBillno.Rows[rindex].FindControl("hdnState2"));
            HiddenField hdnFixed3 = ((HiddenField)grdBillno.Rows[rindex].FindControl("hdnFixed3"));

            CentralBill = Convert.ToInt32(hdnCentral1.Value);
            StateBill = Convert.ToInt32(hdnState2.Value);
            FixedBill = Convert.ToInt32(hdnFixed3.Value);
            LoadGridXls();
        }

        protected void grdBillno_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdBillno.SelectedRow;
            CentralBill = Convert.ToInt32(row.Cells[1].Text);
            StateBill = Convert.ToInt32(row.Cells[2].Text);
            FixedBill = Convert.ToInt32(row.Cells[3].Text);

            LoadGrid();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlDiv.SelectedValue == "")
            {
                MessageAlert("Please select Division", "");
                return;
            }
            if (ddldesg.SelectedValue == "")
            {
                MessageAlert("Please select Designation", "");
                return;
            }
            BillNo();
        }
      

        public void GenerateReport(DataTable tblAbstract)
        {
            string month = ddlMonth.SelectedItem.Text.Trim();
            string yr = ddlYear.SelectedItem.Text.Trim();
            String ReportPath = "";
            String ExportPath = "";
            String PDFNAME = "Abstract_Report_as_on_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            String creatfpdf = Server.MapPath(@"~/ImageGarbage/");
            var finalPDF = System.IO.Path.Combine(creatfpdf, PDFNAME);

            ReportPath = Server.MapPath("~\\Reports\\CrtAbstractRpt.rpt");
            ExportPath = Server.MapPath("..\\ImageGarbage\\") + PDFNAME;

            String[] Parameter = new String[3];
            String[] ParameterVal = new String[3];

            Parameter[0] = "SelDate";
            Parameter[1] = "UserId";
            Parameter[2] = "Selection";

            ParameterVal[0] = month + '-' + yr;
            ParameterVal[1] = Session["UserId"].ToString();
            if (rbd40.Checked == true)
            {
                ParameterVal[2] = "40%";
            }
            if (rbd60.Checked == true)
            {
                ParameterVal[2] = "60%";
            }

            tblAbstract.TableName = "tblAbstract";

            Byte[] btFile = AnganwadiLib.Methods.MstMethods.Report.ViewReport("", tblAbstract, ReportPath, ExportPath, Parameter, ParameterVal, "pdf", "", "", "", "", "", "");

            Response.AddHeader("Content-disposition", "attachment; filename=" + PDFNAME);

            //Response.ContentType = "application/octet-stream";
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(btFile);
            Response.TransmitFile(ExportPath);
            //Response.End();
        }
    }
}