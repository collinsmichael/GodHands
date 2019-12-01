#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;


extern ICON Icon;
extern TABBAR TabBar;
extern HWND hwnd[64];


LRESULT CALLBACK MdiChildProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    REC *rec;
    CREATESTRUCTA *cs;

    switch (uMsg) {
    case WM_CREATE:
        cs = (CREATESTRUCTA*)lParam;
        if (cs) {
            MDICREATESTRUCTA *mc = (MDICREATESTRUCTA*)cs->lpCreateParams;
            rec = (REC*)mc->lParam;
            SetPropA(hWnd, "REC", (HANDLE)rec);
            TabBar.Insert((char*)cs->lpszName, (void*)hWnd);
        }
        break;
    case WM_CLOSE:
        TabBar.Remove((void*)hWnd);
        break;
    case WM_MDIACTIVATE:
        hwnd[WinMdiChild] = (HWND)lParam;
        TabBar.SwitchTo((void*)hwnd[WinMdiChild]);
        return 0;
    }
    return DefMDIChildProcA(hWnd, uMsg, wParam, lParam);
}

static int MdiChild_Create(REC *rec) {
    static char text[256];
    MDICREATESTRUCTA mc;
    HICON hIcon;
    int i;

    for (i = 0; i < rec->LenFileName; i++) {
        text[i] = rec->FileName[i];
        if (text[i] == ';') break;
    }
    text[i] = 0;

    mc.style   = WS_OVERLAPPEDWINDOW | WS_CHILD | WS_VISIBLE | WS_CLIPSIBLINGS | WS_CLIPCHILDREN;
    mc.szClass = "MdiChild";
    mc.szTitle = text;
    mc.x       = CW_USEDEFAULT;
    mc.y       = CW_USEDEFAULT;
    mc.cx      = CW_USEDEFAULT;
    mc.cy      = CW_USEDEFAULT;
    mc.hOwner  = GetModuleHandleA(0);
    mc.lParam  = (LPARAM)rec;
    hwnd[WinMdiChild] = (HWND)SendMessageA(hwnd[WinMdiClient], WM_MDICREATE, 0, (LPARAM)&mc);
    if (!hwnd[WinMdiChild]) {
        return Logger.Error("MdiChile.Create", "Error WM_MDICREATE");
    }

    hIcon = Icon.GetSmallIcon((char*)mc.szTitle, FILE_ATTRIBUTE_NORMAL);
    SendMessageA(hwnd[WinMdiChild], WM_SETICON, ICON_BIG,   (LPARAM)hIcon);
    SendMessageA(hwnd[WinMdiChild], WM_SETICON, ICON_SMALL, (LPARAM)hIcon);
    return 1;
}


struct MDICHILD MdiChild = {
    MdiChild_Create,
};
