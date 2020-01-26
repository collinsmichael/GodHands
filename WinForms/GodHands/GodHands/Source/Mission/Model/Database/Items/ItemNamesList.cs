using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class ItemNamesList {
        private List<MiscItem> items = new List<MiscItem>();

        public bool Load() {
            items.Clear();
            Iso9660.ReadFile(Model.GetRec("ITEMNAME.BIN"));
            Iso9660.ReadFile(Model.GetRec("ITEMHELP.BIN"));
            DirRec rec = Model.GetRec("ITEMNAME.BIN");
            for (int i = 0; i < 512; i++) {
                string key = "DB:Items/Item_"+i;
                MiscItem obj = new MiscItem(key, 0, i, rec);
                items.Add(obj);
                Model.Add(key, obj);
                Publisher.Register(obj);
            }
            return true;
        }

        public bool Clear() {
            items.Clear();
            return true;
        }

        public List<string> GetList() {
            List<string> list = new List<string>();
            DirRec bin = Model.GetRec("ITEMNAME.BIN");
            if (bin != null) {
                int pos = bin.LbaData*2048;
                for (int i = 0; i < 512; i++) {
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
                for (int i = 0; i < 512; i++) {
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

        public bool Open(TreeNode root) {
            foreach (MiscItem item in items) {
                string index = items.IndexOf(item).ToString("X3");
                string key = item.GetUrl();
                string name = "(" + index + ") " + item.Name;
                root.Nodes.Add(key, name, 2, 2);
            }
            return true;
        }
    }
}
