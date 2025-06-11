<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="tablegraph.aspx.cs" Inherits="AnganwadiWebApp.HomePage.tablegraph" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
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
    <style type="text/css">
        #chartContainer > div > a {
            display: none;
        }


        #UpdatePanel1 > div > div.row.rowtop > div:nth-child(2) > div {
            background: #f94e0f;
        }


            #UpdatePanel1 > div > div.row.rowtop > div:nth-child(2) > div:before {
                background: #fb7a4c;
            }



        #UpdatePanel1 > div > div.row.rowtop > div:nth-child(3) > div {
            background: #1383c5;
        }

            #UpdatePanel1 > div > div.row.rowtop > div:nth-child(3) > div:before {
                background: #1d96de;
            }

        #UpdatePanel1 > div > div.row.rowtop > div:nth-child(4) > div {
            background: #c54d14;
        }

            #UpdatePanel1 > div > div.row.rowtop > div:nth-child(4) > div:before {
                background: #de5412;
            }

        #UpdatePanel1 > div > div.row.rowtop > div:nth-child(5) > div {
            background: #c51386;
        }

            #UpdatePanel1 > div > div.row.rowtop > div:nth-child(5) > div:before {
                background: #e820a1;
            }

        .counter {
            color: #fff;
            background: #729C20;
            font-family: 'Poppins', sans-serif;
            text-align: center;
            width: 150px;
            height: 150px;
            padding: 38px 20px;
            margin: 0 auto;
            position: relative;
            z-index: 1;
            clip-path: polygon(30% 0%, 70% 0%, 100% 30%, 100% 70%, 70% 100%, 30% 100%, 0% 70%, 0% 30%);
        }

            .counter:before {
                content: "";
                background: #81af27;
                border-radius: 50%;
                box-shadow: 0px 0px 20px rgb(0 0 0 / 33%);
                position: absolute;
                top: 10px;
                left: 10px;
                right: 10px;
                bottom: 10px;
                z-index: -1;
            }

            .counter .counter-value {
                font-size: 40px;
                font-weight: 500;
                display: block;
                margin-top: -13px;
            }

            .counter h3 {
                font-size: 16px;
                font-weight: 600;
                letter-spacing: 0.3px;
                line-height: 20px;
                text-transform: capitalize;
                margin: 3px 0 0;
            }

            .counter.blue {
                background: #132C55;
            }

                .counter.blue:before {
                    background: #193C72;
                }

        .bottomhalf-first {
            width: 46%;
            float: left;
            margin: 10.5px;
        }

        .bottomhalf-second {
            width: 50%;
            float: left;
        }

        .bottomhalf-first h3 {
            font-size: 19px;
            text-align: center;
            background-color: #1383c5;
            color: white;
            padding: 4px;
            width: 45%;
            border-radius: 3px;
            margin-bottom: 8px;
        }


        .bottomhalf-second h3 {
            font-size: 19px;
            text-align: center;
            background-color: #1383c5;
            color: white;
            padding: 4px;
            width: 55%;
            border-radius: 3px;
            margin-bottom: 8px;
        }

        .rowtop {
            margin-top: 10px;
        }

        table thead {
            background-color: #1383c5;
            color: white;
        }

        table {
            font-size: 12px;
        }

            table tbody {
                background-color: #d3d3d336;
            }

        .table-bordered > tbody > tr > td {
            border: 1px solid #50beff61;
            text-align: right;
            font-weight: bold;
        }

        body > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(1) > td:nth-child(1), body > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(2) > td:nth-child(1), body > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(3) > td:nth-child(1), body > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(4) > td:nth-child(1), body > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(5) > td:nth-child(1), body > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(6) > td:nth-child(1), body > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(7) > td:nth-child(1) {
            text-align: left !important;
        }


        .table-bordered {
            border: none;
        }

        #UpdatePanel1 > div > div.bottomhalf > div.bottomhalf-first > table > tbody > tr:nth-child(7) {
            background-color: #1383c5;
            color: #fff;
        }


        .table > thead:first-child > tr:first-child > th {
            width: 25%;
            border-top-left-radius: 12px;
        }

        .table-bordered > thead > tr > th {
            border: none;
        }

        @media screen and (max-width:990px) {
            .counter {
                margin-bottom: 40px;
            }
        }

        @media (min-width: 992px) {
            .col-md-3 {
                width: 20%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row rowtop">
            <div class="col-md-2">
                <div class="counter">
                    <div>
                        <i class="fa fa-globe"></i>
                    </div>
                    <span class="counter-value"><b>
                        <asp:Label ID="lblInactive" runat="server" /></b> </span>
                    <h3>Inactive Sevika</h3>
                </div>
            </div>
            <div class="col-md-2">
                <div class="counter">
                    <div>
                        <i class="fa fa-rocket"></i>
                    </div>
                    <span class="counter-value"><b>
                        <asp:Label ID="lblDocUpload" runat="server" /></b></span>
                    <h3>Document Upload</h3>
                </div>
            </div>
            <div class="col-md-2 col-sm-6">
                <div class="counter">
                    <div>
                        <i class="fa fa-rocket"></i>
                    </div>
                    <span class="counter-value"><b>
                        <asp:Label ID="lblDocApproval" runat="server" /></b></span>
                    <h3>Document Approval</h3>
                </div>
            </div>
            <div class="col-md-2">
                <div class="counter">
                    <div>
                        <i class="fa fa-rocket"></i>
                    </div>
                    <span class="counter-value"><b>
                        <asp:Label ID="lblLicProcess" runat="server" /></b></span>
                    <h3>LIC Process</h3>
                </div>
            </div>
            <div class="col-md-2">
                <div class="counter">
                    <div>
                        <i class="fa fa-rocket"></i>
                    </div>
                    <span class="counter-value"><b>
                        <asp:Label ID="lblLicPayment" runat="server" /></b> </span>
                    <h3>Payment Done</h3>
                </div>
            </div>
            <div class="col-md-2">
                <div class="counter">
                    <div>
                        <i class="fa fa-rocket"></i>
                    </div>
                    <span class="counter-value"><b>
                        <asp:Label ID="lblHOApproval" runat="server" /></b></span>
                    <h3>HO Approval</h3>
                </div>
            </div>
        </div>
        <div class="bottomhalf" style="clear: both;">
            <div class="bottomhalf-first">
                <h3 style="text-align: center;">Month Wise LIC</h3>
                <div>
                    <asp:GridView ID="GrdDashboard" runat="server" CssClass="Grid" Width="90%" AutoGenerateColumns="False"
                        AllowPaging="true" OnRowDataBound="GrdDashboard_RowDataBound" ShowFooter="true" Font-Size="Small"
                        FooterStyle-BackColor="#428BCA" FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Center">
                        <RowStyle CssClass="GrdRow"></RowStyle>
                        <Columns>
                            <asp:BoundField DataField="divid" HeaderText="DivisionID"></asp:BoundField>
                            <asp:TemplateField HeaderText="Division" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDivName" OnClick="lnkDivName_Click" runat="server" Text='<%# Eval("divname") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnDivId" runat="server" Value='<%# Eval("divid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="inactive_count" HeaderText="Inactive Sevika" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField DataField="docupload_count" HeaderText="Document Upload" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField DataField="DocApprove_count" HeaderText="Approved" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Processed" HeaderText="Processed"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Paid" HeaderText="Paid"></asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                    </asp:GridView>
                </div>
            </div>
            <div class="bottomhalf-second" style="margin-top: 14px;">
                <%--<h3 style="text-align: center;">
                    LIC Document Status</h3>
                <div id="chartContainer" style="height: 273px; width: 100%; border: 2px solid #4cb2ef;">
                </div>
                <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>--%>

                <br />
                <br />
                <br />

                <div>

                    <asp:GridView ID="GrdDivision" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="true" OnRowDataBound="GrdDivision_RowDataBound" ShowFooter="true" Font-Size="Small"
                        FooterStyle-BackColor="#428BCA" FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Center">
                        <RowStyle CssClass="GrdRow"></RowStyle>
                        <Columns>
                            <asp:BoundField DataField="distid" HeaderText="DistrictID"></asp:BoundField>
                            <asp:TemplateField HeaderText="District" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDistName" OnClick="lnkDistName_Click" runat="server" Text='<%# Eval("distname") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnDistId" runat="server" Value='<%# Eval("distid") %>' />
                                    <asp:HiddenField ID="HdnDivId" runat="server" Value='<%# Eval("divid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="inactive_count" HeaderText="Inactive Sevika" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="18%"></asp:BoundField>
                            <asp:BoundField DataField="docupload_count" HeaderText="Document Upload" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="21%"></asp:BoundField>
                            <asp:BoundField DataField="DocApprove_count" HeaderText="Approved" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Processed" HeaderText="Processed"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Paid" HeaderText="Paid"></asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                    </asp:GridView>
                    <asp:GridView ID="GrdDistrict" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="true" OnRowDataBound="GrdDistrict_RowDataBound" ShowFooter="true" Font-Size="Small"
                        FooterStyle-BackColor="#428BCA" FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Center">
                        <RowStyle CssClass="GrdRow"></RowStyle>
                        <Columns>
                            <asp:BoundField DataField="cdpoid" HeaderText="cdpoid"></asp:BoundField>
                            <asp:TemplateField HeaderText="CDPO" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCDPOName" OnClick="lnkCDPOName_Click" runat="server" Text='<%# Eval("cdponame") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnCDPOId" runat="server" Value='<%# Eval("cdpoid") %>' />
                                    <asp:HiddenField ID="HdnDivId" runat="server" Value='<%# Eval("divid") %>' />
                                    <asp:HiddenField ID="HdnDistId" runat="server" Value='<%# Eval("distid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="inactive_count" HeaderText="Inactive Sevika" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="18%"></asp:BoundField>
                            <asp:BoundField DataField="docupload_count" HeaderText="Document Upload" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="21%"></asp:BoundField>
                            <asp:BoundField DataField="DocApprove_count" HeaderText="Approved" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Processed" HeaderText="Processed"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Paid" HeaderText="Paid"></asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                    </asp:GridView>
                    <asp:GridView ID="GrdCDPO" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="true" OnRowDataBound="GrdCDPO_RowDataBound" ShowFooter="true" Font-Size="Small"
                        FooterStyle-BackColor="#428BCA" FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Center">
                        <RowStyle CssClass="GrdRow"></RowStyle>
                        <Columns>
                            <asp:BoundField DataField="cdpoid" HeaderText="cdpoid"></asp:BoundField>
                            <asp:TemplateField HeaderText="BIT" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBitName" OnClick="lnkBitName_Click" runat="server" Text='<%# Eval("bitbitname") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnBitId" runat="server" Value='<%# Eval("bitid") %>' />
                                    <asp:HiddenField ID="HdnDivId" runat="server" Value='<%# Eval("divid") %>' />
                                    <asp:HiddenField ID="HdnDistId" runat="server" Value='<%# Eval("distid") %>' />
                                    <asp:HiddenField ID="HdnCdpo" runat="server" Value='<%# Eval("cdpoid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="inactive_count" HeaderText="Inactive Sevika" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="18%"></asp:BoundField>
                            <asp:BoundField DataField="docupload_count" HeaderText="Document Upload" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="21%"></asp:BoundField>
                            <asp:BoundField DataField="DocApprove_count" HeaderText="Approved" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Processed" HeaderText="Processed"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Paid" HeaderText="Paid"></asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                    </asp:GridView>
                    <asp:GridView ID="GrdBit" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="true" OnRowDataBound="GrdBit_RowDataBound" ShowFooter="true" Font-Size="Small"
                        FooterStyle-BackColor="#428BCA" FooterStyle-ForeColor="White" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Center">
                        <RowStyle CssClass="GrdRow"></RowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Anganwadi" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAngName" OnClick="lnkAngName_Click" runat="server" Text='<%# Eval("angnname") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnAngName" runat="server" Value='<%# Eval("angnname") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="inactive_count" HeaderText="Inactive Sevika" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="18%"></asp:BoundField>
                            <asp:BoundField DataField="docupload_count" HeaderText="Document Upload" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="21%"></asp:BoundField>
                            <asp:BoundField DataField="DocApprove_count" HeaderText="Approved" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Processed" HeaderText="Processed"></asp:BoundField>
                            <asp:BoundField DataField="LIC_Paid" HeaderText="Paid"></asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <!--<div id="chartContainer" style="height: 300px; width: 100%; clear: both;"></div>-->
    </div>
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
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
    <script>
        window.onload = function () {

            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                title: {
                    text: "",
                    fontFamily: "arial black",
                    fontColor: "#695A42"
                },
                axisX: {
                    interval: 1,
                    intervalType: "year"
                },
                axisY: {
                    valueFormatString: "100.00",
                    gridColor: "#B6B1A8",
                    tickColor: "#B6B1A8"
                },
                toolTip: {
                    shared: false,
                    content: toolTipContent
                },
                data: [{
                    type: "stackedColumn",
                    showInLegend: true,
                    color: "#1383c5",
                    name: "Inactive Sevika",
                    dataPoints: [
        { y: 6.75, x: new Date(2010, 0) },
        { y: 8.57, x: new Date(2011, 0) },
        { y: 10.64, x: new Date(2012, 0) },
        { y: 13.97, x: new Date(2013, 0) },
        { y: 15.42, x: new Date(2014, 0) },
        { y: 17.26, x: new Date(2015, 0) },
        { y: 20.26, x: new Date(2016, 0) }
                    ]
                },
      {
          type: "stackedColumn",
          showInLegend: true,
          name: "Document Upload",
          color: "#8ebf2f",
          dataPoints: [
          { y: 6.82, x: new Date(2010, 0) },
          { y: 9.02, x: new Date(2011, 0) },
          { y: 11.80, x: new Date(2012, 0) },
          { y: 14.11, x: new Date(2013, 0) },
          { y: 15.96, x: new Date(2014, 0) },
          { y: 17.73, x: new Date(2015, 0) },
          { y: 21.5, x: new Date(2016, 0) }
          ]
      },
      {
          type: "stackedColumn",
          showInLegend: true,
          name: "Pending",
          color: "#b5b5b5",
          dataPoints: [
          { y: 7.28, x: new Date(2010, 0) },
          { y: 9.72, x: new Date(2011, 0) },
          { y: 13.30, x: new Date(2012, 0) },
          { y: 14.9, x: new Date(2013, 0) },
          { y: 18.10, x: new Date(2014, 0) },
          { y: 18.68, x: new Date(2015, 0) },
          { y: 22.45, x: new Date(2016, 0) }
          ]
      }]
            });
            chart.render();

            function toolTipContent(e) {
                var str = "";
                var total = 0;
                var str2, str3;
                for (var i = 0; i < e.entries.length; i++) {
                    var str1 = "<span style= \"color:" + e.entries[i].dataSeries.color + "\"> " + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong>bn<br/>";
                    total = e.entries[i].dataPoint.y + total;
                    str = str.concat(str1);
                }
                str2 = "<span style = \"color:DodgerBlue;\"><strong>" + (e.entries[0].dataPoint.x).getFullYear() + "</strong></span><br/>";
                total = Math.round(total * 100) / 100;
                str3 = "<span style = \"color:Tomato\">Total:</span><strong> " + total + "</strong>bn<br/>";
                return (str2.concat(str)).concat(str3);
            }

        }
    </script>
</asp:Content>

