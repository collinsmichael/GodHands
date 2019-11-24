#ifndef FONT_H
#define FONT_H
#include <windows.h>


typedef struct FONT {
    int (*StartUp)(void);
    int (*SetFont)(HWND hWnd, char *font);
} FONT;


#endif // FONT_H
