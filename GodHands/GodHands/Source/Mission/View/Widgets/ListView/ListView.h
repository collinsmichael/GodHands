#ifndef LISTVIEW_H
#define LISTVIEW_H


typedef struct LISTVIEW {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*DeleteAll)(void);
    int (*AddItem)(char *text, char *lba, char *size, DWORD Attribute, void *param);
    int (*AddDir)(REC *rec);
    int (*AddFile)(REC *rec);
    int (*Mount)(void *param, REC *rec);
    int (*NavEnter)(REC *rec);
    int (*NavBack)(void);
    int (*NavForward)(void);
} LISTVIEW;


#endif // LISTVIEW_H
