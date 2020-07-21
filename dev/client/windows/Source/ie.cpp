#include "ie.h"

#pragma region appColors
void iAppColorsConfigs() {


	ifstream stream;
	stream.open(dirAppColorsConfigs, ifstream::binary);
	
	while (stream.tellg() != -1) {
		char appColorsConfigsBuff[sizeof(appColors)];
		stream.read(appColorsConfigsBuff, sizeof(appColors));
	}

	char appColorsActiveConfigBuff[sizeof(UINT64)];


}
#pragma endregion
