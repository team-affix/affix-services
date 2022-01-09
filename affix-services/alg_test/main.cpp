#include "affix-services/connection.h"
#include "affix-base/async_authenticate.h"
#include "affix-base/rsa.h"
#include "affix-base/networking.h"
#include "affix-services/rolling_token.h"

using asio::io_context;
using namespace asio::ip;
using namespace asio;
using affix_base::data::ptr;
using affix_services::security::rolling_token;

std::mutex g_std_cout_mutex;

bool connect_two_sockets(
	io_context& a_context_0,
	io_context& a_context_1,
	const uint16_t& a_port_num,
	tcp::socket& a_socket_0,
	tcp::socket& a_socket_1)
{
	tcp::endpoint l_acceptor_remote_endpoint(make_address("192.168.1.141"), a_port_num);
	tcp::endpoint l_acceptor_local_endpoint(asio::ip::tcp::v4(), a_port_num);

	tcp::socket l_socket_0(a_context_0);
	tcp::acceptor l_acceptor(a_context_1, l_acceptor_local_endpoint);

	bool l_connecting_successful = false;
	bool l_accepting_successful = false;

	l_socket_0.async_connect(l_acceptor_remote_endpoint,
		[&](error_code a_ec)
		{
			std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
			std::cout << "Connecting to acceptor: ";
			std::cout << a_ec.message() << std::endl;
			l_connecting_successful = !a_ec;
			if (l_connecting_successful)
			{
				a_socket_0 = std::move(l_socket_0);
			}
		});
	l_acceptor.async_accept(
		[&](error_code a_ec, tcp::socket a_socket)
		{
			std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
			std::cout << "Accepting connection: ";
			std::cout << a_ec.message() << std::endl;
			l_accepting_successful = !a_ec;
			if (l_accepting_successful)
			{
				a_socket_1 = std::move(a_socket);
			}
		});

	std::thread l_connect_thread([&]{ a_context_0.reset(); a_context_0.run(); });
	std::thread l_accept_thread([&]{ a_context_1.reset(); a_context_1.run(); });

	if (l_connect_thread.joinable())
		l_connect_thread.join();
	if (l_accept_thread.joinable())
		l_accept_thread.join();

	if (!l_connecting_successful || !l_accepting_successful)
		return false;
	else
		return true;

}

/// <summary>
/// This function GENERATES AND POPULATES the following fields:
/// a_local_key_pair, a_local_token, a_remote_public_key, a_remote_token
/// </summary>
/// <param name="a_context"></param>
/// <param name="a_socket"></param>
/// <param name="a_local_key_pair"></param>
/// <param name="a_local_token"></param>
/// <param name="a_remote_public_key"></param>
/// <param name="a_remote_token"></param>
/// <param name="a_authenticate_remote_first"></param>
/// <returns></returns>
bool async_authenticate_connected_socket(
	io_context& a_context,
	tcp::socket& a_socket,
	affix_base::cryptography::rsa_key_pair& a_local_key_pair,
	rolling_token& a_local_token,
	CryptoPP::RSA::PublicKey& a_remote_public_key,
	rolling_token& a_remote_token,
	bool a_authenticate_remote_first
)
{
	// Create Socket IO Guard
	affix_base::networking::socket_io_guard l_socket_io_guard(a_socket);

	// Generate Random Remote Seed
	std::vector<uint8_t> l_remote_seed(affix_services::security::AS_SEED_SIZE);
	CryptoPP::AutoSeededRandomPool l_random;
	l_random.GenerateBlock(l_remote_seed.data(), l_remote_seed.size());

	// Generate Local RSA Key Pair
	affix_base::cryptography::rsa_key_pair l_local_key_pair = affix_base::cryptography::rsa_generate_key_pair(2048);

	// Create Async Authentication Request
	bool l_authenticated_successfully = false;
	affix_base::networking::async_authenticate l_authenticate(
		l_socket_io_guard,
		l_remote_seed,
		l_local_key_pair,
		a_authenticate_remote_first,
		[&](bool a_result)
		{
			l_authenticated_successfully = a_result;
			std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
			std::cout << "Authentication successful." << std::endl;
		});

	a_context.reset();
	a_context.run();

	a_local_key_pair = l_local_key_pair;
	a_local_token = affix_services::security::rolling_token(l_authenticate.m_authenticate_local->m_local_seed);
	a_remote_public_key = l_authenticate.m_authenticate_remote->m_remote_public_key;
	a_remote_token = affix_services::security::rolling_token(l_authenticate.m_authenticate_remote->m_remote_seed);

	return l_authenticated_successfully;

}

bool test_connection_object_send(
	io_context& a_context,
	tcp::socket&& a_socket,
	const CryptoPP::RSA::PrivateKey& a_local_private_key,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token
)
{
	affix_services::networking::connection l_connection(
		std::move(a_socket),
		a_local_private_key,
		a_local_token,
		a_remote_public_key,
		a_remote_token
	);

	std::vector<uint8_t> l_sent_header_data = { 1, 2, 3 };
	std::vector<uint8_t> l_sent_body_data = { 4, 5, 6 };

	bool l_successful = false;

	l_connection.async_send(l_sent_header_data, l_sent_body_data,
		[&](bool a_result)
		{
			l_successful = a_result;
			std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
			if (l_successful)
			{
				std::cout << "Sent successfully" << std::endl;
			}
			else
			{
				std::cout << "Send unsuccessful" << std::endl;
			}
		});

	a_context.reset();
	a_context.run();

	return l_successful;

}

