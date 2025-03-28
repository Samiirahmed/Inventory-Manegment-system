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
    public partial class Register_module : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");
        SqlCommand cmd;
        public Register_module()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            product_show form3 = new product_show();
            form3.Show();
            fillData();
        }

        void fillData()

        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter Da = new SqlDataAdapter("select * from production  ",con);
                ds.Clear();
                Da.Fill(ds, "production");
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
           

            try
            {
                if (txtname.Text == "" || txtID.Text == "" || txtprice.Text == "" || txtdesc.Text == "" || txtCategories.Text=="")
                {
                    MessageBox.Show("Please fill production ");
                }
                else
                {
                    con.Open();
                    decimal price = decimal.Parse(txtprice.Text);
                    cmd = new SqlCommand("insert into production values ('" + txtID.Text + "','" + txtname.Text + "','" + price + "', '" + txtdesc.Text + "' , '"+ txtCategories .Text+ "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order succeful Registred ");
                    txtname.Text = "";
                    txtprice.Text = "";
                    txtdesc.Text = "";
                    txtCategories.Text = "";
                    generatuserId();
                    con.Close();
                    fillData();
                

                }
            }

            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            product_show form3 = new product_show();
            form3.Show();

        }

        public void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Display();
            generatuserId();
        }

        public string project_id
        {
            get;
        }
            

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            product_show form = new product_show();

            try
            {

                //if (form.txtserch.Text == " ")
                //{
                //    MessageBox.Show("plz insert search textbox something");

                //}
                con.Open();
                decimal price = decimal.Parse(txtprice.Text);
                string query = "update production set  pro_id='" + txtID.Text + "',   pro_name='" + txtname.Text + "', pro_price='" +price+ "', pro_descrip='" + txtdesc.Text + "', pro_Category='" + txtCategories.Text + "' where pro_id = '"+txtID.Text+"'";
                 cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();
                MessageBox.Show(result == 1 ? "Update is Done" : "Failed");
                con.Close();
                Display();
              
                txtID.Text = "";
                txtname.Text = "";
                txtprice.Text = "";
                txtdesc.Text = "";
                txtCategories.Text = "";
          

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Display()
        {
            try
            {
                product_show form = new product_show();
                SqlConnection con = new SqlConnection("Server=.; Initial Catalog=inventory_System;Integrated Security=True");
                con.Open();
                DataSet set = new DataSet();
                string query = "select * from production";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(set, "production");
                form.dataview.DataSource = set.Tables["production"];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public String generatuserId()
        {

            int curentId = 0;
            string genertedID = "Pro_100";
            con.Open();
            cmd = new SqlCommand("select Count(pro_id) from production ", con);
            SqlDataReader dr = cmd.ExecuteReader();



            if (dr.Read())
            {
                curentId = Convert.ToInt32(dr[0]) + 1;
                txtID.Text = genertedID + curentId;
            }

            else
            {
                txtID.Text = genertedID + curentId;
            }

            con.Close();

            return txtID.Text;

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
