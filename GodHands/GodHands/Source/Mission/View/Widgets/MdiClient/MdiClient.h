#ifndef MDICLIENT_H
#define MDICLIENT_H


typedef struct MDICLIENT {
    int (*Create)(void);
    int (*StartUp)(void);
} MDICLIENT;


#endif // MDICLIENT_H
