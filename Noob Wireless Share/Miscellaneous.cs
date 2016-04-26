using System;
using System.Windows.Forms;

namespace Noob_Wireless_Share {
    public partial class Miscellaneous : MaterialSkin.Controls.MaterialForm {
        public Miscellaneous() {
            InitializeComponent();
        }

        private void Miscellaneous_Load(object sender, EventArgs e) {
            comboBox1.Items.Clear();
            try {
                NETCONLib.NetSharingManager nsm = new NETCONLib.NetSharingManager();
                foreach (NETCONLib.INetConnection con in nsm.EnumEveryConnection) {
                    NETCONLib.INetConnectionProps prop = nsm.NetConnectionProps[con];
                    if (!prop.Name.StartsWith("Bluetooth") && !prop.Name.StartsWith("藍牙") && prop.Name != Form1.nicShare.Name) {
                        comboBox1.Items.Add(prop.Name);
                    }
                }
            } catch (Exception) { }
            if (Form1.Preference.ShareNetwork != "[Auto]") {
                comboBox1.Text = Form1.Preference.ShareNetwork;
            } else {
                chkAuto.Checked = true;
                comboBox1.Enabled = false;
                comboBox1.Text = Form1.nicBest.Name;
            }
            chkboxShareOnStart.Checked = Form1.Preference.ShareOnStart;
            chkboxStartOnBoot.Checked = Form1.Preference.StartOnBoot;
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e) {
            comboBox1.Enabled = !(chkAuto.Checked);
        }

        private void Miscellaneous_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            string shareNetwork =  chkAuto.Checked ? "[Auto]" : comboBox1.Text;
            if(Form1.stat == Form1.stateSharing.STATESHARING_SHARING && Form1.Preference.ShareNetwork!=shareNetwork) {
                Form1.Preference.ShareNetwork = shareNetwork;
                MessageBox.Show("部分設定要等到重新分享網路才會生效", "Noob Wireless Share", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (chkboxStartOnBoot.Checked) {
                rk.SetValue("Noob Wireless Share", Application.ExecutablePath.ToString());
            } else {
                rk.DeleteValue("Noob Wireless Share", false);
            }
            rk.Dispose();
            Form1.Preference.ShareOnStart = chkboxShareOnStart.Checked;
            Form1.Preference.StartOnBoot = chkboxStartOnBoot.Checked;
        }
    }
}
