using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataPromotion
    {

        AnganwadiLib.Business.BoPromotion ObjPromotion;
        public DataPromotion()
        { }

        public DataPromotion(AnganwadiLib.Business.BoPromotion BoPromotion)
        {
            ObjPromotion = BoPromotion;
        }

        public void Insert(AnganwadiLib.Business.BoPromotion BoPromotion)
        {
            //this.BoPromotion = BoPromotion;
            //AnganwadiLib.Data.GetCon Con = new GetCon();
            Data.GetCon Con = new GetCon();
           
            OracleCommand cmd = new OracleCommand("Aoup_Promotion_ins", Con.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_COMPID", OracleDbType.Int64).Direction = ParameterDirection.Input;      
            cmd.Parameters.Add("in_SEVIKAID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_PromoteDT", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldQualification", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewQualification", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldDesFlag", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewDesFlag", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldDesID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewDesID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldPayscale", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewPayscale", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldExp", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewExpID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            
            cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("out_ErrorMessage", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            
            cmd.Parameters["in_UserId"].Value = BoPromotion.UserName;
            cmd.Parameters["in_Mode"].Value = BoPromotion.Mode;
            cmd.Parameters["in_COMPID"].Value = BoPromotion.COMPID;
            cmd.Parameters["in_SEVIKAID"].Value = BoPromotion.SEVIKAID;
            if (BoPromotion.PromoteDT != DateTime.MinValue)
            {
                cmd.Parameters["in_PromoteDT"].Value = BoPromotion.PromoteDT;
            }
            else
            {
                cmd.Parameters["in_PromoteDT"].Value = DBNull.Value;
            }
            //cmd.Parameters["in_PromoteDT"].Value = BoPromotion.PromoteDT;
            cmd.Parameters["in_OldQualification"].Value = BoPromotion.OldQualification;
            cmd.Parameters["in_NewQualification"].Value = BoPromotion.NewQualification;
            cmd.Parameters["in_OldDesFlag"].Value = BoPromotion.OldDesFlag;
            cmd.Parameters["in_NewDesFlag"].Value = BoPromotion.NewDesFlag;
            cmd.Parameters["in_OldDesID"].Value = BoPromotion.OldDesID;
            cmd.Parameters["in_NewDesID"].Value = BoPromotion.NewDesID;
            cmd.Parameters["in_OldPayscale"].Value = BoPromotion.OldPayscale;
            cmd.Parameters["in_NewPayscale"].Value = BoPromotion.NewPayscale;
            cmd.Parameters["in_OldExp"].Value = BoPromotion.OldExp;
            cmd.Parameters["in_NewExpID"].Value = BoPromotion.NewExpID;
            Con.OpenConn();
            cmd.ExecuteNonQuery();
            Con.CloseConn();


            BoPromotion.ErrorCode = Convert.ToInt32(cmd.Parameters["out_Errorcode"].Value.ToString());
            BoPromotion.ErrorMessage = cmd.Parameters["out_ErrorMessage"].Value.ToString();

        }

        public void Demotion(AnganwadiLib.Business.BoPromotion BoPromotion)
        {
            //this.BoPromotion = BoPromotion;
            //AnganwadiLib.Data.GetCon Con = new GetCon();
            Data.GetCon Con = new GetCon();

            OracleCommand cmd = new OracleCommand("Aoup_Demotion_ins", Con.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_COMPID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_SEVIKAID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_PromoteDT", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldQualification", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewQualification", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldDesFlag", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewDesFlag", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldDesID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewDesID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldPayscale", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewPayscale", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_OldExp", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewExpID", OracleDbType.Int64).Direction = ParameterDirection.Input;

            cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("out_ErrorMessage", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


            cmd.Parameters["in_UserId"].Value = BoPromotion.UserName;
            cmd.Parameters["in_Mode"].Value = BoPromotion.Mode;
            cmd.Parameters["in_COMPID"].Value = BoPromotion.COMPID;
            cmd.Parameters["in_SEVIKAID"].Value = BoPromotion.SEVIKAID;
            if (BoPromotion.PromoteDT != DateTime.MinValue)
            {
                cmd.Parameters["in_PromoteDT"].Value = BoPromotion.PromoteDT;
            }
            else
            {
                cmd.Parameters["in_PromoteDT"].Value = DBNull.Value;
            }
            //cmd.Parameters["in_PromoteDT"].Value = BoPromotion.PromoteDT;
            cmd.Parameters["in_OldQualification"].Value = BoPromotion.OldQualification;
            cmd.Parameters["in_NewQualification"].Value = BoPromotion.NewQualification;
            cmd.Parameters["in_OldDesFlag"].Value = BoPromotion.OldDesFlag;
            cmd.Parameters["in_NewDesFlag"].Value = BoPromotion.NewDesFlag;
            cmd.Parameters["in_OldDesID"].Value = BoPromotion.OldDesID;
            cmd.Parameters["in_NewDesID"].Value = BoPromotion.NewDesID;
            cmd.Parameters["in_OldPayscale"].Value = BoPromotion.OldPayscale;
            cmd.Parameters["in_NewPayscale"].Value = BoPromotion.NewPayscale;
            cmd.Parameters["in_OldExp"].Value = BoPromotion.OldExp;
            cmd.Parameters["in_NewExpID"].Value = BoPromotion.NewExpID;
            Con.OpenConn();
            cmd.ExecuteNonQuery();
            Con.CloseConn();


            BoPromotion.ErrorCode = Convert.ToInt32(cmd.Parameters["out_Errorcode"].Value.ToString());
            BoPromotion.ErrorMessage = cmd.Parameters["out_ErrorMessage"].Value.ToString();

        }
    }
}
