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
    public partial class usersout : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader read;
        usersReg Reguser = new usersReg();

        public usersout()
        {
            InitializeComponent();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string query = "Select * from Login where ID = '" + txtserch.Text + "'";
            con.Open();
            cmd = new SqlCommand(query, con);
            read = cmd.ExecuteReader();

            if (read.Read())
            {
                this.Hide();
                usersReg Alluser = new usersReg();
                Alluser.btninsert.Enabled = false;
                Alluser.Show();
                Alluser.Txtuser.Text = read.GetValue(0).ToString();
                Alluser.usennametxt.Text = read.GetValue(1).ToString();
                Alluser.passtxt.Text = read.GetValue(2).ToString();                      

            }
            else
            {
                MessageBox.Show("This is invalid information", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            con.Close();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            string query = "delete from Login where ID = '" + txtserch.Text + "'";
            con.Open();
            cmd = new SqlCommand(query, con);
            int result = cmd.ExecuteNonQuery();
            MessageBox.Show(result == 1 ? "Delete is Done" : "Failed");
            con.Close();
            display();
        }

        private void usersout_Load(object sender, EventArgs e)
        {
            display();
        }

        public void display()
        {
            try
            {
                con.Open();
                DataSet set = new DataSet();
                string query = "select * from Login";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(set, "Login");
                dataview.DataSource = set.Tables["Login"];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Register_form formreg = new Register_form();
            formreg.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            usersReg ragister = new usersReg();
            ragister.Show();
        }

        private void txtserch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtserch.Text == "")
                {
                    display();
                }
                else
                {
                    string query = "select * from Login where ID = '" + txtserch.Text + "' ";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    DataSet set = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(set, "Login");
                    dataview.DataSource = set.Tables[0];
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
