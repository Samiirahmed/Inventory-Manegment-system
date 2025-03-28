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
    public partial class usersReg : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=inventory_System;Integrated Security=True");
        SqlCommand cmd;
        public usersReg()
        {
            InitializeComponent();
        }

        private void usersReg_Load(object sender, EventArgs e)
        {
            Display();
        }


        public void Display()
        {
            try
            {
                //pratiko_update form = new pratiko_update()/*;*/
                con.Open();
                DataSet set = new DataSet();
                string query = "select * from Login";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(set, "Login");
                //form.dataview.DataSource = set.Tables["Login"];
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
                SqlDataAdapter Da = new SqlDataAdapter("select * from Login  ", con);
                ds.Clear();
                Da.Fill(ds, "Login");
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
                if ( Txtuser.Text == "" ||usennametxt.Text == "" || passtxt.Text == "" )
                {
                    MessageBox.Show("Please fill user ");
                }
                else
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("insert into Login values (ID = @User_ID, username = @UserName, password = @password)", con);
                        cmd.CommandText = "InsertingUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@User_ID", Txtuser.Text);
                        cmd.Parameters.AddWithValue("@UserName", usennametxt.Text);
                        cmd.Parameters.AddWithValue("@password", passtxt.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("user succeful Registred ");
                        con.Close();
                        usennametxt.Text = "";
                        passtxt.Text = "";
                        fillData();
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }

            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                product_show form = new product_show();
                con.Open();
                string query = "update Login set  Username='" + usennametxt.Text + "',   Password='" + passtxt.Text + "' where ID = '" + Txtuser.Text + "'";
                cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();
                MessageBox.Show(result == 1 ? "Update is Done" : "Failed");
                con.Close();
                Display();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public String generatuserId()
        {

            int curentId = 0;
            string genertedID = "US_100";
            con.Open();
            cmd = new SqlCommand("select Count(ID) from Login ", con);
            SqlDataReader dr = cmd.ExecuteReader();



            if (dr.Read())
            {
                curentId = Convert.ToInt32(dr[0]) + 1;
                Txtuser.Text = genertedID + curentId;
            }

            else
            {
                Txtuser.Text = genertedID + curentId;
            }

            con.Close();

            return Txtuser.Text;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            usersout user = new usersout();
            user.Show();
        }

        private void Btndel_Click(object sender, EventArgs e)
        {
            usersout outputs = new usersout();
            outputs.Show();
        }
    }
}
