using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HomeAlarm
{
    public partial class SensorUserCtrl : UserControl
    {
        public event EventHandler onActiveChange;

        public SensorUserCtrl()
        {
            InitializeComponent();
        }

        private String name;
        private bool active;
        private bool fired;

        public String DeviceType { get; set; }

        public String Code
        {
            get;
            set;
        }
        public String Name
        {
            get
            { 
                return this.name;
            }
            set 
            {
                 this.name = value;                 
                 this.chk_device.Text = value;
            }
        }
        public bool Active
        {
            get
            {
                return this.active;
            }
            set
            {
                this.active = value;
                this.chk_device.Checked = value;
                if(value)                    
                    this.pictureBox1.Image = HomeAlarm.Resource.green_led;
                else
                    this.pictureBox1.Image = HomeAlarm.Resource.gray_led;
            }
        }
        public String Message
        {
            get;
            set;
        }
        public String NormalizedMessage
        {
            get;
            set;
        }
        public bool Fired
        {
            get
            {
                return this.fired;
            }
            set
            {
                this.fired = value;
                if (value)
                {
                    this.pictureBox1.Image = HomeAlarm.Resource.red_led;
                    this.chk_device.BackColor = Color.HotPink;
                }
                else
                {
                    this.pictureBox1.Image = HomeAlarm.Resource.green_led;
                    this.chk_device.BackColor = SystemColors.ControlDark;
                }
            }
        }
        
        private void chk_device_CheckedChanged(object sender, EventArgs e)
        {
            Active = chk_device.Checked;
            if (onActiveChange != null)
            {
                onActiveChange.Invoke(this, null);
            }
            if (Active)
            {
                Fired = false;
            }
        }
    }
}
