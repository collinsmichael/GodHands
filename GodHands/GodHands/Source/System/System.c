#include "GodHands.h"


extern struct LOGGER Logger;


static int System_StartUp(int argc, char *argv[]) {
    return Logger.Done("System.StartUp", "Done");
}

static int System_CleanUp(void) {
    return Logger.Done("System.CleanUp", "Done");
}

static int System_Execute(void) {
    return Logger.Done("System.Execute", "Done");
}


struct SYSTEM System = {
    System_StartUp,
    System_CleanUp,
    System_Execute,
};
