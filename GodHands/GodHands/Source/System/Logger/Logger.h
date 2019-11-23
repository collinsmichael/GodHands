#ifndef LOGGER_H
#define LOGGER_H

typedef struct LOGGER {
    int (*Enable)(int mask);
    int (*Done)(char *func, char *format, ...);
    int (*Info)(char *func, char *format, ...);
    int (*Pass)(char *func, char *format, ...);
    int (*Warn)(char *func, char *format, ...);
    int (*Fail)(char *func, char *format, ...);
    int (*Error)(char *func, char *format, ...);
} LOGGER;

#endif // LOGGER_H
