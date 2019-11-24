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


struct TABBAR TabBar = {
    TabBar_StartUp
};
