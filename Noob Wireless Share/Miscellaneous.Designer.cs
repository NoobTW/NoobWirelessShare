namespace Noob_Wireless_Share {
    partial class Miscellaneous {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.chkboxStartOnBoot = new System.Windows.Forms.CheckBox();
            this.chkboxShareOnStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "分享網路";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 95);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(248, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.BackColor = System.Drawing.Color.Transparent;
            this.chkAuto.Location = new System.Drawing.Point(70, 75);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(75, 20);
            this.chkAuto.TabIndex = 2;
            this.chkAuto.Text = "自動選擇";
            this.chkAuto.UseVisualStyleBackColor = false;
            this.chkAuto.CheckedChanged += new System.EventHandler(this.chkAuto_CheckedChanged);
            // 
            // chkboxStartOnBoot
            // 
            this.chkboxStartOnBoot.AutoSize = true;
            this.chkboxStartOnBoot.BackColor = System.Drawing.Color.Transparent;
            this.chkboxStartOnBoot.Location = new System.Drawing.Point(15, 126);
            this.chkboxStartOnBoot.Name = "chkboxStartOnBoot";
            this.chkboxStartOnBoot.Size = new System.Drawing.Size(220, 20);
            this.chkboxStartOnBoot.TabIndex = 3;
            this.chkboxStartOnBoot.Text = "開機自動啟動 Noob Wireless Share";
            this.chkboxStartOnBoot.UseVisualStyleBackColor = false;
            // 
            // chkboxShareOnStart
            // 
            this.chkboxShareOnStart.AutoSize = true;
            this.chkboxShareOnStart.BackColor = System.Drawing.Color.Transparent;
            this.chkboxShareOnStart.Location = new System.Drawing.Point(15, 152);
            this.chkboxShareOnStart.Name = "chkboxShareOnStart";
            this.chkboxShareOnStart.Size = new System.Drawing.Size(259, 20);
            this.chkboxShareOnStart.TabIndex = 4;
            this.chkboxShareOnStart.Text = "開啟 Noob Wireless Share 時自動分享網路";
            this.chkboxShareOnStart.UseVisualStyleBackColor = false;
            // 
            // Miscellaneous
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 190);
            this.Controls.Add(this.chkboxShareOnStart);
            this.Controls.Add(this.chkboxStartOnBoot);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Miscellaneous";
            this.Text = "Miscellaneous";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Miscellaneous_FormClosing);
            this.Load += new System.EventHandler(this.Miscellaneous_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.CheckBox chkboxStartOnBoot;
        private System.Windows.Forms.CheckBox chkboxShareOnStart;
    }
}