#ifndef JOBQUEUE_H
#define JOBQUEUE_H

#define MS   (1)
#define SEC  (1000*MS)
#define MIN  (60*SEC)
#define HOUR (60*MIN)
#define DAY  (24*HOUR)
#define WEEK (7*DAY)

typedef struct JOBQUEUE {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Schedule)(int(*callback)(void*), void *args);
} JOBQUEUE;

#endif // JOBQUEUE_H
