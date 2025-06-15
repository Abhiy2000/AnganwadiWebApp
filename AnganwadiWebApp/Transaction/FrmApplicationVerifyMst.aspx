<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmApplicationVerifyMst.aspx.cs"
    Inherits="AnganwadiWebApp.Transaction.FrmApplicationVerifyMst" %>

<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="LblGrdHead" runat="server"></asp:Label>
                        </h5>

                    </div>
                    <div class="ibox-content">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="nav-tabs-custom">
                                    <div class="tab-content">
                                        <div class="form-horizontal">
                                            <div class="form-group row">
                                                <label class="col-sm-2 control-label">
                                                    Name of Applicant :
                                                </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtNameApp" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2 control-label">
                                                    Address of Applicant :
                                                </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2 control-label">
                                                    Division :
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    District :
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" />
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    Anganwadi :
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlAnganwadi" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2 control-label">
                                                    Position :
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlporition" runat="server" CssClass="form-control" />
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    Birth Date :
                                                </label>
                                                <div class="col-md-2">
                                                    <uc1:Date ID="birthdt" runat="server" />
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    Age :
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-12 col-xs-12">
                                                    <center>
                                                        <asp:GridView ID="grdDoc" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                                                            Width="80%" AutoGenerateColumns="false" OnRowCommand="grdDoc_RowCommand"
                                                            AllowPaging="True" PageSize="10" OnRowDataBound="grdDoc_RowDataBound"
                                                            OnSelectedIndexChanged="grdDoc_SelectedIndexChanged" ShowFooter="true">
                                                            <RowStyle CssClass="GrdRow"></RowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="Docid" HeaderText="DocumentID" />
                                                                <asp:TemplateField HeaderText="Document Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocNames" Text='<%#Eval("Docname")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="View">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkView" runat="server"
                                                                            CssClass="btn btn-secondary" CommandName="PreviewFile" CommandArgument='<%# Eval("Docid") %>' Text="View"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Marks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtMarks" runat="server" Text="0" CssClass="form-control" OnTextChanged="txtMarks_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                         <asp:Label ID="lblToTMarks" runat="server" Text="Total Marks : " Font-Bold="true" ForeColor="DarkBlue"></asp:Label>
                                                                        <asp:Label ID="lblTotalMarks" runat="server" Font-Bold="true" Text="0" ForeColor="DarkBlue"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                                                        </asp:GridView>
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2 control-label">
                                                    Document Verified :
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="rdbDocVerify" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Yes" Value="Y" Selected ="True"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N" style="margin-left: 15px;"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    Application Approved :
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="rdbAppliApproved" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Yes" Value="Y" Selected ="True"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N" style="margin-left: 15px;"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2 control-label">
                                                    Remark :
                                                </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                </div>
                                            </div>
                                            <div class="col-lg-offset-5">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="Btnsubmit" />
                                                        <asp:PostBackTrigger ControlID="btnClose" />
                                                        <asp:PostBackTrigger ControlID="grdDoc" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Button ID="Btnsubmit" CssClass="btn btn-primary" runat="server" Text="Submit"
                                                            ValidationGroup="A" Style="margin-top: 15px;" OnClick="Btnsubmit_Click" />
                                                        <asp:Button ID="BtnClose" runat="server" Style="margin-top: 15px;" Text="Close" CssClass="btn btn-white"
                                                            OnClick="BtnClose_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
