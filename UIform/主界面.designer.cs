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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 清除所有ToolStripMenuItem
            // 
            this.清除所有ToolStripMenuItem.Enabled = false;
            this.清除所有ToolStripMenuItem.Name = "清除所有ToolStripMenuItem";
            this.清除所有ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.清除所有ToolStripMenuItem.Text = "清除所有(表格和曲线）";
            this.清除所有ToolStripMenuItem.Click += new System.EventHandler(this.清除所有ToolStripMenuItem_Click);
            // 
            // 加载最后一次数据ToolStripMenuItem
            // 
            this.加载最后一次数据ToolStripMenuItem.Enabled = false;
            this.加载最后一次数据ToolStripMenuItem.Name = "加载最后一次数据ToolStripMenuItem";
            this.加载最后一次数据ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.加载最后一次数据ToolStripMenuItem.Text = "加载最后一次表格数据";
            this.加载最后一次数据ToolStripMenuItem.Click += new System.EventHandler(this.加载最后一次数据ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hWindowControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 511);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像显示";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(6, 20);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(640, 480);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(640, 480);
            // 
            // 主界面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(679, 538);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "主界面";
            this.Text = "主界面";
            this.Load += new System.EventHandler(this.主界面_Load);
            this.Shown += new System.EventHandler(this.主界面_Shown);
            this.SizeChanged += new System.EventHandler(this.主界面_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清除所有ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载最后一次数据ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private HalconDotNet.HWindowControl hWindowControl1;
    }
}