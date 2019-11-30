#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"

LRESULT CALLBACK OnNotifyProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

extern struct LOGGER Logger;
extern struct DIALOG Dialog;
extern struct JOBQUEUE JobQueue;
extern struct STATUSBAR StatusBar;
extern struct LISTVIEW ListView;
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
    RECT frame;
    RECT toolbar;
    RECT tabbar;
    RECT status;
    RECT tree;
    RECT list;
    RECT client;

    GetClientRect(hwnd[WinMdiFrame], &frame);
    MoveWindow(hwnd[WinStatusBar], frame.left, frame.top, frame.right, frame.bottom, TRUE);
    GetClientRect(hwnd[WinStatusBar], &status);
    MoveWindow(hwnd[WinProgressBar], status.left+2, status.top+4, 132, status.bottom-6, TRUE);
    GetClientRect(hwnd[WinTabBar], &tabbar);
    frame.bottom -= status.bottom;

    GetClientRect(hwnd[WinToolBar], &toolbar);
    frame.top += toolbar.bottom;
    toolbar.left = frame.left;
    toolbar.right = frame.right;
    MoveWindow(hwnd[WinToolBar], toolbar.left, toolbar.top, toolbar.right, toolbar.bottom, TRUE);

    GetWindowRect(hwnd[WinTreeView], &tree);
    GetWindowRect(hwnd[WinListView], &list);
    tree.right = tree.right - tree.left;
    tree.left = 0;
    tree.top = frame.top;
    tree.bottom = frame.bottom - toolbar.bottom;

    list.right = list.right - list.left;
    list.left = 0;
    list.top = frame.top;
    list.bottom = frame.bottom - toolbar.bottom;

    tabbar.top = frame.top;
    tabbar.left = tree.right + 4;
    tabbar.right = frame.right - tabbar.left;

    client.top = frame.top + tabbar.bottom;
    client.bottom = frame.bottom - client.top;
    client.left = tabbar.left;
    client.right = tabbar.right;

    MoveWindow(hwnd[WinTabBar], tabbar.left, tabbar.top, tabbar.right, tabbar.bottom, TRUE);
    MoveWindow(hwnd[WinTreeView], tree.left, tree.top, tree.right, tree.bottom, TRUE);
    MoveWindow(hwnd[WinListView], list.left, list.top, list.right, list.bottom, TRUE);
    MoveWindow(hwnd[WinSplitter], tree.right, tree.top, 4, tree.bottom, TRUE);
    SetWindowPos(hwnd[WinMdiClient], 0, client.left, client.top, client.right, client.bottom, SWP_NOZORDER);
    return 1;
}

LRESULT CALLBACK MdiFrame_OnCommand(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    MDICREATESTRUCT mc;
    DWORD style;
    int code = LOWORD(wParam);

    switch (LOWORD(wParam)) {
    case WM_USER+0x0101:
        ListView.NavBack();
        break;
    case WM_USER+0x0102:
        ListView.NavForward();
        break;
    case WM_USER+0x0201:
        MenuFile_Open(0);
        break;
    case WM_USER+0x0202:
        JobQueue.Schedule(MenuFile_Save, 0);
        break;
    case WM_USER+0x0203:
        MenuFile_Close(0);
        break;
    case WM_USER+0x0204:
        MenuFile_Exit(0);
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
        ListView_Arrange(hwnd[WinListView], LVA_DEFAULT);
        break;
    case WM_USER+0x0403:
        ShowWindow(hwnd[WinTreeView], SW_HIDE);
        ShowWindow(hwnd[WinListView], SW_SHOW);
        style = GetWindowLongA(hwnd[WinListView], GWL_STYLE) & (~LVS_TYPEMASK);
        SetWindowLongA(hwnd[WinListView], GWL_STYLE, style| LVS_SMALLICON);
        ListView_Arrange(hwnd[WinListView], LVA_DEFAULT);
        break;
    case WM_USER+0x0404:
        ShowWindow(hwnd[WinTreeView], SW_HIDE);
        ShowWindow(hwnd[WinListView], SW_SHOW);
        style = GetWindowLongA(hwnd[WinListView], GWL_STYLE) & (~LVS_TYPEMASK);
        SetWindowLongA(hwnd[WinListView], GWL_STYLE, style | LVS_LIST);
        ListView_Arrange(hwnd[WinListView], LVA_DEFAULT);
        break;
    case WM_USER+0x0405:
        ShowWindow(hwnd[WinTreeView], SW_HIDE);
        ShowWindow(hwnd[WinListView], SW_SHOW);
        style = GetWindowLongA(hwnd[WinListView], GWL_STYLE) & (~LVS_TYPEMASK);
        SetWindowLongA(hwnd[WinListView], GWL_STYLE, style | LVS_REPORT);
        ListView_Arrange(hwnd[WinListView], LVA_DEFAULT);
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
        OnNotifyProc(hWnd, uMsg, wParam, lParam);
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
