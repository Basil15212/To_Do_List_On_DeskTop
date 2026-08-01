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
    public partial class frmPeopleScreen : Form
    {
        public frmPeopleScreen()
        {
            InitializeComponent();
        }

        public void FilldgvPeople()
        {
            try
            {
                DataTable dtPeople = clsPerson.GetAllPersons();

                // 1. Reset the grid binding to prevent layout duplication glitches
                dgvListPeople.DataSource = null;

                // 2. Protect your code from throwing a NullReferenceException if the DB fails
                if (dtPeople != null && dtPeople.Rows.Count > 0)
                {
                    // 3. Bind your fresh data rows
                    dgvListPeople.DataSource = dtPeople;

                    // 4. Force columns to expand horizontally and fill the empty gray grid space
                    dgvListPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // 5. Automatically adjust row heights to fit the text padding perfectly
                    dgvListPeople.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            lblPeopleCount.Text =dgvListPeople.RowCount.ToString();
        }

        private void dgvListPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmPeopleScreen_Load(object sender, EventArgs e)
        {
            SetForm();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(int.TryParse(dgvListPeople.CurrentRow.Cells[0].Value?.ToString(), out int val))
            {
                frmAddUpdatePerson frmAddEdit = new frmAddUpdatePerson(val);
                frmAddEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cant Edit this person");
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAdd_Edit = new frmAddUpdatePerson();
            frmAdd_Edit.ShowDialog();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddNew_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetForm();
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvListPeople.CurrentRow.Cells[0].Value?.ToString(), out int val))
            {
                frmShowInfo frmShowInfo = new frmShowInfo(val);
                frmShowInfo.ShowDialog();
            }
                
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvListPeople.CurrentRow.Cells[0].Value?.ToString(), out int val))
            {
                DialogResult result = MessageBox.Show(
                "Are you sure you want to Delete Person [" + val + "] ?",
                 "Confirm Delete",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
                 );
                
                if(result == DialogResult.OK)
                {
                   
                    clsPerson.DeletePersonByID(val);
                    button1_Click(sender, e);
                    MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("This person cannot be deleted because they are linked to other data records in the system.",
                                        "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
