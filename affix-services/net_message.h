#pragma once
#include "pch.h"
#include "net_message_header.h"

namespace affix_services {
	namespace net_common {
		struct message {
		public:
			message_header header{};
			vector<uint8_t> body;

		public:
			size_t size() const {
				return sizeof(message_header) + body.size();
			}
			friend std::ostream& operator <<(std::ostream& os, const message& msg) {
				os << "ID:" << int(msg.header.id) << " Size:" << msg.header.size;
				return os;
			}
			template<typename DataType>
			friend message& operator <<(message& msg, const DataType& data) {
				static_assert(std::is_standard_layout<DataType>::value, "Data cannot be pushed into the message body.");
				size_t i = msg.body.size();
				msg.body.resize(msg.body.size() + sizeof(DataType));
				std::memcpy(msg.body.data() + i, &data, sizeof(DataType));
				msg.header.size = msg.size();
				return msg;
			}
			template<typename DataType>
			friend message& operator >>(message& msg, DataType& data) {
				static_assert(std::is_standard_layout<DataType>::value, "Data cannot be extracted from the message body.");
				size_t i = msg.body.size() - sizeof(DataType);
				std::memcpy(&data, msg.body.data() + i, sizeof(DataType));
				msg.body.resize(i);
				msg.header.size = msg.size();
				return msg;
			}

		};
	}
}