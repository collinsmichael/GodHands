#ifndef TOOLTIP_H
#define TOOLTIP_H


typedef struct TOOLTIP {
    int (*SetToolTip)(int win, char *text);
} TOOLTIP;


#endif // TOOLTIP_H
