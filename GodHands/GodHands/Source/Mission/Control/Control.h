#ifndef CONTROL_H
#define CONTROL_H


typedef struct CONTROL {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Execute)(void);
    int (*OpenRecord)(void *evt);
} CONTROL;

int MenuFile_Open(void *evt);
int MenuFile_Save(void *evt);
int MenuFile_Close(void *evt);
int MenuFile_Exit(void *evt);
int Control_OpenRecord(void *evt);


#endif // CONTROL_H
