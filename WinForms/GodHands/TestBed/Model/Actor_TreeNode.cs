using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestBed {
    public class Tim {
        public bool IsColorLookUp = false;
        public int PosX = 0;
        public int PosY = 0;
        public int Width = 0;
        public int Height = 0;

        void Import(string path) {
            Image file = Image.FromFile(path, false);
            Width = file.Width;
            Height = file.Width;
            if (file.PixelFormat == PixelFormat.Format8bppIndexed) {
                IsColorLookUp = false;
            }
        }
    }
}
