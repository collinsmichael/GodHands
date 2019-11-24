#include <windows.h>
#include "GodHands.h"


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
