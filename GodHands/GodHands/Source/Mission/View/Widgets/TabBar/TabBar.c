#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern LOGGER Logger;
extern ICON Icon;
extern HWND hwnd[64];

static TC_ITEM tci;
static HIMAGELIST hSmallIcons;
static HIMAGELIST him;


int TabBar_StartUp(void) {
    hSmallIcons = Icon.GetSmallIcons();
    him = TabCtrl_SetImageList(hwnd[WinTabBar], hSmallIcons);
    if (him) ImageList_Destroy(him);
    return 1;
}

int TabBar_Insert(char *text, void *param) {
    int tab;
    tci.mask        = TCIF_TEXT | TCIF_IMAGE | TCIF_PARAM;
    tci.pszText     = text;
    tci.cchTextMax  = lstrlenA(text);
    tci.iImage      = Icon.GetIndexFromAttributes(text, FILE_ATTRIBUTE_NORMAL);
    tci.lParam      = (LPARAM)param;
    tab = TabCtrl_GetItemCount(hwnd[WinTabBar]);
    TabCtrl_InsertItem(hwnd[WinTabBar], tab, &tci);
    TabCtrl_SetCurSel(hwnd[WinTabBar], tab);
    return 1;
}

int TabBar_Remove(void *param) {
    RECT rc;
    int i;
    int count = TabCtrl_GetItemCount(hwnd[WinTabBar]);
    for (i = 0; i < count; i++) {
        tci.mask = TCIF_PARAM;
        TabCtrl_GetItem(hwnd[WinTabBar], i, &tci);
        if (tci.lParam == (LPARAM)param) {
            TabCtrl_DeleteItem(hwnd[WinTabBar], i);
            break;
        }
    }
    GetClientRect(hwnd[WinTabBar], &rc);
    InvalidateRect(hwnd[WinTabBar], &rc, TRUE);
    return 1;
}

int TabBar_SwitchTo(void *param) {
    RECT rc;
    int i;
    int count = TabCtrl_GetItemCount(hwnd[WinTabBar]);
    for (i = 0; i < count; i++) {
        tci.mask = TCIF_PARAM;
        TabCtrl_GetItem(hwnd[WinTabBar], i, &tci);
        if (tci.lParam == (LPARAM)param) {
            TabCtrl_SetCurSel(hwnd[WinTabBar], i);
            break;
        }
    }
    GetClientRect(hwnd[WinTabBar], &rc);
    InvalidateRect(hwnd[WinTabBar], &rc, TRUE);
    GetClientRect(hwnd[WinMdiClient], &rc);
    InvalidateRect(hwnd[WinMdiClient], &rc, TRUE);
    return 1;
}


struct TABBAR TabBar = {
    TabBar_StartUp,
    TabBar_Insert,
    TabBar_Remove,
    TabBar_SwitchTo,
};
