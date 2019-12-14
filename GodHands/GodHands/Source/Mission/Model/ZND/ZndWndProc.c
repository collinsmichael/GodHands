#include <stdint.h>
#include <windows.h>
#include <commctrl.h>
#include "GodHands.h"


extern struct LOGGER    Logger;
extern struct VIEW      View;
extern struct FONT      Font;
extern struct ICON      Icon;
extern struct STATUSBAR StatusBar;
extern struct TABBAR    TabBar;
extern struct TOOLTIP   ToolTip;
extern struct ISO9660   Iso9660;
extern struct RAMDISK   RamDisk;

static TV_INSERTSTRUCT tvi;
static HTREEITEM hRoot;
static HIMAGELIST hSmallIcons;
static HIMAGELIST hLargeIcons;
static char path[256];

static struct WINDOW wx[] = {
    //{ 0x00000000,"Static",  "Available Rooms:", 0x56000000, 20, 20,128, 20,0,0,0, "MS Sans Serif", 0 },
    //{ 0x00000000,"ListBox", "Available Rooms",  0x56300000, 20, 40,128,160,0,0,0, "Consolas", "A List of available rooms" },
    //{ 0x00000000,"Static",  "Included Rooms:",  0x56000000,220, 20,128, 20,0,0,0, "MS Sans Serif", 0 },
    //{ 0x00000000,"ListBox", "Included Rooms",   0x56300000,220, 40,128,160,0,0,0, "Consolas", "A List of rooms included in this zone" },
    //{ 0x00000000,"Button",  "<",                0x56000000,156,104, 28, 32,0,0,0, "Consolas", "Remove the selected room from this zone" },
    //{ 0x00000000,"Button",  ">",                0x56000000,188,104, 28, 32,0,0,0, "Consolas", "Add the selected room to this zone" },

    { 0x00000000,"SysTreeView32", "TreeView", 0x5680000F, 12,12,128,552,0,0,0, "MS Sans Serif", "TreeView" },
    { 0x00000000,"Splitter",      "Splitter1",0x56000000,136, 0,  6,552,0,0,0, "MS Sans Serif", "Splitter" },
    { 0x00000000,"OpenGL",        "OpenGL",   0x56800004,142,12,180,552,0,0,0, "Consolas",      "Viewer" },
    { 0x00000000,"Splitter",      "Splitter2",0x56000000,326, 0,  6,552,0,0,0, "MS Sans Serif", "Splitter" },
    { 0x00000000,"PropSheet",     "PropSheet",0x56300000,332,12,264,552,0,0,0, "Consolas",      "Property Sheet" },

    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20, 20, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112, 20,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20, 48, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112, 48,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "3D Effects:", 0x56000000, 20, 72, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112, 72,128, 20,0,0,0, "Consolas", "Location in table of 3D model special effects" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,100, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,100,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "Name:",       0x56000000, 20,128, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,128,128, 20,0,0,0, "Consolas", "Character name" },
    //{ 0x00000000,"Static",   "HP:",         0x56000000, 20,156, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,156,128, 20,0,0,0, "Consolas", "Health points" },
    //{ 0x00000000,"Static",   "MP:",         0x56000000, 20,184, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,184,128, 20,0,0,0, "Consolas", "Magic points" },
    //{ 0x00000000,"Static",   "STR:",        0x56000000, 20,212, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,212,128, 20,0,0,0, "Consolas", "Strength" },
    //{ 0x00000000,"Static",   "INT:",        0x56000000, 20,240, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,240,128, 20,0,0,0, "Consolas", "Intelligence" },
    //{ 0x00000000,"Static",   "AGL:",        0x56000000, 20,268, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,268,128, 20,0,0,0, "Consolas", "Agility" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,296, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,296,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,324, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,324,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "CarrySPD:",   0x56000000, 20,352, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,352,128, 20,0,0,0, "Consolas", "Speed when carrying a crate" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,380, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,380,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "RunSPD:",     0x56000000, 20,408, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,408,128, 20,0,0,0, "Consolas", "Running speed" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,436, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,436,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,464, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,464,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,492, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,492,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"Static",   "Unknown:",    0x56000000, 20,520, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,112,520,128, 20,0,0,0, "Consolas", "Unknown" },
    //{ 0x00000000,"OpenGL",   0,             0x56000000,260, 20,400,520,0,0,0, "Consolas", "Video Display" },

    //{ 0x00000000,"Static",   "Item Name:",  0x56000000,260, 20, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"ComboBox", 0,             0x56810202,340, 20,128, 20,0,0,0, "Consolas", "Item Names List" },
    //{ 0x00000000,"Static",   "Item List:",  0x56000000,260, 48, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"ComboBox", 0,             0x56810202,340, 48,128, 20,0,0,0, "Consolas", "Item List (Depends on item type)" },
    //{ 0x00000000,"Static",   "WEP File:",   0x56000000,260, 76, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"ComboBox", 0,             0x56810202,340, 76,128, 20,0,0,0, "Consolas", "Weapon 3D Model" },
    //{ 0x00000000,"Static",   "Category:",   0x56000000,260,104, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"ComboBox", 0,             0x56810202,340,104,128, 20,0,0,0, "Consolas", "Item Category" },
    //{ 0x00000000,"Static",   "STR:",        0x56000000,260,132, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,340,132,128, 20,0,0,0, "Consolas", "Strength" },
    //{ 0x00000000,"Static",   "INT:",        0x56000000,260,160, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,340,160,128, 20,0,0,0, "Consolas", "Intelligence" },
    //{ 0x00000000,"Static",   "AGL:",        0x56000000,260,188, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,340,188,128, 20,0,0,0, "Consolas", "Agility" },
    //{ 0x00000000,"Static",   "Cur DP:",     0x56000000,260,216, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,340,216,128, 20,0,0,0, "Consolas", "Current Damage Points" },
    //{ 0x00000000,"Static",   "Max DP:",     0x56000000,260,244, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,340,244,128, 20,0,0,0, "Consolas", "Maximum Damage Points" },
    //{ 0x00000000,"Static",   "Cur PP:",     0x56000000,260,272, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,340,272,128, 20,0,0,0, "Consolas", "Current Phantom Points" },
    //{ 0x00000000,"Static",   "Max PP:",     0x56000000,260,300, 80, 20,0,0,0, "Consolas", 0 },
    //{ 0x00000000,"Edit",     0,             0x56810004,340,300,128, 20,0,0,0, "Consolas", "Maximum Phantom Points" },
};

