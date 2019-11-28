#ifndef LISTVIEW_H
#define LISTVIEW_H


typedef struct LISTVIEW {
    int (*StartUp)(void);
    int (*DeleteAllItems)(void);
    int (*ResetColumns)(void);
    int (*AddItem)(char *text, char *lba, char *size, char *type, DWORD Attribute, void *param);
    int (*AddDir)(ISO9660_DIR *rec);
    int (*AddFile)(ISO9660_DIR *rec);
} LISTVIEW;


#endif // LISTVIEW_H
