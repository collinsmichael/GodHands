#include <windows.h>
#include <commctrl.H>
#include "GodHands.h"


extern LOGGER Logger;


static HMODULE hShell32;
static HIMAGELIST hIconSmall;
static HIMAGELIST hIconLarge;
static int nCount;
static SHFILEINFO sfi;
static HIMAGELIST hImageList;
static HIMAGELIST fi;
static DWORD Attributes;
static int UsingShell;


static HIMAGELIST Icon_GetSmallIcons(void) {
    return hIconSmall;
}

static HIMAGELIST Icon_GetLargeIcons(void) {
    return hIconLarge;
}

static HICON Icon_GetSmallIcon(char *path) {
    if (UsingShell) {
        Attributes = GetFileAttributesA(path);
        fi = (HIMAGELIST)SHGetFileInfoA(path, Attributes, &sfi, sizeof(sfi),
            SHGFI_USEFILEATTRIBUTES | SHGFI_SYSICONINDEX | SHGFI_SMALLICON);
        if (!fi) {
            Logger.Error("Icon.GetSmallIcon",
                "Failed to retrieve file info '%s'", path);
            return 0;
        }
        //ImageList_Destroy(fi);
        return sfi.hIcon;
    } else {
        int iIcon = Icon_GetIndex(path);
        return ImageList_GetIcon(hIconSmall, iIcon, ILD_IMAGE | ILD_NORMAL);
    }
    return 0;
}

static HICON Icon_GetLargeIcon(char *path) {
    if (UsingShell) {
        Attributes = GetFileAttributesA(path);
        fi = (HIMAGELIST)SHGetFileInfoA(path, Attributes, &sfi, sizeof(sfi),
            SHGFI_USEFILEATTRIBUTES | SHGFI_SYSICONINDEX | SHGFI_LARGEICON);
        if (!fi) {
            Logger.Error("Icon.GetLargeIcon",
                "Failed to retrieve file info '%s'", path);
            return 0;
        }
        //ImageList_Destroy(fi);
        return sfi.hIcon;
    } else {
        int iIcon = Icon_GetIndex(path);
        return ImageList_GetIcon(hIconLarge, iIcon, ILD_IMAGE | ILD_NORMAL);
    }
    return 0;
}

static BITMAP *Icon_GetBitmap(HICON hIcon) {
    ICONINFO ii;
    static BITMAP bmp;
    GetIconInfo(hIcon, &ii);
    GetObject(ii.hbmColor, sizeof(bmp), &bmp);
    DeleteObject(ii.hbmColor);
    return &bmp;
}

static int Icon_LargeX(void) {
    if (UsingShell) {
        HICON hIcon = Icon_GetLargeIcon("test.exe");
        BITMAP *bmp = Icon_GetBitmap(hIcon);
        return bmp->bmWidth;
    } else {
        return 48;
    }
}

static int Icon_LargeY(void) {
    if (UsingShell) {
        HICON hIcon = Icon_GetLargeIcon("test.exe");
        BITMAP *bmp = Icon_GetBitmap(hIcon);
        return bmp->bmHeight;
    } else {
        return 48;
    }
}

static int Icon_SmallX(void) {
    if (UsingShell) {
        HICON hIcon = Icon_GetSmallIcon("test.exe");
        BITMAP *bmp = Icon_GetBitmap(hIcon);
        return bmp->bmWidth;
    } else {
        return 16;
    }
}

static int Icon_SmallY(void) {
    if (UsingShell) {
        HICON hIcon = Icon_GetSmallIcon("test.exe");
        BITMAP *bmp = Icon_GetBitmap(hIcon);
        return bmp->bmHeight;
    } else {
        return 16;
    }
}

