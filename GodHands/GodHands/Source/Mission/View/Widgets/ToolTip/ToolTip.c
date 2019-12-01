#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern HWND hwnd[16];


static int ToolTip_SetToolTip(HWND hWnd, char *text) {
    TOOLINFO tti;
    stosb(&tti, 0, sizeof(tti));
    tti.cbSize   = sizeof(tti);
    tti.uFlags   = TTF_SUBCLASS | TTF_IDISHWND;
    tti.uId      = (UINT)hWnd;
    tti.lpszText = text;
    SendMessageA(hwnd[WinToolTip], TTM_ADDTOOLA, 0, (LPARAM)&tti);
    return 1; //Logger.Done("ToolTip.SetToolTip", "Done");
}


struct TOOLTIP ToolTip = {
    ToolTip_SetToolTip
};
