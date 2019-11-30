#ifndef MODELSHP_H
#define MODELSHP_H


typedef struct MODELSHP {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddShp)(REC *rec);
} MODELSHP;


#endif // MODELSHP_H
