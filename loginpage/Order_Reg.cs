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
  
    public partial class Order_Reg : Form
    {


        decimal TotalAmount = 1;
        public Order_Reg()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");
        SqlCommand cmd;
        string query;

        private void Order_Reg_Load(object sender, EventArgs e)
        {
            

            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM production", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataview.DataSource = dt;

            }

            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM customer", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewCus.DataSource = dt;
            }



        }



        public void display()
        {
            try
            {
                con.Open();
                DataSet set = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from customer", con);
                adapter.Fill(set, "customer");
                dataGridViewCus.DataSource = set.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       

        public String generatuserId()
        {
            con.Open();
            int curentId = 0;
            string genertedID = "OR_100";
            cmd = new SqlCommand("select Count(OrderID) from Orders ", con);
            SqlDataReader dr = cmd.ExecuteReader();



            if (dr.Read())
            {
                curentId = Convert.ToInt32(dr[0]) + 1;
                txt_ID.Text = genertedID + curentId;
            }

            else
            {
                txt_ID.Text = genertedID + curentId;
            }

            con.Close();

            return txt_ID.Text;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            try
            {
                string productID = txtProID.Text;
                string OrderID = txt_ID.Text; 
                string CustomerID = txtCusID.Text;
                int quantity = Convert.ToInt32(txtQty.Text);

                // Insert data into database
                using (SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True;"))

                {
                    con.Open();
                    string query = "insert into Orders values(@OrderID, @pro_id, @Customer_ID, @orderDate, @Quantity, @totals)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", OrderID);
                        cmd.Parameters.AddWithValue("@pro_id", productID);
                        cmd.Parameters.AddWithValue("@Customer_ID", CustomerID);
                        cmd.Parameters.AddWithValue("@orderDate", dateTimePicker.Value);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@totals", txtTotalAmount.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order is successfully!");
                        clear();
                        generatuserId();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void clear()
        {
            txt_ID.Clear(); txtTotalAmount.Clear(); txtProName.Clear(); txtQty.Clear(); lblPrice.Text = ""; txtProID.Clear();
            txtCusID.Clear(); txtCusN.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Orderout form = new Orderout();
                string query = "update Orders set pro_id = '"+txtProID.Text+"' , Customer_ID = '"+txtCusID.Text+"' , orderDate = '"+dateTimePicker.Value+"' , Quantity = '"+txtQty.Text+"' where OrderID = '"+txt_ID.Text+"'";
                con.Open();
                cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();
                MessageBox.Show(result == 1 ? "Update is Done" : "Failed");
                con.Close();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);

            }
        }

        private void txtSearchPRO_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtSearchCUS.Text == "")
            //    {
            //        display();
            //    }
            //    else
            //    {
            //        string query = "select * from customer where Customer_ID = '" + txtSearchCUS.Text + "' ";
            //        con.Open();
            //        SqlCommand cmd = new SqlCommand(query, con);
            //        cmd.ExecuteNonQuery();
            //        DataSet set = new DataSet();
            //        SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            //        adapter.Fill(set, "customer");
            //        dataGridViewCus.DataSource = set.Tables[0];
            //        con.Close();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Orderout orders = new Orderout();
            orders.Show();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

          if (txtQty.Text == ""){
                    txtTotalAmount.Clear();
                }
                else
                {
                    int Price = int.Parse(lblPrice.Text);
                    int quantity = int.Parse(txtQty.Text);
                    TotalAmount = quantity * Price;
                txtTotalAmount.Text = TotalAmount.ToString();
                }
        }

        private void dataGridViewCus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.RowIndex >= 0)
            //{
            //    DataGridViewRow data = dataGridViewCus.Rows[e.RowIndex];
            //    txtCusID.Text = data.Cells["Customer_ID"].Value.ToString();
            //    txtCusN.Text = data.Cells["Customer_Name"].Value.ToString();
            //}           
        }

        private void txtProID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select pro_name, pro_price from production where pro_id = '" + txtProID.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtProName.Text = reader.GetString(0);
                    lblPrice.Text = reader.GetValue(1).ToString();
                }
                else
                {
                    txtProName.Text = "";
                    lblPrice.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtCusID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select Customer_Name from customer where Customer_ID = '"+ txtCusID.Text+"'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtCusN.Text = reader.GetValue(0).ToString();
                }
                else
                {
                    txtCusN.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
