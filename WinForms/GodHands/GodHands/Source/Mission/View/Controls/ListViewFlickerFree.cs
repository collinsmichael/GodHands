using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class ListViewFlickerFree : ListView {
        public ListViewFlickerFree() {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m) {
            const int WM_ERASEBKGND = 0x14;
            if (m.Msg != WM_ERASEBKGND) {
                base.OnNotifyMessage(m);
            }
        }
    }
}
