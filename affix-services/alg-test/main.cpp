#pragma once
#include <iostream>

#define _WIN32_WINNT 0x0A00
#define ASIO_STANDALONE

#include <asio.hpp>
#include <asio/ts/buffer.hpp>
#include <asio/ts/internet.hpp>
#include <string>

#include <files.h>
#include <modes.h>
#include <osrng.h>
#include <rsa.h>
#include <sha.h>

#include "interpret.h"

using namespace CryptoPP;
using namespace cfvi::interpretation;
//
//void aes_test() {
//
//    AutoSeededRandomPool prng;
//    HexEncoder encoder(new FileSink(std::cout));
//
//    SecByteBlock key(AES::DEFAULT_KEYLENGTH);
//    SecByteBlock iv(AES::BLOCKSIZE);
//
//    prng.GenerateBlock(key, key.size());
//    prng.GenerateBlock(iv, iv.size());
//
//    std::string plain = "CBC Mode Test";
//    std::string cipher, recovered;
//
//    std::cout << "plain text: " << plain << std::endl;
//
//    /*********************************\
//    \*********************************/
//
//    try
//    {
//        CBC_Mode< AES >::Encryption e;
//        e.SetKeyWithIV(key, key.size(), iv);
//
//        StringSource s(plain, true,
//            new StreamTransformationFilter(e,
//                new StringSink(cipher)
//            ) // StreamTransformationFilter
//        ); // StringSource
//    }
//    catch (const Exception& e)
//    {
//        std::cerr << e.what() << std::endl;
//        exit(1);
//    }
//
//    /*********************************\
//    \*********************************/
//
//    std::cout << "key: ";
//    encoder.Put(key, key.size());
//    encoder.MessageEnd();
//    std::cout << std::endl;
//
//    std::cout << "iv: ";
//    encoder.Put(iv, iv.size());
//    encoder.MessageEnd();
//    std::cout << std::endl;
//
//    std::cout << "cipher text: ";
//    encoder.Put((const byte*)&cipher[0], cipher.size());
//    encoder.MessageEnd();
//    std::cout << std::endl;
//
//    /*********************************\
//    \*********************************/
//
//    try
//    {
//        CBC_Mode< AES >::Decryption d;
//        d.SetKeyWithIV(key, key.size(), iv);
//
//        StringSource s(cipher, true,
//            new StreamTransformationFilter(d,
//                new StringSink(recovered)
//            ) // StreamTransformationFilter
//        ); // StringSource
//
//        std::cout << "recovered text: " << recovered << std::endl;
//    }
//    catch (const Exception& e)
//    {
//        std::cerr << e.what() << std::endl;
//        exit(1);
//    }
//
//    return;
//}

//void rsa_test()
//{
//	SecByteBlock key(2048, )
//	RandomNumberGenerator rng;
//	rng.GenerateBlock()
//	InvertibleRSAFunction privateKey;
//	privateKey.GenerateRandomWithKeySize(rng, 3072);
//}

int main() {


	interpreter i = interpreter("./");
	i.process_import("main.cpp");


	asio::error_code ec;
	asio::io_context context;
	asio::ip::tcp::endpoint endpoint(asio::ip::make_address("93.184.216.34", ec), 80);
	asio::ip::tcp::socket socket(context);
	socket.connect(endpoint, ec);

	if (!ec) {
		std::cout << "CONNECTED" << std::endl;
	}
	else {
		std::cout << "ERROR" << std::endl;
	}

	return 0;
}
