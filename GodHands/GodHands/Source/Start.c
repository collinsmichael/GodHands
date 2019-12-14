#ifndef UNITTEST
#include <windows.h>

extern int main(int argc, char *argv[]);
int _fltused;

void start(void) {
    ExitProcess(main(0,0));
}

#endif // UNITTEST
