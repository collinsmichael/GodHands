#ifndef DIALOG_H
#define DIALOG_H


typedef struct DIALOG {
    char *(*OpenFileDialog)(char *filter);
    char *(*SaveFileDialog)(char *filter);
} DIALOG;


#endif // DIALOG_H
