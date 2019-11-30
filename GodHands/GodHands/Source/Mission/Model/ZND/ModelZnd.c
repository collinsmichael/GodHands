// ZND Files
// LBA TABLE is in SLUS-010.40@0x0003FCCC 256x8 (4:LBA,4:LEN)
// ZoneID is an index into the above array
//
// Contains
//  o List of LBAs to MPD which form this zone
//  o List of ZUDs to Enemies which populate this zone
//  o Stats for each enemy and their equipment
//  o Texture maps
#include "GodHands.h"


extern struct LOGGER Logger;

// 256 x ZND Files
static char znd_map[256];
static int znd_lba[256];
static int znd_len[256];
static REC *znd_rec[256];
static int znd;

static int ModelZnd_Reset(void) {
    stosd(znd_map, 0, sizeof(znd_map)/4);
    stosd(znd_lba, 0, sizeof(znd_lba)/4);
    stosd(znd_len, 0, sizeof(znd_len)/4);
    znd = 0;
    return 1;
}

static int ModelZnd_StartUp(void) {
    return 1;
}

static int ModelZnd_AddZnd(REC *rec) {
    if (znd < elementsof(znd_rec)) {
        znd_lba[znd] = rec->LsbLbaData;
        znd_len[znd] = rec->LsbLenData;
        znd_rec[znd] = rec;
        znd_map[znd] = 'x';
        znd++;
    }
    return Logger.Done("Znd.AddZnd", "Loaded %d/%d", znd, elementsof(znd_rec));
}

struct MODELZND ModelZnd = {
    ModelZnd_StartUp,
    ModelZnd_Reset,
    ModelZnd_AddZnd,
};
