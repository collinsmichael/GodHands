using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    public class InMemory : BaseClass {
        private Record rec;
        private string display_text = "";

        public InMemory(BaseClass parent, string url, int pos, Record rec):
        base(parent, url, pos) {
            this.rec = rec;
            display_text = url.Substring(url.LastIndexOf('/')+1);
            Model.Add(url, this);
            Publisher.Register(this);
        }

        public override string GetText() {
            return display_text;
        }

        public void SetText(string text) {
            display_text = text;
        }

        public Record GetRec() {
            return rec;
        }

        public void SetRec(Record rec) {
            this.rec = rec;
        }

        public override int GetPos() {
            int address = rec.LbaData*2048;
            int offset = base.GetPos();
            return address + offset;
        }

        public virtual byte[] RawBytes() {
            int len = GetLen();
            byte[] raw = new byte[len];
            if (raw != null) {
                if (!RamDisk.Get(GetPos(), len, raw)) {
                    return null;
                }
            }
            return raw;
        }

        public virtual bool ExportRaw(string path) {
            byte[] raw = RawBytes();
            if (raw == null) {
                return false;
            }
            File.WriteAllBytes(path, raw);
            return true;
        }

        public virtual bool ImportRaw(string path) {
            byte[] raw = File.ReadAllBytes(path);
            if (raw == null) {
                return Logger.Fail("File not found "+path);
            }

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

        public virtual string GetExportFilter() {
            return "VS Files|*.VS|"
                 + "ARM Files|*.ARM|BIN Files|*.BIN|"
                 + "DAT Files|*.DAT|MPD Files|*.MPD|"
                 + "PRG Files|*.PRG|WEP Files|*.WEP|"
                 + "SEQ Files|*.SEQ|SHP Files|*.SHP|"
                 + "ZND Files|*.ZND|ZUD Files|*.ZUD|"
                 + "All Files|*.*";
        }
        public virtual string GetExportName() {
            return null;
        }
    }
}
