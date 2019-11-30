#include "GodHands.h"


extern struct LOGGER Logger;
extern struct RAMDISK RamDisk;


// 21 x PRG Files + 1 SLUS
static int prg_lba[32];
static int prg_len[32];
static REC *prg_rec[32];
static REC *slus;
static int prg;

static int ModelPrg_Reset(void) {
    stosd(prg_lba, 0, sizeof(prg_lba)/4);
    stosd(prg_len, 0, sizeof(prg_len)/4);
    prg = 0;
    slus = 0;
    return 1;
}

static int ModelPrg_StartUp(void) {
    return 1;
}

static int ModelPrg_AddSlus(REC *rec) {
    slus = rec;
    return Logger.Done("Prg.AddSlus", "Done");
}

static int ModelPrg_AddPrg(REC *rec) {
    if (prg < elementsof(prg_rec)) {
        prg_lba[prg] = rec->LsbLbaData;
        prg_len[prg] = rec->LsbLenData;
        prg_rec[prg] = rec;
        prg++;
    }
    if (prg >= elementsof(prg_rec)) {
        Logger.Warn("ModelPrg.AddPrg", "Maximum reached of %d/%d PRG Files", prg, elementsof(prg_rec));
    }
    return Logger.Done("Prg.AddPrg", "Done");
}

static REC *ModelPrg_GetPrg(char *name) {
    int pos;
    int i;
    int len = lstrlenA(name);
    for (i = 0; i < len; i++) {
        if (i == 4) {
            int lba = slus->LsbLbaData;
            int len = slus->LsbLenData/(2*KB);
            if (!RamDisk.Read(lba, len)) return 0;
            return slus;
        }
        if (name[i] != "SLUS"[i]) break;
    }
    for (pos = 0; pos < prg; pos++) {
        for (i = 0; i <= len; i++) {
            if (i == len) {
                int lba = prg_rec[pos]->LsbLbaData;
                int len = prg_rec[pos]->LsbLenData/(2*KB);
                if (!RamDisk.Read(lba, len)) return 0;
                return prg_rec[pos];
            }
            if (name[i] != prg_rec[pos]->FileName[i]) break;
        }
    }
    return 0;
}

struct MODELPRG ModelPrg = {
    ModelPrg_StartUp,
    ModelPrg_Reset,
    ModelPrg_AddSlus,
    ModelPrg_AddPrg,
    ModelPrg_GetPrg,
};
