using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace vacati_on
{
    public partial class frmDiscountUpdater : Form
    {
        public frmDiscountUpdater()
        {
            InitializeComponent();
        }

        OleDbConnection DiscountConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\vacati-on_db.accdb");

        public void prepareUpdater()
        {
            listView1.Items.Clear();

            DiscountConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = DiscountConnection;
            AccessCommand.CommandText = ("Select * from tblDiscount Where ID = @ID");
            AccessCommand.Parameters.AddWithValue("@ID", frmReservation.Globals.id);
            OleDbDataReader read = AccessCommand.ExecuteReader();
            while (read.Read())
            {
                ListViewItem addNew = new ListViewItem();

                addNew.Text = read["ID"].ToString();
                addNew.SubItems.Add(read["Type"].ToString());
                addNew.SubItems.Add(read["Rate"].ToString());

                listView1.Items.Add(addNew);

                textBox1.Text = read["Type"].ToString();
                textBox2.Text = read["Rate"].ToString();
                

            }
            DiscountConnection.Close();
        }

        private void updateDiscount()
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {

                DiscountConnection.Open();
                string sqlText = "Update tblDiscount set Type='" + textBox1.Text.ToString() + "',Rate='" + textBox2.Text.ToString() +"' Where ID =" + frmReservation.Globals.id + "";
                OleDbCommand AccessCommand = new OleDbCommand(sqlText, DiscountConnection);
                AccessCommand.ExecuteNonQuery();
                DiscountConnection.Close();
                MessageBox.Show("New Discount Saved Successfully", "Message", MessageBoxButtons.OK);
                frmDiscount discount = new frmDiscount();
                discount.Show();
                this.Close();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateDiscount();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDiscount discount = new frmDiscount();
            discount.Show();
            this.Close();
        }

        private void frmDiscountUpdater_Load(object sender, EventArgs e)
        {
            prepareUpdater();
            this.AcceptButton = button1;
        }
    }
}
