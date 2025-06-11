<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectManagement.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>|| Anganwadi Web App ||</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <link rel="shortcut icon" href="../Images/favicon-16x16.png" />
    <link href="~/CSS/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .login-box
        {
            width: 400px;
            margin: 9% auto;
        }
        
        .login-logo, .register-logo
        {
            font-size: 20px;
            text-align: center;
            margin-bottom: 40px;
            font-weight: 300;
            color: #1F618D;
        }
        
        .login-box-body, .register-box-body
        {
            background: #FDEDEC;
            padding: 20px;
            border-top: 0;
            color: #666;
        }
        
        .LoginButton
        {
            background-color: #1F618D;
            color: #f7f7f7;
            border: 1px solid #4db1a7;
            border-radius: 2px;
            height: 30px;
            width: 90px;
            font-weight: bolder;
        }
        
        .bodyNew
        {
            background: url(Images/login_screen.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }
    </style>
    <script>
        function button_click(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
                document.getElementById(objBtnID).click();
            }
        }
    </script>
</head>
<body class="bodyNew">
    <%--background-color: #545558;--%>
    <form id="form1" runat="server">
    <div class="login-box">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />
        <div class="login-logo">
        </div>
        <div class="login-box-body">
            <center>
                <p style="margin-bottom: 10px">
                    <b>Anganwadi Web Application</b></p>
                <br />
                <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox" placeholder="User Id"
                    autocomplete="off" Style="margin-bottom: 10px"></asp:TextBox>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="TextBox" placeholder="Password"
                    Style="margin-bottom: 17px" TextMode="Password"></asp:TextBox>
                <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                    FilterMode="ValidChars" TargetControlID="txtPassword" ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@ "
                </ajaxToolkit:FilteredTextBoxExtender>--%>
                
                <%-- // Comment for ICDS_TEST
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Image ID="imgPayConfCaptcha" runat="server" Height="35px" Width="130px" />
                        <asp:ImageButton ID="btnPayConfRefresh" runat="server" ToolTip="Refresh" OnClick="btnPayConfRefresh_Click"
                            ImageUrl="~/Images/btnRefresh.png" Width="30px" Height="30px" />
                        <asp:LinkButton ID="BtnRefresh" runat="server" OnClick="BtnRefresh_Click" Visible="false">Refresh</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <asp:TextBox ID="txtPayConfCaptcha" runat="server" CssClass="TextBox" placeholder="Enter Captcha"
                    autocomplete="off" Style="margin-bottom: 10px" onkeydown="return (event.keyCode!=13);"></asp:TextBox>--%>
                <br />
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="LoginButton" OnClick="btnLogin_Click" />
                <br />
                <br />
                <asp:LinkButton ID="lnkCheckPaymentDetail" runat="server" Text="Check Payment Details"
                    OnClick="lnkCheckPaymentDetail_Click"></asp:LinkButton>
                <br />
                <br />
                <asp:Label ID="lblVersion" runat="server" Text="Version 1.2"></asp:Label>
            </center>
        </div>
    </div>
    </form>
</body>
</html>
