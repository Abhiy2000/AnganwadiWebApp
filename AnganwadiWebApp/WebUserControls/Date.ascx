<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Date.ascx.cs" Inherits="ProjectManagement.WebUserControls.Date" %>

<link href="../CSS/main.css" rel="stylesheet" type="text/css" />
<div style="width: 130px">
    <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="100px" Height="28px"
        MaxLength="10"></asp:TextBox>&nbsp;
    <asp:ImageButton ID="BtnDate" runat="server" ImageUrl="~/Images/Cel.png" Width="18px"
        Height="18px" />
    <ajaxtoolkit:CalendarExtender id="DateCalendarExtender" cssclass="MyCalendar" targetcontrolid="txtDate"
        runat="server" popupbuttonid="BtnDate" format="dd/MM/yyyy"></ajaxtoolkit:CalendarExtender>
</div>
