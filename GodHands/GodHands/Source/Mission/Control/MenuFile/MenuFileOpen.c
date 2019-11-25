#include <windows.h>
#include "GodHands.h"


extern LOGGER Logger;
extern RAMDISK RamDisk;
extern DIALOG Dialog;
extern STATUSBAR StatusBar;

int MenuFile_Open(int code) {
    char *path = Dialog.OpenFileDialog(
        "CD Images (img bin iso)\0*.img;*.bin;*.iso\0"
        "All Files\0*.*\0\0");
    if (!path) return 0;

    if (!RamDisk.Open(path)) return 0;
    StatusBar.SetStatus("File.Open", "In Progress");
    StatusBar.SetProgress(0);

    Sleep(1*SEC);
    StatusBar.SetProgress(10);
    Sleep(1*SEC);
    StatusBar.SetProgress(20);
    Sleep(1*SEC);
    StatusBar.SetProgress(30);
    Sleep(1*SEC);
    StatusBar.SetProgress(40);
    Sleep(1*SEC);
    StatusBar.SetProgress(50);
    Sleep(1*SEC);
    StatusBar.SetProgress(60);
    Sleep(1*SEC);
    StatusBar.SetProgress(70);
    Sleep(1*SEC);
    StatusBar.SetProgress(80);
    Sleep(1*SEC);
    StatusBar.SetProgress(90);
    Sleep(1*SEC);
    StatusBar.SetProgress(100);

    // Do these quickly
    // [01] Read Primary Volume Descriptor
    // [02] Read Path Tables
    // [03] Read Root Directory
    // [04] Read Sub Directories
    // [05] Add Directories To TreeView
    // [06] Add Files To TreeView
    // [07] Add Directories (From Root Directory Only) To ListView
    // [08] Add Files (From Root Directory Only) To ListView

    // Do these slowly
    // [09] Read SLUS-010.40
    // [10] Read PRG Files
    // [11] Read ARM Files
    // [12] Read ZND Files
    // [13] Read MPD Files
    // [14] Read ZUD Files
    // [15] Read WEP Files
    // [16] Read SHP Files
    // [17] Read SEQ Files
    // [18] Read DAT Files

    // Do these very slowly
    // [19] Scan entire disk
    Sleep(10*SEC);
    StatusBar.SetStatus("File.Open", "Done");
    StatusBar.SetProgress(0);
    return Logger.Done("File.Open", "Done");
}
