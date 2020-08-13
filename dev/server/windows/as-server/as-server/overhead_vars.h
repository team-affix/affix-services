#pragma once
#include "overhead_include.h"


/////////// directories

inline const string dir_main = "main\\";
inline const string dir_server = dir_main + "server\\";
inline const string dir_accounts = dir_main + "accounts\\";
inline const string dir_accounts_registered = dir_accounts + "registered\\";
inline const string dir_accounts_banned = dir_accounts + "banned\\";
inline const string dir_machines = dir_main + "machines\\";
inline const string dir_logs = dir_main + "logs\\";

/////////// files

inline string fil_log = dir_logs + "log.dat";
inline ofstream ofs_log;


/////////// threads
inline vector<thread> vector_threads = vector<thread>();