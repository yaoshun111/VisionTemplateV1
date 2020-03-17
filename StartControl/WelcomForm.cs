using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartControl
{
    public partial class Welcom : Form
    {
        
        public Welcom()
        {
            InitializeComponent();
            Opacity = 0;
            this.loadingCircle1.StylePreset = ZZX.MV.CONTROLEX.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.InnerCircleRadius = 30;
            this.loadingCircle1.OuterCircleRadius = 50;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.NumberSpoke = 15;
            this.loadingCircle1.SpokeThickness = 6;
            this.loadingCircle1.Color = Color.Green;
        }

        private void loading_Load(object sender, EventArgs e)
        {
            this.loadingCircle1.Active = true;
            timer1.Start();
            timer2.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity >= 0.8)
            {
                timer1.Stop();
            }
            else
            {
                Opacity += 0.2;
            }
        }

        public  void Start()
        {
            Task task = new Task(new Action(() =>
            {
                this.ShowDialog();
            }));
            task.Start();
        }

        public void Stop()
        {
            this.Invoke(new Action(() =>
                {
                    this.Close();
                }));
        }
        int a = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (a)
            {
                case 0:
                    label1.Text = "Loading";
                    a++;
                    break;
                case 1:
                    label1.Text = "Loading.";
                    a++;
                    break;
                case 2:
                    label1.Text = "Loading..";
                    a++;
                    break;
                case 3:
                    label1.Text = "Loading...";
                    a = 0;
                    break;
            }
        }
    }
}
