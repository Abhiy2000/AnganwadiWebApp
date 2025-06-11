<%@ Page Title="Transfer of Sevika" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmTransferAuth.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmTransferAuth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <div class="Panel" style="width: 100%; height: 468px; overflow: auto">
        <asp:Panel ID="PnlDetail" runat="server">
            <fieldset>
                <legend style="font-family: Times New Roman; font-size: large;">Current Information</legend>
                <table>
                    <tr>
                        <td style="width: 90px">
                            Name
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td colspan="5">
                            <asp:TextBox runat="server" CssClass="TextBox" Width="550px" ID="txtSevikaName" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px">
                            Designation
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtDesg" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td style="width: 90px">
                            Payscale
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtPayscale" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px">
                            Division
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtCurrDiv" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td style="width: 90px">
                            District
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtCurrDist" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px">
                            CDPO
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtCurrCDPO" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td style="width: 90px">
                            BIT
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtCurrBIT" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 110px">
                           Old Anganwadi
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td colspan="5">
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtCurrAnganwadi" ReadOnly="true"
                                Width="550px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <br />
            <fieldset>
                <legend style="font-family: Times New Roman; font-size: large">Transfer To</legend>
                <table>
                    <tr>
                        <td style="width: 90px">
                            Division
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtDiv" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td style="width: 90px">
                            District
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtDist" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px">
                            CDPO
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtCDPO" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td style="width: 90px">
                            BIT
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtBIT" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px">
                            Aadhar No
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtAadharNo" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 110px">
                            New Anganwadi
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td colspan="5">
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtNewAnganwadi" ReadOnly="true"
                                Width="550px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px">
                           Remark
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="TextBox" ID="txtRemark" TextMode="MultiLine" Height="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="30px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                        </td>
                        <td style="width: 10px">
                            
                        </td>
                        <td>
                             <asp:Button ID="btnAccept" runat="server" CssClass="Button" Text="Accept" OnClick="btnAccept_Click" />
                             &nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnReject" runat="server" CssClass="Button" Text="Reject" 
                                 onclick="btnReject_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
                PopupControlID="pnlMessage1" CancelControlID="imgClose2">
            </ajaxToolkit:ModalPopupExtender>
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
</asp:Content>
