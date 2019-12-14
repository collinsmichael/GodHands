#include "ThirdParty\sqlite3.h"
#include "GodHands.h"


static sqlite3 *db;


static int _cdecl Database_Open(char *filename) {
	return sqlite3_open(filename, &db);
}

static int _cdecl Database_Close(void) {
	return sqlite3_close(db);
}

static void _cdecl Database_Free(void) {
	sqlite3_free(db);
	return;
}

static int _cdecl Database_Exec(const char *sql, int (*callback)(void*,int,char**,char**), void *param, char **errmsg) {
	return sqlite3_exec(db, sql, callback, param, errmsg);
}


DATABASE DataBase = {
	Database_Open,
	Database_Close,
	Database_Free,
	Database_Exec
};
