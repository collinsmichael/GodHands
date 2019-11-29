/*****************************************************************************/
/* win32 list view example                                                   */
/*****************************************************************************/
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern ICON Icon;
extern HWND hwnd[64];

static TV_INSERTSTRUCT tvi;
static HTREEITEM hRoot;
static HIMAGELIST hSmallIcons;
static HIMAGELIST hLargeIcons;
static char path[256];


int TreeView_Reset(void) {
    return (int)TreeView_DeleteAllItems(hwnd[WinTreeView]);
}

int TreeView_DeleteAll(void) {
    return (int)TreeView_DeleteAllItems(hwnd[WinTreeView]);
}

int TreeView_AddItem(int parent, char *path, DWORD Attribute, void *param) {
    int iIcon = Icon.GetIndexFromAttributes(path, Attribute);
    tvi.hParent             = (parent) ? (HTREEITEM)parent : TVI_ROOT;
    tvi.hInsertAfter        = TVI_LAST;
    tvi.item.mask           = TVIF_TEXT|TVIF_HANDLE|TVIF_PARAM|TVIF_IMAGE|TVIF_SELECTEDIMAGE;
    tvi.item.pszText        = path;
    tvi.item.cchTextMax     = lstrlenA(path);
    tvi.item.iImage         = iIcon;
    tvi.item.iSelectedImage = (FILE_ATTRIBUTE_DIRECTORY) ? iIcon+1 : iIcon;
    return (int)TreeView_InsertItem(hwnd[WinTreeView], &tvi);
}

int TreeView_AddDir(int parent, ISO9660_DIR *rec) {
    int i;
    for (i = 0; i < rec->LenFileName; i++) {
        if (rec->FileName[i] == ';') {
            break;
        } else {
            path[i] = rec->FileName[i];
        }
    }
    path[i] = 0;
    path[rec->LenFileName] = 0;
    return TreeView_AddItem(parent, path, FILE_ATTRIBUTE_DIRECTORY, rec);
}

int TreeView_AddFile(int parent, ISO9660_DIR *rec) {
    int i;
    for (i = 0; i < rec->LenFileName; i++) {
        if (rec->FileName[i] == ';') {
            break;
        } else {
            path[i] = rec->FileName[i];
        }
    }
    path[i] = 0;
    path[rec->LenFileName] = 0;
    return TreeView_AddItem(parent, path, FILE_ATTRIBUTE_NORMAL, rec);
}

int TreeView_StartUp(void) {
    hSmallIcons = Icon.GetSmallIcons();
    TreeView_SetImageList(hwnd[WinTreeView], hSmallIcons, TVSIL_NORMAL);
    //for (i = 0; i < 4; i++) {
    //    static char *ext[] = {
    //        "", ".exe", ".txt", ".bmp", ".png", ".pdf", ".wav", ".mp3"
    //    };
    //    int iIcon;
    //    wsprintfA(path, "folder %d", i);
    //    iIcon = Icon.GetIndexFromAttributes(path, FILE_ATTRIBUTE_DIRECTORY);
    //    tvi.hParent             = TVI_ROOT;
    //    tvi.hInsertAfter        = TVI_LAST;
    //    tvi.item.mask           = TVIF_TEXT | TVIF_HANDLE | TVIF_IMAGE | TVIF_PARAM | TVIF_SELECTEDIMAGE; // | TVIF_CHILDREN | TVIF_STATE;
    //    //tvi.item.hItem          = 0;
    //    //tvi.item.state          = 0;
    //    //tvi.item.stateMask      = 0;
    //    tvi.item.pszText        = path;
    //    tvi.item.cchTextMax     = lstrlenA(path);
    //    tvi.item.iImage         = iIcon;
    //    tvi.item.iSelectedImage = iIcon+1;
    //    //tvi.item.cChildren      = 0;
    //    //tvi.item.lParam         = 0;
    //    tvi.hParent             = TreeView_InsertItem(hwnd[WinTreeView], &tvi);
    //    for (j = 1; j < elementsof(ext); j++) {
    //        wsprintfA(path, "document%s", ext[j]);
    //        iIcon = Icon.GetIndexFromAttributes(path, FILE_ATTRIBUTE_NORMAL);
    //        tvi.item.iImage         = iIcon;
    //        tvi.item.iSelectedImage = iIcon;
    //        tvi.item.pszText        = path;
    //        tvi.item.cchTextMax     = lstrlenA(path);
    //        TreeView_InsertItem(hwnd[WinTreeView], &tvi);
    //    }
    //}
    return 1;
}


struct TREEVIEW TreeView = {
    TreeView_StartUp,
    TreeView_DeleteAll,
    TreeView_AddItem,
    TreeView_AddDir,
    TreeView_AddFile,
};
