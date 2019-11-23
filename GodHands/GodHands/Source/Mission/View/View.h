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
} WINDOW;


#endif // VIEW_H
