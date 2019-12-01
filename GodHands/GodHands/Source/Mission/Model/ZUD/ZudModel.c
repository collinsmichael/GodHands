#include "GodHands.h"


extern struct LOGGER Logger;

// 455 x ZUD Files
static int zud_lba[1024];
static int zud_len[1024];
static REC *zud_rec[1024];
static int zud;


static int ModelZud_Reset(void) {
    stosd(zud_lba, 0, sizeof(zud_lba)/4);
    stosd(zud_len, 0, sizeof(zud_len)/4);
    zud = 0;
    return 1;
}

static int ModelZud_StartUp(void) {
    return 1;
}

static int ModelZud_AddZud(REC *rec) {
    if (zud < elementsof(zud_rec)) {
        zud_lba[zud] = rec->LsbLbaData;
        zud_len[zud] = rec->LsbLenData;
        zud_rec[zud] = rec;
        zud++;
    }
    if (zud >= elementsof(zud_rec)) {
        Logger.Warn("ModelZud.AddZud", "Maximum reached of %d/%d ZUD Files", zud, elementsof(zud_rec));
    }
    return Logger.Done("ModelZud.AddZud", "Loaded %d/%d", zud, elementsof(zud_rec));
}

struct MODELZUD ModelZud = {
    ModelZud_StartUp,
    ModelZud_Reset,
    ModelZud_AddZud,
};
