#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct MODEL Model;

extern ICON Icon;
extern TABBAR TabBar;
extern HWND hwnd[64];

static PIXELFORMATDESCRIPTOR pfd = {
    sizeof(pfd),0x01,0x15,0,0x20,0,0,0,0,0,0,0,0,0,0,0,0,0,0x20,0,0,0,0,0,0,0
};


LRESULT CALLBACK MdiChildProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    REC *rec;
    CREATESTRUCTA *cs;
    WNDPROC WndProc;
    HDC hDC;

    switch (uMsg) {
    case WM_CREATE:
        cs = (CREATESTRUCTA*)lParam;
        if (cs) {
            MDICREATESTRUCTA *mc = (MDICREATESTRUCTA*)cs->lpCreateParams;
            rec = (REC*)mc->lParam;
            WndProc = Model.GetWndProc(rec);
            SetPropA(hWnd, "REC", (HANDLE)rec);
            SetPropA(hWnd, "VIEW", (HANDLE)WndProc);
            TabBar.Insert((char*)cs->lpszName, hWnd);
        }
        hDC = GetDC(hWnd);
        if (hDC) {
            SetPixelFormat(hDC, ChoosePixelFormat(hDC, &pfd), &pfd);
        }
        FlickerFree(hWnd);
        break;
    case WM_CLOSE:
        TabBar.Remove((void*)hWnd);
        break;
    case WM_MDIACTIVATE:
        hwnd[WinMdiChild] = (HWND)lParam;
        TabBar.SwitchTo(hwnd[WinMdiChild]);
        return 0;
    }

    WndProc = (WNDPROC)GetPropA(hWnd, "VIEW");
    if (WndProc) {
        WndProc(hWnd, uMsg, wParam, lParam);
    }
    return DefMDIChildProcA(hWnd, uMsg, wParam, lParam);
}

static int MdiChild_Create(REC *rec) {
    static char text[256];
    MDICREATESTRUCTA mc;
    HICON hIcon;
    int i;

    if (rec) {
        for (i = 0; i < rec->LenFileName; i++) {
            text[i] = rec->FileName[i];
            if (text[i] == ';') break;
        }
        text[i] = 0;
    } else {
        lstrcpyA(text, "document.txt");
    }

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
