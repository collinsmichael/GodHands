#ifndef OPENGL_H
#define OPENGL_H
#include <windows.h>

LRESULT CALLBACK OpenGLProc(HWND,UINT,WPARAM,LPARAM);


typedef struct OPENGL {
    int (*StartUp)(void);
    int (*CleanUp)(void);
} OPENGL;


#endif // OPENGL_H