static int GetShellIcons(void) {
    BOOL (__stdcall*Shell_GetImageLists)(HIMAGELIST *hLarge, HIMAGELIST *hSmall);
    BOOL (__stdcall*FileIconInit)(BOOL fFullInit);
    if (hIconSmall && hIconLarge) return 1;

    hShell32 = LoadLibraryA("shell32.dll");
    if (!hShell32) {
        return Logger.Error("Icon.GetShellIcons", "Can't find shell32.dll");
    }

    FileIconInit = (BOOL(__stdcall*)(BOOL))GetProcAddress(hShell32, (char*)660);
    if (!FileIconInit) {
        return Logger.Error("Icon.GetShellIcons", "Failed find FileIconInit");
    }

    Shell_GetImageLists = (BOOL(__stdcall*)(HIMAGELIST*,HIMAGELIST*))GetProcAddress(hShell32, (char*)71);
    if (!Shell_GetImageLists) {
        return Logger.Error("Icon.GetShellIcons", "Can't find GetImageLists");
    }

    if (!FileIconInit(TRUE)) {
        return Logger.Error("Icon.GetShellIcons", "Can't find shell icons");
    }

    nCount = Shell_GetImageLists(&hIconLarge, &hIconSmall);
    if (!nCount) {
        return Logger.Error("Icon.GetShellIcons", "Can't retrieve icons");
    }
    return (hIconLarge && hIconSmall);
}

static int Icon_StartUp(void) {
    if (GetShellIcons()) {
        UsingShell = 1;
        return 1;
    } else {
        HBITMAP hBitmap;
        UsingShell = 0;

        hBitmap = (HBITMAP)LoadImage( NULL, "img16x16.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
        if (!hBitmap) {
            return Logger.Error("Icon.StartUp", "Failed to load small icon pack!");
        }
        hIconSmall = ImageList_Create(16, 16, ILC_COLOR24, 40, 0);
        if (!hIconSmall) {
            return Logger.Error("Icon.StartUp", "Failed to load small icon pack!");
        }
        ImageList_Add(hIconSmall, hBitmap, NULL);

        hBitmap = (HBITMAP)LoadImage(NULL, "img48x48.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
        if (!hBitmap) {
            return Logger.Error("Icon.StartUp", "Can't find icon pack!");
        }
        hIconLarge = ImageList_Create(48, 48, ILC_COLOR24, 40, 0);
        if (!hIconLarge) {
            return Logger.Error("Icon.StartUp", "Can't find icon pack!");
        }
        ImageList_Add(hIconLarge, hBitmap, NULL);
        return (hIconLarge && hIconSmall);
    }
    return 0;
}


static int Icon_CleanUp(void) {
    if (hShell32) FreeLibrary(hShell32);
    hIconSmall = hIconLarge = 0;
    hShell32 = 0;
    return 1;
}

static int Icon_GetIndexFromAttributes(char *path, DWORD Attributes) {
    if (UsingShell) {
        fi = (HIMAGELIST)SHGetFileInfoA(path, Attributes, &sfi, sizeof(sfi),
            SHGFI_USEFILEATTRIBUTES | SHGFI_SYSICONINDEX | SHGFI_SMALLICON);
        if (!fi) {
            return Logger.Error("Icon.GetIndexFromAttribute",
                "Failed to retrieve file info '%s'", path);
        }
        ImageList_Destroy(fi);
        return sfi.iIcon;
    } else {
        if (Attributes & FILE_ATTRIBUTE_DIRECTORY) {
            return 38;
        } else {
            int i;
            for (i = lstrlenA(path)-1; i >= 0; i--) {
                if (path[i] == '.') {
                    char x = path[i+1];
                    if (x >= 'a' && x <= 'z') return x - 'a';
                    if (x >= 'A' && x <= 'Z') return x - 'A';
                    if (x >= '0' && x <= '9') return x - '0';
                    return 36;
                }
            }
        }
        return 36;
    }
    return 0;
}

static int Icon_GetIndex(char *path) {
    Attributes = FILE_ATTRIBUTE_NORMAL;
    return Icon_GetIndexFromAttributes(path, Attributes);
}


struct ICON Icon = {
    Icon_StartUp,
    Icon_CleanUp,
    Icon_GetSmallIcons,
    Icon_GetLargeIcons,
    Icon_GetIndexFromAttributes,
    Icon_GetIndex,
    Icon_LargeX,
    Icon_LargeY,
    Icon_SmallX,
    Icon_SmallY,
};
