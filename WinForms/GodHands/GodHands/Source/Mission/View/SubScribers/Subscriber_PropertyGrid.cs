using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Subscriber_PropertyGrid : ISubscriber {
        private PropertyGrid win;

        public Subscriber_PropertyGrid(PropertyGrid win) {
            this.win = win;
        }

        public bool Notify(object obj) {
            try {
                object old = win.SelectedObject;
                if (old != null) {
                    Publisher.Unsubscribe(old as IBound, this);
                }
                win.SelectedObject = obj;
                if (obj != null) {
                    Publisher.Subscribe(obj as IBound, this);
                }
            } catch {
                //NullReferenceExeception is expected
            }
            return true;
        }
    }
}
