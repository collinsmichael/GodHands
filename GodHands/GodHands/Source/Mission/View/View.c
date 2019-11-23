#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern LRESULT CALLBACK MdiFrameProc(HWND,UINT,WPARAM,LPARAM);
extern LRESULT CALLBACK MdiChildProc(HWND,UINT,WPARAM,LPARAM);

extern struct LOGGER    Logger;
extern struct MENUBAR   MenuBar;
extern struct STATUSBAR StatusBar;
extern struct TOOLTIP   ToolTip;

extern HMENU hmenu[64];
extern HACCEL hAccel;


static struct WINCLASS cx[] = {
    { "MdiFrame",MdiFrameProc,IDI_APPLICATION,IDC_ARROW,0x7F7F7F },
    { "MdiChild",MdiChildProc,IDI_APPLICATION,IDC_ARROW,0x7F7F7F },
};
static struct WINDOW wx[] = {
    { 0 },
    { 0,"MdiFrame","GodHands", 0x16CF0000,0x80000000,0x80000000,640,480,          0,0x01,0, "GodHands" },
    { 0,"tooltips_class32",0,  0x00000001,0x80000000,0x80000000,  0,  0,          0,0x00,0, 0},
    { 0,"msctls_statusbar32",0,0x56000100,0x80000000,0x80000000,  0,  0,WinMdiFrame,0x00,0, "StatusBar" },
};

ATOM atom[elementsof(cx)];
HWND hwnd[64];
HDC hdc[64];
HINSTANCE hInstance;

static int View_StartUp(void) {
    int i;

    InitCommonControls();
    OleInitialize(0);

    hwnd[WinConsole] = GetConsoleWindow();
    ShowWindow(hwnd[WinConsole], SW_HIDE);

    hInstance = GetModuleHandle(0);
    if (!MenuBar.StartUp()) return 0;

    for (i = 0; i < elementsof(cx); i++) {
        WNDCLASSA wc;
        stosb(&wc, 0, sizeof(wc));
        wc.lpfnWndProc = cx[i].WndProc;
        wc.lpszClassName = cx[i].ClassName;

        wc.style = 0x23;
        wc.hInstance = hInstance;
        wc.hIcon = LoadIconA(0, cx[i].hIcon);
        wc.hCursor = LoadCursorA(0, cx[i].hCursor);
        wc.hbrBackground = CreateSolidBrush(cx[i].hBackground);
        atom[i] = RegisterClassA(&wc);
        if (!atom[i]) {
            return Logger.Error("View.StartUp",
                "Class not registered '%s'", cx[i].ClassName);
        }
    }

    for (i = 0; i < elementsof(wx); i++) {
        if (!wx[i].Class) continue;
        hwnd[i] = CreateWindowExA(wx[i].ExStyle, wx[i].Class, wx[i].Window,
            wx[i].Style, wx[i].PosX, wx[i].PosY, wx[i].Width, wx[i].Height,
            hwnd[wx[i].Parent], hmenu[wx[i].Menu], hInstance, wx[i].Param);
        if (!hwnd[i]) {
            return Logger.Error("View.StartUp", "Error creating window");
        }
    }

    StatusBar.StartUp();
    for (i = 0; i < elementsof(wx); i++) {
        if (wx[i].ToolTip) {
            ToolTip.SetToolTip(i, wx[i].ToolTip);
        }
    }

    StatusBar.SetStatus("TEST", "TESTING STATUSBAR");
    return Logger.Done("View.StartUp", "Done");
}

static int View_CleanUp(void) {
    int i;

    hInstance = GetModuleHandle(0);
    for (i = elementsof(wx)-1; i >= 0; i--) {
        if (atom[i]) UnregisterClassA(cx[i].ClassName, hInstance);
    }
    MenuBar.CleanUp();
    OleUninitialize();
    ShowWindow(hwnd[WinConsole], SW_SHOW);
    SetForegroundWindow(hwnd[WinConsole]);
    return Logger.Done("View.CleanUp", "Done");
}

static int View_Execute(void) {
    MSG msg;
    do {
        if (GetMessageA(&msg,0,0,0)) {
            TranslateMessage(&msg);
            DispatchMessageA(&msg);
        }
    } while (msg.message != WM_QUIT);
    return Logger.Done("View.Execute", "Done");
}


struct VIEW View = {
    View_StartUp,
    View_CleanUp,
    View_Execute,
};
