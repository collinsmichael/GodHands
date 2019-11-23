#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct TOOLTIP ToolTip;

extern HWND hwnd[16];


static int StatusBar_StartUp(void) {
    int sb[3] = { 136, 192, -1 };
    SendMessageA(hwnd[WinStatusBar], SB_SETPARTS, (WPARAM)elementsof(sb), (LPARAM)&sb);
    return 1;
}

static int StatusBar_SetStatus(char *status, char *text) {
    SendMessageA(hwnd[WinStatusBar], SB_SETTEXT, (WPARAM)1, (LPARAM)status);
    SendMessageA(hwnd[WinStatusBar], SB_SETTEXT, (WPARAM)2, (LPARAM)text);

    SendMessageA(hwnd[WinProgressBar], PBM_SETRANGE, (WPARAM)0, (LPARAM)100);
    SendMessageA(hwnd[WinProgressBar], PBM_SETSTEP,  (WPARAM)1, 0);
    SendMessageA(hwnd[WinProgressBar], PBM_DELTAPOS, (WPARAM)1, 0);
    SendMessageA(hwnd[WinProgressBar], PBM_SETPOS,   (WPARAM)0, 0);

    ToolTip.SetToolTip(WinStatusBar, text);
    return 1;
}

static int StatusBar_SetProgress(int percent) {
    if (percent < 0) percent = 0;
    if (percent > 100) percent = 100;
    SendMessageA(hwnd[WinProgressBar], PBM_SETPOS, (WPARAM)percent, 0);
    return 1;
}


struct STATUSBAR StatusBar = {
    StatusBar_StartUp,
    StatusBar_SetStatus,
    StatusBar_SetProgress
};
