#include <windows.h>
#include "GodHands.h"


extern LOGGER Logger;
extern ISO9660 Iso9660;
extern RAMDISK RamDisk;
extern MDICHILD MdiChild;
extern STATUSBAR StatusBar;

int Control_OpenRecord(void *evt) {
    REC *rec = (REC*)evt;
    int lsb = rec->LsbLbaData;
    int len = rec->LsbLenData/(2*KB);
    if (!RamDisk.Read(lsb, len)) return 0;
    if (!MdiChild.Create(rec)) return 0;

    StatusBar.SetStatus("Control.OpenRecord", "Done");
    StatusBar.SetProgress(0);
    return Logger.Done("Control.OpenRecord", "Done");
}
