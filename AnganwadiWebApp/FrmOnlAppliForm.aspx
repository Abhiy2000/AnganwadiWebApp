<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageCitizen.Master" AutoEventWireup="true" CodeBehind="FrmOnlAppliForm.aspx.cs"
    Inherits="AnganwadiWebApp.FrmOnlAppliForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>

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
    <script type="text/javascript">
    function autoSubmitFileUpload(input) {
        if (input.value) {
            __doPostBack('', '');
        }
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

        .style2 {
            height: 27px;
        }

        .style3 {
            width: 11px;
        }

        .style4 {
            width: 12px;
        }

        .style5 {
            height: 35px;
        }

        .style6 {
            height: 15px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 100%; overflow: auto">
        <div>
            <table width="100%">
                <tr>
                    <td align="left">Division<asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style3">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                    </td>
                    <td align="left">District<asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Project<asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlproj" runat="server" CssClass="DropDown" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Name of Applicant<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtNameApp" runat="server" CssClass="TextBox" TextMode="MultiLine" Width="230px" Height="50px" />
                    </td>
                    <td align="left">Anganwadi<asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlAnganwadi" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Address of Applicant<asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtAddress" runat="server" CssClass="TextBox" TextMode="MultiLine" Width="230px" Height="50px" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Position<asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlporition" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Birth Date<asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<uc1:Date ID="birthdt" runat="server" />
                    </td>
                    <td align="left">Physically Handicapped<asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:RadioButtonList ID="rbdhandicapp" runat="server" RepeatDirection="Horizontal" Style="margin-top: -10px;">
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N" style="margin-left: 15px;"></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left">Age<asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txthandicapAge" runat="server" CssClass="TextBox" Width="230px" MaxLength="3" />
                    </td>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                        TargetControlID="txthandicapAge" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>
                    <td align="left">Disability %:<asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtDisability" runat="server" CssClass="TextBox" Width="230px" />
                    </td>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                        TargetControlID="txtDisability" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>
                    <td align="left">Age<asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtage" runat="server" Text="0" Enabled="false" CssClass="TextBox" Width="100px" MaxLength="3" />
                        <asp:Button ID="btnCalculate" runat="server" CssClass="Button" Text="Calculate Age" OnClick="btnCalculate_Click" Width="120px" />
                    </td>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom"
                        TargetControlID="txtage" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>
                </tr>
                <tr>
                    <td align="left">Marital Status<asp:Label ID="Label13" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlMarStatus" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Educational Qualification<asp:Label ID="Label14" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlEduQuali" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Aadhar No<asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtAadhar" runat="server" CssClass="TextBox" Width="230px" MaxLength="12" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Pan No<asp:Label ID="Label16" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtPanNo" runat="server" CssClass="TextBox" Width="230px" MaxLength="12" />
                    </td>
                    <td align="left">Religion<asp:Label ID="Label17" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtReligion" runat="server" CssClass="TextBox" Width="230px" />
                    </td>
                    <td align="left">Cast<asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtcast" runat="server" CssClass="TextBox" Width="230px" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Subcast<asp:Label ID="Label19" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtSubcast" runat="server" CssClass="TextBox" Width="230px" />
                    </td>
                </tr>
            </table>
            <hr />
            <table width="100%">
                <tr align="center">
                    <td align="left">Document Attach<asp:Label ID="Label20" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>

                <tr align="center">
                    <td>
                        <asp:GridView ID="grdDoc" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                            Width="100%" AutoGenerateColumns="false" OnRowCommand="grdDoc_RowCommand"
                            AllowPaging="True" PageSize="10" OnRowDataBound="grdDoc_RowDataBound"
                            OnSelectedIndexChanged="grdDoc_SelectedIndexChanged">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="Docid" HeaderText="DocumentID" />
                                <asp:TemplateField HeaderText="Document Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocNames" Text='<%#Eval("Docname")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upload">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="FlDoc" runat="server" onchange="autoSubmitFileUpload(this)" accept=".png, .jpg, .jpeg, .pdf"></asp:FileUpload>
                                        <asp:Image runat="server" ID="UplImg" Width="50" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server"
                                            CssClass="btn btn-secondary" CommandName="PreviewFile" CommandArgument='<%# Eval("Docid") %>' Text="View"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
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
    <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 460px; height: 200px; display: none;">
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
