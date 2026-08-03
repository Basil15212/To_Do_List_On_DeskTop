using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Data_Layer;

namespace To_Do_List_Business_Layer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode _Mode = enMode.AddNew;

        public int UserID {  get; set; }
        public int PersonID { get; set; }
        public string UserName {  get; set; }
        public string Password { get; set; }
        public bool isActive {  get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsUser()
        {
            this.isActive = false;
            this.PersonID = -1;
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this._Mode = enMode.AddNew;

        }
        private clsUser(int UserID ,int PersonID ,string UserName,string Password ,bool isActive)
        {
            this.UserID = UserID ;
            this.PersonID = PersonID ;
            this.UserName = UserName ;
            this.Password = Password ;
            this.isActive= isActive;

            this.PersonInfo = clsPerson.Find(PersonID);
            this._Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID =clsUserData.AddNewUser(this.PersonID,this.UserName,this.Password,this.isActive);
            return (UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.PersonID,this.UserID, this.UserName, this.Password, this.isActive);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public static bool DeleteUserByID(int UserID)
        {
            return clsUserData.DeleteUserWithID(UserID);
        }

        public static clsUser FindUserByID(int UserID)
        {
            int PErsonID=-1; string USerName = "", PassWord = ""; bool isActive = false;
            if(clsUserData.GetUserInfoByID(UserID ,ref PErsonID ,ref USerName ,ref PassWord ,ref isActive))
            {
                return new clsUser(UserID,PErsonID ,USerName,PassWord ,isActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindUserByUserName(string UserName)
        {
            int PersonID = -1, UserId = -1;string PassWord = ""; bool isActive = false;
            if(clsUserData.GetUserInfoByUSerName(UserName ,ref UserId ,ref PersonID ,ref PassWord ,ref isActive))
            {
                return new clsUser(UserId, PersonID ,UserName ,PassWord ,isActive);
            }
            else { return null; }

        }
        public static clsUser FindUserByIDAndUSerName(int UserID,string USerName)
        {
            int PErsonID=-1; string  PassWord = ""; bool isActive = false;
            if(clsUserData.GetUserinfoByIDAndUserName(UserID ,USerName ,ref PErsonID ,ref PassWord ,ref isActive))
            {
                return new clsUser(UserID ,PErsonID ,USerName,PassWord,isActive);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllUsers()
        {
            DataTable dtAllUSers = new DataTable();
            dtAllUSers = clsUserData.GetAllUsers();
            return dtAllUSers;
        }

        public static bool IsUSerExist(int USerID)
        {
            return clsUserData.IsUserExist(USerID);
        }
        public static bool ISPersonLinked(int PersonID)
        {
            return clsUserData.IsPersonLinkedWithUser(PersonID);
        }
    }
}
