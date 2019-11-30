#include "GodHands.h"


extern struct LOGGER Logger;
extern struct MISSION Mission;
extern struct JOBQUEUE JobQueue;
extern struct RAMDISK RamDisk;


static int System_StartUp(int argc, char *argv[]) {
    if (!RamDisk.Reset()) return 0;
    if (!JobQueue.StartUp()) return 0;
    return Logger.Done("System.StartUp", "Done");
}

static int System_CleanUp(void) {
    if (!JobQueue.CleanUp()) return 0;
    if (!RamDisk.Reset()) return 0;
    return Logger.Done("System.CleanUp", "Done");
}

static int System_Execute(void) {
    return Logger.Done("System.Execute", "Done");
}

static int System_Reset(void) {
    JobQueue.KillAll();
    Mission.Reset();
    return Logger.Done("System.Reset", "Done");
}


struct SYSTEM System = {
    System_StartUp,
    System_CleanUp,
    System_Execute,
    System_Reset,
};
