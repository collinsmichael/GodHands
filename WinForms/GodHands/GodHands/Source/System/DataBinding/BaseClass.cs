using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class BaseClass : IBound {
        private string url;

        public BaseClass(string url, int pos) {
            this.url = url;
            Model.SetPos(url, pos);
        }

        public string GetUrl() {
            return url;
        }

        public virtual string GetText() {
            return "";
        }

        public virtual int GetPos() {
            return Model.GetPos(url);
        }

        public virtual void SetPos(int pos) {
            Model.SetPos(url, pos);
        }

        public virtual int GetLen() {
            return 0;
        }
    }
}
