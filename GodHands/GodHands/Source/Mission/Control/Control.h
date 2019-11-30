#ifndef CONTROL_H
#define CONTROL_H


typedef struct CONTROL {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Execute)(void);
} CONTROL;

int MenuFile_Open(void *evt);
int MenuFile_Save(void *evt);
int MenuFile_Close(void *evt);
int MenuFile_Exit(void *evt);


#endif // CONTROL_H
