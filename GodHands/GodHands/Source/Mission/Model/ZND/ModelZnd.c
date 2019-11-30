// ZND Files
// LBA TABLE is in SLUS-010.40@0x0003FCCC 256x8 (4:LBA,4:LEN)
// ZoneID is an index into the above array
//
// Contains
//  o List of LBAs to MPD which form this zone
//  o List of ZUDs to Enemies which populate this zone
//  o Stats for each enemy and their equipment
//  o Texture maps
#include <stdint.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct RAMDISK RamDisk;
extern struct MODELPRG ModelPrg;


// 256 x ZND Files
static char znd_map[256];
static int znd_lba[256];
static int znd_len[256];
static REC *znd_rec[256];
static uint8_t *znd_ptr[256];
static int znd;
static REC *slus_prg;
static uint32_t *lba_tbl;
static int len_tbl;


static int ModelZnd_Reset(void) {
    stosd(znd_map, 0, sizeof(znd_map)/4);
    stosd(znd_lba, 0, sizeof(znd_lba)/4);
    stosd(znd_len, 0, sizeof(znd_len)/4);
    znd = 0;
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

// scan MENU5.PRG for the ARM LBA table
// then fixup our internal table to match
static int ModelZnd_FindLbaTable(void) {
    int first;
    int last;
    int pos;
    int i;
    int j;
    uint32_t *ptr;
    int size;

    slus_prg = ModelPrg.GetPrg("SLUS");
    if (!slus_prg) {
        return Logger.Error("ModelArm.StartUp", "Error SLUS-010 not found");
    }
    size = slus_prg->LsbLenData;
    ptr = (uint32_t*)RamDisk.AddressOf(slus_prg->LsbLbaData);

    // scan MENU5.PRG for LBA table
    first = size;
    last = 0;
    for (pos = 0; pos < znd; pos++) {
        int i;
        int lba = znd_rec[pos]->LsbLbaData;
        int len = znd_rec[pos]->LsbLenData;
        len = (len + 2*KB-1) & ~(2*KB-1);
        for (i = 0; i < size/4; i++) {
            if ((ptr[i] == lba) && (ptr[i+1] == len)) {
                if (first > i) first = i;
                if (last < i) last = i;
            }
        }
    }
    lba_tbl = &ptr[first];
    len_tbl = last - first;

    // fixup internal table
    // this may look pointless with a fresh CD image
    // but once we start moving/resizing files it will be crucial
    for (i = 0; i < len_tbl/2; i++) {
        REC *tmp = znd_rec[i];
        znd_lba[i] = lba_tbl[2*i + 0];
        znd_len[i] = lba_tbl[2*i + 1];
        for (j = i; j < znd; j++) {
            REC *rec = znd_rec[j];
            if (rec->LsbLbaData == tmp->LsbLbaData) {
                znd_rec[i] = rec;
                znd_rec[j] = tmp;
                break;
            }
        }
    }
    return 1;
}

static int ModelZnd_LoadZndFiles(void) {
    int pos;
    for (pos = 0; pos < znd; pos++) {
        int lba = znd_rec[pos]->LsbLbaData;
        int len = znd_rec[pos]->LsbLenData/(2*KB);
        if (!RamDisk.Read(lba, len)) return 0;
        znd_ptr[pos] = (uint8_t*)RamDisk.AddressOf(lba);
    }
    return 1;
}

static int ModelZnd_StartUp(void) {
    ModelZnd_FindLbaTable();
    ModelZnd_LoadZndFiles();
    return 1;
}


struct MODELZND ModelZnd = {
    ModelZnd_StartUp,
    ModelZnd_Reset,
    ModelZnd_AddZnd,
};
