#ifndef MODELMPD_H
#define MODELMPD_H


typedef struct MODELMPD {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddMpd)(REC *rec);
} MODELMPD;


#endif // MODELMPD_H
