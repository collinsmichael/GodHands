#include "GodHands.h"


extern struct LOGGER Logger;
extern struct RAMDISK RamDisk;
extern struct MODELPRG ModelPrg;


// 127 x WEP Files
static int wep_lba[256];
static int wep_len[256];
static REC *wep_rec[256];
static int wep;
static REC *battle_prg;
static uint16_t *lba_tbl;
static uint8_t *len_tbl;


static int ModelWep_Reset(void) {
    stosd(wep_lba, 0, sizeof(wep_lba)/4);
    stosd(wep_len, 0, sizeof(wep_len)/4);
    wep = 0;
    return 1;
}

static int ModelWep_AddWep(REC *rec) {
    if (wep < elementsof(wep_rec)) {
        uint8_t id = hex2int(rec->FileName);
        if (id < elementsof(wep_rec)) {
            wep_lba[id] = rec->LsbLbaData;
            wep_len[id] = rec->LsbLenData;
            wep_rec[id] = rec;
            wep++;
        }
    }
    if (wep >= elementsof(wep_rec)) {
        Logger.Warn("ModelWep.AddWep", "Maximum reached of %d/%d WEP Files", wep, elementsof(wep_rec));
    }
    return Logger.Done("ModelWep.AddWep", "Loaded %d/%d", wep, elementsof(wep_rec));
}

// scan BATTLE.PRG for the WEP LBA table
// then fixup our internal table to match
static int ModelWep_FindLbaTable(void) {
    uint32_t lba_01wep;
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
    lba_01wep = wep_rec[1]->LsbLbaData;
    for (i = size-512; i >= 0; i -= 2) {
        int count;
        if (wep_rec[3*64]) {
            int lba3 = lba_01wep + ptr[i+6*64+1]*256 + ptr[i+6*64];
            if (lba3 != wep_rec[3*64]->LsbLbaData) continue;
        }
        if (wep_rec[2*64]) {
            int lba2 = lba_01wep + ptr[i+4*64+1]*256 + ptr[i+4*64];
            if (lba2 != wep_rec[2*64]->LsbLbaData) continue;
        }
        if (wep_rec[1*64]) {
            int lba1 = lba_01wep + ptr[i+2*64+1]*256 + ptr[i+2*64];
            if (lba1 != wep_rec[1*64]->LsbLbaData) continue;
        }
        if (wep_rec[0*64]) {
            int lba0 = lba_01wep + ptr[i+0*64+1]*256 + ptr[i+0*64];
            if (lba0 != wep_rec[0*64]->LsbLbaData) continue;
        }

        for (count = 0; count < 128; count++) {
            int lba = ptr[i+count*2+1]*256 + ptr[i+count*2];
            if (wep_rec[count] == 0) {
                if (lba != 0) break;
                continue;
            }
            lba += lba_01wep;
            if (lba != wep_rec[count]->LsbLbaData) {
                break;
            }
        }
        if (count == 128) {
            lba_tbl = (uint16_t*)&ptr[i];
            len_tbl = (uint8_t*)&ptr[i+512];
            break;
        }
    }
    return 1;
}

static int ModelWep_StartUp(void) {
    ModelWep_FindLbaTable();
    return 1;
}


struct MODELWEP ModelWep = {
    ModelWep_StartUp,
    ModelWep_Reset,
    ModelWep_AddWep,
};
