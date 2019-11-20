#ifndef UNITTEST
#include "System/System.h"


extern struct LOGGER Logger;
extern struct RAMDISK RamDisk;

    char expect[2*KB];
    char actual[2*KB];

int main(int argc, char *argv[]) {
    Logger.Enable(3);
    stosb(expect, 'x', sizeof(expect));
    RamDisk.Open("does not exist");
    RamDisk.Write(15, 1, expect);
    RamDisk.Read(15, 1, actual);
    RamDisk.Close();

    return 0;
}

#endif // UNITTEST
