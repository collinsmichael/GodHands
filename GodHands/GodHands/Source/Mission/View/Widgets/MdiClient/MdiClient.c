/*****************************************************************************/
/* win32 list view example                                                   */
/*****************************************************************************/
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern LOGGER Logger;
extern HMENU hMenu[16];
extern HWND hwnd[64];
struct WINDOW wx[16];


static int MdiClient_Create(void) {
    RECT rc;
    CLIENTCREATESTRUCT cc;
    DWORD style = WS_CHILD|WS_VISIBLE|WS_CLIPCHILDREN|WS_CLIPSIBLINGS;
    cc.hWindowMenu  = GetSubMenu(GetMenu(hwnd[WinMdiFrame]), 3);
    cc.idFirstChild = 0x0100;

    GetClientRect(hwnd[WinMdiFrame], &rc);
    hwnd[WinMdiClient] = CreateWindowExA(0,"MdiClient",0,style,
        rc.left,rc.top,rc.right,rc.bottom,hwnd[WinMdiFrame],0,0,&cc);
    if (!hwnd[WinMdiClient]) {
        return Logger.Error("MdiClient.Create", "Error Createing Mdi Client");
    }
    return Logger.Done("MdiClient.Create", "Done");
}

static int MdiClient_StartUp(void) {
    RECT rc;
    GetWindowRect(hwnd[WinStatusBar], &rc);
    wx[WinMdiClient].PosX = 0;
    wx[WinMdiClient].PosY = 0;
    wx[WinMdiClient].Width = -1;
    wx[WinMdiClient].Height = -(rc.bottom-rc.top);
    return Logger.Done("MdiClient.StartUp", "Done");
}

struct MDICLIENT MdiClient = {
    MdiClient_Create,
    MdiClient_StartUp
};
