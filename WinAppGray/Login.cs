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

namespace WinAppGray
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Khurram-Mac\\Desktop\\db_Gray.mdf;Integrated Security=True;Connect Timeout=30");
            //Command
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHere Email = '"+txtEmail.Text+"' AND Password = '"+txtPass.Text+"'", con);

            //Adopter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable
            DataTable dt = new DataTable();

            //Fill
            da.Fill(dt);
            //if
            if (dt.Rows.Count > 0 )
            {
                Form1 f = new Form1();
                f.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Email or Password incorrect");
            }


        }
    }
}
