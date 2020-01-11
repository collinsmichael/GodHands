using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class DirRec : IBound {
        private string url;
        private int pos;

        public DirRec(string url, int pos) {
            this.url = url;
            this.pos = pos;
        }

        public string GetUrl() {
            return url;
        }

        public int GetPos() {
            return pos;
        }

        public void SetPos(int pos) {
            this.pos = pos;
        }

        // ********************************************************************
        // Fields
        // ********************************************************************
        //uint8_t  LenRecord;         // Length of Directory Record.
        [ReadOnly(true)]
        [Category("Record")]
        public byte LenRecord {
            get { return RamDisk.GetU8(pos+0); }
            set {
                //UndoRedo.Exec(new BindU8(this, 0, value));
                Publisher.Publish(url, this);
                Logger.Warn("Not Implemented");
            }
        }

        //uint8_t  LenXA;             // Extended Attribute Record length.
        [Category("Record")]
        public byte LenXA {
            get { return RamDisk.GetU8(pos+1); }
            set {
                //UndoRedo.Exec(new BindU8(this, 1, value));
                Publisher.Publish(url, this);
                Logger.Warn("Not Implemented");
            }
        }

        //uint32_t LsbLbaData;        // Location of extent (LBA) in little-endian
        //uint32_t MsbLbaData;        // Location of extent (LBA) in big-endian
        [ReadOnly(true)]
        [Category("Record")]
        public uint LbaData {
            get { return RamDisk.GetU32(pos+2); }
            set {
                //byte[] buf = new byte[8] {
                //    (byte)((value) % 256),
                //    (byte)((value/256) % 256),
                //    (byte)((value/65536) % 256),
                //    (byte)((value/16777216) % 256),
                //    (byte)((value/16777216) % 256),
                //    (byte)((value/65536) % 256),
                //    (byte)((value/256) % 256),
                //    (byte)((value) % 256)
                //};
                //UndoRedo.Exec(new BindArray(this, 2, 8, buf));
                Publisher.Publish(url, this);
                Logger.Warn("Not Implemented");
            }
        }

        //uint32_t LsbLenData;        // Data length in little-endian
        //uint32_t MsbLenData;        // Data length in big-endian
        [ReadOnly(true)]
        [Category("Record")]
        public uint LenData {
            get { return RamDisk.GetU32(pos+10); }
            set {
                //byte[] buf = new byte[8] {
                //    (byte)((value) % 256),
                //    (byte)((value/256) % 256),
                //    (byte)((value/65536) % 256),
                //    (byte)((value/16777216) % 256),
                //    (byte)((value/16777216) % 256),
                //    (byte)((value/65536) % 256),
                //    (byte)((value/256) % 256),
                //    (byte)((value) % 256)
                //};
                //UndoRedo.Exec(new BindArray(this, 10, 8, buf));
                Publisher.Publish(url, this);
                Logger.Warn("Not Implemented");
            }
        }

        //uint8_t  DateTime[7];       // Recording date and time
        [Category("Record")]
        public DateTime CreatedTime {
            get; set;
        }

        //uint8_t  FileFlags;         // See Below
        [ReadOnly(true)]
        [Category("File Flags")]
        public byte FileFlags {
            get { return RamDisk.GetU8(pos+25); }
            set { UndoRedo.Exec(new BindU8(this, 25, value)); }
        }
        [Category("File Flags")]
        public bool FileFlags_Hidden {
            get {
                // logic is inverted
                byte flags = RamDisk.GetU8(pos+25);
                return ((flags & 0x01) == 0);
            }
            set { 
                // logic is inverted
                byte flags = RamDisk.GetU8(pos+25);
                flags = (byte)((value) ? (flags&0xFE) : (flags|0x01));
                UndoRedo.Exec(new BindU8(this, 25, flags));
            }
        }
        [Category("File Flags")]
        public bool FileFlags_Directory {
            get {
                byte flags = RamDisk.GetU8(pos+25);
                return ((flags & 0x02) != 0);
            }
            set { 
                byte flags = RamDisk.GetU8(pos+25);
                flags = (byte)((value) ? (flags|0x02) : (flags&0xFD));
                UndoRedo.Exec(new BindU8(this, 25, flags));
            }
        }
        [Category("File Flags")]
        public bool FileFlags_Associated {
            get {
                byte flags = RamDisk.GetU8(pos+25);
                return ((flags & 0x04) != 0);
            }
            set { 
                byte flags = RamDisk.GetU8(pos+25);
                flags = (byte)((value) ? (flags|0x04) : (flags&0xFB));
                UndoRedo.Exec(new BindU8(this, 25, flags));
            }
        }
        [Category("File Flags")]
        public bool FileFlags_Record {
            get {
                byte flags = RamDisk.GetU8(pos+25);
                return ((flags & 0x08) != 0);
            }
            set { 
                byte flags = RamDisk.GetU8(pos+25);
                flags = (byte)((value) ? (flags|0x08) : (flags&0xF7));
                UndoRedo.Exec(new BindU8(this, 25, flags));
            }
        }
        [Category("File Flags")]
        public bool FileFlags_Protection {
            get {
                byte flags = RamDisk.GetU8(pos+25);
                return ((flags & 0x10) != 0);
            }
            set { 
                byte flags = RamDisk.GetU8(pos+25);
                flags = (byte)((value) ? (flags|0x10) : (flags&0xEF));
                UndoRedo.Exec(new BindU8(this, 25, flags));
            }
        }
        [Category("File Flags")]
        public bool FileFlags_MultiExtent {
            get {
                byte flags = RamDisk.GetU8(pos+25);
                return ((flags & 0x80) != 0);
            }
            set { 
                byte flags = RamDisk.GetU8(pos+25);
                flags = (byte)((value) ? (flags|0x80) : (flags&0x7F));
                UndoRedo.Exec(new BindU8(this, 25, flags));
            }
        }

        //uint8_t  FileUnitSize;      // File unit in interleaved mode only
        [Category("Record")]
        public byte FileUnitSize {
            get { return RamDisk.GetU8(pos+26); }
            set { UndoRedo.Exec(new BindU8(this, 26, value)); }
        }

        //uint8_t  InterleaveGapSize; // Interleaved mode only
        [Category("Record")]
        public byte InterleaveGapSize {
            get { return RamDisk.GetU8(pos+27); }
            set { UndoRedo.Exec(new BindU8(this, 27, value)); }
        }

        //uint16_t LsbVolumeSeqNo;    // The volume that this extent is recorded on
        //uint16_t MsbVolumeSeqNo;    // The volume that this extent is recorded on
        [Category("Record")]
        public ushort VolumeSeqNo {
            get { return RamDisk.GetU16(pos+28); }
            set { UndoRedo.Exec(new BindU16(this, 28, value)); }
        }

        //uint8_t  LenFileName;       // Length of file identifier (file name)
        [ReadOnly(true)]
        [Category("Record")]
        public byte LenFileName {
            get { return RamDisk.GetU8(pos+32); }
            set {
                //UndoRedo.Exec(new BindU8(this, 32, value));
                Publisher.Publish(url, this);
                Logger.Warn("Not Implemented");
            }
        }

        //char     FileName[1];       // File Name (size is exactly LenFileName)
        [ReadOnly(true)]
        [Category("Record")]
        public string FileName {
            get { return RamDisk.GetString(pos+33, LenFileName); }
            set {
                //UndoRedo.Exec(new BindString(this, 33, LenFileName, value));
                Publisher.Publish(url, this);
                Logger.Warn("Not Implemented");
            }
        }
    }
}
