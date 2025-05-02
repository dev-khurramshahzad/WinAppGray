using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppGray.AdminPages
{
    public partial class ManageCategories : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\Databases\\db_Gray.mdf;Integrated Security=True;");

        public ManageCategories()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            SqlCommand cmd = new SqlCommand("Select * From Categories", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void CheckRecord()
        {


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand($"Select * From Categories WHERE CatName = '{txtName.Text}'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count  > 0)
            {
                MessageBox.Show($"{txtName.Text} is already exists", "Warning");
                return;
            }

            cmd = new SqlCommand($"INSERT INTO Categories VALUES ('{txtName.Text}','{txtDetails.Text}','{ddlStatus.SelectedItem.ToString()}','Image')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            LoadData();

            txtName.Text = "";
            txtDetails.Text = "";

            MessageBox.Show($"{txtName.Text} is added successfully", "Item Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Enter a valid ID to delete item", "Warning");
                return;
            }

            SqlCommand cmd = new SqlCommand($"Select * From Categories WHERE CatID = {txtID.Text}", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show($"No record found for ID {txtID.Text}", "Warning");
                return;
            }

            var response = MessageBox.Show("Are you sure you want to delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (response == DialogResult.Yes)
            {
                cmd = new SqlCommand($"DELETE FROM Categories Where CatID = {txtID.Text}", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data is deleted successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LoadData();
            }





        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Enter a valid ID to delete item", "Warning");
                return;
            }

            SqlCommand cmd = new SqlCommand($"Select * From Categories WHERE CatID = {txtID.Text}", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show($"No record found for ID {txtID.Text}", "Warning");
                return;
            }

            txtName.Text = dt.Rows[0][1].ToString();
            txtDetails.Text = dt.Rows[0][2].ToString();

            txtID.Enabled = false;
            btnAdd.Enabled = false;

            btnEdit.Visible = false;
            btnUpdate.Visible = true;

            btnDelete.Enabled = false;
            btnView.Enabled = false;


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand($"Update Categories SET CatName = '{txtName.Text}',Details = '{txtDetails.Text}' Where CatID = {txtID.Text}", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Data is Updated successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            LoadData();

            txtName.Text = "";
            txtDetails.Text = "";

            txtID.Enabled = true;
            btnAdd.Enabled = true;

            btnEdit.Visible = true;
            btnUpdate.Visible = false;

            btnDelete.Enabled = true;
            btnView.Enabled = true;
        }
    }
}
