#ifndef ISO9660_H
#define ISO9660_H
#include <stdint.h>


#pragma pack(push, 1)
typedef struct ISO9660_PVD {
    uint8_t  TypeCode;             // Always 0x01 for a Primary Volume Descriptor
    char     Identifier[5];        // Always 'CD001'
    uint8_t  Version;              // Always 0x01
    uint8_t  AlwaysZero;           // Always 0x00
    char     SystemIdentifier[32]; // Name of the intended target system
    char     VolumeIdentifier[32]; // Identification of this volume.
    char     AllZeros[8];          // Unused Field - All zeroes
    uint32_t LsbVolumeSpaceSize;   // Number of Logical Blocks in the volume
    uint32_t MsbVolumeSpaceSize;   // Number of Logical Blocks in the volume
    uint8_t  UnusedField[32];      // Unused Field - All zeroes.
    uint16_t LsbVolumeSetSize;     // The number of disks in the volume
    uint16_t MsbVolumeSetSize;     // The number of disks in the volume
    uint16_t LsbVolumeSequenceNo;  // The number of this disk in the Volume Set
    uint16_t MsbVolumeSequenceNo;  // The number of this disk in the Volume Set
    uint16_t LsbLogicalBlockSize;  // The size in bytes of a logical block
    uint16_t MsbLogicalBlockSize;  // The size in bytes of a logical block
    uint32_t LsbPathTableSize;     // The size in bytes of the path table
    uint32_t MsbPathTableSize;     // The size in bytes of the path table
    uint32_t LsbLbaPathTable1;     // LBA location of the little endian path table
    uint32_t LsbLbaPathTable2;     // LBA location of the little endian path table
    uint32_t MsbLbaPathTable1;     // LBA location of the big endian path table
    uint32_t MsbLbaPathTable2;     // LBA location of the big endian path table
    uint8_t  RootDirectory[34];    // Directory entry for the root directory
    char     VolumeID[128];        // Identifier of the volume set
    char     PublisherID[128];     // Identifier of the publisher
    char     DataPreparerID[128];  // The identifier of the data preparer
    char     ApplicationID[128];   // Identifies how the data are recorded
    char     CopyrightFileID[38];  // Filename of a file in the root directory
    char     AbstractFileID[36];   // Filename of a file in the root directory
    char     BibliographicID[37];  // Filename of a file in the root directory
    char     CreationDate[17];     // The date and time of creation
    char     ModificationDate[17]; // The date and time of modification
    char     ExpirationDate[17];   // Obsolete date
    char     EffectiveDate[17];    // Date the volume can be used
    uint8_t  FileSystemVersion;    // Always 0x01
    uint8_t  Unused;               // Always 0x00
    uint8_t  ApplicationData[512]; // Contents not defined by ISO 9660
    uint8_t  Reserved[653];        //  Reserved by ISO
} ISO9660_PVD;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ISO9660_DIR {
    uint8_t  LenRecord;         // Length of Directory Record.
    uint8_t  LenXA;             // Extended Attribute Record length.
    uint32_t LsbLbaData;        // Location of extent (LBA) in little-endian
    uint32_t MsbLbaData;        // Location of extent (LBA) in big-endian
    uint32_t LsbLenData;        // Data length in little-endian
    uint32_t MsbLenData;        // Data length in big-endian
    uint8_t  DateTime[7];       // Recording date and time
    uint8_t  FileFlags;         // See Below
    uint8_t  FileUnitSize;      // File unit in interleaved mode only
    uint8_t  InterleaveGapSize; // Interleaved mode only
    uint16_t LsbVolumeSeqNo;    // The volume that this extent is recorded on
    uint16_t MsbVolumeSeqNo;    // The volume that this extent is recorded on
    uint8_t  LenFileName;       // Length of file identifier (file name)
    char     FileName[1];       // File Name (size is exactly LenFileName)
} ISO9660_DIR;
#define ISO9660_HIDDEN         0x01
#define ISO9660_DIRECTORY      0x02
#define ISO9660_ASSOCIATED     0x04
#define ISO9660_XA_FORMAT      0x08
#define ISO9660_XA_PERMISSIONS 0x10
#pragma pack(pop)


typedef struct ISO9660 {
    int   (*Open)(char *path);
    int   (*Close)(void);
    char *(*DiskPath)(void);
    char *(*DiskName)(void);
    char *(*FileExt)(ISO9660_DIR *rec);
    ISO9660_DIR *(*RootDir)(void);
    int   (*EnumDir)(void *param, ISO9660_DIR *dir, int(*proc)(void *param, ISO9660_DIR *rec));
} ISO9660;


#endif // ISO9660_H
