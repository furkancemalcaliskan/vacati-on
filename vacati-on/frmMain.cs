using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vacati_on
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void AboutUs_Click(object sender, EventArgs e)
        {
            frmAboutUs aboutUs = new frmAboutUs();
            aboutUs.Show();

        }

    
        private void Reservation_Click(object sender, EventArgs e)
        {
            frmReservation reservation = new frmReservation();
            reservation.Show();
        }

        private void Holidays_Click(object sender, EventArgs e)
        {
            frmHolidays holidays = new frmHolidays();
            holidays.Show();
        }

        private void Discount_Click(object sender, EventArgs e)
        {
            frmDiscount discount = new frmDiscount();
            discount.Show();

        }

        private void Vacationer_Click(object sender, EventArgs e)
        {
            frmVacationer vacationer = new frmVacationer();
            vacationer.Show();
        }

        private void vacationerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonitorVacationer vacationer = new frmMonitorVacationer();
            vacationer.Show();
        }

        private void reservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonitorReservation reservation = new frmMonitorReservation();
            reservation.Show();
        }

        private void holidayListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonitorHoliday holiday = new frmMonitorHoliday();
            holiday.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are You Sure About Closing The Program?", "Message", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            
        }
    }
}
