#include <windows.h>
#include "GodHands.h"


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


LRESULT CALLBACK MdiFrameProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_SIZE: MdiFrame_OnSize(hWnd, uMsg, wParam, lParam); break;
    case WM_CLOSE:
        PostQuitMessage(0);
        break;
    }
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}
