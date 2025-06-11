<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmLICDocUploadList.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmLICDocUploadList" %>

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

        function redirectToNewWindow() {

            window.open("../Transaction/FrmLICDocUploadMst.aspx", "_blank");
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

        .btInGrid {
            color: #000;
            padding: 2px 5px;
            background-color: #F6C60F;
            border: solid 1px #F69E14;
            border-radius: 2px;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            width: 150px;
            cursor: pointer;
            display: block;
        }
    </style>
    <script type="text/javascript">
        function bigImg(x) {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />

    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <table align="left" style="width: 100%;">
            <tr>
                <td>Division
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>District
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>CDPO
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>BIT
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>


        </table>
        <asp:Panel ID="PnlButton" runat="server">
            <div>
                <table align="center">
                    <tr>
                        <td>
                            <asp:UpdatePanel runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnExternal" />
                                    <asp:PostBackTrigger ControlID="BtnDeath" />
                                    <asp:PostBackTrigger ControlID="BtnRetirement" />
                                    <asp:PostBackTrigger ControlID="BtnResignation" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <asp:Button ID="BtnExternal" class="Button" runat="server" Text="External" ValidationGroup="A"
                                Style="margin-top: 15px; width: auto;" OnClick="BtnExternal_Click" /></td>
                        <td>
                            <asp:Button ID="BtnDeath" class="Button" runat="server" Text="Death" ValidationGroup="A"
                                Style="margin-top: 15px;" OnClick="BtnDeath_Click" /></td>
                        <td>
                            <asp:Button ID="BtnRetirement" class="Button" runat="server" Text="Retirement" ValidationGroup="A"
                                Style="margin-top: 15px;" OnClick="BtnRetirement_Click" /></td>
                        <td>
                            <asp:Button ID="BtnResignation" class="Button" runat="server" Text="Termination/ Resignation" ValidationGroup="A"
                                Style="margin-top: 15px; width: auto;" OnClick="BtnResignation_Click" /></td>

                    </tr>
                </table>
            </div>
        </asp:Panel>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePaneldoc2" runat="server" ChildrenAsTriggers="false"
            UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdLICDEF" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="grdLICDEF" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" OnSelectedIndexChanged="grdLICDEF_SelectedIndexChanged"
                    PageSize="20" CssClass="Grid" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Bit" HeaderText="Bit" />
                        <asp:BoundField DataField="SevikaID" HeaderText="SevikaID" />
                        <asp:BoundField DataField="SevikaName" HeaderText="Sevika Name"
                            ItemStyle-Width="300px" />
                        <asp:BoundField DataField="AadharNo" HeaderText="Aadhar No" />
                        <asp:BoundField DataField="ExitDate" HeaderText="Exit Date"
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Reason" HeaderText="Remark" />
                        <asp:BoundField DataField="RejectReason" HeaderText="Reject Reason" />
                        <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemark" runat="server" Width="250px" Height="25px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upload PDF" ItemStyle-Width="78px" >
                            <ItemTemplate>
                                <table style="border: none;">
                                    <tr>
                                        <td style="border: none;">
                                            <div class="">
                                                <asp:FileUpload ID="FileUploadDocPDF" runat="server" Width="78px"
                                                    onchange="this.form.submit()" />
                                            </div>
                                        </td>
                                        <td style="border: none;">
                                            <asp:Image ID="ImgDoc" runat="server" GenerateEmptyAlternateText="False"
                                                Height="20px" Visible="false" />
                                            <asp:Image ID="ImgDocPDF1" runat="server" GenerateEmptyAlternateText="False"
                                                Height="20px" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="" ShowSelectButton="True"
                            SelectText="View Sevika">
                            <HeaderStyle Width="90px" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSevikaID" runat="server" DataField="SevikaID" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>


                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<div class="Panel" style="width: 1125px; height: 500px; overflow: auto">--%>
        <asp:Panel ID="PnlSerch" runat="server">
            <div>
                <asp:Label ID="lblError" runat="server" Style="color: Red;"></asp:Label>
            </div>
            <div>
                <asp:ImageButton ID="BigDocImg" runat="server" Height="250px" Width="226px" BorderWidth="1px"
                    BorderColor="Black" BorderStyle="Solid" Style="margin-top: -440px; display: none; float: right;" />
            </div>
            <div style="text-align: center; margin-top: 20px;">
                <asp:CheckBox ID="chkTerms" AutoPostBack="True"
                    Text=""
                    ValidationGroup="vg" runat="Server" OnCheckedChanged="chkTerms_CheckedChanged" />
                <span><b>I agree with above filled information which is correct
                    <br />
                    as per my good knowledge and received documents</b></span>
                <asp:CustomValidator ID="vTerms"
                    ClientValidationFunction="validateTerms"
                    ErrorMessage="<br/>Terms and Conditions are required."
                    ForeColor="Red"
                    Display="Static"
                    EnableClientScript="true"
                    ValidationGroup="vg"
                    runat="server" />
            </div>
            <div style="margin-left: 500px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnSubmit" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Button ID="BtnSubmit" class="Button" runat="server" Text="Submit" ValidationGroup="A"
                            Style="margin-top: 15px;" OnClick="BtnSubmit_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

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
        </asp:Panel>

    </div>
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
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
</asp:Content>
