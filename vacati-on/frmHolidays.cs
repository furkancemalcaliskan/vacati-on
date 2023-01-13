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
    public partial class frmHolidays : Form
    {
        public frmHolidays()
        {
            InitializeComponent();
        }

        OleDbConnection HolidayConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\vacati-on_db.accdb");
        private void showInformation()
        {
            listView1.Items.Clear();
            HolidayConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = HolidayConnection;
            AccessCommand.CommandText = ("Select * from tblHoliday");
            OleDbDataReader read = AccessCommand.ExecuteReader();
            while (read.Read())
            {
                ListViewItem addNew = new ListViewItem();

                addNew.Text = read["ID"].ToString();
                addNew.SubItems.Add(read["Type"].ToString());
                addNew.SubItems.Add(read["Cost"].ToString());
                addNew.SubItems.Add(read["Members"].ToString());
                addNew.SubItems.Add(read["CurrencySym"].ToString());


                listView1.Items.Add(addNew);
            }
            HolidayConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                HolidayConnection.Open();
                string sqlText = "Insert Into tblHoliday (Type,Cost,Members,CurrencySym) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString()+ "','" + textBox3.Text.ToString() + "','" + comboBox1.SelectedItem.ToString() + "')";
                OleDbCommand AccessCommand = new OleDbCommand(sqlText, HolidayConnection);
                AccessCommand.ExecuteNonQuery();
                HolidayConnection.Close();
                MessageBox.Show("New Holiday Saved Successfully", "Message", MessageBoxButtons.OK);
                showInformation();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.Refresh();
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

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                e.Cancel = true;
                errorProvider3.SetError(textBox3, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(textBox3, null);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            HolidayConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand(); AccessCommand.Connection = HolidayConnection; AccessCommand.CommandText = ("Select * from tblHoliday"); OleDbDataReader read = AccessCommand.ExecuteReader();
            while (read.Read())
            {
                ListViewItem addNew = new ListViewItem();

                addNew.Text = read["ID"].ToString();
                addNew.SubItems.Add(read["Type"].ToString());
                addNew.SubItems.Add(read["Cost"].ToString());
                addNew.SubItems.Add(read["Members"].ToString());
                addNew.SubItems.Add(read["CurrencySym"].ToString());

                listView1.Items.Add(addNew);
            }
            HolidayConnection.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HolidayConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = HolidayConnection;

            AccessCommand.CommandText = ("Delete from tblHoliday Where ID = @ID");
            AccessCommand.Parameters.AddWithValue("@ID", listView1.SelectedItems[0].SubItems[0].Text);
            AccessCommand.ExecuteNonQuery();
            HolidayConnection.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            frmHolidayUpdater updater = new frmHolidayUpdater();
            updater.Show();
            this.Close();
        }

        private void frmHolidays_Load(object sender, EventArgs e)
        {
            showInformation();
            this.AcceptButton = button1;
        }
    }
}
