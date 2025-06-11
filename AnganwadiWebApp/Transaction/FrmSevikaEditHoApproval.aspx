<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmSevikaEditHoApproval.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmSevikaEditHoApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" language="javascript">
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
        }
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
        p {
            margin: 0 0 1px;
        }

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
    </div>
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium">Transaction >> Sevika Edit HO Approval </asp:Label>
    </div>
    <br />
    <div class="Panel" style="height: 580px; width: 1150px; overflow: auto;">
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

                <%-- <td>CDPO
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>--%>
            </tr>


        </table>
        <br />
        <br />
        <hr />
        <asp:Panel ID="PnlSerch" runat="server" Style="height: 560px; width: 100%;">
            <div>
                <asp:Label ID="lblError" runat="server" Style="color: Red;"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="GridCdpoScrn" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="GridCdpoScrn" runat="server" AutoGenerateColumns="false" AllowPaging="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No Records Found!" ShowHeaderWhenEmpty="true"
                            PageSize="10" CssClass="Grid" Width="100%" OnPageIndexChanging="GridCdpoScrn_PageIndexChanging" OnRowDataBound="GridCdpoScrn_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="sevikaedit_id" HeaderText="" />
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <HeaderStyle Width="70px" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="sevikaname" HeaderText="Sevika Details" />
                                <asp:TemplateField HeaderText="Existing">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <p>DOB:<asp:Label ID="lblOldDob" runat="server" Text='<%# Eval("old_dob") %>' /></p>
                                        <p>Join Date:<asp:Label ID="lblOldDoJ" runat="server" Text='<%# Eval("old_joindate") %>' /></p>
                                        <p>Aadhar No:<asp:Label ID="lblOldAadharNo" runat="server" Text='<%# Eval("old_aadharno") %>' /></p>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Change">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <p>
                                            DOB:
                                            <asp:Label ID="lblNewDob" runat="server" Text='<%# Eval("new_dob") %>' />
                                        </p>
                                        <p>
                                            Join Date:
                                            <asp:Label ID="lblNewDoJ" runat="server" Text='<%# Eval("new_joindate") %>' />
                                        </p>
                                        <p>
                                            Aadhar No:<asp:Label ID="lblNewAadharNo" runat="server" Text='<%# Eval("new_aadharno") %>' />
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BIT Doc." HeaderStyle-Width="6%">
                                    <ItemTemplate>
                                        <p>
                                            DOB: 
                                            <asp:LinkButton ID="lnkDownloadDob" OnClick="lnkDownload_Click" runat="server" CommandArgument="BirthDate"></asp:LinkButton>
                                        </p>
                                        <p>
                                            Join Date: 
                                            <asp:LinkButton ID="lnkDownloadDoj" OnClick="lnkDownload_Click" runat="server" CommandArgument="JoinDate"></asp:LinkButton>
                                        </p>
                                        <p>
                                            Aadhar No:<asp:LinkButton ID="lnkDownloadAadhar" OnClick="lnkDownload_Click" runat="server" CommandArgument="Aadhar"></asp:LinkButton>
                                        </p>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DPO Document" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <p>
                                            <asp:LinkButton ID="lnkDownloadDPO" OnClick="lnkDownload_Click" runat="server" CommandArgument="DpoDocuments"></asp:LinkButton>
                                        </p>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:RadioButtonList runat="server" ID="RdbStatus" RepeatDirection="Horizontal" BorderStyle="None">
                                            <asp:ListItem Value="A" Text="Approve"></asp:ListItem>
                                            <asp:ListItem Value="R" Text="Reject"></asp:ListItem>
                                        </asp:RadioButtonList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remark">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" Text='<%# Eval("HoApr_remark") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div>
                <asp:ImageButton ID="BigDocImg" runat="server" Height="300px" Width="300px" BorderWidth="1px"
                    BorderColor="Black" BorderStyle="Solid" Style="display: none; float: right;" />
            </div>

            <div style="margin-left: 500px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">

                    <ContentTemplate>
                        <asp:Button ID="BtnSubmit" class="Button" runat="server" Text="Submit" ValidationGroup="A"
                            Style="margin-top: 15px;" OnClick="BtnSubmit_Click" />

                        <asp:Button ID="BtnExit" class="Button" runat="server" Text="Exit" ValidationGroup="A"
                            Style="margin-top: 15px;" OnClick="BtnExit_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnSubmit" />
                        <asp:PostBackTrigger ControlID="ddlDistrict" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>

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


