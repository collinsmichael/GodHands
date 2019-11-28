#ifndef SYSTEM_H
#define SYSTEM_H

#include "System/Iso9660/Iso9660.h"
#include "System/Logger/Logger.h"
#include "System/Memory/Memory.h"
#include "System/RamDisk/RamDisk.h"
#include "System/JobQueue/JobQueue.h"


typedef struct SYSTEM {
    int (*StartUp)(int argc, char *argv[]);
    int (*CleanUp)(void);
    int (*Execute)(void);
} SYSTEM;


#endif // SYSTEM_H
