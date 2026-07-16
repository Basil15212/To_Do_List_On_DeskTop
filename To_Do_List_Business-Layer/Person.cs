using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Data_Layer;

namespace To_Do_List_Business_Layer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

       public int PersonID { get;set; }
       public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }


        public clsPerson ()
        {
            PersonID = -1;
            FirstName = "";
            LastName = "";
            Address = "";
            Phone = "";
            Email = "";
            DateOfBirth = DateTime.Now;
            Mode = enMode.AddNew;

        }

        private clsPerson (int ID ,string firstName ,string lastName ,string address ,string phone ,string email ,DateTime dateOfBirth )
        {
            PersonID = ID;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
            Email = email;
            DateOfBirth = dateOfBirth;
            Mode = enMode.Update;

        }

        public static clsPerson Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Address = "", PhoneNumber = "";
            DateTime DateOFBirth = DateTime.Now;

            if(clsPersonData.GetPersonInfoByID(ID ,ref FirstName ,ref LastName , ref Email ,ref Address , ref PhoneNumber ,ref DateOFBirth))
            {
                return new clsPerson(ID ,FirstName ,LastName ,Address , PhoneNumber,Email ,DateOFBirth);
            }
            else
            {
                return null;
            }
        }

        private  bool _AddNewUser()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.LastName, this.Email, this.Address, this.Phone, this.DateOfBirth);
            return (this.PersonID != -1);

        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID ,this.FirstName ,this.LastName ,this.Email ,this.Address ,this.Phone ,this.DateOfBirth);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                    case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static bool DeletePersonByID(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static DataTable GetAllPersons()
        {
            return clsPersonData.GetAllPersons();
        }

        public static bool IsExist(int ID)
        {
            return clsPersonData.IsExist(ID);
        }
    }
}
