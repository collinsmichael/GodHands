#ifndef MODELZUD_H
#define MODELZUD_H


typedef struct MODELZUD {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddZud)(REC *rec);
} MODELZUD;


#endif // MODELZUD_H
