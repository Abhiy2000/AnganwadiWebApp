<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmSessionLogOut.aspx.cs" Inherits="ProjectManagement.FrmSessionLogOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  
 <title>Session TimeOut</title>
    <link href="CSS/main.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 33px;
        }
        .style3
        {
            width: 466px;
        }
        .style4
        {
            width: 23px;
        }
        .style5
        {
            width: 23px;
            height: 31px;
        }
        .style6
        {
            width: 466px;
            height: 31px;
        }
        .style7
        {
            width: 23px;
            height: 20px;
        }
        .style8
        {
            width: 466px;
            height: 20px;
        }
        .style9
        {
            width: 151px;
        }
        .style12
        {
            height: 94px;
        }
        .style13
        {
            width: 23px;
            height: 24px;
        }
        .style14
        {
            width: 466px;
            height: 24px;
        }
    </style>
    
    <script type="text/javascript">
        function BlockBackSpace() {
            if (event.keyCode == 8) {
                return false;
            }
        }
    </script>

</head>
<body class="body" onkeydown="return BlockBackSpace()">
    
    <form id="form1" runat="server">
    <div style="width: 100%">
    
        <table class="style1">
        <tr>
        <td class="style12"></td>
        <td class="style12"></td>
        <td class="style12"></td>
        </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center">
    
        <asp:Panel ID="PnlSessionExpire" runat="server" Height="201px" Width="660px">
            <table class="style1" style="border: 1px solid #CCCCCC; height: 200px;">
                <tr style="border: 1px solid #CCCCCC; background-color: #E5E5E5">
                    <td class="style2" colspan="3" style="background-color: #F8F8F8" align="left">
                        &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" 
                            Text="Your session has expired !!!" ForeColor="#3B5998"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td align="left" class="style3">
                        Your session has been ended.<br />
                        <br />
                        Session automatically time out after long time of inactivity as a security precaution.</td>
                        
                    <td rowspan="2">
                        <img alt="" src="Images/SessionTimeout.jpg" 
                            style="width: 147px; height: 145px" /></td>
                </tr>
                <tr>
                    <td class="style5">
                        </td>
                    <td align="left" class="style6">
                        If you would like to return to the application, please
                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" ForeColor="DarkBlue">Login</asp:LinkButton>
                        &nbsp;again.</td>
                </tr>
            </table>
        </asp:Panel>
    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center">
                    <asp:Panel ID="PnlLogout" runat="server" Height="173px" Width="660px">
                        <table class="style1" style="border: 1px solid #CCCCCC; height: 160px;">
                            <tr style="border: 1px solid #CCCCCC; background-color: #E5E5E5">
                                <td class="style2" colspan="3" style="background-color: #F8F8F8" align="left">
                                    &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" 
                            Text="Logout Successfully !!!" ForeColor="#3B5998"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style13">
                                </td>
                                <td class="style14" align="left">
                                    Thank you for using Anganwadi Dashboard.
                                    <br />
                                    <br />
                                    You have been successfully logged out of Anganwadi Dashboard.<br />
                                    <br />
                                    <br />
                                    If you would like to return to the application, please
                                    <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" ForeColor="DarkBlue">Login</asp:LinkButton>
                                    &nbsp;again.</td>
                                <td rowspan="2" class="style9">
                                    <img alt="" src="Images/loginout.jpg" style="width: 134px; height: 124px" /></td>
                            </tr>
                            <tr>
                                <td class="style7">
                                </td>
                                <td class="style8" align="left">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    
    </div>
    </form>
</body>
</html>
