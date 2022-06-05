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
    public partial class frmReservationUpdater : Form
    {
        public frmReservationUpdater()
        {
            InitializeComponent();
        }
      
        OleDbConnection ReservationConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");

        public void prepareUpdater()
        {
            listView1.Items.Clear();

            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblReservation Where ID = @ID");
            AccessCommand.Parameters.AddWithValue("@ID", frmReservation.Globals.id);
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

                comboBox1.SelectedItem = read["FullName"].ToString();
                comboBox2.SelectedItem = read["HolidayType"].ToString();
                dateTimePicker1.Text = read["CheckInDate"].ToString();
                dateTimePicker2.Text = read["CheckOutDate"].ToString();
                textBox5.Text = read["Adults"].ToString();
                textBox6.Text = read["Children"].ToString();
                textBox1.Text = read["Total"].ToString();

            }
            ReservationConnection.Close();
        }

        private void updateReservation()
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
                    string sqlText = "Update tblReservation set FullName='" + comboBox1.SelectedItem.ToString() + "',HolidayType='" + comboBox2.SelectedItem.ToString() + "',CheckInDate='" + dateTimePicker1.Text.ToString() + "',CheckOutDate='" + dateTimePicker2.Text.ToString() + "',Adults='" + textBox5.Text.ToString() + "',Children='" + textBox6.Text.ToString() + "',Total='" + textBox1.Text.ToString() + "' Where ID ="+frmReservation.Globals.id+"";
                    OleDbCommand AccessCommand = new OleDbCommand(sqlText, ReservationConnection);
                    AccessCommand.ExecuteNonQuery();
                    ReservationConnection.Close();
                    MessageBox.Show("New Reservation Saved Successfully", "Message", MessageBoxButtons.OK);
                    frmReservation reservation = new frmReservation();
                    reservation.Show();
                    this.Close();
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
                frmReservation.Globals.members = System.Convert.ToInt32(read["Members"].ToString());
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
                frmReservation.Globals.currency = (read["CurrencySym"].ToString());
            }

            ReservationConnection.Close();
        }
        private void frmReservationUpdater_Load(object sender, EventArgs e)
        {
            prepareUpdater();
            showVacationer();
            showHoliday();
            showDiscount();
            this.AcceptButton = button3;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateReservation();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            showCurrency();

            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = ReservationConnection;
            AccessCommand.CommandText = ("Select * from tblHoliday Where Type ='" + comboBox2.Text + "'");
            OleDbDataReader read = AccessCommand.ExecuteReader();

            while (read.Read())
            {
                frmReservation.Globals.cost = System.Convert.ToInt32(read["Cost"].ToString());
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
                frmReservation.Globals.discount = System.Convert.ToInt32(read["Rate"].ToString());
            }

            ReservationConnection.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan getdate = new TimeSpan();
            getdate = dateTimePicker2.Value - dateTimePicker1.Value;
            frmReservation.Globals.days = getdate.Days;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan getdate = new TimeSpan();
            getdate = dateTimePicker2.Value - dateTimePicker1.Value;
            frmReservation.Globals.days = getdate.Days;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                this.AcceptButton = button1;
                frmReservation.Globals.total = (int)((frmReservation.Globals.days * frmReservation.Globals.cost) - ((frmReservation.Globals.days * frmReservation.Globals.cost) * (frmReservation.Globals.discount / 100)));
                textBox1.Text = (frmReservation.Globals.currency.ToString()) + (frmReservation.Globals.total.ToString());
            }
            else
            {
                MessageBox.Show("You Must Fill All Blanks To Calculate Total Cost", "Message", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmReservation reservation = new frmReservation();
            reservation.Show();
            this.Close();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            frmReservation.Globals.value = Convert.ToInt32(textBox5.Text);
            if (frmReservation.Globals.value > 0)
            {
                frmReservation.Globals.value--;
                textBox5.Text = frmReservation.Globals.value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showMembers();
            frmReservation.Globals.value = Convert.ToInt32(textBox5.Text);
            if (frmReservation.Globals.value + Convert.ToInt32(textBox6.Text) < frmReservation.Globals.members)
            {
                frmReservation.Globals.value++;
                textBox5.Text = frmReservation.Globals.value.ToString();

            }
            else if (frmReservation.Globals.value < frmReservation.Globals.members)
            {
                frmReservation.Globals.value++;
                textBox5.Text = frmReservation.Globals.value.ToString();
                frmReservation.Globals.value = Convert.ToInt32(textBox6.Text);
                frmReservation.Globals.value--;
                textBox6.Text = frmReservation.Globals.value.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmReservation.Globals.value = Convert.ToInt32(textBox6.Text);
            if (frmReservation.Globals.value > 0)
            {
                frmReservation.Globals.value--;
                textBox6.Text = frmReservation.Globals.value.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            showMembers();
            frmReservation.Globals.value = Convert.ToInt32(textBox6.Text);
            if (frmReservation.Globals.value + Convert.ToInt32(textBox5.Text) < frmReservation.Globals.members)
            {
                frmReservation.Globals.value++;
                textBox6.Text = frmReservation.Globals.value.ToString();

            }
            else if (frmReservation.Globals.value < frmReservation.Globals.members)
            {
                frmReservation.Globals.value++;
                textBox6.Text = frmReservation.Globals.value.ToString();
                frmReservation.Globals.value = Convert.ToInt32(textBox5.Text);
                frmReservation.Globals.value--;
                textBox5.Text = frmReservation.Globals.value.ToString();
            }
        }
    }
}
