#ifndef STATUSBAR_H
#define STATUSBAR_H


typedef struct STATUSBAR {
    int (*StartUp)(void);
    int (*SetStatus)(char *status, char *format, ...);
    int (*SetProgress)(int percent);
} STATUSBAR;


#endif // STATUSBAR_H
