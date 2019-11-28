#ifndef TREEVIEW_H
#define TREEVIEW_H


typedef struct TREEVIEW {
    int (*StartUp)(void);
    int (*AddItem)(int parent, char *path, DWORD Attribute, void *param);
    int (*AddDir)(int parent, ISO9660_DIR *rec);
    int (*AddFile)(int parent, ISO9660_DIR *rec);
} TREEVIEW;


#endif // TREEVIEW_H
