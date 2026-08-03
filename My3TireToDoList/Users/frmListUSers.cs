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
    public partial class frmListUSers : Form
    {
        public frmListUSers()
        {
            InitializeComponent();
        }

        public void FilldgvPeople()
        {
            try
            {
                DataTable dtUsers = clsUser.GetAllUsers();

                // 1. Reset the grid binding to prevent layout duplication glitches
                dgvUsers.DataSource = null;

                // 2. Protect your code from throwing a NullReferenceException if the DB fails
                if (dtUsers != null && dtUsers.Rows.Count > 0)
                {
                    // 3. Bind your fresh data rows
                    dgvUsers.DataSource = dtUsers  ;

                    // 4. Force columns to expand horizontally and fill the empty gray grid space
                    dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // 5. Automatically adjust row heights to fit the text padding perfectly
                    dgvUsers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
                else
                {
                    MessageBox.Show("No records found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // 6. Catch connection losses or broken queries without crashing the entire app
                MessageBox.Show($"Failed to load data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SetForm()
        {
            FilldgvPeople();
            lblRecords.Text = dgvUsers.RowCount.ToString();
        }
        private void frmListUSers_Load(object sender, EventArgs e)
        {
            SetForm();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvUsers.CurrentRow.Cells[0].Value.ToString(), out int val))
            {
                frmShowUserDetails frmUSerDetails =new frmShowUserDetails(val);
                frmUSerDetails.ShowDialog();
            }
            else
            {
                MessageBox.Show("UnDefind User ID","Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _RefreshUsersListGrid()
        {
            DataTable dtUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = null;
            if (dtUsers != null)
            {
                dgvUsers.DataSource = dtUsers;
                dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {

            frmAddEditUser frmAddNewUser1 = new frmAddEditUser();
            frmAddNewUser1.OnUserSavedSuccessfully += FrmAddEditUser_OnUserSavedSuccessfully;
            frmAddNewUser1.ShowDialog();
        }
        private void FrmAddEditUser_OnUserSavedSuccessfully()
        {
            _RefreshUsersListGrid();
            lblRecords.Text =dgvUsers.RowCount.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvUsers.CurrentRow.Cells[0].Value.ToString(),out int val))
            {
                frmAddEditUser editUser =new frmAddEditUser(val);
                editUser.ShowDialog();
            }
        }
    }
}
