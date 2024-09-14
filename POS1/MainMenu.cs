using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS1
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AddProducts Obj = new AddProducts();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ViewProducts Obj = new ViewProducts();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            AddSuppliers Obj = new AddSuppliers();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ViewSuppliers Obj = new ViewSuppliers();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            AddCustomers Obj = new AddCustomers();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ViewCustomers Obj = new ViewCustomers();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            ViewBills Obj = new ViewBills();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            ViewProducts Obj = new ViewProducts();
            Obj.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            ViewCustomers Obj = new ViewCustomers();
            Obj.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            ViewSuppliers Obj = new ViewSuppliers();
            Obj.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
                Billings Obj = new Billings();
                Obj.Show();
                this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}
