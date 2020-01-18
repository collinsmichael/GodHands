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

        public int FileLength { get; set; }
        public int FileTag { get; set; }
        public int FileVersion { get; set; }
        public int FileBpp { get; set; }

        public int ColorMap { get; set; }
        public int ClutLength { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Texture(string url, int pos, int len) : base(url, pos) {
            FileLength = this.len = len;
            this.buf = new byte[len];
            RamDisk.Get(pos, len, buf);
        }

        public virtual Image ToImage(Color[] color) {
            int pos = GetPos();
            int len = RamDisk.GetS16(pos+8);
            int lut = pos + 8;
            int img = pos + len + 12;

            FileTag = RamDisk.GetU8(pos+0);
            FileVersion = RamDisk.GetU8(pos+1);

            int bpp = RamDisk.GetU8(pos+4) % 4;
            int[] bits = new int[] { 4, 8, 16, 24 };
            FileBpp = bits[bpp];

            ClutLength = RamDisk.GetS32(lut+0);
            PosX = RamDisk.GetU16(lut+4);
            PosY = RamDisk.GetU16(lut+6);
            Width = RamDisk.GetU16(lut+8)*2;
            Height = RamDisk.GetU16(lut+10);

            int w = Width;
            int h = Height;
            Bitmap bmp = new Bitmap(w*2, h, PixelFormat.Format32bppArgb);
            if (color == null) {
                for (int x = 0; x < w; x++) {
                    for (int y = 0; y < h; y++) {
                        int i = y*w + x;
                        int c = RamDisk.GetU8(lut + 12 + i);
                        Color rgb = Color.FromArgb(c,c,c);
                        bmp.SetPixel(x*2+0, y, rgb);
                        bmp.SetPixel(x*2+1, y, rgb);
                    }
                }
            } else {
                int n = color.Length;
                for (int x = 0; x < w; x++) {
                    for (int y = 0; y < h; y++) {
                        int i = y*w + x;
                        int c = RamDisk.GetU8(lut + 12 + i);
                        Color rgb = color[(ColorMap + c) % n];
                        bmp.SetPixel(x*2+0, y, rgb);
                        bmp.SetPixel(x*2+1, y, rgb);
                    }
                }
            }
            return (Image)bmp;
        }
    }

    public class Clut : Texture {
        public Clut(string url, int pos, int len) : base(url, pos, len) {
        }

        public override Image ToImage(Color[] notused) {
            int pos = GetPos();
            int len = RamDisk.GetS16(pos+8);
            int lut = pos + 8;
            int img = pos + len + 12;

            FileTag = RamDisk.GetU8(pos+0);
            FileVersion = RamDisk.GetU8(pos+1);

            int bpp = RamDisk.GetU8(pos+4) % 4;
            int[] bits = new int[] { 4, 8, 16, 24 };
            FileBpp = bits[bpp];

            ClutLength = RamDisk.GetS32(lut+0);
            PosX = RamDisk.GetU16(lut+4);
            PosY = RamDisk.GetU16(lut+6);
            Width = RamDisk.GetU16(lut+8);
            Height = RamDisk.GetU16(lut+10);

            int w = Width;
            int h = Height;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    int i = y*w + x;
                    int rgb = RamDisk.GetU16(lut + 12 + i*2);
                    int r = ((rgb/0x001) % 32)*0x41/8;
                    int g = ((rgb/0x020) % 32)*0x41/8;
                    int b = ((rgb/0x400) % 32)*0x41/8;
                    bmp.SetPixel(x, y, Color.FromArgb(r,g,b));
                }
            }
            return (Image)bmp;
        }

        public Color[] ToRgb() {
            int pos = GetPos();
            int len = RamDisk.GetS16(pos+8);
            int lut = pos + 8;
            int img = pos + len + 12;

            int bpp = RamDisk.GetU8(pos+4) % 4;
            int[] bits = new int[] { 4, 8, 16, 24 };
            FileBpp = bits[bpp];
            FileTag = RamDisk.GetU8(pos+0);
            FileVersion = RamDisk.GetU8(pos+1);

            ClutLength = RamDisk.GetS32(lut+0);
            PosX = RamDisk.GetU16(lut+4);
            PosY = RamDisk.GetU16(lut+6);
            Width = RamDisk.GetU16(lut+8);
            Height = RamDisk.GetU16(lut+10);

            Color[] map = new Color[256];
            int w = Width;
            int h = Height;
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    int i = y*w + x;
                    int rgb = RamDisk.GetU16(lut + 12 + i*2);
                    int r = ((rgb/0x001) % 32)*0x41/8;
                    int g = ((rgb/0x020) % 32)*0x41/8;
                    int b = ((rgb/0x400) % 32)*0x41/8;
                    map[i] = Color.FromArgb(r,g,b);
                }
            }
            return map;
        }
    }
}
