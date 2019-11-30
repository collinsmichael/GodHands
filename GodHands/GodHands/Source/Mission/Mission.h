#ifndef MISSION_H
#define MISSION_H

#include "Mission/Control/Control.h"
#include "Mission/Model/Model.h"
#include "Mission/Model/ARM/ModelArm.h"
#include "Mission/Model/BIN/ModelBin.h"
#include "Mission/Model/MPD/ModelMpd.h"
#include "Mission/Model/PRG/ModelPrg.h"
#include "Mission/Model/SEQ/ModelSeq.h"
#include "Mission/Model/SHP/ModelShp.h"
#include "Mission/Model/SYD/ModelSyd.h"
#include "Mission/Model/WEP/ModelWep.h"
#include "Mission/Model/VSTEXT/VsText.h"
#include "Mission/Model/ZND/ModelZnd.h"
#include "Mission/Model/ZUD/ModelZud.h"
#include "Mission/View/View.h"
#include "Mission/View/Dialogs/Dialogs.h"
#include "Mission/View/Resources/Font.h"
#include "Mission/View/Resources/Icon.h"
#include "Mission/View/Widgets/ListView/ListView.h"
#include "Mission/View/Widgets/MdiClient/MdiClient.h"
#include "Mission/View/Widgets/MenuBar/MenuBar.h"
#include "Mission/View/Widgets/Splitter/Splitter.h"
#include "Mission/View/Widgets/StatusBar/StatusBar.h"
#include "Mission/View/Widgets/TabBar/TabBar.h"
#include "Mission/View/Widgets/ToolBar/ToolBar.h"
#include "Mission/View/Widgets/ToolTip/ToolTip.h"
#include "Mission/View/Widgets/TreeView/TreeView.h"


typedef struct MISSION {
    int (*StartUp)(int argc, char *argv[]);
    int (*CleanUp)(void);
    int (*Execute)(void);
    int (*Reset)(void);
} MISSION;


#endif // MISSION_H
