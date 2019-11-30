#include <windows.h>
#include "GodHands.h"


extern LOGGER Logger;
extern ISO9660 Iso9660;
extern TREEVIEW TreeView;
extern LISTVIEW ListView;
extern STATUSBAR StatusBar;

int MenuFile_Close(void *evt) {
    if (!Iso9660.Close()) return 0;
    ListView.Reset();
    TreeView.Reset();
    //Model.Close();
    StatusBar.SetStatus("File.Close", "Done");
    StatusBar.SetProgress(0);
    return Logger.Done("File.Close", "Done");
}
