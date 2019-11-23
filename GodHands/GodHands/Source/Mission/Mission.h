#ifndef MISSION_H
#define MISSION_H

#include "Mission/Control/Control.h"
#include "Mission/Model/Model.h"
#include "Mission/View/View.h"
#include "Mission/View/MenuBar/MenuBar.h"
#include "Mission/View/ToolTip/ToolTip.h"


typedef struct MISSION {
    int (*StartUp)(int argc, char *argv[]);
    int (*CleanUp)(void);
    int (*Execute)(void);
} MISSION;


#endif // MISSION_H
