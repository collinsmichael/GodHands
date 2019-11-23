#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct CONTROL Control;
extern struct MODEL Model;
extern struct VIEW View;


static int Mission_StartUp(int argc, char *argv[]) {
    if (!View.StartUp()) return 0;
    return Logger.Done("Mission.StartUp", "Done");
}

static int Mission_CleanUp(void) {
    if (!View.CleanUp()) return 0;
    return Logger.Done("Mission.CleanUp", "Done");
}

static int Mission_Execute(void) {
    if (!View.Execute()) return 0;
    return Logger.Done("Mission.Execute", "Done");
}


struct MISSION Mission = {
    Mission_StartUp,
    Mission_CleanUp,
    Mission_Execute,
};
