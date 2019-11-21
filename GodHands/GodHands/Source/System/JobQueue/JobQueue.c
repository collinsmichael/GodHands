#include <stdint.h>
#include <windows.h>
#include "System\System.h"


extern struct LOGGER Logger;


static volatile uint8_t head;
static volatile uint8_t tail;
static int (*queue[256])(void *param);
static void *param[256];
static void *handle[256];
static void *broker;

static DWORD WINAPI Worker(void *arg) {
    int job = (int)arg;
    if (!queue[job]) {
        return Logger.Fail("JobQueue.Worker", "Job not found");
    }
    queue[job](param[job]);
    queue[job] = 0;
    param[job] = 0;
    handle[job] = 0;
    return Logger.Pass("JobQueue.Worker", "Job %02X complete", job);
}

static DWORD WINAPI Broker(LPVOID arg) {
    for (;;Sleep(0)) {
        if (head == tail) continue;
        tail++;
        handle[tail] = CreateThread(0,0,Worker,(void*)tail,0,0);
        if (!handle[tail]) {
            Logger.Fail("JobQueue.StartUp", "Job Queue failure");
        }
    }
    return Logger.Pass("JobQueue.Schedule", "Job Queue cleaned up");
}

static int JobQueue_Schedule(int(*callback)(void*), void *args) {
    uint8_t next = head + 1;
    if (next != tail) {
        if (!callback) {
            return Logger.Fail("JobQueue.Schedule", "Invalid parameters");
        }
        param[next] = args;
        queue[next] = callback;
        head = next;
        Sleep(0);
        return Logger.Pass("JobQueue.Schedule", "Job %02X scheduled", head);
    }
    return Logger.Fail("JobQueue.Schedule", "Queue full, try again later");
}

static int JobQueue_StartUp(void) {
    broker = CreateThread(0,0,Broker,0,0,0);
    if (!broker) {
        return Logger.Fail("JobQueue.StartUp", "Job Queue failure");
    }
    return Logger.Pass("JobQueue.StartUp", "Job Queue started");
}

static int JobQueue_CleanUp(void) {
    int pos;
    if (broker) CloseHandle(broker);
    broker = 0;
    for (pos = 0; pos < 256; pos++) { 
        if (handle[pos]) CloseHandle(handle[pos]);
    }
    return Logger.Pass("JobQueue.CleanUp", "Job Queue stopped");
}


struct JOBQUEUE JobQueue = {
    JobQueue_StartUp,
    JobQueue_CleanUp,
    JobQueue_Schedule,
};
