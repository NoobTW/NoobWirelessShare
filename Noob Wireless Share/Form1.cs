using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace Noob_Wireless_Share {
    public partial class Form1 : MaterialSkin.Controls.MaterialForm {
        Process cmd = new Process();
        public static NetworkInterface nicBest;
        public static NetworkInterface nicShare;
        public enum stateSharing {
            STATESHARING_NOT_INITIALIZED,
            STATESHARING_ERROR,
            STATESHARING_STOP,
            STATESHARING_SHARING
        }
        public static stateSharing stat = stateSharing.STATESHARING_NOT_INITIALIZED;
        public static Preference Preference = new Preference();
        long lastupload = 0;
        long lastdownload = 0;
        int connection = 0;
        ToolTip tooltip = new ToolTip();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Version ver = new Version(Application.ProductVersion);
            this.Text = this.Text + " v" + ver.Major + "." + ver.Minor;
            Preference.initPreference();
            initSoftAP();
            lbl_ssid.Text = Preference.SSID;
            btnEdit.Left = lbl_ssid.Right;
            NotifyIcon1.Icon = Properties.Resources.logo;
            NotifyIcon1.Visible = true;
            iconNetwork.Image = isConnected() ? Properties.Resources.cloud : Properties.Resources.cloud_off;

            tooltip.SetToolTip(lbl_traffic, "已使用流量");
            tooltip.SetToolTip(btnInfo, "關於 Noob Wirless Share");
            tooltip.SetToolTip(btnSettings, "設定");
            tooltip.SetToolTip(btnEdit, "修改網路設定");
            tooltip.SetToolTip(btnScan, "條碼掃描");
            tooltip.SetToolTip(picUpload, "上傳速度");
            tooltip.SetToolTip(lblSpeedUpload, "上傳速度");
            tooltip.SetToolTip(picDownload, "下載速度");
            tooltip.SetToolTip(lblSpeedDownload, "下載速度");
        }

        short checkstatusinterval = 0;
        private stateSharing checkStatus() {
            if (++checkstatusinterval == 10) checkstatusinterval = 0;

            if (checkstatusinterval % 10 == 0) iconNetwork.Image = isConnected() ? Properties.Resources.cloud : Properties.Resources.cloud_off;

            cmd.Start();
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StandardInput.WriteLine("netsh wlan show hostednetwork");
            cmd.StandardInput.WriteLine("exit");
            cmd.WaitForExit(700);
            try {
                StreamReader sr = cmd.StandardOutput;
                string s;
                while (!sr.EndOfStream) {
                    s = sr.ReadLine().Trim();
                    if (s.StartsWith("狀態")) {
                        string[] str = s.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                        switch (str[str.Length - 1].Trim()) {
                            case "尚未開始":
                                if (stat != stateSharing.STATESHARING_STOP) {
                                    toggle(false);
                                    lastupload = lastdownload = 0;
                                }
                                updateStatus("✓ 就緒，請開始分享網路");
                                stat = stateSharing.STATESHARING_STOP;
                                break;
                            case "已啟動":
                                if(stat != stateSharing.STATESHARING_SHARING) toggle(true);
                                updateStatus(connection != 0 ? connection + "人正在使用網路" : "沒有人正在使用網路");
                                stat = stateSharing.STATESHARING_SHARING;
                                break;
                            case "無法使用":
                                if(stat == stateSharing.STATESHARING_SHARING) {
                                    sharing(false);
                                    lastupload = lastdownload = 0;
                                    toggle(false);
                                }
                                updateStatus("✗ 找不到可分享網路的無線網卡");
                                stat = stateSharing.STATESHARING_ERROR;
                                break;
                        }
                    }
                    if(stat == stateSharing.STATESHARING_SHARING) {
                        if (s.StartsWith("用戶端數目")) {
                            string[] str = s.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            try {
                                connection = int.Parse(str[str.Length - 1].Trim());
                            } catch (Exception) {
                                connection = 0;
                            }
                        }
                    }
                }
                sr.Close();

                if (stat == stateSharing.STATESHARING_SHARING) {
                    float speedDown = (float)(nicShare.GetIPv4Statistics().BytesReceived - lastdownload) / timer1.Interval;
                    float speedUp = (float)(nicShare.GetIPv4Statistics().BytesSent - lastupload) / timer1.Interval;
                    lastdownload = nicShare.GetIPv4Statistics().BytesReceived;
                    lastupload = nicShare.GetIPv4Statistics().BytesSent;

                    string unitDown = " KB/s";
                    string unitUp = " KB/s";

                    if (speedDown > 1048576) {
                        speedDown /= 1048576;
                        unitDown = " GB/s";
                    } else if (speedDown >= 1024) {
                        speedDown /= 1024;
                        unitUp = " MB/s";
                    }
                    if (speedUp > 1048576) {
                        speedUp /= 1048576;
                        unitUp = " GB/s";
                    } else if (speedUp >= 1024) {
                        speedUp /= 1024;
                        unitUp = " MB/s";
                    }

                    lblSpeedDownload.Text = speedDown.ToString("f1") + unitDown;
                    lblSpeedUpload.Text = speedUp.ToString("f1") + unitUp;

                    double traffic = lastupload + lastdownload;
                    if (traffic > 1099511627776) {
                        lbl_traffic_unit.Text = "TB";
                        traffic /= 1099511627776;
                    } else if (traffic > 1073741824) {
                        lbl_traffic_unit.Text = "GB";
                        traffic /= 1073741824;
                    } else if (traffic > 1048576) {
                        lbl_traffic_unit.Text = "MB";
                        traffic /= 1048576;
                    } else if (traffic > 1024) {
                        lbl_traffic_unit.Text = "KB";
                        traffic /= 1024;
                    }
                    lbl_traffic.Text = traffic.ToString("f1");
                    lbl_traffic_unit.Left = lbl_traffic.Right - 10;
                }
            } catch (Exception) { }
            return stat;
        }
        
        private void initSoftAP() {
            try {
                ServiceController WlanSvc = new ServiceController("WlanSvc");
                if(WlanSvc.Status == ServiceControllerStatus.Stopped) {
                    updateStatus("正在啟動WlanSvc服務...");
                    WlanSvc.Start();
                }
            }catch (Exception) {
                updateStatus("✗ 找不到WlanSvc服務");
                return;
            }
            new Thread(() => { initNIC(); }).Start();

            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            
            cmd.Start();
            cmd.StandardInput.WriteLine("netsh wlan stop hostednetwork");
            cmd.StandardInput.WriteLine("netsh wlan set hostednetwork mode=allow");
            cmd.StandardInput.WriteLine("netsh wlan set hostednetwork ssid=\"Noob Wireless Share\" key=12345678");
            cmd.StandardInput.WriteLine("netsh wlan start hostednetwork");
            cmd.StandardInput.WriteLine("netsh wlan stop hostednetwork");
            cmd.StandardInput.WriteLine("exit");
            cmd.WaitForExit(700);
        }
      
        private void initNIC() {
            NETCONLib.NetSharingManager nsm = new NETCONLib.NetSharingManager();
            Dictionary<String, NETCONLib.INetConnection> dic = new Dictionary<string, NETCONLib.INetConnection>() { };
            foreach(NETCONLib.INetConnection con in nsm.EnumEveryConnection) {
                NETCONLib.INetConnectionProps prop = nsm.NetConnectionProps[con];
                if(!prop.Name.StartsWith("Bluetooth") && !prop.Name.StartsWith("藍牙")){
                    dic.Add(prop.Guid, con);
                    nsm.INetSharingConfigurationForINetConnection[con].DisableSharing();
                }   
            }
            nicBest = getBestInterface();
            if(stat == stateSharing.STATESHARING_NOT_INITIALIZED || stat == stateSharing.STATESHARING_ERROR) initMiss();
        }

        private void initMiss() {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => initMiss()));
            } else {
                if (checkStatus() == stateSharing.STATESHARING_STOP) {
                    if (Preference.ShareOnStart) {
                        sharing(true);
                    }
                    timer1.Enabled = true;
                }
                if(checkStatus() == stateSharing.STATESHARING_ERROR) {
                    timer1.Enabled = true;
                }
            }
        }

        private void setNICShare() {
            updateStatus("正在設定網路介面卡...(1/4)");
            short countError = 0;
            short isFind = 1;
            NetworkInterface nicSelected;

            if (Preference.ShareNetwork == "[Auto]") {
                nicSelected = nicBest;
            } else if ((nicSelected = findNicByName(Preference.ShareNetwork)) == null) {
                nicSelected = nicBest;
            }

            NETCONLib.NetSharingManager nsm = new NETCONLib.NetSharingManager();
            foreach (NETCONLib.INetConnection con in nsm.EnumEveryConnection) {
                NETCONLib.INetConnectionProps prop = nsm.NetConnectionProps[con];
                if (prop.Name == nicSelected.Name) {
                    updateStatus("正在設定網路介面卡...(" + ++isFind + "/4)");
                    Boolean success = false;
                    while (countError < 3 && !success) {
                        if (countError != 0) updateStatus("✗ 發生不明錯誤，正在重試...");
                        try {
                            nsm.INetSharingConfigurationForINetConnection[con].EnableSharing(NETCONLib.tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
                            success = true;
                        } catch (Exception) {
                            countError++;
                        }
                    }
                    if (countError == 3) {
                        updateStatus("✗ 發生不明錯誤");
                        return;
                    }
                    countError = 0;
                }
                if (prop.DeviceName == "Microsoft 主控網路虛擬介面卡" || prop.DeviceName == "Microsoft Hosted Network Virtual Adapter") {
                    updateStatus("正在設定網路介面卡...(" + ++isFind + "/4)");
                    Boolean success = false;
                    nicShare = findNicByName(prop.Name);
                    while (countError < 3 && !success) {
                        if (countError != 0) updateStatus("✗ 發生不明錯誤，正在重試...");
                        try {
                            nsm.INetSharingConfigurationForINetConnection[con].EnableSharing(NETCONLib.tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);

                            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + prop.Guid + "\\Connection", true);
                            rk.SetValue("Name", "Noob Wireless Share");
                            rk.Dispose();
                            nicShare = findNicByName("Noob Wireless Share");

                            if (nicShare == null) {
                                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                                foreach (NetworkInterface nic in interfaces) {
                                    if (nic.Description == "Microsoft Hosted Network Virtual Adapter" || nic.Description == "Microsoft 主控網路虛擬介面卡") {
                                        nicShare = nic;
                                        break;
                                    }

                                }
                                interfaces = null;
                            }

                            success = true;
                        } catch (Exception) {
                            countError++;
                        }
                    }
                    if (countError == 3) {
                        updateStatus("✗ 發生不明錯誤");
                        return;
                    }
                }
            }
            updateStatus("正在檢查IP設定...(4/4)");
            cmd.Start();
            string name = "Noob Wireless Share";
            if (nicShare != null) name = nicShare.Name;
            cmd.StandardInput.WriteLine("netsh interface ip set address name=\"" + name + "\" static " + Preference.ServerIP + " 255.255.255.0");
            //cmd.StandardInput.WriteLine("netsh interface ip set address name=\"" + name + "\" dhcp");
            cmd.StandardInput.WriteLine("exit");
            cmd.WaitForExit(700);
        }

        private void sharing(Boolean foo) {
            if (foo == true) {
                if (stat == 0) return;
                cmd.Start();
                cmd.StandardInput.WriteLine("netsh wlan set hostednetwork mode=allow ssid=\"" + Preference.SSID + "\" key=\"" + Preference.Key + "\"");
                cmd.StandardInput.WriteLine("netsh wlan set blockperiod 0");
                cmd.StandardInput.WriteLine("netsh wlan start hostednetwork");
                cmd.StandardInput.WriteLine("exit");
                cmd.WaitForExit(700);
                new Thread(() => { setNICShare(); }).Start();

                NotifyIcon1.BalloonTipTitle = "Noob Wireless Share";
                NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                NotifyIcon1.BalloonTipText = "已開始分享網路：" + Preference.SSID;
                NotifyIcon1.ShowBalloonTip(2500);
            } else {
                new Thread(() => { initNIC(); }).Start();

                cmd.Start();
                cmd.StandardInput.WriteLine("netsh wlan stop hostednetwork");
                cmd.StandardInput.WriteLine("exit");
                cmd.WaitForExit(700);

                NotifyIcon1.BalloonTipTitle = "Noob Wireless Share";
                NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                NotifyIcon1.BalloonTipText = "已停止分享網路";
                NotifyIcon1.ShowBalloonTip(2500);

                lbl_traffic.Text = "0.0";
                lbl_traffic_unit.Text = "Bytes";
                lbl_traffic_unit.Left = lbl_traffic.Right - 10;
            }
            toggle(true);
            lblSpeedDownload.Visible = foo;
            lblSpeedUpload.Visible = foo;
            picUpload.Visible = foo;
            picDownload.Visible = foo;
            btnScan.Visible = foo;
        }

        private NetworkInterface getBestInterface(int port = 80) {
            IPAddress ip;
            try {
                IPHostEntry hostentry = Dns.GetHostEntry("www.google.com");
                ip = hostentry.AddressList[0];
            } catch (Exception ex) {
                IPHostEntry hostentry = Dns.GetHostEntry("www.facebook.com");
                ip = hostentry.AddressList[0];
                throw ex;
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            NetworkInterface outgoingInterface = null;
            try {
                socket.Connect(new IPEndPoint(ip, port));
                if (socket.Connected) {
                    NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                    foreach(NetworkInterface nic in interfaces) {
                        IPInterfaceProperties prop = nic.GetIPProperties();
                        foreach (UnicastIPAddressInformation unicastAddr in prop.UnicastAddresses) {
                            if (unicastAddr.Address.Equals(((IPEndPoint)socket.LocalEndPoint).Address)) {
                                outgoingInterface = nic;
                            }
                        }
                    }
                    return outgoingInterface;
                }
            } catch (Exception ex) {
                throw ex;
            }
            socket.Dispose();
            return null;
        }

        private NetworkInterface findNicByName(string name) {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            
            foreach(NetworkInterface nic in interfaces) {
                if (nic.Name == name) return nic;
            }
            interfaces = null;
            return null;
        }

        private bool isConnected() {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private void updateStatus(string text) {
            if (lblStatus.InvokeRequired) {
                this.Invoke(new Action(() => updateStatus(text)));
            } else {
                lblStatus.Text = text;
            }
        }

        private void toggle(Boolean foo) {
            if (toggle_Sharing.InvokeRequired) {
                this.Invoke(new Action(() => toggle(foo)));
            } else {
                if (foo == true) {
                    toggle_Sharing.Enabled = true;
                    toggle_Sharing.BackColor = Color.Red;
                    toggle_Sharing.Dock = DockStyle.Right;
                    toggle_Sharing.Text = "ON";
                } else {
                    toggle_Sharing.Enabled = true;
                    toggle_Sharing.BackColor = Color.DarkRed;
                    toggle_Sharing.Dock = DockStyle.Left;
                    toggle_Sharing.Text = "OFF";
                }
            }
        }

        private void toggle_Sharing_Click(object sender, EventArgs e) {
            sharing(stat != stateSharing.STATESHARING_SHARING);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (stat==stateSharing.STATESHARING_SHARING) {
                DialogResult msg = MessageBox.Show("結束會停止分享網路，要繼續？", "Noob Wireless Share", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (msg == DialogResult.No) {
                    e.Cancel = true;
                    return;
                } else {
                    sharing(false);
                    cmd.Start();
                    cmd.StandardInput.WriteLine("netsh wlan set hostednetwork mode=disallow");
                    cmd.StandardInput.WriteLine("exit");
                    cmd.WaitForExit(700);
                }
            }
            Preference.SSID = Preference.SSID;
            Preference.Key = Preference.Key;
            Preference.savePreference();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            checkStatus();
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            connectInfo connectInfo = new connectInfo();
            DialogResult result = connectInfo.ShowDialog();
            if (result == DialogResult.OK) {
                lbl_ssid.Text = Preference.SSID;
                btnEdit.Left = lbl_ssid.Right;
            }
            connectInfo.Dispose();
        }

        private void btnSettings_Click(object sender, EventArgs e) {
            Miscellaneous miscellaneous = new Miscellaneous();
            DialogResult result = miscellaneous.ShowDialog();
            miscellaneous.Dispose();
        }

        private void btnInfo_Click(object sender, EventArgs e) {
            Process.Start("https://noob.tw/noob-wirelessshare");
        }

        private void btnScan_Click(object sender, EventArgs e) {
            Barcode barcode = new Barcode();
            barcode.ShowDialog();
            barcode.Dispose();
        }

        private void Form1_Resize(object sender, EventArgs e) {
            if(this.WindowState == FormWindowState.Minimized) {
                this.Hide();
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (this.Visible == true) {
                this.Hide();
            } else {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }
    }

    public class Preference {
        public string PreferencePath = "Preference.ini";

        public string SSID { get; set; }
        public string Key { get; set; }
        public string ShareNetwork { get; set; }
        public string ServerIP { get; set; }
        public Boolean StartOnBoot { get; set; }
        public Boolean ShareOnStart { get; set; }
        public string EnableDHCP { get; set; }

        public void initPreference() {
            if (File.Exists(PreferencePath)) {
                StreamReader sr = new StreamReader(PreferencePath, Encoding.UTF8);
                while (!sr.EndOfStream) {
                    string s = sr.ReadLine().Trim();
                    if (s.StartsWith("[SSID]")) {
                        string[] str = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        SSID = str[str.Length - 1];
                    }
                    if (s.StartsWith("[Key]")) {
                        string[] str = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        Key = Decrypt(str[str.Length - 1], "noobwire");
                    }
                    if (s.StartsWith("[ShareNetwork]")) {
                        string[] str = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        ShareNetwork = str[str.Length - 1];
                    }
                    if (s.StartsWith("[ServerIP]")) {
                        string[] str = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        ServerIP = str[str.Length - 1];
                    }
                    if (s.StartsWith("[StartOnBoot]")) {
                        string[] str = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (str[str.Length - 1] == "true") {
                            StartOnBoot = true;
                        } else {
                            StartOnBoot = false;
                        }
                    }
                    if (s.StartsWith("[ShareOnStart]")) {
                        string[] str = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (str[str.Length - 1] == "true") {
                            ShareOnStart = true;
                        } else {
                            ShareOnStart = false;
                        }
                    }
                    if (s.StartsWith("[EnableDHCP]")) {
                        string[] str = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        EnableDHCP = str[str.Length - 1];
                    }
                    ServerIP = "192.168.137.1";
                }
                sr.Close();
                if (SSID == String.Empty) SSID = "Noob Wireless Share";
                if (Key == String.Empty) Key = "12345678";
                if (ShareNetwork == String.Empty) ShareNetwork = "[Auto]";
                if (ServerIP == String.Empty) ServerIP = "192.168.137.1";
                if (EnableDHCP == string.Empty) EnableDHCP = "idk";
            } else {
                SSID = "Noob Wireless Share";
                Key = "12345678";
                ShareNetwork = "[Auto]";
                ServerIP = "192.168.137.1";
                StartOnBoot = false;
                ShareOnStart = false;
                EnableDHCP = "idk";
            }
        }

        public void savePreference() {
            StreamWriter sw = new StreamWriter(PreferencePath, false, Encoding.UTF8);
            sw.WriteLine("Noob Wireless Share Preference");
            sw.WriteLine("");
            if (SSID != String.Empty) sw.WriteLine("[SSID]=" + SSID);
            if (Key != String.Empty) sw.WriteLine("[Key]=" + Encrypt(Key, "noobwire"));
            if (ShareNetwork != String.Empty) sw.WriteLine("[ShareNetwork]=" + ShareNetwork);
            if (ServerIP != String.Empty) sw.WriteLine("[ServerIP]=" + ServerIP);
            sw.WriteLine("[StartOnBoot]=" + (StartOnBoot ? "true" : "false"));
            sw.WriteLine("[ShareOnStart]=" + (ShareOnStart ? "true" : "false"));
            if (EnableDHCP != String.Empty) sw.WriteLine("[EnableDHCP]=" + EnableDHCP);
            sw.Close();
        }

        private string Encrypt(string content, string key) {
            StringBuilder sb = new StringBuilder();
            System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] skey = Encoding.ASCII.GetBytes(key);
            byte[] iv = Encoding.ASCII.GetBytes(key);
            byte[] dataByteArray = Encoding.UTF8.GetBytes(content);

            des.Key = skey;
            des.IV = iv;
            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)) {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                foreach (byte b in ms.ToArray()) {
                    sb.AppendFormat("{0:X2}", b);
                }
                encrypt = sb.ToString();
            }
            return encrypt;
        }

        private string Decrypt(string content, string key) {
            try {
                byte[] dataByteArray = new byte[content.Length / 2];
                for (int x = 0; x < content.Length / 2; x++) {
                    int i = (Convert.ToInt32(content.Substring(x * 2, 2), 16));
                    dataByteArray[x] = (byte)i;
                }

                System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
                byte[] skey = Encoding.ASCII.GetBytes(key);
                byte[] iv = Encoding.ASCII.GetBytes(key);
                des.Key = skey;
                des.IV = iv;

                using (MemoryStream ms = new MemoryStream()) {
                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)) {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            } catch (Exception) {
                return "";
            }
        }
    }
}

