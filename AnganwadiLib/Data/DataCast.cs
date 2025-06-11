using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using AnganwadiLib.Business;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataCast
    {
        BoCast objCast;

        public DataCast()
        { }

        public DataCast(BoCast Cast)
        {
            objCast = Cast;
        }

        public void InsertCast(BoCast Cast)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_cast_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Cmd.Parameters.Add("in_CompId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CastId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CastName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            //Cmd.Parameters["in_CompId"].Value = Cast.CompId;
            Cmd.Parameters["in_CastId"].Value = Cast.CastId;
            Cmd.Parameters["in_CastName"].Value = Cast.CastName;
            Cmd.Parameters["in_UserId"].Value = Cast.UserId;
            Cmd.Parameters["in_Mode"].Value = Cast.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            Cast.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            Cast.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
