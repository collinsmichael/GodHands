#ifndef TABBAR_H
#define TABBAR_H


typedef struct TABBAR {
    int (*StartUp)(void);
    int (*Insert)(char *text, void *param);
    int (*Remove)(void *param);
    int (*SwitchTo)(void *param);
} TABBAR;


#endif // TABBAR_H
