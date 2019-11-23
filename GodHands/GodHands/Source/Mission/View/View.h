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
    COLORREF hBackground;
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
    int   Menu;
    void *Param;
    char *ToolTip;
} WINDOW;

#define WinConsole     0x00
#define WinMdiFrame    0x01
#define WinToolTip     0x02
#define WinMenuBar     0x03
#define WinStatusBar   0x04
#define WinProgressBar 0x05


#endif // VIEW_H
