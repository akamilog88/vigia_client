using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HomeAlarm
{
    public partial class CountDownDialog : Form
    {
        private int seconds;
        public CountDownDialog(int seconds)
        {
            InitializeComponent();
            this.seconds = seconds;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            lb_seconds.Text = seconds.ToString() + "s";
            timer1.Start();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            seconds--;
            this.lb_seconds.Text = seconds.ToString() + "s";           
            if (seconds<1)
            {
                timer1.Dispose();
                this.Close();
            }
        }
    }
}
