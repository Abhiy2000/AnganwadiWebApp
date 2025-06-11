<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmSevikaEditScrn.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmSevikaEditScrn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style98 {
            width: 90px;
        }

        .style99 {
            width: 200px;
        }
    </style>
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
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
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
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 468px; overflow: auto;">
        <asp:UpdatePanel runat="server" ID="PnlCntls">
            <ContentTemplate></ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnSearchSevika" />
                  <asp:PostBackTrigger ControlID="BtnSubmit" />
                <asp:PostBackTrigger ControlID="BtnRest" />
                

            </Triggers>
        </asp:UpdatePanel>
        <table align="left" cellspacing="5px">
            <tr>
                <td style="width: 112px;">Sevika Id<span style="color: Red">* </span>
                </td>
                <td style="width: 10px">:</td>
                <td>
                    <asp:TextBox CssClass="TextBox" runat="server" ID="txtSevikaId" Width="205px" Height="35px"></asp:TextBox>

                </td>
                <td style="width: 20px"></td>
                <td>
                    <asp:Button ID="BtnSearchSevika" Text="Search" OnClick="BtnSearchSevika_Click" runat="server"
                        CssClass="Button" Width="150px" />
                    <asp:Button ID="BtnRest" Text="Reset" runat="server" OnClick="BtnRest_Click"
                        CssClass="Button" Width="80px" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <hr />
        <div runat="server" id="DivRecords" visible="true">
            <table align="left" width="100%" cellspacing="5px">
                <tr>
                    <td colspan="6" style="font-size: 16px"><u>Existing Details </u></td>
                </tr>
                <tr></tr>
                <tr>
                    <td style="width: 112px;">Sevika Name
                    </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblSevikaName"></asp:Label>
                    </td>
                    <td style="width: 112px;"> AadharNo. </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblSevikaAadhar"></asp:Label>
                    </td>
                    <td style="width: 100px;">Date Of Joining</td>
                    <td style="width: 10px">:</td>
                    <td>
                        <uc1:Date ID="DtDateJoinOld" runat="server"  />
                  
                    </td>
                </tr>

                <tr>
                    <td style="width: 112px;">Birth Date
                    </td>
                    <td style="width: 10px">:</td>
                    <td>
        
                             <uc1:Date ID="DtDobOld" runat="server" />
                    </td>
                    <td style="width: 112px;">Address</td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblAddress"></asp:Label>
                    </td>

                </tr>


                <tr>
                    <td style="width: 112px;">Division
                    </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblDivName"></asp:Label>
                    </td>
                    <td style="width: 112px;">Dist. Name </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblDistName"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="width: 112px;">CDPO
                    </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblCdpo"></asp:Label>
                    </td>
                    <td style="width: 112px;">Status</td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:Label runat="server" ID="lblStatus"></asp:Label>
                    </td>

                </tr>
            </table>


            <table align="left" width="100%" cellspacing="5px" style="margin-top: 3%">
                <tr>
                    <td colspan="6" style="font-size: 16px"><u>Update Details </u></td>
                </tr>
                <tr></tr>
                <tr>
                    <td style="width: 112px;">Birth Date <asp:CheckBox runat="server" ID="ChkDobNew" />
                    </td>
                    <td style="width: 10px">:</td>
                    <td style="width: 20%">
                        <uc1:Date ID="DtDobNew" runat="server" />
                    </td>
                    <td style="width: 112px;">
                        <asp:FileUpload runat="server" ID="flUpdDOB" accept=".jpeg,.jpg,.png,.pdf"></asp:FileUpload>
                    </td>
                    <td style="width: 10px"></td>
                    <td></td>


                </tr>
                <tr>
                    <td style="width: 112px;">Date of Joining <asp:CheckBox runat="server" ID="chkDtJoinNew" />
                    </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <uc1:Date ID="DtDateOfJoinNew" runat="server" />

                    </td>
                    <td style="width: 112px;">
                        <asp:FileUpload runat="server" ID="flUpdDateJoin" accept=".jpeg,.jpg,.png,.pdf"></asp:FileUpload>
                    </td>
                    <td style="width: 10px"></td>
                    <td></td>


                </tr>
                <tr>
                    <td style="width: 112px;">Aadhar No <asp:CheckBox runat="server" ID="chkAadharNew" />
                    </td>
                    <td style="width: 10px">:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtAadharNoNew" MaxLength="12"></asp:TextBox>
                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            FilterMode="ValidChars" TargetControlID="txtAadharNoNew" ValidChars="0123456789">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td style="width: 112px;">
                        <asp:FileUpload runat="server" ID="flUpdAadhar" accept=".jpeg,.jpg,.png,.pdf"></asp:FileUpload>
                    </td>
                    <td style="width: 10px"></td>
                    <td></td>


                </tr>

            </table>
             <table align="left" width="100%" cellspacing="5px" style="margin-top: 3%">
            <tbody>
                <tr>
                    <td  align="center">
                            <asp:Button ID="BtnSubmit" Text="Submit" OnClick="BtnSubmit_Click" runat="server"
                        CssClass="Button" Width="150px" />
                         <asp:Button ID="BtnBack" Text="Back" OnClick="BtnBack_Click" runat="server"
                        CssClass="Button" Width="150px" />
                    </td>
                  
                </tr>
            </tbody>

        </table>
        </div>
       

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
