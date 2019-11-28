/*****************************************************************************/
/* win32 list view example                                                   */
/*****************************************************************************/
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern ICON Icon;
extern HWND hwnd[64];

static HIMAGELIST hSmallIcons;
static HIMAGELIST hLargeIcons;

static LV_COLUMN lvc;
static LV_ITEM   lvi;
static char *columns[] = { "Name", "LBA", "Size", "Type" }; 
static char path[256];
static char lbax[256];
static char type[256];
static char size[256]; 

int ListView__DeleteAllItems(void) {
    ListView_DeleteAllItems(hwnd[WinListView]);
    return 1;
}

int ListView_ResetColumns(void) {
    int col;
    for (col = 0; col < elementsof(columns); col++) {
        lvc.mask       = LVCF_TEXT | LVCF_WIDTH;
        lvc.iSubItem   = col;
        lvc.cx         = 128;
        lvc.pszText    = columns[col];
        lvc.cchTextMax = lstrlenA(columns[col]);
        ListView_InsertColumn(hwnd[WinListView], col, &lvc);
    }
    return 1;
}

int ListView_AddItem(char *text, char *lba, char *size, char *type, DWORD Attribute) {
    int iIcon = Icon.GetIndexFromAttributes(path, Attribute);

    lvi.mask       = LVIF_TEXT | LVIF_IMAGE | LVIF_PARAM | LVIF_STATE;
    lvi.iImage     = iIcon;
    lvi.pszText    = path;
    lvi.cchTextMax = lstrlenA(path);
    lvi.state      = 0;
    lvi.stateMask  = 0;
    lvi.iItem      = ListView_GetItemCount(hwnd[WinListView]);
    lvi.lParam     = lvi.iItem;
    lvi.iSubItem   = 0;
    ListView_InsertItem(hwnd[WinListView], &lvi);

    lvi.mask = LVIF_TEXT;
    lvi.iSubItem++;
    lvi.pszText    = lba;
    lvi.cchTextMax = lstrlenA(lba);
    ListView_SetItem(hwnd[WinListView], &lvi);

    lvi.mask = LVIF_TEXT;
    lvi.iSubItem++;
    lvi.pszText    = size;
    lvi.cchTextMax = lstrlenA(size);
    ListView_SetItem(hwnd[WinListView], &lvi);

    lvi.mask = LVIF_TEXT;
    lvi.iSubItem++;
    lvi.pszText    = type;
    lvi.cchTextMax = lstrlenA(type);
    ListView_SetItem(hwnd[WinListView], &lvi);
    return 1;
}

int ListView_AddDir(ISO9660_DIR *rec) {
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
    wsprintfA(lbax, "0x%08X", rec->LsbLenData);
    wsprintfA(size, "%u bytes", rec->LsbLenData);
    lstrcpyA(type, "folder");
    ListView_AddItem(path, lbax, size, type, FILE_ATTRIBUTE_DIRECTORY);
    return 1;
}

int ListView_AddFile(ISO9660_DIR *rec) {
    int i;
    int x = 0;
    for (i = 0; i < rec->LenFileName; i++) {
        if (rec->FileName[i] == '.') {
            x = i;
        }
        if (rec->FileName[i] == ';') {
            break;
        } else {
            path[i] = rec->FileName[i];
        }
    }
    path[i] = 0;
    path[rec->LenFileName] = 0;
    wsprintfA(lbax, "0x%08X", rec->LsbLenData);
    wsprintfA(size, "%u bytes", rec->LsbLenData);
    lstrcpyA(type, &path[x]);
    ListView_AddItem(path, lbax, size, type, FILE_ATTRIBUTE_NORMAL);
    return 1;
}

int ListView_StartUp(void) {
    hwnd[WinListViewHeader] = (HWND)SendMessageA(hwnd[WinListView], LVM_GETHEADER, 0, 0);
    hSmallIcons = Icon.GetSmallIcons();
    ListView_SetImageList(hwnd[WinListView], hSmallIcons, LVSIL_SMALL);
    hLargeIcons = Icon.GetLargeIcons();
    ListView_SetImageList(hwnd[WinListView], hLargeIcons, LVSIL_NORMAL);

    ListView_DeleteAllItems(hwnd[WinListView]);
    ListView_ResetColumns();
    return 1;
}


struct LISTVIEW ListView = {
    ListView_StartUp,
    ListView__DeleteAllItems,
    ListView_ResetColumns,
    ListView_AddItem,
    ListView_AddDir,
    ListView_AddFile,
};
