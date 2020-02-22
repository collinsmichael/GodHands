using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Binary : BaseClass {
        public Binary(Record parent, string url, int length):
        base(parent, url, 0, length) {
        }

        public override int GetPos() {
            Record record = parent as Record;
            if (record != null) {
                return record.LbaData*2048 + offset;
            }
            return offset;
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
    }
}
