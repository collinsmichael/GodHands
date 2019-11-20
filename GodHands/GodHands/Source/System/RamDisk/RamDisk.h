#ifndef RAMDISK_H
#define RAMDISK_H

#define KB (1024)
#define MB (1024*KB)
#define GB (1024*MB)

typedef struct RAMDISK {
    int (*Reset)(void);
    int (*Open)(char *path);
    int (*Close)(void);
    int (*Read)(int lba, int len, char *buf);
    int (*Write)(int lba, int len, char *buf);
} RAMDISK;

#endif // RAMDISK_H
