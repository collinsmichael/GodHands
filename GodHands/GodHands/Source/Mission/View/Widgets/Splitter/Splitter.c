#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern HWND hwnd[64];
static HWND hWndLeft[4];
static HWND hWndRight[4];


int Splitter_StartUp(void) {
    hWndLeft[0] = hwnd[WinTreeView];
    hWndLeft[1] = hwnd[WinListView];
    hWndRight[0] = hwnd[WinTabBar];
    hWndRight[1] = hwnd[WinMdiClient];
    SendMessageA(hwnd[WinSplitter], SB_SETLEFTWINDOWS,  (WPARAM)2, (LPARAM)hWndLeft);
    SendMessageA(hwnd[WinSplitter], SB_SETRIGHTWINDOWS, (WPARAM)2, (LPARAM)hWndRight);
    //hWndLeft[0]  = hwnd[WinTabBar];
    //hWndLeft[1]  = hwnd[WinMdiClient];
    //hWndRight[0] = hwnd[WinListView];
    //SendMessageA(hwnd[WinSplitLv], SB_SETLEFTWINDOWS,  (WPARAM)2, (LPARAM)hWndLeft);
    //SendMessageA(hwnd[WinSplitLv], SB_SETRIGHTWINDOWS, (WPARAM)1, (LPARAM)hWndRight);
    return 1;
}

static LRESULT Splitter_OnLButtonDown(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    POINT pt;
    int   DragMode;
    GetCursorPos(&pt);
    DragMode = TRUE;
    SetPropA(hWnd, "DRAGMODE", (HANDLE)DragMode);
    SetCapture(hWnd);
    SetPropA(hWnd, "POSXLAST", (HANDLE)pt.x);
    SetPropA(hWnd, "POSYLAST", (HANDLE)pt.y);
    return 0;
}

static LRESULT Splitter_OnLButtonUp(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    int DragMode;

    DragMode = (int)GetPropA(hWnd, "DRAGMODE");
    if (DragMode == FALSE) return 0;
    DragMode = FALSE;
    SetPropA(hWnd, "DRAGMODE", (HANDLE)DragMode);
    ReleaseCapture();
    return 0;
}

static LRESULT Splitter_OnMouseMove(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    POINT pt;
    POINT ptLast;
    POINT ptMove;

    int   DragMode;
    int   NumLeftWindows;
    HWND *hWndLeft;
    int   NumRightWindows;
    HWND *hWndRight;
    int   i;

    DragMode = (int)GetPropA(hWnd, "DRAGMODE");
    if (DragMode == FALSE) return 0;
    GetCursorPos(&pt);

    ptLast.x = (LONG)GetPropA(hWnd, "POSXLAST");
    ptLast.y = (LONG)GetPropA(hWnd, "POSYLAST");
    SetPropA(hWnd, "POSXLAST", (HANDLE)pt.x);
    SetPropA(hWnd, "POSYLAST", (HANDLE)pt.y);
    ptMove.x = pt.x - ptLast.x;
    ptMove.y = pt.y - ptLast.y;

    NumLeftWindows  = (int)GetPropA(hWnd, "NUMLEFTWINDOWS");
    NumRightWindows = (int)GetPropA(hWnd, "NUMRIGHTWINDOWS");
    hWndLeft        = (HWND*)GetPropA(hWnd, "HWNDLEFTWINDOWS");
    hWndRight       = (HWND*)GetPropA(hWnd, "HWNDRIGHTWINDOWS");

    if (hWndLeft) {
        for (i = 0; i < NumLeftWindows; i++) {
            RECT  rc;
            POINT ptLeft;

            if (!hWndLeft[i]) continue;
            GetWindowRect(hWndLeft[i], &rc);

            ptLeft.x = rc.left;
            ptLeft.y = rc.top;
            ScreenToClient(GetParent(hWndLeft[i]), &ptLeft);
            rc.left = ptLeft.x;
            rc.top  = ptLeft.y;

            ptLeft.x = rc.right;
            ptLeft.y = rc.bottom;
            ScreenToClient(GetParent(hWndLeft[i]), &ptLeft);
            rc.right  = ptLeft.x - rc.left;
            rc.bottom = ptLeft.y - rc.top;

            rc.right  += (signed long)ptMove.x;
            if (rc.right < 0) {
                return 0;
            }
        }
    }

    if (hWndRight) {
        for (i = 0; i < NumRightWindows; i++) {
            RECT  rc;
            POINT ptLeft;

            if (!hWndRight[i]) continue;
            GetWindowRect(hWndRight[i], &rc);

            ptLeft.x = rc.left;
            ptLeft.y = rc.top;
            ScreenToClient(GetParent(hWndRight[i]), &ptLeft);
            rc.left = ptLeft.x;
            rc.top  = ptLeft.y;

            ptLeft.x = rc.right;
            ptLeft.y = rc.bottom;
            ScreenToClient(GetParent(hWndRight[i]), &ptLeft);
            rc.right  = ptLeft.x - rc.left;
            rc.bottom = ptLeft.y - rc.top;

            rc.left   += (signed long)ptMove.x;
            rc.right  -= (signed long)ptMove.x;
            if ((rc.right < 0) || (rc.left < 0)) {
                return 0;
            }
        }
    }
    {
        POINT TopLeft;
        POINT BtmRight;
        RECT  Splitter;

        GetWindowRect(hWnd, &Splitter);
        TopLeft.x  = Splitter.left;
        TopLeft.y  = Splitter.top;
        BtmRight.x = Splitter.right;
        BtmRight.y = Splitter.bottom;
        ScreenToClient(GetParent(hWnd), &TopLeft);
        ScreenToClient(GetParent(hWnd), &BtmRight);
        Splitter.left   = TopLeft.x;
        Splitter.top    = TopLeft.y;
        Splitter.right  = BtmRight.x;
        Splitter.bottom = BtmRight.y;
        Splitter.right  -= Splitter.left;
        Splitter.bottom -= Splitter.top;

        Splitter.left   += (signed long)ptMove.x;
        Splitter.right  -= (signed long)ptMove.x;
        if (Splitter.left  < 0) Splitter.left = 0;
        MoveWindow(hWnd, Splitter.left, Splitter.top, 4, Splitter.bottom, TRUE);
    }

    if (hWndLeft) {
        for (i = 0; i < NumLeftWindows; i++) {
            RECT  rc;
            POINT ptLeft;

            if (!hWndLeft[i]) continue;
            GetWindowRect(hWndLeft[i], &rc);

            ptLeft.x = rc.left;
            ptLeft.y = rc.top;
            ScreenToClient(GetParent(hWndLeft[i]), &ptLeft);
            rc.left = ptLeft.x;
            rc.top  = ptLeft.y;

            ptLeft.x = rc.right;
            ptLeft.y = rc.bottom;
            ScreenToClient(GetParent(hWndLeft[i]), &ptLeft);
            rc.right  = ptLeft.x - rc.left;
            rc.bottom = ptLeft.y - rc.top;
            rc.right  += (signed long)ptMove.x;
            MoveWindow(hWndLeft[i], rc.left, rc.top, rc.right, rc.bottom, TRUE);
        }
    }

    if (hWndRight) {
        for (i = 0; i < NumRightWindows; i++) {
            RECT  rc;
            POINT ptLeft;

            if (!hWndRight[i]) continue;
            GetWindowRect(hWndRight[i], &rc);

            ptLeft.x = rc.left;
            ptLeft.y = rc.top;
            ScreenToClient(GetParent(hWndRight[i]), &ptLeft);
            rc.left = ptLeft.x;
            rc.top  = ptLeft.y;

            ptLeft.x = rc.right;
            ptLeft.y = rc.bottom;
            ScreenToClient(GetParent(hWndRight[i]), &ptLeft);
            rc.right  = ptLeft.x - rc.left;
            rc.bottom = ptLeft.y - rc.top;

            rc.left   += (signed long)ptMove.x;
            rc.right  -= (signed long)ptMove.x;
            MoveWindow(hWndRight[i], rc.left, rc.top, rc.right, rc.bottom, TRUE);
        }
    }
    ListView_Arrange(hwnd[WinListView], LVA_DEFAULT);
    return 0;
}

