using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace AnganwadiLib.Data
{
     class Cls_Dbconnection
    {
        

         ~Cls_Dbconnection() { }
         public static OracleConnection dbconn()
         {
             try
             {
                 //OracleConnection conn;
                 //conn = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProjectMgmtTest"].ConnectionString);
                 GetCon con = new GetCon();
                 return con.connection;
             }
             catch { return null; }
         }

    }
}
