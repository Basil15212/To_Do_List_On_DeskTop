using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using To_Do_List_Business_Layer;

namespace My3TireToDoList.People
{
    public partial class frmAddUpdatePerson : Form
    {

        enum enMode { AddNew =0 , UPdate =1}
        enMode _Mode = enMode.AddNew;

        int _PersonID = -1;
        clsPerson _Person;
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdatePerson(int ID)
        {
            InitializeComponent();
            _PersonID = ID;
            _Mode = enMode.UPdate;
        }

        public void FillPersonInfo()
        {
            _Person = clsPerson.Find(_PersonID);
            {
                if( _Person != null )
                {
                    txtFName.Text = _Person.FirstName;
                    txtLName.Text = _Person.LastName;
                    txtEmail.Text = _Person.Email;
                    txtPhone.Text = _Person.Phone;
                    txtAddress.Text = _Person.Address;
                    lblFullName.Text =(txtFName.Text) + (" ") + (txtLName.Text);
                }
                else
                {
                    MessageBox.Show("Person is not found", "Error");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                lblID.Text = "????";
            }
            else
            {
                lblTitle.Text = "Update Person";
                lblID.Text = _PersonID.ToString();
                FillPersonInfo();
            }
        }
    }
}
