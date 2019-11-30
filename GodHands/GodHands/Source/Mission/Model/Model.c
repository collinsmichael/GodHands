#include "GodHands.h"


extern struct LOGGER Logger;


static int Model_StartUp(void) {
    return Logger.Done("Model.StartUp", "Done");
}

static int Model_CleanUp(void) {
    return Logger.Done("Model.CleanUp", "Done");
}

static int Model_Execute(void) {
    return Logger.Done("Model.Execute", "Done");
}

int Model_OpenDisk(void *evt);

struct MODEL Model = {
    Model_StartUp,
    Model_CleanUp,
    Model_Execute,
    Model_OpenDisk,
};
