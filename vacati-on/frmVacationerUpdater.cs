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
    public partial class frmVacationerUpdater : Form
    {
        public frmVacationerUpdater()
        {
            InitializeComponent();
        }

        OleDbConnection VacationerConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");

        public void prepareUpdater()
        {
            listView1.Items.Clear();

            VacationerConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = VacationerConnection;
            AccessCommand.CommandText = ("Select * from tblVacationer Where ID = @ID");
            AccessCommand.Parameters.AddWithValue("@ID", frmReservation.Globals.id);
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

                textBox1.Text = read["FirstName"].ToString();
                textBox2.Text = read["LastName"].ToString();
                textBox3.Text = read["Address"].ToString();
                textBox4.Text = read["ContactNumber"].ToString();
                comboBox1.SelectedItem = read["Gender"].ToString();
                textBox5.Text = read["Email"].ToString();

            }
            VacationerConnection.Close();
        }

        private void updateVacationer()
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                
                VacationerConnection.Open();
                string sqlText = "Update tblVacationer set FirstName='" + textBox1.Text.ToString() + "',LastName='" + textBox2.Text.ToString() + "',Address='" + textBox3.Text.ToString() + "',ContactNumber='" + textBox4.Text.ToString() + "',Gender='" + comboBox1.SelectedItem.ToString() + "',Email='" + textBox5.Text.ToString() + "' Where ID ="+frmReservation.Globals.id+"";
                OleDbCommand AccessCommand = new OleDbCommand(sqlText, VacationerConnection);
                AccessCommand.ExecuteNonQuery();
                VacationerConnection.Close();
                MessageBox.Show("New vacati-oner Saved Successfully", "Message", MessageBoxButtons.OK);
                frmVacationer vacationer = new frmVacationer();
                vacationer.Show();
                this.Close();
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateVacationer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmVacationer vacationer = new frmVacationer();
            vacationer.Show();
            this.Close();
        }

        private void frmVacationerUpdater_Load(object sender, EventArgs e)
        {
            prepareUpdater();
            this.AcceptButton = button1;
        }
    }
}
