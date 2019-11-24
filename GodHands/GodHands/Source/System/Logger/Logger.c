/* ************************************************************************** */
/* Implements logging system.                                                 */
/* Uses lazy loading when reading from Disk                                   */
/* All changes are immediately committed to  to disk.                         */
/* ************************************************************************** */
#include <stdarg.h>
#include <windows.h>
#include "GodHands.h"


extern STATUSBAR StatusBar;


static int enabled;
static char err[0x200];
static char msg[0x100];

static int Logger_Enable(int mask) {
    enabled = mask;
    return 1;
}

static int Logger_Done(char *func, char *format, ...) {
    va_list list;
    va_start(list, format);
    wvsprintfA(msg, format, list);
    va_end(list);
    if ((enabled & 0x10)) StatusBar.SetStatus("Done", msg);
    return 1;
}

static int Logger_Info(char *func, char *format, ...) {
    va_list list;
    va_start(list, format);
    wvsprintfA(msg, format, list);
    va_end(list);

    if ((enabled & 0x08)) StatusBar.SetStatus("Info", msg);
    return 1;
}

static int Logger_Pass(char *func, char *format, ...) {
    va_list list;
    va_start(list, format);
    wvsprintfA(msg, format, list);
    va_end(list);

    StatusBar.SetStatus("Pass", msg);
    if ((enabled & 0x04)) {
        wsprintfA(err, "[PASS] %s\n\n%s", func, msg);
        MessageBoxA(0, msg, func, MB_OK);
    }
    return 1;
}

static int Logger_Warn(char *func, char *format, ...) {
    va_list list;
    va_start(list, format);
    wvsprintfA(msg, format, list);
    va_end(list);

    StatusBar.SetStatus("Warn", msg);
    if ((enabled & 0x02)) {
        wsprintfA(err, "[WARN] %s\n\n%s", func, msg);
        MessageBoxA(0, msg, func, MB_ICONWARNING);
    }
    return 1;
}

static int Logger_Fail(char *func, char *format, ...) {
    va_list list;
    va_start(list, format);
    wvsprintfA(msg, format, list);
    va_end(list);

    StatusBar.SetStatus("Fail", msg);
    if (enabled) {
        wsprintfA(err, "[FAIL] %s\n\n%s", func, msg);
        MessageBoxA(0, msg, func, MB_ICONWARNING);
    }
    return 0;
}

static int Logger_Error(char *func, char *format, ...) {
    va_list list;
    DWORD code = GetLastError();
    char *sys = (char*)LocalAlloc(LMEM_FIXED | LMEM_ZEROINIT, 4*MAX_PATH+2);
    DWORD form = FORMAT_MESSAGE_ALLOCATE_BUFFER|FORMAT_MESSAGE_FROM_SYSTEM;
    DWORD lang = MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT);
    FormatMessageA(form, 0, code, lang, (char*)&sys, 0, 0);

    va_start(list, format);
    wvsprintfA(msg, format, list);
    va_end(list);

    StatusBar.SetStatus("Error", msg);
    if (enabled) {
        wsprintfA(err, "[Error] %s Error code %08X\n\n%s\n\n%s", func, code, msg, sys);
        LocalFree((HLOCAL)sys);
        MessageBoxA(0, err, "Error", MB_ICONERROR);
    }
    return 0;
}


struct LOGGER Logger = {
    Logger_Enable,
    Logger_Done,
    Logger_Info,
    Logger_Pass,
    Logger_Warn,
    Logger_Fail,
    Logger_Error
};
