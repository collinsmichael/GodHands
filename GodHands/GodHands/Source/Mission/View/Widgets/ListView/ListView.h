#ifndef LISTVIEW_H
#define LISTVIEW_H


typedef struct LISTVIEW {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*DeleteAll)(void);
    int (*AddItem)(char *text, char *lba, char *size, DWORD Attribute, void *param);
    int (*AddDir)(ISO9660_DIR *rec);
    int (*AddFile)(ISO9660_DIR *rec);
    int (*Mount)(void *param, ISO9660_DIR *rec);
    int (*NavEnter)(ISO9660_DIR *rec);
    int (*NavBack)(void);
    int (*NavForward)(void);
} LISTVIEW;


#endif // LISTVIEW_H
