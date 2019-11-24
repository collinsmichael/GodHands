#include <windows.h>
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
    char *path;
    switch (LOWORD(wParam)) {
    case WM_USER+0x0101:
        path = Dialog.OpenFileDialog(
            "CD Images (img bin iso)\0*.img;*.bin;*.iso\0"
            "All Files\0*.*\0\0");
        if (path) {
            StatusBar.SetStatus("In Progress", path);
        }
        break;
    case WM_USER+0x0102:
        path = Dialog.SaveFileDialog(
            "CD Images (img bin iso)\0*.img;*.bin;*.iso\0"
            "All Files\0*.*\0\0");
        if (path) {
            StatusBar.SetStatus("In Progress", path);
        }
        break;
    case WM_USER+0x0103:
        MessageBoxA(0,"File/Close", "Menu", 0);
        break;
    case WM_USER+0x0104:
        MessageBoxA(0,"File/Exit", "Menu", 0);
        break;
    case WM_USER+0x0201:
        MessageBoxA(0,"Edit/Undo", "Menu", 0);
        break;
    case WM_USER+0x0202:
        MessageBoxA(0,"Edit/Redo", "Menu", 0);
        break;
    case WM_USER+0x0203:
        MessageBoxA(0,"Edit/Cut", "Menu", 0);
        break;
    case WM_USER+0x0204:
        MessageBoxA(0,"Edit/Cut", "Copy", 0);
        break;
    case WM_USER+0x0205:
        MessageBoxA(0,"Edit/Cut", "Paste", 0);
        break;
    case WM_USER+0x0301:
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
    case WM_SIZE:
        MdiFrame_OnSize(hWnd, uMsg, wParam, lParam);
        break;
    case WM_CLOSE:
        PostQuitMessage(0);
        break;
    }
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}
