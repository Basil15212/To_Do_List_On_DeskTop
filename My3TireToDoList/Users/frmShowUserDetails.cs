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

namespace My3TireToDoList.Users
{
    public partial class frmShowUserDetails : Form
    {
        private int _UserID;
        public frmShowUserDetails()
        {
            InitializeComponent();
        }
        public frmShowUserDetails(int ID)
        {
            InitializeComponent();
            _UserID = ID;
        }

        public void FillShowUserInfo()
        {
            clsUser User1 = clsUser.FindUserByID(_UserID);
            if (User1 != null)
            {
                lblID.Text = User1.PersonInfo.PersonID.ToString();
                lblFirstNAme.Text =     User1.PersonInfo.FirstName.ToString();
                lblLastNAme.Text =      User1.PersonInfo.LastName.ToString();
                lblEmail.Text =         User1.PersonInfo.Email.ToString();
                lblPhone.Text =         User1.PersonInfo.Phone.ToString();
                lblAddress.Text =       User1.PersonInfo.Address.ToString();
                lblDateOfBirth.Text =   User1.PersonInfo.DateOfBirth.ToShortDateString();

                lblUserID.Text =        User1.UserID.ToString();
                lblUserName.Text = User1.UserName.ToString();
                chbIsActive.Checked = User1.isActive;

            }

        }

        private void frmShowUserDetails_Load(object sender, EventArgs e)
        {
            FillShowUserInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
