using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmPolicyUpdateMst : System.Web.UI.Page
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
            //LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";
            if (!IsPostBack)
            {
                // LoadGrid();
                PnlSerch.Visible = false;

                LblGrdHead.Text = "Transation >> Policy Update Master";//Session["LblGrdHead"].ToString();// "Anganwadi List";

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

                    PnlSerch.Visible = true;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (PnlSerch.Visible == true)
            {
                LoadGrid();
            }
            else
            {
                GrdPolicyUpdate.DataSource = null;
                GrdPolicyUpdate.DataBind();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx");
            return;
        }

        protected void GrdPolicyUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = GrdPolicyUpdate.SelectedRow;
            //Session["PrjType"] = row.Cells[5].Text;
            //Session["SalDate"] = row.Cells[3].Text;
            //Session["AngnId"] = row.Cells[6].Text;
            //Response.Redirect("../Transaction/FrmMonthlyAttendenceAuth.aspx");
        }

        protected void GrdPolicyUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int age = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "age"));

                TextBox txtPMJJBYNo = (TextBox)e.Row.FindControl("txtPMJJBYNo");
                //ProjectManagement.WebUserControls.DateGrid dtPMJJBY = (ProjectManagement.WebUserControls.DateGrid)e.Row.FindControl("dtPMJJBY");
                TextBox txtPMSBYNo = (TextBox)e.Row.FindControl("txtPMSBYNo");
                //ProjectManagement.WebUserControls.DateGrid dtPMSBY = (ProjectManagement.WebUserControls.DateGrid)e.Row.FindControl("dtPMSBY");

                if (age >= 18 && age <= 49)
                {
                    txtPMJJBYNo.Enabled = true;
                    //dtPMJJBY.EnableControl();
                    txtPMSBYNo.Enabled = true;
                    //dtPMSBY.DisableControl();

                }
                else if (age >= 50 && age <= 59)
                {
                    txtPMJJBYNo.Enabled = false;
                    //dtPMJJBY.EnableControl();
                    txtPMSBYNo.Enabled = true;
                    //dtPMSBY.EnableControl();
                }
                else
                {
                    txtPMJJBYNo.Enabled = false;
                    //dtPMJJBY.DisableControl();
                    txtPMSBYNo.Enabled = false;
                    //dtPMSBY.DisableControl();
                }
            }
        }

        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlDistrict.DataSource = "";
            ddlDistrict.DataBind();

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            if (ddlDivision.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlDistrict, "companyview", "branchname", "brid", " parentid = " + ddlDivision.SelectedValue.ToString() + " and brcategory = 3 order by branchname", "");
            }
        }

        protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

            ddlCDPO.DataSource = "";
            ddlCDPO.DataBind();

            if (ddlDistrict.SelectedValue.ToString() != "")
            {
                MstMethods.Dropdown.Fill(ddlCDPO, "companyview", "branchname", "brid", " parentid = " + ddlDistrict.SelectedValue.ToString() + " and brcategory = 4 order by branchname", "");
            }
        }

        protected void ddlCDPO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSerch.Visible = false;

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

                PnlSerch.Visible = true;
            }
            else
            {
                PnlSerch.Visible = false;
            }
        }

        protected void LoadGrid()
        {
            string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
            string BrId = "";
            DataTable TblGetHoId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(HoId);

            if (TblGetHoId.Rows.Count > 0)
            {
                String Query = "select sevikacode, aadharno, dob, age, type_, pmjjbyno, pmsbyno from view_sevikapolicy";
                Query += " where bitcode='" + ddlBeat.SelectedValue.ToString() + "' ";

                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    GrdPolicyUpdate.DataSource = dtSevikaList;
                    GrdPolicyUpdate.DataBind();
                    btnSubmit.Visible = true;
                }
                else
                {
                    GrdPolicyUpdate.DataSource = null;
                    GrdPolicyUpdate.DataBind();
                    btnSubmit.Visible = false;
                    MessageAlert("No Data Found", "");
                    return;
                }
            }
            else
            {
                GrdPolicyUpdate.DataSource = null;
                GrdPolicyUpdate.DataBind();
                btnSubmit.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDivision.SelectedValue == "0" || string.IsNullOrEmpty(ddlDivision.SelectedValue))
                {
                    MessageAlert("Division cannot be blank", "");
                    return;
                }
                if (ddlDistrict.SelectedValue == "0" || string.IsNullOrEmpty(ddlDistrict.SelectedValue))
                {
                    MessageAlert("District cannot be blank", "");
                    return;
                }
                if (ddlCDPO.SelectedValue == "0" || string.IsNullOrEmpty(ddlCDPO.SelectedValue))
                {
                    MessageAlert("CDPO cannot be blank", "");
                    return;
                }
                if (ddlBeat.SelectedValue == "0" || string.IsNullOrEmpty(ddlBeat.SelectedValue))
                {
                    MessageAlert("Beat cannot be blank", "");
                    return;
                }

                BoPolicyUpdate objPolicyUpdate = new BoPolicyUpdate();

                objPolicyUpdate.UserId = Session["UserId"].ToString();
                objPolicyUpdate.DivId = Convert.ToInt32(ddlDivision.SelectedValue);
                objPolicyUpdate.DistId = Convert.ToInt32(ddlDistrict.SelectedValue);
                objPolicyUpdate.CDPOId = Convert.ToInt32(ddlCDPO.SelectedValue);
                objPolicyUpdate.BitCode = Convert.ToInt32(ddlBeat.SelectedValue);

                string Dtlstr = "";
                int Sevikacode;
                string Aadharno;
                string Dob;
                int Age;
                string Type;

                if (GrdPolicyUpdate.Rows.Count > 0)
                {
                    for (int i = 0; i < GrdPolicyUpdate.Rows.Count; i++)
                    {
                        Sevikacode = Convert.ToInt32(((Label)GrdPolicyUpdate.Rows[i].FindControl("lblsevikacode")).Text);
                        Aadharno = ((Label)GrdPolicyUpdate.Rows[i].FindControl("lblaadharno")).Text;
                        Dob = ((Label)GrdPolicyUpdate.Rows[i].FindControl("lbldob")).Text;
                        Age = Convert.ToInt32(((Label)GrdPolicyUpdate.Rows[i].FindControl("lblage")).Text);
                        Type = ((Label)GrdPolicyUpdate.Rows[i].FindControl("lblType")).Text;

                        TextBox PMJJBYNo = (TextBox)GrdPolicyUpdate.Rows[i].FindControl("txtPMJJBYNo");
                        TextBox PMSBYNo = (TextBox)GrdPolicyUpdate.Rows[i].FindControl("txtPMSBYNo");

                        if (Type == "18-49")
                        {
                            if (!string.IsNullOrEmpty(PMJJBYNo.Text) && !string.IsNullOrEmpty(PMSBYNo.Text))
                            {
                                Dtlstr += Sevikacode + "$";
                                Dtlstr += Aadharno + "$";
                                DateTime parsedDate;
                                // Try to parse the date with the exact format
                                bool isValidDate = DateTime.TryParseExact(Dob, "dd/MM/yyyy",
                                                                          System.Globalization.CultureInfo.InvariantCulture,
                                                                          System.Globalization.DateTimeStyles.None,
                                                                          out parsedDate);

                                if (isValidDate)
                                {
                                    // Successfully parsed the date, now you can work with parsedDate
                                    // For example, you can format it or assign it to another variable
                                    string formattedDate = parsedDate.ToString("dd/MMM/yyyy");
                                    Dtlstr += formattedDate + "$";
                                }

                                Dtlstr += Age + "$";
                                Dtlstr += Type + "$";
                                Dtlstr += PMJJBYNo.Text + "$";
                                Dtlstr += PMSBYNo.Text + "#";

                            }
                        }
                        if (Type == "18-59")
                        {
                            if (!string.IsNullOrEmpty(PMSBYNo.Text))
                            {
                                Dtlstr += Sevikacode + "$";
                                Dtlstr += Aadharno + "$";
                                DateTime parsedDate;
                                // Try to parse the date with the exact format
                                bool isValidDate = DateTime.TryParseExact(Dob, "dd/MM/yyyy",
                                                                          System.Globalization.CultureInfo.InvariantCulture,
                                                                          System.Globalization.DateTimeStyles.None,
                                                                          out parsedDate);

                                if (isValidDate)
                                {
                                    // Successfully parsed the date, now you can work with parsedDate
                                    // For example, you can format it or assign it to another variable
                                    string formattedDate = parsedDate.ToString("dd/MMM/yyyy");
                                    Dtlstr += formattedDate + "$";
                                }
                                Dtlstr += Age + "$";
                                Dtlstr += Type + "$";
                                Dtlstr += PMJJBYNo.Text + "$";
                                Dtlstr += PMSBYNo.Text + "#";
                            }
                        }


                    }
                }

                if (!string.IsNullOrEmpty(Dtlstr))
                {
                    Dtlstr = Dtlstr.TrimEnd('#');
                }
                else
                {
                    MessageAlert("Please enter at least one record", "");
                    return;
                }
               

                objPolicyUpdate.ParamStr = Dtlstr;
                objPolicyUpdate.InsertPolicy();

                if (objPolicyUpdate.ErrorCode == 9999)
                {
                    MessageAlert(objPolicyUpdate.ErrorMsg, "../Transaction/FrmPolicyUpdateMst.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objPolicyUpdate.ErrorMsg, "../Transaction/FrmPolicyUpdateMst.aspx");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }
    }
}