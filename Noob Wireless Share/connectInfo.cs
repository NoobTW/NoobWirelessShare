using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noob_Wireless_Share {
    public partial class connectInfo : Form {
        public connectInfo() {
            InitializeComponent();
        }

        private void connectInfo_Load(object sender, EventArgs e) {
            textBox1.Text = Form1.Preference.SSID;
            textBox2.Text = Form1.Preference.Key;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (Form1.stat == Form1.stateSharing.STATESHARING_SHARING && (textBox1.Text!=Form1.Preference.SSID || textBox2.Text!=Form1.Preference.Key)) {
                MessageBox.Show("部分設定要等到重新分享網路才會生效", "Noob Wireless Share", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Form1.Preference.Key = textBox2.Text;
            Form1.Preference.SSID = textBox1.Text;
            this.DialogResult = DialogResult.OK;


            this.Close();
        }
    }
}
