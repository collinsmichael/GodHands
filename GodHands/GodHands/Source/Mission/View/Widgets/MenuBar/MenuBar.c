#include <windows.h>
#include "GodHands.h"


int FlickerFree(HWND hwnd);
extern struct LOGGER Logger;
extern HWND hwnd[64];


static MENUX menu[] = {
    /* 0x00 */ { 0 },
    /* 0x01 */ { MENUBAR_MENU,  0x00, 0x0000, "Menu" },
    /* 0x02 */ { MENUBAR_POPUP, 0x01, 0x0000, "File" },
    /* 0x03 */ { MENUBAR_POPUP, 0x01, 0x0000, "Edit" },
    /* 0x04 */ { MENUBAR_POPUP, 0x01, 0x0000, "Help" },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0101, "Open ..." },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0102, "Save ..." },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0103, "Close" },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0000, "-" },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0104, "Exit" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0201, "Undo" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0202, "Redo" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0000, "-" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0203, "Cut" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0204, "Copy" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0205, "Paste" },
    /* .... */ { MENUBAR_ITEM,  0x04, 0x0301, "About" },
};
HMENU hmenu[64];
HACCEL hAccel;


static int MenuBar_StartUp(void) {
    int parent;
    int i;

    MENUITEMINFOA mii;
    stosb(&mii, 0, sizeof(mii));
    mii.cbSize = sizeof(mii);
    mii.fMask  = MIIM_ID|MIIM_STRING;// |MIIM_BITMAP 
    //hBitmap = LoadBitmapA(GetModuleHandleA(0), (char*)IDB_TREE);

    for (i = 0; i < elementsof(menu); i++) {
        if (menu[i].Type == MENUBAR_MENU) hmenu[i] = CreateMenu();
        if (menu[i].Type == MENUBAR_POPUP) hmenu[i] = CreatePopupMenu();
    }

    for (i = elementsof(menu)-1; i >= 0; i--) {
        if (menu[i].Type == MENUBAR_ITEM) {
            if (menu[i].Name[0] == '-') {
                mii.fType = MFT_SEPARATOR;
                mii.dwTypeData = 0;
                mii.cch = 0;
                mii.wID = 0;
                //mii.hbmpItem = 0;
            } else {
                mii.fType = MFT_STRING; // |MFT_BITMAP
                mii.dwTypeData = menu[i].Name;
                mii.cch = strlen(menu[i].Name);
                mii.wID = WM_USER + menu[i].Code;
                //mii.hbmpItem = hBitmap;
            }
            parent = menu[i].Parent;
            if (!InsertMenuItemA(hmenu[parent], 0, TRUE, &mii)) {
                return Logger.Error("MenuBar.StartUp",
                    "Failed to add menu %s/%s",
                    menu[parent].Name, menu[i].Name);
            }
        }
    }

    for (i = 0; i < elementsof(menu); i++) {
        if (menu[i].Type == MENUBAR_POPUP) {
            HMENU hMenu = hmenu[menu[i].Parent];
            if (!AppendMenuA(hMenu, MF_POPUP, (UINT)hmenu[i], menu[i].Name)) {
                return Logger.Error("MenuBar.StartUp", "Failed to add menu %s",
                    menu[i].Name);
            }
        }
    }
    return Logger.Done("MenuBar.StartUp", "Done");
}

static int MenuBar_CleanUp(void) {
    return Logger.Done("MenuBar.CleanUp", "Done");
}

static int MenuBar_SetMenu(int win, int menu) {
    //*****************************************************
    //My best attempt at making the menubar flicker free...
    //Always fails to return a valid mbi.hwndMenu
    //*****************************************************
    MENUBARINFO mbi;
    stosb(&mbi, 0, sizeof(mbi));
    mbi.cbSize = sizeof(mbi);

    SetMenu(hwnd[win], hmenu[menu]);
    if (!GetMenuBarInfo(hwnd[win], OBJID_MENU, 0, &mbi)) {
        Logger.Error("Menu.SetMenu", "Failed to retrieve menubar info");
    }
    hwnd[WinMenuBar] = mbi.hwndMenu;
    if (hwnd[WinMenuBar]) {
        FlickerFree(hwnd[WinMenuBar]);
    }
    return Logger.Done("MenuBar.SetMenu", "Done");
}


struct MENUBAR MenuBar = {
    MenuBar_StartUp,
    MenuBar_CleanUp,
    MenuBar_SetMenu,
};
