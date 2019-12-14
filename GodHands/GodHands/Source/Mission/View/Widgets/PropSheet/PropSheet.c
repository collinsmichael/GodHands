#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"



LRESULT CALLBACK PropSheetProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_CREATE:
        return 0;
    case WM_CLOSE:
        break;
    }
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}
