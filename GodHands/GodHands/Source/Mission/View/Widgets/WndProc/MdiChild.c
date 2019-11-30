#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;


extern ICON Icon;
extern TABBAR TabBar;
extern HWND hwnd[64];


LRESULT CALLBACK MdiChildProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    static char text[256];
    static TCITEM tci;
    static RECT rc;
    static DWORD Style;
    static int count;
    static int i;
    static int TabIndex;

    switch (uMsg) {
    case WM_CREATE:
        //text[0] = 0;
        //GetWindowTextA(hWnd, text, sizeof(text));
        ////memset(&tci, 0, sizeof(tci));
        //tci.mask        = TCIF_TEXT | TCIF_IMAGE | TCIF_PARAM;
        //tci.pszText     = text;
        //tci.cchTextMax  = lstrlenA(text);
        //tci.iImage      = Icon.GetIndexFromAttributes(text, FILE_ATTRIBUTE_NORMAL);
        //tci.lParam      = (LPARAM)hWnd;
        ////SendMessageA(hWndTabCtrl, TCM_INSERTITEM, (WPARAM)1, (LPARAM)&tci);
        //TabIndex = TabCtrl_GetItemCount(hwnd[WinTabBar]);
        //TabCtrl_InsertItem(hwnd[WinTabBar], TabIndex, &tci);
        //TabCtrl_SetCurSel(hwnd[WinTabBar], TabIndex);
        //GetClientRect(hwnd[WinTabBar], &rc);
        //InvalidateRect(hwnd[WinTabBar], &rc, TRUE);
        break;
    case WM_CLOSE:
        count = TabCtrl_GetItemCount(hwnd[WinTabBar]);
        for (i = 0; i < count; i++) {
            tci.mask = TCIF_PARAM;
            TabCtrl_GetItem(hwnd[WinTabBar], i, &tci);
            if (tci.lParam == (LPARAM)hWnd) {
                TabCtrl_DeleteItem(hwnd[WinTabBar], i);
                break;
            }
        }
        GetClientRect(hwnd[WinTabBar], &rc);
        InvalidateRect(hwnd[WinTabBar], &rc, TRUE);
        break;
    case WM_MDIACTIVATE:
        hwnd[WinMdiChild] = (HWND)lParam;
        count = TabCtrl_GetItemCount(hwnd[WinTabBar]);
        for (i = 0; i < count; i++) {
            tci.mask = TCIF_PARAM;
            TabCtrl_GetItem(hwnd[WinTabBar], i, &tci);
            if (tci.lParam == (LPARAM)hwnd[WinMdiChild]) {
                TabCtrl_SetCurSel(hwnd[WinTabBar], i);
                break;
            }
        }
        GetClientRect(hwnd[WinTabBar], &rc);
        InvalidateRect(hwnd[WinTabBar], &rc, TRUE);
        GetClientRect(hwnd[WinMdiClient], &rc);
        InvalidateRect(hwnd[WinMdiClient], &rc, TRUE);
        return 0;
    }
    return DefMDIChildProcA(hWnd, uMsg, wParam, lParam);
}

static int MdiChild_Create(REC *rec) {
    static char text[256];
    MDICREATESTRUCT mc;
    DWORD Attributes;
    HICON hIcon;
    SHFILEINFO sfi;
    HIMAGELIST fi;
    TC_ITEM tci;
    HIMAGELIST him;
    int TabIndex;
    int i;

    for (i = 0; i < rec->LenFileName; i++) {
        text[i] = rec->FileName[i];
        text[i+1] = 0;
        if (text[i] == ';') {
            text[i] = 0;
            break;
        }
    }

    mc.style   = WS_OVERLAPPEDWINDOW | WS_CHILD | WS_VISIBLE | WS_CLIPSIBLINGS | WS_CLIPCHILDREN;
    mc.szClass = "MdiChild";
    mc.szTitle = text;
    mc.x       = CW_USEDEFAULT;
    mc.y       = CW_USEDEFAULT;
    mc.cx      = CW_USEDEFAULT;
    mc.cy      = CW_USEDEFAULT;
    mc.hOwner  = GetModuleHandleA(0);
    mc.lParam  = 0;
    hwnd[WinMdiChild] = (HWND)SendMessageA(hwnd[WinMdiClient], WM_MDICREATE, 0, (LPARAM)&mc);
    if (!hwnd[WinMdiChild]) {
        return Logger.Error("MdiChile.Create", "Error WM_MDICREATE");
    }

    Attributes = FILE_ATTRIBUTE_NORMAL;
    fi = (HIMAGELIST)SHGetFileInfo(mc.szTitle, Attributes, &sfi, sizeof(sfi),
        SHGFI_USEFILEATTRIBUTES | SHGFI_SYSICONINDEX | SHGFI_SMALLICON);
    hIcon = ImageList_GetIcon(fi, sfi.iIcon, ILD_IMAGE | ILD_NORMAL);
    SendMessageA(hwnd[WinMdiChild], WM_SETICON, ICON_BIG,   (LPARAM)hIcon);
    SendMessageA(hwnd[WinMdiChild], WM_SETICON, ICON_SMALL, (LPARAM)hIcon);
    him = TabCtrl_SetImageList(hwnd[WinTabBar], ImageList_Duplicate(fi));
    if (him) {
        ImageList_Destroy(him);
    }

    stosb(&tci, 0, sizeof(tci));
    tci.mask        = TCIF_TEXT | TCIF_IMAGE | TCIF_PARAM;
    tci.pszText     = (char*)mc.szTitle;
    tci.cchTextMax  = lstrlenA(mc.szTitle);
    tci.iImage      = sfi.iIcon;
    tci.lParam      = (LPARAM)hwnd[WinMdiChild];
    //SendMessageA(hWndTabCtrl, TCM_INSERTITEM, (WPARAM)1, (LPARAM)&tci);
    TabIndex = TabCtrl_GetItemCount(hwnd[WinTabBar]);
    TabCtrl_InsertItem(hwnd[WinTabBar], TabIndex, &tci);
    TabCtrl_SetCurSel(hwnd[WinTabBar], TabIndex);
    return 1;
}


struct MDICHILD MdiChild = {
    MdiChild_Create,
};
