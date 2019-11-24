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
static char *ext[] = {
    "", ".exe", ".txt", ".bmp", ".png", ".pdf", ".wav", ".mp3"
}; 
static char text[256];
static char path[256];
static char type[256];
static char size[256]; 


int ListView_StartUp(void) {
    int row;

    hwnd[WinListViewHeader] = (HWND)SendMessageA(hwnd[WinListView], LVM_GETHEADER, 0, 0);
    hSmallIcons = Icon.GetSmallIcons();
    ListView_SetImageList(hwnd[WinListView], hSmallIcons, LVSIL_SMALL);
    hLargeIcons = Icon.GetLargeIcons();
    ListView_SetImageList(hwnd[WinListView], hLargeIcons, LVSIL_NORMAL);

    ListView_DeleteAllItems(hwnd[WinListView]);
    lvc.mask       = LVCF_TEXT | LVCF_WIDTH;
    //lvc.iSubItem   = 0;
    lvc.cx         = 128;
    lvc.pszText    = "Name";
    lvc.cchTextMax = lstrlenA(lvc.pszText);
    ListView_InsertColumn(hwnd[WinListView], 0, &lvc);

    lvc.iSubItem++;//lvc.iSubItem   = 1;
    //lvc.cx         = 128;
    lvc.pszText    = "Size";
    //lvc.cchTextMax = lstrlenA(lvc.pszText);
    ListView_InsertColumn(hwnd[WinListView], 1, &lvc);

    lvc.iSubItem++;//lvc.iSubItem   = 2;
    //lvc.cx         = 128;
    lvc.pszText    = "Type";
    //lvc.cchTextMax = lstrlenA(lvc.pszText);
    ListView_InsertColumn(hwnd[WinListView], 2, &lvc);

    for (row = 0; row < elementsof(ext); row++) { 
        DWORD Attribute;
        int iIcon;

        wsprintfA(path, "document%s", ext[row]);
        wsprintfA(size, "%u bytes", row*1024);
        lstrcpyA(type, (ext[row][0] == '.') ? ext[row] : "folder");
        Attribute = (ext[row][0] == '.')
            ? FILE_ATTRIBUTE_NORMAL : FILE_ATTRIBUTE_DIRECTORY;

        iIcon = Icon.GetIndexFromAttributes(path, Attribute);
        //if (row == 0) iIcon = 38;
        //else iIcon = ext[row][1]-'a';

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
        wsprintfA(size, "%u bytes", iIcon);
        lvi.pszText    = size;
        lvi.cchTextMax = lstrlenA(size);
        ListView_SetItem(hwnd[WinListView], &lvi);

        lvi.mask = LVIF_TEXT;
        lvi.iSubItem++;
        lvi.pszText    = type;
        lvi.cchTextMax = lstrlenA(type);
        ListView_SetItem(hwnd[WinListView], &lvi);
    }
    return 1;
}


struct LISTVIEW ListView = {
    ListView_StartUp,
};
