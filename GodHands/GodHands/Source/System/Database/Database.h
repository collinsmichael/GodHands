#ifndef DATABASE_H
#define DATABASE_H


typedef struct DATABASE {
    int  (*Open)(char *filename);
    int  (*Close)(void);
    void (*Free)(void);
    int  (*Exec)(const char *sql, int (*callback)(void*,int,char**,char**), void *param, char **errmsg);
} DATABASE;


#endif // DATABASE_H
