#ifndef MODELBIN_H
#define MODELBIN_H


typedef struct MODELBIN {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddBin)(REC *rec);
} MODELBIN;


#endif // MODELBIN_H
