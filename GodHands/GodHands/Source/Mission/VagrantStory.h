#ifndef VAGRANTSTORY_H
#define VAGRANTSTORY_H
#include <stdint.h>


#pragma pack(push, 1)
typedef struct RANGE {
    uint8_t X;
    uint8_t Y;
    uint8_t Z;
    uint8_t TargetShape;
} RANGE;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct EQUIP {
    uint16_t ItemNamesList;
    uint8_t  ItemsList;
    uint8_t  WepFiles;
    uint8_t  ItemCategory;
    uint8_t  STR;
    uint8_t  INT;
    uint8_t  AGL;
    uint16_t CurDP;
    uint16_t MaxDP;
    uint16_t CurPP;
    uint16_t MaxPP;
    uint8_t  DamageType;
    uint8_t  StatsCost;
    uint8_t  CostValue;
    uint8_t  Material;
    uint8_t  unknown;
    uint8_t  NumGemSlots;
    uint8_t  GemSpecialEffects;
    uint8_t EquipID_RamOnly;
    RANGE   Range;
    uint8_t Types[4];
    uint8_t Classes[8];
    uint8_t Affinities[8];
} EQUIP;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct WEAPON {
    EQUIP    Blade;
    EQUIP    Grip;
    EQUIP    Gems[3];
    uint8_t  Material;
    uint8_t  DropChance;
    uint8_t  unknown_01;
    uint8_t  unknown_02;
    char     Name[0x18];
} WEAPON;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct SHIELD {
    EQUIP    Shield;
    EQUIP    Gems[3];
    uint8_t  Material;
    uint8_t  DropChance;
    uint8_t  unknown_01;
    uint8_t  unknown_02;
} SHIELD;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ACCESSORY {
    EQUIP    Accessory;
    uint8_t  DropChance;
    uint8_t  unknown_01;
    uint8_t  unknown_02;
    uint8_t  unknown_03;
} ACCESSORY;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ARMOUR {
    EQUIP    Armour;
    uint8_t  Material;
    uint8_t  DropChance;
    uint8_t  Always1;
    uint8_t  unknown_01;
} ARMOUR;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct SKILL {
    uint8_t  SkillList;
    uint8_t  unknown_01;
    uint8_t  unknown_02;
    uint8_t  LocalSkillNo;
} SKILL;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct BODYPART {
    uint16_t HP;
    uint8_t  AGL;
    uint8_t  Evade;
    uint8_t  Types[4];
    uint8_t  Affinities[8];
    SKILL    Skill[4];
    EQUIP    Armour;
    uint8_t  DmgDistribute[6];
    uint8_t  unknown_01;
    uint8_t  unknown_02;
} BODYPART;
#pragma pack(pop)

#pragma pack(push, 1)
typedef struct ZNDENEMY {
    uint8_t   unknown_00;
    uint8_t   unknown_01;
    uint8_t   ModelFX;
    uint8_t   unknown_02;

    char      Name[0x18];
    uint16_t  HP;
    uint16_t  MP;
    uint8_t   STR;
    uint8_t   INT;
    uint8_t   AGL;
    uint8_t   unknown_03;
    uint8_t   unknown_04;
    uint8_t   SpeedWhenBurdened;
    uint8_t   unknown_05;
    uint8_t   RunningSpeed;
    uint8_t   unknown_06;
    uint8_t   unknown_07;
    uint8_t   unknown_08;
    uint8_t   unknown_09;
    uint8_t   unknown_0A;
    uint8_t   unknown_0B;
    uint8_t   unknown_0C;
    uint8_t   unknown_0D;
    uint8_t   unknown_0E;
    uint8_t   unknown_0F;
    uint8_t   unknown_10;
    uint8_t   unknown_11;

    WEAPON    Weapon;
    SHIELD    Shield;
    ACCESSORY Accessory;
    BODYPART  BodyPart[6];
    uint32_t  MpdId;
} ZNDENEMY;
#pragma pack(pop)


#endif // VAGRANTSTORY_H
