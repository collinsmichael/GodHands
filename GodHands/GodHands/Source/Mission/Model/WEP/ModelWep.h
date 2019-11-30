#ifndef MODELWEP_H
#define MODELWEP_H


typedef struct MODELWEP {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddWep)(REC *rec);
} MODELWEP;


#endif // MODELWEP_H
