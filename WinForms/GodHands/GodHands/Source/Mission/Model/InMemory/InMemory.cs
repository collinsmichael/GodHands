using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class InMemory : BaseClass {
        private DirRec rec;
        private string display_text = "";

        public InMemory(string url, int pos, DirRec rec) : base(url, pos) {
            this.rec = rec;
            display_text = url.Substring(url.LastIndexOf('/'));
        }

        public override string GetText() {
            return display_text;
        }

        public void SetText(string text) {
            display_text = text;
        }

        public DirRec GetRec() {
            return rec;
        }

        public void SetRec(DirRec rec) {
            this.rec = rec;
        }

        public override int GetPos() {
            int address = rec.LbaData*2048;
            int offset = base.GetPos();
            return address + offset;
        }

        public override int GetLen() {
            return 0;
        }

        public virtual byte[] ExportRaw() {
            int len = GetLen();
            byte[] raw = new byte[len+16];
            if (!RamDisk.Get(GetPos(), len, raw)) {
                return null;
            }
            return raw;
        }

        public virtual bool ImportRaw(byte[] raw) {
            int len = GetLen();
            byte[] buf = new byte[len];
            if (len < raw.Length) {
                string msg = "Data will be truncated.\r\nProceed?";
                if (!Logger.YesNoCancel(msg)) {
                    return false;
                }
                buf = raw;
            } else if (len > raw.Length) {
                string msg = "Data will be padded with zeroes.\r\nProceed?";
                if (!Logger.YesNoCancel(msg)) {
                    return false;
                }
                raw.CopyTo(buf, 0);
            } else {
                buf = raw;
            }
            return UndoRedo.Exec(new BindArray(this, GetPos(), len, buf));
        }
    }
}
