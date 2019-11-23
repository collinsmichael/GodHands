#include "GodHands.h"


int cmpsd(void *dst, void *src, int len) {
    int equal = 0;
    _asm mov edi, [dst]
    _asm mov esi, [src]
    _asm mov ecx, [len]
    _asm rep cmpsd
    _asm jz  y
    _asm mov [equal], 0
    _asm jmp z
y:  _asm mov [equal], 1
z:  return equal;
}

int movsd(void *dst, void *src, int len) {
    _asm mov edi, [dst]
    _asm mov esi, [src]
    _asm mov ecx, [len]
    _asm rep movsd
    return 1;
}

int stosd(void *dst, int val, int len) {
    _asm mov edi, [dst]
    _asm mov eax, [val]
    _asm mov ecx, [len]
    _asm rep stosd
    return 1;
}

int cmpsw(void *dst, void *src, int len) {
    int equal = 0;
    _asm mov edi, [dst]
    _asm mov esi, [src]
    _asm mov ecx, [len]
    _asm rep cmpsw
    _asm jz  y
    _asm mov [equal], 0
    _asm jmp z
y:  _asm mov [equal], 1
z:  return equal;
}

int movsw(void *dst, void *src, int len) {
    _asm mov edi, [dst]
    _asm mov esi, [src]
    _asm mov ecx, [len]
    _asm rep movsw
    return 1;
}

int stosw(void *dst, int val, int len) {
    _asm mov edi, [dst]
    _asm mov eax, [val]
    _asm mov ecx, [len]
    _asm rep stosw
    return 1;
}

int cmpsb(void *dst, void *src, int len) {
    int equal = 0;
    _asm mov edi, [dst]
    _asm mov esi, [src]
    _asm mov ecx, [len]
    _asm rep cmpsb
    _asm jz  y
    _asm mov [equal], 0
    _asm jmp z
y:  _asm mov [equal], 1
z:  return equal;
}

int movsb(void *dst, void *src, int len) {
    _asm mov edi, [dst]
    _asm mov esi, [src]
    _asm mov ecx, [len]
    _asm rep movsb
    return 1;
}

int stosb(void *dst, int val, int len) {
    _asm mov edi, [dst]
    _asm mov eax, [val]
    _asm mov ecx, [len]
    _asm rep stosb
    return 1;
}
