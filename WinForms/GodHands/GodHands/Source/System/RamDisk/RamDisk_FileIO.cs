using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GodHands {
    // ************************************************************************
    // RamDisk manages CD Image Disk IO.
    // Implements Lazy-Loading (sectors are read into memory only when needed).
    // Writes are immediately committed to file.
    // Rollback is performed in the event of an error.
    // Supports binding object state to disk contents.
    // ************************************************************************
    public static partial class RamDisk {
        private static FileStream file = null;
        private static long size = 0;
        private static long sector = 0;
        private static long offset = 0;
        private static string path = "";
        private static byte[] disk = new byte[0x26F57800];
        private static byte[] map = new byte[0x4DEAF];

        // ********************************************************************
        // Opens a file from disk and reads critical parts into memory
        // ********************************************************************
        public static bool Open(string filepath) {
            Close();
            try {
                path = filepath;
                size = new FileInfo(path).Length;
                FileAccess access = FileAccess.ReadWrite;
                FileShare share = FileShare.ReadWrite;
                file = File.Open(path, FileMode.Open, access, share);
            } catch (Exception e) {
                return Logger.Fail("File not found! "+e.Message);
            }

            byte[] head = new byte[12];
            byte[] sync = new byte[12] {
                0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0x00
            };

            file.Position = 0;
            file.Read(head, 0, 12);

            sector = 2352;
            offset = 0x18;
            for (int i = 0; i < 12; i++) {
                if (head[i] != sync[i]) {
                    sector = 2048;
                    offset = 0;
                    break;
                }
            }

            if ((size % sector) != 0) {
                Logger.Warn("File not aligned to sector boundary");
            }
            return Logger.Pass("File opened "+path);
        }

        // ********************************************************************
        // Closes the file and clears memory
        // ********************************************************************
        public static bool Close() {
            if (file != null) {
                file.Close();
                file = null;
                sector = 0;
                offset = 0;
                size = 0;
                Logger.Pass("File closed "+path);
            }
            for (int i = 0; i < map.Length; i++) {
                map[i] = 0;
            }
            return true;
        }

        // ********************************************************************
        // exchanges (swaps) the contents of sectors lba=src with lba=des
        // ********************************************************************
        public static bool Swap(int src, int des) {
            if (!Read(src) || !Read(des)) {
                return false;
            }

            for (int i = 0; i < 2048; i++) {
                byte temp = disk[src*2048 + i];
                disk[src*2048 + i] = disk[des*2048 + i];
                disk[des*2048 + i] = temp;
            }

            if (!Write(src) || !Write(des)) {
                return false;
            }
            return true;
        }

        // ********************************************************************
        // reads sectors lba into memory, only if it is not already in memory
        // ********************************************************************
        public static bool Read(int lba) {
            if (file == null) {
                return Logger.Fail("No disk!");
            }
            if ((lba < 0) || (lba >= map.Length)) {
                return Logger.Fail("Sector "+lba+" read is out of bounds!");
            }
            try {
                if (map[lba] == 0) {
                    file.Position = lba*sector + offset;
                    file.Read(disk, lba*2048, 2048);
                    map[lba] = 0x78;
                }
            } catch (Exception e) {
                return Logger.Fail("Read failed! "+e.Message);
            }
            return true;
        }

        // ********************************************************************
        // writes sectors lba from memory to file
        // ********************************************************************
        [DllImport("kernel32", SetLastError=true)]
        private static extern bool FlushFileBuffers(IntPtr handle);

        public static bool Write(int lba) {
            if (file == null) {
                return Logger.Fail("No disk!");
            }
            if ((lba < 0) || (lba >= map.Length)) {
                return Logger.Fail("Sector "+lba+" write is out of bounds!");
            }
            try {
                if (map[lba] != 0) {
                    file.Position = lba*sector + offset;
                    file.Write(disk, lba*2048, 2048);
                    file.Flush();
                    FlushFileBuffers(file.SafeFileHandle.DangerousGetHandle());
                }
            } catch (Exception e) {
                return Logger.Fail("Write failed! "+e.Message);
            }
            return true;
        }
    }
}
