using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Generalization;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using AffixServices.Communication;
using Aurora.Networking;
using Security;

namespace AffixServices
{
    public static class Main
    {
        public static void StartUp()
        {
            MainServer.Methods.void_Start();
            MainServer.Methods.void_StartMainServer();
        }
    }
}
