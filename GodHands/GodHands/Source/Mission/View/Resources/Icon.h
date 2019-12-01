#ifndef ICON_H
#define ICON_H
#include <windows.h>
#include <commctrl.h>


typedef struct ICON {
    int        (*StartUp)(void);
    int        (*CleanUp)(void);
    HIMAGELIST (*GetSmallIcons)(void);
    HIMAGELIST (*GetLargeIcons)(void);
    HICON      (*GetSmallIcon)(char *path, DWORD Attributes);
    HICON      (*GetLargeIcon)(char *path, DWORD Attributes);
    int        (*GetIndexFromAttributes)(char *path, DWORD Attributes);
    int        (*GetIndex)(char *path);
    int        (*LargeX)(void);
    int        (*LargeY)(void);
    int        (*SmallX)(void);
    int        (*SmallY)(void);
} ICON;


#endif // ICON_H
