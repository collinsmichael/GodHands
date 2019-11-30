#ifndef MODELARM_H
#define MODELARM_H


typedef struct MODELARM {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddArm)(REC *rec);
} MODELARM;


#endif // MODELARM_H
