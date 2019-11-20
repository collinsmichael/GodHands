/* ************************************************************************** */
/* Implements in-memory file system.                                          */
/* Uses lazy loading when reading from Disk                                   */
/* All changes are immediately committed to  to disk.                         */
/* ************************************************************************** */
#include <memory.h>
#include <windows.h>
#include "System/System.h"


extern LOGGER Logger;


static char disk[0x26F57800];
static char map[0x4DEAF];
static void *file;
static int size;


static int RamDisk_Close(void) {
    if ((!file) || (file == INVALID_HANDLE_VALUE)) {
        return Logger.Fail("RamDisk.Close", "No disk");
    }
    CloseHandle(file);
    file = 0;
    size = 0;
    return Logger.Pass("RamDisk.Close", "Succeeded");
}

static int RamDisk_Open(char *path) {
    if (file) RamDisk_Close();
    file = CreateFileA(path, 0xC0000000, 0x03,0,0x03,0x80,0);
    if (file == INVALID_HANDLE_VALUE) {
        return Logger.Fail("RamDisk.Open", "File not found %s", path);
    }
    size = GetFileSize(file, 0);
    if (size > sizeof(disk)) {
        RamDisk_Close();
        return Logger.Fail("RamDisk.Open", "File too large %s", path);
    }
    if ((size % 2352) != 0) {
        Logger.Warn("RamDisk.Open", "Not aligned to sector boundary");
    }
    size = size/2352;
    return Logger.Pass("RamDisk.Open", "Succeeded");
}

static int RamDisk_Reset(void) {
    if (file) RamDisk_Close();
    stosd(map, ' ', sizeof(map)/4);
    return Logger.Info("RamDisk.Reset", "Succeeded");
}

static int RamDisk_Read(int lba, int len, char *buf) {
    int pos;
    if (!file) return Logger.Fail("RamDisk.Read", "No disk");
    if ((lba < 0) || (lba+len > size)) {
        return Logger.Fail("RamDisk.Read", "Out of bounds lba=%08X", lba);
    }
    for (pos = 0; pos < len; pos++, lba++) {
        if (map[lba] != 'x') {
            DWORD word;
            SetFilePointer(file, lba*2352 + 24, 0, 0);
            if (!ReadFile(file, &disk[lba*2*KB], 2*KB, &word, 0)) {
                return Logger.Fail("RamDisk.Read", "Failed lba=%08X", lba);
            }
            map[lba] = 'x';
        }
        memcpy(buf, &disk[lba*2*KB], 2*KB);
    }
    return Logger.Pass("RamDisk.Read", "Succeeded lba=%08X", lba);
}

static int RamDisk_Write(int lba, int len, char *buf) {
    int pos;
    if (!file) return Logger.Fail("RamDisk.Write", "No disk");
    if ((lba < 0) || (lba+len > size)) {
        return Logger.Fail("RamDisk.Write", "Out of bounds lba=%08X", lba);
    }
    for (pos = 0; pos < len; pos++) {
        DWORD word;
        memcpy(&disk[lba*2*KB], buf, 2*KB);
        SetFilePointer(file, lba*2352 + 24, 0, 0);
        if (!WriteFile(file, &disk[lba*2*KB], 2*KB, &word, 0)) {
            return Logger.Fail("RamDisk.Write", "Failed lba=%08X", lba);
        }
        map[lba] = 'x';
    }
    return Logger.Pass("RamDisk.Write", "Succeeded lba=%08X", lba);
}


struct RAMDISK RamDisk = {
    RamDisk_Reset,
    RamDisk_Open,
    RamDisk_Close,
    RamDisk_Read,
    RamDisk_Write
};

// TODO: Bind GUI widgets to data
