#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern LRESULT CALLBACK MdiFrameProc(HWND,UINT,WPARAM,LPARAM);
extern LRESULT CALLBACK MdiChildProc(HWND,UINT,WPARAM,LPARAM);


extern struct LOGGER    Logger;
extern struct FONT      Font;
extern struct ICON      Icon;
extern struct MENUBAR   MenuBar;
extern struct TOOLBAR   ToolBar;
extern struct STATUSBAR StatusBar;
extern struct TABBAR    TabBar;
extern struct TOOLTIP   ToolTip;
extern struct MDICLIENT MdiClient;
extern struct TREEVIEW  TreeView;
extern struct LISTVIEW  ListView;
extern struct SPLITTER  Splitter;

extern HMENU hmenu[64];
extern HACCEL hAccel;

static struct WINCLASS cx[] = {
    { "MdiFrame",MdiFrameProc,IDI_APPLICATION,IDC_ARROW,  0x7F7F7F },
    { "MdiChild",MdiChildProc,IDI_APPLICATION,IDC_ARROW,  -(COLOR_BTNFACE+1) },
    { "Splitter",SplitterProc,IDI_APPLICATION,IDC_SIZEWE, -(COLOR_3DFACE+1) },
    { "OpenGL",  OpenGLProc,  IDI_APPLICATION,IDC_ARROW,  0 },
};

static struct WINDOW wx[16] = {
    { 0x00000000 },
    { 0x02000000,"MdiFrame", "GodHands",0x06CF0000,  0, 0, 800,600,0,           0,0x01, "MS Sans Serif", "GodHands" },
    { 0x00000000,0,                   0,0x56000000,134,24, 666,552,WinMdiFrame, 0,0x00, "MS Sans Serif", "MdiClient" },
    { 0x00000000 },
    { 0x00000000,"ToolbarWindow32",   0,0x56000101,  0, 0, 800, 32,WinMdiFrame, 0,0x01, "MS Sans Serif", 0 },
    { 0x00000000,"tooltips_class32",  0,0x00000001,  0, 0,   0,  0,0,           0,0x00, "MS Sans Serif", "ToolTip" },
    { 0x00000000,"msctls_statusbar32",0,0x56000100,  0, 0,   0, 24,WinMdiFrame, 0,0x01, "MS Sans Serif", "StatusBar" },
    { 0x00000000,"msctls_progress32", 0,0x56000000,  4, 4, 128, -6,WinStatusBar,0,0x01, "MS Sans Serif", "ProgressBar" },
    { 0x00000000,"SysTabControl32",   0,0x56000000,198, 0, 666, 24,WinMdiFrame, 0,0x01, "MS Sans Serif", "TabBar" },
    { 0x00000000,"SysTreeView32",     0,0x5600000F,  0, 0, 192,552,WinMdiFrame, 0,0x01, "MS Sans Serif", "TreeView" },
    { 0x00000000,"SysListView32",     0,0x56000249,  0, 0, 192,552,WinMdiFrame, 0,0x01, "MS Sans Serif", "ListView" },
    { 0x00000000,0,                   0,0x00000000,  0, 0,   0,  0,0,           0,0x01, "MS Sans Serif", "ListViewHeader" },
    { 0x00000000,"Splitter",          0,0x56000000,192, 0,   6,552,WinMdiFrame, 0,0x01, "MS Sans Serif", "Splitter" },
    { 0x00000000 },
};

static PIXELFORMATDESCRIPTOR pfd = {
    sizeof(pfd),0x01,0x15,0,0x20,0,0,0,0,0,0,0,0,0,0,0,0,0,0x20,0,0,0,0,0,0,0
};
ATOM atom[elementsof(cx)];
HWND hwnd[64];
HINSTANCE hInstance;


static HWND View_Window(HWND hParent, WINDOW *wx) {
    HWND hChild;
    HDC hDC;

    if (!wx->Class) return 0;
    hChild = CreateWindowExA(wx->ExStyle, wx->Class, wx->Window,
        wx->Style, wx->PosX, wx->PosY, wx->Width, wx->Height,
        hParent, 0, hInstance, wx->Param);
    if (!hChild) {
        Logger.Error("View.StartUp", "Error creating window");
        return 0;
    }
    hDC = GetDC(hChild);
    if (!hDC) {
        Logger.Error("View.StartUp", "Error no device context");
        return 0;
    }
    SetPixelFormat(hDC, ChoosePixelFormat(hDC, &pfd), &pfd);
    if (wx->Font) {
        Font.SetFont(hChild, wx->Font);
    }
    if (wx->ToolTip) {
        ToolTip.SetToolTip(hChild, wx->ToolTip);
    }
    if (wx->DoubleBuffer) {
        FlickerFree(hChild);
    }
    return hChild;
}

static int View_StartUp(void) {
    int i;

    InitCommonControls();
    OleInitialize(0);

    hInstance = GetModuleHandle(0);
    if (!Font.StartUp()) return 0;
    if (!Icon.StartUp()) return 0;
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
        hwnd[i] = View_Window(hwnd[wx[i].Parent], &wx[i]);
        if (!hwnd[i]) return 0;
    }
    SetMenu(hwnd[WinMdiFrame], hmenu[0x01]);
    StatusBar.StartUp();
    MdiClient.StartUp();
    ToolBar.StartUp();
    TabBar.StartUp();
    TreeView.StartUp();
    ListView.StartUp();
    Splitter.StartUp();

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
    Icon.CleanUp();
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

static int View_Reset(void) {
    return Logger.Done("View.Reset", "Done");
}


struct VIEW View = {
    View_StartUp,
    View_CleanUp,
    View_Execute,
    View_Reset,
    View_Window,
};
