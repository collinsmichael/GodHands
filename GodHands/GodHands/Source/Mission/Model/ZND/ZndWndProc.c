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


static struct WINDOW wx[] = {
    //{ 0x00000000,"SysTabControl32",   0,0x56000000,134, 0, 666, 24,0, 0,0, "MS Sans Serif", "TabBar" },
    //{ 0x00000000,"SysListView32",     0,0x56000249,  0, 0, 128,552,0, 0,0, "MS Sans Serif", "ListView" },
    //{ 0x00000000,"SysListView32",     0,0x56000249,  0, 0, 128,552,0, 0,0, "MS Sans Serif", "ListView" },
    //{ 0x00000000,"Splitter",          0,0x56000000,128, 0,   6,552,0, 0,0, "MS Sans Serif", "Splitter" },

    //{ 0x00000000,"Static",  "Available Rooms:", 0x56000000, 20, 20,128, 20,0,0,0, "MS Sans Serif", 0 },
    //{ 0x00000000,"ListBox", "Available Rooms",  0x56300000, 20, 40,128,160,0,0,0, "Consolas", "A List of available rooms" },
    //{ 0x00000000,"Static",  "Included Rooms:",  0x56000000,220, 20,128, 20,0,0,0, "MS Sans Serif", 0 },
    //{ 0x00000000,"ListBox", "Included Rooms",   0x56300000,220, 40,128,160,0,0,0, "Consolas", "A List of rooms included in this zone" },
    //{ 0x00000000,"Button",  "<",                0x56000000,156,104, 28, 32,0,0,0, "Consolas", "Remove the selected room from this zone" },
    //{ 0x00000000,"Button",  ">",                0x56000000,188,104, 28, 32,0,0,0, "Consolas", "Add the selected room to this zone" },
    
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20, 20, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112, 20,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20, 48, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112, 48,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "3D Effects:", 0x56000000, 20, 72, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112, 72,128, 20,0,0,0, "Consolas", "Location in table of 3D model special effects" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,100, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,100,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "Name:",       0x56000000, 20,128, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,128,128, 20,0,0,0, "Consolas", "Character name" },
    { 0x00000000,"Static",  "HP:",         0x56000000, 20,156, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,156,128, 20,0,0,0, "Consolas", "Health points" },
    { 0x00000000,"Static",  "MP:",         0x56000000, 20,184, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,184,128, 20,0,0,0, "Consolas", "Magic points" },
    { 0x00000000,"Static",  "STR:",        0x56000000, 20,212, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,212,128, 20,0,0,0, "Consolas", "Strength" },
    { 0x00000000,"Static",  "INT:",        0x56000000, 20,240, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,240,128, 20,0,0,0, "Consolas", "Intelligence" },
    { 0x00000000,"Static",  "AGL:",        0x56000000, 20,268, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,268,128, 20,0,0,0, "Consolas", "Agility" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,296, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,296,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,324, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,324,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "CarrySPD:",   0x56000000, 20,352, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,352,128, 20,0,0,0, "Consolas", "Speed when carrying a crate" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,380, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,380,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "RunSPD:",     0x56000000, 20,408, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,408,128, 20,0,0,0, "Consolas", "Running speed" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,436, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,436,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,464, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,464,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,492, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,492,128, 20,0,0,0, "Consolas", "Unknown" },
    { 0x00000000,"Static",  "Unknown:",    0x56000000, 20,520, 80, 20,0,0,0, "Consolas", 0 },
    { 0x00000000,"Edit",    0,             0x56810004,112,520,128, 20,0,0,0, "Consolas", "Unknown" },
};


static int ZndOnCreate(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    int i;
    for (i = 0; i < elementsof(wx); i++) {
        HWND hChild = View.Window(hWnd, &wx[i]);
        if (!hChild) return 0;
        if (lstrcmpiA(wx[i].Class, "ListBox") == 0) {
            int j;
            for (j = 0; j < 32; j++) {
                char text[] = "ROOM01";
                text[4] = (j/10) + '0';
                text[5] = (j%10) + '0';
                SendMessage(hChild, LB_ADDSTRING, 0, (LPARAM)text);
            }
        }
    }
    return 0;
}

static int ZndOnDestroy(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    return 0;
}

LRESULT CALLBACK ZndWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_CREATE:  ZndOnCreate(hWnd, uMsg, wParam, lParam); break;
    case WM_DESTROY: ZndOnDestroy(hWnd, uMsg, wParam, lParam); break;
    }
    return 0;
}
