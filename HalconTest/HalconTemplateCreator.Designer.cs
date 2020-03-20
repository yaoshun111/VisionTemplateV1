namespace HalconTest
{
    partial class HalconTemplateCreator
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
            this.btDraw = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSaveTemplate = new System.Windows.Forms.Button();
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
            // btDraw
            // 
            this.btDraw.ForeColor = System.Drawing.Color.Fuchsia;
            this.btDraw.Location = new System.Drawing.Point(741, 572);
            this.btDraw.Name = "btDraw";
            this.btDraw.Size = new System.Drawing.Size(132, 33);
            this.btDraw.TabIndex = 0;
            this.btDraw.Text = "开始画模板";
            this.btDraw.UseVisualStyleBackColor = true;
            this.btDraw.Click += new System.EventHandler(this.btDraw_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Fuchsia;
            this.button1.Location = new System.Drawing.Point(3, 572);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开一张图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.Color.Fuchsia;
            this.textBox1.Location = new System.Drawing.Point(242, 578);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(426, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Fuchsia;
            this.label1.Location = new System.Drawing.Point(132, 583);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "模板保存路径";
            // 
            // btSaveTemplate
            // 
            this.btSaveTemplate.Location = new System.Drawing.Point(674, 577);
            this.btSaveTemplate.Name = "btSaveTemplate";
            this.btSaveTemplate.Size = new System.Drawing.Size(38, 21);
            this.btSaveTemplate.TabIndex = 5;
            this.btSaveTemplate.Text = "...";
            this.btSaveTemplate.UseVisualStyleBackColor = true;
            this.btSaveTemplate.Click += new System.EventHandler(this.btSaveTemplate_Click);
            // 
            // HalconTemplateCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btSaveTemplate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btDraw);
            this.Controls.Add(this.hWindowControltemp);
            this.Name = "HalconTemplateCreator";
            this.Size = new System.Drawing.Size(894, 632);
            this.Load += new System.EventHandler(this.HalconTemplateCreator_Load);
            this.ParentChanged += new System.EventHandler(this.HalconTemplateCreator_ParentChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public HalconDotNet.HWindowControl hWindowControltemp;
        private System.Windows.Forms.Button btDraw;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSaveTemplate;
    }
}
