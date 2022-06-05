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
    public partial class frmHolidayUpdater : Form
    {
        public frmHolidayUpdater()
        {
            InitializeComponent();
        }

        OleDbConnection HolidayConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");

        public void prepareUpdater()
        {
            listView1.Items.Clear();

            HolidayConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = HolidayConnection;
            AccessCommand.CommandText = ("Select * from tblHoliday Where ID = @ID");
            AccessCommand.Parameters.AddWithValue("@ID", frmReservation.Globals.id);
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

                textBox1.Text = read["Type"].ToString();
                textBox2.Text = read["Cost"].ToString();
                textBox3.Text = read["Members"].ToString();
                comboBox1.SelectedItem = read["CurrencySym"].ToString();


            }
            HolidayConnection.Close();
        }
        private void updateHoliday()
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {

                HolidayConnection.Open();
                string sqlText = "Update tblHoliday set Type='" + textBox1.Text.ToString() + "',Cost='" + textBox2.Text.ToString() + "',Members='" + textBox3.Text.ToString() + "',CurrencySym='" + comboBox1.SelectedItem.ToString() + "' Where ID =" + frmReservation.Globals.id + "";
                OleDbCommand AccessCommand = new OleDbCommand(sqlText, HolidayConnection);
                AccessCommand.ExecuteNonQuery();
                HolidayConnection.Close();
                MessageBox.Show("New Holiday Saved Successfully", "Message", MessageBoxButtons.OK);
                frmHolidays holiday = new frmHolidays();
                holiday.Show();
                this.Close();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateHoliday();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmHolidays holiday = new frmHolidays();
            holiday.Show();
            this.Close();
        }

        private void frmHolidayUpdater_Load(object sender, EventArgs e)
        {
            prepareUpdater();
            this.AcceptButton = button1;
        }
    }
}
