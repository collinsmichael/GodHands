#include "GodHands.h"


extern struct LOGGER Logger;

// 202 x SHP Files
static int shp_lba[256];
static int shp_len[256];
static REC *shp_rec[256];
static int shp;

static int ModelShp_Reset(void) {
    stosd(shp_lba, 0, sizeof(shp_lba)/4);
    stosd(shp_len, 0, sizeof(shp_len)/4);
    shp = 0;
    return 1;
}

static int ModelShp_StartUp(void) {
    return 1;
}

static int ModelShp_AddShp(REC *rec) {
    if (shp < elementsof(shp_rec)) {
        shp_lba[shp] = rec->LsbLbaData;
        shp_len[shp] = rec->LsbLenData;
        shp_rec[shp] = rec;
        shp++;
    }
    if (shp >= elementsof(shp_rec)) {
        Logger.Warn("ModelShp.AddShp", "Maximum reached of %d/%d SHP Files", shp, elementsof(shp_rec));
    }
    return Logger.Done("ModelShp.AddShp", "Loaded %d/%d", shp, elementsof(shp_rec));
}

struct MODELSHP ModelShp = {
    ModelShp_StartUp,
    ModelShp_Reset,
    ModelShp_AddShp,
};
