namespace HalconTest
{
    partial class HalconLineGuageCreator
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
            this.hWindowControltemp = new HalconDotNet.HWindowControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PathtextBox = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.transCom = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.IgnoreTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.calipNumTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.thresholdTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.widthTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.heightTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControltemp
            // 
            this.hWindowControltemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hWindowControltemp.BackColor = System.Drawing.Color.Black;
            this.hWindowControltemp.BorderColor = System.Drawing.Color.Black;
            this.hWindowControltemp.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControltemp.Location = new System.Drawing.Point(3, 3);
            this.hWindowControltemp.Name = "hWindowControltemp";
            this.hWindowControltemp.Size = new System.Drawing.Size(885, 515);
            this.hWindowControltemp.TabIndex = 0;
            this.hWindowControltemp.WindowSize = new System.Drawing.Size(885, 515);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Fuchsia;
            this.button1.Location = new System.Drawing.Point(9, 658);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 49);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开一张图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(859, 520);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 21);
            this.button2.TabIndex = 8;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Fuchsia;
            this.label2.Location = new System.Drawing.Point(6, 521);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "直线工具保存路径";
            // 
            // PathtextBox
            // 
            this.PathtextBox.ForeColor = System.Drawing.Color.Fuchsia;
            this.PathtextBox.Location = new System.Drawing.Point(113, 521);
            this.PathtextBox.Name = "PathtextBox";
            this.PathtextBox.Size = new System.Drawing.Size(740, 21);
            this.PathtextBox.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Fuchsia;
            this.button3.Location = new System.Drawing.Point(161, 658);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(713, 64);
            this.button3.TabIndex = 9;
            this.button3.Text = "开始画直线测量工具";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.transCom);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.IgnoreTxt);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.calipNumTxt);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.thresholdTxt);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.widthTxt);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.heightTxt);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Location = new System.Drawing.Point(9, 572);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(686, 78);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "卡尺设置";
            // 
            // transCom
            // 
            this.transCom.FormattingEnabled = true;
            this.transCom.Location = new System.Drawing.Point(520, 52);
            this.transCom.Name = "transCom";
            this.transCom.Size = new System.Drawing.Size(100, 20);
            this.transCom.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(451, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "卡尺极性";
            // 
            // IgnoreTxt
            // 
            this.IgnoreTxt.Location = new System.Drawing.Point(520, 22);
            this.IgnoreTxt.Name = "IgnoreTxt";
            this.IgnoreTxt.Size = new System.Drawing.Size(100, 21);
            this.IgnoreTxt.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(451, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "忽略点数";
            // 
            // calipNumTxt
            // 
            this.calipNumTxt.Location = new System.Drawing.Point(305, 47);
            this.calipNumTxt.Name = "calipNumTxt";
            this.calipNumTxt.Size = new System.Drawing.Size(100, 21);
            this.calipNumTxt.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(236, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "卡尺数量";
            // 
            // thresholdTxt
            // 
            this.thresholdTxt.Location = new System.Drawing.Point(305, 22);
            this.thresholdTxt.Name = "thresholdTxt";
            this.thresholdTxt.Size = new System.Drawing.Size(100, 21);
            this.thresholdTxt.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(236, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "边缘阈值";
            // 
            // widthTxt
            // 
            this.widthTxt.Location = new System.Drawing.Point(89, 49);
            this.widthTxt.Name = "widthTxt";
            this.widthTxt.Size = new System.Drawing.Size(100, 21);
            this.widthTxt.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "卡尺宽度";
            // 
            // heightTxt
            // 
            this.heightTxt.Location = new System.Drawing.Point(89, 22);
            this.heightTxt.Name = "heightTxt";
            this.heightTxt.Size = new System.Drawing.Size(100, 21);
            this.heightTxt.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "卡尺高度";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(723, 617);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 35);
            this.button4.TabIndex = 17;
            this.button4.Text = "保存参数到本地";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(723, 580);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 35);
            this.button5.TabIndex = 18;
            this.button5.Text = "确认";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Fuchsia;
            this.label1.Location = new System.Drawing.Point(6, 548);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "卡尺参数保存路径";
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.Color.Fuchsia;
            this.textBox1.Location = new System.Drawing.Point(113, 545);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(740, 21);
            this.textBox1.TabIndex = 19;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // HalconLineGuageCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PathtextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hWindowControltemp);
            this.Name = "HalconLineGuageCreator";
            this.Size = new System.Drawing.Size(894, 725);
            this.Load += new System.EventHandler(this.HalconTemplateCreator_Load);
            this.ParentChanged += new System.EventHandler(this.HalconTemplateCreator_ParentChanged);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public HalconDotNet.HWindowControl hWindowControltemp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox PathtextBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox transCom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox IgnoreTxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox calipNumTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox thresholdTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox widthTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox heightTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
    }
}
