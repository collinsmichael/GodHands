using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    public static partial class RamDisk {
        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static string GetString(int pos, int len) {
            byte[] buf = new byte[len];
            if (!Get(pos, len, buf)) {
                return null;
            }
            for (int i = 0; i < len; i++) {
                if (buf[i] == 0) {
                    len = i;
                }
            }
            return Encoding.ASCII.GetString(buf, 0, len);
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool SetString(int pos, int len, string val) {
            byte[] buf = new byte[len];
            byte[] str = Encoding.ASCII.GetBytes(val);
            for (int i = 0; i < len; i++) {
                if (i < str.Length) {
                    buf[i] = str[i];
                } else {
                    buf[i] = 0;
                }
            }
            if (!Set(pos, len, buf)) {
                return false;
            }
            return true;
        }

        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static byte GetU8(int pos) {
            byte[] buf = new byte[1];
            if (!Get(pos, 1, buf)) {
                return (byte)0;
            }
            byte val = buf[0];
            return val;
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool SetU8(int pos, byte val) {
            byte[] buf = new byte[1] { val };
            if (!Set(pos, 1, buf)) {
                return false;
            }
            return true;
        }

        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static ushort GetU16(int pos) {
            byte[] buf = new byte[2];
            if (!Get(pos, 2, buf)) {
                return (ushort)0;
            }
            ushort val = (ushort)(buf[0] + buf[1]*256);
            return val;
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool SetU16(int pos, ushort val) {
            byte[] buf = new byte[2] { (byte)(val%256), (byte)(val/256) };
            if (!Set(pos, 2, buf)) {
                return false;
            }
            return true;
        }

        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static uint GetU32(int pos) {
            byte[] buf = new byte[4];
            if (!Get(pos, 4, buf)) {
                return (uint)0;
            }
            uint val = (uint)(buf[0] + buf[1]*256 + buf[2]*65536 + buf[3]*16777216);
            return val;
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool SetU32(int pos, uint val) {
            byte[] buf = new byte[4] {
                (byte)(val%256), (byte)(val/256), (byte)(val/65536), (byte)(val/16777216)
            };
            if (!Set(pos, 4, buf)) {
                return false;
            }
            return true;
        }

        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static sbyte GetS8(int pos) {
            return (sbyte)GetU8(pos);
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool SetS8(int pos, sbyte val) {
            return SetU8(pos, (byte)val);
        }

        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static short GetS16(int pos) {
            return (short)GetU16(pos);
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool SetS16(int pos, short val) {
            return SetU16(pos, (ushort)val);
        }

        // ********************************************************************
        // get data from disk, read sector into memory if necessary
        // ********************************************************************
        public static int GetS32(int pos) {
            return (int)GetU32(pos);
        }

        // ********************************************************************
        // set data in disk and commit changes to file
        // ********************************************************************
        public static bool SetS32(int pos, int val) {
            return SetU32(pos, (uint)val);
        }
    }
}
