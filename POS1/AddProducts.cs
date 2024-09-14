using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS1
{
    public partial class AddProducts : Form
    {
        public AddProducts()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Anushka\OneDrive\Documents\POSDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void Reset()
        {
            PNameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            PCatCb.SelectedIndex = -1;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
                if (PNameTb.Text == "" || PCatCb.SelectedIndex == -1 || PriceTb.Text == "" || QtyTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    if (!double.TryParse(PriceTb.Text, out double price) || price < 0)
                    {
                        MessageBox.Show("Invalid Price Input");
                    }
                    else if (!int.TryParse(QtyTb.Text, out int quantity) || quantity < 0)
                    {
                        MessageBox.Show("Invalid Quantity Input");
                    }
                    else
                    {
                        try
                        {
                            Con.Open();
                            SqlCommand cmd = new SqlCommand("insert into ProductTbl(PName,PCat,PPrice,PQty)values(@PN,@PC,@PP,@PQ)", Con);
                            cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                            cmd.Parameters.AddWithValue("@PC", PCatCb.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                            cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Product Saved");
                            Con.Close();
                            Reset();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }
                    }
                }
        }


        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void PriceTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
