namespace FastCtr
{
    partial class ANDweigh
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRealTimeWeight = new System.Windows.Forms.Button();
            this.btnStableTimeWeight = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSwitchOn = new System.Windows.Forms.Button();
            this.btnSwitchOff = new System.Windows.Forms.Button();
            this.btnSerialPortSetting = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(98, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "AND称重器";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(213, 14);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(71, 36);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "清零";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnRealTimeWeight
            // 
            this.btnRealTimeWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRealTimeWeight.FlatAppearance.BorderSize = 0;
            this.btnRealTimeWeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealTimeWeight.Location = new System.Drawing.Point(19, 14);
            this.btnRealTimeWeight.Name = "btnRealTimeWeight";
            this.btnRealTimeWeight.Size = new System.Drawing.Size(71, 36);
            this.btnRealTimeWeight.TabIndex = 2;
            this.btnRealTimeWeight.Text = "实时称重";
            this.btnRealTimeWeight.UseVisualStyleBackColor = false;
            this.btnRealTimeWeight.Click += new System.EventHandler(this.btnRealTimeWeight_Click);
            // 
            // btnStableTimeWeight
            // 
            this.btnStableTimeWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnStableTimeWeight.FlatAppearance.BorderSize = 0;
            this.btnStableTimeWeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStableTimeWeight.Location = new System.Drawing.Point(119, 14);
            this.btnStableTimeWeight.Name = "btnStableTimeWeight";
            this.btnStableTimeWeight.Size = new System.Drawing.Size(71, 36);
            this.btnStableTimeWeight.TabIndex = 3;
            this.btnStableTimeWeight.Text = "稳定称重";
            this.btnStableTimeWeight.UseVisualStyleBackColor = false;
            this.btnStableTimeWeight.Click += new System.EventHandler(this.btnStableTimeWeight_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(28, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(175, 28);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "实时值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "稳定值";
            // 
            // btnSwitchOn
            // 
            this.btnSwitchOn.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSwitchOn.FlatAppearance.BorderSize = 0;
            this.btnSwitchOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchOn.ForeColor = System.Drawing.Color.Black;
            this.btnSwitchOn.Location = new System.Drawing.Point(19, 94);
            this.btnSwitchOn.Name = "btnSwitchOn";
            this.btnSwitchOn.Size = new System.Drawing.Size(43, 26);
            this.btnSwitchOn.TabIndex = 8;
            this.btnSwitchOn.Text = "开机";
            this.btnSwitchOn.UseVisualStyleBackColor = false;
            this.btnSwitchOn.Click += new System.EventHandler(this.btnSwitchOn_Click);
            // 
            // btnSwitchOff
            // 
            this.btnSwitchOff.BackColor = System.Drawing.Color.Red;
            this.btnSwitchOff.FlatAppearance.BorderSize = 0;
            this.btnSwitchOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchOff.Location = new System.Drawing.Point(68, 94);
            this.btnSwitchOff.Name = "btnSwitchOff";
            this.btnSwitchOff.Size = new System.Drawing.Size(43, 26);
            this.btnSwitchOff.TabIndex = 9;
            this.btnSwitchOff.Text = "关机";
            this.btnSwitchOff.UseVisualStyleBackColor = false;
            this.btnSwitchOff.Click += new System.EventHandler(this.btnSwitchOff_Click);
            // 
            // btnSerialPortSetting
            // 
            this.btnSerialPortSetting.BackColor = System.Drawing.Color.Blue;
            this.btnSerialPortSetting.FlatAppearance.BorderSize = 0;
            this.btnSerialPortSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerialPortSetting.Location = new System.Drawing.Point(241, 73);
            this.btnSerialPortSetting.Name = "btnSerialPortSetting";
            this.btnSerialPortSetting.Size = new System.Drawing.Size(40, 32);
            this.btnSerialPortSetting.TabIndex = 10;
            this.btnSerialPortSetting.Text = "串口配置";
            this.btnSerialPortSetting.UseVisualStyleBackColor = false;
            this.btnSerialPortSetting.Click += new System.EventHandler(this.btnSerialPortSetting_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "当前串口：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "已关闭";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 41);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnStableTimeWeight);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.btnRealTimeWeight);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnSwitchOn);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnSwitchOff);
            this.panel2.Controls.Add(this.btnSerialPortSetting);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 131);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 68);
            this.panel3.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 68);
            this.panel4.TabIndex = 14;
            // 
            // ANDweigh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ANDweigh";
            this.Size = new System.Drawing.Size(301, 240);
            this.Load += new System.EventHandler(this.ANDweigh_Load);
            this.ParentChanged += new System.EventHandler(this.ANDweigh_ParentChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRealTimeWeight;
        private System.Windows.Forms.Button btnStableTimeWeight;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSwitchOn;
        private System.Windows.Forms.Button btnSwitchOff;
        private System.Windows.Forms.Button btnSerialPortSetting;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}
