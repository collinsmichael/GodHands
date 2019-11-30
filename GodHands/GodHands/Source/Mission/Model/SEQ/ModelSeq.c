#include "GodHands.h"


extern struct LOGGER Logger;

// 700 x SEQ Files
static int seq_lba[1024];
static int seq_len[1024];
static REC *seq_rec[1024];
static int seq;

static int ModelSeq_Reset(void) {
    stosd(seq_lba, 0, sizeof(seq_lba)/4);
    stosd(seq_len, 0, sizeof(seq_len)/4);
    seq = 0;
    return 1;
}

static int ModelSeq_StartUp(void) {
    return 1;
}

static int ModelSeq_AddSeq(REC *rec) {
    if (seq < elementsof(seq_rec)) {
        seq_lba[seq] = rec->LsbLbaData;
        seq_len[seq] = rec->LsbLenData;
        seq_rec[seq] = rec;
        seq++;
    }
    if (seq >= elementsof(seq_rec)) {
        Logger.Warn("ModelSeq.AddSeq", "Maximum reached of %d/%d SEQ Files", seq, elementsof(seq_rec));
    }
    return Logger.Done("ModelSeq.AddSeq", "Loaded %d/%d", seq, elementsof(seq_rec));
}

struct MODELSEQ ModelSeq = {
    ModelSeq_StartUp,
    ModelSeq_Reset,
    ModelSeq_AddSeq,
};
