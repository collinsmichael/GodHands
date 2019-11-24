#ifndef SPLITTER_H
#define SPLITTER_H
#include <windows.h>

#define SB_SETLEFTWINDOWS   (0x0400 + 0x8100)
#define SB_SETRIGHTWINDOWS  (0x0400 + 0x8101)

LRESULT CALLBACK SplitterProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

typedef struct SPLITTER {
    int (*StartUp)(void);
} SPLITTER;


#endif // SPLITTER_H
