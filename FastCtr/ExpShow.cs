using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastCtr
{
    public partial class ExpShow : UserControl
    {
        public ExpShow()
        {
            InitializeComponent();
        }

        public ExpShow(object instance)
        {
            InitializeComponent();


            //PropertyGrid pg = new PropertyGrid();
            //pg.ExpandAllGridItems

        }


        private void ExpShow_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
            if ((treeView1.SelectedNode != null) && (e.Button == MouseButtons.Left))
            {

            }
        }

    }
}
