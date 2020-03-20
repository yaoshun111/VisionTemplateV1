namespace HalconTest
{
    partial class HalconCircleGuageCreator
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
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // hWindowControltemp
            // 
            this.hWindowControltemp.BackColor = System.Drawing.Color.Black;
            this.hWindowControltemp.BorderColor = System.Drawing.Color.Black;
            this.hWindowControltemp.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControltemp.Location = new System.Drawing.Point(3, 3);
            this.hWindowControltemp.Name = "hWindowControltemp";
            this.hWindowControltemp.Size = new System.Drawing.Size(885, 563);
            this.hWindowControltemp.TabIndex = 0;
            this.hWindowControltemp.WindowSize = new System.Drawing.Size(885, 563);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Fuchsia;
            this.button1.Location = new System.Drawing.Point(3, 572);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开一张图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Fuchsia;
            this.button4.Location = new System.Drawing.Point(741, 579);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(132, 33);
            this.button4.TabIndex = 13;
            this.button4.Text = "开始画圆形测量工具";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(674, 585);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(38, 21);
            this.button5.TabIndex = 12;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Fuchsia;
            this.label3.Location = new System.Drawing.Point(100, 585);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "圆形工具保存路径";
            // 
            // textBox3
            // 
            this.textBox3.ForeColor = System.Drawing.Color.Fuchsia;
            this.textBox3.Location = new System.Drawing.Point(242, 585);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(426, 21);
            this.textBox3.TabIndex = 10;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // HalconCircleGuageCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hWindowControltemp);
            this.Name = "HalconCircleGuageCreator";
            this.Size = new System.Drawing.Size(894, 632);
            this.Load += new System.EventHandler(this.HalconTemplateCreator_Load);
            this.ParentChanged += new System.EventHandler(this.HalconTemplateCreator_ParentChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public HalconDotNet.HWindowControl hWindowControltemp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox3;
    }
}
