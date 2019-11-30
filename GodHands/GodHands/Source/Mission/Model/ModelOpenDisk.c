#include <stdint.h>
#include <windows.h>
#include "GodHands.h"


extern LOGGER Logger;
extern ISO9660 Iso9660;
extern RAMDISK RamDisk;
extern MODELZND ModelZnd;
extern STATUSBAR StatusBar;

char *extensions[] = {
    "SLUS", ".PRG", ".ARM", ".ZND",
    ".MPD", ".ZUD", ".SHP", ".WEP",
    ".SEQ", ".BIN", ".SYD"
};

static int Model_HiPriority(void *parent, REC *rec) {
    if ((rec->LenRecord > 0x30)) {
        if ((rec->FileFlags & RECECTORY)) {
            if (!Iso9660.EnumDir(0, rec, Model_HiPriority)) return 0;
        } else {
            uint32_t *files = (uint32_t*)extensions;
            uint32_t *ext = (uint32_t*)Iso9660.FileExt(rec);
            uint32_t *name = (uint32_t*)rec->FileName;
            if (!ext) return 1;
            if ((name[0] == files[0]) || (ext[0] == files[1]) || (ext[0] == files[2])) {
                int lba = rec->LsbLbaData;
                int len = rec->LsbLenData/(2*KB);
                RamDisk.Read(lba, len);
                if (name[0] == files[0]) ModelZnd.AddSlus(rec);
                if (ext[0] == files[1]) ModelZnd.AddZnd(rec);
            }
        }
    }
    return 1;
}

static int Model_LoPriority(void *parent, REC *rec) {
    if ((rec->LenRecord > 0x30)) {
        if ((rec->FileFlags & RECECTORY)) {
            if (!Iso9660.EnumDir(0, rec, Model_LoPriority)) return 0;
        } else {
            uint32_t *files = (uint32_t*)extensions;
            uint32_t *ext = (uint32_t*)Iso9660.FileExt(rec);
            if (!ext) return 1;
            if ((ext[0] == files[3]) || (ext[0] == files[4])
            ||  (ext[0] == files[5]) || (ext[0] == files[6])
            ||  (ext[0] == files[7]) || (ext[0] == files[8])
            ||  (ext[0] == files[9]) || (ext[0] == files[10])) {
                int lba = rec->LsbLbaData;
                int len = rec->LsbLenData/(2*KB);
                RamDisk.Read(lba, len);
                Sleep(0);
            }
        }
    }
    return 1;
}

static int Model_LoadAll(void *parent, REC *rec) {
    if ((rec->LenRecord > 0x30)) {
        if ((rec->FileFlags & RECECTORY)) {
            if (!Iso9660.EnumDir(0, rec, Model_LoadAll)) return 0;
        } else {
            int lba = rec->LsbLbaData;
            int len = rec->LsbLenData/(2*KB);
            RamDisk.Read(lba, len);
            Sleep(0);
        }
    }
    return 1;
}

static int Model_FindUnused(void) {
    int lba;
    for (lba = 0; lba < 0x4DEAF; lba++) {
        if (!RamDisk.Scan(lba)) return 0;
        Sleep(0);
    }
    return 1;
}

int Model_OpenDisk(void *evt) {
    StatusBar.SetStatus("Model.OpenDisk", "Loading priority files");
    if (!Iso9660.EnumDir(0, 0, Model_HiPriority)) return 0;
    StatusBar.SetStatus("Model.OpenDisk", "Loading files");
    if (!Iso9660.EnumDir(0, 0, Model_LoPriority)) return 0;
    StatusBar.SetStatus("Model.OpenDisk", "Loading file system");
    if (!Iso9660.EnumDir(0, 0, Model_LoadAll)) return 0;
    StatusBar.SetStatus("Model.OpenDisk", "Finding unused sectors");
    Model_FindUnused();
    StatusBar.SetStatus("Model.OpenDisk", "Done");
    StatusBar.SetProgress(0);
    return Logger.Done("Model.OpenDisk", "Done");
}
