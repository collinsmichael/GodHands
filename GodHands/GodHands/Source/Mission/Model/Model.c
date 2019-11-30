#include "GodHands.h"


extern struct LOGGER Logger;
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

int Model_OpenDisk(void *evt);

struct MODEL Model = {
    Model_StartUp,
    Model_CleanUp,
    Model_Execute,
    Model_Reset,
    Model_OpenDisk,
};
