#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct DIALOG Dialog;
extern struct STATUSBAR StatusBar;
extern struct MDICLIENT MdiClient;
extern struct MENUBAR MenuBar;
extern struct WINDOW wx[16];
extern HWND hwnd[16];

static SHFILEINFO sfi;
static HIMAGELIST fi;
static TC_ITEM tci;
static HIMAGELIST him;
static HICON hIcon;
static int TabIndex;
static DWORD Attributes;

LRESULT CALLBACK MdiFrame_OnSize(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    int i;
    for (i = 0; i < elementsof(wx); i++) {
        RECT rect;
        RECT rc;

        if (!wx[i].Parent) continue;
        if ((wx[i].Style & WS_CHILD) == 0) continue;

        GetClientRect(hwnd[wx[i].Parent], &rect);
        rc.left = (wx[i].PosX < 0)
            ? rect.right + 1 + wx[i].PosX
            : wx[i].PosX;
        rc.top = (wx[i].PosY < 0)
            ? rect.bottom + 1 + wx[i].PosY
            : wx[i].PosY;
        rc.right = (wx[i].Width < 0)
            ? rect.right + 1 + wx[i].Width
            : wx[i].Width;
        rc.bottom = (wx[i].Height < 0)
            ? rect.bottom + 1 + wx[i].Height
            : wx[i].Height;

        if (i == WinMdiClient) {
            SetWindowPos(hwnd[i], 0, rc.left, rc.top, rc.right, rc.bottom, SWP_NOZORDER);
        } else {
            MoveWindow(hwnd[i], rc.left, rc.top, rc.right, rc.bottom, TRUE);
        }
    }
    return 1;
}

