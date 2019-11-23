#include <windows.h>
#include "GodHands.h"


LRESULT CALLBACK MdiFrameProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_KEYDOWN:
    case WM_CLOSE:
        PostQuitMessage(0);
        break;
    }
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}
