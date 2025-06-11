<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmCompanyListAdmin.aspx.cs" Inherits="ProjectManagement.MstListPages.FrmCompanyListAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript">
      function closeMsgPopupnew() {
          $find("mpeMsgnew").hide();
          debugger;
          var el = document.getElementById('ContentPlaceHolder1_lblredirect');
          var aa = el.innerHTML;
          if (aa != "") {
              window.location.href = aa;
          }
          return false;
      }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--
<h1 class="PageTitle">Company List</h1>
    <br />
    <br />
    <br />--%>
    <div class="boxHead">
       <b>
            <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label></b>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 468px; overflow: auto">
   
        <asp:GridView ID="GrdCompanyList" runat="server" Width="60%" CssClass="Grid" ShowFooter="True"
            AutoGenerateColumns="False" OnRowDataBound="GrdCompanyList_RowDataBound" OnSelectedIndexChanged="GrdCompanyList_SelectedIndexChanged">
            <RowStyle CssClass="GrdRow" />
            <AlternatingRowStyle CssClass="GrdAltRow" />
            <Columns>
                <asp:CommandField ShowSelectButton="true" SelectText="Select" HeaderText="Select">
                    <HeaderStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField HeaderText="CompanyId" DataField="CompanyId">
                    <HeaderStyle Width="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                    <HeaderStyle Width="330px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle CssClass="GridFooter" />
        </asp:GridView>
    </div>

  
</asp:Content>
