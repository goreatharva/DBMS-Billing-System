using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS1
{
    public partial class ViewCustomers : Form
    {
        int Key = 0;
        public ViewCustomers()
        {
            InitializeComponent();
            DisplayCustomers();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Anushka\OneDrive\Documents\POSDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayCustomers()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            CNameTb.Text = "";
            CAddressTb.Text = "";
            CPhoneTb.Text = "";
            Key = 0;
        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MBox.Show("Select the customer!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustId = @CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MBox.Show("Customer deleted");
                    Con.Close();
                    DisplayCustomers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = CustomersDGV.Rows[e.RowIndex];

                CNameTb.Text = selectedRow.Cells[1].Value.ToString();
                CAddressTb.Text = selectedRow.Cells[2].Value.ToString();
                CPhoneTb.Text = selectedRow.Cells[3].Value.ToString();

                Key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        } 
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CAddressTb.Text == "" || CPhoneTb.Text == "" || Key==0)
            {
                MBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl set CustName=@CN,CustAd=@CA,CustPhone=@CP where CustId=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CAddressTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MBox.Show("Customer Updated");
                    Con.Close();
                    DisplayCustomers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Close();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void CAddressTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
