<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmPolicyUpdateMst.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmPolicyUpdateMst" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 97%; height: 468px; overflow: auto">
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table>
                <tr>
                    <td style="width: 90px">Division
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                    </td>
                    <td style="width: 70px">District
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 240px">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td>CDPO
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged" />
                    </td>
                    <td>BIT
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Button" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="Button" OnClick="btnBack_Click" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>

        </asp:Panel>
        <asp:Panel ID="PnlSerch" runat="server">
            <hr />
            <table>
                <tr>
                    <td class="style1">
                        <asp:GridView ID="GrdPolicyUpdate" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            OnSelectedIndexChanged="GrdPolicyUpdate_SelectedIndexChanged" OnRowDataBound="GrdPolicyUpdate_RowDataBound">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <%-- <asp:CommandField HeaderText="Modify" ShowSelectButton="True" SelectText="Modify">
                                    <HeaderStyle Width="60px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Sevika Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsevikacode" runat="server" Text='<%# Eval("sevikacode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Addhar No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblaadharno" runat="server" Text='<%# Eval("aadharno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Date of Birth">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldob" runat="server" Text='<%# Eval("dob") %>' DataFormatString="{0:dd-MMM-yyyy}" />
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Age">
                                    <ItemTemplate>
                                        <asp:Label ID="lblage" runat="server" Text='<%# Eval("age") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("type_") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="PMJJBY Policy No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMJJBYNo" runat="server"  Text='<%# Bind("pmjjbyno") %>' CssClass="TextBox" Height="28px" Width="110px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:TemplateField HeaderText="PMJJBY Policy Date">
                                    <ItemTemplate>
                                        <uc1:Date ID="dtPMJJBY" runat="server" Width="150px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="PMJJBY Policy Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMJJBY" runat="server" Width="150px" />
                                        <ajaxToolkit:MaskedEditExtender ID="meDate" runat="server" TargetControlID="txtPMJJBY" Mask="99/99/9999" ClearMaskOnLostFocus="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="PMSBY Policy No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMSBYNo" runat="server"  Text='<%# Bind("pmsbyno") %>' CssClass="TextBox" Height="28px" Width="130px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="PMSBY Policy">
                                    <ItemTemplate>
                                        <uc1:Date ID="dtPMSBY" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:BoundField DataField="saldate" HeaderText="Salary Date" DataFormatString="{0:dd-MMM-yyyy}" />--%>
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                        </asp:GridView>
                    </td>
                </tr> 
                               
            </table>
            <br />
            <table>
                <tr>
                    <td style="width: 100px"></td>
                    <td style="width: 100px"></td>
                    <td style="width: 50px"></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="Button" OnClick="btnSubmit_Click" Visible="false"/>
                    </td>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
        
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
    </div>
</asp:Content>
