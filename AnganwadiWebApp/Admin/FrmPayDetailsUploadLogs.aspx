<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="FrmPayDetailsUploadLogs.aspx.cs"
    Inherits="AnganwadiWebApp.Admin.FrmPayDetailsUploadLogs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<head id="Head1" runat="server">
    <title>Upload Log</title>
    <link href="../CSS/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .body
        {
            font-family: "lucida grande" ,tahoma,verdana,arial,sans-serif;
            font-size: 12px;
            color: #333333;
        }
        
        .Panel
        {
            font-family: "lucida grande" , tahoma, verdana, arial, sans-serif;
            margin: 5px 0 10px 0;
            border-collapse: collapse;
        }
        
        .Panel td
        {
            padding: 5px;
            height: 20px;
        }
        
        .Panel th
        {
            padding: 5px;
            background-color: #3b5998;
            font-size: 11px;
            height: 20px;
            color: #cccccc;
            font-weight: bold;
            text-align: left;
        }
        .Panel tr:hover
        {
            background-color: #d8dfea;
            color: #3b5998;
        }
        .Panel .PRow
        {
            background-color: #f7f7f7;
        }
        .Panel .PAltRow
        {
            background-color: #ffffff;
        }
        
        
        .body a
        {
            color: #3b5998;
            text-decoration: none;
            font-size: 11px;
            font-weight: bold;
        }
        
        .body a:hover
        {
            text-decoration: underline;
        }
        
        .Grid
        {
            font-family: "lucida grande" , tahoma, verdana, arial, sans-serif;
            margin: 5px 0 10px 0;
            border: solid 1px #3b5998;
            border-collapse: collapse;
        }
        
        .Grid td
        {
            padding: 5px;
            border: solid 1px #cccccc;
            height: 20px;
        }
        
        .Grid th
        {
            padding: 5px;
            background-color: #3b5998;
            font-size: 11px;
            height: 20px;
            color: #cccccc;
            font-weight: bold;
            text-align: left;
        }
        
        .Grid .GridFooter
        {
            padding: 5px;
            background-color: #3b5998;
            border: solid 1px #cccccc;
            font-size: 11px;
            height: 20px;
            color: #cccccc;
            font-weight: bold;
        }
        
        .Grid .GridFooter:hover
        {
            background-color: #3b5998;
            color: #cccccc;
        }
        
        .Grid tr:hover
        {
            background-color: #d8dfea;
            color: #3b5998;
        }
        .Grid .GrdRow
        {
            background-color: #f7f7f7;
        }
        .Grid .GrdAltRow
        {
            background-color: #ffffff;
        }
        .style4
        {
            text-decoration: underline;
        }
        .style5
        {
            width: 221px;
        }
        .style6
        {
            width: 744px;
        }
    </style>
</head>
<html xmlns="http://www.w3.org/1999/xhtml">
<body class="body">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <span class="style4">Upload Log</span> :
                </td>
            </tr>
        </table>
        &nbsp;</div>
    <asp:Panel ID="pnlTxtLog" runat="server">
        <table>
            <tr>
                <td class="style5" valign="top">
                    <asp:GridView ID="GrdLogList" runat="server" CssClass="Grid" AutoGenerateColumns="False"
                        OnRowDataBound="GrdLogList_RowDataBound" OnSelectedIndexChanged="GrdLogList_SelectedIndexChanged1">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True">
                                <ItemStyle Width="50px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="Name" HeaderText="Log Files" />
                            <asp:BoundField DataField="Link" HeaderText="Link" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td class="style6" valign="top">
                    <asp:Label ID="lblHead" runat="server" Font-Bold="False" Font-Underline="True"></asp:Label>
                    <br />
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Font-Size="8pt" Height="418px"
                        ReadOnly="True" TextMode="MultiLine" Width="733px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDbLogs" runat="server">
        <table style="width:80%">
            <tr>
                <td class="style5" valign="top">
                    <asp:GridView Width="100%" ID="GrdDBLogs" runat="server" CssClass="Grid" AutoGenerateColumns="False">
                        <Columns>
                         <asp:TemplateField HeaderText="Sr. No.">
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="description" HeaderStyle-Width="250px" HeaderText="Error Description" />
                            <asp:BoundField DataField="month" HeaderStyle-Width="100px" HeaderText="Months" />
                            <asp:BoundField DataField="payment_date" HeaderStyle-Width="100px" HeaderText="Payment Date" />
                            <asp:BoundField DataField="creditstatus_pb1" HeaderStyle-Width="100px" HeaderText="Credit Status" />
                            <asp:BoundField DataField="bankreasondescforfailure1" HeaderStyle-Width="100px" HeaderText="Bank Reason" />
                            <asp:BoundField DataField="purpose2" HeaderText="Purpose 1" />
                            <asp:BoundField DataField="accountnumberasperbank1" HeaderText="Account No" />
                            <asp:BoundField DataField="craadhaarnumber_pb" HeaderText="Aadhar No" />
                            <asp:BoundField DataField="totalamount2" HeaderText="Amount" />
                            <asp:BoundField DataField="schemespecificid2" HeaderStyle-Width="100px" HeaderText="SchemeID" />
                            <asp:BoundField DataField="ddo_code" HeaderStyle-Width="100px" HeaderText="DDO Code" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
