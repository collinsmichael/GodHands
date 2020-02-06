using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Room : InMemory {
        private MPD mpd;
        private Zone zone;
        private RoomMainSection main;
        private RoomClearedSection cleared;
        private RoomScriptSection script;
        private RoomDoorSection doors;
        private RoomEnemySection enemies;
        private RoomTreasureSection treasure;

        public Room(string url, int pos, DirRec rec,
        Zone zone, int zoneid, int roomid):
        base(url, pos, rec) {
            mpd = Model.mpds[rec.GetUrl()];
            this.zone = zone;
            ZoneId = zoneid;
            RoomId = roomid;
            Name = rec.GetFileName();
        }

        public override int GetLen() {
            return 0;
        }

        public string Name { get; set; }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int ZoneId { get; set; }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int RoomId { get; set; }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int ptrMainSection {
            get { return RamDisk.GetS32(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindS32(this, 0x00, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int lenMainSection {
            get { return RamDisk.GetS32(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindS32(this, 0x04, value)); }
        }
        
        [ReadOnly(true)][Category(" INTERNAL")]
        public int ptrClearedSection {
            get { return RamDisk.GetS32(GetPos()+0x08); }
            set { UndoRedo.Exec(new BindS32(this, 0x08, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int lenClearedSection {
            get { return RamDisk.GetS32(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindS32(this, 0x0C, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int ptrScriptSection {
            get { return RamDisk.GetS32(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindS32(this, 0x10, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int lenScriptSection {
            get { return RamDisk.GetS32(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindS32(this, 0x14, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int ptrDoorSection {
            get { return RamDisk.GetS32(GetPos()+0x18); }
            set { UndoRedo.Exec(new BindS32(this, 0x18, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int lenDoorSection {
            get { return RamDisk.GetS32(GetPos()+0x1C); }
            set { UndoRedo.Exec(new BindS32(this, 0x1C, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int ptrEnemySection {
            get { return RamDisk.GetS32(GetPos()+0x20); }
            set { UndoRedo.Exec(new BindS32(this, 0x20, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int lenEnemySection {
            get { return RamDisk.GetS32(GetPos()+0x24); }
            set { UndoRedo.Exec(new BindS32(this, 0x24, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int ptrTreasureSection {
            get { return RamDisk.GetS32(GetPos()+0x28); }
            set { UndoRedo.Exec(new BindS32(this, 0x28, value)); }
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int lenTreasureSection {
            get { return RamDisk.GetS32(GetPos()+0x2C); }
            set { UndoRedo.Exec(new BindS32(this, 0x2C, value)); }
        }

        public bool AddRoom(TreeNode root, string url, int id, int pos) {
            main = new RoomMainSection(url+"/Main", ptrMainSection, lenMainSection, GetRec());
            main.OpenSection(root.Nodes.Add(url+"/Main", "Main", 28, 28));
            //root.Nodes.Add(url+"/Geometry", "Geometry", 28, 28);
            //root.Nodes.Add(url+"/Lighting", "Lighting", 32, 32);
            //root.Nodes.Add(url+"/Collisions", "Collisions", 31, 31);

            cleared = new RoomClearedSection(url+"/Cleared", ptrClearedSection, lenClearedSection, GetRec());
            cleared.OpenSection(root.Nodes.Add(url+"/Cleared", "Cleared", 31, 31));

            script = new RoomScriptSection(url+"/Script", ptrScriptSection, lenScriptSection, GetRec());
            script.OpenSection(root.Nodes.Add(url+"/Script", "Script", 36, 36));

            doors = new RoomDoorSection(url+"/Doors", ptrDoorSection, lenDoorSection, GetRec());
            doors.OpenSection(root.Nodes.Add(url+"/Doors", "Doors", 33, 34));

            enemies = new RoomEnemySection(url+"/Enemies", ptrEnemySection, lenEnemySection, GetRec(), zone, this);
            enemies.OpenSection(root.Nodes.Add(url+"/Enemies", "Enemies", 35, 35));

            treasure = new RoomTreasureSection(url+"/Treasure", ptrTreasureSection, lenTreasureSection, GetRec());
            treasure.OpenSection(root.Nodes.Add(url+"/Treasure", "Treasure", 37, 37));
            return true;
        }
    }
}
