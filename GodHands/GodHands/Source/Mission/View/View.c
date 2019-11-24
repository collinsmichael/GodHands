#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern LRESULT CALLBACK MdiFrameProc(HWND,UINT,WPARAM,LPARAM);
extern LRESULT CALLBACK MdiChildProc(HWND,UINT,WPARAM,LPARAM);
int FlickerFree(HWND hwnd);


extern struct LOGGER    Logger;
extern struct FONT      Font;
extern struct MENUBAR   MenuBar;
extern struct STATUSBAR StatusBar;
extern struct TOOLTIP   ToolTip;
extern struct TREEVIEW  TreeView;
extern struct LISTVIEW  ListView;

extern HMENU hmenu[64];
extern HACCEL hAccel;


static struct WINCLASS cx[] = {
    { "MdiFrame",MdiFrameProc,IDI_APPLICATION,IDC_ARROW, 0x7F7F7F },
    { "MdiChild",MdiChildProc,IDI_APPLICATION,IDC_ARROW, 0x7F7F7F },
};

struct WINDOW wx[16] = {
    { 0 },
    { 0,"MdiFrame","GodHands", 0x06CF0000,0,0,640,480,0,           0,0x01, "MS Sans Serif", "GodHands" },
    { 0 },
    { 0,"tooltips_class32",  0,0x00000001,0,0,  0,  0,0,           0,0x00, "MS Sans Serif", 0 },
    { 0,"msctls_statusbar32",0,0x56000100,0,0,  0,  0,WinMdiFrame, 0,0x00, "MS Sans Serif", "StatusBar" },
    { 0,"msctls_progress32", 0,0x56000000,4,4,128, -6,WinStatusBar,0,0x00, "MS Sans Serif", "ProgressBar" },
    { 0 },//{ 0,"SysTreeView32",     0,0x5600000F,0,0, -1, -1,WinMdiFrame, 0,0x00, "MS Sans Serif", "TreeView" },
    { 0,"SysListView32",     0,0x56000249,0,0, -1, -1,WinMdiFrame, 0,0x00, "MS Sans Serif", "ListView" },
    { 0,0,                   0,0x00000000,0,0,  0,  0,0,           0,0x00, "MS Sans Serif", "ListViewHeader" },
};

ATOM atom[elementsof(cx)];
HWND hwnd[64];
HDC hdc[64];
HINSTANCE hInstance;

static int View_StartUp(void) {
    int i;

    InitCommonControls();
    OleInitialize(0);

    hInstance = GetModuleHandle(0);
    if (!Font.StartUp()) return 0;
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
        if (cx[i].hBackground < 0) {
            wc.hbrBackground = (HBRUSH)(-cx[i].hBackground);
        } else {
            wc.hbrBackground = CreateSolidBrush(cx[i].hBackground);
        }
        atom[i] = RegisterClassA(&wc);
        if (!atom[i]) {
            return Logger.Error("View.StartUp",
                "Class not registered '%s'", cx[i].ClassName);
        }
    }

    wx[WinMdiFrame].PosX = CW_USEDEFAULT;
    wx[WinMdiFrame].PosY = CW_USEDEFAULT;
    for (i = 0; i < elementsof(wx); i++) {
        if (!wx[i].Class) continue;
        hwnd[i] = CreateWindowExA(wx[i].ExStyle, wx[i].Class, wx[i].Window,
            wx[i].Style, wx[i].PosX, wx[i].PosY, wx[i].Width, wx[i].Height,
            hwnd[wx[i].Parent], hmenu[wx[i].Menu], hInstance, wx[i].Param);
        if (!hwnd[i]) {
            return Logger.Error("View.StartUp", "Error creating window");
        }
    }

    MenuBar.SetMenu(WinMdiFrame, wx[WinMdiFrame].Menu);
    StatusBar.StartUp();
    TreeView.StartUp();
    ListView.StartUp();

    for (i = 0; i < elementsof(wx); i++) {
        if (wx[i].Font) Font.SetFont(hwnd[i], wx[i].Font);
    }

    for (i = 0; i < elementsof(wx); i++) {
        if (wx[i].ToolTip) ToolTip.SetToolTip(i, wx[i].ToolTip);
    }

    for (i = 0; i < elementsof(wx); i++) {
        if (i == WinToolTip) continue;
        if (hwnd[i]) FlickerFree(hwnd[i]);
    }

    ShowWindow(hwnd[WinMdiFrame], SW_SHOW);
    UpdateWindow(hwnd[WinMdiFrame]);
    hwnd[WinConsole] = GetConsoleWindow();
    ShowWindow(hwnd[WinConsole], SW_HIDE);

    StatusBar.SetStatus("No Disk", "Idle");
    StatusBar.SetProgress(0);

    return Logger.Done("View.StartUp", "Done");
}

static int View_CleanUp(void) {
    int i;
    hInstance = GetModuleHandle(0);
    for (i = elementsof(cx)-1; i >= 0; i--) {
        if (atom[i]) {
            UnregisterClassA(cx[i].ClassName, hInstance);
        }
    }
    MenuBar.CleanUp();
    OleUninitialize();
    ShowWindow(hwnd[WinConsole], SW_SHOW);
    SetForegroundWindow(hwnd[WinConsole]);
    return Logger.Done("View.CleanUp", "Done");
}

static int View_Execute(void) {
    static MSG msg;
    for (; msg.message != WM_QUIT; GetMessageA(&msg, 0, 0, 0)) {
        if (!TranslateAcceleratorA(hwnd[WinMdiFrame], hAccel, &msg)) {
            TranslateMessage(&msg);
            DispatchMessageA(&msg);
        }
    }
    return Logger.Done("View.Execute", "Done");
}


struct VIEW View = {
    View_StartUp,
    View_CleanUp,
    View_Execute,
};
