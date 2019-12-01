#ifndef VIEW_H
#define VIEW_H
#include <Windows.h>


int FlickerFree(HWND hwnd);


typedef struct WINCLASS {
    char    *ClassName;
    WNDPROC  WndProc;
    char    *hIcon;
    char    *hCursor;
    int      hBackground;
} WINCLASS;

typedef struct WINDOW {
    DWORD ExStyle;
    char *Class;
    char *Window;
    DWORD Style;
    int   PosX;
    int   PosY;
    int   Width;
    int   Height;
    int   Parent;
    void *Param;
    int   DoubleBuffer;
    char *Font;
    char *ToolTip;
} WINDOW;

#define WinConsole        0x00
#define WinMdiFrame       0x01
#define WinMdiClient      0x02
#define WinMenuBar        0x03
#define WinToolBar        0x04
#define WinToolTip        0x05
#define WinStatusBar      0x06
#define WinProgressBar    0x07
#define WinTabBar         0x08
#define WinTreeView       0x09
#define WinListView       0x0A
#define WinListViewHeader 0x0B
#define WinSplitter       0x0C
#define WinMdiChild       0x0D

typedef struct VIEW {
    int  (*StartUp)(void);
    int  (*CleanUp)(void);
    int  (*Execute)(void);
    int  (*Reset)(void);
    HWND (*Window)(HWND hParent, WINDOW *wx);
} VIEW;

typedef struct MDICHILD {
    int (*Create)(REC *rec);
} MDICHILD;


#endif // VIEW_H
