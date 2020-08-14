#ifndef OVERHEAD_TYPES_H
#define OVERHEAD_TYPES_H

#endif // OVERHEAD_TYPES_H

#pragma once
#include "overhead_include.h"

namespace overhead_types{

    template<class T>
    class ptr : public shared_ptr<T>{
    public:
        ptr(){

        }
        ptr(T *_value){
            shared_ptr<T>::reset(_value);
        }
    };

}
