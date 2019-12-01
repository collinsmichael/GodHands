#include <stdint.h>
#include <windows.h>
#include "GodHands.h"


extern struct LOGGER Logger;


LRESULT CALLBACK ZndWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_CREATE:
        CreateWindowExA(0,"label", "HP", WS_CHILD|WS_VISIBLE, 100,100,100,50, hWnd, 0,0,0);
        CreateWindowExA(0,"edit", "MP", WS_CHILD|WS_VISIBLE, 200,100,100,50, hWnd, 0,0,0);
        CreateWindowExA(0,"label", "HP", WS_CHILD|WS_VISIBLE, 100,100,100,50, hWnd, 0,0,0);
        CreateWindowExA(0,"edit", "MP", WS_CHILD|WS_VISIBLE, 200,100,100,50, hWnd, 0,0,0);
        break;
    }
    return 0;
}
