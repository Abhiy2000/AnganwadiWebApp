using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataRetirementAge
    {
        BoRetirementAge objAge;

        public DataRetirementAge()
        { }

        public DataRetirementAge(BoRetirementAge Age)
        {
            objAge = Age;
        }

        public void InsertAge(BoRetirementAge Age)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_retirementage_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Cmd.Parameters.Add("in_CompId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AgeId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Age", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_DesgId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AffectDate", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            //Cmd.Parameters["in_CompId"].Value = Cast.CompId;
            Cmd.Parameters["in_AgeId"].Value = Age.AgeId;
            Cmd.Parameters["in_Age"].Value = Age.Age;
            Cmd.Parameters["in_DesgId"].Value = Age.Desg;
            Cmd.Parameters["in_AffectDate"].Value = Age.AffectDate;
            Cmd.Parameters["in_UserId"].Value = Age.UserId;
            Cmd.Parameters["in_Mode"].Value = Age.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            Age.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            Age.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
