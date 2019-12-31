#include <stdint.h>
#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct ISO9660 Iso9660;
extern struct RAMDISK RamDisk;
extern struct MODELARM ModelArm;
extern struct MODELPRG ModelPrg;
extern struct MODELZND ModelZnd;
extern struct MODELMPD ModelMpd;
extern struct MODELZUD ModelZud;
extern struct MODELSHP ModelShp;
extern struct MODELWEP ModelWep;
extern struct MODELSEQ ModelSeq;
extern struct MODELBIN ModelBin;
extern struct MODELSYD ModelSyd;
extern struct STATUSBAR StatusBar;


static char *extensions[] = {
    "SLUS", ".PRG", ".ARM", ".ZND",
    ".MPD", ".ZUD", ".SHP", ".WEP",
    ".SEQ", ".BIN", ".SYD"
};

static int Model_HiPriority(void *parent, REC *rec) {
    if ((rec->LenRecord > 0x30)) {
        if ((rec->FileFlags & ISO9660_DIRECTORY)) {
            if (!Iso9660.EnumDir(0, rec, Model_HiPriority)) return 0;
        } else {
            uint32_t **files = (uint32_t**)extensions;
            uint32_t *ext = (uint32_t*)Iso9660.FileExt(rec);
            uint32_t *name = (uint32_t*)rec->FileName;
            if (!ext) return 1;
            if ((name[0] == *files[0]) || (ext[0] == *files[1])
            ||  (ext[0] == *files[2]) || (ext[0] == *files[3])) {
                int lba = rec->LsbLbaData;
                int len = rec->LsbLenData/(2*KB);
                RamDisk.Read(lba, len);
                if (name[0] == *files[0]) ModelPrg.AddSlus(rec);
                if (ext[0] == *files[1]) ModelPrg.AddPrg(rec);
                if (ext[0] == *files[2]) ModelArm.AddArm(rec);
                if (ext[0] == *files[3]) ModelZnd.AddZnd(rec);
            }
        }
    }
    return 1;
}

static int Model_LoPriority(void *parent, REC *rec) {
    if ((rec->LenRecord > 0x30)) {
        if ((rec->FileFlags & ISO9660_DIRECTORY)) {
            if (!Iso9660.EnumDir(0, rec, Model_LoPriority)) return 0;
        } else {
            uint32_t **files = (uint32_t**)extensions;
            uint32_t *ext = (uint32_t*)Iso9660.FileExt(rec);
            if (!ext) return 1;
            if ((ext[0] == *files[0x04]) || (ext[0] == *files[0x05])
            ||  (ext[0] == *files[0x06]) || (ext[0] == *files[0x07])
            ||  (ext[0] == *files[0x08]) || (ext[0] == *files[0x09])
            ||  (ext[0] == *files[0x0A])) {
                int lba = rec->LsbLbaData;
                int len = rec->LsbLenData/(2*KB);
                RamDisk.Read(lba, len);
                if (ext[0] == *files[0x04]) ModelMpd.AddMpd(rec);
                if (ext[0] == *files[0x05]) ModelZud.AddZud(rec);
                if (ext[0] == *files[0x06]) ModelShp.AddShp(rec);
                if (ext[0] == *files[0x07]) ModelWep.AddWep(rec);
                if (ext[0] == *files[0x08]) ModelSeq.AddSeq(rec);
                if (ext[0] == *files[0x09]) ModelBin.AddBin(rec);
                if (ext[0] == *files[0x0A]) ModelSyd.AddSyd(rec);
                Sleep(0);
            }
        }
    }
    return 1;
}

static int Model_LoadAll(void *parent, REC *rec) {
    if ((rec->LenRecord > 0x30)) {
        if ((rec->FileFlags & ISO9660_DIRECTORY)) {
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
    ModelPrg.StartUp();
    ModelPrg.StartUp();
    ModelArm.StartUp();
    ModelZnd.StartUp();

    StatusBar.SetStatus("Model.OpenDisk", "Loading files");
    if (!Iso9660.EnumDir(0, 0, Model_LoPriority)) return 0;
    ModelMpd.StartUp();
    ModelZud.StartUp();
    ModelShp.StartUp();
    ModelWep.StartUp();
    ModelSeq.StartUp();
    ModelBin.StartUp();
    ModelSyd.StartUp();

    StatusBar.SetStatus("Model.OpenDisk", "Loading file system");
    if (!Iso9660.EnumDir(0, 0, Model_LoadAll)) return 0;
    StatusBar.SetStatus("Model.OpenDisk", "Finding unused sectors");
    Model_FindUnused();
    StatusBar.SetStatus("Model.OpenDisk", "Done");
    StatusBar.SetProgress(0);

    RamDisk.SaveMap("disk.map");
    return Logger.Done("Model.OpenDisk", "Done");
}
