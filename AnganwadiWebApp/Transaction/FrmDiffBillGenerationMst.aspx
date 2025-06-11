<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmDiffBillGenerationMst.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmDiffBillGenerationMst" %>


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
        <div>
            <table align="left">
                <tr>
                    <td style="width: 90px">
                        Division<span style="color: Red"> * </span>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"></asp:DropDownList>
                           <%-- OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />--%>
                    </td>
                    <td style="width: 70px">
                        <%--District--%>
                    </td>
                    <td style="width: 10px">
                     <%--   :--%>
                    </td>
                    <td style="width: 240px">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" visible="false"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bill Date<span style="color: Red"> * </span>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <uc1:Date ID="dtBillDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Salary Date
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="DropDown" Width="110px">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdBoth" runat="server" Text="Both" GroupName="Desg" Checked="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdWorker" runat="server" Text="Worker" GroupName="Desg" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdHelper" runat="server" Text="Helper" GroupName="Desg" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdCentral" runat="server" Text="Central" GroupName="Slab" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdState" runat="server" Text="State" GroupName="Slab" />
                       <%-- &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdFixed" runat="server" Text="Fixed" GroupName="Slab" />--%>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        Sevika
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdRegistered" runat="server" Text="Registered" GroupName="SevikaType"
                            Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdUnregistered" runat="server" Text="Unregistered" GroupName="SevikaType" />
                    </td>
                </tr>--%>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div>
            <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; border: none;">
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="grdDet" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                Width="100%">
                                <RowStyle CssClass="GrdRow"></RowStyle>
                                <Columns>
                                    <asp:BoundField DataField="District" HeaderText="District" FooterText="Total">
                                        <HeaderStyle Width="350px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SevikaCount" HeaderText="Sevika Count" DataFormatString="{0:0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <br />
        <asp:Panel ID="Paneldept" runat="server" Style="overflow: auto; height: 50px; border: none;">
            <asp:Button ID="btnGenBill" runat="server" CssClass="Button" Text="Generate Bill"
                Visible="false" Width="100px" OnClick="btnGenBill_Click" />
        </asp:Panel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
    <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 460px;
        height: 200px; display: none;">
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
</asp:Content>
