#ifndef MODELPRG_H
#define MODELPRG_H


typedef struct MODELPRG {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddSlus)(REC *rec);
    int (*AddPrg)(REC *rec);
    REC *(*GetPrg)(char *name);
} MODELPRG;


#endif // MODELPRG_H
