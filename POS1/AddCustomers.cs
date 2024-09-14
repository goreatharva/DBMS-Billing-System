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
    public partial class AddCustomers : Form
    {
        public AddCustomers()
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
            CNameTb.Text = "";
            CPhoneTb.Text = "";
            CAddressTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CPhoneTb.Text == "" || CAddressTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                if (!int.TryParse(CPhoneTb.Text, out int phoneNumber) || phoneNumber < 0)
                {
                    MessageBox.Show("Invalid Input for Phone Number. Please enter a valid positive numeric value.");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTbl (CustName, CustAd, CustPhone) VALUES (@CN, @CA, @CP)", Con);
                        cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                        cmd.Parameters.AddWithValue("@CA", CAddressTb.Text);
                        cmd.Parameters.AddWithValue("@CP", phoneNumber); 
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer Saved");
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
    }
}
