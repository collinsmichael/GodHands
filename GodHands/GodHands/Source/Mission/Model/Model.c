#include "GodHands.h"


extern struct LOGGER Logger;
extern struct MODELZND ModelZnd;


static int Model_StartUp(void) {
    return Logger.Done("Model.StartUp", "Done");
}

static int Model_CleanUp(void) {
    return Logger.Done("Model.CleanUp", "Done");
}

static int Model_Execute(void) {
    return Logger.Done("Model.Execute", "Done");
}

static int Model_Reset(void) {
    ModelZnd.Reset();
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
