/* ************************************************************************** */
/* Implements in-memory file system.                                          */
/* Uses lazy loading when reading from Disk                                   */
/* All changes are immediately committed to  to disk.                         */
/* ************************************************************************** */
#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct RAMDISK RamDisk;

static ISO9660_PVD pvd;
static ISO9660_DIR dir;
static char *disk;
static char *root;

static int Iso9660_Close(void) {
    disk = 0;
    stosb(&pvd, 0, sizeof(pvd));
    stosb(&dir, 0, sizeof(dir));
    if (!RamDisk.Close()) return 0;
    return Logger.Done("Iso9660.Close", "Done");
}

static int Iso9660_Open(char *path) {
    int lba;
    int len;

    Iso9660_Close();
    if (!RamDisk.Open(path)) return 0;
    disk = RamDisk.AddressOf(0);
    if (!disk) return 0;
    if (!RamDisk.Read(0, 18)) return 0;

    movsb(&pvd, &disk[16*2*KB], sizeof(pvd));
    movsb(&dir, &pvd.RootDirectory, sizeof(dir));
    lba = dir.LsbLbaData;
    len = (dir.LsbLenData+2*KB-1) / (2*KB);
    if (!RamDisk.Read(0, lba)) return 0;
    if (!RamDisk.Read(lba, len)) return 0;
    root = &disk[lba*2*KB];
    return Logger.Done("Iso9660.Open", "Done");
}

static int Iso9660_EnumDir(ISO9660_DIR *dir, int(*proc)(ISO9660_DIR *rec)) {
    ISO9660_DIR *rec;
    int lba;
    int len;
    int pos;
    if ((!disk) || (!root)) {
        return Logger.Done("Iso9660.EnumDir", "No Disk");
    }

    if (dir == 0) dir = (ISO9660_DIR*)root;
    lba = dir->LsbLbaData;
    len = (dir->LsbLenData+2*KB-1) / (2*KB);
    if (!RamDisk.Read(lba, len)) return 0;

    rec = (ISO9660_DIR*)&disk[lba*2*KB];
    for (pos = 0x30; pos < len*2*KB; pos += rec->LenRecord) {
        rec = (ISO9660_DIR*)&disk[lba*2*KB + pos];
        if (rec->LenRecord == 0) break;
        if ((rec->FileFlags & ISO9660_DIRECTORY) != 0) {
            proc(rec);
        }
    }

    rec = (ISO9660_DIR*)&disk[lba*2*KB];
    for (pos = 0x30; pos < len*2*KB; pos += rec->LenRecord) {
        rec = (ISO9660_DIR*)&disk[lba*2*KB + pos];
        if (rec->LenRecord == 0) break;
        if ((rec->FileFlags & ISO9660_DIRECTORY) == 0) {
            proc(rec);
        }
    }
    return Logger.Done("Iso9660.EnumDir", "Done");
}


struct ISO9660 Iso9660 = {
    Iso9660_Open,
    Iso9660_Close,
    Iso9660_EnumDir,
};
