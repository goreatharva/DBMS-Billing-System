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
    public partial class MBox : Form
    {
        public MBox()
        {
            InitializeComponent();
            MessageLbl.Text = Message;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        static string Message;
        public static void Show(String msg)
        {
            Message = msg;
            MBox Obj = new MBox();
            Obj.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void ShowDialog(string v)
        {
            throw new NotImplementedException();
        }
    }
}
