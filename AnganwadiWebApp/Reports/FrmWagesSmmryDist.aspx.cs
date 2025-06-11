using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;

namespace AnganwadiWebApp.Reports
{
    public partial class FrmWagesSmmryDist : System.Web.UI.Page
    {
        DateTime Lastdate;
        string lstdate;

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
                PnlSerch.Visible = true;
                LblGrdHead.Text = Session["LblGrdHead"].ToString();
                BindDropdrownlist();

                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();

                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                Int32 BRCategory = Convert.ToInt32(TblResult.Rows[0]["BRCategory"]);

                if (BRCategory == 2)   // Div
                {
                    Session["Div"] = Session["GrdLevel"];
                    btnBack.Visible = false;
                    Panel1.Visible = true;
                    ddlMonth.Enabled = true;
                    ddlYear.Enabled = true;
                }
                else
                {
                    Panel1.Visible = true;
                    ddlMonth.Enabled = false;
                    ddlYear.Enabled = false;
                    btnSubmit.Enabled = false;
                    lstdate = Session["lstdate"].ToString();
                    LoadGrid();
                }
            }
        }

        protected void grdDistWgs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        public void BindDropdrownlist()
        {
            ddlMonth.Items.Add(new ListItem("January", "1"));
            ddlMonth.Items.Add(new ListItem("February", "2"));
            ddlMonth.Items.Add(new ListItem("March", "3"));
            ddlMonth.Items.Add(new ListItem("April", "4"));
            ddlMonth.Items.Add(new ListItem("May", "5"));
            ddlMonth.Items.Add(new ListItem("June", "6"));
            ddlMonth.Items.Add(new ListItem("July", "7"));
            ddlMonth.Items.Add(new ListItem("August", "8"));
            ddlMonth.Items.Add(new ListItem("September", "9"));
            ddlMonth.Items.Add(new ListItem("October", "10"));
            ddlMonth.Items.Add(new ListItem("November", "11"));
            ddlMonth.Items.Add(new ListItem("December", "12"));

            ddlYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), "1"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), "2"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 2).ToString(), "3"));
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();

            ddlYear.SelectedItem.Text = System.DateTime.Now.Year.ToString();
        }

        public void LoadGrid()
        {
            try
            {
                String Dist = "select distname,distid, count(Helper) Helper,count(Worker) Worker,sum(num_salary_central) Central,sum(num_salary_state) State,(sum(num_salary_central)+sum(num_salary_state)) Total from ";
                Dist += " (select distname,distid,case when var_designation_flag='H' then 1 else null end Helper,case when var_designation_flag='W' then 1 else null end Worker, ";
                Dist += " num_salary_central,num_salary_state,num_salary_fixed from aoup_salary_def left join corpinfo on bitid=num_salary_compid ";
                Dist += " left join aoup_designation_def on num_designation_desigid=num_salary_desigid where divid=" + Session["Div"] + "  and date_salary_date= '" + lstdate + "') ";
                Dist += " group by distname,distid order by distname";

                DataTable tblDistlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Dist);

                if (tblDistlist.Rows.Count > 0)
                {
                    grdDistWgs.DataSource = tblDistlist;
                    grdDistWgs.DataBind();

                    decimal Helper = Math.Round(tblDistlist.AsEnumerable().Sum(row => row.Field<decimal>("Helper")));
                    grdDistWgs.FooterRow.Cells[2].Text = Helper.ToString("0");

                    decimal Worker = Math.Round(tblDistlist.AsEnumerable().Sum(row => row.Field<decimal>("Worker")));
                    grdDistWgs.FooterRow.Cells[3].Text = Worker.ToString("0");

                    decimal Central = Math.Round(tblDistlist.AsEnumerable().Sum(row => row.Field<decimal>("Central")));
                    grdDistWgs.FooterRow.Cells[4].Text = Central.ToString("0");

                    decimal State = Math.Round(tblDistlist.AsEnumerable().Sum(row => row.Field<decimal>("State")));
                    grdDistWgs.FooterRow.Cells[5].Text = State.ToString("0");

                    decimal Total = Math.Round(tblDistlist.AsEnumerable().Sum(row => row.Field<decimal>("Total")));
                    grdDistWgs.FooterRow.Cells[6].Text = Total.ToString("0");

                    //decimal Fixed = Math.Round(tblDistlist.AsEnumerable().Sum(row => row.Field<decimal>("Fixed")));
                    //grdDistWgs.FooterRow.Cells[11].Text = Fixed.ToString("0");
                }
                else
                {
                    grdDistWgs.DataSource = null;
                    grdDistWgs.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Lastdate = new DateTime(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue)));
            lstdate = Lastdate.ToString("dd-MMM-yyyy");
            Session["lstdate"] = lstdate;

            LoadGrid();
        }

        protected void lnkDist_Click(object sender, EventArgs e)
        {
            int rindex = (((GridViewRow)(((LinkButton)(sender)).Parent.BindingContainer))).RowIndex;
            HiddenField hdndist = (HiddenField)grdDistWgs.Rows[rindex].FindControl("hdndist");
            string hdndistval = hdndist.Value;
            Session["Dist"] = hdndistval.ToString();
            if (hdndistval != "0")
            {
                Response.Redirect("../Reports/FrmWagesSmmryCDPO.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Session["GrdLevel"] = Session["Dist"];
            Response.Redirect("../Reports/FrmWagesSmmryDiv.aspx");
        }
    }
}