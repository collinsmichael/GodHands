#ifdef UNITTEST

#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;


UTEST_STATE();

int main(int argc, const char *const argv[]) {
    Logger.Enable(0);
    return utest_main(argc, argv);
}

#endif // UNITTEST
