#ifdef UNITTEST

#include <windows.h>
#include "System/System.h"
#include "UnitTest/UnitTest.h"


extern struct LOGGER Logger;


UTEST_STATE();

int main(int argc, const char *const argv[]) {
    Logger.Enable(0);
    return utest_main(argc, argv);
}

#endif // UNITTEST
