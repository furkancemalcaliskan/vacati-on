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
    public partial class frmMonitorHoliday : Form
    {
        public frmMonitorHoliday()
        {
            InitializeComponent();
        }
        OleDbConnection HolidayConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");
        private void showInformation()
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
        private void frmMonitorHoliday_Load(object sender, EventArgs e)
        {
            showInformation();
        }
    }
}
