using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNamesList {

        public bool Load() {
            return true;
        }

        public bool Clear() {
            return true;
        }

        public List<string> GetList() {
            List<string> list = new List<string>();
            DirRec bin = Model.GetRec("ITEMNAME.BIN");
            if (bin != null) {
                int pos = bin.LbaData*2048;
                for (int i = 0; i < 256; i++) {
                    byte[] kildean = new byte[0x18];
                    RamDisk.Get(pos + i*0x18, 0x18, kildean);
                    string str = Kildean.ToAscii(kildean);
                    list.Add(str);
                }
            }
            return list;
        }

        public string GetName(int index) {
            DirRec bin = Model.GetRec("ITEMNAME.BIN");
            if (bin != null) {
                int pos = bin.LbaData*2048;
                byte[] kildean = new byte[0x18];
                RamDisk.Get(pos + index*0x18, 0x18, kildean);
                return Kildean.ToAscii(kildean);
            }
            return "";
        }

        public int GetIndexByName(string name) {
            DirRec bin = Model.GetRec("ITEMNAME.BIN");
            if (bin != null) {
                int pos = bin.LbaData*2048;
                for (int i = 0; i < 256; i++) {
                    byte[] kildean = new byte[0x18];
                    RamDisk.Get(pos + i*0x18, 0x18, kildean);
                    string str = Kildean.ToAscii(kildean);
                    if (str == name) {
                        return i;
                    }
                }
            }
            return 0;
        }
    }
}
