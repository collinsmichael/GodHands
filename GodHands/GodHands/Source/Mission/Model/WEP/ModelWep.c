#include "GodHands.h"


extern struct LOGGER Logger;

// 127 x WEP Files
static int wep_lba[256];
static int wep_len[256];
static REC *wep_rec[256];
static int wep;

static int ModelWep_Reset(void) {
    stosd(wep_lba, 0, sizeof(wep_lba)/4);
    stosd(wep_len, 0, sizeof(wep_len)/4);
    wep = 0;
    return 1;
}

static int ModelWep_StartUp(void) {
    return 1;
}

static int ModelWep_AddWep(REC *rec) {
    if (wep < elementsof(wep_rec)) {
        wep_lba[wep] = rec->LsbLbaData;
        wep_len[wep] = rec->LsbLenData;
        wep_rec[wep] = rec;
        wep++;
    }
    if (wep >= elementsof(wep_rec)) {
        Logger.Warn("ModelWep.AddWep", "Maximum reached of %d/%d WEP Files", wep, elementsof(wep_rec));
    }
    return Logger.Done("ModelWep.AddWep", "Loaded %d/%d", wep, elementsof(wep_rec));
}

struct MODELWEP ModelWep = {
    ModelWep_StartUp,
    ModelWep_Reset,
    ModelWep_AddWep,
};
