/*****************************************************************************/
/* win32 list view example                                                   */
/*****************************************************************************/
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern HWND hwnd[64];

static TV_INSERTSTRUCT tvi;
static HTREEITEM hRoot;
static HIMAGELIST hSmallIcons;
static HIMAGELIST hLargeIcons;
static char *ext[] = {
    "", ".exe", ".txt", ".bmp", ".png", ".pdf", ".wav", ".mp3"
}; 
static char path[256];

int TreeView_StartUp(void) {
    int i;
    int j;
    //hSmallIcons = ShellGetSmallIcons();
    //TreeView_SetImageList(hwnd[WinTreeView], hSmallIcons, TVSIL_NORMAL);
    for (i = 0; i < 4; i++) {
        int iIcon;
        wsprintfA(path, "folder %d", i);
        iIcon = 0;//ShellGetIconIndexGivenAttributes(path, FILE_ATTRIBUTE_DIRECTORY);
        tvi.hParent             = TVI_ROOT;
        tvi.hInsertAfter        = TVI_LAST;
        tvi.item.mask           = TVIF_TEXT | TVIF_HANDLE | TVIF_IMAGE | TVIF_PARAM | TVIF_SELECTEDIMAGE; // | TVIF_CHILDREN | TVIF_STATE;
        //tvi.item.hItem          = 0;
        //tvi.item.state          = 0;
        //tvi.item.stateMask      = 0;
        tvi.item.pszText        = path;
        tvi.item.cchTextMax     = lstrlenA(path);
        tvi.item.iImage         = iIcon;
        tvi.item.iSelectedImage = iIcon+1;
        //tvi.item.cChildren      = 0;
        //tvi.item.lParam         = 0;
        tvi.hParent             = TreeView_InsertItem(hwnd[WinTreeView], &tvi);
        for (j = 1; j < elementsof(ext); j++) {
            wsprintfA(path, "document%s", ext[j]);
            iIcon = 0;//ShellGetIconIndexGivenAttributes(path, FILE_ATTRIBUTE_NORMAL);
            tvi.item.iImage         = iIcon;
            tvi.item.iSelectedImage = iIcon;
            tvi.item.pszText        = path;
            tvi.item.cchTextMax     = lstrlenA(path);
            TreeView_InsertItem(hwnd[WinTreeView], &tvi);
        }
    }
    return 1;
}


struct TREEVIEW TreeView = {
    TreeView_StartUp,
};
