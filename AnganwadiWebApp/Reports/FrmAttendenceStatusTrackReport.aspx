<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmAttendenceStatusTrackReport.aspx.cs" Inherits="AnganwadiWebApp.Reports.FrmAttendenceStatusTrackReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:Label ID="Label1" runat="server" Style="font-size: 15px; outline-color: Red;"><b> Attendance Status tracking report details</b></asp:Label>--%>
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="margin-top: 9px;">
        <asp:Button runat="server" ID="btnDownload" Text="Download Excel" class="Button"
            Width="120px" OnClick="btnDownload_Click" />
        <center>
            <asp:Label ID="lblError" runat="server" Style="color: Red;"></asp:Label>
            <br />
            <asp:UpdatePanel runat="server" ID="fg">
                <ContentTemplate>
                    <asp:GridView ID="grdMonAttendance" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                        Width="100%" Visible="true" AllowPaging="true" PageSize="6" OnPageIndexChanging="grdMonAttendance_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="divname" HeaderText="Division Name" />
                            <asp:BoundField DataField="distname" HeaderText="District name" />
                            <asp:BoundField DataField="bitbitname" HeaderText="DDO code" />
                            <asp:BoundField DataField="cdponame" HeaderText="Project Name" />
                            <asp:BoundField DataField="active_sevika" HeaderText="Active sevika report count" />
                            <asp:BoundField DataField="attendance" HeaderText="Bit level attendance count" />
                            <asp:BoundField DataField="auth_count" HeaderText="DDO level Attendance authorize count" />
                            <asp:BoundField DataField="pending_attendance" HeaderText="Pending Bit level attendance count (E-F)" />
                            <asp:BoundField DataField="pending_auth" HeaderText="Pending DDO level authorization count (F-G)" />
                            <asp:BoundField DataField="total_pending" HeaderText="Total Pending Attendance (H+I)" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDownload" />
                </Triggers>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
