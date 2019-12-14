// ZND Files
// LBA TABLE is in SLUS-010.40@0x0003FCCC 256x8 (4:LBA,4:LEN)
// ZoneID is an index into the above array
//
// Contains
//  o List of LBAs to MPD which form this zone
//  o List of ZUDs to Enemies which populate this zone
//  o Stats for each enemy and their equipment
//  o Texture maps

#ifndef MODELZND_H
#define MODELZND_H

#pragma pack(push, 1)
typedef struct ZNDHDR {
    uint32_t PtrMpd;
    uint32_t LenMpd;
    uint32_t PtrZud;
    uint32_t LenZud;
    uint8_t  WaveId;
    uint8_t  Unused[7];
} ZNDHDR;
#pragma pack(pop)


typedef struct MODELZND {
    int (*StartUp)(void);
    int (*Reset)(void);
    int (*AddZnd)(REC *rec);
} MODELZND;


#endif // MODELZND_H
