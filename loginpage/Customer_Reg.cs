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
using System.Text.RegularExpressions;

namespace loginpage
{
    public partial class Register_form : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");
        SqlCommand cmd;
        public Register_form()
        {
            InitializeComponent();
        }

        public void Display()
        {
            try
            {
                //pratiko_update form = new pratiko_update();
                con.Open();
                DataSet set = new DataSet();
                string query = "select * from customer";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(set, "customer");
                //form.dataview.DataSource = set.Tables["customer"];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        void fillData()

        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter Da = new SqlDataAdapter("select * from customer  ", con);
                ds.Clear();
                Da.Fill(ds, "customer");
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        private void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (nametxt.Text == "" || telltxt.Text == "" || txtemail.Text == "")
                {
                    MessageBox.Show("Please fill customer information.");
                }
                else if (!IsValidEmail(txtemail.Text)) // Validate Email
                {
                    MessageBox.Show("Invalid email format! Email must contain '@' and '.'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    con.Open();
                    string query = "INSERT INTO customer VALUES (Customer_Name=@CustomerName,Phone=@Phone,Email=@Email)";
                    cmd = new SqlCommand(query, con);
                    cmd.CommandText = "InsertingCustomer";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerName",nametxt.Text);
                    cmd.Parameters.AddWithValue("@Phone", telltxt.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer successfully registered.");                    
                    // Clear fields after successful entry
                    nametxt.Text = "";
                    telltxt.Text = "";
                    txtemail.Text = "";
                    fillData();                   
                }
            
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            // Function to validate email
            bool IsValidEmail(string email)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(txtemail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Email-ka waa sax la'aan, fadlan geli email sax ah.");
                    return;
                }

                product_show form = new product_show();
                con.Open();
                string query = "update customer set Customer_Name='" + nametxt.Text + "', PHONE='" + telltxt.Text + "', Email='" + txtemail.Text + "' where Customer_ID = '" + txtID.Text + "'";
                cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();
                MessageBox.Show(result == 1 ? "Update is Done" : "Failed");
                con.Close();
                Display();

                // Clear fields after successful entry
                nametxt.Text = "";
                telltxt.Text = "";
                txtemail.Text = "";
                fillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ragistre_pratiko_Load(object sender, EventArgs e)
        {
            Display();
        }

        

        private void Btndel_Click(object sender, EventArgs e)
        {
            this.Hide();
            //pratiko_update pro = new pratiko_update();
            //pro;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //pratiko_update update = new pratiko_update();
            //update.Show();
        }
    }
}
