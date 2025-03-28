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
    public partial class product_show : Form

    {
        SqlCommand cmd;SqlDataReader read;
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");
        public product_show()
        {
            InitializeComponent();
            display();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            Register_module form4 = new Register_module();
            form4.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            Dashboard form2 = new Dashboard();
            form2.Show();

        }

       

       public void display()
        {
            try
            {
                SqlConnection con = new SqlConnection("Server=.; Initial Catalog=inventory_System;Integrated Security=True");
                con.Open();
                DataSet set = new DataSet();
                string query = "select * from production";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(set, "production");
                dataview.DataSource = set.Tables["production"];
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard form2 = new Dashboard();
            form2.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Register_module form4 = new Register_module();
            form4.btnUpdate.Enabled = false;
            form4.Show();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {           

            string query = "Select * from production where pro_id = '" + txtserch.Text+"'";
            con.Open();
            cmd = new SqlCommand(query, con);
            read = cmd.ExecuteReader();

            if (read.Read())
            {
                this.Hide();
                Register_module form4 = new Register_module();
                form4.btnsave.Enabled = false;
                form4.Show();
                form4.txtID.Text = read.GetValue(0).ToString();
                form4.txtname.Text = read.GetValue(1).ToString();
                form4.txtprice.Text = read.GetValue(2).ToString();
                form4.txtdesc.Text = read.GetValue(3).ToString();
                form4.txtCategories.Text = read.GetValue(4).ToString();
                
            }
            else
            {
                MessageBox.Show("This is invalid information", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();



        }

        private void txtserch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txtserch.Text == "")
                {
                    display();
                }
                else
                {
                    string query = "select * from production where pro_id = '" + txtserch.Text + "' ";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    DataSet set = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(set, "production");
                    dataview.DataSource = set.Tables[0];
                    con.Close();
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btndel_Click(object sender, EventArgs e)
        {            
            string query = "delete from production where pro_id = '" + txtserch.Text+"'";
            con.Open();
            cmd = new SqlCommand(query, con);
            int result = cmd.ExecuteNonQuery();
            MessageBox.Show(result == 1 ? "Delete is Done" : "Failed");
            display();
            txtserch.Clear();
            con.Close();
        }

        private void product_show_Load(object sender, EventArgs e)
        {

        }



    }

       
    }

