namespace VisionFram3
{
    partial class 存数据库
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
            this.menuButton1 = new VisionFram3.menuButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // menuButton1
            // 
            this.menuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.menuButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuButton1.ControlText = "测试";
            this.menuButton1.FontSize = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuButton1.Location = new System.Drawing.Point(102, 71);
            this.menuButton1.Name = "menuButton1";
            this.menuButton1.Selected = false;
            this.menuButton1.Size = new System.Drawing.Size(67, 67);
            this.menuButton1.TabIndex = 1;
            this.menuButton1.SelectedChanged += new System.EventHandler(this.menuButton1_SelectedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 存数据库
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 346);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuButton1);
            this.Name = "存数据库";
            this.Text = "存数据库";
            this.Load += new System.EventHandler(this.存数据库_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private menuButton menuButton1;
        private System.Windows.Forms.Button button1;

    }
}