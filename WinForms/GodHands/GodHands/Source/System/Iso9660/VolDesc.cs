using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace GodHands {
    public class VolDesc : IBound {
        private string url;
        private int pos;

        public VolDesc(string url, int pos) {
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
        //uint8_t  TypeCode;             // Always 0x01 for a Primary Volume Descriptor
        [Category("Volume")]
        public byte TypeCode {
            get { return RamDisk.GetU8(pos+0); }
            set { UndoRedo.Exec(new BindU8(this, 0, value)); }
        }

        //char     Identifier[5];        // Always 'CD001'
        [Category("Volume")]
        public string Identifier {
            get { return RamDisk.GetString(pos+1, 5); }
            set { UndoRedo.Exec(new BindString(this, 1, 5, value)); }
        }

        //uint8_t  Version;              // Always 0x01
        [Category("Volume")]
        public byte Version {
            get { return RamDisk.GetU8(pos+6); }
            set { UndoRedo.Exec(new BindU8(this, 6, value)); }
        }

        //uint8_t  AlwaysZero;           // Always 0x00
        //char     SystemIdentifier[32]; // Name of the intended target system
        [Category("Volume")]
        public string SystemIdentifier {
            get { return RamDisk.GetString(pos+8, 32); }
            set { UndoRedo.Exec(new BindString(this, 8, 32, value)); }
        }

        //char     VolumeIdentifier[32]; // Identification of this volume.
        [Category("Volume")]
        public string VolumeIdentifier {
            get { return RamDisk.GetString(pos+40, 32); }
            set { UndoRedo.Exec(new BindString(this, 40, 32, value)); }
        }

        //char     AllZeros[8];          // Unused Field - All zeroes
        //uint32_t LsbVolumeSpaceSize;   // Number of Logical Blocks in the volume
        //uint32_t MsbVolumeSpaceSize;   // Number of Logical Blocks in the volume
        [Category("Volume")]
        public uint VolumeSpaceSize {
            get { return RamDisk.GetU32(pos+80); }
            set {
                byte[] buf = new byte[8] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/65536) % 256),
                    (byte)((value/16777216) % 256),
                    (byte)((value/16777216) % 256),
                    (byte)((value/65536) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, 80, 8, buf));
            }
        }

        //uint8_t  UnusedField[32];      // Unused Field - All zeroes.
        //uint16_t LsbVolumeSetSize;     // The number of disks in the volume
        //uint16_t MsbVolumeSetSize;     // The number of disks in the volume
        [Category("Volume")]
        public ushort VolumeSetSize {
            get { return RamDisk.GetU16(pos+120); }
            set {
                byte[] buf = new byte[4] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, 120, 4, buf));
            }
        }

        //uint16_t LsbVolumeSequenceNo;  // The number of this disk in the Volume Set
        //uint16_t MsbVolumeSequenceNo;  // The number of this disk in the Volume Set
        [Category("Volume")]
        public ushort VolumeSequenceNo {
            get { return RamDisk.GetU16(pos+124); }
            set {
                byte[] buf = new byte[4] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, 124, 4, buf));
            }
        }

        //uint16_t LsbLogicalBlockSize;  // The size in bytes of a logical block
        //uint16_t MsbLogicalBlockSize;  // The size in bytes of a logical block
        [Category("Volume")]
        public ushort LogicalBlockSize {
            get { return RamDisk.GetU16(pos+128); }
            set {
                byte[] buf = new byte[4] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, 128, 4, buf));
            }
        }

        //uint32_t LsbPathTableSize;     // The size in bytes of the path table
        //uint32_t MsbPathTableSize;     // The size in bytes of the path table
        [Category("Volume")]
        public uint PathTableSize {
            get { return RamDisk.GetU32(pos+132); }
            set {
                byte[] buf = new byte[8] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/65536) % 256),
                    (byte)((value/16777216) % 256),
                    (byte)((value/16777216) % 256),
                    (byte)((value/65536) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, 132, 8, buf));
            }
        }

        //uint32_t LsbLbaPathTable1;     // LBA location of the little endian path table
        //uint32_t LsbLbaPathTable2;     // LBA location of the little endian path table
        //uint32_t MsbLbaPathTable1;     // LBA location of the big endian path table
        //uint32_t MsbLbaPathTable2;     // LBA location of the big endian path table
        [Category("Volume")]
        public uint LbaPathTable1 {
            get { return RamDisk.GetU32(pos+140); }
            set {
                byte[] buf = new byte[16];
                RamDisk.Get(pos+140,16,buf);
                buf[0] = (byte)((value) % 256);
                buf[1] = (byte)((value/256) % 256);
                buf[2] = (byte)((value/65536) % 256);
                buf[3] = (byte)((value/16777216) % 256);
                buf[8] = (byte)((value/16777216) % 256);
                buf[9] = (byte)((value/65536) % 256);
                buf[10] = (byte)((value/256) % 256);
                buf[11] = (byte)((value) % 256);
                UndoRedo.Exec(new BindArray(this, 140, 16, buf));
            }
        }
        [Category("Volume")]
        public uint LbaPathTable2 {
            get { return RamDisk.GetU32(pos+144); }
            set {
                byte[] buf = new byte[16];
                RamDisk.Get(pos+140,16,buf);
                buf[4] = (byte)((value) % 256);
                buf[5] = (byte)((value/256) % 256);
                buf[6] = (byte)((value/65536) % 256);
                buf[7] = (byte)((value/16777216) % 256);
                buf[12] = (byte)((value/16777216) % 256);
                buf[13] = (byte)((value/65536) % 256);
                buf[14] = (byte)((value/256) % 256);
                buf[15] = (byte)((value) % 256);
                UndoRedo.Exec(new BindArray(this, 140, 16, buf));
            }
        }

        //uint8_t  RootDirectory[34];    // Directory entry for the root directory
        private DirRec root = null;
        public DirRec GetRootDir() {
            if (root == null) {
                root = new DirRec("ROOT", pos+156);
            }
            return root;
        }

        //char     VolumeID[128];        // Identifier of the volume set
        [Category("Volume")]
        public string VolumeID {
            get { return RamDisk.GetString(pos+190, 128); }
            set { UndoRedo.Exec(new BindString(this, 190, 128, value)); }
        }

        //char     PublisherID[128];     // Identifier of the publisher
        [Category("Volume")]
        public string PublisherID {
            get { return RamDisk.GetString(pos+318, 128); }
            set { UndoRedo.Exec(new BindString(this, 318, 128, value)); }
        }

        //char     DataPreparerID[128];  // The identifier of the data preparer
        [Category("Volume")]
        public string DataPreparerID {
            get { return RamDisk.GetString(pos+446, 128); }
            set { UndoRedo.Exec(new BindString(this, 446, 128, value)); }
        }

        //char     ApplicationID[128];   // Identifies how the data are recorded
        [Category("Volume")]
        public string ApplicationID {
            get { return RamDisk.GetString(pos+574, 128); }
            set { UndoRedo.Exec(new BindString(this, 574, 128, value)); }
        }

        //char     CopyrightFileID[38];  // Filename of a file in the root directory
        [Category("Volume")]
        public string CopyrightFileID {
            get { return RamDisk.GetString(pos+702, 38); }
            set { UndoRedo.Exec(new BindString(this, 702, 38, value)); }
        }

        //char     AbstractFileID[36];   // Filename of a file in the root directory
        [Category("Volume")]
        public string AbstractFileID {
            get { return RamDisk.GetString(pos+739, 36); }
            set { UndoRedo.Exec(new BindString(this, 739, 36, value)); }
        }

        //char     BibliographicID[37];  // Filename of a file in the root directory
        [Category("Volume")]
        public string BibliographicID {
            get { return RamDisk.GetString(pos+776, 37); }
            set { UndoRedo.Exec(new BindString(this, 776, 37, value)); }
        }

        //char     CreationDate[17];     // The date and time of creation
        [Category("Date and Time")]
        public DateTime CreationDate { get; set; }

        //char     ModificationDate[17]; // The date and time of modification
        [Category("Date and Time")]
        public DateTime ModificationDate { get; set; }

        //char     ExpirationDate[17];   // Obsolete date
        [Category("Date and Time")]
        public DateTime ExpirationDate { get; set; }

        //char     EffectiveDate[17];    // Date the volume can be used
        [Category("Date and Time")]
        public DateTime EffectiveDate { get; set; }

        //uint8_t  FileSystemVersion;    // Always 0x01
        //uint8_t  Unused;               // Always 0x00
        //uint8_t  ApplicationData[512]; // Contents not defined by ISO 9660
        //uint8_t  Reserved[653];        //  Reserved by ISO
    }
}