static LRESULT CALLBACK Splitter_OnSetLeftWindows(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    int   NumWindows;
    HWND *hWindows;
    HWND *hWin;

    NumWindows = (int)wParam;
    hWindows   = (HWND*)lParam;

    if (NumWindows > 0) {
        hWin = (HWND*)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, (NumWindows+1)*sizeof(HWND));
        if (hWin) {
            int i;
            for (i = 0; i < NumWindows; i++) {
                hWin[i] = hWindows[i];
            }
            SetPropA(hWnd, "NUMLEFTWINDOWS", (HANDLE)NumWindows);
            SetPropA(hWnd, "HWNDLEFTWINDOWS", (HANDLE)hWin);
        }
    } else {
        hWin = (HWND*)GetPropA(hWnd, "HWNDLEFTWINDOWS");
        if (hWin) {
            HeapFree(GetProcessHeap(), 0, hWin);
        }
        SetPropA(hWnd, "NUMLEFTWINDOWS", (HANDLE)0);
        SetPropA(hWnd, "HWNDLEFTWINDOWS", (HANDLE)0);
    }
    return 0;
}

static LRESULT CALLBACK Splitter_OnSetRightWindows(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    int   NumWindows;
    HWND *hWindows;
    HWND *hWin;

    NumWindows = (int)wParam;
    hWindows   = (HWND*)lParam;

    if (NumWindows > 0) {
        hWin = (HWND*)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, (NumWindows+1)*sizeof(HWND));
        if (hWin) {
            int i;
            for (i = 0; i < NumWindows; i++) {
                hWin[i] = hWindows[i];
            }
            SetPropA(hWnd, "NUMRIGHTWINDOWS", (HANDLE)NumWindows);
            SetPropA(hWnd, "HWNDRIGHTWINDOWS", (HANDLE)hWin);
        }
    } else {
        hWin = (HWND*)GetPropA(hWnd, "HWNDRIGHTWINDOWS");
        if (hWin) {
            HeapFree(GetProcessHeap(), 0, hWin);
        }
        SetPropA(hWnd, "NUMRIGHTWINDOWS", (HANDLE)0);
        SetPropA(hWnd, "HWNDRIGHTWINDOWS", (HANDLE)0);
    }
    return 0;
}

LRESULT CALLBACK SplitterProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg) {
    case SB_SETLEFTWINDOWS:  Splitter_OnSetLeftWindows(hWnd, uMsg, wParam, lParam);  break;
    case SB_SETRIGHTWINDOWS: Splitter_OnSetRightWindows(hWnd, uMsg, wParam, lParam); break;
    case WM_LBUTTONDOWN:     Splitter_OnLButtonDown(hWnd, uMsg, wParam, lParam);     break;
    case WM_LBUTTONUP:       Splitter_OnLButtonUp(hWnd, uMsg, wParam, lParam);       break;
    case WM_MOUSEMOVE:       Splitter_OnMouseMove(hWnd, uMsg, wParam, lParam);       break;
    }
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}


struct SPLITTER Splitter = {
    Splitter_StartUp
};