bool test_connection_object_recv(
	io_context& a_context,
	tcp::socket&& a_socket,
	const CryptoPP::RSA::PrivateKey& a_local_private_key,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token
)
{
	affix_services::networking::connection l_connection(
		std::move(a_socket),
		a_local_private_key,
		a_local_token,
		a_remote_public_key,
		a_remote_token
	);

	std::vector<uint8_t> l_recv_header_data;
	std::vector<uint8_t> l_recv_body_data;

	std::vector<uint8_t> l_header_data_expected = { 1, 2, 3 };
	std::vector<uint8_t> l_body_data_expected = { 4, 5, 6 };

	bool l_successful = false;

	l_connection.async_receive(l_recv_header_data, l_recv_body_data,
		[&](bool a_result)
		{
			l_successful = a_result &&
				std::equal(l_recv_header_data.begin(), l_recv_header_data.end(), l_header_data_expected.begin(), l_header_data_expected.end()) &&
				std::equal(l_recv_body_data.begin(), l_recv_body_data.end(), l_body_data_expected.begin(), l_body_data_expected.end());
			std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
			if (l_successful)
			{
				std::cout << "Received successfully" << std::endl;
			}
			else
			{
				std::cout << "Received incorrect information" << std::endl;
			}
		});

	a_context.reset();
	a_context.run();
	
	return l_successful;

}

int main()
{
	io_context l_context_0;
	io_context l_context_1;

	tcp::socket l_socket_0(l_context_0);
	tcp::socket l_socket_1(l_context_1);

	// Try to connect two sockets
	if (!connect_two_sockets(l_context_0, l_context_1, 8090, l_socket_0, l_socket_1))
	{
		std::cout << "Unable to connect two sockets." << std::endl;
		return 1;
	}

	// Prepare necessary security information to be populated
	affix_base::cryptography::rsa_key_pair l_local_key_pair_0;
	rolling_token l_local_token_0;
	CryptoPP::RSA::PublicKey l_remote_public_key_0;
	rolling_token l_remote_token_0;

	// Prepare necessary security information to be populated
	affix_base::cryptography::rsa_key_pair l_local_key_pair_1;
	rolling_token l_local_token_1;
	CryptoPP::RSA::PublicKey l_remote_public_key_1;
	rolling_token l_remote_token_1;

	bool l_authentication_successful_0 = false;
	std::thread l_authenticate_thread_0(
		[&]
		{ 
			if (!async_authenticate_connected_socket(l_context_0, l_socket_0, l_local_key_pair_0, l_local_token_0, l_remote_public_key_0, l_remote_token_0, true))
			{
				std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
				std::cout << "Failed to authenticate remote peer." << std::endl;
			}
			else
			{
				l_authentication_successful_0 = true;
			}
		});

	bool l_authentication_successful_1 = false;
	std::thread l_authenticate_thread_1(
		[&]
		{
			if (!async_authenticate_connected_socket(l_context_1, l_socket_1, l_local_key_pair_1, l_local_token_1, l_remote_public_key_1, l_remote_token_1, false))
			{
				std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
				std::cout << "Failed to authenticate remote peer." << std::endl;
			}
			else
			{
				l_authentication_successful_1 = true;
			}
		});

	if (l_authenticate_thread_0.joinable())
		l_authenticate_thread_0.join();
	if (l_authenticate_thread_1.joinable())
		l_authenticate_thread_1.join();

	if (!l_authentication_successful_0 || !l_authentication_successful_1)
		return 1;

	bool l_connection_transmit_0_successful = false;
	std::thread l_connection_transmit_0(
		[&]
		{
			if (!test_connection_object_send(l_context_0, std::move(l_socket_0), l_local_key_pair_0.private_key, l_local_token_0, l_remote_public_key_0, l_remote_token_0))
			{
				std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
				std::cout << "Failed to send information to the remote peer." << std::endl;
			}
			else
			{
				l_connection_transmit_0_successful = true;
			}
		});

	bool l_connection_transmit_1_successful = false;
	std::thread l_connection_transmit_1(
		[&]
		{
			if (!test_connection_object_recv(l_context_1, std::move(l_socket_1), l_local_key_pair_1.private_key, l_local_token_1, l_remote_public_key_1, l_remote_token_1))
			{
				std::lock_guard<std::mutex> l_lock(g_std_cout_mutex);
				std::cout << "Failed to receive information from the remote peer." << std::endl;
			}
			else
			{
				l_connection_transmit_1_successful = true;
			}
		});

	if (!l_connection_transmit_0_successful || !l_connection_transmit_1_successful)
		return 1;

	return 0;
}
