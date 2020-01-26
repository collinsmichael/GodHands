using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Zone : InMemory {
        private ZND znd;
        public List<Room> rooms = new List<Room>();
        public List<Actor> actors = new List<Actor>();
        public List<ActorBodyPart> bodyparts = new List<ActorBodyPart>();
        public List<ActorArmour> armours = new List<ActorArmour>();
        public List<ActorShield> shields = new List<ActorShield>();
        public List<ActorAccessory> accessories = new List<ActorAccessory>();
        public List<Texture> images = new List<Texture>();

        public Zone(string url, int pos, DirRec rec):
        base(url, pos, rec) {
            znd = Model.znds[rec.GetUrl()];
        }

        public bool AddRoom(TreeNode root, int id, int pos) {
            int lba = RamDisk.GetS32(pos + 8*id);
            try {
                string url = GetUrl()+"/Room/"+id;
                DirRec mpd = Iso9660.GetByLba(lba);
                Room obj = new Room(mpd.GetUrl(), mpd.LbaData*2048, mpd);
                rooms.Add(obj);
                Model.Add(url, obj);
                Publisher.Register(obj);

                TreeNode node = root.Nodes.Add(url, mpd.GetFileName(), 1, 1);
                node.Nodes.Add(url+"/Geometry", "Geometry", 28, 28);
                node.Nodes.Add(url+"/Collisions", "Collisions", 31, 31);
                node.Nodes.Add(url+"/Lighting", "Lighting", 32, 32);
                node.Nodes.Add(url+"/Doors", "Doors", 33, 34);
                node.Nodes.Add(url+"/Enemies", "Enemies", 35, 35);
                node.Nodes.Add(url+"/Script", "Script", 36, 36);
                node.Nodes.Add(url+"/Treasure", "Treasure", 37, 37);
                return true;
            } catch {
                return false;
            }
        }

        public bool AddActor(TreeNode root, int id, int pos, int lba) {
            try {
                // ************************************************************
                // Add the actor
                string url = GetUrl()+"/Actors/Actor_"+id;
                string znd_file = GetRec().GetFileName();
                DirRec zud = Iso9660.GetByLba(lba);
                Actor obj = new Actor(url, pos, GetRec(), zud);
                actors.Add(obj);
                Model.Add(url, obj);
                Publisher.Register(obj);
                TreeNode node = root.Nodes.Add(url, obj.Name, 2, 2);

                // ************************************************************
                // Add the ZUD file
                TreeNode tv_model = node.Nodes.Add(url+"/Model", znd_file, 28, 28);
                tv_model.Nodes.Add(url+"/Model/SHP", "SHP", 28, 28);
                tv_model.Nodes.Add(url+"/Model/WEP", "WEP", 28, 28);
                tv_model.Nodes.Add(url+"/Model/SEQ", "SEQ Common", 29, 29);
                tv_model.Nodes.Add(url+"/Model/SEQ", "SEQ Battle", 29, 29);

                // ************************************************************
                // Add bodyparts
                TreeNode tv_body = node.Nodes.Add(url+"/BodyParts", "BodyParts", 5, 5);
                for (int j = 0; j < 6; j++) {
                    string[] part_name = new string[] {
                        "L.ARM", "R.ARM", "HEAD", "BODY", "LEGS", "OTHER",
                    };

                    int ptr_part = pos + 0x238 + j*0x5C;
                    string k1 = url+"/BodyParts/"+part_name[j];
                    ActorBodyPart part = new ActorBodyPart(k1, ptr_part, GetRec());
                    bodyparts.Add(part);
                    Model.Add(k1, part);
                    Publisher.Register(part);
                }
                tv_body.Nodes.Add(url+"/BodyParts/L.ARM", "L.ARM", 8, 8);
                tv_body.Nodes.Add(url+"/BodyParts/R.ARM", "R.ARM", 8, 8);
                tv_body.Nodes.Add(url+"/BodyParts/HEAD", "HEAD", 6, 6);
                tv_body.Nodes.Add(url+"/BodyParts/BODY", "BODY", 7, 7);
                tv_body.Nodes.Add(url+"/BodyParts/LEGS", "LEGS", 9, 9);
                tv_body.Nodes.Add(url+"/BodyParts/OTHER", "OTHER", 5, 5);

                // ************************************************************
                // Add armours
                TreeNode tv_equip = node.Nodes.Add(url+"/Equip", "Equipment", 39, 39);
                for (int j = 0; j < 6; j++) {
                    string[] part_name = new string[] {
                        "L.ARM", "R.ARM", "HEAD", "BODY", "LEGS", "OTHER",
                    };

                    int ptr_part = pos + 0x238 + j*0x5C + 0x20;
                    string k2 = url+"/Equip/"+part_name[j];
                    ActorArmour armour = new ActorArmour(k2, ptr_part, GetRec());
                    armours.Add(armour);
                    Model.Add(k2, armour);
                    Publisher.Register(armour);
                }
                tv_equip.Nodes.Add(url+"/Equip/L.ARM", "L.ARM", 8, 8);
                tv_equip.Nodes.Add(url+"/Equip/R.ARM", "R.ARM", 8, 8);
                tv_equip.Nodes.Add(url+"/Equip/HEAD", "HEAD", 6, 6);
                tv_equip.Nodes.Add(url+"/Equip/BODY", "BODY", 7, 7);
                tv_equip.Nodes.Add(url+"/Equip/LEGS", "LEGS",  9, 9);
                tv_equip.Nodes.Add(url+"/Equip/OTHER", "OTHER",  5, 5);

                // ************************************************************
                // Add weapon
                TreeNode tv_weapon = node.Nodes.Add(url+"/Weapon", "Weapon", 12, 12);
                tv_weapon.Nodes.Add(url+"/Weapon/Blade", "Blade", 13, 13);
                tv_weapon.Nodes.Add(url+"/Weapon/Grip",  "Grip",  14, 14);
                tv_weapon.Nodes.Add(url+"/Weapon/Gem1",  "Gem1",  24, 24);
                tv_weapon.Nodes.Add(url+"/Weapon/Gem2",  "Gem2",  25, 25);
                tv_weapon.Nodes.Add(url+"/Weapon/Gem3",  "Gem3",  26, 26);

                // ************************************************************
                // Add shield
                TreeNode tv_shield = node.Nodes.Add(url+"/Shield", "Shield", 11, 11);
                ActorShield shield = new ActorShield(url+"/Shield", pos + 0x140, GetRec());
                shields.Add(shield);
                Model.Add(url+"/Shield", shield);
                Publisher.Register(shield);

                tv_shield.Nodes.Add(url+"/Shield/Gem1",  "Gem1",  24, 24);
                tv_shield.Nodes.Add(url+"/Shield/Gem2",  "Gem2",  25, 25);
                tv_shield.Nodes.Add(url+"/Shield/Gem3",  "Gem3",  26, 26);

                // ************************************************************
                // Add accessory
                TreeNode tv_accessory = node.Nodes.Add(url+"/Accessory", "Accessory", 10, 10);
                ActorAccessory accessory = new ActorAccessory(url+"/Accessory", pos + 0x204, GetRec());
                accessories.Add(accessory);
                Model.Add(url+"/Accessory", accessory);
                Publisher.Register(accessory);
                return true;
            } catch {
                return false;
            }
        }

        public bool OpenZone(TreeView treeview) {
            Iso9660.ReadFile(GetRec());
            int pos = znd.GetPos();

            treeview.Nodes.Clear();
            TreeNode root = new TreeNode("Zone", 0, 0);
            TreeNode tv_rooms = root.Nodes.Add("Zone/Rooms", "Rooms", 1, 1);
            TreeNode tv_actor = root.Nodes.Add("Zone/Actors", "Actors", 2, 2);
            TreeNode tv_image = root.Nodes.Add("Zone/Images", "Images", 3, 3);
            tv_rooms.ToolTipText = "List of rooms";
            tv_actor.ToolTipText = "List of actors";
            tv_image.ToolTipText = "Texture pack";
            treeview.Nodes.Add(root);
            root.ToolTipText = "Zone";
            root.Expand();

            int ptr_mpd = RamDisk.GetS32(pos+0x00);
            int len_mpd = RamDisk.GetS32(pos+0x04);
            int num_mpd = len_mpd/8;
            for (int i = 0; i < num_mpd; i++) {
                AddRoom(tv_rooms, i, pos + ptr_mpd);
            }

            int ptr_zud = RamDisk.GetS32(pos+0x08);
            int len_zud = RamDisk.GetS32(pos+0x0C);
            int num_zud = RamDisk.GetS32(pos + ptr_zud);
            for (int i = 0; i < num_zud; i++) {
                int ptr = ptr_zud + 4 + 8*num_zud + 0x464*i;
                int lba = RamDisk.GetS32(pos + ptr_zud + 4 + 8*i);
                AddActor(tv_actor, i, ptr, lba);
            }

            int ptr_tim = RamDisk.GetS32(pos+0x10);
            int len_tim = RamDisk.GetS32(pos+0x14);
            int num_tim = RamDisk.GetS32(pos + ptr_tim + 0x10);
            int ptrx = ptr_tim + 0x14;
            for (int i = 0; i < num_tim; i++) {
                int len = RamDisk.GetS32(pos + ptrx);
                try {
                    string key = GetUrl()+"/Images/Image_"+i;
                    Texture obj = new Texture(key, ptrx+4, len, GetRec());
                    images.Add(obj as Texture);
                    Model.Add(key, obj);
                    Publisher.Register(obj);
                } catch {}
                ptrx += len + 4;
            }

            foreach (Texture img in images) {
                int index = images.IndexOf(img);
                string text = "Image_"+index.ToString("D2");
                int icon = (img.IsLookUpTable()) ? 4 : 3;
                tv_image.Nodes.Add(img.GetUrl(), text, icon, icon);
            }
            return true;
        }
    }
}
