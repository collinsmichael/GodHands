
typedef struct EDIT {
    int   Job;
    int   Pos;
    int   Len;
    char *Buf;
} EDIT;

typedef struct ACTOR {
    char *Name;
    int   STR;
    int   INT;
    int   AGL;
} ACTOR;


int ZndActor_Insert(EDIT *edit) {
    return 1;
}

int ZndActor_InsertUndo(EDIT *edit) {
    return 1;
}

int ZndActor_InsertRedo(EDIT *edit) {
    return 1;
}

int ZndActor_Remove(EDIT *edit) {
    return 1;
}

int ZndActor_RemoveUndo(EDIT *edit) {
    return 1;
}

int ZndActor_RemoveRedo(EDIT *edit) {
    return 1;
}

int ZndActor_Modify(EDIT *edit) {
    return 1;
}

int ZndActor_ModifyUndo(EDIT *edit) {
    return 1;
}

int ZndActor_ModifyRedo(EDIT *edit) {
    return 1;
}
