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
    public partial class Dashboard : Form
    {
        SqlConnection con = new SqlConnection("server=.; database=inventory_System;Integrated Security=true");
        SqlCommand cmd;
        SqlDataReader reader;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            product_show form3 = new product_show();
            form3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Loginform = new Login();
            Loginform.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //pratiko_update customer = new pratiko_update();
           
        }

        private void txuseout_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            usersout outuser = new usersout();
            outuser.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Orderout outt = new Orderout();
            outt.Show();
        }
    }
}
