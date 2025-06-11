using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.HomePage
{
    public partial class tablegraph : System.Web.UI.Page
    {
        decimal totalinactive = (decimal)0.0;
        decimal totaldocupload = (decimal)0.0;
        decimal totalDocApprove = (decimal)0.0;
        decimal Divtotalinactive = (decimal)0.0;
        decimal Divtotaldocupload = (decimal)0.0;
        decimal DivtotalDocApprove = (decimal)0.0;
        //decimal totalProcessed = (decimal)0.0;
        //decimal totalPaid = (decimal)0.0;

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

            if (!IsPostBack)
            {

                UserSession();
                GetHOList();

                // GrdDivision.Visible = false;
                //GrdDistrict.Visible = false;
                //GrdCDPO.Visible = false;
                //GrdBit.Visible = false;
            }
           
        }

        public void UserSession()
        {
            //Session["UserId"] = "ICDSAdmin";
            //Session["CompId"] = 11;
            //Session["brid"] = 11;
            //Session["UserFullName"] = "ICDS Administrator";
            //Session["brcategory"] = "1";
            //Session["lastlogin"] = "7-Jan-2021 09:01:08";
            //Session["lastlogout"] = "7-Jan-2021 09:38:24";
            //Session["lastchangepwd"] = null;
            //Session["DesgId"] = 1;
            //Session["branchname"] = "ACCOUNT";
            //Session["companyname"] = "Maharashtra";
            //Session["desgname"] = "";
            //Session["GrdLevel"] = "11";
        }
        public void GetHOList()
        {
            string qry = "";
            Int32 DivID = 0;
            Int32 DistID = 0;
            Int32 CdpoID = 0;

            int BrCategory = Convert.ToInt32(Session["brcategory"]);
            if (BrCategory == 2)
            {
                qry += "select divid from corpinfo where divid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 3)
            {
                qry += "select divid from corpinfo where distid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 4)
            {
                qry += "select divid,distid from corpinfo where cdpoid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 5)
            {
                qry += "select divid,distid,cdpoid from corpinfo where bitid=" + Session["GrdLevel"] + "";
            }
            if (qry != "")
            {
                DataTable tbldivid = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
                if (tbldivid.Rows.Count > 0)
                {
                    DivID = Convert.ToInt32(tbldivid.Rows[0]["divid"]);
                    if (BrCategory == 4)
                    {
                        DistID = Convert.ToInt32(tbldivid.Rows[0]["distid"]);
                    }

                    if (BrCategory == 5)
                    {
                        DistID = Convert.ToInt32(tbldivid.Rows[0]["distid"]);
                        CdpoID = Convert.ToInt32(tbldivid.Rows[0]["cdpoid"]);
                    }

                }
            }

            String Query = "  Select divid, divname, sum(inactive_count) inactive_count, sum(docupload_count) docupload_count,  ";
            Query += " sum(DocApprove_count) DocApprove_count, sum(LIC_Processed) LIC_Processed, sum(LIC_Paid) LIC_Paid, sum(HO_Approval) HO_Approval ";
            Query += " from ( select divid, divname, distid,distname,cdpoid,cdponame,bitid,bitbitname,var_lic_approvflag, ";
            Query += " case when var_lic_approvflag is null or var_lic_approvflag <>'R' then 1 else 0 end Inactive_count, ";
            Query += " case when img_lic_document is not null and var_lic_approvflag <>'R' then 1 else 0 end DocUpload_count, ";
            Query += " case when var_lic_approvflag = 'A' then 1 else 0 end DocApprove_count, ";  //var_lic_hoapprovflag = 'A'
            Query += " case when var_lic_licapprovflag = 'A' then 1 else 0 end LIC_Processed, ";
            Query += " case when var_lic_flag_payment = 'P' then 1 else 0 end LIC_Paid,  ";
            Query += " case when var_lic_hoapprovflag = 'A' then 1 else 0 end HO_Approval ";
            Query += " From corpinfo inner join aoup_lic_def on bitid = num_lic_compid ";
            Query += " where stateid=11 ";
            if (DivID != 0)
            {
                Query += " and divid=" + DivID + "";
            }
            Query += " ) Group by divid, divname Order by divname ";

            DataTable TblList = (DataTable)MstMethods.Query2DataTable.GetResult(Query);

            if (TblList.Rows.Count > 0)
            {

                GrdDashboard.DataSource = TblList;
                GrdDashboard.DataBind();
                //GrdDivision.Visible = false;
                if (TblList.Rows.Count >= 0)
                {
                    if (BrCategory == 2)
                    {
                        GetDivisionList(TblList.Rows[0]["divid"].ToString());
                        GrdDivision.Visible = true;
                    }
                    else if (BrCategory == 3)
                    {
                        GetDistrictList(DivID.ToString(), Session["GrdLevel"].ToString());
                        GrdDivision.Visible = false;
                        GrdDistrict.Visible = true;
                    }
                    else if (BrCategory == 4)
                    {
                        GetCDPOList(DivID.ToString(), DistID.ToString(), Session["GrdLevel"].ToString());
                        GrdDivision.Visible = false;
                        GrdDistrict.Visible = false;
                        GrdCDPO.Visible = true;
                    }
                    else if (BrCategory == 5)
                    {
                        GetBITList(DivID.ToString(), DistID.ToString(), CdpoID.ToString(), Session["GrdLevel"].ToString());
                        GrdDivision.Visible = false;
                        GrdDistrict.Visible = false;
                        GrdCDPO.Visible = false;
                        GrdBit.Visible = true;
                    }
                    else
                    {
                        GetDivisionList(TblList.Rows[0]["divid"].ToString());
                        GrdDivision.Visible = true;
                        GrdDistrict.Visible = false;
                        GrdCDPO.Visible = false;
                        GrdBit.Visible = false;
                    }
                }


                lblInactive.Text = TblList.Compute("SUM(inactive_count)", string.Empty).ToString();
                lblDocUpload.Text = TblList.Compute("SUM(docupload_count)", string.Empty).ToString();
                lblDocApproval.Text = TblList.Compute("SUM(DocApprove_count)", string.Empty).ToString();
                lblLicProcess.Text = TblList.Compute("SUM(LIC_Processed)", string.Empty).ToString();
                lblLicPayment.Text = TblList.Compute("SUM(LIC_Paid)", string.Empty).ToString();
                lblHOApproval.Text = TblList.Compute("SUM(HO_Approval)", string.Empty).ToString();
            }
            else
            {
                GrdDashboard.DataSource = null;
                GrdDashboard.DataBind();
                GrdDivision.Visible = false;
                GrdDistrict.Visible = false;
                GrdCDPO.Visible = false;
                GrdBit.Visible = false;
                MessageAlert("Record Not found", "");
                return;
            }
        }

        public void GetDivisionList(string hdnDivId)
        {
            string qry = "";
            Int32 DistID = 0;
            int BrCategory = Convert.ToInt32(Session["brcategory"]);

            if (BrCategory == 3)
            {
                qry += "select distid from corpinfo where distid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 4)
            {
                qry += "select distid from corpinfo where cdpoid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 5)
            {
                qry += "select distid from corpinfo where bitid=" + Session["GrdLevel"] + "";
            }
            if (qry != "")
            {
                DataTable tbldistid = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
                if (tbldistid.Rows.Count > 0)
                {
                    DistID = Convert.ToInt32(tbldistid.Rows[0]["distid"]);
                }
            }
            String Query = "  select divid,distid, distname, sum(inactive_count) inactive_count, sum(docupload_count) docupload_count, sum(DocApprove_count) DocApprove_count, ";
            Query += " sum(LIC_Processed) LIC_Processed, sum(LIC_Paid) LIC_Paid, sum(HO_Approval) HO_Approval ";
            Query += " from ( select divid, divname, distid,distname,cdpoid,cdponame,bitid,bitbitname,var_lic_approvflag, ";
            Query += " case when var_lic_approvflag is null or var_lic_approvflag <>'R' then 1 else 0 end Inactive_count, ";
            Query += " case when img_lic_document is not null and var_lic_approvflag <>'R' then 1 else 0 end DocUpload_count, ";
            Query += " case when var_lic_approvflag = 'A' then 1 else 0 end DocApprove_count, ";
            Query += " case when var_lic_licapprovflag = 'A' then 1 else 0 end LIC_Processed, ";
            Query += " case when var_lic_flag_payment = 'P' then 1 else 0 end LIC_Paid,  ";
            Query += " case when var_lic_hoapprovflag = 'A' then 1 else 0 end HO_Approval ";
            Query += " From corpinfo inner join aoup_lic_def on bitid = num_lic_compid ";
            Query += " where stateid=11 and divid=" + hdnDivId + "  ";
            if (DistID != 0)
            {
                Query += " and distid=" + DistID + "";
            }
            Query += " ) Group by divid,distid, distname Order by distname  ";

            DataTable TblList = (DataTable)MstMethods.Query2DataTable.GetResult(Query);

            if (TblList.Rows.Count > 0)
            {

                lblInactive.Text = TblList.Compute("SUM(inactive_count)", string.Empty).ToString();
                lblDocUpload.Text = TblList.Compute("SUM(docupload_count)", string.Empty).ToString();
                lblDocApproval.Text = TblList.Compute("SUM(DocApprove_count)", string.Empty).ToString();
                lblLicProcess.Text = TblList.Compute("SUM(LIC_Processed)", string.Empty).ToString();
                lblLicPayment.Text = TblList.Compute("SUM(LIC_Paid)", string.Empty).ToString();
                lblHOApproval.Text = TblList.Compute("SUM(HO_Approval)", string.Empty).ToString();

                //GrdDashboard.Visible = false;
                GrdDivision.DataSource = TblList;
                GrdDivision.DataBind();
                GrdDistrict.Visible = false;
                GrdCDPO.Visible = false;
                GrdBit.Visible = false;
            }
            else
            {
                //GrdDashboard.Visible = true;
                GrdDivision.DataSource = null;
                GrdDivision.DataBind();
                GrdDistrict.Visible = false;
                GrdCDPO.Visible = false;
                GrdBit.Visible = false;

                MessageAlert("Record Not found", "");
                return;
            }
        }

        public void GetDistrictList(string hdnDivId, string hdnDistId)
        {
            string qry = "";
            //Int32 CdpoID = 0;
            String CdpoID = "";
            int BrCategory = Convert.ToInt32(Session["brcategory"]);

            if (BrCategory == 3)
            {
                qry += "select DISTINCT(cdpoid) from corpinfo where distid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 4)
            {
                qry += "select DISTINCT(cdpoid) from corpinfo where cdpoid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 5)
            {
                qry += "select DISTINCT(cdpoid) from corpinfo where bitid=" + Session["GrdLevel"] + "";
            }
            if (qry != "")
            {
                DataTable tbldistid = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
                //if (tbldistid.Rows.Count > 0)
                //{
                //    CdpoID = Convert.ToInt32(tbldistid.Rows[0]["cdpoid"]);
                //}


                for (int i = 0; i < tbldistid.Rows.Count; i++)
                {
                    CdpoID += "" + Convert.ToInt32(tbldistid.Rows[i]["cdpoid"]) + ",";
                }
                CdpoID = CdpoID.Remove(CdpoID.Length - 1);
            }

            String Query = "  select divid,distid,cdpoid, cdponame, sum(inactive_count) inactive_count, sum(docupload_count) docupload_count, sum(DocApprove_count) DocApprove_count, ";
            Query += " sum(LIC_Processed) LIC_Processed, sum(LIC_Paid) LIC_Paid, sum(HO_Approval) HO_Approval ";
            Query += " from ( select divid, divname, distid,distname,cdpoid,cdponame,bitid,bitbitname,var_lic_approvflag, ";
            Query += " case when var_lic_approvflag is null or var_lic_approvflag <>'R' then 1 else 0 end Inactive_count , ";
            Query += " case when img_lic_document is not null and var_lic_approvflag <>'R' then 1 else 0 end DocUpload_count, ";
            Query += " case when var_lic_approvflag = 'A' then 1 else 0 end DocApprove_count, ";
            Query += " case when var_lic_licapprovflag = 'A' then 1 else 0 end LIC_Processed, ";
            Query += " case when var_lic_flag_payment = 'P' then 1 else 0 end LIC_Paid,  ";
            Query += " case when var_lic_hoapprovflag = 'A' then 1 else 0 end HO_Approval ";
            Query += " From corpinfo inner join aoup_lic_def on bitid = num_lic_compid ";
            Query += " where stateid=11 and divid=" + hdnDivId + " and distid=" + hdnDistId + "  ";
            if (CdpoID != "")
            {
                Query += " and cdpoid in (" + CdpoID + ")";
            }
            Query += " ) Group by divid,distid,cdpoid, cdponame  Order by cdponame ";

            DataTable TblList = (DataTable)MstMethods.Query2DataTable.GetResult(Query);

            if (TblList.Rows.Count > 0)
            {

                lblInactive.Text = TblList.Compute("SUM(inactive_count)", string.Empty).ToString();
                lblDocUpload.Text = TblList.Compute("SUM(docupload_count)", string.Empty).ToString();
                lblDocApproval.Text = TblList.Compute("SUM(DocApprove_count)", string.Empty).ToString();
                lblLicProcess.Text = TblList.Compute("SUM(LIC_Processed)", string.Empty).ToString();
                lblLicPayment.Text = TblList.Compute("SUM(LIC_Paid)", string.Empty).ToString();
                lblHOApproval.Text = TblList.Compute("SUM(HO_Approval)", string.Empty).ToString();

                GrdDistrict.DataSource = TblList;
                GrdDistrict.DataBind();
                //GrdDashboard.Visible = false;
                GrdDivision.Visible = false;
                GrdDistrict.Visible = true;
                GrdCDPO.Visible = false;
                GrdBit.Visible = false;
            }
            else
            {
                //GrdDashboard.Visible = false;
                GrdDivision.Visible = true;
                GrdDistrict.Visible = false;
                GrdCDPO.Visible = false;
                GrdBit.Visible = false;
                GrdDistrict.DataSource = null;
                GrdDistrict.DataBind();
                MessageAlert("Record Not found", "");
                return;
            }
        }

        public void GetCDPOList(string divid, string distid, string cdpoid)
        {
            string qry = "";
            //Int32 BitID = 0;
            string BitID = "";
            int BrCategory = Convert.ToInt32(Session["brcategory"]);

            if (BrCategory == 4)
            {
                qry += "select bitid from corpinfo where cdpoid=" + Session["GrdLevel"] + "";
            }
            else if (BrCategory == 5)
            {
                qry += "select bitid from corpinfo where bitid=" + Session["GrdLevel"] + "";
            }
            if (qry != "")
            {
                DataTable tbldistid = (DataTable)MstMethods.Query2DataTable.GetResult(qry);
                //if (tbldistid.Rows.Count > 0)
                //{
                //    BitID = Convert.ToInt32(tbldistid.Rows[0]["bitid"]);
                //}

                for (int i = 0; i < tbldistid.Rows.Count; i++)
                {
                    BitID += "" + Convert.ToInt32(tbldistid.Rows[i]["bitid"]) + ",";
                }
                BitID = BitID.Remove(BitID.Length - 1);
            }
            String Query = "  select divid,distid,cdpoid,bitid, bitbitname, sum(inactive_count) inactive_count, sum(docupload_count) docupload_count, sum(DocApprove_count) DocApprove_count, ";
            Query += " sum(LIC_Processed) LIC_Processed, sum(LIC_Paid) LIC_Paid,SUM(HO_Approval) HO_Approval ";
            Query += " from ( select divid, divname, distid,distname,cdpoid,cdponame,bitid,bitbitname,var_lic_approvflag, ";
            Query += " case when var_lic_approvflag is null or var_lic_approvflag <>'R' then 1 else 0 end Inactive_count, ";
            Query += " case when img_lic_document is not null and var_lic_approvflag <>'R' then 1 else 0 end DocUpload_count, ";
            Query += " case when var_lic_approvflag = 'A' then 1 else 0 end DocApprove_count, ";
            Query += " case when var_lic_licapprovflag = 'A' then 1 else 0 end LIC_Processed, ";
            Query += " case when var_lic_flag_payment = 'P' then 1 else 0 end LIC_Paid, ";
            Query += " case when var_lic_hoapprovflag = 'A' then 1 else 0 end HO_Approval ";
            Query += " From corpinfo inner join aoup_lic_def on bitid = num_lic_compid ";
            Query += " where stateid=11 and divid=" + divid + " and distid=" + distid + " and cdpoid=" + cdpoid + "  ";
            if (BitID != "")
            {
                Query += " and bitid in (" + BitID + ")";
            }
            Query += " ) Group by divid,distid,cdpoid,bitid, bitbitname Order  by bitbitname ";

            DataTable TblList = (DataTable)MstMethods.Query2DataTable.GetResult(Query);

            if (TblList.Rows.Count > 0)
            {

                lblInactive.Text = TblList.Compute("SUM(inactive_count)", string.Empty).ToString();
                lblDocUpload.Text = TblList.Compute("SUM(docupload_count)", string.Empty).ToString();
                lblDocApproval.Text = TblList.Compute("SUM(DocApprove_count)", string.Empty).ToString();
                lblLicProcess.Text = TblList.Compute("SUM(LIC_Processed)", string.Empty).ToString();
                lblLicPayment.Text = TblList.Compute("SUM(LIC_Paid)", string.Empty).ToString();
                lblHOApproval.Text = TblList.Compute("SUM(HO_Approval)", string.Empty).ToString();

                GrdCDPO.DataSource = TblList;
                GrdCDPO.DataBind();
                //GrdDashboard.Visible = false;
                GrdDivision.Visible = false;
                GrdDistrict.Visible = false;
                GrdCDPO.Visible = true;
                GrdBit.Visible = false;
            }
            else
            {
                GrdCDPO.DataSource = null;
                GrdCDPO.DataBind();
                //GrdDashboard.Visible = false;
                GrdDivision.Visible = false;
                GrdDistrict.Visible = true;
                GrdCDPO.Visible = false;
                GrdBit.Visible = false;
                MessageAlert("Record Not found", "");
                return;
            }
        }

        public void GetBITList(string divid, string distid, string cdpoid, string bitid)
        {
            String Query = "  select angnname, sum(inactive_count) inactive_count, sum(docupload_count) docupload_count, sum(DocApprove_count) DocApprove_count, ";
            Query += " sum(LIC_Processed) LIC_Processed, sum(LIC_Paid) LIC_Paid, sum(HO_Approval) HO_Approval ";
            Query += " from ( select divid, divname, distid, distname, cdpoid, cdponame, bitid, bitbitname, var_angnwadimst_angnname angnname, var_lic_approvflag, ";
            Query += " case when var_lic_approvflag is null or var_lic_approvflag <>'R' then 1 else 0 end Inactive_count, ";
            Query += " case when img_lic_document is not null and var_lic_approvflag <>'R' then 1 else 0 end DocUpload_count, ";
            Query += " case when var_lic_approvflag = 'A' then 1 else 0 end DocApprove_count, ";
            Query += " case when var_lic_licapprovflag = 'A' then 1 else 0 end LIC_Processed, ";
            Query += " case when var_lic_flag_payment = 'P' then 1 else 0 end LIC_Paid,  ";
            Query += " case when var_lic_hoapprovflag = 'A' then 1 else 0 end HO_Approval ";
            Query += " From corpinfo inner join aoup_lic_def on bitid = num_lic_compid ";
            Query += " inner join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_lic_sevikaid ";
            Query += " inner join aoup_angnwadimst_def on num_angnwadimst_angnid =num_sevakmaster_anganid ";
            Query += " where stateid=11 and divid=" + divid + " and distid=" + distid + " and cdpoid=" + cdpoid + " and bitid=" + bitid + " ) ";
            Query += "  Group by angnname Order by angnname ";

            DataTable TblList = (DataTable)MstMethods.Query2DataTable.GetResult(Query);

            if (TblList.Rows.Count > 0)
            {
                Int32 sum = Convert.ToInt32(TblList.Compute("SUM(inactive_count)", string.Empty));

                lblInactive.Text = TblList.Compute("SUM(inactive_count)", string.Empty).ToString();
                lblDocUpload.Text = TblList.Compute("SUM(docupload_count)", string.Empty).ToString();
                lblDocApproval.Text = TblList.Compute("SUM(DocApprove_count)", string.Empty).ToString();
                lblLicProcess.Text = TblList.Compute("SUM(LIC_Processed)", string.Empty).ToString();
                lblLicPayment.Text = TblList.Compute("SUM(LIC_Paid)", string.Empty).ToString();
                lblHOApproval.Text = TblList.Compute("SUM(HO_Approval)", string.Empty).ToString();

                GrdBit.DataSource = TblList;
                GrdBit.DataBind();
                //GrdDashboard.Visible = false;
                GrdDivision.Visible = false;
                GrdDistrict.Visible = false;
                GrdCDPO.Visible = false;
                GrdBit.Visible = true;
            }
            else
            {
                GrdBit.DataSource = null;
                GrdBit.DataBind();
                //GrdDashboard.Visible = false;
                GrdDivision.Visible = false;
                GrdDistrict.Visible = false;
                GrdCDPO.Visible = true;
                MessageAlert("Record Not found", "");
                return;
            }
        }

        protected void GrdDashboard_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalinactive += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "inactive_count"));
                totaldocupload += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "docupload_count"));
                totalDocApprove += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DocApprove_count"));
                //totalProcessed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Processed"));
                //totalPaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Paid"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = String.Format("{0:0}", totalinactive);
                e.Row.Cells[2].Text = String.Format("{0:0}", totaldocupload);
                e.Row.Cells[3].Text = String.Format("{0:0}", totalDocApprove);
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }

        }
        protected void lnkDivName_Click(object sender, EventArgs e)
        {
            int BrCategory = Convert.ToInt32(Session["brcategory"]);
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnDivId = (HiddenField)GrdDashboard.Rows[rindex].FindControl("hdnDivId");
                if (hdnDivId.Value != "")
                {
                    string DivId = GrdDashboard.Rows[rindex].Cells[0].Text;
                    lblInactive.Text = GrdDashboard.Rows[rindex].Cells[2].Text;
                    lblDocUpload.Text = GrdDashboard.Rows[rindex].Cells[3].Text;
                    lblDocApproval.Text = GrdDashboard.Rows[rindex].Cells[4].Text;
                    lblLicProcess.Text = GrdDashboard.Rows[rindex].Cells[5].Text;
                    lblLicPayment.Text = GrdDashboard.Rows[rindex].Cells[6].Text;
                    //GrdDashboard.Visible = false;


                    if (BrCategory == 3 || BrCategory == 4 || BrCategory == 5)
                    {
                        GetHOList();
                    }
                    else
                    {
                        GetDivisionList(DivId);
                        GrdDivision.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }
        }



        protected void GrdDivision_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Divtotalinactive += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "inactive_count"));
                Divtotaldocupload += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "docupload_count"));
                DivtotalDocApprove += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DocApprove_count"));
                //totalProcessed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Processed"));
                //totalPaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Paid"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = String.Format("{0:0}", Divtotalinactive);
                e.Row.Cells[2].Text = String.Format("{0:0}", Divtotaldocupload);
                e.Row.Cells[3].Text = String.Format("{0:0}", DivtotalDocApprove);
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }
        protected void lnkDistName_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnDistId = (HiddenField)GrdDivision.Rows[rindex].FindControl("hdnDistId");
                HiddenField HdnDivId = (HiddenField)GrdDivision.Rows[rindex].FindControl("HdnDivId");
                if (hdnDistId.Value != "")
                {
                    string DistId = Convert.ToString(hdnDistId.Value);
                    lblInactive.Text = GrdDivision.Rows[rindex].Cells[2].Text;
                    lblDocUpload.Text = GrdDivision.Rows[rindex].Cells[3].Text;
                    lblDocApproval.Text = GrdDivision.Rows[rindex].Cells[4].Text;
                    lblLicProcess.Text = GrdDivision.Rows[rindex].Cells[5].Text;
                    lblLicPayment.Text = GrdDivision.Rows[rindex].Cells[6].Text;
                    string DivId = Convert.ToString(HdnDivId.Value);

                    //GrdDashboard.Visible = false;
                    GrdDivision.Visible = false;
                    GrdDistrict.Visible = true;
                    GetDistrictList(DivId, DistId);
                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }
        }

        protected void GrdDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalinactive += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "inactive_count"));
                totaldocupload += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "docupload_count"));
                totalDocApprove += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DocApprove_count"));
                //totalProcessed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Processed"));
                //totalPaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Paid"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = String.Format("{0:0}", totalinactive);
                e.Row.Cells[2].Text = String.Format("{0:0}", totaldocupload);
                e.Row.Cells[3].Text = String.Format("{0:0}", totalDocApprove);
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }
        protected void lnkCDPOName_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnCDPOId = (HiddenField)GrdDistrict.Rows[rindex].FindControl("hdnCDPOId");
                HiddenField HdnDivId = (HiddenField)GrdDistrict.Rows[rindex].FindControl("HdnDivId");
                HiddenField HdnDistId = (HiddenField)GrdDistrict.Rows[rindex].FindControl("HdnDistId");
                if (hdnCDPOId.Value != "")
                {
                    lblInactive.Text = GrdDistrict.Rows[rindex].Cells[2].Text;
                    lblDocUpload.Text = GrdDistrict.Rows[rindex].Cells[3].Text;
                    lblDocApproval.Text = GrdDistrict.Rows[rindex].Cells[4].Text;
                    lblLicProcess.Text = GrdDistrict.Rows[rindex].Cells[5].Text;
                    lblLicPayment.Text = GrdDistrict.Rows[rindex].Cells[6].Text;

                    GetCDPOList(HdnDivId.Value, HdnDistId.Value, hdnCDPOId.Value);
                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }
        }

        protected void GrdCDPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalinactive += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "inactive_count"));
                totaldocupload += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "docupload_count"));
                totalDocApprove += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DocApprove_count"));
                //totalProcessed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Processed"));
                //totalPaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Paid"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = String.Format("{0:0}", totalinactive);
                e.Row.Cells[2].Text = String.Format("{0:0}", totaldocupload);
                e.Row.Cells[3].Text = String.Format("{0:0}", totalDocApprove);
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }
        protected void lnkBitName_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnBitId = (HiddenField)GrdCDPO.Rows[rindex].FindControl("hdnBitId");
                HiddenField HdnDivId = (HiddenField)GrdCDPO.Rows[rindex].FindControl("HdnDivId");
                HiddenField HdnDistId = (HiddenField)GrdCDPO.Rows[rindex].FindControl("HdnDistId");
                HiddenField HdnCdpo = (HiddenField)GrdCDPO.Rows[rindex].FindControl("HdnCdpo");

                if (hdnBitId.Value != "")
                {
                    lblInactive.Text = GrdCDPO.Rows[rindex].Cells[2].Text;
                    lblDocUpload.Text = GrdCDPO.Rows[rindex].Cells[3].Text;
                    lblDocApproval.Text = GrdCDPO.Rows[rindex].Cells[4].Text;
                    lblLicProcess.Text = GrdCDPO.Rows[rindex].Cells[5].Text;
                    lblLicPayment.Text = GrdCDPO.Rows[rindex].Cells[6].Text;
                    GetBITList(HdnDivId.Value, HdnDistId.Value, HdnCdpo.Value, hdnBitId.Value);
                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }
        }

        protected void GrdBit_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalinactive += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "inactive_count"));
                totaldocupload += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "docupload_count"));
                totalDocApprove += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DocApprove_count"));
                //totalProcessed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Processed"));
                //totalPaid += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LIC_Paid"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = String.Format("{0:0}", totalinactive);
                e.Row.Cells[2].Text = String.Format("{0:0}", totaldocupload);
                e.Row.Cells[3].Text = String.Format("{0:0}", totalDocApprove);
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
            }
        }
        protected void lnkAngName_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnAngName = (HiddenField)GrdBit.Rows[rindex].FindControl("hdnAngName");

                if (hdnAngName.Value != "")
                {
                    lblInactive.Text = GrdBit.Rows[rindex].Cells[1].Text;
                    lblDocUpload.Text = GrdBit.Rows[rindex].Cells[2].Text;
                    lblDocApproval.Text = GrdBit.Rows[rindex].Cells[3].Text;
                    lblLicProcess.Text = GrdBit.Rows[rindex].Cells[4].Text;
                    lblLicPayment.Text = GrdBit.Rows[rindex].Cells[5].Text;
                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }
        }
    }
}