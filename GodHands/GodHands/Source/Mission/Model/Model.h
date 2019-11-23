#ifndef MODEL_H
#define MODEL_H


typedef struct MODEL {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Execute)(void);
} MODEL;


#endif // MODEL_H
