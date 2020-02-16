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

        public bool Insert(object obj) { return Notify(obj); }
        public bool Remove(object obj) { return Notify(obj); }
        public bool Notify(object obj) {
            try {
                object old = win.SelectedObject;
                if (old != null) {
                    Publisher.Unsubscribe(old as BaseClass, this);
                }
                win.SelectedObject = obj;
                if (obj != null) {
                    Publisher.Subscribe(obj as BaseClass, this);
                }
            } catch {
                //NullReferenceExeception is expected
            }
            return true;
        }
    }
}
