#include "GodHands.h"


extern struct LOGGER Logger;

// 3 x SYD Files
static int syd_lba[4];
static int syd_len[4];
static REC *syd_rec[4];
static int syd;

static int ModelSyd_Reset(void) {
    stosd(syd_lba, 0, sizeof(syd_lba)/4);
    stosd(syd_len, 0, sizeof(syd_len)/4);
    syd = 0;
    return 1;
}

static int ModelSyd_StartUp(void) {
    return 1;
}

static int ModelSyd_AddSyd(REC *rec) {
    if (syd < elementsof(syd_rec)) {
        syd_lba[syd] = rec->LsbLbaData;
        syd_len[syd] = rec->LsbLenData;
        syd_rec[syd] = rec;
        syd++;
    }
    return Logger.Done("ModelSyd.AddSyd", "Loaded %d/%d", syd, elementsof(syd_rec));
}

struct MODELSYD ModelSyd = {
    ModelSyd_StartUp,
    ModelSyd_Reset,
    ModelSyd_AddSyd,
};
