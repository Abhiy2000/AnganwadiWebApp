<%@ Page Title="Authorized Sevika" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmSevikaAuthMst.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmSevikaAuthMst" %>

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
    <style type="text/css">
        .style1
        {
            width: 317px;
        }
        .tab-content
        {
            padding-bottom: 23px !important;
        }
    </style>
    <script type="text/javascript">

        //     $(document).ready(function () {

        function ShowAdd() {
            $("#liModify").removeClass("active");
            $("#liAdd").addClass("active");
            $("#liNom").removeClass("active");
            $("#tab-1").show();
            $("#tab-2").hide();
            $("#tab-3").hide();
        }

        function ShowModify() {

            $("#liAdd").removeClass("active");
            $("#liModify").addClass("active");
            $("#liNom").removeClass("active");
            $("#tab-2").show();
            $("#tab-1").hide();
            $("#tab-3").hide();
        }

        function ShowModify1() {

            $("#liNom").addClass("active");
            $("#liAdd").removeClass("active");
            $("#liModify").removeClass("active");
            $("#tab-3").show();
            $("#tab-2").hide();
            $("#tab-1").hide();
        }

        function ShowModify2() {

            $("#liNom").addClass("active");
            $("#liAdd").removeClass("active");
            $("#liModify").removeClass("active");
            $("#tab-1").show();
            $("#tab-2").hide();
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
   <%-- <script src="https://code.jquery.com/jquery-1.11.0.min.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 97%; height: 500px; overflow: auto">
        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li class="active" id="liAdd"><a href="#tab-1" data-toggle="tab" onclick="ShowAdd()">
                    Personal Information</a></li>
                <li id="liModify"><a href="#tab-2" id="amodify" runat="server" data-toggle="tab"
                    onclick="ShowModify()">Payment Details </a></li>
                <li id="liNom"><a href="#tab-3" data-toggle="tab" onclick="ShowModify1()">Nominee Details</a></li>
            </ul>
            <div class="tab-content">
                <div class="active tab-pane" id="tab-1">
                    <table align="center">
                        <tr>
                            <td height="30px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sevika Id<span style="color: Red"> * </span>
                                <asp:HiddenField runat="server" ID="hdnSevikaId" />
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtSevikaId" CssClass="TextBox" runat="server" MaxLength="50" ReadOnly="True"
                                    Width="230px" />
                            </td>
                            <td>
                                Angan Wadi <span style="color: Red">&nbsp;* </span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtAngan" CssClass="TextBox" runat="server" ReadOnly="True" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Full Name<span style="color: Red"> * </span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtName" CssClass="TextBox" runat="server" MaxLength="100" Width="230px" />
                            </td>
                            <td>
                                Middle Name / Father Name <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TxtMidName" CssClass="TextBox" runat="server" MaxLength="100" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtAddr" CssClass="TextBox" runat="server" MaxLength="500" TextMode="MultiLine"
                                    Height="50px" Width="230px" />
                            </td>
                            <td>
                                Village
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TxtVillage" CssClass="TextBox" runat="server" MaxLength="500" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PinCode
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TxtPinCode" CssClass="TextBox" runat="server" MaxLength="6" Width="230px" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterMode="ValidChars" TargetControlID="TxtPinCode" ValidChars="0123456789">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                            <td>
                                Phone No
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtphone" CssClass="TextBox" runat="server" MaxLength="10" Width="230px" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterMode="ValidChars" TargetControlID="txtphone" ValidChars="0123456789">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile No
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TxtMobNo" CssClass="TextBox" runat="server" MaxLength="10" Width="230px" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                    FilterMode="ValidChars" TargetControlID="TxtMobNo" ValidChars="0123456789">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                            <td>
                                Birth Date <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <uc1:Date ID="DtDob" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Religion <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtReligion" CssClass="TextBox" runat="server" ReadOnly="True" Width="230px" />
                            </td>
                            <td>
                                Category
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtCast" CssClass="TextBox" runat="server" ReadOnly="True" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Maritual Status <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtMaritStat" CssClass="TextBox" runat="server" ReadOnly="True"
                                    Width="230px" />
                            </td>
                            <td>
                                <asp:Label ID="lblReason" runat="server">
                                Reason <span style="color: Red">*</span></asp:Label>
                            </td>
                            <td style="width: 10px">
                                <asp:Label ID="Label2" runat="server">:</asp:Label>
                            </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlReason" runat="server" CssClass="DropDown" Enabled="false">
                                    <asp:ListItem>-- Select Option--</asp:ListItem>
                                    <asp:ListItem>Death</asp:ListItem>
                                    <asp:ListItem>Retirement</asp:ListItem>
                                    <asp:ListItem>Resignation</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:RadioButtonList ID="rdbActive" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" style="margin-right: 20px" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" style="margin-right: 20px" Value="N" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                Remark
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TxtRemark" runat="server" CssClass="TextBox" MaxLength="100" Width="230px" ReadOnly="true" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <center>
                        <asp:Button ID="BtnNextTab1" runat="server" Text="Next" CssClass="Button" OnClick="BtnNextTab1_OnClick" />
                    </center>
                </div>
                <br />
                <div class="tab-pane" id="tab-2">
                    <table>
                        <tr>
                            <td>
                                Aadhar No <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtAadharNo" CssClass="TextBox" runat="server" MaxLength="12" Width="230px" />
                                <asp:Button ID="btnTextBoxEventHandler" runat="server" OnClick="btnTextBoxEventHandler_Click"
                                    Style="display: none" />
                            </td>
                            <td>
                                Pan No
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPanNo" CssClass="TextBox" runat="server" MaxLength="20" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Scheme Specific Code<span style="color: Red"> * </span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TxtSevikaCode" CssClass="TextBox" runat="server" MaxLength="20"
                                    ReadOnly="true" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Join Date<span style="color: Red"> *</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DtJoin" runat="server" CssClass="TextBox" ReadOnly="true" Width="100px" />
                                <asp:ImageButton ID="BtnDate" runat="server" ImageUrl="../Images/Cel.png" Width="18px" Enabled="false"
                                    Height="18px" />
                            </td>
                            <td>
                                Order No
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TxtOrderNo" CssClass="TextBox" runat="server" MaxLength="10" Width="230px" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Order Date
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <uc1:Date ID="DtOrder" runat="server" />
                            </td>
                            <td>
                                Retirement Date<span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRetireDt" runat="server" CssClass="TextBox" Enabled="false" Width="230px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Experience
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtExperience" runat="server" CssClass="TextBox" ReadOnly="true" Width="230px"></asp:TextBox>
                            </td>
                            <td>
                                Qualification <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtEdu" CssClass="TextBox" runat="server" ReadOnly="True" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation <span style="color: Red">*</span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtDesgFlag" CssClass="TextBox" runat="server" ReadOnly="true" Width="90px" />
                                <asp:TextBox ID="txtDesg" CssClass="TextBox" runat="server" ReadOnly="true" Width="135px" />
                            </td>
                            <td>
                                Pay Scale<span style="color: Red">* </span>
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtPayScale" CssClass="TextBox" runat="server" ReadOnly="true" Width="230px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtBank" CssClass="TextBox" runat="server" ReadOnly="true" Width="230px" />
                            </td>
                            <td>
                                IFSC
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td width="350px">
                                <asp:TextBox ID="txtIFSC" runat="server" CssClass="TextBox" Width="230px"></asp:TextBox>&nbsp;
                                <%--<asp:LinkButton ID="LinkSerchBankBranch" runat="server" OnClick="LinkSerchBankBranch_OnClick">Validate IFSC</asp:LinkButton>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Branch
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtBranch" CssClass="TextBox" runat="server" ReadOnly="true" Width="230px" />
                            </td>  
                            <td>
                                Account No
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TxtAccNO" runat="server" CssClass="TextBox" MaxLength="20" Width="230px" ReadOnly="true" />
                            </td>                          
                        </tr>
                        <tr>
                            <td>
                                PFMS Code
                            </td>
                            <td style="width: 10px">
                                :
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TxtCPSMSCode" CssClass="TextBox" runat="server" MaxLength="20" Width="230px" ReadOnly="true" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <center>
                        <asp:Button ID="BtnBackTab2" runat="server" Text="Back" CssClass="Button" OnClick="BtnBackTab2_OnClick" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnNextTab2" runat="server" Text="Next" CssClass="Button" OnClick="BtnNextTab2_OnClick" />
                    </center>
                </div>
                <div class="tab-pane" id="tab-3">
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td height="30px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" height="50px">
                                            <b>
                                                <asp:Label ID="Label1" runat="server">---First Nominee Detais---</asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Name
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtNomNM1" CssClass="TextBox" runat="server" MaxLength="100" Width="230px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Relation
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtRel1" CssClass="TextBox" runat="server" Width="230px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Age
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtNomAge1" CssClass="TextBox" runat="server" MaxLength="5" Width="230px" ReadOnly="true" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                FilterMode="ValidChars" TargetControlID="txtNomAge1" ValidChars="0123456789">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Address
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtAddress1" CssClass="TextBox" runat="server" MaxLength="100" TextMode="MultiLine" ReadOnly="true"
                                                Height="50px" Width="230px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="80px">
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td height="30px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" height="50px">
                                            <b>
                                                <asp:Label ID="lblSecond" runat="server">---Second Nominee Detais---</asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Name
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtNomNM2" CssClass="TextBox" runat="server" MaxLength="100" Width="230px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Relation
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtRel2" CssClass="TextBox" runat="server" Width="230px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Age
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtNomAge2" CssClass="TextBox" runat="server" MaxLength="5" Width="230px" ReadOnly="true" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                FilterMode="ValidChars" TargetControlID="txtNomAge2" ValidChars="0123456789">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nominee Address
                                        </td>
                                        <td style="width: 10px">
                                            :
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtAddress2" CssClass="TextBox" runat="server" MaxLength="100" TextMode="MultiLine" ReadOnly="true"
                                                Height="50px" Width="230px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnAuth" runat="server" Text="Authorized" CssClass="Button" ValidationGroup="A"
                                    OnClick="btnAuth_Click" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="BtnBackTab3" runat="server" Text="Back" CssClass="Button" OnClick="BtnBackTab3_OnClick" />
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="Button" ValidationGroup="A"
                                    OnClick="BtnCancel_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
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
</asp:Content>
