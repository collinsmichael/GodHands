#ifndef RAMDISK_H
#define RAMDISK_H

typedef struct RAMDISK {
    int   (*Reset)(void);
    int   (*Open)(char *path);
    int   (*Close)(void);
    int   (*Read)(int lba, int len);
    int   (*Write)(int lba, int len);
    int   (*Scan)(int lba);
    int   (*Clear)(int lba, int len);
    char *(*AddressOf)(int lba);
} RAMDISK;

#endif // RAMDISK_H