LRESULT CALLBACK MdiFrame_OnCommand(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    MDICREATESTRUCT mc;
    DWORD style;
    char *path;
    switch (LOWORD(wParam)) {
    case WM_USER+0x0201:
        path = Dialog.OpenFileDialog(
            "CD Images (img bin iso)\0*.img;*.bin;*.iso\0"
            "All Files\0*.*\0\0");
        if (path) {
            StatusBar.SetStatus("In Progress", path);
        }
        break;
    case WM_USER+0x0202:
        path = Dialog.SaveFileDialog(
            "CD Images (img bin iso)\0*.img;*.bin;*.iso\0"
            "All Files\0*.*\0\0");
        if (path) {
            StatusBar.SetStatus("In Progress", path);
        }
        break;
    case WM_USER+0x0203:
        MessageBoxA(0,"File/Close", "Menu", 0);
        break;
    case WM_USER+0x0204:
        MessageBoxA(0,"File/Exit", "Menu", 0);
        break;
    case WM_USER+0x0301:
        MessageBoxA(0,"Edit/Undo", "Menu", 0);
        break;
    case WM_USER+0x0302:
        MessageBoxA(0,"Edit/Redo", "Menu", 0);
        break;
    case WM_USER+0x0303:
        MessageBoxA(0,"Edit/Cut", "Menu", 0);
        break;
    case WM_USER+0x0304:
        MessageBoxA(0,"Edit/Cut", "Copy", 0);
        break;
    case WM_USER+0x0305:
        MessageBoxA(0,"Edit/Cut", "Paste", 0);
        break;
    case WM_USER+0x0401:
        ShowWindow(hwnd[WinTreeView], SW_SHOW);
        ShowWindow(hwnd[WinListView], SW_HIDE);
        break;
    case WM_USER+0x0402:
        ShowWindow(hwnd[WinTreeView], SW_HIDE);
        ShowWindow(hwnd[WinListView], SW_SHOW);
        style = GetWindowLongA(hwnd[WinListView], GWL_STYLE) & (~LVS_TYPEMASK);
        SetWindowLongA(hwnd[WinListView], GWL_STYLE, style | LVS_ICON);
        break;
    case WM_USER+0x0403:
        ShowWindow(hwnd[WinTreeView], SW_HIDE);
        ShowWindow(hwnd[WinListView], SW_SHOW);
        style = GetWindowLongA(hwnd[WinListView], GWL_STYLE) & (~LVS_TYPEMASK);
        SetWindowLongA(hwnd[WinListView], GWL_STYLE, style| LVS_SMALLICON);
        break;
    case WM_USER+0x0404:
        ShowWindow(hwnd[WinTreeView], SW_HIDE);
        ShowWindow(hwnd[WinListView], SW_SHOW);
        style = GetWindowLongA(hwnd[WinListView], GWL_STYLE) & (~LVS_TYPEMASK);
        SetWindowLongA(hwnd[WinListView], GWL_STYLE, style | LVS_LIST);
        break;
    case WM_USER+0x0405:
        ShowWindow(hwnd[WinTreeView], SW_HIDE);
        ShowWindow(hwnd[WinListView], SW_SHOW);
        style = GetWindowLongA(hwnd[WinListView], GWL_STYLE) & (~LVS_TYPEMASK);
        SetWindowLongA(hwnd[WinListView], GWL_STYLE, style | LVS_REPORT);
        break;

    case WM_USER+0x0501:
        SendMessageA(hwnd[WinMdiClient], WM_MDITILE, MDITILE_HORIZONTAL, 0);
        break;
    case WM_USER+0x0502:
        SendMessageA(hwnd[WinMdiClient], WM_MDITILE, MDITILE_VERTICAL, 0);
        break;
    case WM_USER+0x0503:
        SendMessageA(hwnd[WinMdiClient], WM_MDICASCADE, MDITILE_SKIPDISABLED, 0);
        break;
    case WM_USER+0x0504:
        SendMessageA(hwnd[WinMdiClient], WM_MDIICONARRANGE, 0, 0);
        break;

    case WM_USER+0x0601:
        MessageBoxA(0,"Help/About", "Menu", 0);
        break;

    case WM_USER+0x0701:
        mc.style   = WS_OVERLAPPEDWINDOW | WS_CHILD | WS_VISIBLE | WS_CLIPSIBLINGS | WS_CLIPCHILDREN;
        mc.szClass = "MdiChild";

        mc.szTitle = "document.pdf";
        mc.x       = CW_USEDEFAULT;
        mc.y       = CW_USEDEFAULT;
        mc.cx      = CW_USEDEFAULT;
        mc.cy      = CW_USEDEFAULT;
        mc.hOwner  = GetModuleHandleA(0);
        mc.lParam  = 0;
        hwnd[WinMdiChild] = (HWND)SendMessageA(hwnd[WinMdiClient], WM_MDICREATE, 0, (LPARAM)&mc);
        if (!hwnd[WinMdiChild]) {
            return Logger.Error("Menu.New", "Error WM_MDICREATE");
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

        memset(&tci, 0, sizeof(tci));
        tci.mask        = TCIF_TEXT | TCIF_IMAGE | TCIF_PARAM;
        tci.pszText     = (char*)mc.szTitle;
        tci.cchTextMax  = lstrlenA(mc.szTitle);
        tci.iImage      = sfi.iIcon;
        tci.lParam      = (LPARAM)hwnd[WinMdiChild];
        //SendMessageA(hWndTabCtrl, TCM_INSERTITEM, (WPARAM)1, (LPARAM)&tci);
        TabIndex = TabCtrl_GetItemCount(hwnd[WinTabBar]);
        TabCtrl_InsertItem(hwnd[WinTabBar], TabIndex, &tci);
        TabCtrl_SetCurSel(hwnd[WinTabBar], TabIndex);
        break;
    }
    return 1;
}

static LRESULT CALLBACK MdiFrame_OnNotify(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    TCITEM tci;
    RECT rc;
    int TabIndex;
    NMHDR *NmHdr = (NMHDR*)lParam;

    switch (NmHdr->code) {
    case TCN_SELCHANGE:
        TabIndex = TabCtrl_GetCurSel(hwnd[WinTabBar]);
        tci.mask = TCIF_PARAM;
        TabCtrl_GetItem(hwnd[WinTabBar], TabIndex, &tci);
        if (tci.lParam) {
            SendMessageA(hwnd[WinMdiClient], WM_MDIACTIVATE, (WPARAM)tci.lParam, 0);
            GetClientRect(hwnd[WinMdiClient], &rc);
            InvalidateRect(hwnd[WinMdiClient], &rc, TRUE);
        }
        break;
    }
    return 1;
}

LRESULT CALLBACK MdiFrameProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_CREATE:
        hwnd[WinMdiFrame] = hWnd;
        MenuBar.SetMenu(WinMdiFrame, wx[WinMdiFrame].Menu);
        MdiClient.Create();
        break;
    case WM_COMMAND:
        MdiFrame_OnCommand(hWnd, uMsg, wParam, lParam);
        break;
    case WM_NOTIFY:
        MdiFrame_OnNotify(hWnd, uMsg, wParam, lParam);
        break;
    case WM_ENTERSIZEMOVE:     case WM_EXITSIZEMOVE:
    case WM_SIZING:            case WM_SIZE:
    case WM_MOVING:            case WM_MOVE:
    case WM_WINDOWPOSCHANGING: case WM_WINDOWPOSCHANGED:
        MdiFrame_OnSize(hWnd, uMsg, wParam, lParam);
        if (uMsg == WM_WINDOWPOSCHANGED) return 0;
        break;
    case WM_CLOSE:
        PostQuitMessage(0);
        break;
    }
    if (hwnd[WinMdiClient]) {
        return DefFrameProcA(hWnd, hwnd[WinMdiClient], uMsg, wParam, lParam);
    } else {
        return DefWindowProcA(hWnd, uMsg, wParam, lParam);
    }
}
