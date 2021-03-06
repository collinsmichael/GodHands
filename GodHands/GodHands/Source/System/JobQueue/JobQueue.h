#ifndef JOBQUEUE_H
#define JOBQUEUE_H

typedef struct JOBQUEUE {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Schedule)(int(*callback)(void*), void *args);
    int (*KillAll)(void);
} JOBQUEUE;

#endif // JOBQUEUE_H
