using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace AnganwadiLib.Data
{
    public class GetCon
    {
        public OracleConnection connection = new OracleConnection();

        public GetCon()
        {
            connection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProjectMgmt"].ConnectionString); //Local -ProjectMgmt  --For Live ProjectMgmtTest
        }

        public void OpenConn()
        {
            connection.Open();
        }

        public void CloseConn()
        {
            connection.Close();
        }
    }
}
