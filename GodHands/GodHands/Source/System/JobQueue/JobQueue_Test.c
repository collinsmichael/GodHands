#ifdef UNITTEST
#include <windows.h>
#include "GodHands.h"


extern struct JOBQUEUE JobQueue;


static int expect;
static int actual;


static int update(void *param) {
    if (param == &expect) {
        actual = *(int*)param;
    }
    return 1;
}

UTEST(Test_1001_JobQueue, SetUp) {
    ASSERT_TRUE(JobQueue.StartUp());
}

UTEST(Test_1002_JobQueue, NonExistentFunc) {
    ASSERT_FALSE(JobQueue.Schedule(0,0));
}

UTEST(Test_1003_JobQueue, DoWork) {
    void *param = (void*)&expect;
    expect = 42;
    ASSERT_TRUE(JobQueue.Schedule(update, param));
    Sleep(1*SEC);
    ASSERT_TRUE(expect == actual);
}

UTEST(Test_1004_JobQueue, TearDown) {
    ASSERT_TRUE(JobQueue.CleanUp());
}

#endif // UNITTEST
