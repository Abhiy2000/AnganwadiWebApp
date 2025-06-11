<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmLICApproveMst.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmLICApproveMst" %>


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
        function closeMsgPopupnew1() {
            $find("mpeMsg2").hide();
            debugger;
            return false;
        }

    </script>
    <style type="text/css">
        .style1 {
            width: 317px;
        }

        .tab-content {
            padding-bottom: 23px !important;
        }
    </style>
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
    <script type="text/javascript">
        $(document).ready(function () {
            ShowAdd();
        });

        function ShowAdd() {

            $("#liModify").removeClass("active");
            $("#liAdd").addClass("active");
            $("#liNom").removeClass("active");
            $("#tab-1").show();
            //            $("#tab-2").hide();
            $("#tab-3").hide();
        }

        function ShowModify() {

            //            $("#liAdd").removeClass("active");
            //            $("#liModify").addClass("active");
            //            $("#liNom").removeClass("active");
            //            $("#tab-3").show();
            //            $("#tab-1").hide();
        }

        function ShowModify1() {

            $("#liNom").addClass("active");
            $("#liAdd").removeClass("active");
            $("#liModify").removeClass("active");
            $("#tab-3").show();
            //            $("#tab-2").hide();
            $("#tab-1").hide();
        }

        function ShowModify2() {

            $("#liNom").addClass("active");
            $("#liAdd").removeClass("active");
            $("#liModify").removeClass("active");
            $("#tab-1").show();
            //            $("#tab-2").hide();
            $("#tab-3").hide();
        }
    </script>
    <script type="text/javascript" language="javascript">
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
        }
        function bigImg(x) {
            debugger;
            var BigImg = document.getElementById('<%= BigDocImg.ClientID %>');
            BigImg.setAttribute('src', x.src);
            BigImg.style.display = 'block';
        }

        function normalImg(x) {
            var BigImg = document.getElementById('<%= BigDocImg.ClientID %>');
            BigImg.setAttribute('src', '');
            BigImg.style.display = 'none';
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
    <%--  <script src="https://code.jquery.com/jquery-1.11.0.min.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 97%; height: 500px; overflow: auto">
        <div class="nav-tabs-custom">
            <div class="active tab-pane" id="tab-1">
                <table align="center">
                    <tr>
                        <td height="30px"></td>
                    </tr>
                    <tr>
                        <td>Sevika Id<span style="color: Red"> * </span>
                            <asp:HiddenField runat="server" ID="hdnSevikaId" />
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSevikaId" CssClass="TextBox" runat="server" MaxLength="50" ReadOnly="True"
                                Width="230px" />
                        </td>
                        <td>Anganwadi <span style="color: Red">&nbsp;* </span>
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnganwadi" CssClass="TextBox" runat="server" ReadOnly="True" Width="230px" />
                        </td>
                    </tr>
                    <tr>
                        <td>Sevika Name<span style="color: Red"> * </span>
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtName" CssClass="TextBox" runat="server" MaxLength="100" Width="230px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                FilterMode="ValidChars" TargetControlID="txtName" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td>Middle Name / Father Name <span style="color: Red">*</span>
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtMidName" CssClass="TextBox" runat="server" MaxLength="100" Width="230px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                FilterMode="ValidChars" TargetControlID="TxtMidName" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Address <span style="color: Red">*</span>
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtAddr" CssClass="TextBox" runat="server" MaxLength="500" TextMode="MultiLine"
                                onkeydown="return (event.keyCode!=13);" Height="50px" Width="230px" />
                        </td>
                        <td>Village
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtVillage" CssClass="TextBox" runat="server" MaxLength="500" Width="230px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                FilterMode="ValidChars" TargetControlID="TxtVillage" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>PinCode
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtPinCode" CssClass="TextBox" runat="server" MaxLength="6" Width="230px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                FilterMode="ValidChars" TargetControlID="TxtPinCode" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td>Phone No
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtphone" CssClass="TextBox" runat="server" MaxLength="10" Width="230px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                FilterMode="ValidChars" TargetControlID="txtphone" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Mobile No
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtMobNo" CssClass="TextBox" runat="server" MaxLength="10" Width="230px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                FilterMode="ValidChars" TargetControlID="TxtMobNo" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td>Birth Date <span style="color: Red">*</span>
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <uc1:Date ID="DtDob" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Remark
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtRemark" runat="server" CssClass="TextBox" MaxLength="100" Width="230px" />
                        </td>
                        <td>Designation
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbActive" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                <%--OnSelectedIndexChanged="rdbActive_SelectedIndexChanged">--%>
                                <asp:ListItem Text="Anganwadi Helper" style="margin-right: 20px" Value="H" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Anganwadi Worker" style="margin-right: 20px" Value="W"></asp:ListItem>
                                <%-- <asp:ListItem Text="Mini-Anganwadi" style="margin-right: 20px" Value="M"></asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>Bank
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtBank" runat="server" CssClass="TextBox" Width="230px"></asp:TextBox>

                        </td>
                        <td>IFSC
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td width="350px">
                            <asp:TextBox ID="txtIFSC" runat="server" CssClass="TextBox" Width="230px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                FilterMode="ValidChars" TargetControlID="txtIFSC" ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Branch
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtBranch" runat="server" CssClass="TextBox" Width="230px"></asp:TextBox>
                        </td>
                        <td>Account No
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtAccNO" runat="server" CssClass="TextBox" MaxLength="20" Width="230px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                FilterMode="ValidChars" TargetControlID="TxtAccNO" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Status
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RdoStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                <%--OnSelectedIndexChanged="rdbActive_SelectedIndexChanged">--%>
                                <asp:ListItem Text="Approved" style="margin-right: 20px" Value="A" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Reject" style="margin-right: 20px" Value="R"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>Reason
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtReason" runat="server" CssClass="TextBox" Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 10px"></td>
                        <td></td>
                        <td>View Document
                        </td>
                        <td style="width: 10px">:
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updtpanl" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkDownload" />
                                </Triggers>
                                <ContentTemplate>
                                    <%-- <asp:Image ID="Image1" runat="server" Height="20px" ImageUrl="~/Images/defaultpreview.jpg"
                                                Width="20px" Visible="true" onmouseover="bigImg(this)" onmouseout="normalImg(this)" />--%>
                                    <asp:LinkButton ID="lnkDownload" OnClick="lnkDownload_Click" runat="server" Text="Click Here to Download Document"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>

                    <%--added--%>
                    <tr>
                        <td></td>
                        <td style="width: 10px"></td>
                        <td></td>
                        <td><asp:Label runat="server" ID="lblverify" Visible="false">verify otp</asp:Label> 
                        </td>
                        <td style="width: 10px"><asp:Label runat="server" ID="lbl1" Visible="false">:</asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtotp" runat="server" CssClass="TextBox" MaxLength="20" Width="230px" Visible="false" />
                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                FilterMode="ValidChars" TargetControlID="txtotp" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>--%>
                        </td>
                    </tr>
                </table>

                <br />
                <br />
                <table align="left" style="margin-left: 450px;">
                    <tr>
                        <td>
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" ValidationGroup="A"
                                OnClick="BtnSubmit_Click" />
                        </td>
                        <td width="10px"></td>
                        <td>
                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="Button" ValidationGroup="A"
                                OnClick="BtnCancel_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <asp:ImageButton ID="BigDocImg" runat="server" Height="300px" Width="300px" BorderWidth="1px"
            BorderColor="Black" BorderStyle="Solid" Style="display: none; float: right;" />
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
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
    &nbsp;&nbsp;&nbsp;
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 500px; height: 160px; display: none;">
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
    <cc1:ModalPopupExtender ID="popMsg1" runat="server" BehaviorID="mpeMsg2" TargetControlID="hdnPop6"
        PopupControlID="pnlMessage2" CancelControlID="imgClose3">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop6" runat="server" />
    <asp:Panel ID="pnlMessage2" runat="server" CssClass="Popup" Style="width: 460px; height: 200px; display: none;">
        <%-- display: none; --%>
        <asp:Image ID="imgClose3" ToolTip="Close" runat="server" Style="z-index: -1; float: right; margin-top: -15px; margin-right: -15px;"
            onclick="closeMsgPopupNew1();" ImageUrl="~/Images/closebtn.png" />
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
                        <asp:Label ID="lblPopupResponse1" runat="server" Font-Bold="true" Text="Do you want to save Sevika in InActive Mode ?"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnYes" runat="server" CssClass="Button" Text="Yes" />
                        <%-- <asp:Button ID="btnClodeMsg" runat="server" CssClass="Button" Text="Close" />--%>
                        <input id="btnClodeMsg1" class="Button" runat="server" type="button" value="No" style="width: 100px;"
                            onclick="closeMsgPopupnew1();" />
                        <asp:Label ID="lblredirect1" runat="server" Style="display: none"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
