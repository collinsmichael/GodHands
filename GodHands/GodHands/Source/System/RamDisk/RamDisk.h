#ifndef RAMDISK_H
#define RAMDISK_H

typedef struct RAMDISK {
    int (*Reset)(void);
    int (*Open)(char *path);
    int (*Close)(void);
    int (*Read)(int lba, int len, char *buf);
    int (*Write)(int lba, int len, char *buf);
} RAMDISK;

#endif // RAMDISK_H
