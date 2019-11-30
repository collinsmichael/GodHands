#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern struct DIALOG Dialog;
extern struct LISTVIEW ListView;

extern struct JOBQUEUE JobQueue;
extern struct STATUSBAR StatusBar;
extern struct MDICLIENT MdiClient;
extern struct MENUBAR MenuBar;
extern struct WINDOW wx[16];
extern HWND hwnd[16];

static LRESULT NotifyOnDoubleClick(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    LV_ITEMA lvi;
    REC *rec;

    NMHDR *nm_hdr = (NMHDR*)lParam;
    if (nm_hdr->hwndFrom == hwnd[WinTreeView]) {
        //TabControl.MakeTab(navselection);
    } else if (nm_hdr->hwndFrom == hwnd[WinListView]) {
        lvi.iItem = ((LPNMITEMACTIVATE)lParam)->iItem;
        lvi.mask = LVIF_PARAM;
        ListView_GetItem(hwnd[WinListView], &lvi);
        rec = (REC*)lvi.lParam;
        if ((rec->FileFlags & RECECTORY)) {
            ListView.NavEnter(rec);
        } else {
            StatusBar.SetStatus("DoubleClick", "TODO Open Selected File");
        }
    }
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
}

#if 0
static LRESULT NotifyOnRightClick(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    static NMHDR *nm_hdr;
    static NM_TREEVIEW *nm_tv;
    nm_hdr = (NMHDR*)lParam;
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
}

static LRESULT NotifyOnTabControlSelectChanged(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    CurTab = TabCtrl_GetCurSel(hWin[WndTabCtrl]);
    TabControl.SetTab(CurTab);
    //StatusBar.Text("tab : %u", CurTab);
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
}

static LRESULT NotifyOnTreeViewSelectChanged(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    static RECT rect;
    static NMHDR       *nm_hdr;
    static NM_TREEVIEW *nm_tv;
    static word oldItem, newItem, newParent, curItem;
    static char itemname[0x100], itemsize[0x100], itemext[0100], itemlba[0x100], temp[0x100];
    nm_hdr = (NMHDR*)lParam;
    nm_tv = (NM_TREEVIEW*)nm_hdr;

    oldItem = (int)nm_tv->itemOld.lParam;
    newItem = (int)nm_tv->itemNew.lParam;
    newParent = FileParent[newItem];
    navselection = newItem;
    if ((FileFlag[newItem] & FLAG_DIRECTORY) == FLAG_DIRECTORY) {
        newdirectory = newItem;
    } else {
        newdirectory = newParent;
    }
    StatusBar.Text("%s", &FileName[newItem*0x100]);
    if (newdirectory != navdirectory) {
        navdirectory = newdirectory;
        SendMessage(hWin[WndListView], WM_SETREDRAW, FALSE, 0);
        ListView.Clear(hWin[WndListView]);
        curItem = 0;
        for (word i = 0; i < NumFiles; i++) {
            if ((FileParent[i] == navdirectory) && (i != navdirectory)) {
                strcpy_s(itemname, sizeof(itemname), &FileName[i*0x100]);
                wsprintf(itemsize, "%u", FileSize[i]);
                if ((FileFlag[i] & FLAG_DIRECTORY) == FLAG_DIRECTORY) {
                    strcpy_s(itemext, sizeof(itemext), ".Folder");
                } else {
                    _splitpath_s(&FileName[i*0x100], 0,0,0,0,0,0, temp,sizeof(temp));
                    wsprintf(itemext, "%s File", temp);
                }
                wsprintf(itemlba, "%u", FileLBA[i]);
                ListView.Row(hWin[WndListView],     curItem, 0, itemname, i);
                ListView.SubItem(hWin[WndListView], curItem, 1, itemsize, i);
                ListView.SubItem(hWin[WndListView], curItem, 2, &itemext[1], i);
                ListView.SubItem(hWin[WndListView], curItem, 3, itemlba, i);
                curItem++;
            }
        }
        SendMessage(hWin[WndListView], WM_SETREDRAW, TRUE, 0);
        RedrawWindow(hWin[WndListView], 0, 0, RDW_UPDATENOW);
    }
    GetClientRect(hWin[WndSplitter], &rect);
    MoveWindow(hWin[WndSplitter], rect.left, rect.top, rect.right, rect.bottom, TRUE);
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
}
#endif

static LRESULT NotifyOnListViewItemChanged(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    //NM_LISTVIEW *nm_lv = (NM_LISTVIEW*)lParam;
    //REC *rec = (REC*)nm_lv->lParam;
    //if (rec->FileFlags & RECECTORY) {
    //    return ListView.NavEnter(rec);
    //}
    return 0;
}

static LRESULT CALLBACK OnNotifyTcnSelChange(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    TCITEM tci;
    RECT rc;
    NMHDR *NmHdr = (NMHDR*)lParam;
    int TabIndex = TabCtrl_GetCurSel(hwnd[WinTabBar]);
    tci.mask = TCIF_PARAM;
    TabCtrl_GetItem(hwnd[WinTabBar], TabIndex, &tci);
    if (tci.lParam) {
        SendMessageA(hwnd[WinMdiClient], WM_MDIACTIVATE, (WPARAM)tci.lParam, 0);
        GetClientRect(hwnd[WinMdiClient], &rc);
        InvalidateRect(hwnd[WinMdiClient], &rc, TRUE);
    }
    return 1;
}

LRESULT CALLBACK OnNotifyProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    static NMHDR *nm_hdr;
    nm_hdr = (NMHDR*)lParam;

    switch (nm_hdr->code) {
    case NM_DBLCLK:          return NotifyOnDoubleClick(hWnd, uMsg, wParam, lParam);
    case NM_RCLICK:          return 0;//NotifyOnRightClick(hWnd, uMsg, wParam, lParam);
    // Tree View Notifications
    case TVN_BEGINDRAG:      break;
    case TVN_BEGINLABELEDIT: break;
    case TVN_BEGINRDRAG:     break;
    case TVN_DELETEITEM:     break;
    case TVN_ENDLABELEDIT:   break;
    case TVN_GETDISPINFO:    break;
    case TVN_ITEMEXPANDED:   break;
    case TVN_ITEMEXPANDING:  break;
    case TVN_KEYDOWN:        break;
    case TVN_SELCHANGING:    break;
    case TVN_SETDISPINFO:    break;
    case TVN_SELCHANGED:     return 0;//NotifyOnTreeViewSelectChanged(hWnd, uMsg, wParam, lParam);
    // List View Notifications
    case LVN_BEGINDRAG:      break;
    case LVN_BEGINLABELEDIT: break;
    case LVN_BEGINRDRAG:     break;
    case LVN_COLUMNCLICK:    break;
    case LVN_DELETEALLITEMS: break;
    case LVN_DELETEITEM:     break;
    case LVN_ENDLABELEDIT:   break;
    case LVN_GETDISPINFO:    break;
    case LVN_INSERTITEM:     break;
    case LVN_ITEMCHANGING:   break;
    case LVN_KEYDOWN:        break;
    case LVN_SETDISPINFO:    break;
    case LVN_ITEMCHANGED:    return 0;//NotifyOnListViewItemChanged(hWnd, uMsg, wParam, lParam);
    // Tab Control Notifications
    case TCN_KEYDOWN:        break;
    case TCN_SELCHANGING:    break;
    case TCN_SELCHANGE:      return OnNotifyTcnSelChange(hWnd, uMsg, wParam, lParam);
    }
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}
