using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Threading;

namespace VisionFram3
{
    //public delegate void EventHandler();
    public partial class menuButton : UserControl 
    {
        public event EventHandler SelectedChanged;
        public event EventHandler OnClicked;
        public event PropertyChangedEventHandler PropertyChanged;
        private Color colorSelected = Color.FromArgb(0xAE, 0xDA, 0x97);
        private Color colorUnselected = Color.FromArgb(0xEA, 0xEA, 0xEB);
        private string text;
        private bool selected = false;
        private Font fontsize;
        private Color fontColor;
        private bool Mousein;
        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }
        public menuButton()
        {
            InitializeComponent();
            //this.MaximumSize = this.Size;
            //this.MinimumSize = this.Size;
            this.BackColor = colorUnselected;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.Click += new EventHandler(MenuButton_Click);
            this.text = "text";
            fontsize = new Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            fontColor = Color.DarkBlue;
            this.Paint += this_Paint;  //给属性赋值时 触发事件
        
        }

        public void SetBackColor(Color colorSelected, Color colorUnselected)
        {
            this.colorSelected = colorSelected;
            this.colorUnselected = colorUnselected;
        }
        public void IsSelect(bool isSelect)
        {
            Selected = isSelect;
        }

        
        public bool Selected
        {
            get { return this.selected; }
            set 
            {
                if (value == selected)
                {
                    return;
                }
                this.selected = value;
                //this.BackColor = selected ? colorSelected : colorUnselected;
                if (this.Selected == true)
                {
                    this.BackColor = MyColor.LightRed;
                    Application.DoEvents();
                }
                else
                {
                    if (Mousein)
                    {
                        this.BackColor = MyColor.Green;

                    }
                    else
                    {
                        this.BackColor = MyColor.None;
                    }
                }
                if (Selected == true)
                {
                    if (SelectedChanged != null)
                    {
                        SelectedChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
        public Font FontSize
        {
            get { return fontsize; }
            set { fontsize = value; 
                this.Invalidate(); }
        }

        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; this.Invalidate(); }
        }
          

        public string ControlText
        {
            get { return this.text; }
            set
            {
                this.text = value;
                this.Invalidate();
            }
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {

            Brush p = new SolidBrush(fontColor);
            Graphics graphics = CreateGraphics();
            SizeF sizeF = graphics.MeasureString(text, fontsize);
            e.Graphics.DrawString(this.text, this.fontsize, p, (this.Width - sizeF.Width) / 2, (this.Height - sizeF.Height) / 2);
        } 
       
    
        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (!Selected)
                Selected = true;
            //Selected = false;
            //Selected = !this.selected;
            if (OnClicked != null)
            {
                OnClicked(sender, e);
            }
        }

       
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void MenuButton_MouseEnter(object sender, EventArgs e)
        {
            Mousein = true;
            if (this.Selected == false)
            {
                this.BackColor = MyColor.Green;
            }
        }

        private void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            Mousein = false;
            if (this.Selected == false)
            {
                this.BackColor = MyColor.None;
            }
        }

        public  bool Enable
        {
            set { if (value) { this.Enabled = true; this.BackColor = SystemColors.Control; } else { this.Enabled = false; this.BackColor = SystemColors.ControlDark; } }
        }

    }
}
