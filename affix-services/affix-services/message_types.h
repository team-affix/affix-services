#pragma once
#include "affix-base/pch.h"

namespace affix_services {
	namespace messaging {

		enum class message_types {

			unknown = 0,

			identity_authenticate_0,			// seed remote peer
			identity_authenticate_1,			// peer signs token (seed + index) with private key
			identity_authenticate_2,			// verify token signature

			identity_pull_0,		// pull identity info
			identity_pull_1,		
			identity_push_0,		// push identity info
			identity_push_1,
			identity_delete_0,		// delete identity
			identity_delete_1,

			turn_create_0,			// create a TURN group
			turn_create_1,
			turn_pull_0,			// pull TURN group data
			turn_pull_1,
			turn_push_0,
			turn_push_1,
			turn_delete_0,
			turn_delete_1,

			relay_0,				// relay data to connected party
			relay_1,

		};

	}
}
