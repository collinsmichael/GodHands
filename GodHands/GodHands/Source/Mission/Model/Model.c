#include "GodHands.h"


int Model_OpenDisk(void *evt);


extern struct LOGGER Logger;
extern struct ISO9660 Iso9660;
extern struct MODELARM ModelArm;
extern struct MODELPRG ModelPrg;
extern struct MODELZND ModelZnd;
extern struct MODELMPD ModelMpd;
extern struct MODELZUD ModelZud;
extern struct MODELSHP ModelShp;
extern struct MODELWEP ModelWep;
extern struct MODELSEQ ModelSeq;
extern struct MODELBIN ModelBin;
extern struct MODELSYD ModelSyd;


static char *associations[] = {
    ".ARM",".MPD",".SHP",".WEP",".ZND",".ZUD",
};

static WNDPROC WndProc[] = {
    ArmWndProc,
    MpdWndProc,
    ShpWndProc,
    WepWndProc,
    ZndWndProc,
    ZudWndProc,
};


WNDPROC Model_GetWndProc(REC *rec) {
    int i;
    char *ext = Iso9660.FileExt(rec);
    if (!ext) return 0;
    for (i = 0; i < elementsof(associations); i++) {
        uint32_t *src = (uint32_t*)ext;
        uint32_t *des = (uint32_t*)associations[i];
        if (*src == *des) {
            return WndProc[i];
        }
    }
    return HexEditorProc;
}

static int Model_StartUp(void) {
    VsTextStartUp();
    return Logger.Done("Model.StartUp", "Done");
}

static int Model_CleanUp(void) {
    return Logger.Done("Model.CleanUp", "Done");
}

static int Model_Execute(void) {
    return Logger.Done("Model.Execute", "Done");
}

static int Model_Reset(void) {
    ModelArm.Reset();
    ModelPrg.Reset();
    ModelZnd.Reset();
    ModelArm.Reset();
    ModelPrg.Reset();
    ModelZnd.Reset();
    ModelMpd.Reset();
    ModelZud.Reset();
    ModelShp.Reset();
    ModelWep.Reset();
    ModelSeq.Reset();
    ModelBin.Reset();
    ModelSyd.Reset();
    return Logger.Done("Model.Reset", "Done");
}


struct MODEL Model = {
    Model_StartUp,
    Model_CleanUp,
    Model_Execute,
    Model_Reset,
    Model_OpenDisk,
    Model_GetWndProc,
};
