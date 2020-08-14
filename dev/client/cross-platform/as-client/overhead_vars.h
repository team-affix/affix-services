#ifndef OVERHEAD_VARS_H
#define OVERHEAD_VARS_H

#endif // OVERHEAD_VARS_H

#pragma once
#include "overhead_include.h"
#include "overhead_types.h"
#include <QMainWindow>

namespace ot = overhead_types;

namespace overhead_vars{

    inline vector<ot::ptr<QMainWindow>> active_windows = vector<ot::ptr<QMainWindow>>();

}
