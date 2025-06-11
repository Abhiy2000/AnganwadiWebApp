using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GridUserControl.WebUserControls
{
    public partial class GridViewMst : System.Web.UI.UserControl
    {
        String _QueryStr;
        String _CssClass;
        String _FooterStyleCssClass;

        Boolean _ShowFooter = false;
        Boolean _AutoGenerateColumns = true;

        Int64 _NoOfRowsToShow;
        Int64 _TotalRecords;

        public String QueryStr
        {
            get { return _QueryStr; }
            set { _QueryStr = value; }
        }

        public String CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }

        public String FooterStyleCssClass
        {
            get { return _FooterStyleCssClass; }
            set { _FooterStyleCssClass = value; }
        }

        public Boolean ShowFooter
        {
            get { return _ShowFooter; }
            set { _ShowFooter = value; }
        }

        public Boolean AutoGenerateColumns
        {
            get { return _AutoGenerateColumns; }
            set { _AutoGenerateColumns = value; }
        }

        public Int64 NoOfRowsToShow
        {
            get { return _NoOfRowsToShow; }
            set { _NoOfRowsToShow = value; }
        }

        public Int64 TotalRecords
        {
            get { return _TotalRecords; }
            set { _TotalRecords = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            GridView1.CssClass = CssClass;
            GridView1.ShowFooter = ShowFooter;
            GridView1.FooterStyle.CssClass = FooterStyleCssClass;
            GridView1.AutoGenerateColumns = AutoGenerateColumns;

            lblPageNumber.Visible = false;
            ddlPageNo.Visible = false;
            lblTotalRecords.Visible = false;
        }

        public void FillGridView(Int64 PageNo, out Int64 RecordCount)
        {
            RecordCount = 0;

            try
            {
                if (PageNo == 1)
                {
                    Session[this.ID + "QueryStr"] = QueryStr;
                    Session[this.ID + "NoOfRowsToShow"] = NoOfRowsToShow;
                    String QueryStrCnt = "select count(*) as cnt from (" + QueryStr + ")";

                    DataTable TblResult = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(QueryStrCnt);

                    if (TblResult.Rows.Count > 0)
                    {
                        TotalRecords = Convert.ToInt64(TblResult.Rows[0]["cnt"]);

                        lblTotalRecords.Text = "Total Records : " + TotalRecords;
                    }

                    Session[this.ID + "TotalRecords"] = TotalRecords;

                    RecordCount = TotalRecords;
                }

                if (Session[this.ID + "QueryStr"].ToString() != "")
                {
                    DataTable TblResult = new DataTable();

                    if (Convert.ToInt64(Session[this.ID + "TotalRecords"]) > 0)
                    {
                        if (PageNo == -1)
                        {
                            PageNo = 1;
                        }

                        TblResult = new DataTable();

                        Int64 FromROWNUM = 0;
                        Int64 ToROWNUM = 0;

                        ToROWNUM = Convert.ToInt64(Session[this.ID + "NoOfRowsToShow"]) * PageNo;
                        FromROWNUM = ToROWNUM - Convert.ToInt64(Session[this.ID + "NoOfRowsToShow"]);

                        String QueryStrResult = "select * from (select result.* , ROWNUM rownumber  from (" + Session[this.ID + "QueryStr"].ToString() + ") ";
                        QueryStrResult += "result) where rownumber > " + FromROWNUM + " and rownumber <= " + ToROWNUM;

                        TblResult = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(QueryStrResult);

                        if (TblResult.Rows.Count > 0)
                        {
                            TblResult.Columns.Remove("rownumber");

                            GridView1.DataSource = TblResult;
                            GridView1.DataBind();

                            if (GridView1.Rows.Count > 0)
                            {
                                lblPageNumber.Visible = true;
                                ddlPageNo.Visible = true;
                                lblTotalRecords.Visible = true;
                            }
                            else
                            {
                                lblPageNumber.Visible = false;
                                ddlPageNo.Visible = false;
                                lblTotalRecords.Visible = false;
                            }

                            if (PageNo == 1)
                            {
                                Double NoOfLinks = Convert.ToDouble(Session[this.ID + "TotalRecords"]) / Convert.ToDouble(Session[this.ID + "NoOfRowsToShow"]);

                                NoOfLinks = Math.Ceiling(NoOfLinks);

                                DataTable TblDropDown = new DataTable();

                                TblDropDown.Columns.Add("PageNo", typeof(String));
                                TblDropDown.Columns.Add("Value", typeof(String));

                                Int64 PageNoTemp = 0;

                                for (int i = 0; i < NoOfLinks; i++)
                                {
                                    PageNoTemp = PageNoTemp + 1;

                                    if (PageNoTemp == 1)
                                    {
                                        TblDropDown.Rows.Add(PageNoTemp.ToString(), -1);
                                    }

                                    else
                                    {
                                        TblDropDown.Rows.Add(PageNoTemp.ToString(), PageNoTemp.ToString());
                                    }
                                }

                                ddlPageNo.DataSource = TblDropDown;
                                ddlPageNo.DataTextField = "PageNo";
                                ddlPageNo.DataValueField = "Value";
                                ddlPageNo.DataBind();
                            }
                        }
                    }

                    else
                    {
                        GridView1.DataSource = "";
                        GridView1.DataBind();

                        lblPageNumber.Visible = false;
                        ddlPageNo.Visible = false;
                        lblTotalRecords.Visible = false;
                    }
                }
            }

            catch (Exception ex)
            {
                String ExStr = ex.Message;
            }
        }

        protected void ddlPageNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 PageNo = Convert.ToInt64(ddlPageNo.SelectedValue);

            Int64 RecordCount = 0;

            FillGridView(PageNo, out RecordCount);
        }
    }
}