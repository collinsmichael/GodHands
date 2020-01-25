using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Subscriber_ListView : ISubscriber {
        private string url = null;
        private object obj = null;
        private string filter = null;
        private ListView win;

        public Subscriber_ListView(string url, ListView win) {
            this.url = url;
            this.win = win;
            Publisher.Subscribe(url, this);
        }

        ~Subscriber_ListView() {
            Publisher.Unsubscribe(url, this);
        }

        public bool Notify(object obj) {
            win.Items.Clear();
            this.obj = obj;
            List<string> list = obj as List<string>;
            if (list != null) {
                if ((filter == null) || (filter.Length == 0)) {
                    foreach (string str in list) {
                        int icon = 0;
                        if (str.Contains("[FAIL]")) icon = 0;
                        if (str.Contains("[INFO]")) icon = 1;
                        if (str.Contains("[PASS]")) icon = 2;
                        if (str.Contains("[WARN]")) icon = 3;
                        win.Items.Add(str, icon);
                    }
                    //win.Items.AddRange(list.ToArray());
                } else {
                    string find = filter.ToUpper();
                    foreach (string str in list) {
                        if (str.ToUpper().Contains(find)) {
                            int icon = 0;
                            if (str.Contains("[FAIL]")) icon = 0;
                            if (str.Contains("[INFO]")) icon = 1;
                            if (str.Contains("[PASS]")) icon = 2;
                            if (str.Contains("[WARN]")) icon = 3;
                            win.Items.Add(str, icon);
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
