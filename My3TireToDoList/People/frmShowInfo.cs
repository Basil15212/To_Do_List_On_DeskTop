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
    public partial class frmShowInfo : Form
    {

        int PersonID;
        public frmShowInfo()
        {
            InitializeComponent();
        }

        public frmShowInfo(int ID)
        {
            InitializeComponent();
            PersonID = ID;
        }

        public void FillShowPersonInfo()
        {
            clsPerson person = clsPerson.Find(PersonID);
            if(person != null)
            {
                lblID.Text = person.PersonID.ToString();
                lblFirstNAme.Text = person.FirstName.ToString();
                lblLastNAme.Text = person.LastName.ToString();
                lblEmail.Text = person.Email.ToString();
                lblPhone.Text = person.Phone.ToString();
                lblAddress.Text = person.Address.ToString();
                lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int.TryParse(lblID.Text, out int id);
            frmAddUpdatePerson frmEdit = new frmAddUpdatePerson(id);
            frmEdit.ShowDialog();
        }

        private void frmShowInfo_Load(object sender, EventArgs e)
        {
            FillShowPersonInfo();
        }
    }
}
