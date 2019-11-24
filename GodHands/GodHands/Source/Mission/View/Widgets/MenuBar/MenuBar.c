#include <windows.h>
#include "GodHands.h"


int FlickerFree(HWND hwnd);
extern struct LOGGER Logger;
extern HWND hwnd[64];

static ACCEL accel[] = {
    { FCONTROL|FVIRTKEY,        'O', WM_USER + 0x0201 }, // File/Open
    { FCONTROL|FVIRTKEY,        'S', WM_USER + 0x0202 }, // File/Save
    { FCONTROL|FVIRTKEY|FSHIFT, 'C', WM_USER + 0x0203 }, // File/Close
    { FCONTROL|FVIRTKEY,        'Q', WM_USER + 0x0204 }, // File/Exit
    { FCONTROL|FVIRTKEY,        'Z', WM_USER + 0x0301 }, // Edit/Undo
    { FCONTROL|FVIRTKEY,        'Y', WM_USER + 0x0302 }, // Edit/Redo
    { FCONTROL|FVIRTKEY,        'X', WM_USER + 0x0303 }, // Edit/Cut
    { FCONTROL|FVIRTKEY,        'C', WM_USER + 0x0304 }, // Edit/Copy
    { FCONTROL|FVIRTKEY,        'V', WM_USER + 0x0305 }, // Edit/Paste
    { FVIRTKEY,               VK_F1, WM_USER + 0x0501 }, // Help/About
};

static MENUX menu[] = {
    /* 0x00 */ { 0 },
    /* 0x01 */ { MENUBAR_MENU,  0x00, 0x0000, "Menu" },
    /* 0x02 */ { MENUBAR_POPUP, 0x01, 0x0000, "File" },
    /* 0x03 */ { MENUBAR_POPUP, 0x01, 0x0000, "Edit" },
    /* 0x04 */ { MENUBAR_POPUP, 0x01, 0x0000, "View" },
    /* 0x05 */ { MENUBAR_POPUP, 0x01, 0x0000, "Window" },
    /* 0x06 */ { MENUBAR_POPUP, 0x01, 0x0000, "Help" },
    /* 0x07 */ { MENUBAR_POPUP, 0x01, 0x0000, "Dev" },

    /* .... */ { MENUBAR_ITEM,  0x02, 0x0201, "Open ..." },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0202, "Save ..." },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0203, "Close" },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0000, "-" },
    /* .... */ { MENUBAR_ITEM,  0x02, 0x0204, "Exit" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0301, "Undo" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0302, "Redo" },
    /* .... */ { MENUBAR_ITEM,  0x03, 0x0000, "-" },
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

    hAccel = CreateAcceleratorTableA(accel, elementsof(accel));
    if (!hAccel) {
        return Logger.Error("MenuBar.StartUp", "Failed to create hot keys");
    }
    return Logger.Done("MenuBar.StartUp", "Done");
}

static int MenuBar_CleanUp(void) {
    DestroyAcceleratorTable(hAccel);
    return Logger.Done("MenuBar.CleanUp", "Done");
}

static int MenuBar_SetMenu(int win, int menu) {
    SetMenu(hwnd[win], hmenu[menu]);
    return Logger.Done("MenuBar.SetMenu", "Done");
}


struct MENUBAR MenuBar = {
    MenuBar_StartUp,
    MenuBar_CleanUp,
    MenuBar_SetMenu,
};
