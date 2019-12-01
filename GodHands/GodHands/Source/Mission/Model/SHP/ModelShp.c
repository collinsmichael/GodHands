#include "GodHands.h"


extern struct LOGGER Logger;
extern struct RAMDISK RamDisk;
extern struct MODELPRG ModelPrg;


// 202 x SHP Files
static int shp_lba[256];
static int shp_len[256];
static REC *shp_rec[256];
static int shp;
static REC *battle_prg;
static uint16_t *lba_tbl;
static uint8_t *len_tbl;


static int ModelShp_Reset(void) {
    stosd(shp_lba, 0, sizeof(shp_lba)/4);
    stosd(shp_len, 0, sizeof(shp_len)/4);
    shp = 0;
    return 1;
}


static int ModelShp_AddShp(REC *rec) {
    if (shp < elementsof(shp_rec)) {
        uint8_t id = hex2int(rec->FileName);
        if (id < elementsof(shp_rec)) {
            shp_lba[id] = rec->LsbLbaData;
            shp_len[id] = rec->LsbLenData;
            shp_rec[id] = rec;
            shp++;
        }
    }
    if (shp >= elementsof(shp_rec)) {
        Logger.Warn("ModelShp.AddShp", "Maximum reached of %d/%d SHP Files", shp, elementsof(shp_rec));
    }
    return Logger.Done("ModelShp.AddShp", "Loaded %d/%d", shp, elementsof(shp_rec));
}

// scan BATTLE.PRG for the SHP LBA table
// then fixup our internal table to match
static int ModelShp_FindLbaTable(void) {
    uint32_t lba_00shp;
    uint8_t *ptr;
    int size;
    int i;

    battle_prg = ModelPrg.GetPrg("BATTLE.PRG");
    if (!battle_prg) {
        return Logger.Error("ModelArm.StartUp", "Error SLUS-010 not found");
    }
    size = battle_prg->LsbLenData;
    ptr = (uint8_t*)RamDisk.AddressOf(battle_prg->LsbLbaData);
    lba_tbl = 0;
    len_tbl = 0;

    // scan BATTLE.PRG for LBA table
    lba_00shp = shp_rec[0]->LsbLbaData;
    for (i = size-512; i >= 0; i -= 2) {
        int count;
        if (shp_rec[3*64]) {
            int lba3 = lba_00shp + ptr[i+6*64+1]*256 + ptr[i+6*64];
            if (lba3 != shp_rec[3*64]->LsbLbaData) continue;
        }
        if (shp_rec[2*64]) {
            int lba2 = lba_00shp + ptr[i+4*64+1]*256 + ptr[i+4*64];
            if (lba2 != shp_rec[2*64]->LsbLbaData) continue;
        }
        if (shp_rec[1*64]) {
            int lba1 = lba_00shp + ptr[i+2*64+1]*256 + ptr[i+2*64];
            if (lba1 != shp_rec[1*64]->LsbLbaData) continue;
        }
        if (shp_rec[0*64]) {
            int lba0 = lba_00shp + ptr[i+0*64+1]*256 + ptr[i+0*64];
            if (lba0 != shp_rec[0*64]->LsbLbaData) continue;
        }

        for (count = 0; count < 256; count++) {
            int lba = ptr[i+count*2+1]*256 + ptr[i+count*2];
            if (shp_rec[count] == 0) {
                if (lba != 0) break;
                continue;
            }
            lba += lba_00shp;
            if (lba != shp_rec[count]->LsbLbaData) {
                break;
            }
        }
        if (count == 256) {
            lba_tbl = (uint16_t*)&ptr[i];
            len_tbl = (uint8_t*)&ptr[i+512];
            break;
        }
    }
    return 1;
}

static int ModelShp_StartUp(void) {
    ModelShp_FindLbaTable();
    return 1;
}


struct MODELSHP ModelShp = {
    ModelShp_StartUp,
    ModelShp_Reset,
    ModelShp_AddShp,
};
