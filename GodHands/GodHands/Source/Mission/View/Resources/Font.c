/*****************************************************************************/
/* win32 list view example                                                   */
/*****************************************************************************/
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


static LOGFONTA lf[] = {
    { -11,0,0,0,400,0,0,0,0,1,2,1,34, "MS Sans Serif" },
    { -13,0,0,0,400,0,0,0,0,3,2,1,49, "Courier New" },
    { -13,0,0,0,400,0,0,0,0,3,2,1,49, "Consolas" }
};
static HFONT hfont[4];


#define MsSansSerif 1
#define CourierNew  2
#define Consolas    3


static int Font_StartUp(void) {
    hfont[MsSansSerif] = CreateFontIndirectA(&lf[MsSansSerif-1]);
    hfont[CourierNew] = CreateFontIndirectA(&lf[CourierNew-1]);
    hfont[Consolas] = CreateFontIndirectA(&lf[Consolas-1]);
    return 1;
}

static int Font_SetFont(HWND hWnd, char *font) {
    if (lstrcmp(font, "MS Sans Serif") == 0) {
        SendMessageA(hWnd, WM_SETFONT, (WPARAM)hfont[MsSansSerif], 0);
    } else if (lstrcmp(font, "Courier New") == 0) {
        SendMessageA(hWnd, WM_SETFONT, (WPARAM)hfont[CourierNew], 0);
    } else if (lstrcmp(font, "Consolas") == 0) {
        SendMessageA(hWnd, WM_SETFONT, (WPARAM)hfont[Consolas], 0);
    }
    return 1;
}


struct FONT Font = {
    Font_StartUp,
    Font_SetFont
};
