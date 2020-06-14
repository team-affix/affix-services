import random
import math
import sys
import time
import io

listcalls = (sys.argv)
i2 = 0
if len(sys.argv) == 5:
    i = int(listcalls[3])
    I = (int(listcalls[1]) ** i) % int(listcalls[2])
    file = open(str(listcalls[4]), "w")
    file.write(str(I))
    file.close()
