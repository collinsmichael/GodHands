using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Zone : BaseClass {
        private DirRec rec;
        private ZND znd;
        public List<Room> rooms = new List<Room>();
        public List<Actor> actors = new List<Actor>();
        public List<Texture> images = new List<Texture>();

        public Zone(string url, int pos, DirRec rec) : base(url, pos) {
            this.rec = rec;
            znd = Model.znds[rec.GetUrl()];
        }

        public DirRec GetRec() {
            return rec;
        }

        public bool OpenZone() {
            Iso9660.ReadFile(rec);
            int pos = znd.GetPos();

            int ptr_mpd = RamDisk.GetS32(pos+0x00);
            int len_mpd = RamDisk.GetS32(pos+0x04);
            int num_mpd = len_mpd/8;
            for (int i = 0; i < num_mpd; i++) {
                int lba = RamDisk.GetS32(pos + ptr_mpd + 8*i);
                try {
                    DirRec mpd = Iso9660.GetByLba(lba);
                    Room obj = new Room(mpd.GetUrl(), mpd.LbaData*2048, mpd);
                    rooms.Add(obj);
                    Model.Add(GetUrl()+"/Room/"+i, obj);
                    Publisher.Register(obj);
                } catch {}
            }

            int ptr_zud = RamDisk.GetS32(pos+0x08);
            int len_zud = RamDisk.GetS32(pos+0x0C);
            int num_zud = RamDisk.GetS32(pos + ptr_zud);
            for (int i = 0; i < num_zud; i++) {
                try {
                    int lba = RamDisk.GetS32(pos + ptr_zud + 4 + 8*i);
                    int npc = pos + ptr_zud + 4 + 8*num_zud + 0x464*i;
                    string key = GetUrl()+"/Actors/Actor_"+i;
                    DirRec zud = Iso9660.GetByLba(lba);
                    Actor obj = new Actor(key, npc, zud);
                    actors.Add(obj);
                    Model.Add(key, obj);
                    Publisher.Register(obj);
                } catch {}
            }

            int ptr_tim = RamDisk.GetS32(pos+0x10);
            int len_tim = RamDisk.GetS32(pos+0x14);
            int num_tim = RamDisk.GetS32(pos + ptr_tim + 0x10);
            int ptr = ptr_tim + 0x14;
            for (int i = 0; i < num_tim; i++) {
                int len = RamDisk.GetS32(pos + ptr);
                string key = GetUrl()+"/Images/Image_"+i;
                Texture obj = new Texture(key, pos+ptr+4, len);
                images.Add(obj as Texture);
                Model.Add(key, obj);
                Publisher.Register(obj);
                ptr += len + 4;
            }
            return true;
        }
    }
}
