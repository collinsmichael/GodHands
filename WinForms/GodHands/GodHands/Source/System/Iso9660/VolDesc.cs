using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace GodHands {
    public class VolDesc : BaseClass {
        public VolDesc(string url, int pos) : base(url, pos) {
            RamDisk.map[pos/2048] = 0x6F;
        }

        public override int GetLen() {
            return 0;
        }

        public override string GetText() {
            return VolumeIdentifier;
        }

        // ********************************************************************
        // Fields
        // ********************************************************************
        //uint8_t  TypeCode;             // Always 0x01 for a Primary Volume Descriptor
        [Category("Volume")]
        public byte TypeCode {
            get { return RamDisk.GetU8(GetPos()+0); }
            set { UndoRedo.Exec(new BindU8(this, 0, value)); }
        }

        //char     Identifier[5];        // Always 'CD001'
        [Category("Volume")]
        public string Identifier {
            get { return RamDisk.GetString(GetPos()+1, 5); }
            set { UndoRedo.Exec(new BindString(this, 1, 5, value)); }
        }

        //uint8_t  Version;              // Always 0x01
        [Category("Volume")]
        public byte Version {
            get { return RamDisk.GetU8(GetPos()+6); }
            set { UndoRedo.Exec(new BindU8(this, 6, value)); }
        }

        //uint8_t  AlwaysZero;           // Always 0x00
        //char     SystemIdentifier[32]; // Name of the intended target system
        [Category("Volume")]
        public string SystemIdentifier {
            get { return RamDisk.GetString(GetPos()+8, 32); }
            set { UndoRedo.Exec(new BindString(this, 8, 32, value)); }
        }

        //char     VolumeIdentifier[32]; // Identification of this volume.
        [Category("Volume")]
        public string VolumeIdentifier {
            get { return RamDisk.GetString(GetPos()+40, 32); }
            set { UndoRedo.Exec(new BindString(this, 40, 32, value)); }
        }

        //char     AllZeros[8];          // Unused Field - All zeroes
        //uint32_t LsbVolumeSpaceSize;   // Number of Logical Blocks in the volume
        //uint32_t MsbVolumeSpaceSize;   // Number of Logical Blocks in the volume
        [Category("Volume")]
        public int VolumeSpaceSize {
            get { return RamDisk.GetS32(GetPos()+80); }
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
                UndoRedo.Exec(new BindArray(this, GetPos()+80, 8, buf));
            }
        }

        //uint8_t  UnusedField[32];      // Unused Field - All zeroes.
        //uint16_t LsbVolumeSetSize;     // The number of disks in the volume
        //uint16_t MsbVolumeSetSize;     // The number of disks in the volume
        [Category("Volume")]
        public short VolumeSetSize {
            get { return RamDisk.GetS16(GetPos()+120); }
            set {
                byte[] buf = new byte[4] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, GetPos()+120, 4, buf));
            }
        }

        //uint16_t LsbVolumeSequenceNo;  // The number of this disk in the Volume Set
        //uint16_t MsbVolumeSequenceNo;  // The number of this disk in the Volume Set
        [Category("Volume")]
        public short VolumeSequenceNo {
            get { return RamDisk.GetS16(GetPos()+124); }
            set {
                byte[] buf = new byte[4] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, GetPos()+124, 4, buf));
            }
        }

        //uint16_t LsbLogicalBlockSize;  // The size in bytes of a logical block
        //uint16_t MsbLogicalBlockSize;  // The size in bytes of a logical block
        [Category("Volume")]
        public short LogicalBlockSize {
            get { return RamDisk.GetS16(GetPos()+128); }
            set {
                byte[] buf = new byte[4] {
                    (byte)((value) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value/256) % 256),
                    (byte)((value) % 256)
                };
                UndoRedo.Exec(new BindArray(this, GetPos()+128, 4, buf));
            }
        }

        //uint32_t LsbPathTableSize;     // The size in bytes of the path table
        //uint32_t MsbPathTableSize;     // The size in bytes of the path table
        [Category("Volume")]
        public int PathTableSize {
            get { return RamDisk.GetS32(GetPos()+132); }
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
                UndoRedo.Exec(new BindArray(this, GetPos()+132, 8, buf));
            }
        }

        //uint32_t LsbLbaPathTable1;     // LBA location of the little endian path table
        //uint32_t LsbLbaPathTable2;     // LBA location of the little endian path table
        //uint32_t MsbLbaPathTable1;     // LBA location of the big endian path table
        //uint32_t MsbLbaPathTable2;     // LBA location of the big endian path table
        [Category("Volume")]
        public int LbaPathTable1 {
            get { return RamDisk.GetS32(GetPos()+140); }
            set {
                byte[] buf = new byte[16];
                RamDisk.Get(GetPos()+140,16,buf);
                buf[0] = (byte)((value) % 256);
                buf[1] = (byte)((value/256) % 256);
                buf[2] = (byte)((value/65536) % 256);
                buf[3] = (byte)((value/16777216) % 256);
                buf[8] = (byte)((value/16777216) % 256);
                buf[9] = (byte)((value/65536) % 256);
                buf[10] = (byte)((value/256) % 256);
                buf[11] = (byte)((value) % 256);
                UndoRedo.Exec(new BindArray(this, GetPos()+140, 16, buf));
            }
        }
        [Category("Volume")]
        public int LbaPathTable2 {
            get { return RamDisk.GetS32(GetPos()+144); }
            set {
                byte[] buf = new byte[16];
                RamDisk.Get(GetPos()+140,16,buf);
                buf[4] = (byte)((value) % 256);
                buf[5] = (byte)((value/256) % 256);
                buf[6] = (byte)((value/65536) % 256);
                buf[7] = (byte)((value/16777216) % 256);
                buf[12] = (byte)((value/16777216) % 256);
                buf[13] = (byte)((value/65536) % 256);
                buf[14] = (byte)((value/256) % 256);
                buf[15] = (byte)((value) % 256);
                UndoRedo.Exec(new BindArray(this, GetPos()+140, 16, buf));
            }
        }

        //uint8_t  RootDirectory[34];    // Directory entry for the root directory
        private DirRec root = null;
        public DirRec GetRootDir() {
            if (root == null) {
                root = new DirRec("CD:ROOT", GetPos()+156);
            }
            return root;
        }

        //char     VolumeID[128];        // Identifier of the volume set
        [Category("Volume")]
        public string VolumeID {
            get { return RamDisk.GetString(GetPos()+190, 128); }
            set { UndoRedo.Exec(new BindString(this, 190, 128, value)); }
        }

        //char     PublisherID[128];     // Identifier of the publisher
        [Category("Volume")]
        public string PublisherID {
            get { return RamDisk.GetString(GetPos()+318, 128); }
            set { UndoRedo.Exec(new BindString(this, 318, 128, value)); }
        }

        //char     DataPreparerID[128];  // The identifier of the data preparer
        [Category("Volume")]
        public string DataPreparerID {
            get { return RamDisk.GetString(GetPos()+446, 128); }
            set { UndoRedo.Exec(new BindString(this, 446, 128, value)); }
        }

        //char     ApplicationID[128];   // Identifies how the data are recorded
        [Category("Volume")]
        public string ApplicationID {
            get { return RamDisk.GetString(GetPos()+574, 128); }
            set { UndoRedo.Exec(new BindString(this, 574, 128, value)); }
        }

        //char     CopyrightFileID[38];  // Filename of a file in the root directory
        [Category("Volume")]
        public string CopyrightFileID {
            get { return RamDisk.GetString(GetPos()+702, 38); }
            set { UndoRedo.Exec(new BindString(this, 702, 38, value)); }
        }

        //char     AbstractFileID[36];   // Filename of a file in the root directory
        [Category("Volume")]
        public string AbstractFileID {
            get { return RamDisk.GetString(GetPos()+739, 36); }
            set { UndoRedo.Exec(new BindString(this, 739, 36, value)); }
        }

        //char     BibliographicID[37];  // Filename of a file in the root directory
        [Category("Volume")]
        public string BibliographicID {
            get { return RamDisk.GetString(GetPos()+776, 37); }
            set { UndoRedo.Exec(new BindString(this, 776, 37, value)); }
        }

        private DateTime GetTime(int delta) {
            byte[] buf = new byte[17];
            RamDisk.Get(GetPos()+delta, 17, buf);
            int YYYY = (buf[0x00]-0x30)*1000 + (buf[0x01]-0x30)*100
                     + (buf[0x02]-0x30)*10 + (buf[0x03]-0x30);
            int MM   = (buf[0x04]-0x30)*10 + (buf[0x05]-0x30);
            int DD   = (buf[0x06]-0x30)*10 + (buf[0x07]-0x30);
            int hh   = (buf[0x08]-0x30)*10 + (buf[0x09]-0x30);
            int mm   = (buf[0x0A]-0x30)*10 + (buf[0x0B]-0x30);
            int ss   = (buf[0x0C]-0x30)*10 + (buf[0x0D]-0x30);
            int ms   = (buf[0x0E]-0x30)*100 + (buf[0x0F]-0x30)*10;
            try {
                DateTime t = new DateTime(YYYY, MM, DD, hh, mm, ss, ms);
                return t;
            } catch {
                return DateTime.Now;
            }
        }

        private void SetTime(int delta, DateTime t) {
            int YYYY = t.Year;
            int MM   = t.Month;
            int DD   = t.Day;
            int hh   = t.Hour;
            int mm   = t.Minute;
            int ss   = t.Second;
            int ms   = t.Millisecond;
            byte[] buf = new byte[17] {
                (byte)((YYYY/1000)%10+0x30), (byte)((YYYY/100)%10+0x30),
                (byte)((YYYY/10)%10+0x30), (byte)((YYYY/1)%10+0x30),
                (byte)((MM/10)%10+0x30), (byte)((MM/1)%10+0x30),
                (byte)((DD/10)%10+0x30), (byte)((DD/1)%10+0x30),
                (byte)((hh/10)%10+0x30), (byte)((hh/1)%10+0x30),
                (byte)((mm/10)%10+0x30), (byte)((mm/1)%10+0x30),
                (byte)((ss/10)%10+0x30), (byte)((ss/1)%10+0x30),
                (byte)((ms/100)%10+0x30), (byte)((ms/10)%10+0x30),
                0
            };
            UndoRedo.Exec(new BindArray(this, GetPos()+delta, 17, buf));
        }

        //char     CreationDate[17];     // The date and time of creation
        [Category("Date and Time")]
        public DateTime CreationDate {
            get { return GetTime(813); }
            set { SetTime(813, value); }
        }

        //char     ModificationDate[17]; // The date and time of modification
        [Category("Date and Time")]
        public DateTime ModificationDate {
            get { return GetTime(830); }
            set { SetTime(830, value); }
        }

        //char     ExpirationDate[17];   // Obsolete date
        [Category("Date and Time")]
        public DateTime ExpirationDate {
            get { return GetTime(847); }
            set { SetTime(847, value); }
        }

        //char     EffectiveDate[17];    // Date the volume can be used
        [Category("Date and Time")]
        public DateTime EffectiveDate {
            get { return GetTime(864); }
            set { SetTime(864, value); }
        }

        //uint8_t  FileSystemVersion;    // Always 0x01
        //uint8_t  Unused;               // Always 0x00
        //uint8_t  ApplicationData[512]; // Contents not defined by ISO 9660
        //uint8_t  Reserved[653];        //  Reserved by ISO
    }
}
