#include "GodHands.h"


extern struct LOGGER Logger;


static int Control_StartUp(void) {
    return Logger.Done("Control.StartUp", "Done");
}

static int Control_CleanUp(void) {
    return Logger.Done("Control.CleanUp", "Done");
}

static int Control_Execute(void) {
    return Logger.Done("Control.Execute", "Done");
}


struct CONTROL Control = {
    Control_StartUp,
    Control_CleanUp,
    Control_Execute,
    Control_OpenRecord,
};
