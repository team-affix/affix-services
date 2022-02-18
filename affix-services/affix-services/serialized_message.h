#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"

namespace affix_services
{
	/// <summary>
	/// An outbound message serializer type.
	/// </summary>
	/// <typeparam name="MESSAGE_TYPE"></typeparam>
	template<typename MESSAGE_TYPE>
	struct serialized_message
	{
		/// <summary>
		/// The connection from which this message originated.
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_originating_connection;



	};
}
