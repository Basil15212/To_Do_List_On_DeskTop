using My3TireToDoList.People;
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
    public partial class frmAddEditUser : Form
    {
        public event Action OnUserSavedSuccessfully;
        enum enMode { addNew =0 ,Update =1}
        enMode _Mode = enMode.addNew;

        private int _UserID = -1;
        private clsUser _USer;
        private int _SelectedPersonID = -1;

        private DataView _dtUsersView;

        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.addNew;
        }
        public frmAddEditUser(int UserId)
        {
            InitializeComponent();
            _UserID = UserId;
            _Mode = enMode.Update;
        }

        private void _loadPeopleList()
        {
            DataTable dtPeople = clsPerson.GetAllPersons();
            if (dtPeople != null)

            {

                _dtUsersView = dtPeople.DefaultView;
                dgvPErsonSelection.DataSource = _dtUsersView;
                dgvPErsonSelection.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }


        private void _ResetDefaultValues()
        {
            cbFiter.SelectedIndex = 0;
            txtSearchValue.Text = "";
            tabUserAddEdit.TabPages[1].Enabled = false;

            _loadPeopleList();
        }

        private void _LoadUSerData()
        {
            _USer = clsUser.FindUserByID(_UserID);
            if(_USer == null )
            {
                MessageBox.Show("This user does not exist in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            cbFiter.Enabled = false;
            btnNext.Enabled = true;
            tabUserAddEdit.TabPages[1].Enabled = true;

            _SelectedPersonID = _USer.PersonID;

            lblUserID.Text = _USer.UserID.ToString();
            txtUserName.Text =_USer.UserName;

            //Filling the pass word in case dont break on update
            txtPassWord.Text = _USer.Password;
            txtConfirmPassWord.Text = _USer.Password;

            chkIsActive.Checked = _USer.isActive;

        }

        private bool _ValidateFields()
        {
            bool IsValid = true;
            if(string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "\"This username is already taken by another user.\"");
                IsValid = false;
            }
            else
            {

                clsUser UserWithThisName =clsUser.FindUserByUserName(txtUserName.Text.Trim());
                if( UserWithThisName != null )
                {
                    if(_Mode ==enMode.addNew)
                    {
                        errorProvider1.SetError(txtUserName, "This username is already taken by another account.");
                        IsValid = false;
                    }
                    else if(_Mode == enMode.Update && UserWithThisName.UserID!= _UserID)
                    {
                        errorProvider1.SetError(txtUserName, "This username is taken. Please choose another.");
                        IsValid = false;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtUserName, "");
                }
                
            }

            if (string.IsNullOrWhiteSpace(txtPassWord.Text))
            {
                errorProvider1.SetError(txtPassWord, "Password cannot be completely empty.");
                IsValid = false;
            }
            else
            {
                errorProvider1.SetError(txtPassWord, "");
            }

            if(string.IsNullOrWhiteSpace(txtConfirmPassWord.Text))
            {
                errorProvider1.SetError(txtConfirmPassWord, "Please re-type your password here to confirm.");
                IsValid = false;
            }
            else if(txtPassWord.Text != txtConfirmPassWord.Text)
            {
                errorProvider1.SetError(txtConfirmPassWord, "Passwords do not match. Please verify your typing.");
                IsValid = false;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassWord, "");
            }

            return IsValid;
        }



        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if(_Mode == enMode.addNew)
            {
                _loadPeopleList();
                lblTitle.Text = "Add New User";
                _USer = new clsUser();
            }
            else
            {
                lblTitle.Text = "Update User Info";
                _LoadUSerData();
            }
        }

        private void txtSearchValue_TextChanged(object sender, EventArgs e)
        {
            if (_dtUsersView == null)
                return;
            string Filter = "";
            switch(cbFiter.Text)
            {
                case "ID":
                    Filter = "PersonID";
                    break;
                case "FirstName":
                    Filter = "FirstName";
                    break;
                case "PhoneNumber":
                    Filter = "Phone";
                    break;

            }

            if(string.IsNullOrEmpty(txtSearchValue.Text))
            {
                _dtUsersView.RowFilter = "";
            }
            else
            {
                if (Filter == "PersonID")
                {
                    if (int.TryParse(txtSearchValue.Text.Trim(), out int ID))
                    {
                        _dtUsersView.RowFilter = $"{Filter} = {ID}";
                    }
                    else
                    {
                        _dtUsersView.RowFilter = "1=0";
                    }
                }
                else
                {
                    _dtUsersView.RowFilter = $"{Filter} like '{txtSearchValue.Text.Trim()}%'";
                }
            }
            
        }
        private void cbFiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear out the search text box whenever they switch filter modes
            txtSearchValue.Text = "";

            if (_dtUsersView != null)
            {
                _dtUsersView.RowFilter = ""; // Reset the view to show all rows
            }

            // Put cursor focus straight into the textbox for immediate typing convenience
            txtSearchValue.Focus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dgvPErsonSelection.CurrentRow != null && dgvPErsonSelection.CurrentRow.Index >= 0)
            {
                btnNext.Enabled = true;
                if (int.TryParse(dgvPErsonSelection.CurrentRow.Cells["PersonId"].Value.ToString() ,out int val))
                {
                    if(_Mode ==enMode.addNew)
                    {
                        if(clsUser.ISPersonLinked(val))
                        {
                            MessageBox.Show(
                            "This person is already linked to an existing system user account.\nPlease select another person or check the user records list.",
                            "Account Duplication Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Hand
                            );
                            return;
                        }
                        
                    }

                    _SelectedPersonID = val;
                    _USer.PersonID = val;
                    tabUserAddEdit.TabPages[1].Enabled = true;
                    btnNext.Enabled = true;
                    tabUserAddEdit.SelectedIndex = 1;
                }
            }
            else
            {
                btnNext.Enabled = false;
                MessageBox.Show("Please highlight a valid data row from the selection list first.", "Selection Missing");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!_ValidateFields())
            {
                MessageBox.Show("Please hover over the red icons and correct all form errors before saving.",
                        "Validation Errors Exist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            _USer.PersonID = _SelectedPersonID;
            _USer.UserName = txtUserName.Text.Trim();
            _USer.Password= txtPassWord.Text;
            _USer.isActive =chkIsActive.Checked;

            try
            {
                if(_USer.Save())
                {
                    MessageBox.Show("User Account Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _UserID = _USer.UserID;
                    lblUserID.Text = _UserID.ToString();

                    _Mode = enMode.Update;
                    lblTitle.Text = "Update User Info";

                    // the delegate
                    OnUserSavedSuccessfully?.Invoke();
                }
                else
                {
                    MessageBox.Show("Error: Data failed to write down to the SQL database tables.",
                            "Database Insertion Failure",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"A systemic crash exception was successfully caught: {ex.Message}",
                        "Runtime Protection Active",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmPersonPopup= new frmAddUpdatePerson();
            frmPersonPopup.OnPersonSaved += FrmPersonPopup_OnPersonSaved;
            frmPersonPopup.ShowDialog();
        }
        private void FrmPersonPopup_OnPersonSaved(int NewPersonID)
        {
            _loadPeopleList();
            _SelectedPersonID = NewPersonID;


            dgvPErsonSelection.ClearSelection();

        }
    }
}
