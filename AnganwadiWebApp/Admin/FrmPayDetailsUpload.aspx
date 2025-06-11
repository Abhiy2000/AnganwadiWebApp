<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmPayDetailsUpload.aspx.cs" Inherits="AnganwadiWebApp.Admin.FrmPayDetailsUpload" %>

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
    <script type="text/javascript" language="javascript">
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
        }
    </script>
    <style type="text/css">
        .transparent
        {
            /* IE 8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /* IE 5-7 */
            filter: alpha(opacity=50); /* Netscape */
            -moz-opacity: 0.5; /* Safari 1.x */
            -khtml-opacity: 0.5; /* Good browsers */
            opacity: 0.5;
        }
        .loader
        {
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
        .loader img
        {
            padding: 10px;
            position: fixed;
            top: 45%;
            left: 50%;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 468px; overflow: auto">
        <div class="active tab-pane">
            <div class="form-horizontal">
                <div class="form-group">
                    <br />
                    <div>
                        <label class="col-sm-2 control-label">
                            Instruction <span style="color: red; font-size: 13px;">*</span>:
                        </label>
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBox" TextMode="MultiLine"
                            ReadOnly="true" Text="Steps to Upload File
                            1. Download excel template from above given link.
                            2. Fill data in an excel file in a proper way.
                            3. After data filled in excel need to remove header in excel file.
                            4. That excel file save it as Text (Tab Delimited) (.txt*).
                            5. Upload Tab delimited file. " Height="130px" Width="550px"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div>
                        <label class="col-sm-2 control-label">
                            File Upload <span style="color: red; font-size: 13px;">*</span>:
                        </label>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-7">
                            <asp:FileUpload ID="FileUpload" runat="server" Style="padding-top: 5px;" Height="25px"
                                Width="300px" />
                            &nbsp;&nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload"
                                ErrorMessage="Invalid File" ValidationExpression="(.*\.([Tt][Xx][Tt])$)" ValidationGroup="A">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click">Download Template</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmit" />
                            <asp:PostBackTrigger ControlID="lnkDownload" />
                        </Triggers>
                        <ContentTemplate>
                              <asp:Button ID="btnSubmit" CssClass="Button" runat="server" Text="Upload" ValidationGroup="A"
                            OnClick="btnSubmit_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                    </div>
                    <div class="col-md-5">
                        <div class="col-md-3" style="font-weight: bold; margin-top: 4px;">
                            <asp:LinkButton ID="lnklog" runat="server" OnClick="LinkLog_Click">File Log</asp:LinkButton>
                        </div>
                        <div class="col-md-9" style="font-weight: bold; margin-top: 4px;">
                            <asp:LinkButton ID="lnkDBLogs" runat="server" OnClick="LinkDBLog_Click">Data Log</asp:LinkButton></div>
                    </div>
                    <%--   <div class="col-md-5">
                   
                </div>--%>
                    <div class="col-md-2" style="padding-top: 5px;">
                        
                    </div>
                </div>
                <div class="form-group" id="divLog" runat="server">
                    <br />
                    <div>
                        <label class="col-sm-2 control-label">
                            Logs <span style="color: red; font-size: 13px;">*</span>:
                        </label>
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtLog" runat="server" CssClass="TextBox" TextMode="MultiLine" ReadOnly="true"
                            Text=" " Height="130px" Width="550px"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <%-- Loader --%>
            <div id="imgrefresh" class="loader transparent">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loader.GIF" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server"
        TargetControlID="UpdatePanel2">
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
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
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
</asp:Content>
