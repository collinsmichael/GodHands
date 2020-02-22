using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class BaseClass {
        public BaseClass parent;
        public string Key;
        public string url;
        public int offset;
        public int length;

        public BaseClass(BaseClass parent, string url, int offset) {
            this.parent = parent;
            this.url = url;
            this.offset = offset;
        }

        public BaseClass(BaseClass parent, string url, int offset, int length) {
            this.parent = parent;
            this.url = url;
            this.offset = offset;
            this.length = length;
        }

        public int Pos {
            get { return (parent != null) ? parent.Pos + offset : offset; }
            set { }
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
