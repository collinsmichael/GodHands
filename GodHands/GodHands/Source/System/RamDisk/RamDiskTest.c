#ifdef UNITTEST
#include "System/System.h"
#include "UnitTest/UnitTest.h"


extern struct RAMDISK RamDisk;


UTEST(Test_0001_RamDisk, NonExistentFile) {
    ASSERT_TRUE(RamDisk.Reset());
    ASSERT_FALSE(RamDisk.Open("does not exist"));
    ASSERT_FALSE(RamDisk.Close());
}

UTEST(Test_0002_RamDisk, OpenAndClose) {
    ASSERT_TRUE(RamDisk.Open("test.img"));
    ASSERT_TRUE(RamDisk.Close());
}

UTEST(Test_0003_RamDisk, OutOfBounds) {
    char buf[2*KB];
    ASSERT_TRUE(RamDisk.Open("test.img"));
    ASSERT_FALSE(RamDisk.Read(-1, 1, buf));
    ASSERT_FALSE(RamDisk.Read(16, 1, buf));
    ASSERT_FALSE(RamDisk.Write(-1, 1, buf));
    ASSERT_FALSE(RamDisk.Write(16, 1, buf));
    ASSERT_TRUE(RamDisk.Close());
}

UTEST(Test_0004_RamDisk, ReadAndWrite) {
    char expect[2*KB];
    char actual[2*KB];
    stosb(expect, 'x', sizeof(expect));
    ASSERT_TRUE(RamDisk.Open("test.img"));
    ASSERT_TRUE(RamDisk.Write(15, 1, expect));
    ASSERT_TRUE(RamDisk.Read(15, 1, actual));
    ASSERT_TRUE(cmpsb(actual, expect, sizeof(actual)));
    ASSERT_TRUE(RamDisk.Close());
}

#endif // UNITTEST
