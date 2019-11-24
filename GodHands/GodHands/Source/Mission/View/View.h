#ifndef VIEW_H
#define VIEW_H
#include <Windows.h>


typedef struct VIEW {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Execute)(void);
} VIEW;

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
    int   Menu;
    char *Font;
    char *ToolTip;
} WINDOW;

#define WinConsole        0x00
#define WinMdiFrame       0x01
#define WinMdiClient      0x02
#define WinMenuBar        0x03
#define WinToolTip        0x04
#define WinStatusBar      0x05
#define WinProgressBar    0x06
#define WinTreeView       0x07
#define WinListView       0x08
#define WinListViewHeader 0x09
#define WinMdiChild       0x0A

#endif // VIEW_H
