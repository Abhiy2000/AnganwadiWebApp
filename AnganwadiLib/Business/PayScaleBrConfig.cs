using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class PayScaleBrConfig
    {
        Int32 _BrId;
        Int32 _PayId;
        Int32 _ProjId;
        Int32 _EduId;
        Int32 _wages;
        Int32 _central;
        Int32 _state;
        Int32 _fixed;
        String _active;
        String _Pay;
        Int32 _desigid;
        Int32 _expfrom;
        Int32 _expto;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrCode;
        String _ErrMsg;
        DateTime _EffectiveFrmDt;  // added on 20/08/19


        public Int32 BrId
        {
            get { return _BrId; }
            set { _BrId = value; }
        }

        public Int32 EduId
        {
            get { return _EduId; }
            set { _EduId = value; }
        }

        public Int32 State
        {
            get { return _state; }
            set { _state = value; }
        }

        public Int32 Central
        {
            get { return _central; }
            set { _central = value; }
        }

        public Int32 fixed1
        {
            get { return _fixed; }
            set { _fixed = value; }
        }

        public Int32 Wages
        {
            get { return _wages; }
            set { _wages = value; }
        }

        public Int32 PayId
        {
            get { return _PayId; }
            set { _PayId = value; }
        }

        public String Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public Int32 ProjId
        {
            get { return _ProjId; }
            set { _ProjId = value; }
        }

        public String Pay
        {
            get { return _Pay; }
            set { _Pay = value; }
        }

        public Int32 Desigid
        {
            get { return _desigid; }
            set { _desigid = value; }
        }

        public Int32 Expfrom
        {
            get { return _expfrom; }
            set { _expfrom = value; }
        }

        public Int32 Expto
        {
            get { return _expto; }
            set { _expto = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 ErrCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        public Int32 Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }


        public DateTime EffectiveFrmDt
        {
            get { return _EffectiveFrmDt; }
            set { _EffectiveFrmDt = value; }
        }


        DataPayScaleBrConfig DtPayScaleBrConfig;

        public PayScaleBrConfig()
        {
            DtPayScaleBrConfig = new AnganwadiLib.Data.DataPayScaleBrConfig(this);
        }

        public void Insert()
        {
            DtPayScaleBrConfig.Insert(this);
        }
    }
}
