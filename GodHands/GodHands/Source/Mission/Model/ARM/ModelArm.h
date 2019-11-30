#ifndef MODELARM_H
#define MODELARM_H
#include <stdint.h>


#pragma pack(push, 1)
typedef struct ARMROOM {
    uint32_t ram_only;
    uint32_t len_gfx;
    uint16_t znd_no;
    uint16_t mpd_no;
} ARMROOM;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMHDR {
    uint32_t num_rooms;
    ARMROOM  room[1];
} ARMHDR;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMVERTEX3 {
    uint16_t x;
    uint16_t y;
    uint16_t z;
    uint16_t padding;
} ARMVERTEX3;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMPOLY3 {
    uint8_t p1;
    uint8_t p2;
    uint8_t p3;
    uint8_t padding;
} ARMPOLY3;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMPOLY4 {
    uint8_t p1;
    uint8_t p2;
    uint8_t p3;
    uint8_t p4;
} ARMPOLY4;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMPOLY2 {
    uint8_t  p1;
    uint8_t  p2;
    uint16_t padding;
} ARMPOLY2;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMICON {
    uint8_t vertex;
    uint8_t zone_exit;
    uint8_t info;
    uint8_t lock;
} ARMICON;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMNAME {
    char     text[0x20];
    uint16_t prev;
    uint16_t next;
} ARMNAME;
#pragma pack(pop)


typedef struct MODELARM {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddArm)(REC *rec);
} MODELARM;


#endif // MODELARM_H
