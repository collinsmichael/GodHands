#ifndef GODHANDS_H
#define GODHANDS_H


#define MS   (1)
#define SEC  (1000*MS)
#define MIN  (60*SEC)
#define HOUR (60*MIN)
#define DAY  (24*HOUR)
#define WEEK (7*DAY)
#define KB   (1024)
#define MB   (1024*KB)
#define GB   (1024*MB)


#define elementsof(x) (sizeof(x)/sizeof(x[0]))
#include "System/System.h"
#include "Mission/Mission.h"


#ifdef UNITTEST
#include "UnitTest/UnitTest.h"
#include "ThirdParty/utest.h" // https://github.com/sheredon/utest.h
#endif


#endif // GODHANDS_H
