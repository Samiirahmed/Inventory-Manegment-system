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
    public partial class ForgetPassword : Form
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");


        private void ForgetPassword_Load(object sender, EventArgs e)
        {
            resetbtn.Visible = false;
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "select Password from Login where Username = '" + txtusername.Text + "' or ID = '" + txtusername.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MessageBox.Show(rd.GetString(0));
            }
            con.Close();
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            string query = "select Password from Login where Username = '"+txtusername.Text+"' or ID = '"+txtusername.Text+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                resetbtn.Visible = true;
            }else
            {
                resetbtn.Visible = false;
            }
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();

        }
    }
}
