using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionFram3
{
    public partial class SwitchButton : UserControl
    {
        public event Action ON;
        public event Action OFF;
        public event Action<SwitchButton> Trigger;
        private Color colorON = MyColor.Green;
        private Color colorOFF = MyColor.None;
        private bool sts;

        public event EventHandler TextChange;
        public SwitchButton()
        {
            InitializeComponent();
            this.button1.Text = this.Name;

            this.button1.TextChanged += button1_TextChanged;
        }

        void button1_TextChanged(object sender, EventArgs e)
        {
            if (TextChange != null)
                TextChange(sender, e);
        }

        public void SetText(string text)
        {
            this.button1.Text = text;
        }

        public void AddDataSource(Binding source)
        {
            this.button1.DataBindings.Add(source);
        }

        public void SetColor(Color colorON, Color colorOFF)
        {
            this.colorON = colorON;
            this.colorOFF = colorOFF;
        }

        public void SetColor(Color color)
        {
            this.button1.BackColor = color;
        }

        public bool Locked { get; set; }

        public bool STS
        {
            get { return sts; }
            set 
            {
                this.sts = value;
                if (this.sts == true)
                {
                    this.button1.BackColor = colorON;
                    if (ON != null)
                    {
                        ON();
                    }
                }
                else
                {
                    this.button1.BackColor = colorOFF;
                    if (OFF != null)
                    {
                        OFF();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Locked)
            {
                return;
            }
            if (this.sts == true)
            {
                STS = false;
            }
            else
            {
                STS = true;
            }
            if (Trigger != null)
            {
                Trigger(this);
            }
        }

        private void SwitchButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = colorON;
        }

    }
}
