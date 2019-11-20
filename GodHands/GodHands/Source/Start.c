#ifndef UNITTEST
#include <windows.h>

extern int main(int argc, char *argv[]);

void start(void) {
    ExitProcess(main(0,0));
}

#endif // UNITTEST
