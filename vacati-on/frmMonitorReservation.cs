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
    public partial class frmMonitorReservation : Form
    {
        public frmMonitorReservation()
        {
            InitializeComponent();
        }

        OleDbConnection ReservationConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\vacati-on_db.accdb");
        private void showInformation()
        {
            listView1.Items.Clear();
            ReservationConnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand(); AccessCommand.Connection = ReservationConnection; AccessCommand.CommandText = ("Select * from tblReservation"); OleDbDataReader read = AccessCommand.ExecuteReader();
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

        private void frmMonitorReservation_Load(object sender, EventArgs e)
        {
            showInformation();
        }
    }
}
