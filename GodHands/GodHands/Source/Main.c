#ifndef UNITTEST
#include "GodHands.h"


extern struct MISSION Mission;
extern struct SYSTEM System;
extern struct LOGGER Logger;


static int StartUp(int argc, char *argv[]) {
    Logger.Enable(0x0F);
    if (!System.StartUp(argc, argv)) return 0;
    if (!Mission.StartUp(argc, argv)) return 0;
    return 1;
}

static int CleanUp(void) {
    System.CleanUp();
    Mission.CleanUp();
    return 1;
}

static int Execute(void) {
    if (!System.Execute()) return 0;
    if (!Mission.Execute()) return 0;
    return 1;
}

int main(int argc, char *argv[]) {
    if (StartUp(argc, argv)) {
        Execute();
    }
    CleanUp();
    return 0;
}

#endif // UNITTEST
