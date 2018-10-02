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
    public partial class FilterDialog : Form
    {
        public DateTime FechInicio { get; private set; }
        public DateTime FechFin { get; private set; }
        public String Message { get; private set; }

        public bool Accepted { get; private set; }

        public FilterDialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FechInicio = this.monthCalendar1.SelectionStart;
            FechFin = this.monthCalendar1.SelectionEnd;   
            Accepted = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            Accepted = false;
            this.Close();
        }
    }
}
