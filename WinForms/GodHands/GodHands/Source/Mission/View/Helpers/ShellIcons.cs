using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public static class ShellIcons {
        private static int dir_select = -1;
        private static int dir_normal = -1;
        private static int bin_normal = -1;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public int dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbSizeFileInfo, int uFlags);
        [DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
		
        public const int SHGFI_LARGEICON         = 0x000000000;
        public const int SHGFI_SMALLICON         = 0x000000001;
        public const int SHGFI_OPENICON          = 0x000000002;
        public const int SHGFI_SHELLICONSIZE     = 0x000000004;
        public const int SHGFI_PIDL              = 0x000000008;
        public const int SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const int SHGFI_ICON              = 0x000000100;
        public const int SHGFI_DISPLAYNAME       = 0x000000200;
        public const int SHGFI_TYPENAME          = 0x000000400;
        public const int SHGFI_ATTRIBUTES        = 0x000000800;
        public const int SHGFI_ICONLOCATION      = 0x000001000;
        public const int SHGFI_EXETYPE           = 0x000002000;
        public const int SHGFI_SYSICONINDEX      = 0x000004000;
        public const int SHGFI_LINKOVERLAY       = 0x000008000;
        public const int SHGFI_SELECTED          = 0x000010000;

        public const int FILE_ATTRIBUTE_READONLY            = 0x00000001;
        public const int FILE_ATTRIBUTE_HIDDEN              = 0x00000002;
        public const int FILE_ATTRIBUTE_SYSTEM              = 0x00000004;
        public const int FILE_ATTRIBUTE_DIRECTORY           = 0x00000010;
        public const int FILE_ATTRIBUTE_ARCHIVE             = 0x00000020;
        public const int FILE_ATTRIBUTE_DEVICE              = 0x00000040;
        public const int FILE_ATTRIBUTE_NORMAL              = 0x00000080;
        public const int FILE_ATTRIBUTE_TEMPORARY           = 0x00000100;
        public const int FILE_ATTRIBUTE_SPARSE_FILE         = 0x00000200;
        public const int FILE_ATTRIBUTE_REPARSE_POINT       = 0x00000400;
        public const int FILE_ATTRIBUTE_COMPRESSED          = 0x00000800;
        public const int FILE_ATTRIBUTE_OFFLINE             = 0x00001000;
        public const int FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
        public const int FILE_ATTRIBUTE_ENCRYPTED           = 0x00004000;
        public const int FILE_ATTRIBUTE_VIRTUAL             = 0x00010000;

        public static bool GetShellIcons(Control widget) {
            SHFILEINFO sfi = new SHFILEINFO();
            int flags = SHGFI_USEFILEATTRIBUTES|SHGFI_SYSICONINDEX|SHGFI_SMALLICON;
            IntPtr himg = SHGetFileInfo(@"", 0, ref sfi, Marshal.SizeOf(sfi), flags);

            if (widget as ListView != null) {
			    int LVM_SETIMAGELIST = 0x1003;
			    SendMessage(widget.Handle, LVM_SETIMAGELIST, (IntPtr)0, himg);
            }

            if (widget as TreeView != null) {
                int TVM_SETIMAGELIST = 0x1109;
                int TVSIL_NORMAL = 0;
			    SendMessage(widget.Handle, TVM_SETIMAGELIST, (IntPtr)TVSIL_NORMAL, himg);
            }
            return true;
        }

        public static int GetFileIconIndex(string path) {
            SHFILEINFO sfi = new SHFILEINFO();
            int attributes = FILE_ATTRIBUTE_NORMAL;
            int flags = SHGFI_USEFILEATTRIBUTES|SHGFI_SYSICONINDEX|SHGFI_SMALLICON;
            IntPtr himg = SHGetFileInfo(path, attributes, ref sfi, Marshal.SizeOf(sfi), (int)flags);
            return (int)sfi.iIcon;
        }

        public static int GetDirIconIndex(bool selected) {
            SHFILEINFO sfi = new SHFILEINFO();
            string path = Directory.GetCurrentDirectory();
            int attributes = FILE_ATTRIBUTE_DIRECTORY;
            int flags = SHGFI_USEFILEATTRIBUTES|SHGFI_SYSICONINDEX|SHGFI_SMALLICON;
            if (selected) flags |= SHGFI_SELECTED;
            IntPtr himg = SHGetFileInfo(path, attributes, ref sfi, Marshal.SizeOf(sfi), (int)flags);
            return (int)sfi.iIcon;
        }
    }
}
