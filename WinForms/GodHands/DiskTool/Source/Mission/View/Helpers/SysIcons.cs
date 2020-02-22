using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public static class SysIcons {
        public static ImageList small = null;
        public static ImageList large = null;
        public static bool IconsLoaded = false;
        public static bool UsingSysIcons = false;

        public static bool GetSysIcons(Control widget) {
            try {
                ShellIcons.GetShellIcons(widget);
                UsingSysIcons = true;
            } catch (Exception e) {
                Logger.Info("Cannot access system icons "+e.Message);
                UsingSysIcons = false;

                small = View.ImageListFromDir("/img/files");
                large = View.ImageListFromDir("/img/files");
                if (widget as ListView != null) {
                    (widget as ListView).SmallImageList = small;
                    (widget as ListView).LargeImageList = large;
                }
                if (widget as TreeView != null) {
                    (widget as TreeView).ImageList = small;
                }
            }
            return true;
        }

        public static int GetDirIconIndex(bool selected) {
            if (UsingSysIcons) {
                return ShellIcons.GetDirIconIndex(selected);
            } else {
                return (selected) ? 0x26 : 0x27;
            }
        }

        public static int GetDiskIconIndex() {
            if (UsingSysIcons) {
                return ShellIcons.GetFileIconIndex("test.iso");
            } else {
                return 0x25;
            }
        }

        public static int GetFileIconIndex(string file) {
            if (UsingSysIcons) {
                return ShellIcons.GetFileIconIndex(file);
            } else {
                if (!file.Contains('.')) {
                    return 0x24;
                }
                string ext = file.Substring(file.LastIndexOf('.'));
                switch (ext[1]) {
                case '0': return 0x00; case '1': return 0x01;
                case '2': return 0x02; case '3': return 0x03;
                case '4': return 0x04; case '5': return 0x05;
                case '6': return 0x06; case '7': return 0x07;
                case '8': return 0x08; case '9': return 0x09;
                case 'A': return 0x0A; case 'B': return 0x0B;
                case 'C': return 0x0C; case 'D': return 0x0D;
                case 'E': return 0x0E; case 'F': return 0x0F;
                case 'G': return 0x10; case 'H': return 0x11;
                case 'I': return 0x12; case 'J': return 0x13;
                case 'K': return 0x14; case 'L': return 0x15;
                case 'M': return 0x16; case 'N': return 0x17;
                case 'O': return 0x18; case 'P': return 0x19;
                case 'Q': return 0x1A; case 'R': return 0x1B;
                case 'S': return 0x1C; case 'T': return 0x1D;
                case 'U': return 0x1E; case 'V': return 0x1F;
                case 'W': return 0x20; case 'X': return 0x21;
                case 'Y': return 0x22; case 'Z': return 0x23;
                }
                return 0;
            }
        }
    }
}
