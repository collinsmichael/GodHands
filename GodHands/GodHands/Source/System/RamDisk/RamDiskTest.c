#ifdef UNITTEST
#include "System/System.h"
#include "GodHands.h"


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
    ASSERT_TRUE(RamDisk.Open("test.img"));
    ASSERT_FALSE(RamDisk.Read(-1, 1));
    ASSERT_FALSE(RamDisk.Read(16, 1));
    ASSERT_FALSE(RamDisk.Write(-1, 1));
    ASSERT_FALSE(RamDisk.Write(16, 1));
    ASSERT_TRUE(RamDisk.Close());
}

UTEST(Test_0004_RamDisk, ReadAndWrite) {
    char expect[2*KB];
    char *actual;
    stosb(expect, 'x', sizeof(expect));
    ASSERT_TRUE(RamDisk.Open("test.img"));
    ASSERT_TRUE(RamDisk.Read(15, 1));
    ASSERT_NE(0, actual = RamDisk.AddressOf(15));
    ASSERT_EQ(0, cmpsb(actual, expect, sizeof(expect)));
    ASSERT_TRUE(RamDisk.Write(15, 1));
    ASSERT_TRUE(RamDisk.Close());
}

#endif // UNITTEST
