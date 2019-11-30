/* ************************************************************************** */
/* Implements in-memory file system.                                          */
/* Uses lazy loading when reading from Disk                                   */
/* All changes are immediately committed to  to disk.                         */
/* ************************************************************************** */
#include <memory.h>
#include <windows.h>
#include "GodHands.h"


extern struct SYSTEM System;
extern struct LOGGER Logger;
extern struct JOBQUEUE JobQueue;


static char disk[0x26F57800];
static char map[0x4DEAF];
static void *file;
static int size;
static int is_iso;
static int consumed;
static int total;


static int RamDisk_Read(int lba, int len) {
    int pos;
    if (!file) return Logger.Fail("RamDisk.Read", "No disk");
    if ((lba < 0) || (lba+len > size)) {
        return Logger.Fail("RamDisk.Read", "Out of bounds lba=%08X", lba);
    }
    for (pos = 0; pos < len; pos++, lba++) {
        if (map[lba] == ' ') {
            DWORD word;
            if (is_iso) {
                SetFilePointer(file, lba*2*KB, 0, 0);
            } else {
                SetFilePointer(file, lba*2352 + 24, 0, 0);
            }
            if (!ReadFile(file, &disk[lba*2*KB], 2*KB, &word, 0)) {
                return Logger.Error("RamDisk.Read", "Failed lba=%08X", lba);
            }
            map[lba] = 'x';
        }
    }
    return Logger.Done("RamDisk.Read", "Done");
}

static int RamDisk_Write(int lba, int len) {
    int pos;
    if (!file) return Logger.Fail("RamDisk.Write", "No disk");
    if ((lba < 0) || (lba+len > size)) {
        return Logger.Fail("RamDisk.Write", "Out of bounds lba=%08X", lba);
    }

    for (pos = 0; pos < len; pos++) {
        DWORD word;
        if (is_iso) {
            SetFilePointer(file, lba*2*KB, 0, 0);
        } else {
            SetFilePointer(file, lba*2352 + 24, 0, 0);
        }
        if (!WriteFile(file, &disk[lba*2*KB], 2*KB, &word, 0)) {
            return Logger.Error("RamDisk.Write", "Failed lba=%08X", lba);
        }
        map[lba] = 'x';
    }
    return Logger.Done("RamDisk.Write", "Done");
}

static int RamDisk_Scan(int lba) {
    int pos;
    if (!file) return Logger.Fail("RamDisk.Scan", "No disk");
    if ((lba < 0) || (lba >= size)) {
        return Logger.Fail("RamDisk.Scan", "Out of bounds lba=%08X", lba);
    }
    if (map[lba] == ' ') {
        DWORD word;
        char mark;
        if (is_iso) {
            SetFilePointer(file, lba*2*KB, 0, 0);
        } else {
            SetFilePointer(file, lba*2352 + 24, 0, 0);
        }
        if (!ReadFile(file, &disk[lba*2*KB], 2*KB, &word, 0)) {
            return Logger.Error("RamDisk.Scan", "Failed lba=%08X", lba);
        }
        mark = '-';
        for (pos = 0; pos < 2*KB; pos++) {
            if (disk[lba*2*KB + pos] != 0x00) {
                mark = 'H';
                break;
            }
        }
        map[lba] = mark;
    }
    return Logger.Done("RamDisk.Scan", "Done");
}

static int RamDisk_Clear(int lba, int len) {
    int pos;
    if (!file) return Logger.Fail("RamDisk.Clear", "No disk");
    if ((lba < 0) || (lba+len > size)) {
        return Logger.Fail("RamDisk.Clear", "Out of bounds lba=%08X", lba);
    }
    stosd(&disk[lba*2*KB], 0, len*2*KB/4);
    for (pos = 0; pos < len; pos++, lba++) {
         DWORD word;
         if (is_iso) {
             SetFilePointer(file, lba*2*KB, 0, 0);
         } else {
             SetFilePointer(file, lba*2352 + 24, 0, 0);
         }
         if (!WriteFile(file, &disk[lba*2*KB], 2*KB, &word, 0)) {
             return Logger.Error("RamDisk.Clear", "Failed lba=%08X", lba);
         }
         map[lba] = '-';
    }
    return Logger.Done("RamDisk.Clear", "Done");
}

static int RamDisk_Close(void) {
    if ((file != 0) && (file != INVALID_HANDLE_VALUE)) {
        System.Reset();
        CloseHandle(file);
    }
    file = 0;
    size = 0;
    is_iso = 0;
    stosd(map, 0x20202020, sizeof(map)/4);
    return Logger.Done("RamDisk.Close", "Done");
}

static int RamDisk_Reset(void) {
    RamDisk_Close();
    return Logger.Done("RamDisk.Reset", "Done");
}

static int RamDisk_Open(char *path) {
    static char sync[] = "\x00\xFF\xFF\xFF\xFF\xFF\xFF\xFF\xFF\xFF\xFF\x00";
    char buf[16];
    DWORD word;

    RamDisk_Close();
    file = CreateFileA(path, 0xC0000000, 0x03,0,0x03,0x80,0);
    if (file == INVALID_HANDLE_VALUE) {
        return Logger.Error("RamDisk.Open", "File not found %s", path);
    }

    SetFilePointer(file, 0, 0, 0);
    if (!ReadFile(file, buf, 16, &word, 0)) {
        RamDisk_Close();
        return Logger.Fail("RamDisk.Open", "File is Read Only");
    }

    size = GetFileSize(file, 0);
    if (cmpsd(buf, sync, sizeof(sync)/4) == 0) {
        is_iso = 0;
        if ((size % 2352) != 0) {
            Logger.Warn("RamDisk.Open", "Not aligned to sector boundary");
        }
        size = size/2352;
    } else {
        is_iso = 1;
        if ((size % (2*KB)) != 0) {
            Logger.Warn("RamDisk.Open", "Not aligned to sector boundary");
        }
        size = size/(2*KB);
    }
    if (size > sizeof(disk)) {
        RamDisk_Close();
        return Logger.Fail("RamDisk.Open", "File too large %s", path);
    }
    return Logger.Done("RamDisk.Open", "Done");
}

static char *RamDisk_AddressOf(int lba) {
    if ((!file) || (lba < 0) || (lba >= size)) return 0;
    return &disk[lba*2*KB];
}


struct RAMDISK RamDisk = {
    RamDisk_Reset,
    RamDisk_Open,
    RamDisk_Close,
    RamDisk_Read,
    RamDisk_Write,
    RamDisk_Scan,
    RamDisk_Clear,
    RamDisk_AddressOf
};

// TODO: Bind GUI widgets to data
