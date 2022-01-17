#pragma once
#include "affix-base/pch.h"

namespace affix_services {
	namespace messaging {

		enum class message_types {

			unknown = 0,

			rqt_identity_push,		// push identity info
			rsp_identity_push,
			rqt_identity_delete,		// delete identity
			rsp_identity_delete,

			rqt_turn_create,			// create a TURN group
			rsp_turn_create,
			rqt_turn_pull,			// pull TURN group data
			rsp_turn_pull,
			rqt_turn_push,
			rsp_turn_push,
			rqt_turn_delete,
			rsp_turn_delete,

			rqt_relay,				// relay data to connected party
			rsp_relay,

		};

	}
}
