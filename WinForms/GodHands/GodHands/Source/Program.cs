using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Program {
        public static Frame MainForm = null;

        [STAThread]
        static void Main(string[] args) {
            Logger.SetUp();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainForm = new Frame());
        }
    }
}
