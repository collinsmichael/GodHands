#ifndef MODEL_H
#define MODEL_H
#include <windows.h>


LRESULT CALLBACK ArmWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
LRESULT CALLBACK MpdWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
LRESULT CALLBACK ShpWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
LRESULT CALLBACK WepWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
LRESULT CALLBACK ZndWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
LRESULT CALLBACK ZudWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);


typedef struct MODEL {
    int (*StartUp)(void);
    int (*CleanUp)(void);
    int (*Execute)(void);
    int (*Reset)(void);

    int (*OpenDisk)(void *evt);
    WNDPROC (*GetWndProc)(REC *rec);
} MODEL;


#endif // MODEL_H
