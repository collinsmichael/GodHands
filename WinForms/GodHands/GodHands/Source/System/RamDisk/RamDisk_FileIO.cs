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
        private static int size = 0;
        private static int length = 0;
        private static int offset = 0;
        private static string path = "";
        public static int count = 0;
        public static byte[] disk = new byte[0x26F57800];
        public static byte[] map = new byte[0x4DEAF];

        // ********************************************************************
        // Autodetect CD-ROM sector format
        // ********************************************************************
        public static bool DetectCdFormat() {
            byte[] head = new byte[12];
            byte[] sync = new byte[12] {
                0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0x00
            };
            file.Position = 0;
            file.Read(head, 0, 12);

            // sync fields imply sectors are 2352 bytes
            length = 2352;
            offset = 0;
            for (int i = 0; i < 12; i++) {
                if (head[i] != sync[i]) {
                    length = 2048;
                    break;
                }
            }

            // detect the presence of subheader
            if (length == 2352) {
                byte[] buffer = new byte[2352];
                byte[] cd001 = new byte[6] {
                    0x01,0x43,0x44,0x30,0x30,0x31
                };

                file.Position = 16 * 2352;
                file.Read(buffer, 0, 2352);

                int offset_16 = 0;
                int offset_24 = 0;
                for (int i = 0; i < 6; i++) {
                    if (buffer[0x10 + i] == cd001[i]) offset_16++;
                    if (buffer[0x18 + i] == cd001[i]) offset_24++;
                }

                if (offset_16 == 6) offset = 0x10;
                if (offset_24 == 6) offset = 0x18;
            }
            return true;
        }

        // ********************************************************************
        // Opens a file from disk and reads critical parts into memory
        // ********************************************************************
        public static bool Open(string filepath) {
            Close();
            try {
                path = filepath;
                size = (int)(new FileInfo(path).Length);
                FileAccess access = FileAccess.ReadWrite;
                FileShare share = FileShare.ReadWrite;
                file = File.Open(path, FileMode.Open, access, share);
            } catch (Exception e) {
                return Logger.Fail("File not found! "+e.Message);
            }
            for (int i = 0; i < 16; i++) {
                map[i] = 0x6F;
            }

            DetectCdFormat();
            switch (offset) {
            case 0x00: Logger.Info("ISO CD-ROM image detected"); break;
            case 0x10: Logger.Info("RAW CD-ROM image detected"); break;
            case 0x18: Logger.Info("RAW CD-ROM/XA image detected"); break;
            }

            count = size / length;
            if ((size % length) != 0) {
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
                length = 0;
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

            byte x = map[src];
            byte y = map[des];
            map[src] = y;
            map[des] = x;

            for (int i = 0; i < 2048; i++) {
                byte s = disk[src*2048 + i];
                byte d = disk[des*2048 + i];
                disk[src*2048 + i] = d;
                disk[des*2048 + i] = s;
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
                if (map[lba] != 0x78) {
                    file.Position = lba*length + offset;
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
                if (map[lba] != 0x78) {
                    Read(lba);
                }
                file.Position = lba*length + offset;
                file.Write(disk, lba*2048, 2048);
                file.Flush();
                FlushFileBuffers(file.SafeFileHandle.DangerousGetHandle());
            } catch (Exception e) {
                return Logger.Fail("Write failed! "+e.Message);
            }
            return true;
        }
    }
}
