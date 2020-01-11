using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Subscriber_ListBox : ISubscriber {
        private string filter = null;
        private object obj = null;
        private ListBox win;

        public Subscriber_ListBox(ListBox win) {
            this.win = win;
            Publisher.Subscribe("LOG", this);
        }

        ~Subscriber_ListBox() {
            Publisher.Unsubscribe("LOG", this);
        }

        public bool Notify(object obj) {
            win.Items.Clear();
            this.obj = obj;
            List<string> list = obj as List<string>;
            if (list != null) {
                if ((filter == null) || (filter.Length == 0)) {
                    win.Items.AddRange(list.ToArray());
                } else {
                    foreach (string str in list) {
                        if (str.Contains(filter)) {
                            win.Items.Add(str);
                        }
                    }
                }
            }
            return true;
        }

        public bool SetFilter(string filter) {
            this.filter = filter;
            return Notify(obj);
        }
    }
}
