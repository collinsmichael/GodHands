#include <stdint.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct RAMDISK RamDisk;
extern struct MODELPRG ModelPrg;


// 32 x ARM Files
static int arm_lba[256];
static int arm_len[256];
static REC *arm_rec[256];
static uint8_t *arm_ptr[256];
static REC *menu5_prg;
static int arm;
static uint32_t *lba_tbl;
static int len_tbl;

static int ModelArm_Reset(void) {
    stosd(arm_lba, 0, sizeof(arm_lba)/4);
    stosd(arm_len, 0, sizeof(arm_len)/4);
    arm = 0;
    menu5_prg = 0;
    return 1;
}

static int ModelArm_AddArm(REC *rec) {
    if (arm < elementsof(arm_rec)) {
        arm_lba[arm] = rec->LsbLbaData;
        arm_len[arm] = rec->LsbLenData;
        arm_rec[arm] = rec;
        arm++;
    }
    if (arm >= elementsof(arm_rec)) {
        Logger.Warn("ModelArm.AddArm", "Maximum reached of %d/%d ARM Files", arm, elementsof(arm_rec));
    }
    return Logger.Done("ModelArm.AddArm", "Done");
}

// scan MENU5.PRG for the ARM LBA table
// then fixup our internal table to match
static int ModelArm_FindLbaTable(void) {
    int first;
    int last;
    int pos;
    int i;
    int j;
    uint32_t *ptr;
    int size;

    menu5_prg = ModelPrg.GetPrg("MENU5.PRG");
    if (!menu5_prg) {
        return Logger.Error("ModelArm.StartUp", "Error MENU5.PRG not found");
    }
    size = menu5_prg->LsbLenData;
    ptr = (uint32_t*)RamDisk.AddressOf(menu5_prg->LsbLbaData);

    // scan MENU5.PRG for LBA table
    first = size;
    last = 0;
    for (pos = 0; pos < arm; pos++) {
        int i;
        int lba = arm_rec[pos]->LsbLbaData;
        int len = arm_rec[pos]->LsbLenData;
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
        REC *tmp = arm_rec[i];
        arm_lba[i] = lba_tbl[2*i + 0];
        arm_len[i] = lba_tbl[2*i + 1];
        for (j = i; j < arm; j++) {
            REC *rec = arm_rec[j];
            if (rec->LsbLbaData == tmp->LsbLbaData) {
                arm_rec[i] = rec;
                arm_rec[j] = tmp;
                break;
            }
        }
    }
    return 1;
}

static int ModelArm_LoadArmFiles(void) {
    int pos;
    for (pos = 0; pos < arm; pos++) {
        int lba = arm_rec[pos]->LsbLbaData;
        int len = arm_rec[pos]->LsbLenData/(2*KB);
        if (!RamDisk.Read(lba, len)) return 0;
        arm_ptr[pos] = (uint8_t*)RamDisk.AddressOf(lba);
    }
    return 1;
}

static int ModelArm_StartUp(void) {
    ModelArm_FindLbaTable();
    ModelArm_LoadArmFiles();
    return 1;
}


struct MODELARM ModelArm = {
    ModelArm_StartUp,
    ModelArm_Reset,
    ModelArm_AddArm,
};
