﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class MonitorTool : Form {
        public MonitorTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/tools-monitor-16.png");
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            View.monitortool = null;
        }
    }
}
