using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace loginpage
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection("server=.; database=inventory_System;Integrated Security=true");
        public Login()
        {
            InitializeComponent();
        }

        private void logginbtn_Click(object sender, EventArgs e)
        {
          

            string username = txusername.Text;
            string password = txtPassword.Text;

            con.Open();
            string query = "SELECT * FROM Login WHERE Username = '" + username + "' AND Password = '" + password + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            if (dr.Read())
            {
                Dashboard form2 = new Dashboard();
                this.Hide();
                form2.txuseout.Text = dr[1].ToString();

                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!");
            }

            con.Close();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txusername.Text = "";
            txtPassword.Text = "";
            txusername.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ForgetPassword forget = new ForgetPassword();
            forget.Show();
        }
    }
}
