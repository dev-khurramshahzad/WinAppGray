using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinAppGray
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            //Required Field
            if (txtName.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Please Fill all required Fields Properly..", "Incomplete Form");
                return;
            }


            //Password Matching
            if (txtPass.Text != txtCPass.Text)
            {
                MessageBox.Show("Password do not match. Please Try Again", "Incomplete Form");
                return;
            }


            //Email Validation
            if (!Regex.IsMatch(txtEmail.Text, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
            {
                MessageBox.Show("Email is not in correct format", "Incomplete Form");
                return;
            }

            //Password Strenth
            if (!Regex.IsMatch(txtPass.Text, "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
            {
                MessageBox.Show("Password must contaain an upercase, a lower a symbol and must be 8 characters long ", "Weak Password");
                return;
            }

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Khurram-Mac\\Desktop\\db_Gray.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES ('"+txtName.Text+"','"+txtEmail.Text+"','"+txtPass.Text+"','"+txtAddress.Text+"','"+txtPhone.Text+"','image','very good student','pending')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();




            //Confirmation
            MessageBox.Show("Account Registered","Success");
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtPass.Text, "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
            {
                lblpassword.Text = "Weak Password";
                lblpassword.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblpassword.Text = "Strong Password";
                lblpassword.ForeColor = System.Drawing.Color.Green;


            }


        }
    }
}
