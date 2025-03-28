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
    public partial class Orderout : Form
    {
        public Orderout()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader read;

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btndel_Click(object sender, EventArgs e)
        {
            string query = "delete  Orders where OrderID = '" + txnsearchOrder.Text + "'";
            con.Open();
            cmd = new SqlCommand(query, con);
            int result = cmd.ExecuteNonQuery();
            MessageBox.Show(result == 1 ? "Delete is Done" : "Failed");
            con.Close();
            display();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txnsearchOrder.Text == "")
            {
                MessageBox.Show("Please Fill The information", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {

                string query = "Select * from Orders where OrderID = '" + txnsearchOrder.Text + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                read = cmd.ExecuteReader();

                if (read.Read())
                {
                    this.Hide();
                    Order_Reg ro = new Order_Reg();
                    ro.btnSave.Enabled = false;
                    ro.Show();
                    ro.txt_ID.Text = read.GetValue(0).ToString();
                    ro.txtProID.Text = read.GetValue(1).ToString();
                    ro.txtCusID.Text = read.GetValue(2).ToString();
                    ro.dateTimePicker.Value = Convert.ToDateTime(read.GetValue(3));
                    ro.txtQty.Text = read.GetValue(4).ToString();
                    ro.txtTotalAmount.Text = read.GetValue(5).ToString();
                }
                else
                {
                    MessageBox.Show("This is invalid information", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.Close();
            }
        }

        private void dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order_Reg register = new Order_Reg();
            register.btnUpdate.Enabled = false;
            register.generatuserId();
            register.Show();
        }

        private void Orderout_Load(object sender, EventArgs e)
        {
            displayAll();

        }

        private void txnsearchOrder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txnsearchOrder.Text == "")
                {
                    display();
                }
                else
                {
                    string query = "select o.OrderID,cs.Customer_Name, pr.pro_name,o.orderDate,o.Quantity , o.totals from Orders o,customer cs,production pr where cs.Customer_ID = o.Customer_ID and pr.pro_id = o.pro_id and o.OrderID = '" + txnsearchOrder.Text + "' ";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    DataSet set = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(set, "Orders");
                    dataviewtable.DataSource = set.Tables[0];
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void display()
        {
            try
            {
                con.Open();
                DataSet set = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("select o.OrderID,cs.Customer_Name, pr.pro_name,o.orderDate,o.Quantity , o.totals from Orders o,customer cs,production pr where cs.Customer_ID = o.Customer_ID and pr.pro_id = o.pro_id", con);
                adapter.Fill(set, "Orders");
                dataviewtable.DataSource = set.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void displayAll()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select o.OrderID,cs.Customer_Name, pr.pro_name,o.orderDate,o.Quantity , o.totals from Orders o, customer cs, production pr where cs.Customer_ID = o.Customer_ID and pr.pro_id = o.pro_id", con);
                adapter.Fill(dt);
                dataviewtable.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order_Reg or = new Order_Reg();
            or.Show();
        }
    }
}