static int TreeView_AddItem(HWND hTree, void *parent, char *path, DWORD Attribute, void *param) {
    int iIcon = Icon.GetIndexFromAttributes(path, Attribute);
    tvi.hParent             = (parent) ? (HTREEITEM)parent : TVI_ROOT;
    tvi.hInsertAfter        = TVI_LAST;
    tvi.item.mask           = TVIF_TEXT|TVIF_HANDLE|TVIF_PARAM|TVIF_IMAGE|TVIF_SELECTEDIMAGE;
    tvi.item.pszText        = path;
    tvi.item.cchTextMax     = lstrlenA(path);
    tvi.item.iImage         = iIcon;
    tvi.item.iSelectedImage = (Attribute == FILE_ATTRIBUTE_DIRECTORY) ? iIcon+1 : iIcon;
    tvi.item.lParam         = (LPARAM)param;
    return (int)TreeView_InsertItem(hTree, &tvi);
}

static int TreeView_AddDir(HWND hTree, void *parent, REC *rec) {
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
    return TreeView_AddItem(hTree, parent, path, FILE_ATTRIBUTE_DIRECTORY, rec);
}

static int ZndOnLoad(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    HWND hTree;
    ZNDHDR *hdr;
    uint8_t *ptr;
    int root;

    REC *rec = (REC*)GetPropA(hWnd, "REC");
    if (!rec) return 0;
    ptr = (uint8_t*)RamDisk.AddressOf(rec->LsbLbaData);
    if (!ptr) return 0;
    hdr = (ZNDHDR*)ptr;

    hTree = (HWND)GetPropA(hWnd, "TreeView");
    TreeView_DeleteAllItems(hTree);
    root = TreeView_AddDir(hTree, TVI_ROOT, rec);
    TreeView_AddDir(hTree, (void*)root, rec);
    TreeView_AddDir(hTree, (void*)root, rec);
    TreeView_AddDir(hTree, (void*)root, rec);
    TreeView_AddDir(hTree, (void*)root, rec);
    return 0;
}

