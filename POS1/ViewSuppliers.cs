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
    public partial class ViewSuppliers : Form
    {
        int Key = 0;
        public ViewSuppliers()
        {
            InitializeComponent();
            DisplaySuppliers();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Anushka\OneDrive\Documents\POSDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplaySuppliers()
        {
            Con.Open();
            string Query = "Select * from SupplierTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SuppliersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MBox.Show("Select the Supplier!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SupplierTbl where SupId = @SKey", Con);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MBox.Show("Supplier deleted");
                    Con.Close();
                    DisplaySuppliers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void SuppliersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = SuppliersDGV.Rows[e.RowIndex];

                SNameTb.Text = selectedRow.Cells[1].Value.ToString();
                SAddressTb.Text = selectedRow.Cells[2].Value.ToString();
                SPhoneTb.Text = selectedRow.Cells[3].Value.ToString();
                SRemarksTb.Text = selectedRow.Cells[4].Value.ToString();

                Key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }
        private void Reset()
        {
            SNameTb.Text = "";
            SAddressTb.Text = "";
            SPhoneTb.Text = "";
            SRemarksTb.Text = "";
            Key = 0;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SRemarksTb.Text == "" || SPhoneTb.Text == "" || SAddressTb.Text == "")
            {
                MBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update SupplierTbl set SupName=@SN, SupAddress=@SA, SupPhone=@SP, SupRem=@SR where SupId=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddressTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SR", SRemarksTb.Text);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MBox.Show("Supplier updated");
                    Con.Close();
                    DisplaySuppliers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Close();
        }
    }
}
