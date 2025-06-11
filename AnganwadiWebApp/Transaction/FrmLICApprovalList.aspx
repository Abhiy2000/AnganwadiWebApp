<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmLICApprovalList.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmLICApprovalList" %>

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

        function redirectToNewWindow() {

            window.open("../Transaction/FrmLICApproveMst.aspx?@=1", "_blank");//?@=1
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
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 1125px; height: 500px; overflow: auto">
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="BtnExternal" class="Button" runat="server" Text="External" ValidationGroup="A"
                            Style="margin-top: 15px; width: auto;" OnClick="BtnExternal_Click" />
                    </td>
                    <td>
                        <asp:Button ID="BtnDeath" class="Button" runat="server" Text="Death" ValidationGroup="A"
                            Style="margin-top: 15px;" OnClick="BtnDeath_Click" />
                    </td>
                    <td>
                        <asp:Button ID="BtnRetirement" class="Button" runat="server" Text="Retirement" ValidationGroup="A"
                            Style="margin-top: 15px;" OnClick="BtnRetirement_Click" />
                    </td>
                    <td>
                        <asp:Button ID="BtnResignation" class="Button" runat="server" Text="Termination/ Resignation"
                            ValidationGroup="A" Style="margin-top: 15px; width: auto;" OnClick="BtnResignation_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <asp:Panel ID="PnlSerch" runat="server" Style="height: 500px;">
            <div>
                <asp:Label ID="lblError" runat="server" Style="color: Red;"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="grdLICDEF" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="grdLICDEF" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            PageSize="10" CssClass="Grid" Width="100%" OnSelectedIndexChanged="grdLICDEF_SelectedIndexChanged"
                            OnRowDataBound="grdLICDEF_OnRowDataBound">
                            <Columns>
                                <asp:CommandField SelectText="Select" ShowSelectButton="true" HeaderText="Select" />
                                <asp:BoundField DataField="divname" HeaderText="Division" />
                                <asp:BoundField DataField="distname" HeaderText="District" />
                                <asp:BoundField DataField="cdponame" HeaderText="CDPO" ItemStyle-Width="30%" />
                                <asp:BoundField DataField="bitbitname" HeaderText="BIT" ItemStyle-Width="30%" />
                                <asp:TemplateField HeaderText="SevikaID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemStyle HorizontalAlign="left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSevikaID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SevikaID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:BoundField DataField="SevikaID" HeaderText="SevikaID" />--%>
                                <asp:BoundField DataField="SevikaName" HeaderText="Sevika Name" ItemStyle-Width="30%" />
                                <asp:BoundField DataField="AadharNo" HeaderText="Aadhar No" />
                                <asp:BoundField DataField="ExitDate" HeaderText="Exit Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="ExitReason" HeaderText="Exit Reason" />
                                <%--<asp:TemplateField HeaderText="View Document" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePaneldoc" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Image ID="ImgDoc" runat="server" Height="20px" ImageUrl="../Images/defaultpreview.jpg"
                                                    Width="20px" Visible="true" onmouseover="bigImg(this)" onmouseout="normalImg(this)" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="LastSalary" HeaderText="Last Salary" />
                                <asp:BoundField DataField="ClaimAmount" HeaderText="Claim Amount" />
                                <asp:BoundField DataField="OLDTSOFTWARENO" HeaderText="Old Training Software No." />
                                <asp:BoundField DataField="Flag" HeaderText="Flag" />
                                <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-Width="10%" HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkBtnViewDoc" Text="" OnClick="lnkBtnViewDoc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" OnClick="lnkDownload_Click" runat="server" Text="Download"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnDownload" runat="server" Value='<%# Bind("SevikaID") %>' />
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
