#ifndef TOOLTIP_H
#define TOOLTIP_H
#include <windows.h>


typedef struct TOOLTIP {
    int (*SetToolTip)(HWND hWnd, char *text);
} TOOLTIP;


#endif // TOOLTIP_H
