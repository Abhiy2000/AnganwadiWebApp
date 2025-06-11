<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmDemotionMst.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmDemotionMst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 920px;
            height: 260px;
        }
        .rbl input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 1px;
        }
    </style>
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
    function checkDate(sender, args) {
        javascript: __doPostBack('<%= dummybtn.UniqueID %>', '')
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Panel">
        <div class="boxHead">
            <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
        </div>
        <hr />
        <div class="panel-body">
            <table class="style1">
                <%--  <tr>
                    <td>
                        Aadhar No
                    </td>
                    <td colspan="6">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtAdharNo" placeholder="Input Aadhar Number"
                            Width="205px"></asp:TextBox>
                        &nbsp; &nbsp;<asp:Button runat="server" ID="btnSearch" class="Button" Text="Search"
                            OnClick="btnSearch_Click" Visible="false" />
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        Division
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtDivision" Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        District
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtDistrict" Width="205px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        CDPO
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtCDPO" Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        BIT
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtBitCompId" Width="205px"></asp:TextBox>
                        <asp:TextBox CssClass="TextBox" runat="server" ID="txtCompId" Width="205px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtName" Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Aadhar No
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtAdharNo" placeholder="Input Aadhar Number"
                            Width="205px"></asp:TextBox>
                        &nbsp; &nbsp;<asp:Button runat="server" ID="btnSearch" class="Button" Text="Search"
                            OnClick="btnSearch_Click" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Join Date
                    </td>
                    <td colspan="1">
                      :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtJoinDt"
                            Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Experience
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtExperience" Width="205px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Qualification
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtQualification"
                            Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Designation
                    </td>
                    <td colspan="1">
                        :&nbsp;&nbsp;
                       <asp:DropDownList ID="ddlDesgFlag" runat="server" CssClass="DropDown" AutoPostBack="true" Enabled="false"
                            Width="90px" Font-Overline="False"  OnSelectedIndexChanged="ddlFlag_SelectedIndexChanged">
                            <asp:ListItem Text="--Select Option--" Value=""></asp:ListItem>
                            <asp:ListItem Text="Worker" Value="W"></asp:ListItem>
                            <asp:ListItem Text="Helper" Value="H"></asp:ListItem>
                            <asp:ListItem Text="Mini-Anganwadi" Value="M"></asp:ListItem>
                        </asp:DropDownList>                       
                        <asp:TextBox CssClass="TextBox" runat="server" ID="txtDesignation" Width="105px"></asp:TextBox>
                        <asp:TextBox CssClass="TextBox" runat="server" ID="txtDesigID" Width="105px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Payscale
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtPayscale" Width="205px"></asp:TextBox>
                         <asp:TextBox CssClass="TextBox" runat="server" ID="txtOldPayscale" Width="105px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px; padding-top: 10px;">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Maroon">Demotion</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Demotion Date
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;
                        <asp:Button ID="dummybtn" runat="server" Text="Click" Style="display: none" OnClick="dummybtn_Click" />
                        <asp:TextBox ID="txtPromoteDate" runat="server" CssClass="TextBox" ReadOnly="true"
                            Width="100px" />
                        <asp:ImageButton ID="BtnPromoteDate" runat="server" ImageUrl="../Images/Cel.png"
                            Width="18px" Height="18px" />
                        <ajaxToolkit:CalendarExtender ID="Cale1" runat="server" TargetControlID="txtPromoteDate"
                            PopupButtonID="BtnPromoteDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Experience
                    </td>
                    <td colspan="1">
                        :&nbsp;&nbsp;
                        <asp:TextBox CssClass="TextBox" runat="server" ID="txtExp" Width="205px" ReadOnly="true" Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="ddlExperience" runat="server" CssClass="DropDown" AutoPostBack="true"  Width="205px"
                          OnSelectedIndexChanged="ddlExperience_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Qualification
                    </td>
                    <td colspan="1">
                        :&nbsp; &nbsp;
                         <asp:TextBox CssClass="TextBox" runat="server" ID="txtQuali" Width="205px" ReadOnly="true"  Visible="false"></asp:TextBox>
                         <asp:DropDownList ID="ddlEduID" runat="server" CssClass="DropDown" Width="205px"
                           AutoPostBack="true" OnSelectedIndexChanged="ddlEduID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Designation
                    </td>
                    <td colspan="1">
                        :&nbsp;&nbsp;
                         <asp:TextBox CssClass="TextBox" runat="server" ID="txtDesgin" Width="205px" ReadOnly="true"></asp:TextBox>
                        <asp:DropDownList ID="ddlFlag" runat="server" CssClass="DropDown" AutoPostBack="true" Visible="false"
                            Width="90px" Font-Overline="False"  OnSelectedIndexChanged="ddlFlag_SelectedIndexChanged">
                            <asp:ListItem Text="--Select Option--" Value=""></asp:ListItem>
                            <asp:ListItem Text="Worker" Value="W"></asp:ListItem>
                            <asp:ListItem Text="Helper" Value="H"></asp:ListItem>
                            <asp:ListItem Text="Mini-Anganwadi" Value="M"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:TextBox CssClass="TextBox" runat="server" ID="txtDesgID" Width="105px" ReadOnly="true"></asp:TextBox>
                        <asp:DropDownList ID="ddldesigID" runat="server" CssClass="DropDown" AutoPostBack="true"  Visible="false"
                            OnSelectedIndexChanged="ddldesigID_SelectedIndexChanged" Width="90px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Payscale
                    </td>
                    <td colspan="1">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPayscaleID" runat="server" CssClass="DropDown" Width="205px" > <%--Enabled="false"--%>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <center>
                <br />
                <asp:HiddenField ID="hdnSavikaId" runat="server" />
                <asp:Button runat="server" ID="btnDelete" class="Button" Text="Delete" OnClick="btnDelete_Click"
                    Style="margin-right: 15px;" Visible="false" />
                <asp:Button runat="server" ID="btnSubmit" class="Button" Text="Submit" OnClick="btnSubmit_Click"
                    Style="margin-right: 15px;" />
                <asp:Button runat="server" ID="btnCancel" class="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </center>
        </div>
    </div>
    <div>
        <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
            PopupControlID="pnlMessage1" CancelControlID="imgClose2">
        </cc1:ModalPopupExtender>
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
    </div>
</asp:Content>
