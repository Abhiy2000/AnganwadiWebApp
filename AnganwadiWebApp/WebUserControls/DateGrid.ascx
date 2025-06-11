<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Date.ascx.cs" Inherits="ProjectManagement.WebUserControls.DateGrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="../CSS/main.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    /* Ensure styles are applied to the controls inside the GridView */
.GridViewContainer .MyCalendar {
    left: -9px !important;  
    z-index: 1000; 
    display: block;
}

.GridViewContainer .ajax__calendar_body {
    height: 140px;
    margin: auto;
    overflow: hidden;
    position: relative;
    /*width: 150px;*/
}

 .MyCalendar  {
    border: 1px solid #376091; 
    background-color: #f9f9f9; 
    color: #4db1a7; 
    margin-top: 0px;
    border-radius: 5px;
    position: relative;
    overflow: hidden;
    width: 23% !important;
}

.GridViewContainer .TextBox {
    width: 100px; 
    height: 28px; 
    padding: 5px;
    font-size: 14px;
    border: 1px solid #ccc; 
    border-radius: 5px; 
    margin-right: 10px; 
}

.GridViewContainer #BtnDate {
    width: 18px;
    height: 18px;
    cursor: pointer; 
}

@media screen and (max-width: 600px) {
    .GridViewContainer .TextBox {
        width: 80%; 
    }
    .GridViewContainer .ajax__calendar_body {
        width: 140px; 
    }
    .GridViewContainer #BtnDate {
        width: 16px;
        height: 16px;
    }
}

/*.MyCalendar {
    left: -9px !important;  
    z-index: 1000; 
    display: block;
}

.ajax__calendar_body {
    height: 120px;
    margin: auto;
    overflow: hidden;
    position: relative;
    width: 150px;
}

.MyCalendar .ajax__calendar_container {
    border: 1px solid #376091; 
    background-color: #f9f9f9; 
    color: #4db1a7; 
    margin-top: 0px;
    border-radius: 5px;
    position: relative;
    overflow: hidden;
}

.TextBox {
    width: 100px; 
    height: 28px; 
    padding: 5px;
    font-size: 14px;
    border: 1px solid #ccc; 
    border-radius: 5px; 
    margin-right: 10px; 
}

#BtnDate {
    width: 18px;
    height: 18px;
    cursor: pointer; 
}

@media screen and (max-width: 600px) {
    .TextBox {
        width: 80%; 
    }
    .ajax__calendar_body {
        width: 140px; 
    }
    #BtnDate {
        width: 16px;
        height: 16px;
    }*/
}


</style>
<div style="width: 130px">
    <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="100px" Height="28px"
        MaxLength="10"></asp:TextBox>&nbsp;
    <asp:ImageButton ID="BtnDate" runat="server" ImageUrl="~/Images/Cel.png" Width="18px"
        Height="18px" />
    <ajaxToolkit:CalendarExtender ID="DateCalendarExtender" CssClass="MyCalendar" TargetControlID="txtDate" 
        runat="server" PopupButtonID="BtnDate" Format="dd/MM/yyyy">
    </ajaxToolkit:CalendarExtender>
</div>
