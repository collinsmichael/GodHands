#include <windows.h>
#include "GodHands.h"


extern LOGGER Logger;
extern ISO9660 Iso9660;
extern TREEVIEW TreeView;
extern LISTVIEW ListView;
extern DIALOG Dialog;
extern STATUSBAR StatusBar;

int MenuFile_Save(void *evt) {
    char *path = Dialog.SaveFileDialog(
        "CD Images (img bin iso)\0*.img;*.bin;*.iso\0"
        "All Files\0*.*\0\0");
    if (!path) return 0;

    //if (!Iso9660.Save(path)) return 0;
    //Model.OpenDisk();
    StatusBar.SetStatus("File.Save", "TODO");
    StatusBar.SetProgress(0);
    return Logger.Done("File.Save", "TODO");
}
