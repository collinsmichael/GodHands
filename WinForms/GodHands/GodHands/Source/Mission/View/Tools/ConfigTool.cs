using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class ConfigTool : Form {
        public ConfigTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/tools-options-16.png");
        }
    }
}
