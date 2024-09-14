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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings Obj = new Billings();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(UNameTb.Text=="" || PasswordTb.Text == "")
            {
                MBox.Show("Enter Username And Password");
            }
            else if(UNameTb.Text=="Admin" && PasswordTb.Text=="Password")
            {
                MainMenu Obj = new MainMenu();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MBox.Show("Wrong Username or Password");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
