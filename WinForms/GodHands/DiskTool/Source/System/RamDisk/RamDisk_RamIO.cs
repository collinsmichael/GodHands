using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    public static partial class RamDisk {
        private static byte[] save = null;

        // ********************************************************************
        // make a backup of disk contents in case we need to rollback
        // ********************************************************************
        public static bool Backup(int pos, int len) {
            if ((pos < 0) || (pos >= size)) {
                return Logger.Fail("Read "+pos+" out of bounds!");
            }

            save = new byte[len];
            for (int i = 0; i < len; i++) {
                save[i] = disk[pos + i];
            }
            return true;
        }

        // ********************************************************************
        // rollback disk contents in case of errors
        // ********************************************************************
        public static bool Rollback(int pos) {
            if ((pos < 0) || (pos >= size)) {
                return Logger.Fail("Read "+pos+" out of bounds!");
            }

            for (int i = 0; i < save.Length; i++) {
                disk[pos + i] = save[i];
            }
            return true;
        }

        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static bool Get(int pos, int len, byte[] buf) {
            if (!Backup(pos, len)) {
                return false;
            }

            for (int x = 0; x < len; x += 2048) {
                if (!Read((pos + x)/2048)) {
                    return Rollback(pos);
                }
                for (int i = 0; i < 2048; i++) {
                    if (x + i >= len) break;
                    buf[x + i] = disk[pos + x + i];
                }
            }
            return true;
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool Set(int pos, int len, byte[] buf) {
            if (!Backup(pos, len)) {
                return false;
            }

            for (int x = 0; x < len; x += 2048) {
                for (int i = 0; i < 2048; i++) {
                    if (x + i >= len) break;
                    disk[pos + x + i] = buf[x + i];
                }
                if (!Write((pos + x)/2048)) {
                    return Rollback(pos);
                }
            }
            return true;
        }
    }
}
