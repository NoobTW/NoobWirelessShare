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
    public partial class Barcode : MaterialSkin.Controls.MaterialForm {
        public Barcode() {
            InitializeComponent();
        }

        private void Barcode_Load(object sender, EventArgs e) {
            pictureBox1.Load ("http://chart.apis.google.com/chart?cht=qr&chs=300x300&chl=WIFI: T:WPA; S: " + Form1.Preference.SSID+"; P: "+ Form1.Preference.Key + "; ;&chld=H|0");
        }
    }
}