static int ZndOnCreate(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    HWND hChild[elementsof(wx)];
    RECT rc;
    int i;
    int width;
    int height;

    GetClientRect(hWnd, &rc);
    width = rc.right - rc.left - 36;
    height = rc.bottom - rc.top;

    wx[0].Width = width/4;
    wx[2].Width = width/2;
    wx[4].Width = width/4;
    wx[1].PosX = wx[0].PosX + wx[0].Width;
    wx[2].PosX = wx[1].PosX + wx[1].Width;
    wx[3].PosX = wx[2].PosX + wx[2].Width;
    wx[4].PosX = wx[3].PosX + wx[3].Width;

    for (i = 0; i < elementsof(wx); i++) {
        wx[i].PosY = (i%2 == 0) ? 12 : 0;
        wx[i].Height = height - 2*wx[i].PosY;
    }
    for (i = 0; i < elementsof(wx); i++) {
        hChild[i] = View.Window(hWnd, &wx[i]);
        if (!hChild[i]) return 0;
        SetPropA(hWnd, wx[i].Window, (HANDLE)hChild[i]);
    }

    SendMessageA(hChild[1], SB_SETLEFTWINDOWS,  (WPARAM)1, (LPARAM)&hChild[0]);
    SendMessageA(hChild[1], SB_SETRIGHTWINDOWS, (WPARAM)1, (LPARAM)&hChild[2]);
    SendMessageA(hChild[3], SB_SETLEFTWINDOWS,  (WPARAM)1, (LPARAM)&hChild[2]);
    SendMessageA(hChild[3], SB_SETRIGHTWINDOWS, (WPARAM)1, (LPARAM)&hChild[4]);
    return ZndOnLoad(hWnd, uMsg, wParam, lParam);
}

static int ZndOnDestroy(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    return 0;
}

static int ZndOnSize(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    HWND hTree, hSplit1, hOpenGL, hSplit2, hProp;
    RECT rTree, rSplit1, rOpenGL, rSplit2, rProp, rClient;

    hTree   = (HWND)GetPropA(hWnd, "TreeView");
    hSplit1 = (HWND)GetPropA(hWnd, "Splitter1");
    hOpenGL = (HWND)GetPropA(hWnd, "OpenGL");
    hSplit2 = (HWND)GetPropA(hWnd, "Splitter2");
    hProp   = (HWND)GetPropA(hWnd, "PropSheet");

    GetClientRect(hWnd, &rClient);
    GetWindowRect(hTree, &rTree);
    GetWindowRect(hSplit1, &rSplit1);
    GetWindowRect(hOpenGL, &rOpenGL);
    GetWindowRect(hSplit2, &rSplit2);
    GetWindowRect(hProp, &rProp);

    rClient.bottom = rClient.bottom - rClient.top;
    rClient.right  = rClient.right  - rClient.left;
    rClient.top    = 0;
    rClient.left   = 0;

    rTree.right    = rTree.right    - rTree.left;
    rProp.right    = rProp.right    - rProp.left;
    rSplit1.right  = 6;
    rSplit2.right  = 6;
    rTree.left     = 0;
    rProp.left     = rClient.right - rProp.right;

    rSplit1.left   = rTree.left    + rTree.right;
    rSplit2.left   = rProp.left    - rSplit2.right;
    rOpenGL.left   = rSplit1.left  + rSplit2.right;
    rOpenGL.right  = rSplit2.left  - rOpenGL.left;

    MoveWindow(hTree,   rTree.left,   0, rTree.right,   rClient.bottom, TRUE);
    MoveWindow(hOpenGL, rOpenGL.left, 0, rOpenGL.right, rClient.bottom, TRUE);
    MoveWindow(hProp,   rProp.left,   0, rProp.right,   rClient.bottom, TRUE);
    MoveWindow(hSplit1, rSplit1.left, 0, rSplit1.right, rClient.bottom, TRUE);
    MoveWindow(hSplit2, rSplit2.left, 0, rSplit2.right, rClient.bottom, TRUE);
    return 0;
}

LRESULT CALLBACK ZndWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_CREATE:  ZndOnCreate(hWnd, uMsg, wParam, lParam); break;
    case WM_DESTROY: ZndOnDestroy(hWnd, uMsg, wParam, lParam); break;
    case WM_SIZE:    ZndOnSize(hWnd, uMsg, wParam, lParam); break;
    }
    return 0;
}
