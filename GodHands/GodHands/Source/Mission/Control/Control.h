#ifndef CONTROL_H
#define CONTROL_H


typedef struct CONTROL {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Execute)(void);
} CONTROL;


#endif // CONTROL_H
