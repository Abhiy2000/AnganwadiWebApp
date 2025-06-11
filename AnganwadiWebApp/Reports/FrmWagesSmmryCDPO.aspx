<%@ Page Title="Wages Summary Report" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmWagesSmmryCDPO.aspx.cs" Inherits="AnganwadiWebApp.Reports.FrmWagesSmmryCDPO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function closeMsgPopupnew() {
            $find("mpeMsg1").hide();
            debugger;
            var el = document.getElementById('ContentPlaceHolder1_lblredirect');
            var aa = el.innerHTML;
            if (aa != "") {
                window.location.href = aa;
            }
            return false;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
        }
    </script>
    <style type="text/css">
        .transparent
        {
            /* IE 8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /* IE 5-7 */
            filter: alpha(opacity=50); /* Netscape */
            -moz-opacity: 0.5; /* Safari 1.x */
            -khtml-opacity: 0.5; /* Good browsers */
            opacity: 0.5;
        }
        .loader
        {
            position: fixed;
            text-align: center;
            height: 100%;
            width: 100%;
            top: 0;
            right: 0;
            left: 0;
            z-index: 9999999;
            background-color: #FFFFFF;
            opacity: 0.3;
            visibility: hidden;
        }
        .loader img
        {
            padding: 10px;
            position: fixed;
            top: 45%;
            left: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <asp:Panel ID="Panel1" runat="server">
            <table>
                <tr>
                    <td>
                        Salary Date :
                    </td>
                    <td style="padding-left: 10px">
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="padding-left: 10px">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="PnlSerch" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="grdCDPOWgs" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10" ShowFooter="True" FooterStyle-BackColor="#428BCA" FooterStyle-Font-Bold="true"
                            FooterStyle-ForeColor="white" FooterStyle-HorizontalAlign="Right" OnRowDataBound="grdCDPOWgs_RowDataBound">
                            <FooterStyle BackColor="#428BCA" ForeColor="White" HorizontalAlign="Right" />
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="CDPO" HeaderStyle-HorizontalAlign="Center" FooterText="Total">
                                    <HeaderStyle Width="270px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCDPO" runat="server" Text='<%# Eval("cdponame") %>' OnClick="lnkCDPO_Click"></asp:LinkButton>
                                        <asp:HiddenField ID="hdndist" runat="server" Value='<%# Eval("cdpoid").ToString()%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="" HeaderText="District">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="cdpoid" HeaderText="CDPO ID" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Helper" HeaderText="Helper count" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Worker" HeaderText="Worker Count" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Central" HeaderText="Central" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="State" HeaderText="State" DataFormatString="{0:0}" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:0}" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnBack" runat="server" CssClass="Button" Text="Back" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 430px;
        height: 160px; display: none;">
        <%-- display: none; --%>
        <asp:Image ID="imgClose2" ToolTip="Close" runat="server" Style="z-index: -1; float: right;
            margin-top: -15px; margin-right: -15px;" onclick="closeMsgPopupNew();" ImageUrl="~/Images/closebtn.png" />
        <center>
            <br />
            <table width="100%">
                <tr>
                    <td align="left" colspan="3" style="color: #094791; font-weight: bold;">
                        &nbsp;&nbsp;&nbsp;Message
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr />
                    </td>
                </tr>
            </table>
            <table width="90%">
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="lblPopupResponse" runat="server" Font-Bold="true" Text=""></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <%-- <asp:Button ID="btnClodeMsg" runat="server" CssClass="Button" Text="Close" />--%>
                        <input id="btnClodeMsg" class="Button" runat="server" type="button" value="OK" style="width: 100px;"
                            onclick="closeMsgPopupnew();" />
                        <asp:Label ID="lblredirect" runat="server" Style="display: none"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <%-- Loader --%>
            <div id="imgrefresh" class="loader transparent">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loader.GIF" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server"
        TargetControlID="UpdatePanel1">
        <Animations>
        <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" /> 
               </Parallel>
            </OnUpdating>
            <OnUpdated>
               <Parallel duration="0">
                   <ScriptAction Script="onComplete();" /> 
               </Parallel>
            </OnUpdated>
        </Animations>
    </cc1:UpdatePanelAnimationExtender>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
