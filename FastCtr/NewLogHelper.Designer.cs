﻿namespace FastCtr
{
    partial class NewLogHelper
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtProcessDisp = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtProcessDisp
            // 
            this.txtProcessDisp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProcessDisp.Location = new System.Drawing.Point(0, 0);
            this.txtProcessDisp.Name = "txtProcessDisp";
            this.txtProcessDisp.Size = new System.Drawing.Size(399, 334);
            this.txtProcessDisp.TabIndex = 0;
            this.txtProcessDisp.Text = "";
            // 
            // NewLogHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtProcessDisp);
            this.Name = "NewLogHelper";
            this.Size = new System.Drawing.Size(399, 334);
            this.Load += new System.EventHandler(this.NewLogHelper_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtProcessDisp;
    }
}
