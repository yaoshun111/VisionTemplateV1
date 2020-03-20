using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastCtr
{
    public partial class NewPanel : Panel
    {
        public NewPanel()
        {
            InitializeComponent();
            
        }

        public NewPanel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public void Show(Form ctr)
        {
            if (!this.Controls.Contains(ctr))
            {
                this.Controls.Clear();
                ctr.FormBorderStyle = FormBorderStyle.None;
                ctr.Dock = DockStyle.Fill;
                ctr.TopLevel = false;
                this.Controls.Add(ctr);
                ctr.Show();
            }
        }




    }
}
