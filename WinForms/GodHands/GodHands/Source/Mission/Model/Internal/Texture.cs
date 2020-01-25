using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Texture : BaseClass {
        private int len;
        private byte[] buf;
        public bool IsLookUpTable() {
            int Height = RamDisk.GetU16(GetPos() + 18);
            return (Height <= 4);
        }

        public int FileLength { get; set; }
        public int FileTag { get; set; }
        public int FileVersion { get; set; }
        public int FileBpp { get; set; }

        public int ClutLength { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture(string url, int pos, int len) : base(url, pos) {
            FileLength = this.len = len;
            this.buf = new byte[len];
            RamDisk.Get(pos, len, buf);
            QueryFile();
        }

        public void QueryFile() {
            int pos = GetPos();
            int lut = pos + 8;
            int bpp = RamDisk.GetU8(pos+4) % 4;
            int[] bits = new int[] { 4, 8, 16, 24 };
            FileBpp = bits[bpp];
            FileTag = RamDisk.GetU8(pos+0);
            FileVersion = RamDisk.GetU8(pos+1);


            ClutLength = RamDisk.GetS32(lut+0);
            PosX = RamDisk.GetU16(lut+4);
            PosY = RamDisk.GetU16(lut+6);
            Height = RamDisk.GetU16(lut+10);
            Width = RamDisk.GetU16(lut+8);
        }

        public virtual Image ToImage() {
            QueryFile();
            int pos = GetPos();
            int len = RamDisk.GetS16(pos+8);
            int lut = pos + 8;
            int w = (IsLookUpTable()) ? Width : Width*2;
            int h = Height;
            Bitmap bmp = new Bitmap(w*2, h, PixelFormat.Format32bppArgb);
            if (Height <= 4) {
                for (int x = 0; x < w; x++) {
                    for (int y = 0; y < h; y++) {
                        int i = y*w + x;
                        int rgb = RamDisk.GetU16(lut + 12 + i*2);
                        int r = ((rgb/0x001) % 32)*0x41/8;
                        int g = ((rgb/0x020) % 32)*0x41/8;
                        int b = ((rgb/0x400) % 32)*0x41/8;
                        bmp.SetPixel(x*2 + 0, y, Color.FromArgb(r,g,b));
                        bmp.SetPixel(x*2 + 1, y, Color.FromArgb(r,g,b));
                    }
                }
            } else {
                for (int x = 0; x < w; x++) {
                    for (int y = 0; y < h; y++) {
                        int i = y*w + x;
                        int c = RamDisk.GetU8(lut + 12 + i);
                        bmp.SetPixel(x*2 + 0, y, Color.FromArgb(c,c,c));
                        bmp.SetPixel(x*2 + 1, y, Color.FromArgb(c,c,c));
                    }
                }
            }
            return (Image)bmp;
        }
    }
}
