namespace UIform
{
    partial class SettingForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_SaveImage = new System.Windows.Forms.Button();
            this.Btn_ReadImage = new System.Windows.Forms.Button();
            this.Btn_GrabContinue = new System.Windows.Forms.Button();
            this.Btn_GrabSingle = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Btn_SetUsual = new System.Windows.Forms.Button();
            this.btn_DeleteOld = new System.Windows.Forms.Button();
            this.btn_AddNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_NewType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comBox_TypeNow = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Btn_ReadExGa = new System.Windows.Forms.Button();
            this.Btn_SaveExGa = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numUD_Gain = new System.Windows.Forms.NumericUpDown();
            this.numUD_Exposure = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Exposure)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hWindowControl1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(644, 441);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(3, 11);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(635, 424);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(635, 424);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btn_SaveImage);
            this.groupBox2.Controls.Add(this.Btn_ReadImage);
            this.groupBox2.Controls.Add(this.Btn_GrabContinue);
            this.groupBox2.Controls.Add(this.Btn_GrabSingle);
            this.groupBox2.Location = new System.Drawing.Point(658, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "获取图片";
            // 
            // Btn_SaveImage
            // 
            this.Btn_SaveImage.Location = new System.Drawing.Point(97, 52);
            this.Btn_SaveImage.Name = "Btn_SaveImage";
            this.Btn_SaveImage.Size = new System.Drawing.Size(75, 27);
            this.Btn_SaveImage.TabIndex = 3;
            this.Btn_SaveImage.Text = "保存图片";
            this.Btn_SaveImage.UseVisualStyleBackColor = true;
            this.Btn_SaveImage.Click += new System.EventHandler(this.Btn_SaveImage_Click);
            // 
            // Btn_ReadImage
            // 
            this.Btn_ReadImage.Location = new System.Drawing.Point(16, 52);
            this.Btn_ReadImage.Name = "Btn_ReadImage";
            this.Btn_ReadImage.Size = new System.Drawing.Size(75, 27);
            this.Btn_ReadImage.TabIndex = 2;
            this.Btn_ReadImage.Text = "打开图片";
            this.Btn_ReadImage.UseVisualStyleBackColor = true;
            this.Btn_ReadImage.Click += new System.EventHandler(this.Btn_ReadImage_Click);
            // 
            // Btn_GrabContinue
            // 
            this.Btn_GrabContinue.Location = new System.Drawing.Point(97, 20);
            this.Btn_GrabContinue.Name = "Btn_GrabContinue";
            this.Btn_GrabContinue.Size = new System.Drawing.Size(75, 27);
            this.Btn_GrabContinue.TabIndex = 1;
            this.Btn_GrabContinue.Text = "连续采图";
            this.Btn_GrabContinue.UseVisualStyleBackColor = true;
            this.Btn_GrabContinue.Click += new System.EventHandler(this.Btn_GrabContinue_Click);
            // 
            // Btn_GrabSingle
            // 
            this.Btn_GrabSingle.Location = new System.Drawing.Point(16, 20);
            this.Btn_GrabSingle.Name = "Btn_GrabSingle";
            this.Btn_GrabSingle.Size = new System.Drawing.Size(75, 27);
            this.Btn_GrabSingle.TabIndex = 0;
            this.Btn_GrabSingle.Text = "单帧采图";
            this.Btn_GrabSingle.UseVisualStyleBackColor = true;
            this.Btn_GrabSingle.Click += new System.EventHandler(this.Btn_GrabSingle_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Btn_SetUsual);
            this.groupBox3.Controls.Add(this.btn_DeleteOld);
            this.groupBox3.Controls.Add(this.btn_AddNew);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tb_NewType);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.comBox_TypeNow);
            this.groupBox3.Location = new System.Drawing.Point(658, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 102);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "料号设置";
            // 
            // Btn_SetUsual
            // 
            this.Btn_SetUsual.Location = new System.Drawing.Point(127, 69);
            this.Btn_SetUsual.Name = "Btn_SetUsual";
            this.Btn_SetUsual.Size = new System.Drawing.Size(53, 27);
            this.Btn_SetUsual.TabIndex = 6;
            this.Btn_SetUsual.Text = "常用";
            this.Btn_SetUsual.UseVisualStyleBackColor = true;
            this.Btn_SetUsual.Click += new System.EventHandler(this.Btn_SetUsual_Click);
            // 
            // btn_DeleteOld
            // 
            this.btn_DeleteOld.Location = new System.Drawing.Point(68, 69);
            this.btn_DeleteOld.Name = "btn_DeleteOld";
            this.btn_DeleteOld.Size = new System.Drawing.Size(53, 27);
            this.btn_DeleteOld.TabIndex = 5;
            this.btn_DeleteOld.Text = "删除";
            this.btn_DeleteOld.UseVisualStyleBackColor = true;
            this.btn_DeleteOld.Click += new System.EventHandler(this.btn_DeleteOld_Click);
            // 
            // btn_AddNew
            // 
            this.btn_AddNew.Location = new System.Drawing.Point(16, 69);
            this.btn_AddNew.Name = "btn_AddNew";
            this.btn_AddNew.Size = new System.Drawing.Size(46, 27);
            this.btn_AddNew.TabIndex = 4;
            this.btn_AddNew.Text = "添加";
            this.btn_AddNew.UseVisualStyleBackColor = true;
            this.btn_AddNew.Click += new System.EventHandler(this.btn_AddNew_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "新增料号";
            // 
            // tb_NewType
            // 
            this.tb_NewType.Location = new System.Drawing.Point(68, 46);
            this.tb_NewType.Name = "tb_NewType";
            this.tb_NewType.Size = new System.Drawing.Size(100, 21);
            this.tb_NewType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前料号";
            // 
            // comBox_TypeNow
            // 
            this.comBox_TypeNow.FormattingEnabled = true;
            this.comBox_TypeNow.Location = new System.Drawing.Point(68, 20);
            this.comBox_TypeNow.Name = "comBox_TypeNow";
            this.comBox_TypeNow.Size = new System.Drawing.Size(100, 20);
            this.comBox_TypeNow.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button12);
            this.groupBox4.Controls.Add(this.button11);
            this.groupBox4.Controls.Add(this.button10);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Location = new System.Drawing.Point(658, 310);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(195, 137);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "模板设置";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(97, 107);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 7;
            this.button12.Text = "button12";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(16, 107);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 6;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(97, 78);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 5;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(16, 78);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 4;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(97, 49);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 3;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(16, 49);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 2;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(97, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 1;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(16, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(871, 483);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(863, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "视觉设置";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Btn_ReadExGa);
            this.groupBox5.Controls.Add(this.Btn_SaveExGa);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.numUD_Gain);
            this.groupBox5.Controls.Add(this.numUD_Exposure);
            this.groupBox5.Location = new System.Drawing.Point(658, 125);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(195, 88);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "亮度设置";
            // 
            // Btn_ReadExGa
            // 
            this.Btn_ReadExGa.Location = new System.Drawing.Point(127, 55);
            this.Btn_ReadExGa.Name = "Btn_ReadExGa";
            this.Btn_ReadExGa.Size = new System.Drawing.Size(46, 27);
            this.Btn_ReadExGa.TabIndex = 6;
            this.Btn_ReadExGa.Text = "读取";
            this.Btn_ReadExGa.UseVisualStyleBackColor = true;
            this.Btn_ReadExGa.Click += new System.EventHandler(this.Btn_ReadExGa_Click);
            // 
            // Btn_SaveExGa
            // 
            this.Btn_SaveExGa.Location = new System.Drawing.Point(127, 20);
            this.Btn_SaveExGa.Name = "Btn_SaveExGa";
            this.Btn_SaveExGa.Size = new System.Drawing.Size(46, 27);
            this.Btn_SaveExGa.TabIndex = 5;
            this.Btn_SaveExGa.Text = "保存";
            this.Btn_SaveExGa.UseVisualStyleBackColor = true;
            this.Btn_SaveExGa.Click += new System.EventHandler(this.Btn_SaveExGa_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "增益";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "曝光";
            // 
            // numUD_Gain
            // 
            this.numUD_Gain.Location = new System.Drawing.Point(55, 61);
            this.numUD_Gain.Name = "numUD_Gain";
            this.numUD_Gain.Size = new System.Drawing.Size(66, 21);
            this.numUD_Gain.TabIndex = 1;
            // 
            // numUD_Exposure
            // 
            this.numUD_Exposure.Location = new System.Drawing.Point(55, 21);
            this.numUD_Exposure.Name = "numUD_Exposure";
            this.numUD_Exposure.Size = new System.Drawing.Size(66, 21);
            this.numUD_Exposure.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(863, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "参数设置";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(869, 484);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingForm";
            this.Text = "SetForm";
            this.Load += new System.EventHandler(this.SetForm_Load);
            this.SizeChanged += new System.EventHandler(this.SettingForm_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Exposure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Btn_SaveImage;
        private System.Windows.Forms.Button Btn_ReadImage;
        private System.Windows.Forms.Button Btn_GrabContinue;
        private System.Windows.Forms.Button Btn_GrabSingle;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comBox_TypeNow;
        private System.Windows.Forms.Button btn_DeleteOld;
        private System.Windows.Forms.Button btn_AddNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_NewType;
        private System.Windows.Forms.Button Btn_SetUsual;
        private System.Windows.Forms.NumericUpDown numUD_Gain;
        private System.Windows.Forms.NumericUpDown numUD_Exposure;
        private System.Windows.Forms.Button Btn_ReadExGa;
        private System.Windows.Forms.Button Btn_SaveExGa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}