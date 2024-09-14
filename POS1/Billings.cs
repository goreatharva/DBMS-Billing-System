using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS1
{
    public partial class Billings : Form
    {
        int Key = 0;
        public Billings()
        {
            InitializeComponent();
            DisplayProducts();
            GetCustomer();
          
        }

        private void CAddressTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
        

        private void Billings_Load(object sender, EventArgs e)
        {

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

        public void GetCustName()
        {
            Con.Open();
            string Query = "select * from CustomerTbl where CustId = " + CustIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }

        private void SearchProducts()
        {
            Con.Open();
            string Query = "Select * from ProductTbl where PName='"+SearchTb.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchProducts();
            SearchTb.Text = "";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            DisplayProducts();
            SearchTb.Text = "";
        }
        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustId from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            CustIdCb.ValueMember = "CustId";
            CustIdCb.DataSource = dt;
            Con.Close();
        }
        private void Reset()
        {
            PName = "";
            QtyTb.Text = "";
            Key = 0;
        }
        private void UpdateQty()
        {
            int newQty = PStock - Convert.ToInt32(QtyTb.Text);
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProductTbl set PQty=@PQ where PId=@PKey", Con); 
                    cmd.Parameters.AddWithValue("@PQ", newQty);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    DisplayProducts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
        }
        string PName;
        int PPrice, PStock;
        int n = 1, total= 0;
        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = ProductsDGV.Rows[e.RowIndex];

                PName = selectedRow.Cells[1].Value.ToString();
                PPrice = Convert.ToInt32(selectedRow.Cells[3].Value.ToString());
                PStock = Convert.ToInt32(selectedRow.Cells[4].Value.ToString());

                Key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void VATTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void VATTb_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void VATTb_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void VATTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (VATTb.Text == "" || SubTotalTb.Text == "")
            {
                GrdTotalTb.Text = "";
            }
            else
            {
                try
                {
                    double GST = (Convert.ToDouble(VATTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    double discount = string.IsNullOrEmpty(DiscTb.Text) ? 0 : (Convert.ToDouble(DiscTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    double grandTotal = Convert.ToInt32(SubTotalTb.Text) + GST - discount;

                    TotTaxTb.Text = GST.ToString();
                    DiscTotTb.Text = discount.ToString();
                    GrdTotalTb.Text = grandTotal.ToString();
                }
                catch (Exception Ex)
                {
                    MBox.Show(Ex.Message);
                }
            }
        }

        private void DiscTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (DiscTb.Text == "" || SubTotalTb.Text == "")
            {
                GrdTotalTb.Text = "";
            }
            else
            {
                try
                {
                    double discount = (Convert.ToDouble(DiscTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    double VAT = string.IsNullOrEmpty(VATTb.Text) ? 0 : (Convert.ToDouble(VATTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    double grandTotal = Convert.ToInt32(SubTotalTb.Text) + VAT - discount;

                    DiscTotTb.Text = discount.ToString();
                    TotTaxTb.Text = VAT.ToString();
                    GrdTotalTb.Text = grandTotal.ToString();
                }
                catch (Exception Ex)
                {
                    MBox.Show(Ex.Message);
                }
            }
        }

        private void InsertBill()
        {
            if (CustIdCb.SelectedIndex == -1 || PaymentMtdCb.SelectedIndex == -1 || GrdTotalTb.Text == "" )
            {
                MBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(BDate,CustId,CustName,PMethod,Amt)values(@BD,@CI,@CN,@PM,@Am)", Con);
                    cmd.Parameters.AddWithValue("@BD", BDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@PM", PaymentMtdCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Am",  Convert.ToDouble(GrdTotalTb.Text));
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int flag = 1;
        private void PrintBtn_Click(object sender, EventArgs e)
        {

            if (CustIdCb.SelectedIndex == -1 || PaymentMtdCb.SelectedIndex == -1 || GrdTotalTb.Text == "" || DiscTb.Text == "" || VATTb.Text == "" || SubTotalTb.Text == "")
            {
                MBox.Show("Missing Information");
                return; 
            }

            using (BillSavedMsg billSavedMessageBox = new BillSavedMsg())
            {
                InsertBill();
                billSavedMessageBox.ShowDialog(); 
                flag = 1;
                if (flag == 1)
                {
                    printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
            }
        }

        int prodid, prodqty, prodprice, tottal, pos = 65;

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void QtyTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            ViewBills Obj = new ViewBills();
            Obj.Show();
        }

        string prodname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Lucida Sans Typewriter", 8, FontStyle.Bold);
            Brush brush = Brushes.DarkBlue;

            int topMargin = 10; 
            int leftMargin = 10;
            int verticalSpacing = 12;

            int y = topMargin;

            e.Graphics.DrawString("*************************", new Font("Lucida Sans Typewriter", 12, FontStyle.Regular), Brushes.Black, new Point(leftMargin, y-5));
            y += verticalSpacing;
            e.Graphics.DrawString("DMART POS", new Font("Lucida Sans Typewriter", 15, FontStyle.Bold), Brushes.Red, new Point(leftMargin + 80, y-5));
            y += verticalSpacing;
            e.Graphics.DrawString("-------------------------", new Font("Lucida Sans Typewriter", 12, FontStyle.Regular), Brushes.Black, new Point(leftMargin, y));
            y += verticalSpacing;
            e.Graphics.DrawString("Customer Name: " + CustNameTb.Text, new Font("Lucida Sans Typewriter", 8, FontStyle.Bold), Brushes.DarkGreen, new Point(leftMargin, y));
            y = y + 15;
            e.Graphics.DrawString("Date: " + BDate.Text, new Font("Lucida Sans Typewriter", 8, FontStyle.Bold), Brushes.DarkGreen, new Point(leftMargin, y));
            y += verticalSpacing;
            e.Graphics.DrawString("-------------------------", new Font("Lucida Sans Typewriter", 12, FontStyle.Regular), Brushes.Black, new Point(leftMargin, y));
            y += verticalSpacing;
            int xID = leftMargin;
            int xProduct = 45; 
            int xPrice = 167;
            int xQuantity = 117; 
            int xTotal = 232; 
            e.Graphics.DrawString("S.No", font, brush, new Point(xID, y));
            e.Graphics.DrawString("Product", font, brush, new Point(xProduct, y));
            e.Graphics.DrawString("Quantity", font, brush, new Point(xPrice, y));
            e.Graphics.DrawString("Price", font, brush, new Point(xQuantity, y));
            e.Graphics.DrawString("Total", font, brush, new Point(xTotal, y));
            y = y + 5;
            e.Graphics.DrawString("-------------------------", new Font("Lucida Sans Typewriter", 12, FontStyle.Regular), Brushes.Black, new Point(leftMargin, y));
            y += verticalSpacing;
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + prodid, font, brush, new Point(xID, y));
                e.Graphics.DrawString("" + prodname, font, brush, new Point(xProduct, y));
                e.Graphics.DrawString("" + prodprice, font, brush, new Point(xPrice, y));
                e.Graphics.DrawString("" + prodqty, font, brush, new Point(xQuantity, y));
                e.Graphics.DrawString("" + tottal, font, brush, new Point(xTotal, y));

                y += verticalSpacing;
            }

            e.Graphics.DrawString("-------------------------", new Font("Lucida Sans Typewriter", 12, FontStyle.Regular), Brushes.Black, new Point(leftMargin, y + 40));;
            y += verticalSpacing;
            e.Graphics.DrawString("Grand Total: ₹ " + GrdTotalTb.Text, new Font("Lucida Sans Typewriter", 9, FontStyle.Bold), Brushes.DarkGreen, new Point(leftMargin, y + 50));
            e.Graphics.DrawString("*************************", new Font("Lucida Sans Typewriter", 12, FontStyle.Regular), Brushes.Black, new Point(leftMargin, y + 85));
            e.Graphics.DrawString("Paid Using " + PaymentMtdCb.Text, new Font("Lucida Sans Typewriter", 9, FontStyle.Bold), Brushes.DarkGreen, new Point(leftMargin, y + 110));
            // Clear and reset
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            GrdTotalTb.Text = "";
        }


        // Print button click event
        private void print_btn_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CustomSize", 850, 1100);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }


        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void AddToBill_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MBox.Show("Select a product!");
            }
            else if (string.IsNullOrWhiteSpace(QtyTb.Text))
            {
                MBox.Show("Enter the quantity");
            }
            else
            {
                try
                {
                    int quantity = Convert.ToInt32(QtyTb.Text);
                    if (quantity > PStock)
                    {
                        MBox.Show("Sorry! The Product is Out of Stock");
                    }
                    else
                    {
                        int SubTotal = quantity * PPrice;
                        total = total + SubTotal;
                        DataGridViewRow newRow = new DataGridViewRow();
                        newRow.CreateCells(BillDGV);
                        newRow.Cells[0].Value = n;
                        newRow.Cells[1].Value = PName;
                        newRow.Cells[2].Value = QtyTb.Text;
                        newRow.Cells[3].Value = PPrice;
                        newRow.Cells[4].Value = SubTotal;
                        BillDGV.Rows.Add(newRow);
                        n++;
                        SubTotalTb.Text = "" + total;
                        UpdateQty();
                        Reset();
                    }
                }
                catch (FormatException)
                {
                    MBox.Show("Quantity must be a valid number.");
                }
            }
        }

    }
}
