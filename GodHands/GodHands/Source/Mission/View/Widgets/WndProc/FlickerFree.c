#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


static LRESULT CALLBACK FlickerFreeProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    WNDPROC WndProc;
    RECT    rc;
    HDC     hdcMem;
    HBITMAP hbmMem, hbmOld;
    HBRUSH  hbrBkGnd;
    PAINTSTRUCT ps;

    WndProc = (WNDPROC)GetProp(hWnd, "WNDPROC");
    switch (uMsg) {
    case WM_ERASEBKGND:
        return 1;
    case WM_PAINT:
        BeginPaint(hWnd, &ps);
        GetClientRect(hWnd, &rc);
        hdcMem = CreateCompatibleDC(ps.hdc);
        hbmMem = CreateCompatibleBitmap(ps.hdc, rc.right-rc.left, rc.bottom-rc.top);
        hbmOld = (HBITMAP)SelectObject(hdcMem, hbmMem);
        hbrBkGnd = (HBRUSH)GetClassLong(hWnd, GCL_HBRBACKGROUND);
        FillRect(hdcMem, &rc, hbrBkGnd);
        DeleteObject(hbrBkGnd);
        CallWindowProcA(WndProc, hWnd, uMsg, (WPARAM)hdcMem, 0);
        BitBlt(ps.hdc, rc.left, rc.top, rc.right-rc.left, rc.bottom-rc.top, hdcMem, 0, 0, SRCCOPY);
        SelectObject(hdcMem, hbmOld);
        DeleteObject(hbmMem);
        DeleteDC(hdcMem);
        EndPaint(hWnd, &ps);
        return 0;
    case WM_CLOSE:
    case WM_DESTROY:
    case WM_ENDSESSION:
        SetWindowLong(hWnd, GWL_WNDPROC, (LONG)WndProc);
        break;
    }
    return CallWindowProcA(WndProc, hWnd, uMsg, wParam, lParam);
}


int FlickerFree(HWND hwnd) {
    WNDPROC WndProc = (WNDPROC)GetWindowLongA(hwnd, GWL_WNDPROC);
    if (WndProc) {
        if (SetProp(hwnd, "WNDPROC", (HANDLE)WndProc)) {
            SetWindowLong(hwnd, GWL_WNDPROC, (LONG)FlickerFreeProc);
        }
    }
    return 1;
}
