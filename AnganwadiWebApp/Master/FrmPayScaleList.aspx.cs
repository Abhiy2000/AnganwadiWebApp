using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmPayScaleList : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"PayScale List";
                LoadGrid();
            }
        }

        protected void LoadGrid()
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];

            DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);

            if (TblGetHoId.Rows.Count > 0)
            {
                String Query = " SELECT a.num_payscal_payscalid PayId, c.var_projecttype_prjtype,b.var_education_educname,num_designation_desigid,var_designation_desig, ";
                Query += " a.var_payscal_payscal, a.num_payscal_wages,a.num_payscal_central, a.num_payscal_state, a.num_payscal_fixed,num_payscal_expfrom,num_payscal_expto, ";
                Query += " CASE WHEN a.var_payscal_active = 'N' THEN 'Inactive' ELSE 'Active' END var_payscal_active FROM aoup_payscal_def a ";
                Query += " LEFT JOIN aoup_Education_def b ON  a.num_payscal_educid = b.num_education_educid ";
                Query += " LEFT JOIN aoup_ProjectType_def c ON a.num_payscal_compid=c.num_projecttype_compid and a.num_payscal_prjtypeid = c.num_projecttype_prjtypeid ";
                Query += " left join aoup_designation_def d on a.num_payscal_desigid=d.num_designation_desigid  ";
                Query += " where num_payscal_compid=" + TblGetHoId.Rows[0]["hoid"].ToString();
                Query += " order by var_payscal_payscal ";

                DataTable TblMenuList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (TblMenuList.Rows.Count > 0)
                {
                    GrdPayList.DataSource = TblMenuList;
                    GrdPayList.DataBind();
                }
                else
                {
                    GrdPayList.DataSource = null;
                    GrdPayList.DataBind();
                    //MessageAlert("No record found", "");
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmPayScaleMaster.aspx?@=1");
        }

        protected void GrdPayList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }

        protected void GrdPayList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdPayList.SelectedRow;
            Session["PayId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmPayScaleMaster.aspx?@=2");
        }

        protected void GrdPayList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdPayList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}