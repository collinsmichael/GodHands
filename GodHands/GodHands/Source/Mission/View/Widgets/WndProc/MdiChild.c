#include <windows.h>
#include "GodHands.h"


LRESULT CALLBACK MdiChildProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}
