/*****************************************************************************/
/* win32 list view example                                                   */
/*****************************************************************************/
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern ISO9660 Iso9660;

extern ICON Icon;
extern HWND hwnd[64];

static HIMAGELIST hSmallIcons;
static HIMAGELIST hLargeIcons;

static LV_COLUMN lvc;
static LV_ITEM   lvi;
static char *columns[] = { "Name", "LBA", "Size" }; 
static char *format[elementsof(columns)] = { "%s", "0x%08X", "0x%08X" };
static char path[256];
static char lbax[256];
static char type[256];
static char size[256]; 
static ISO9660_DIR *nav_stack[256];
static int nav;
static int end;


static int ListView_DeleteAll(void) {
    ListView_DeleteAllItems(hwnd[WinListView]);
    return 1;
}

static int ListView_Reset(void) {
    int col;
    ListView_DeleteAllItems(hwnd[WinListView]);
    for (col = elementsof(columns); col >= 0; col--) {
        ListView_DeleteColumn(hwnd[WinListView], col);
    }
    for (col = 0; col < elementsof(columns); col++) {
        lvc.mask       = LVCF_TEXT | LVCF_WIDTH;
        lvc.iSubItem   = col;
        lvc.cx         = 128;
        lvc.pszText    = columns[col];
        lvc.cchTextMax = lstrlenA(columns[col]);
        ListView_InsertColumn(hwnd[WinListView], col, &lvc);
    }
    end = nav = 0;
    return 1;
}

static int ListView_AddItem(char *text, char *lba, char *size, DWORD Attribute, void *param) {
    int iIcon = Icon.GetIndexFromAttributes(path, Attribute);
    lvi.mask       = LVIF_TEXT | LVIF_IMAGE | LVIF_PARAM | LVIF_STATE;
    lvi.iImage     = iIcon;
    lvi.pszText    = path;
    lvi.cchTextMax = lstrlenA(path);
    lvi.state      = 0;
    lvi.stateMask  = 0;
    lvi.iItem      = ListView_GetItemCount(hwnd[WinListView]);
    lvi.lParam     = (LPARAM)param;
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
    return 1;
}

static int ListView_AddDir(ISO9660_DIR *rec) {
    int i;
    char *str = path;
    for (i = 0; i < rec->LenFileName; i++) {
        if (rec->FileName[i] < 0x20) continue;
        if (rec->FileName[i] == ';') break;
        *str++ = rec->FileName[i];
    }
    *str++ = 0;
    if (lstrlenA(path) > 0) {
        wsprintfA(lbax, format[1], rec->LsbLenData);
        wsprintfA(size, format[2], rec->LsbLenData);
        ListView_AddItem(path, lbax, size, FILE_ATTRIBUTE_DIRECTORY, rec);
    }
    return 1;
}

static int ListView_AddFile(ISO9660_DIR *rec) {
    int i;
    char *str = path;
    for (i = 0; i < rec->LenFileName; i++) {
        if (rec->FileName[i] < 0x20) continue;
        if (rec->FileName[i] == ';') break;
        *str++ = rec->FileName[i];
    }
    *str++ = 0;
    wsprintfA(lbax, format[1], rec->LsbLenData);
    wsprintfA(size, format[2], rec->LsbLenData);
    ListView_AddItem(path, lbax, size, FILE_ATTRIBUTE_NORMAL, rec);
    return 1;
}

static int ListView_Mount(void *param, ISO9660_DIR *rec) {
    if ((rec->FileFlags & ISO9660_DIRECTORY)) {
        ListView_AddDir(rec);
    } else {
        ListView_AddFile(rec);
    }
    return 1;
}

static int ListView_NavEnter(ISO9660_DIR *rec) {
    ListView_DeleteAll();
    if (!Iso9660.EnumDir(0, rec, ListView_Mount)) return 0;
    if (nav < elementsof(nav_stack)-1) {
        nav_stack[++nav] = rec;
        end = nav;
    }
    return 1;
}

static int ListView_NavBack(void) {
    if (nav > 1) {
        ISO9660_DIR *rec = nav_stack[--nav];
        ListView_DeleteAll();
        if (!Iso9660.EnumDir(0, rec, ListView_Mount)) return 0;
    }
    return 1;
}

static int ListView_NavForward(void) {
    if (nav < end) {
        ISO9660_DIR *rec = nav_stack[++nav];
        ListView_DeleteAll();
        if (!Iso9660.EnumDir(0, rec, ListView_Mount)) return 0;
    }
    return 1;
}

static int ListView_StartUp(void) {
    hwnd[WinListViewHeader] = (HWND)SendMessageA(hwnd[WinListView], LVM_GETHEADER, 0, 0);
    hSmallIcons = Icon.GetSmallIcons();
    ListView_SetImageList(hwnd[WinListView], hSmallIcons, LVSIL_SMALL);
    hLargeIcons = Icon.GetLargeIcons();
    ListView_SetImageList(hwnd[WinListView], hLargeIcons, LVSIL_NORMAL);
    ListView_Reset();
    return 1;
}


struct LISTVIEW ListView = {
    ListView_StartUp,
    ListView_Reset,
    ListView_DeleteAll,
    ListView_AddItem,
    ListView_AddDir,
    ListView_AddFile,
    ListView_Mount,
    ListView_NavEnter,
    ListView_NavBack,
    ListView_NavForward,
};
