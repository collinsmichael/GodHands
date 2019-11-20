#ifndef MEMORY_H
#define MEMORY_H

int cmpsd(void *dst, void *src, int len);
int movsd(void *dst, void *src, int len);
int stosd(void *dst, int val, int len);
int cmpsw(void *dst, void *src, int len);
int movsw(void *dst, void *src, int len);
int stosw(void *dst, int val, int len);
int cmpsb(void *dst, void *src, int len);
int movsb(void *dst, void *src, int len);
int stosb(void *dst, int val, int len);

#endif // MEMORY_H
