using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    class Program {
        static void Main(string[] args) {
            if (RamDisk.Open("test.img")) {
                Model.Open();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TestForm());
            }
        }
    }
}
