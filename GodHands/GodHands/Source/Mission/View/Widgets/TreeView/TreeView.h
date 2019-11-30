#ifndef TREEVIEW_H
#define TREEVIEW_H


typedef struct TREEVIEW {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*Expand)(void *hitem);
    int (*Mount)(void);
    int (*AddItem)(void *parent, char *path, DWORD Attribute, void *param);
    int (*AddDir)(void *parent, ISO9660_DIR *rec);
    int (*AddFile)(void *parent, ISO9660_DIR *rec);
} TREEVIEW;


#endif // TREEVIEW_H
