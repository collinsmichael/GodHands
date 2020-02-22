using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Folder : Binary {
        private List<Record> records = new List<Record>();

        public Folder(Record parent, string url, int length):
        base(parent, url, length) {
            //Publisher.Register(this);
            //if (parent != null) {
            //    Iso9660.ReadFile(parent);
            //}
            //int ptr = parent.LbaData*2048;
            //int delta = 0;
            //delta += RamDisk.GetU8(ptr+delta); // skip current directory
            //delta += RamDisk.GetU8(ptr+delta); // skip parent directory
            //for (int i = 0; delta < length; i++) {
            //    string key = url+"/"+i;
            //    Record rec = new Record(this, key, ptr+delta);
            //    int len = rec.LenRecord;
            //    if (len == 0) {
            //        delta = ((delta/2048)+1)*2048;
            //    } else {
            //        records.Add(rec);
            //        Publisher.Register(rec);
            //        delta += len;
            //    }
            //}
        }
    }
}
