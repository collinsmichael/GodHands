#include <windows.h>
#include "GodHands.h"


extern LOGGER Logger;
extern ISO9660 Iso9660;
extern TREEVIEW TreeView;
extern LISTVIEW ListView;
extern STATUSBAR StatusBar;

int MenuFile_Exit(void *evt) {
    if (!MenuFile_Close(0)) return 0;
    PostQuitMessage(0);
    return Logger.Done("File.Exit", "Done");
}
