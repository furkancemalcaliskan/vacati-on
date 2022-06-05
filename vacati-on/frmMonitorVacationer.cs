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
    public partial class frmMonitorVacationer : Form
    {
        public frmMonitorVacationer()
        {
            InitializeComponent();
        }

        OleDbConnection VacationerConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Furkan Çalışkan\\source\\repos\\vacati-on\\database\\vacati-on_db.accdb");

        private void showInformation()
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

        private void frmMonitorVacationer_Load(object sender, EventArgs e)
        {
            showInformation();
        }
    }
}
