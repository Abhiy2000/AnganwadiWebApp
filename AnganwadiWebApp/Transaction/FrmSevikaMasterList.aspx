<%@ Page Title="Sevika Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmSevikaMasterList.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmSevikaMasterList" %>

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
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table>
                <tr>
                    <td style="width: 90px">
                        Division
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                    </td>
                    <td style="width: 70px">
                        District
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 240px">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td>
                        CDPO
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged" />
                    </td>
                    <td>
                        BIT
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlSerch" runat="server">
            <hr />
            <asp:Button ID="btnNew" runat="server" Text="Enter New Sevika" CssClass="Button"
                OnClick="btnNew_Click" Width="150px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnRejoinSevika" runat="server" Text="Sevika Rejoin" CssClass="Button"
                OnClick="btnRejoinSevika_Click" Width="150px" /> 
            <hr />
            <table>
                <tr>
                    <td style="width: 90px">
                        Sevika Name
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 230px">
                        <asp:TextBox ID="txtSevikaName" runat="server" CssClass="TextBox"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                            FilterMode="ValidChars" TargetControlID="txtSevikaName" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                    </td>
                    <td style="width: 90px">
                        Aadhar No.
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 230px">
                        <asp:TextBox ID="txtAadharNo" runat="server" CssClass="TextBox" MaxLength="12"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            FilterMode="ValidChars" TargetControlID="txtAadharNo" ValidChars="0123456789">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                    </td>
                    <td style="width: 90px">
                        Anganwadi
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAnganID" runat="server" CssClass="DropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <br />
                        <asp:Button ID="search" runat="server" Text="Search" CssClass="Button" OnClick="search_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblrowCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td class="style1">
                        <asp:GridView ID="GrdSevikaList" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="10" OnRowDataBound="GrdSevikaList_RowDataBound"
                            OnSelectedIndexChanged="GrdSevikaList_SelectedIndexChanged" OnPageIndexChanging="GrdSevikaList_PageIndexChanging">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:CommandField HeaderText="Modify" ShowSelectButton="True" SelectText="Modify">
                                    <HeaderStyle Width="60px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <HeaderStyle Width="70px" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="num_sevikamaster_sevikaid" HeaderText="Sevika ID">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="var_angnwadimst_angnname" HeaderText="Anganwadi">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="num_sevakmaster_anganid" HeaderText="Angan Id" />
                                <asp:BoundField DataField="var_sevikamaster_name" HeaderText="Name">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="var_sevikamaster_sevikacode" HeaderText="Scheme Specific Code">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="var_education_educname" HeaderText="Education" />
                                <asp:BoundField DataField="date_sevikamaster_birthdate" HeaderText="Birth Date" />
                                <asp:BoundField DataField="var_sevikamaster_address" HeaderText="Address">
                                    <HeaderStyle Width="230px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="num_sevikamaster_mobileno" HeaderText="Mobile No" />
                                <asp:BoundField DataField="var_sevikamaster_active" HeaderText="Status" />
                                <asp:BoundField DataField="var_angnwadimst_angncode" HeaderText="Anganwadi Code" />
                                <asp:BoundField DataField="var_designation_desig" HeaderText="Desigation" />
                                <asp:BoundField DataField="var_payscal_payscal" HeaderText="Pay Scale" />
                                <asp:BoundField DataField="var_bankbranch_branchname" HeaderText="Branch Name" />
                                <asp:BoundField DataField="var_bank_bankname" HeaderText="Bank Name" />
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
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
