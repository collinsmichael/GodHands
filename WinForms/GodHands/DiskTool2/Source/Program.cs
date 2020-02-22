using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Program {
        [STAThread]
        static void Main(string[] args) {
            Logger.SetUp();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DiskTool());
        }
    }
}
