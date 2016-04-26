namespace Noob_Wireless_Share
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblStatus = new System.Windows.Forms.Label();
            this.Panel_toggle_sharing = new System.Windows.Forms.Panel();
            this.toggle_Sharing = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_ssid_intro = new System.Windows.Forms.Label();
            this.lbl_ssid = new System.Windows.Forms.Label();
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbl_traffic_unit = new System.Windows.Forms.Label();
            this.lbl_traffic = new System.Windows.Forms.Label();
            this.iconNetwork = new System.Windows.Forms.PictureBox();
            this.btnEdit = new System.Windows.Forms.PictureBox();
            this.btnSettings = new System.Windows.Forms.PictureBox();
            this.btnInfo = new System.Windows.Forms.PictureBox();
            this.picUpload = new System.Windows.Forms.PictureBox();
            this.picDownload = new System.Windows.Forms.PictureBox();
            this.lblSpeedDownload = new System.Windows.Forms.Label();
            this.lblSpeedUpload = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.PictureBox();
            this.Panel_toggle_sharing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconNetwork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnScan)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Location = new System.Drawing.Point(22, 179);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(173, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "請稍後，正在設定網路介面卡...";
            // 
            // Panel_toggle_sharing
            // 
            this.Panel_toggle_sharing.BackColor = System.Drawing.Color.Transparent;
            this.Panel_toggle_sharing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_toggle_sharing.Controls.Add(this.toggle_Sharing);
            this.Panel_toggle_sharing.Font = new System.Drawing.Font("微軟正黑體 Light", 7F);
            this.Panel_toggle_sharing.ForeColor = System.Drawing.SystemColors.Control;
            this.Panel_toggle_sharing.Location = new System.Drawing.Point(325, 72);
            this.Panel_toggle_sharing.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Panel_toggle_sharing.Name = "Panel_toggle_sharing";
            this.Panel_toggle_sharing.Size = new System.Drawing.Size(89, 26);
            this.Panel_toggle_sharing.TabIndex = 11;
            // 
            // toggle_Sharing
            // 
            this.toggle_Sharing.BackColor = System.Drawing.Color.DarkRed;
            this.toggle_Sharing.Dock = System.Windows.Forms.DockStyle.Left;
            this.toggle_Sharing.FlatAppearance.BorderSize = 0;
            this.toggle_Sharing.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.toggle_Sharing.Font = new System.Drawing.Font("微軟正黑體", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toggle_Sharing.ForeColor = System.Drawing.Color.White;
            this.toggle_Sharing.Location = new System.Drawing.Point(0, 0);
            this.toggle_Sharing.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.toggle_Sharing.Name = "toggle_Sharing";
            this.toggle_Sharing.Size = new System.Drawing.Size(41, 24);
            this.toggle_Sharing.TabIndex = 0;
            this.toggle_Sharing.TabStop = false;
            this.toggle_Sharing.Text = "OFF";
            this.toggle_Sharing.UseVisualStyleBackColor = false;
            this.toggle_Sharing.Click += new System.EventHandler(this.toggle_Sharing_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_ssid_intro
            // 
            this.lbl_ssid_intro.AutoSize = true;
            this.lbl_ssid_intro.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ssid_intro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lbl_ssid_intro.ForeColor = System.Drawing.Color.Black;
            this.lbl_ssid_intro.Location = new System.Drawing.Point(12, 82);
            this.lbl_ssid_intro.Name = "lbl_ssid_intro";
            this.lbl_ssid_intro.Size = new System.Drawing.Size(32, 13);
            this.lbl_ssid_intro.TabIndex = 16;
            this.lbl_ssid_intro.Text = "SSID";
            // 
            // lbl_ssid
            // 
            this.lbl_ssid.AutoSize = true;
            this.lbl_ssid.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ssid.Font = new System.Drawing.Font("微軟正黑體 Light", 15F);
            this.lbl_ssid.ForeColor = System.Drawing.Color.Black;
            this.lbl_ssid.Location = new System.Drawing.Point(44, 72);
            this.lbl_ssid.Name = "lbl_ssid";
            this.lbl_ssid.Size = new System.Drawing.Size(199, 25);
            this.lbl_ssid.TabIndex = 15;
            this.lbl_ssid.Text = "Noob Wireless Share";
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon1.BalloonTipText = "TEST";
            this.NotifyIcon1.BalloonTipTitle = "Noob Wireless Share";
            this.NotifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon1.Icon")));
            this.NotifyIcon1.Text = "Noob Wireless Share";
            this.NotifyIcon1.Visible = true;
            this.NotifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // lbl_traffic_unit
            // 
            this.lbl_traffic_unit.AutoSize = true;
            this.lbl_traffic_unit.BackColor = System.Drawing.Color.Transparent;
            this.lbl_traffic_unit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lbl_traffic_unit.ForeColor = System.Drawing.Color.Black;
            this.lbl_traffic_unit.Location = new System.Drawing.Point(128, 148);
            this.lbl_traffic_unit.Name = "lbl_traffic_unit";
            this.lbl_traffic_unit.Size = new System.Drawing.Size(42, 16);
            this.lbl_traffic_unit.TabIndex = 19;
            this.lbl_traffic_unit.Text = "Bytes";
            // 
            // lbl_traffic
            // 
            this.lbl_traffic.AutoSize = true;
            this.lbl_traffic.BackColor = System.Drawing.Color.Transparent;
            this.lbl_traffic.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.lbl_traffic.ForeColor = System.Drawing.Color.Black;
            this.lbl_traffic.Location = new System.Drawing.Point(12, 99);
            this.lbl_traffic.Name = "lbl_traffic";
            this.lbl_traffic.Size = new System.Drawing.Size(126, 76);
            this.lbl_traffic.TabIndex = 18;
            this.lbl_traffic.Text = "0.0";
            // 
            // iconNetwork
            // 
            this.iconNetwork.BackColor = System.Drawing.Color.Transparent;
            this.iconNetwork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.iconNetwork.Image = global::Noob_Wireless_Share.Properties.Resources.cloud_off;
            this.iconNetwork.Location = new System.Drawing.Point(396, 35);
            this.iconNetwork.Name = "iconNetwork";
            this.iconNetwork.Size = new System.Drawing.Size(18, 18);
            this.iconNetwork.TabIndex = 20;
            this.iconNetwork.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Image = global::Noob_Wireless_Share.Properties.Resources.edit;
            this.btnEdit.Location = new System.Drawing.Point(243, 77);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(18, 18);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.TabStop = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Image = global::Noob_Wireless_Share.Properties.Resources.settings;
            this.btnSettings.Location = new System.Drawing.Point(378, 164);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(36, 36);
            this.btnSettings.TabIndex = 22;
            this.btnSettings.TabStop = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.Image = global::Noob_Wireless_Share.Properties.Resources.info;
            this.btnInfo.Location = new System.Drawing.Point(336, 164);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(36, 36);
            this.btnInfo.TabIndex = 23;
            this.btnInfo.TabStop = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // picUpload
            // 
            this.picUpload.BackColor = System.Drawing.Color.White;
            this.picUpload.Image = global::Noob_Wireless_Share.Properties.Resources.arrow_up;
            this.picUpload.Location = new System.Drawing.Point(325, 106);
            this.picUpload.Name = "picUpload";
            this.picUpload.Size = new System.Drawing.Size(18, 18);
            this.picUpload.TabIndex = 24;
            this.picUpload.TabStop = false;
            this.picUpload.Visible = false;
            // 
            // picDownload
            // 
            this.picDownload.BackColor = System.Drawing.Color.White;
            this.picDownload.Image = global::Noob_Wireless_Share.Properties.Resources.arrow_down;
            this.picDownload.Location = new System.Drawing.Point(325, 130);
            this.picDownload.Name = "picDownload";
            this.picDownload.Size = new System.Drawing.Size(18, 18);
            this.picDownload.TabIndex = 24;
            this.picDownload.TabStop = false;
            this.picDownload.Visible = false;
            // 
            // lblSpeedDownload
            // 
            this.lblSpeedDownload.AutoSize = true;
            this.lblSpeedDownload.BackColor = System.Drawing.Color.Transparent;
            this.lblSpeedDownload.Location = new System.Drawing.Point(345, 130);
            this.lblSpeedDownload.Name = "lblSpeedDownload";
            this.lblSpeedDownload.Size = new System.Drawing.Size(44, 16);
            this.lblSpeedDownload.TabIndex = 25;
            this.lblSpeedDownload.Text = "0 KB/S";
            this.lblSpeedDownload.Visible = false;
            // 
            // lblSpeedUpload
            // 
            this.lblSpeedUpload.AutoSize = true;
            this.lblSpeedUpload.BackColor = System.Drawing.Color.Transparent;
            this.lblSpeedUpload.Location = new System.Drawing.Point(345, 106);
            this.lblSpeedUpload.Name = "lblSpeedUpload";
            this.lblSpeedUpload.Size = new System.Drawing.Size(42, 16);
            this.lblSpeedUpload.TabIndex = 26;
            this.lblSpeedUpload.Text = "0 KB/s";
            this.lblSpeedUpload.Visible = false;
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Transparent;
            this.btnScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScan.Image = global::Noob_Wireless_Share.Properties.Resources.scan;
            this.btnScan.Location = new System.Drawing.Point(294, 164);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(36, 36);
            this.btnScan.TabIndex = 27;
            this.btnScan.TabStop = false;
            this.btnScan.Visible = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 212);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.lblSpeedUpload);
            this.Controls.Add(this.lblSpeedDownload);
            this.Controls.Add(this.picDownload);
            this.Controls.Add(this.picUpload);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.iconNetwork);
            this.Controls.Add(this.lbl_traffic_unit);
            this.Controls.Add(this.lbl_traffic);
            this.Controls.Add(this.lbl_ssid_intro);
            this.Controls.Add(this.lbl_ssid);
            this.Controls.Add(this.Panel_toggle_sharing);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Noob Wireless Share";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.Panel_toggle_sharing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconNetwork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnScan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.Panel Panel_toggle_sharing;
        internal System.Windows.Forms.Button toggle_Sharing;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.Label lbl_ssid_intro;
        internal System.Windows.Forms.Label lbl_ssid;
        private System.Windows.Forms.NotifyIcon NotifyIcon1;
        internal System.Windows.Forms.Label lbl_traffic_unit;
        internal System.Windows.Forms.Label lbl_traffic;
        private System.Windows.Forms.PictureBox iconNetwork;
        private System.Windows.Forms.PictureBox btnEdit;
        private System.Windows.Forms.PictureBox btnSettings;
        private System.Windows.Forms.PictureBox btnInfo;
        private System.Windows.Forms.PictureBox picUpload;
        private System.Windows.Forms.PictureBox picDownload;
        private System.Windows.Forms.Label lblSpeedDownload;
        private System.Windows.Forms.Label lblSpeedUpload;
        private System.Windows.Forms.PictureBox btnScan;
    }
}

