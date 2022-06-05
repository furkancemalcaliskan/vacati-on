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
    public partial class frmReservation : Form
    {
        
        public frmReservation()
        {
            InitializeComponent();
        }
        public static class Globals
        {
            public static int total, days, cost, members, value;
            public static string currency, id;
            public static double discount;
        }
        OleDbConnection ReservationConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");
        private void showInformation()
        {
            listView1.Items.Clear();
            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblReservation");
            OleDbDataReader read = AccessCommand.ExecuteReader();
            while (read.Read())
            {
                ListViewItem addNew = new ListViewItem();

                addNew.Text = read["ID"].ToString();
                addNew.SubItems.Add(read["FullName"].ToString());
                addNew.SubItems.Add(read["HolidayType"].ToString());
                addNew.SubItems.Add(read["CheckInDate"].ToString());
                addNew.SubItems.Add(read["CheckOutDate"].ToString());
                addNew.SubItems.Add(read["NumberOfDays"].ToString());
                addNew.SubItems.Add(read["Adults"].ToString());
                addNew.SubItems.Add(read["Children"].ToString());
                addNew.SubItems.Add(read["Total"].ToString());

                listView1.Items.Add(addNew);
            }
            ReservationConnection.Close();
        }
        

        private void saveReservation()
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {

                    MessageBox.Show("You Must Calculate Total Cost To Reserve", "Message", MessageBoxButtons.OK);
                } 
                else 
                {
                    ReservationConnection.Open();
                    string sqlText = "INSERT INTO tblReservation (FullName,HolidayType,CheckInDate,CheckOutDate,Adults,Children,Total) values ('" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "','" + dateTimePicker1.Text.ToString() + "','" + dateTimePicker2.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + textBox1.Text.ToString() + "')";
                    OleDbCommand AccessCommand = new OleDbCommand(sqlText, ReservationConnection);
                    AccessCommand.ExecuteNonQuery();
                    ReservationConnection.Close();
                    MessageBox.Show("New Reservation Saved Successfully", "Message", MessageBoxButtons.OK);
                    showInformation();
                    comboBox1.Refresh();
                    comboBox2.Refresh();
                    comboBox3.Refresh();
                    textBox5.Text = "0";
                    textBox6.Text = "0";
                }

            }
        }

        public void showVacationer()
        {

            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblVacationer");
            OleDbDataReader read = AccessCommand.ExecuteReader();
            comboBox1.Items.Clear();
            while (read.Read())
            {
                string fullName;
                fullName = (read["FirstName"].ToString()) + " " + (read["LastName"].ToString());
                comboBox1.Items.Add(fullName);
            }
            ReservationConnection.Close();

        }

        public void showHoliday()
        {

            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblHoliday");
            OleDbDataReader read = AccessCommand.ExecuteReader();
            comboBox2.Items.Clear();
            while (read.Read())
            {
                comboBox2.Items.Add(read["Type"].ToString());
                
            }
            ReservationConnection.Close();

        }

        public void showDiscount()
        {

            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblDiscount");
            OleDbDataReader read = AccessCommand.ExecuteReader();
            comboBox3.Items.Clear();
            while (read.Read())
            {
                comboBox3.Items.Add(read["Type"].ToString());
         
            }
            ReservationConnection.Close();

        }

        public void showMembers()
        {
            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblHoliday Where Type ='" + comboBox2.Text + "'");
            OleDbDataReader read = AccessCommand.ExecuteReader();

            while (read.Read())
            {
                Globals.members = System.Convert.ToInt32(read["Members"].ToString());
            }

            ReservationConnection.Close();
        }

        public void showCurrency()
        {
            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblHoliday Where Type ='" + comboBox2.Text + "'");
            OleDbDataReader read = AccessCommand.ExecuteReader();

            while (read.Read())
            {
                Globals.currency = (read["CurrencySym"].ToString());
            }

            ReservationConnection.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReservation_Load(object sender, EventArgs e)
        {
            showInformation();
            showVacationer();
            showHoliday();
            showDiscount();
            this.AcceptButton = button3;
            textBox5.Text = "0";
            textBox6.Text = "0";
            DateTime time = DateTime.Now;
            dateTimePicker1.MinDate = time;
            dateTimePicker1.MaxDate = time.AddYears(1);
            dateTimePicker1.Text = time.AddHours(5).ToString();
            dateTimePicker2.MinDate = dateTimePicker1.Value.Date.AddDays(1);
            dateTimePicker2.MaxDate = dateTimePicker1.Value.Date.AddYears(1);
            dateTimePicker2.Text = time.AddDays(1).AddHours(5).ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveReservation();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            showCurrency();

            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblHoliday Where Type ='"+comboBox2.Text+"'");
            OleDbDataReader read = AccessCommand.ExecuteReader();

            while (read.Read())
            {
                Globals.cost = System.Convert.ToInt32(read["Cost"].ToString());
            }
            
            ReservationConnection.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblDiscount Where Type ='" + comboBox3.Text + "'");
            OleDbDataReader read = AccessCommand.ExecuteReader();

            while (read.Read())
            {
                Globals.discount = System.Convert.ToInt32(read["Rate"].ToString());
            }

            ReservationConnection.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan getdate = new TimeSpan();
            getdate = dateTimePicker2.Value - dateTimePicker1.Value;
            Globals.days = getdate.Days;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan getdate = new TimeSpan();
            getdate = dateTimePicker2.Value - dateTimePicker1.Value;
            Globals.days = getdate.Days;            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                this.AcceptButton = button1;
                Globals.total = (int)((Globals.days * Globals.cost) - ((Globals.days * Globals.cost) * (Globals.discount / 100)));
                textBox1.Text = (Globals.currency.ToString()) + (Globals.total.ToString());
            }
            else 
            {
                MessageBox.Show("You Must Fill All Blanks To Calculate Total Cost", "Message", MessageBoxButtons.OK);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            
            AccessCommand.CommandText = ("Delete from tblReservation Where ID = @ID");
            AccessCommand.Parameters.AddWithValue("@ID", listView1.SelectedItems[0].SubItems[0].Text);
            AccessCommand.ExecuteNonQuery();
            ReservationConnection.Close();
            button4.Enabled = false;
            button9.Enabled = false;

            showInformation();

        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox1, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox1, null);
            }
        }

        private void comboBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                e.Cancel = false;
                errorProvider2.SetError(comboBox2, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(comboBox2, null);
            }
        }

        private void comboBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                e.Cancel = false;
                errorProvider3.SetError(comboBox3, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(comboBox3, null);
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                e.Cancel = false;
                errorProvider4.SetError(textBox5, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(textBox5, null);
            }
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                e.Cancel = false;
                errorProvider5.SetError(textBox6, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider5.SetError(textBox6, null);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Globals.value = Convert.ToInt32(textBox5.Text);
            if(Globals.value > 0)
            {
                Globals.value--;
                textBox5.Text = Globals.value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showMembers();
            Globals.value = Convert.ToInt32(textBox5.Text);
            if (Globals.value + Convert.ToInt32(textBox6.Text) < Globals.members)
            {
                Globals.value++;
                textBox5.Text = Globals.value.ToString();
                
            }
            else if (Globals.value <Globals.members)
            {
                Globals.value++;
                textBox5.Text = Globals.value.ToString();
                Globals.value = Convert.ToInt32(textBox6.Text);
                Globals.value--;
                textBox6.Text = Globals.value.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Globals.value = Convert.ToInt32(textBox6.Text);
            if (Globals.value > 0)
            {
                Globals.value--;
                textBox6.Text = Globals.value.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            showMembers();
            Globals.value = Convert.ToInt32(textBox6.Text);
            if (Globals.value + Convert.ToInt32(textBox5.Text) < Globals.members)
            {
                Globals.value++;
                textBox6.Text = Globals.value.ToString();

            }
            else if (Globals.value < Globals.members)
            {
                Globals.value++;
                textBox6.Text = Globals.value.ToString();
                Globals.value = Convert.ToInt32(textBox5.Text);
                Globals.value--;
                textBox5.Text = Globals.value.ToString();
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            button9.Enabled = true;
            Globals.id = listView1.SelectedItems[0].SubItems[0].Text.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmReservationUpdater updater = new frmReservationUpdater();
            updater.Show();
            this.Close();
        }

        
    }
}

