namespace UIform
{
    partial class 主界面
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清除所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载最后一次数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Enabled = false;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除所有ToolStripMenuItem,
            this.加载最后一次数据ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(201, 48);
            // 
            // 清除所有ToolStripMenuItem
            // 
            this.清除所有ToolStripMenuItem.Enabled = false;
            this.清除所有ToolStripMenuItem.Name = "清除所有ToolStripMenuItem";
            this.清除所有ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.清除所有ToolStripMenuItem.Text = "清除所有(表格和曲线）";
            // 
            // 加载最后一次数据ToolStripMenuItem
            // 
            this.加载最后一次数据ToolStripMenuItem.Enabled = false;
            this.加载最后一次数据ToolStripMenuItem.Name = "加载最后一次数据ToolStripMenuItem";
            this.加载最后一次数据ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.加载最后一次数据ToolStripMenuItem.Text = "加载最后一次表格数据";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Location = new System.Drawing.Point(132, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 396);
            this.panel1.TabIndex = 1;
            // 
            // 主界面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(717, 588);
            this.Controls.Add(this.panel1);
            this.Name = "主界面";
            this.Text = "主界面";
            this.Load += new System.EventHandler(this.主界面_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清除所有ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载最后一次数据ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}