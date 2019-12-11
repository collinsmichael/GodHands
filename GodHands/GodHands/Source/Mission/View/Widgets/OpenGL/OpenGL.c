#include <math.h>
#include <windows.h>
#include <gl\gl.h>
#include <gl\glu.h>
#include <gl\glaux.h>
#include "GodHands.h"


extern LOGGER Logger;

static PIXELFORMATDESCRIPTOR pfd = {
    sizeof(pfd),0x01,0x25,0,0x10,0,0,0,0,0,0,0,0,0,0,0,0,0,0x10,0,0,0,0,0,0,0
};


int InitOpenGLTexture(GLuint *ptr, int num, int resx, int resy, int *data) {
	glGenTextures(num, ptr);
	glBindTexture(GL_TEXTURE_2D, ptr[0]);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, resx, resy, 0, GL_RGBA, GL_UNSIGNED_BYTE, data);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	return 1;
}

int FreeOpenGLTexture(GLuint *ptr, int num) {
    glDeleteTextures(num, ptr);
    return 1;
}

void glPerspective(GLdouble FovY, GLdouble Aspect, GLdouble zNear, GLdouble zFar) { 
    GLdouble fW, fH;
    GLdouble pi = 3.1415926535897932384626433832795;
    fH = tan((FovY/2)/180*pi)*zNear;
    fH = tan(FovY/360*pi)*zNear;
    fW = fH*Aspect;
    glFrustum(-fW, fW, -fH, fH, zNear, zFar);
} 

LRESULT CALLBACK OpenGLProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    PAINTSTRUCT ps;
    RECT rc;
    HDC hDC;
    HGLRC hRC;

    switch (uMsg) {
    case WM_PAINT:
        BeginPaint(hWnd, &ps);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        glLoadIdentity();
        hDC = GetDC(hWnd);
        glRotatef(0.1f,0.0,0.0,1.0);
        glBegin(GL_QUADS);
        glColor3f(1.0,0.1,0.1);
        glVertex3f(-0.6,-0.6,0.0);
        glColor3f(0.1,0.1,0.1);
        glVertex3f(0.6,-0.6,0.0);
        glColor3f(0.1,0.1,1.0);
        glVertex3f(0.6,0.6,0.0);
        glColor3f(1.0,0.1,1.0);
        glVertex3f(-0.6,0.6,0.0);
        glEnd();
        SwapBuffers(hDC);
        EndPaint(hWnd, &ps);
        break;
    case WM_CREATE:
        hDC = GetDC(hWnd);
        SetPixelFormat(hDC, ChoosePixelFormat(hDC, &pfd), &pfd);
        hRC = wglCreateContext(hDC);
        if (!hRC) {
            return Logger.Error("OpenGL", "Failed to create GL context");
        }
        SetPropA(hWnd, "GLRC", (HANDLE)hRC);
        if (!wglMakeCurrent(hDC, hRC))  {
            return Logger.Error("OpenGL", "Failed to activate GL context");
        }
        //glShadeModel(GL_SMOOTH);
        //glClearColor(0.25f, 0.25f, 0.25f, 0.0f);
        //glClearDepth(65536.0f);
        //glEnable(GL_DEPTH_TEST);
        //glEnable(GL_TEXTURE_2D);
        //glDepthFunc(GL_LEQUAL);
        //glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);

        GetClientRect(hWnd, &rc);
        //glMatrixMode(GL_PROJECTION);
        //glLoadIdentity();
        glViewport(0, 0, rc.right, rc.bottom);
        //glPerspective(45.0f, (GLfloat)rc.right/(GLfloat)rc.bottom, 1.0f/1024.0f, 32768.0f);
        //glMatrixMode(GL_MODELVIEW);
        //glLoadIdentity();
        break;
    case WM_SIZE:
        GetClientRect(hWnd, &rc);
        //glMatrixMode(GL_PROJECTION);
        //glLoadIdentity();
        glViewport(0, 0, rc.right, rc.bottom);
        //glPerspective(45.0f, (GLfloat)rc.right/(GLfloat)rc.bottom, 1.0f/1024.0f, 32768.0f);
        //glMatrixMode(GL_MODELVIEW);
        //glLoadIdentity();
        break;
    case WM_CLOSE:
        hDC = GetDC(hWnd);
        hRC = (HGLRC)GetPropA(hWnd, "GLRC");
        wglMakeCurrent(0, 0);
        wglDeleteContext(hRC);
        ReleaseDC(hWnd, hDC);
        break;
    }
    return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}


//struct OPENGL OpenGL = {
//    OpenGL_Create,
//};
