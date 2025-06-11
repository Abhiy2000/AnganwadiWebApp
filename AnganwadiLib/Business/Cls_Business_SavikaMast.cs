using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class Cls_Business_SavikaMast
    {
        Int32 _CompID;
        Int32 _SevikaID;
        String _UserID;
        Int32 _AnganId;
        String _Name;
        String _SevikaCode;
        Int32 _EducId;
        DateTime _BirthDate;
        DateTime _RetireDate;
        String _Address;
        String _MobileNo;
        String _PhoneNo;
        String _PanNo;
        String _AadharNo;
        DateTime _JoinDate;
        String _OrderNo;
        DateTime _Orderdate;
        Int32 _DesigID;
        Int32 _PayScalID;
        Int32 _BranchId;
        String _AccNo;
        String _Remark;
        String _Active;
        Int32 _Mode;

        String _CPSMSCode;
        Int32 _ReligID;
        Int32 _CastID;
        String _MiddleName;
        String _Village;
        String _PinCode;
        Int32 _maritstatid;
        String _Reason;
        Int32 _Experience;

        String _Nom1Name;
        Int32 _Nom1RelatnId;
        Int32 _Nom1Age;
        String _Nom1Address;
        Int32 _Nom1BrID;
        String _Nom1Accno;
        Int32 _Nom1Ratio;     

        String _Nom2Name;
        Int32 _Nom2RelationId;
        Int32 _Nom2Age;
        String _Nom2Address;
        Int32 _Nom2BrID;
        String _Nom2Accno;
        Int32 _Nom2Ratio;

        String _Nom3name; 
        Int32 _Nom3relaid;
        Int32 _Nom3age;
        String _Nom3address;
        Int32 _Nom3BrID;
        String _Nom3Accno;
        Int32 _Nom3Ratio;
        DateTime _ExitDate;
        Int32 _ErrCode;
        String _ErrMsg;

        public String CPSMSCode
        {
            get { return _CPSMSCode; }
            set { _CPSMSCode = value; }
        }

        public Int32 ReligID
        {
            get { return _ReligID; }
            set { _ReligID = value; }
        }

        public Int32 CastID
        {
            get { return _CastID; }
            set { _CastID = value; }
        }

        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        public String Village
        {
            get { return _Village; }
            set { _Village = value; }
        }

        public String PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }

        public Int32 Maritstatid
        {
            get { return _maritstatid; }
            set { _maritstatid = value; }
        }

        public String Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }

        public Int32 Experience
        {
            get { return _Experience; }
            set { _Experience = value; }
        }

        public Int32 CompID
        {
            get { return _CompID; }
            set { _CompID = value; }
        }

        public Int32 SevikaID
        {
            get { return _SevikaID; }
            set { _SevikaID = value; }
        }

        public String UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public Int32 AnganId
        {
            get { return _AnganId; }
            set { _AnganId = value; }
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public String SevikaCode
        {
            get { return _SevikaCode; }
            set { _SevikaCode = value; }
        }

        public Int32 EducId
        {
            get { return _EducId; }
            set { _EducId = value; }
        }

        public DateTime BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        public DateTime RetireDate
        {
            get { return _RetireDate; }
            set { _RetireDate = value; }
        }

        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public String MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        public String PhoneNo
        {
            get { return _PhoneNo; }
            set { _PhoneNo = value; }
        }

        public String PanNo
        {
            get { return _PanNo; }
            set { _PanNo = value; }
        }

        public String AadharNo
        {
            get { return _AadharNo; }
            set { _AadharNo = value; }
        }

        public DateTime JoinDate
        {
            get { return _JoinDate; }
            set { _JoinDate = value; }
        }

        public String OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        public DateTime Orderdate
        {
            get { return _Orderdate; }
            set { _Orderdate = value; }
        }

        public Int32 DesigID
        {
            get { return _DesigID; }
            set { _DesigID = value; }
        }

        public Int32 PayScalID
        {
            get { return _PayScalID; }
            set { _PayScalID = value; }
        }

        public Int32 BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        public String AccNo
        {
            get { return _AccNo; }
            set { _AccNo = value; }
        }

        public String Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        public String Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public String Nom1Name
        {
            get { return _Nom1Name; }
            set { _Nom1Name = value; }
        }

        public Int32 Nom1RelatnId
        {
            get { return _Nom1RelatnId; }
            set { _Nom1RelatnId = value; }
        }

        public Int32 Nom1Age
        {
            get { return _Nom1Age; }
            set { _Nom1Age = value; }
        }

        public String Nom1Address
        {
            get { return _Nom1Address; }
            set { _Nom1Address = value; }
        }

        public String Nom2Name
        {
            get { return _Nom2Name; }
            set { _Nom2Name = value; }
        }

        public Int32 Nom2RelationId
        {
            get { return _Nom2RelationId; }
            set { _Nom2RelationId = value; }
        }

        public Int32 Nom1BrID
        {
            get { return _Nom1BrID; }
            set { _Nom1BrID = value; }
        }

        public String Nom1Accno
        {
            get { return _Nom1Accno; }
            set { _Nom1Accno = value; }
        }

        public Int32 Nom1Ratio
        {
            get { return _Nom1Ratio; }
            set { _Nom1Ratio = value; }
        }

        public Int32 Nom2Age
        {
            get { return _Nom2Age; }
            set { _Nom2Age = value; }
        }

        public String Nom2Address
        {
            get { return _Nom2Address; }
            set { _Nom2Address = value; }
        }

        public Int32 Nom2BrID
        {
            get { return _Nom2BrID; }
            set { _Nom2BrID = value; }
        }

        public String Nom2Accno
        {
            get { return _Nom2Accno; }
            set { _Nom2Accno = value; }
        }

        public Int32 Nom2Ratio
        {
            get { return _Nom2Ratio; }
            set { _Nom2Ratio = value; }
        }

        public String Nom3name
        {
            get { return _Nom3name; }
            set { _Nom3name = value; }
        }

        public Int32 Nom3relaid
        {
            get { return _Nom3relaid; }
            set { _Nom3relaid = value; }
        }

        public Int32 Nom3age
        {
            get { return _Nom3age; }
            set { _Nom3age = value; }
        }

        public String Nom3address
        {
            get { return _Nom3address; }
            set { _Nom3address = value; }
        }

        public Int32 Nom3BrID
        {
            get { return _Nom3BrID; }
            set { _Nom3BrID = value; }
        }

        public String Nom3Accno
        {
            get { return _Nom3Accno; }
            set { _Nom3Accno = value; }
        }

        public Int32 Nom3Ratio
        {
            get { return _Nom3Ratio; }
            set { _Nom3Ratio = value; }
        }


        public DateTime ExitDate
        {
            get
            {
                return _ExitDate;
            }

            set
            {
                _ExitDate = value;
            }
        }
        public string rejoinflag { get; set; }

        AnganwadiLib.Data.Cls_Data_SavikaMast DtSavikaMast;

        public Int32 Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public Int32 ErrorCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrorMessage
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }


        public Cls_Business_SavikaMast()
        {
            DtSavikaMast = new AnganwadiLib.Data.Cls_Data_SavikaMast();
        }

        public void UpDate()
        {
            DtSavikaMast.Update(this);
        }
    }
}
