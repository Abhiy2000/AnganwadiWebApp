<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmApplicationVerifyList.aspx.cs"
    Inherits="AnganwadiWebApp.Transaction.FrmApplicationVerifyList" %>

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
                                                <div class="col-sm-12 col-xs-12">
                                                    <center>
                                                        <asp:GridView ID="GrdApplication" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                                                            Width="80%" AutoGenerateColumns="false"
                                                            AllowPaging="False" PageSize="10" OnRowDataBound="GrdApplication_RowDataBound"
                                                            OnSelectedIndexChanged="GrdApplication_SelectedIndexChanged">
                                                            <RowStyle CssClass="GrdRow"></RowStyle>
                                                            <Columns>
                                                                <asp:CommandField HeaderText="Select" ShowSelectButton="True" SelectText="Select">
                                                                </asp:CommandField>
                                                                <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="APPLIID" HeaderText="Application Id">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="APPLINO" HeaderText="Application No">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="APPNAME" HeaderText="Name of Applicant">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PORTID" HeaderText="Position Id">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="POSITION" HeaderText="Applied Position">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ANGNID" HeaderText="Anganwadi Id">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ANGNNAME" HeaderText="Anganwadi">
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                                                        </asp:GridView>
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="col-lg-offset-5">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnClose" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Button ID="BtnClose" runat="server" Style="margin-top: 15px;" Text="Back" CssClass="btn btn-white"
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
