#include <windows.h>
#include "GodHands.h"


static OPENFILENAMEA ofn;
static char path[1024];


static char *Dialog_OpenFileDialog(char *filter) {
    ofn.lStructSize = sizeof(ofn);
    ofn.Flags       = OFN_EXPLORER | OFN_ALLOWMULTISELECT |
                      OFN_OVERWRITEPROMPT | OFN_CREATEPROMPT |
                      OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
    ofn.lpstrFile   = path;
    ofn.nMaxFile    = sizeof(path);
    ofn.lpstrTitle  = "Open File";
    ofn.lpstrFilter = filter;
    if (GetOpenFileNameA(&ofn)) {
        return path;
    }
    return 0;
}

static char *Dialog_SaveFileDialog(char *filter) {
    ofn.lStructSize = sizeof(ofn);
    ofn.Flags       = OFN_EXPLORER | OFN_ALLOWMULTISELECT |
                      OFN_OVERWRITEPROMPT | OFN_CREATEPROMPT |
                      OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
    ofn.lpstrFile   = path;
    ofn.nMaxFile    = sizeof(path);
    ofn.lpstrTitle  = "Save File";
    ofn.lpstrFilter = filter;
    if (GetSaveFileNameA(&ofn)) {
        return path;
    }
    return 0;
}


struct DIALOG Dialog = {
    Dialog_OpenFileDialog,
    Dialog_SaveFileDialog
};
