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
    public partial class AddSuppliers : Form
    {
        public AddSuppliers()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Anushka\OneDrive\Documents\POSDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void Reset()
        {
            SNameTb.Text = "";
            SPhoneTb.Text = "";
            SAddressTb.Text = "";
            SRemarks.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SRemarks.Text == "" || SPhoneTb.Text == "" || SAddressTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                if (!int.TryParse(SPhoneTb.Text, out int phoneNumber) || phoneNumber < 0)
                {
                    MessageBox.Show("Invalid Phone Number. Please enter a positive integer.");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into SupplierTbl (SupName,SupAddress,SupPhone,SupRem) values (@SN, @SA, @SP, @SR)", Con);
                        cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                        cmd.Parameters.AddWithValue("@SA", SAddressTb.Text);
                        cmd.Parameters.AddWithValue("@SP", phoneNumber); 
                        cmd.Parameters.AddWithValue("@SR", SRemarks.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Supplier Saved");
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

        private void SNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
