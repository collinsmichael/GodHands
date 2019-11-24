#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER Logger;
extern HWND hwnd[64];

static TBADDBITMAP tbbmp;
static TBBUTTON tbbtn[32];

static char *tip[32] = {
    "Back", "Forward", "Favorites", "Add To Favorites", "Tree View",
    "Cut", "Copy", "Paste",
    "Undo", "Redo", "Delete",
    "New File", "Open File", "Save File",
    "Print Preview", "Properties", "Help", "Find", "Replace", "Print",
    "Large Icons", "Small Icons", "List", "Details",
    "Sort Name", "Sort Size", "Sort Date", "Sort Type",
    "Parent Folder", "Net Connect", "Net Disconnect", "New Folder"
};
static int tool[] = {
    0x0000, 0x0000, 0x0000, 0x0000, 0x0401,
    0x0303, 0x0304, 0x0305,
    0x0301, 0x0302, 0x0000,
    0x0701, 0x0201, 0x0202,
    0x0000, 0x0000, 0x0601, 0x0000, 0x0000, 0x0000,
    0x0402, 0x0403, 0x0404, 0x0405,
    0x0000, 0x0000, 0x0000, 0x0000,
    0x0000, 0x0000, 0x0000, 0x0000
};

static MENUX menu[] = {
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0201, "Open ..." },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0202, "Save ..." },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0203, "Close" },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0204, "Exit" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0301, "Undo" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0302, "Redo" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0303, "Cut" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0304, "Copy" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0305, "Paste" },
    /* .... */ { MENUBAR_ITEM,  0x04, 0x0401, "Tree View" },
    /* .... */ { MENUBAR_ITEM,  0x04, 0x0402, "Large Icons" },
    /* .... */ { MENUBAR_ITEM,  0x04, 0x0403, "Small Icons" },
    /* .... */ { MENUBAR_ITEM,  0x04, 0x0404, "List View" },
    /* .... */ { MENUBAR_ITEM,  0x04, 0x0405, "Details" },
    /* .... */ { MENUBAR_ITEM,  0x05, 0x0501, "Tile Vertical" },
    /* .... */ { MENUBAR_ITEM,  0x05, 0x0502, "Tile Horizontal" },
    /* .... */ { MENUBAR_ITEM,  0x05, 0x0503, "Cascade" },
    /* .... */ { MENUBAR_ITEM,  0x05, 0x0504, "Arrange Icons" },
    /* .... */ { MENUBAR_ITEM,  0x06, 0x0601, "About" },
    /* .... */ { MENUBAR_ITEM,  0x07, 0x0701, "New" },
};

static int ToolBar_StartUp(void) {
    int i;

    tbbmp.hInst = HINST_COMMCTRL;
    SendMessageA(hwnd[WinToolBar], TB_BUTTONSTRUCTSIZE, (WPARAM)sizeof(TBBUTTON), 0);
    tbbmp.nID = IDB_HIST_SMALL_COLOR;
    SendMessageA(hwnd[WinToolBar], TB_ADDBITMAP, (WPARAM)0, (LPARAM)&tbbmp);
    tbbmp.nID = IDB_STD_SMALL_COLOR;
    SendMessageA(hwnd[WinToolBar], TB_ADDBITMAP, (WPARAM)0,  (LPARAM)&tbbmp);
    tbbmp.nID = IDB_VIEW_SMALL_COLOR;
    SendMessageA(hwnd[WinToolBar], TB_ADDBITMAP, (WPARAM)0, (LPARAM)&tbbmp);
    SendMessageA(hwnd[WinToolBar], TB_SETMAXTEXTROWS, 0, 0);

    for (i = 0; i < elementsof(tbbtn); i++ ) {
        tbbtn[i].iBitmap   = i;
        tbbtn[i].fsState   = TBSTATE_ENABLED;
        tbbtn[i].fsStyle   = TBSTYLE_BUTTON;
        tbbtn[i].idCommand = WM_USER + tool[i];
        tbbtn[i].iString   = (INT_PTR)tip[i];
    }
    SendMessageA(hwnd[WinToolBar], TB_ADDBUTTONS, (WPARAM)elementsof(tbbtn), (LPARAM)&tbbtn);
    return Logger.Done("ToolBar.StartUp", "Done");
}


struct TOOLBAR ToolBar = {
    ToolBar_StartUp
};
