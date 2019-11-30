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

static char znd_map[256];
static int znd_lba[256];
static int znd_len[256];
static REC *znd_rec[256];
static int znd;
static REC *slus;

static int Znd_Reset(void) {
    stosd(znd_map, 0, sizeof(znd_map)/4);
    stosd(znd_lba, 0, sizeof(znd_lba)/4);
    stosd(znd_len, 0, sizeof(znd_len)/4);
    znd = 0;
    slus = 0;
    return 0;
}

static int Znd_AddSlus(REC *rec) {
    slus = rec;
    return Logger.Done("Znd.AddSlus", "Done");
}

static int Znd_AddZnd(REC *rec) {
    if (znd < 256) {
        znd_lba[znd] = rec->LsbLbaData;
        znd_len[znd] = rec->LsbLenData;
        znd_rec[znd] = rec;
        znd_map[znd] = 'x';
        znd++;
    }
    return Logger.Done("Znd.AddZnd", "Done");
}

struct MODELZND ModelZnd = {
    Znd_Reset,
    Znd_AddSlus,
    Znd_AddZnd,
};
