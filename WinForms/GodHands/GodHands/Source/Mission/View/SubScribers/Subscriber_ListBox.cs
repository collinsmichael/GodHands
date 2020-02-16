using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Subscriber_ListBox : ISubscriber {
        private string url = null;
        private object obj = null;
        private string filter = null;
        private ListBox win;

        public Subscriber_ListBox(string url, ListBox win) {
            this.url = url;
            this.win = win;
            Publisher.Subscribe(url, this);
        }

        ~Subscriber_ListBox() {
            Publisher.Unsubscribe(url, this);
        }

        public bool Insert(object obj) { return Notify(obj); }
        public bool Remove(object obj) { return Notify(obj); }
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
