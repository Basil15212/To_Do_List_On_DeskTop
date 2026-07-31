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
    }
}
