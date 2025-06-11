<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmAdditionalChargePay.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmAdditionalChargePay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 750px;
            height: 200px;
        }

        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }
    </style>
    <script type="text/javascript">
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
        .transparent {
            /* IE 8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /* IE 5-7 */
            filter: alpha(opacity=50); /* Netscape */
            -moz-opacity: 0.5; /* Safari 1.x */
            -khtml-opacity: 0.5; /* Good browsers */
            opacity: 0.5;
        }

        .loader {
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

            .loader img {
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
    <div class="Panel">
        <div class="panel-body">
            <table>
                <tr>
                    <td style="width: 112px;">Aadhar No <span style="color: Red">* </span>
                    </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:TextBox CssClass="TextBox" runat="server" ID="txtAdharNo" placeholder="Input Aadhar Number"
                            Width="205px" Height="35px"></asp:TextBox>
                    </td>
                    <td style="padding-left: 70px;">
                        <asp:Button runat="server" ID="btnSearch" class="Button" Text="Search"
                            OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
            <div id="divResult" runat="server" style="border-top: 1px solid #eeeeee; padding:10px; margin-top:10px;">
                <table class="style1">
                    <tr>
                        <td>CDPO
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <asp:TextBox CssClass="TextBox" runat="server" ID="txtCDPO" Width="205px" Enabled="false" BackColor="#EEEEEE"></asp:TextBox>
                        </td>
                        <td>BIT
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <asp:TextBox CssClass="TextBox" runat="server" ID="txtBitCompId" Width="205px" Enabled="false" BackColor="#EEEEEE"></asp:TextBox>
                            <asp:TextBox CssClass="TextBox" runat="server" ID="txtCompId" Width="205px" Visible="false" Enabled="false" BackColor="#EEEEEE"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Name
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <asp:TextBox CssClass="TextBox" runat="server" ID="txtName" Width="205px" Enabled="false" BackColor="#EEEEEE"></asp:TextBox>
                        </td>
                        <td>Anganwadi
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <asp:TextBox CssClass="TextBox" runat="server" ID="txtExistAnganwadi" Width="205px" Enabled="false" BackColor="#EEEEEE"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Anganwadi<span style="color: Red"> * </span>
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlAnganID" runat="server" CssClass="DropDown" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>From Date<span style="color: Red"> * </span>
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <uc1:Date ID="dtFrom" runat="server" />
                        </td>
                        <td>To Date<span style="color: Red"> * </span>
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <uc1:Date ID="dtTo" runat="server" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 101px;">Reason<span style="color: Red"> * </span>
                        </td>
                        <td style="width: 10px">:</td>
                        <td>
                            <asp:TextBox CssClass="TextBox" runat="server" ID="txtReason" Width="575px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="margin-left: 250px;">
                    <br />
                    <asp:HiddenField ID="hdnSavikaId" runat="server" />
                    <asp:Button runat="server" ID="btnSubmit" class="Button" Text="Submit" OnClick="btnSubmit_Click"
                        Style="margin-right: 15px;" />
                    <asp:Button runat="server" ID="btnCancel" class="Button" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </div>
    <div>
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
        <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 430px; height: 160px; display: none;">
            <%-- display: none; --%>
            <asp:Image ID="imgClose2" ToolTip="Close" runat="server" Style="z-index: -1; float: right; margin-top: -15px; margin-right: -15px;"
                onclick="closeMsgPopupNew();" ImageUrl="~/Images/closebtn.png" />
            <center>
                <br />
                <table width="100%">
                    <tr>
                        <td align="left" colspan="3" style="color: #094791; font-weight: bold;">&nbsp;&nbsp;&nbsp;Message
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
    </div>
</asp:Content>
