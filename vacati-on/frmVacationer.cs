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
    public partial class frmVacationer : Form
    {
        public frmVacationer()
        {
            InitializeComponent();
        }

        OleDbConnection VacationerConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");
        private void showInformation()
        {
            listView1.Items.Clear();
            VacationerConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = VacationerConnection;
            AccessCommand.CommandText = ("Select * from tblVacationer");
            OleDbDataReader read = AccessCommand.ExecuteReader();
            while (read.Read())
            {
                ListViewItem addNew = new ListViewItem();

                addNew.Text = read["ID"].ToString();
                addNew.SubItems.Add(read["FirstName"].ToString());
                addNew.SubItems.Add(read["LastName"].ToString());
                addNew.SubItems.Add(read["Address"].ToString());
                addNew.SubItems.Add(read["ContactNumber"].ToString());
                addNew.SubItems.Add(read["Gender"].ToString());
                addNew.SubItems.Add(read["Email"].ToString());

                listView1.Items.Add(addNew);
            }
            VacationerConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                VacationerConnection.Open();
                string sqlText = "INSERT INTO tblVacationer (FirstName,LastName,Address,ContactNumber,Gender,Email) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + comboBox1.SelectedItem.ToString() + "','" + textBox5.Text.ToString() + "')";
                OleDbCommand AccessCommand = new OleDbCommand(sqlText, VacationerConnection);
                AccessCommand.ExecuteNonQuery();
                VacationerConnection.Close();
                MessageBox.Show("New vacati-oner Saved Successfully", "Message", MessageBoxButtons.OK);
                showInformation();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                comboBox1.Refresh();
                textBox5.Clear();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                e.Cancel = true;
                errorProvider4.SetError(textBox4, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(textBox4, null);
            }
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                e.Cancel = true;
                errorProvider5.SetError(comboBox1, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider5.SetError(comboBox1, null);
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                e.Cancel = true;
                errorProvider6.SetError(textBox5, "Fill in the blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider6.SetError(textBox5, null);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            VacationerConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand(); AccessCommand.Connection = VacationerConnection; AccessCommand.CommandText = ("Select * from tblVacationer"); OleDbDataReader read = AccessCommand.ExecuteReader();
            while (read.Read())
            {
                ListViewItem addNew = new ListViewItem();

                addNew.Text = read["ID"].ToString();
                addNew.SubItems.Add(read["FirstName"].ToString());
                addNew.SubItems.Add(read["LastName"].ToString());
                addNew.SubItems.Add(read["Address"].ToString());
                addNew.SubItems.Add(read["ContactNumber"].ToString());
                addNew.SubItems.Add(read["Gender"].ToString());
                addNew.SubItems.Add(read["Email"].ToString());

                listView1.Items.Add(addNew);
            }
            VacationerConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                VacationerConnection.Open();
                OleDbCommand AccessCommand = new OleDbCommand();
                AccessCommand.Connection = VacationerConnection;

                AccessCommand.CommandText = ("Delete from tblVacationer Where ID = @ID");
                AccessCommand.Parameters.AddWithValue("@ID", listView1.SelectedItems[0].SubItems[0].Text);
                AccessCommand.ExecuteNonQuery();
                VacationerConnection.Close();
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

        private void frmVacationer_Load(object sender, EventArgs e)
        {
            showInformation();
            this.AcceptButton = button1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmVacationerUpdater updater = new frmVacationerUpdater();
            updater.Show();
            this.Close();
        }
    }
}
