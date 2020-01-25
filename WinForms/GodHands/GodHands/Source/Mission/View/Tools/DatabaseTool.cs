using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class DatabaseTool : Form {
        public DatabaseTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/menu/tools-database-16.png");
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            View.databasetool = null;
        }
    }
}
