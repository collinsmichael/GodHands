#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct DIALOG Dialog;
extern struct STATUSBAR StatusBar;
extern struct WINDOW wx[16];
extern HWND hwnd[16];


LRESULT CALLBACK MdiFrame_OnSize(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    int i;
    for (i = 0; i < elementsof(wx); i++) {
        RECT rect;
        RECT rc;

        if (!wx[i].Class) continue;
        if ((wx[i].Style & WS_CHILD) == 0) continue;

        GetClientRect(hwnd[wx[i].Parent], &rect);
        rc.left = (wx[i].PosX < 0)
            ? rect.right + wx[i].PosX
            : wx[i].PosX;
        rc.top = (wx[i].PosY < 0)
            ? rect.bottom + wx[i].PosY
            : wx[i].PosY;
        rc.right = (wx[i].Width < 0)
            ? rect.right + wx[i].Width
            : wx[i].Width;
        rc.bottom = (wx[i].Height < 0)
            ? rect.bottom + wx[i].Height
            : wx[i].Height;
        MoveWindow(hwnd[i], rc.left, rc.top, rc.right, rc.bottom, TRUE);
    }
    return 1;
}

LRESULT CALLBACK MdiFrame_OnCommand(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
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
        MessageBoxA(0,"Help/About", "Menu", 0);
        break;
    }
    return 1;
}

LRESULT CALLBACK MdiFrameProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_COMMAND:
        MdiFrame_OnCommand(hWnd, uMsg, wParam, lParam);
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
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}
