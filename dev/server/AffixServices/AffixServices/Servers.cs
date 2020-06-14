using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AffixServices.Communication;
using Aurora.Networking;
using Aurora.Generalization;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using Security;
using System.IO;
using System.Reflection;
using AffixServices.Data;

namespace AffixServices
{
    public unsafe static class MainServer
    {
        public static class Vals
        {
            public static bool bool_ServerActive;
            public static bool bool_ProcessingAccounts;
            public static bool bool_ProcessingMachines;
            public static string string_PrivateKey = "<RSAKeyValue><Modulus>4igTaAPOT3l6JrsWHVN5hpOEsKnFYVUKHwJYwcQOCC980QXLW9ogdst0u04LjftZrT7uOx0g82UimO2qXSwVvBOa5A8mptOewj9H3M58qYp34ZBtaX/9gZt9jBX/5g9B96DV3s2Kd8C3AHtKjvaOg2VOK0qad/RhXQEQJM/1mC78Dx9miFNVVcXfIhSvzggIyvGarXusNtTgKdR2VmI7FMP2N8LisNYBGnBmI00daGdZn8SDO7NiA7CeM4/nmj5MFIcox3l0larnkXhYE0eM550TrZ8phbZvaMOn0nfjT4YVkhfd6ECz+TtVxxVZNHswxiCiTtCQ3qotj19BqxUK8lGBxqlLTmMI6nNUUGN8EwrsVSwm7hVkmsfKnnFKZ+U6GbAmPgyDe/4t+gz9emG/7TxAGr/ln/P/hYRNChuLTUA4JpuK6LFw+57qxvSb75EOo5Q6Zl2ORxHWQ+gPrnkRPk8rQL46RLweUeeE8Wnli7yFxAqiaIHhZbWBWGiwci2xcSjfWRrBcRe9W6j1hxCDS9Jf1sn6sIhnwlDr2CEBPI73ikZAV88yLSUUWRSaPWIIpSI+spNd6NWN2arR7dFvaoAv+3kXY7eM/mt4kGscuSlvnjwxa5pLXlEmkcCOizy6bNs6vblqw1ddCgwhrJzkMf/iA6XIldhMU7+u7enOGYk=</Modulus><Exponent>AQAB</Exponent><P>8/RH4FZwFzXahfTNyPVT8oFG1+RBd8d3KifdSJHG6A0ddA+Vu4Ovjle1XVdbVBIUWN/eYWwn2DSrn1dPil8oYujo/UuYw+na4bgRrvyVvFEWq8XJ3IsIbsepG8yvDE2eDL6U8X4kYKm6SIykr4Q1akmZpRTyoK5ZE+LB3zbV4nF/E7x/cqvzeWSDDPzhcSVJPuH+yTcJeC4cP9u29LNLyruSuiaMyR1rt/xY700lahA8QDI+H1I2PAb8zVN9WTNar50ac0xBjn4DnOoFgFhpnJLLSGRgoVCcUirPhbNZIq0ZkWVPUhQ4Q0Rjc+5o4YfOjgaIxaTZWb8G2W2dLiymIw==</P><Q>7VLShjvq9XIteaEnVywAtNuhihIxhEThxU4PuRG/KQwC4tIzeK+q54l4QFhDBPP+bfkcxbHBlvIFHdn5zGfG/9mo4I41HjqK2JKYbvFVxdPKuyt0eEXGw70Gz4rdQuWfQvGCR7ecSA/egqwgL6gJoUTcXsU5dFGniFwAMvPQ4mZVXlMAtRmWud1lebFnMsWvMCh8wbNEwWHKgwk3kXRl4IM/4lRg3CzyFxVhdYzgptbty+EBpv1Fy25zdvJwq3XqbHVGcPXnNCELYRUmsfjkHM2+L1jFQ/uxeAkPa4INZSwPhH9UkKRJRIfPGt9D6rLVQi5Rm7pgEuI2TMP3jmNeYw==</Q><DP>rVzim0UyAPyPX3S5rN7SmlpOauvVrnY8c+PCeGjILm3riWft40TQxFoGE+AZkeQLO6FT2m3O3vGHKQQWDWJUQ/36XM6VMFyG5LM3NeBf1GTXtJgpPKY9BuSyg4PwvdCGyc3B8N/QoDTdoeL4wZY1k2PcYRlC1vcvbFdPqP3B/1rikLb65xXVP2YzT06CMzLpG1vlAT8wZox2HNdZ7Aliih7ERa9RizZyL1yQIvby6K3C82gBB6cXzY61poB3aqV5A6Wkf10olUdt79vKrwuledRDzB0xIlIz1JyBvyTV6jOTIOUa+lxOI1d9SYyaUfN90SZJmhLtNr7u8iLYeVDXew==</DP><DQ>QY0c5ULrlTwiX3Qp36Qh1dLMfcykrLox+kOjiCTkvju5GFmjKfSrqiSIeI1ohZe3cTzbu6drp3vV+fbZTqJjjwvZ35yoA198NnHXdN1oKapkVuqb0xTC4BH4LZ9XmkC+iskWiSSoICdx+Hn+sesiLc9NZ0fmpMBhHAL9cxVxXveRIOqgSWV2AoSqoMBlltqPlye3vfD8TsXqH+ON27lg4nCeVq+jyW0IuWOIdeWhr7OyHxxZTnyqqJQrhHFWuaVqSObjvBPP9tAhyoEDHSl4KJSTsZQ9eJeu307xWWqktItFBf39p64ZJrq0c+GzasJWsV8F35PNMaoWAYIngMblrQ==</DQ><InverseQ>Ow0B7ATzRdsnudoWoQNlNeo4RH8WUuZRWo0nDI2dKqiqgu9OmLZomNr37lRbmFrfOxV5r2JjT3N7s3AO5ItBTXSEH+WCZzlYGvaACKIO1cvKSyHNF4cl4LaAssqmhhUDeIPdj92d/j38H1ec4Yln8h3hYBof28YtXNF+hDAkKohrdtZ4Yq73Ra0r2icYzLXcj6KXk5F0OqLeLFxUuxmm+pX4Pakg12c3ZU+hnbZNQtu844Umw9CVr6/Eba/gpsiBTsjzPBciSmmigmYn7OTRdFQ2tNOwkbTo6FXVpesIussnUeE0/GFo8B86VjNZQPHfffp2cg1IQr3Sld9ezK7IwQ==</InverseQ><D>DCdfM53QvpCmseIoBBdyVnsjDGNzShHJ43S61F726hWzPYB0N1F3oSq58T72fADgBuF06BReqvXqcBGFsFAoMt1S26K8zwZcZ9ACyqkGJg5c9vBOEPdh5k825gpgoDp9rxMjqz+ci2b97raxgRFbA1C0bLsrpfqqKNdfyLqsVitgPGfRmM3xrhReM3StJgNkhZOKXnxg+ulhyp2yZl4NEsadOs3sZNh7YrJbqtYAGJZFSMLE0raHM++P8PFw3ucuK4Q/+shaLZjrrPOsuAyGVAQrBI+jKZ5uSlEJedQbkTCBhEJoGI0xntlDeNl628krC9rBHj8zKeQ3YpcZ7lnJJrbBUHSdf+ZKeqEBv+HUxJqRIKWVCWiJR7CajJaVf1lpFD1p4mWlmMe8aNgqdQK7CjR5AEezo+lrxbOVmDx62RiHsyCmnew6D2wj9qjK3Uj34wmP/ujJVeHYfn18X8mkNqWENu0BGx/3r8MHjPU/lBGGmNjgde7cBhTfcTjhI9dUqolgjiYwx23L/JoA/ifT39dcxPSUqtPV4eS2FmPOARrP2H71Lwllfcz/gwuq5s4jYHwGhbrjZZGF2lQ6fdrBY00IEkChXPPAvbdCaNJsjC+Pf+w223AorPiznkxqXACkgoHeaLETD1paTFpd40eRdSS2x5r3lpHqteI1zE8UIQk=</D></RSAKeyValue>";
            public static string string_PublicKey = "<RSAKeyValue><Modulus>4igTaAPOT3l6JrsWHVN5hpOEsKnFYVUKHwJYwcQOCC980QXLW9ogdst0u04LjftZrT7uOx0g82UimO2qXSwVvBOa5A8mptOewj9H3M58qYp34ZBtaX/9gZt9jBX/5g9B96DV3s2Kd8C3AHtKjvaOg2VOK0qad/RhXQEQJM/1mC78Dx9miFNVVcXfIhSvzggIyvGarXusNtTgKdR2VmI7FMP2N8LisNYBGnBmI00daGdZn8SDO7NiA7CeM4/nmj5MFIcox3l0larnkXhYE0eM550TrZ8phbZvaMOn0nfjT4YVkhfd6ECz+TtVxxVZNHswxiCiTtCQ3qotj19BqxUK8lGBxqlLTmMI6nNUUGN8EwrsVSwm7hVkmsfKnnFKZ+U6GbAmPgyDe/4t+gz9emG/7TxAGr/ln/P/hYRNChuLTUA4JpuK6LFw+57qxvSb75EOo5Q6Zl2ORxHWQ+gPrnkRPk8rQL46RLweUeeE8Wnli7yFxAqiaIHhZbWBWGiwci2xcSjfWRrBcRe9W6j1hxCDS9Jf1sn6sIhnwlDr2CEBPI73ikZAV88yLSUUWRSaPWIIpSI+spNd6NWN2arR7dFvaoAv+3kXY7eM/mt4kGscuSlvnjwxa5pLXlEmkcCOizy6bNs6vblqw1ddCgwhrJzkMf/iA6XIldhMU7+u7enOGYk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            public static string string_StorageKey = "1jFlYV)WOXaf3n5ZnfvoS1T98ZkZO&V2nzFPcYudB!5FO6@Or!*ia5baJQx&LqhOct$D3oXJ5s8W!ouc5QITbN2CxEfFBvLk9qDv$L6crJx2(zfvvvo!3IMILKfxjriI2W9Y6&js5GBMpaF*z4kfgQCzY@4CFI$&B9auKAKvl!kwhatLfh*zhcCkuJa^p27vNBlA&kH&t9@kFgsXVswyKM$yG^abzTfEi03Z0$L!U*fsirYoqwp(EI6bI7@jkGTU3JbQyM931BYLGTqol65cDi&Ud2qc(C5nzFKSpi^XIGrc5tO0QZ*yVRqUKBMQw75yRKmbjS*c!hO(%)04mBGrDGM3p^i9PtBVW$FuhN1*2!DOct4NM3D@zM@k(D6tJkHWZ$u*LOWTVIKId*Z)gPw70Gvj*uxY2HIRI19k*5c(g5t%cFh*Y0m@8U7sQl&qr($n&#awjP(vNz1fmEhMny&%vN48#QN756Zi%f%iF60j$^BI@2fmeHd#";
            public static int int_TimerUpdateIndex;
            public static Random Random = new Random(DateTime.Now.Millisecond + DateTime.Now.Hour - DateTime.Now.Year + DateTime.Now.Day);
            public static Timer Timer_Update;
            public static Thread Thread_EstablishConnections = new Thread(() =>
            {
                while (MainServer.Vals.bool_ServerActive)
                {
                    MainServer.Vals.Socket.Listen(0);
                    try
                    {
                        Socket s = MainServer.Vals.Socket.Accept();
                        MainServerProcessing.Methods.void_InitTransceiver(s);
                    }
                    catch (SocketException se) { }
                }
            });
            public static List<Transceiver> List_ConnectedTransceivers = new List<Transceiver> { };
            public static Dictionary<Transceiver, TransceiverAccess> Dictionary_TransceiverAccesses = new Dictionary<Transceiver, TransceiverAccess> { };
            public static Socket Socket;
            public static List<Account> List_Account = new List<Account> { };
            public static List<Account> List_AccountPrevious = new List<Account> { };
            public static List<Machine> List_Machine = new List<Machine> { };
            public static List<Machine> List_MachinePrevious = new List<Machine> { };
            public static Action Action_TimerUpdateCallBack = () => { };
            public static Action Action_ListAccountChanged = () => { };
            public static Action Action_ListMachineChanged = () => { };
        }

        public static class Methods
        {
            public static void void_Start()
            {
                #region Directories
                string[] Directories = new string[]
                {
                    "data\\",
                    "data\\accounts",
                    "data\\machines",
                };
                for(int i = 0; i < Directories.Length; i++)
                {
                    if (!Directory.Exists(Directories[i]))
                    {
                        Directory.CreateDirectory(Directories[i]);
                    }
                }
                #endregion
                void_InitVals();
            }
            public static void void_InitVals()
            {
                #region Load
                string[]
                    AccountDirectories = Directory.GetDirectories("data\\accounts"),
                    MachineDirectories = Directory.GetDirectories("data\\machines");

                for (int i = 0; i < AccountDirectories.Length; i++)
                {
                    string FileLocation = AccountDirectories[i] + "\\account.dll";
                    Account Account = T_ImportFile<Account>(FileLocation);
                    Vals.List_Account.Add(Account);
                }
                for (int i = 0; i < MachineDirectories.Length; i++)
                {
                    string FileLocation = MachineDirectories[i] + "\\machine.dll";
                    Machine Machine = T_ImportFile<Machine>(FileLocation);
                    Vals.List_Machine.Add(Machine);
                }

                Vals.List_AccountPrevious = Vals.List_Account.Clone();
                Vals.List_MachinePrevious = Vals.List_Machine.Clone();
                #endregion
                #region Timer
                Vals.Action_TimerUpdateCallBack += void_RefreshCallBackConditions;
                Vals.Action_ListAccountChanged += void_ProcessAccountChanges;
                Vals.Action_ListMachineChanged += void_ProcessMachineChanges;
                Vals.Timer_Update = new Timer(new TimerCallback(void_TimerUpdateCallBack), null, 0, 1);
                #endregion
            }


            public static void void_TimerUpdateCallBack(object state)
            {
                Vals.Action_TimerUpdateCallBack();
            }
            public static void void_RefreshCallBackConditions()
            {
                if(!Vals.List_Account.ToByte().SequenceEqual(Vals.List_AccountPrevious.ToByte()) && !Vals.bool_ProcessingAccounts)
                {
                    Vals.bool_ProcessingAccounts = true;
                    Vals.Action_ListAccountChanged();
                    Vals.bool_ProcessingAccounts = false;
                }

                if(!Vals.List_Machine.ToByte().SequenceEqual(Vals.List_MachinePrevious.ToByte()) && !Vals.bool_ProcessingMachines)
                {
                    Vals.bool_ProcessingMachines = true;
                    Vals.Action_ListMachineChanged();
                    Vals.bool_ProcessingMachines = false;
                }
            }


            public static void void_StartMainServer()
            {
                Vals.bool_ServerActive = true;
                Vals.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Vals.Socket.Bind(new IPEndPoint(IPAddress.Any, 8090));
                Vals.Thread_EstablishConnections.Start();
            }
            public static void void_StopMainServer()
            {
                Vals.bool_ServerActive = false;
                Vals.Socket.Close();
                Vals.List_ConnectedTransceivers.ForEach(x => { x.Close(); });
                Vals.List_ConnectedTransceivers.Clear();
                Vals.Thread_EstablishConnections.Abort();
            }


            public static void void_ProcessAccountChanges()
            {
                for (int i = 0; i < Vals.List_Account.Count; i++)
                {
                    if (!Vals.List_AccountPrevious.Contains(Vals.List_Account[i]))
                    {
                        string AccountDirectory = "data\\accounts\\" + Vals.List_Account[i].string_IdentificationID;
                        if (!Directory.Exists(AccountDirectory))
                        {
                            Methods.void_InitAccountDirectory(Vals.List_Account[i].string_IdentificationID);
                        } // New account
                        Methods.void_ExportFile(AccountDirectory + "\\account.dll", Vals.List_Account[i]);
                    }
                } // Check for additions or changes

                for (int i = 0; i < Vals.List_AccountPrevious.Count; i++)
                {
                    string ID = Vals.List_AccountPrevious[i].string_IdentificationID;
                    if (Vals.List_Account.Find(Account => Account.string_IdentificationID == ID).Equals(default(Account)))
                    {
                        Methods.void_RemoveAccountDirectory(ID);
                    }
                } // Check for removals
                Vals.List_AccountPrevious = Vals.List_Account.Clone();
            }
            public static void void_ProcessMachineChanges()
            {
                for (int i = 0; i < Vals.List_Machine.Count; i++)
                {
                    if (!Vals.List_MachinePrevious.Contains(Vals.List_Machine[i]))
                    {
                        string MachineDirectory = "data\\machines\\" + Vals.List_Machine[i].string_IdentificationID;
                        if (!Directory.Exists(MachineDirectory))
                        {
                            Methods.void_InitMachineDirectory(Vals.List_Machine[i].string_IdentificationID);
                        } // New account
                        Methods.void_ExportFile(MachineDirectory + "\\machine.dll", Vals.List_Machine[i]);
                    }
                } // Check for additions or changes

                for (int i = 0; i < Vals.List_MachinePrevious.Count; i++)
                {
                    string ID = Vals.List_MachinePrevious[i].string_IdentificationID;
                    if (Vals.List_Machine.Find(Machine => Machine.string_IdentificationID == ID).Equals(default(Machine)))
                    {
                        Methods.void_RemoveMachineDirectory(ID);
                    }
                } // Check for removals
                Vals.List_MachinePrevious = Vals.List_Machine.Clone();
            }


            public static string string_GenerateAccountID()
            {
                return Crypt.RandomString_AlphaNumeric(15, MainServer.Vals.Random);
            }
            public static string string_GenerateMachineID()
            {
                return Crypt.RandomString_AlphaNumeric(20, MainServer.Vals.Random);
            }


            public static Machine Machine_InitMachine(string Name, string PublicKey)
            {
                string
                    MachineID = string_GenerateMachineID(),
                    MachineName = Name,
                    MachinePublicKey = PublicKey;
                bool
                    IsBanned = false,
                    IsVerified = false,
                    IsOnline = false,
                    IsActive = false,
                    bool_ConfigurationUseAssociation2FA = false;
                NotificationMethod 
                    NotificationMethod_SuspiciousActivity = new NotificationMethod { Email = true };
                List<Contract>
                    List_AccountAuth = new List<Contract> { };
                List<Server>
                    List_Server = new List<Server> { };

                Machine Machine = new Machine(MachineID, MachineID, PublicKey, IsBanned, IsVerified, IsOnline, bool_ConfigurationUseAssociation2FA, NotificationMethod_SuspiciousActivity, List_AccountAuth, List_Server);
                return Machine;
            }
            public static Account Account_InitAccount(string Name, string EmailAddress, IFASecurity MainIFASecurity, IFASecurity BackupIFASecurity, Contract AuthenticatedMachine)
            {
                string
                    AccountID = string_GenerateAccountID(),
                    AccountName = Name,
                    AccountEmailAddress = EmailAddress;
                bool
                    AccountIsBanned = false,
                    AccountIsVerified = false,
                    AccountIsOnline = false,
                    bool_ConfigurationUseAssociation2FA = false;
                List<Contract>
                    List_ContractMachineAuth = new List<Contract> { AuthenticatedMachine },
                    List_ContractServerP2P = new List<Contract> { },
                    List_ContractAccountP2P = new List<Contract> { };
                List<P2P>
                    List_ConnectionServer = new List<P2P> { },
                    List_ConnectionAccount = new List<P2P> { };
                List<IFASecurity>
                    List_IFASecurities = new List<IFASecurity> { MainIFASecurity, BackupIFASecurity };
                List<string>
                    List_PermanentAccessTokens = new List<string> { };

                Account Account = new Account(AccountID, AccountName, EmailAddress, AccountIsBanned, AccountIsVerified, AccountIsOnline, bool_ConfigurationUseAssociation2FA, List_ContractMachineAuth, List_ContractServerP2P, List_ContractAccountP2P, List_ConnectionServer, List_ConnectionAccount, List_IFASecurities, List_PermanentAccessTokens);
                return Account;
            }


            public static void void_InitMachineDirectory(string ID)
            {
                string MachineDirectory = "data\\machines\\" + ID;
                Directory.CreateDirectory(MachineDirectory);
            }
            public static void void_InitAccountDirectory(string ID)
            {
                string AccountDirectory = "data\\accounts\\" + ID;
                Directory.CreateDirectory(AccountDirectory);
            }


            public static void void_RemoveMachineDirectory(string ID)
            {

            }
            public static void void_RemoveAccountDirectory(string ID)
            {

            }


            public static void void_GenerateAccessToken(string publicKey, out string AccessToken, out byte[] EncryptedAccessToken)
            {
                AccessToken = Crypt.RandomString_AlphaNumeric(30, MainServer.Vals.Random);
                EncryptedAccessToken = Crypt.EncryptRSA(publicKey, AccessToken.ToByte());
            }


            public static T T_ImportFile<T>(string FileName)
            {
                return Crypt.DecryptECB(File.ReadAllBytes(FileName), MainServer.Vals.string_StorageKey.ToByte()).To<T>();
            }
            public static void void_ExportFile<T>(string FileName, T Object)
            {
                byte[] data = Object.ToByte();
                File.WriteAllBytes(FileName, Crypt.EncryptECB(data, MainServer.Vals.string_StorageKey.ToByte()));
            }
        }
    }

    public struct TransceiverAccess
    {
        public Machine Machine;
        public Account Account;
    }
}
