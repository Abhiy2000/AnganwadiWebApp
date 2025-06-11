using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmDocDownload : System.Web.UI.Page
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
            LblGrdHead.Text = Session["LblGrdHead"].ToString(); // "Sevika LIC Document Upload"
            if (!IsPostBack)
            {
                // LoadGrid();
                Session["GrdLevel"].ToString();
 
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

                    ddlDivision.SelectedValue = TblResult.Rows[0]["div"].ToString();
                    ddlDistrict.SelectedValue = TblResult.Rows[0]["dis"].ToString();
                    ddlCDPO.SelectedValue = TblResult.Rows[0]["cdpo"].ToString();

                    ddlDivision.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlCDPO.Enabled = false;

                  }
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
                HiddenField hdnDownload = (HiddenField)grdLICDEF.Rows[rindex].FindControl("hdnDownload");
                //Label lblFlagType = (Label)grdLICDEF.Rows[rindex].FindControl("lblFlagType");
                if (hdnDownload.Value != "0")
                {
                    DownloadFile(Convert.ToInt32(hdnDownload.Value));
                }
            }
            catch (Exception ex)
            {
                // MessageAlert(ex.Message.ToString(), "");
            }

        }
        public void DownloadFile(int SevikaID)
        {

            string Query = " Select img_lic_document DocImage from aoup_LIC_DEF ";
            Query += " where img_lic_document is not null ";
            Query += " and num_lic_sevikaid='" + SevikaID + "' ";

            DataTable TblFile = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
            if (TblFile.Rows.Count > 0)
            {
                Byte[] DocImage = new Byte[0];
                if (TblFile.Rows[0]["DocImage"] != DBNull.Value)
                {
                    DocImage = (Byte[])(TblFile.Rows[0]["DocImage"]);
                    string base64String = Convert.ToBase64String(DocImage);

                    Response.Clear();
                    MemoryStream ms = new MemoryStream(DocImage);
                    Response.ContentType = "application/pdf";
                    //String FILENAME = " Doc_" + System.DateTime.Now.ToString("ddmmyyyyhhMMss") + "Doc_.jpg";
                    Response.AddHeader("content-disposition", "attachment;filename=Doc.pdf");
                    Response.Buffer = true;
                    ms.WriteTo(Response.OutputStream);
                    Response.End();

                }
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            String Query = "";
            Query += " SELECT   divname, distname, cdponame,bitbitname, ";
            Query += "divid, distid,cdpoid, ";
            Query += " num_sevikamaster_sevikaid SevikaID, ";
            Query += "  var_sevikamaster_name sevikaname, ";
            Query += "   var_sevikamaster_aadharno aadharno, ";
            Query += "   (CASE WHEN var_sevikamaster_active = 'Y' THEN 	'Active' ELSE 'InActive' END) status, ";
            Query += "  num_sevikamaster_compid comp_id , img_lic_document ";
            Query += "   FROM aoup_sevikamaster_def ";
            Query += " INNER JOIN aoup_angnwadimst_def  ON num_angnwadimst_angnid = 	num_sevakmaster_anganid ";
            Query += "  INNER JOIN corpinfo ON bitid = num_sevikamaster_compid  ";
            Query += "  inner join aoup_LIC_DEF on 	num_sevikamaster_sevikaid=num_lic_sevikaid  ";
            Query += " WHERE   img_lic_document is not null ";
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
                grdLICDEF.Visible = true;
                grdLICDEF.DataSource = dtSevikaList;
                grdLICDEF.DataBind();
                ViewState["TblData"] = dtSevikaList;
            }
            else
            {
                grdLICDEF.Visible = false;
                MessageAlert(" Record Not Found ", "");
                return;
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
 
            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
 
            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

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
                //LoadSearch();
             }
        }

        protected void grdLICDEF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdLICDEF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLICDEF.PageIndex = e.NewPageIndex;
            DataTable tbl = ViewState["TblData"] as DataTable;

            grdLICDEF.DataSource = tbl;
            grdLICDEF.DataBind();
        }
    }
}