<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmDocDownload.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmDocDownload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript"> 
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
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>District
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
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
                        OnSelectedIndexChanged="ddlCDPO_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>BIT
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBeat_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>


        </table>
        <br />
        <br />
        <div >
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                 <%--UpdateMode="conditional"--%>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSubmit" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlDistrict" />
                    <asp:PostBackTrigger ControlID="ddlCDPO" />
                    <asp:PostBackTrigger ControlID="ddlBeat" />
                    <asp:PostBackTrigger ControlID="grdLICDEF" />
                    
                 </Triggers>
                <ContentTemplate>
                    <asp:Button ID="BtnSubmit" class="Button" runat="server" Text="Search" ValidationGroup="A"
                        Style="margin-top: 15px; margin-bottom: 20px;" OnClick="BtnSubmit_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       <%-- <asp:UpdatePanel ID="PnlSerch" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdLICDEF" />
            </Triggers>
            <ContentTemplate>
                
            </ContentTemplate>
        </asp:UpdatePanel>--%>

        <div>
            <asp:GridView ID="grdLICDEF" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" OnSelectedIndexChanged="grdLICDEF_SelectedIndexChanged"
                    PageSize="20" CssClass="Grid" Width="100%" Visible="false" OnPageIndexChanging="grdLICDEF_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="divname" HeaderText="Division" />
                        <asp:BoundField DataField="distname" HeaderText="District" />
                        <asp:BoundField DataField="cdponame" HeaderText="CDPO" />
                        <asp:BoundField DataField="bitbitname" HeaderText="Bit" />
                        <asp:TemplateField HeaderText="SevikaID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblSevikaID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SevikaID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="sevikaname" HeaderText="Sevika Name"
                            ItemStyle-Width="300px" />
                        <asp:TemplateField HeaderText="Download" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" OnClick="lnkDownload_Click" runat="server" Text="Download"></asp:LinkButton>
                                <asp:HiddenField ID="hdnDownload" runat="server" Value='<%# Bind("SevikaID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>


                </asp:GridView>
        </div>



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
