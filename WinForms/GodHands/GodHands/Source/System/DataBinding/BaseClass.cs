using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class BaseClass {
        private BaseClass parent;
        public string Key;
        private string url;
        private int offset;
        private int length;

        public int Pos {
            get { return (parent != null) ? offset : parent.Pos + offset; }
            set { }
        }

        public BaseClass(BaseClass parent, string url, int offset) {
            this.parent = parent;
            this.url = url;
            this.offset = offset;
        }

        public string GetUrl() {
            return url;
        }

        public virtual string GetText() {
            return "";
        }

        public virtual int GetPos() {
            return offset;
        }

        public virtual void SetLen(int length) {
            this.length = length;
        }

        public virtual int GetLen() {
            return length;
        }
    }
}
