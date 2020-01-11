using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            Logger.SetUp();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frame());
        }
    }
}
