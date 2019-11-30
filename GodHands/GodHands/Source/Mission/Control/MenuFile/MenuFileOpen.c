#include <windows.h>
#include "GodHands.h"


extern MODEL Model;
extern LOGGER Logger;
extern JOBQUEUE JobQueue;
extern ISO9660 Iso9660;
extern TREEVIEW TreeView;
extern LISTVIEW ListView;
extern DIALOG Dialog;
extern STATUSBAR StatusBar;

int MenuFile_Open(void *evt) {
    char *path = Dialog.OpenFileDialog(
        "CD Images (img bin iso)\0*.img;*.bin;*.iso\0"
        "All Files\0*.*\0\0");
    if (!path) return 0;

    if (!Iso9660.Open(path)) return 0;
    ListView.Reset();
    ListView.NavEnter(0);
    TreeView.Mount();
    TreeView.Expand(0);
    JobQueue.Schedule(Model.OpenDisk, 0);
    StatusBar.SetStatus("File.Open", "Done");
    StatusBar.SetProgress(0);
    return Logger.Done("File.Open", "Done");
}
