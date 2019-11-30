#ifndef MODELSEQ_H
#define MODELSEQ_H


typedef struct MODELSEQ {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddSeq)(REC *rec);
} MODELSEQ;


#endif // MODELSEQ_H
