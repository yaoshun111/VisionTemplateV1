namespace VisionFram3
{
    partial class PLC扫描
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
            this.njCompolet1 = new OMRON.Compolet.CIP.NJCompolet(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ipInput1 = new IPInputControl.Ctrl.IPInput();
            this.menuButton1 = new VisionFram3.menuButton();
            this.button8 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // njCompolet1
            // 
            this.njCompolet1.Active = false;
            this.njCompolet1.ConnectionType = OMRON.Compolet.CIP.ConnectionType.UCMM;
            this.njCompolet1.DontFragment = false;
            this.njCompolet1.HeartBeatTimer = 0;
            this.njCompolet1.LocalPort = 2;
            this.njCompolet1.PeerAddress = "";
            this.njCompolet1.ReceiveTimeLimit = ((long)(750));
            this.njCompolet1.RoutePath = "";
            this.njCompolet1.UseRoutePath = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ipInput1);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 57);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP设置";
            // 
            // ipInput1
            // 
            this.ipInput1.BackColor = System.Drawing.Color.White;
            this.ipInput1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipInput1.ipText = "...";
            this.ipInput1.Location = new System.Drawing.Point(13, 22);
            this.ipInput1.Name = "ipInput1";
            this.ipInput1.Size = new System.Drawing.Size(154, 26);
            this.ipInput1.TabIndex = 7;
            // 
            // menuButton1
            // 
            this.menuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.menuButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuButton1.ControlText = "设置";
            this.menuButton1.FontColor = System.Drawing.Color.DarkBlue;
            this.menuButton1.FontSize = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuButton1.Location = new System.Drawing.Point(202, 34);
            this.menuButton1.Name = "menuButton1";
            this.menuButton1.Selected = false;
            this.menuButton1.Size = new System.Drawing.Size(67, 37);
            this.menuButton1.TabIndex = 7;
            this.menuButton1.SelectedChanged += new System.EventHandler(this.menuButton1_SelectedChanged);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(146, 88);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(123, 21);
            this.button8.TabIndex = 12;
            this.button8.Text = "PLC后电子秤1写测试";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(146, 115);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(123, 21);
            this.button11.TabIndex = 14;
            this.button11.Text = "PLC后电子秤2写测试";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(17, 232);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(123, 65);
            this.button5.TabIndex = 9;
            this.button5.Text = "PLC前电子秤2触发";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(187, 232);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(123, 65);
            this.button6.TabIndex = 11;
            this.button6.Text = "PLC后电子秤2触发";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(187, 160);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(123, 65);
            this.button7.TabIndex = 10;
            this.button7.Text = "PLC后电子秤1触发";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(17, 160);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(123, 65);
            this.button4.TabIndex = 8;
            this.button4.Text = "PLC前电子秤1触发";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(17, 115);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(123, 21);
            this.button9.TabIndex = 16;
            this.button9.Text = "PLC前电子秤2写测试";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(17, 88);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(123, 21);
            this.button10.TabIndex = 15;
            this.button10.Text = "PLC前电子秤1写测试";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(357, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // PLC扫描
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(558, 342);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.menuButton1);
            this.Controls.Add(this.groupBox1);
            this.Name = "PLC扫描";
            this.Text = "PLC扫描";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PLC扫描_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OMRON.Compolet.CIP.NJCompolet njCompolet1;
        private System.Windows.Forms.GroupBox groupBox1;
       // private IPInputControl.Ctrl.IPInput ipInput1;
        private menuButton menuButton1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button1;
    }
}