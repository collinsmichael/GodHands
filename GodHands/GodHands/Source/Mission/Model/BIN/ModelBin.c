#include "GodHands.h"


extern struct LOGGER Logger;

// 322 x BIN Files
static int bin_lba[512];
static int bin_len[512];
static REC *bin_rec[512];
static int bin;

static int ModelBin_Reset(void) {
    stosd(bin_lba, 0, sizeof(bin_lba)/4);
    stosd(bin_len, 0, sizeof(bin_len)/4);
    bin = 0;
    return 1;
}

static int ModelBin_StartUp(void) {
    return 1;
}

static int ModelBin_AddBin(REC *rec) {
    if (bin < elementsof(bin_rec)) {
        bin_lba[bin] = rec->LsbLbaData;
        bin_len[bin] = rec->LsbLenData;
        bin_rec[bin] = rec;
        bin++;
    }
    if (bin >= elementsof(bin_rec)) {
        Logger.Warn("ModelBin.AddBin", "Maximum reached of %d/%d BIN Files", bin, elementsof(bin_rec));
    }
    return Logger.Done("ModelBin.AddBin", "Loaded %d/%d", bin, elementsof(bin_rec));
}

struct MODELBIN ModelBin = {
    ModelBin_StartUp,
    ModelBin_Reset,
    ModelBin_AddBin,
};
