#include <stdarg.h>
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct TOOLTIP ToolTip;
extern HACCEL hAccel;
extern HWND hwnd[16];
static char text[0x100];


static int StatusBar_StartUp(void) {
    int sb[3] = { 136, 256, -1 };
    SendMessageA(hwnd[WinStatusBar], SB_SETPARTS, (WPARAM)elementsof(sb), (LPARAM)&sb);
    SendMessageA(hwnd[WinProgressBar], PBM_SETRANGE, (WPARAM)0, (LPARAM)100);
    SendMessageA(hwnd[WinProgressBar], PBM_SETSTEP,  (WPARAM)1, 0);
    SendMessageA(hwnd[WinProgressBar], PBM_DELTAPOS, (WPARAM)1, 0);
    SendMessageA(hwnd[WinProgressBar], PBM_SETPOS,   (WPARAM)0, 0);
    return 1;
}

static int StatusBar_SetStatus(char *status, char *format, ...) {
    va_list list;
    va_start(list, format);
    wvsprintfA(text, format, list);
    va_end(list);
    SendMessageA(hwnd[WinStatusBar], SB_SETTEXT, (WPARAM)1, (LPARAM)status);
    SendMessageA(hwnd[WinStatusBar], SB_SETTEXT, (WPARAM)2, (LPARAM)text);
    UpdateWindow(hwnd[WinStatusBar]);
    ToolTip.SetToolTip(hwnd[WinStatusBar], text);
    SwitchToThread();
    return 1;
}

static int StatusBar_SetProgress(int percent) {
    if (percent < 0) percent = 0;
    if (percent > 100) percent = 100;
    PostMessageA(hwnd[WinProgressBar], PBM_SETPOS, (WPARAM)percent, 0);
    SwitchToThread();
    Sleep(100);
    return 1;
}


struct STATUSBAR StatusBar = {
    StatusBar_StartUp,
    StatusBar_SetStatus,
    StatusBar_SetProgress
};
