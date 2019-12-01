#ifndef MODELSYD_H
#define MODELSYD_H


typedef struct MODELSYD {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddSyd)(REC *rec);
} MODELSYD;


#endif // MODELSYD_H
