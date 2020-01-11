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
            object old = win.SelectedObject;
            Publisher.Unsubscribe(old as IBound, this);
            win.SelectedObject = obj;
            Publisher.Subscribe(obj as IBound, this);
            return true;
        }
    }
}
