#ifndef MENUBAR_H
#define MENUBAR_H
#include <Windows.h>


typedef struct MENUBAR {
    int (*StartUp)(void);
    int (*CleanUp)(void);
} MENUBAR;

typedef struct MENUX {
    int   Type;
    int   Parent;
    int   Code;
    char *Name;
} MENUX;

#define MENUBAR_MENU  1
#define MENUBAR_POPUP 2
#define MENUBAR_ITEM  3


#endif // MENUBAR_H
