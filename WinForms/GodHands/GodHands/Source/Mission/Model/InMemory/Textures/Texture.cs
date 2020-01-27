using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Texture : InMemory {
        private int len;
        private byte[] buf;
        private int id;

        public Texture(string url, int pos, int len, int id, DirRec rec):
        base(url, pos, rec) {
            this.id = id;
            FileLength = this.len = len;
            this.buf = new byte[len];
            RamDisk.Get(pos, len, buf);
            QueryFile();
        }

        // sizeof BMP file
        public override int GetLen() {
            QueryFile();
            return 0x436 + Width*Height;
        }

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

        public virtual Image ToImage(bool fix_aspec) {
            QueryFile();
            int pos = GetPos();
            int len = RamDisk.GetS16(pos+8);
            int lut = pos + 8;
            int w = (IsLookUpTable()) ? Width : Width*2;
            int h = Height;
            int s = (fix_aspec) ? 2 : 1;
            Bitmap bmp = new Bitmap(w*s, h, PixelFormat.Format32bppArgb);
            if (Height <= 4) {
                for (int x = 0; x < w; x++) {
                    for (int y = 0; y < h; y++) {
                        int i = y*w + x;
                        int rgb = RamDisk.GetU16(lut + 12 + i*s);
                        int r = ((rgb/0x001) % 32)*0x41/8;
                        int g = ((rgb/0x020) % 32)*0x41/8;
                        int b = ((rgb/0x400) % 32)*0x41/8;
                        if (fix_aspec) {
                            bmp.SetPixel(x*2 + 0, y, Color.FromArgb(r,g,b));
                            bmp.SetPixel(x*2 + 1, y, Color.FromArgb(r,g,b));
                        } else {
                            bmp.SetPixel(x, y, Color.FromArgb(r,g,b));
                        }
                    }
                }
            } else {
                for (int x = 0; x < w; x++) {
                    for (int y = 0; y < h; y++) {
                        int i = y*w + x;
                        int c = RamDisk.GetU8(lut + 12 + i);
                        if (fix_aspec) {
                            bmp.SetPixel(x*2 + 0, y, Color.FromArgb(c,c,c));
                            bmp.SetPixel(x*2 + 1, y, Color.FromArgb(c,c,c));
                        } else {
                            bmp.SetPixel(x, y, Color.FromArgb(c,c,c));
                        }
                    }
                }
            }
            return (Image)bmp;
        }

        public override bool ExportRaw(string path) {
            Bitmap bmp = ToImage(false) as Bitmap;
            string ext = Path.GetExtension(path).ToUpper();
            switch (ext) {
            case "PNG":  bmp.Save(path, ImageFormat.Png);  break;
            case "BMP":  bmp.Save(path, ImageFormat.Bmp);  break;
            default:
            case "TIM":
                byte[] raw = RawBytes();
                if (raw == null) {
                    return false;
                }
                File.WriteAllBytes(path, raw);
                break;
            }
            return true;
        }

        public override string GetExportFilter() {
            return "Uncompressed Image Files|*.TIM;*.BMP;*.PNG|"
                    + "TIM Files|*.TIM|"
                    + "BMP Files|*.BMP|"
                    + "PNG Files|*.PNG|"
                    + "All Files|*.*";
        }

        public override string GetExportName() {
            DirRec rec = GetRec();
            string name = rec.GetFileName();
            string[] parts = name.Split(new char[] {'.'});
            return parts[0]+"-IMAGE-"+id.ToString("D2");
        }
    }
}
