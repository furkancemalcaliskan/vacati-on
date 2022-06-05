
namespace vacati_on
{
    partial class frmMonitorReservation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CheckInDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CheckOutDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumberOfDays = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Adults = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Children = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.FullName,
            this.Type,
            this.CheckInDate,
            this.CheckOutDate,
            this.NumberOfDays,
            this.Adults,
            this.Children,
            this.Total});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(860, 426);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 43;
            // 
            // FullName
            // 
            this.FullName.Text = "Name";
            this.FullName.Width = 146;
            // 
            // Type
            // 
            this.Type.Text = "Holiday Type";
            this.Type.Width = 87;
            // 
            // CheckInDate
            // 
            this.CheckInDate.Text = "Check In Date";
            this.CheckInDate.Width = 92;
            // 
            // CheckOutDate
            // 
            this.CheckOutDate.Text = "Check Out Date";
            this.CheckOutDate.Width = 102;
            // 
            // NumberOfDays
            // 
            this.NumberOfDays.Text = "Number of Days";
            this.NumberOfDays.Width = 90;
            // 
            // Adults
            // 
            this.Adults.Text = "No. of Adults";
            this.Adults.Width = 83;
            // 
            // Children
            // 
            this.Children.Text = "No. of Children";
            this.Children.Width = 90;
            // 
            // Total
            // 
            this.Total.Text = "Total";
            this.Total.Width = 122;
            // 
            // frmMonitorReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 450);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMonitorReservation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vacati-on - Reservation Monitor";
            this.Load += new System.EventHandler(this.frmMonitorReservation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader FullName;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader CheckInDate;
        private System.Windows.Forms.ColumnHeader CheckOutDate;
        private System.Windows.Forms.ColumnHeader NumberOfDays;
        private System.Windows.Forms.ColumnHeader Adults;
        private System.Windows.Forms.ColumnHeader Children;
        private System.Windows.Forms.ColumnHeader Total;
    }
}