<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmONLAppForm.aspx.cs"
    Inherits="AnganwadiWebApp.Transaction.FrmONLAppForm" %>

<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <%-- <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>--%>
    <!-- Optional: Bootstrap Datepicker JS -->
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>--%>

    <link href="../CSS/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datepicker.min.js"></script>



    <%--<script>
        $(document).ready(function () {
            $('#datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on('changeDate', function (e) {
                var selectedDate = $('#datepicker').val();

                CalculateAge(selectedDate);
            });

            function CalculateAge(dateString) {
                if (!dateString) return;
                var parts = dateString.split('/');
                if (parts.length !== 3) return;

                var day = parseInt(parts[0], 10);
                var month = parseInt(parts[1], 10) - 1;
                var year = parseInt(parts[2], 10);

                var birthDate = new Date(year, month, day);
                var today = new Date();
                if (birthDate > today) {
                    document.getElementById('<%= txtage.ClientID %>').value = 0;
                    return;
                }

                var age = today.getFullYear() - birthDate.getFullYear();
                var m = today.getMonth() - birthDate.getMonth();

                if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                    age--;
                }
                document.getElementById('<%= txtage.ClientID %>').value = age;
            }
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            var invDueDate = '<%= hdnduedate.Value %>';
            $("[id$=hdndtwithdr]").val(invDueDate);
            $('#datepicker').val(invDueDate);
            $('#datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).datepicker('setDate', invDueDate).on('changeDate', function (e) {
                var selectedDate = $('#datepicker').val();
                //alert('Selected Date: ' + selectedDate);
                //CalculateAge(selectedDate);
            });

        });
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Label ID="LblGrdHead" runat="server"></asp:Label>
                        </h5>

                    </div>
                    <div class="ibox-content">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="nav-tabs-custom">
                                    <div class="tab-content">
                                        <form>
                                            <div class="form-horizontal">
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Division :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        District :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Project :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlproj" runat="server" CssClass="form-control" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlproj_SelectedIndexChanged" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Name of Applicant :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtNameApp" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Anganwadi :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlAnganwadi" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Address of Applicant :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Porition :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlporition" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Birth Date :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <uc1:Date ID="birthdt" runat="server" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Physically Handicapped :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:RadioButtonList ID="rbdhandicapp" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="N" style="margin-left: 15px;"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Age :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txthandicapAge" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Disability %:
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDisability" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Age :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtage" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Marital Status :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlMarStatus" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Educational Qualification:
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlEduQuali" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Aadhar No :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAadhar" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Pan No :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Religion :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtReligion" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Cast :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtcast" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Subcast :
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtSubcast" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 control-label">
                                                        Document Attach :
                                                    </label>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-12 col-xs-12">
                                                        <center>
                                                            <asp:GridView ID="grdDoc" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                                                                Width="80%" AutoGenerateColumns="false" OnRowCommand="grdDoc_RowCommand"
                                                                AllowPaging="True" PageSize="10" OnRowDataBound="grdDoc_RowDataBound"
                                                                OnSelectedIndexChanged="grdDoc_SelectedIndexChanged">
                                                                <RowStyle CssClass="GrdRow"></RowStyle>
                                                                <Columns>
                                                                    <asp:BoundField DataField="Docid" HeaderText="DocumentID" />
                                                                    <asp:TemplateField HeaderText="Document Name">
                                                                        <ItemTemplate>
                                                                            s
                                                                            <asp:Label ID="lblDocNames" Text='<%#Eval("Docname")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload">
                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="FlDoc" runat="server"></asp:FileUpload>
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
                                                        </center>
                                                    </div>
                                                </div>
                                                <div class="col-lg-offset-5">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnSave" />
                                                            <asp:PostBackTrigger ControlID="Btnsubmit" />
                                                            <asp:PostBackTrigger ControlID="btnClose" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save"
                                                                ValidationGroup="A" Style="margin-top: 15px;" OnClick="btnSave_Click" />
                                                            <asp:Button ID="Btnsubmit" CssClass="btn btn-primary" runat="server" Text="Submit"
                                                                ValidationGroup="A" Style="margin-top: 15px;" OnClick="Btnsubmit_Click" />
                                                            <asp:Button ID="BtnClose" runat="server" Style="margin-top: 15px;" Text="Close" CssClass="btn btn-white"
                                                                OnClick="BtnClose_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
