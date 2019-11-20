/* ************************************************************************** */
/* Implements logging system.                                                 */
/* Uses lazy loading when reading from Disk                                   */
/* All changes are immediately committed to  to disk.                         */
/* ************************************************************************** */
#include <stdarg.h>
#include <windows.h>
#include "System/System.h"


static int Logger_Print(char *level, char *func, char *format, va_list *list) {
    return 1;
}

static int Logger_Info(char *func, char *format, ...) {
    return 1;
}

static int Logger_Pass(char *func, char *format, ...) {
    return 1;
}

static int Logger_Warn(char *func, char *format, ...) {
    return 1;
}

static int Logger_Fail(char *func, char *format, ...) {
    return 0;
}


struct LOGGER Logger = {
    Logger_Info,
    Logger_Pass,
    Logger_Warn,
    Logger_Fail
};
