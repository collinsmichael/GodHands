#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;


LRESULT CALLBACK ZudWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_CREATE:
        CreateWindowExA(0,"button", "ZUD",WS_CHILD|WS_VISIBLE, 100,100,100,50, hWnd, 0,0,0);
        break;
    }
    return 0;
}
