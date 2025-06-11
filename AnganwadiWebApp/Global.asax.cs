using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AnganwadiWebApp
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            if (HttpContext.Current.Server.GetLastError() != null)
            {
                Exception myException = HttpContext.Current.Server.GetLastError().GetBaseException();

                string pagename = HttpContext.Current.Request.Url.AbsolutePath;

                StackTrace st = new StackTrace(myException, true);

                StackFrame stackFrame = new StackFrame(0);

                WriteErrorLog(" Page Name : " + pagename + " | " + " Error Message : " + myException.Message);

                WriteErrorLog("---------------------------------------------------------------------------------------------------------------------------------------------------------------------");

               // Response.Redirect("~/DefaultError.aspx", true);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        public bool WriteErrorLog(string LogMessage)
        {
            bool Status = false;
            string LogDirectory = Server.MapPath("~/CustomErrorLog/");// Server.MapPath("CustomErrorLog\\");

            DateTime CurrentDateTime = DateTime.Now;
            string CurrentDateTimeString = CurrentDateTime.ToString();
            CheckCreateLogDirectory(LogDirectory);
            string logLine = BuildLogLine(CurrentDateTime, LogMessage);
            LogDirectory = (LogDirectory + "Log_" + LogFileName(DateTime.Now) + ".txt");
            StreamWriter oStreamWriter = null;
            try
            {
                oStreamWriter = new StreamWriter(LogDirectory, true);
                oStreamWriter.WriteLine(logLine);
                Status = true;
            }
            catch
            {

            }
            finally
            {
                if (oStreamWriter != null)
                {
                    oStreamWriter.Close();
                }
            }
            return Status;
        }

        private bool CheckCreateLogDirectory(string LogPath)
        {
            bool loggingDirectoryExists = false;
            DirectoryInfo oDirectoryInfo = new DirectoryInfo(LogPath);
            if (oDirectoryInfo.Exists)
            {
                loggingDirectoryExists = true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(LogPath);
                    loggingDirectoryExists = true;
                }
                catch
                {
                    // Logging failure
                }
            }
            return loggingDirectoryExists;
        }

        private string BuildLogLine(DateTime CurrentDateTime, string LogMessage)
        {
            StringBuilder loglineStringBuilder = new StringBuilder();
            loglineStringBuilder.Append(LogFileEntryDateTime(CurrentDateTime));
            loglineStringBuilder.Append(" \t");
            loglineStringBuilder.Append(LogMessage);
            return loglineStringBuilder.ToString();
        }

        public string LogFileEntryDateTime(DateTime CurrentDateTime)
        {
            return CurrentDateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }

        private string LogFileName(DateTime CurrentDateTime)
        {
            return CurrentDateTime.ToString("dd_MM_yyyy");
        }
    }
}
