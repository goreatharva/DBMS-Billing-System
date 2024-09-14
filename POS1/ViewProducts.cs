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
    
    public partial class ViewProducts : Form
    {
        int Key = 0;
        public ViewProducts()
        {
            InitializeComponent();
            DisplayProducts();
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
        private void DisplayProducts()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
            {
                if (Key == 0)
                {
                    MBox.Show("Select the product!");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from ProductTbl where PId = @PKey", Con);
                        cmd.Parameters.AddWithValue("@PKey", Key);
                        cmd.ExecuteNonQuery();
                        MBox.Show("Product deleted");
                        Con.Close();
                        DisplayProducts();
                        Reset();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = ProductsDGV.Rows[e.RowIndex];

                PNameTb.Text = selectedRow.Cells[1].Value.ToString();
                PCatCb.SelectedItem = selectedRow.Cells[2].Value.ToString();
                PriceTb.Text = selectedRow.Cells[3].Value.ToString();
                QtyTb.Text = selectedRow.Cells[4].Value.ToString();

                Key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }
        private void Reset()
        {
            PNameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            PCatCb.SelectedIndex = -1;
            Key = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "" || PCatCb.SelectedIndex == -1 || PriceTb.Text == "" || QtyTb.Text == "")
            {
                MBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProductTbl set PName=@PN,PCat=@PC,PPrice=@PP,PQty=@PQ where PId=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", PCatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MBox.Show("Product Updated");
                    Con.Close();
                    DisplayProducts();
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
