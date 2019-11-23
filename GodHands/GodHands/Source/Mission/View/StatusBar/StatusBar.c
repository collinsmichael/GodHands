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
    ToolTip.SetToolTip(WinStatusBar, text);
    return 1;
}

static int StatusBar_SetProgress(int percent) {
    return Logger.Done("StatusBar.SetProgress", "Done");
}


struct STATUSBAR StatusBar = {
    StatusBar_StartUp,
    StatusBar_SetStatus,
    StatusBar_SetProgress
};
