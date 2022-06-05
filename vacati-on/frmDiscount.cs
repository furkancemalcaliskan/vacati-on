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
    public partial class frmDiscount : Form
    {
        public frmDiscount()
        {
            InitializeComponent();
        }

        OleDbConnection DiscountConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");
        private void showInformation()
        {
            listView1.Items.Clear();
            DiscountConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand(); AccessCommand.Connection = DiscountConnection; AccessCommand.CommandText = ("Select * from tblDiscount"); OleDbDataReader read = AccessCommand.ExecuteReader();
            while (read.Read())
            {
                ListViewItem addNew = new ListViewItem();

                addNew.Text = read["ID"].ToString();
                addNew.SubItems.Add(read["Type"].ToString());
                addNew.SubItems.Add(read["Rate"].ToString());
                

                listView1.Items.Add(addNew);
            }
            DiscountConnection.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                DiscountConnection.Open();
                string sqlText = "INSERT INTO tblDiscount (Type,Rate) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "')";
                OleDbCommand AccessCommand = new OleDbCommand(sqlText, DiscountConnection);
                AccessCommand.ExecuteNonQuery();
                DiscountConnection.Close();
                MessageBox.Show("New Discount Saved Successfully", "Message", MessageBoxButtons.OK);
                showInformation();
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, null);
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                e.Cancel = true;
                errorProvider2.SetError(textBox2, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(textBox2, null);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DiscountConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = DiscountConnection;

            AccessCommand.CommandText = ("Delete from tblDiscount Where ID = @ID");
            AccessCommand.Parameters.AddWithValue("@ID", listView1.SelectedItems[0].SubItems[0].Text);
            AccessCommand.ExecuteNonQuery();
            DiscountConnection.Close();
            button3.Enabled = false;
            button4.Enabled = false;

            showInformation();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = true;
            frmReservation.Globals.id = listView1.SelectedItems[0].SubItems[0].Text.ToString();
        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {
            showInformation();
            this.AcceptButton = button1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmDiscountUpdater updater = new frmDiscountUpdater();
            updater.Show();
            this.Close();
        }
    }
}
