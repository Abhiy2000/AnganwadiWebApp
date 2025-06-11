using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using AnganwadiLib.Methods;
using System.Net.Mail;
using System.Net;

namespace AnganwadiWebApp
{
    public partial class FrmAutoMail : System.Web.UI.Page
    {
        String FromEmailId = ConfigurationSettings.AppSettings["FromEmailId"].ToString().Trim();
        String FromPassword = ConfigurationSettings.AppSettings["FromPassword"].ToString().Trim();
        String smtpClientHost = ConfigurationSettings.AppSettings["smtpClientHost"].ToString().Trim();
        String DisplayName = ConfigurationSettings.AppSettings["DisplayName"].ToString().Trim();
        String Subject = ConfigurationSettings.AppSettings["Subject"].ToString().Trim();

        protected void Page_Load(object sender, EventArgs e)
        {
            String Query = "select * from view_dailysummary";

            DataTable tblDailySmmry = (DataTable)MstMethods.Query2DataTable.GetResult(Query);

            if (tblDailySmmry.Rows.Count > 0)
            {
                try
                {
                    String MessageTo = "";
                    Query = "";
                    Query = "select trim(lower(var_comp_email)) email from aoup_email_mobile_def where var_comp_email is not null";
                    DataTable tblemail = (DataTable)MstMethods.Query2DataTable.GetResult(Query);
                    if (tblemail.Rows.Count > 0)
                    {
                        for (int i = 0; i < tblemail.Rows.Count; i++)
                        {
                            MessageTo += tblemail.Rows[i]["email"].ToString() + ",";
                        }
                        MessageTo = MessageTo.Substring(0, MessageTo.Length - 1);

                        SmtpClient smtpClient = new SmtpClient();
                        MailMessage message = new MailMessage();

                        smtpClient.Credentials = new NetworkCredential(FromEmailId, FromPassword);
                        smtpClient.Host = smtpClientHost;
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;

                        message.From = new MailAddress(FromEmailId, DisplayName, System.Text.Encoding.UTF8);
                        message.IsBodyHtml = true;
                        message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                        message.To.Add(MessageTo);

                        message.Subject = "[" + Subject + " " + DateTime.Today.Date.ToString("dd-MMM-yyyy") + "]";

                        message.Body += "<html>";
                        message.Body += "<head><style>";

                        message.Body += " .style1";
                        message.Body += " {";
                        message.Body += " background-color: Yellow;";
                        message.Body += " font-weight: bold;";
                        message.Body += " text-align: center;";
                        message.Body += " }";
                        message.Body += " .style2";
                        message.Body += " {";
                        message.Body += " text-align: center;";
                        message.Body += " }";
                        message.Body += " table.blueTable";
                        message.Body += " {";
                        message.Body += " font-size: 16px;";
                        message.Body += " border: 1px solid #000000;";
                        message.Body += " width: 50%;";
                        message.Body += " text-align: left;";
                        message.Body += " border-collapse: collapse;";
                        message.Body += " }";
                        message.Body += " table.blueTable td, table.blueTable th";
                        message.Body += " {";
                        message.Body += " border: 1px solid #000000;";
                        message.Body += " padding: 3px 2px;";
                        message.Body += " }";
                        message.Body += " ";
                        message.Body += " </style>";

                        message.Body += "</head>";
                        message.Body += "<body style='font-family:Verdana;font-size:12px'>";
                        message.Body += "Sir/Madam,";
                        message.Body += "<br>";
                        message.Body += "<br>";
                        message.Body += "Following is the Sammanpranali Summary for the day " + DateTime.Today.Date.ToString("dd-MMM-yyyy") + ".";
                        message.Body += "<br>";
                        message.Body += "<br>";

                        message.Body += "<table class='blueTable'>";
                        message.Body += "<tr style='background-color:Yellow'>";
                        message.Body += "<th>";
                        message.Body += " Status ";
                        message.Body += "</th>";
                        message.Body += "<th>";
                        message.Body += " Count ";
                        message.Body += "</th>";
                        message.Body += "</tr>";

                        for (int i = 0; i < tblDailySmmry.Rows.Count; i++)
                        {
                            message.Body += "<tr>";
                            message.Body += "<td>";
                            message.Body += tblDailySmmry.Rows[i]["title"].ToString();
                            message.Body += "</td>";
                            message.Body += "<td align='right'>";
                            message.Body += tblDailySmmry.Rows[i]["details"].ToString();
                            message.Body += "</td>";
                            message.Body += "</tr>";
                        }

                        message.Body += "</table>";

                        message.Body += "<br>";
                        message.Body += "<br>";
                        message.Body += "Have a nice day.";
                        message.Body += "<br>";
                        message.Body += "<br>";
                        message.Body += "<br>";
                        message.Body += "Thanks & Regards, ";
                        message.Body += "<br>";
                        message.Body += "<br>";
                        message.Body += "Sammanpranali Team.";
                        message.Body += "</body></html>";

                        smtpClient.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    String a = ex.Message;
                }
            }
        }
    }
}