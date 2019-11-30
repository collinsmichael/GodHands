#include "GodHands.h"


extern struct LOGGER Logger;

// 512 x MPD Files
static int mpd_lba[1024];
static int mpd_len[1024];
static REC *mpd_rec[1024];
static int mpd;

static int ModelMpd_Reset(void) {
    stosd(mpd_lba, 0, sizeof(mpd_lba)/4);
    stosd(mpd_len, 0, sizeof(mpd_len)/4);
    mpd = 0;
    return 1;
}

static int ModelMpd_StartUp(void) {
    return 1;
}

static int ModelMpd_AddMpd(REC *rec) {
    if (mpd < elementsof(mpd_rec)) {
        mpd_lba[mpd] = rec->LsbLbaData;
        mpd_len[mpd] = rec->LsbLenData;
        mpd_rec[mpd] = rec;
        mpd++;
    }
    if (mpd >= elementsof(mpd_rec)) {
        Logger.Warn("ModelMpd.AddMpd", "Maximum reached of %d/%d MPD Files", mpd, elementsof(mpd_rec));
    }
    return Logger.Done("ModelMpd.AddMpd", "Loaded %d/%d", mpd, elementsof(mpd_rec));
}


struct MODELMPD ModelMpd = {
    ModelMpd_StartUp,
    ModelMpd_Reset,
    ModelMpd_AddMpd,
};
