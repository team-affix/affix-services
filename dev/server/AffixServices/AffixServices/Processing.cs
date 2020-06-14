using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Generalization;
using Aurora.Sequencing;
using System.Net.Sockets;
using AffixServices.Communication;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Security;
using System.Net;
using AffixServices.Data;

namespace AffixServices
{
    public static class MainServerProcessing
    {
        public static class Vals
        {

        }

        public static class Methods
        {
            public static void void_NOAUTHPROCESS(Transceiver Transceiver, Discourse Discourse, Transmission Transmission)
            {
                string FunctionName = (string)Transmission["function_name"];
                if (FunctionName == "request_get_seed_key_pair_seed")
                {
                    #region Import
                    string MachinePublicKey = (string)Transmission["machine_public_key"];
                    #endregion
                    #region Flags
                    bool[] Flags = new bool[] { };
                    #endregion
                    #region Process
                    Transceiver.Security.SeedKeyPair = new SeedKeyPair(Crypt.RandomString_AlphaNumeric(20, MainServer.Vals.Random), x =>
                    {
                        byte[] NewVal = (Transceiver.Security.SeedKeyPair.Seed + x).ToByte();
                        return Crypt.EncryptECB(NewVal, NewVal);
                    });

                    Discourse.HandleTransmission = void_SKPAUTHPROCESS;

                    Transmission Response = new Transmission(Transmission.DiscourseID);
                    Response.Add("function_name", "response_get_seed_key_pair_seed");
                    Response.Add("function_flags", Flags);
                    Response.Add("seed_key_pair_seed", Transceiver.Security.SeedKeyPair.Seed);
                    Transceiver.SendTransmission(Response, MachinePublicKey);
                    #endregion
                }
            }
            public static void void_SKPAUTHPROCESS(Transceiver Transceiver, Discourse Discourse, Transmission Transmission)
            {
                string FunctionName = (string)Transmission["function_name"];
                if (FunctionName == "request_register_machine")
                {
                    #region Import
                    string
                        MachineName = (string)Transmission["machine_name"],
                        MachinePublicKey = (string)Transmission["machine_public_key"];

                    #endregion
                    #region Flags
                    bool
                        MachineNameExists = MachineName != null && MachineName != "",
                        MachinePublicKeyExists = MachinePublicKey != null && MachinePublicKey != "";

                    bool[] Flags = new bool[]
                    {
                        !MachineNameExists,
                        !MachinePublicKeyExists
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true) && MachinePublicKeyExists)
                    {
                        Transmission ErrorResponse = new Transmission(Transmission.DiscourseID);
                        ErrorResponse.Add("function_name", "response_register_machine");
                        ErrorResponse.Add("function_flags", Flags);
                        Transceiver.SendTransmission(ErrorResponse, MachinePublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Machine Machine = MainServer.Methods.Machine_InitMachine(MachineName, MachinePublicKey);
                        MainServer.Vals.List_Machine.Add(Machine);

                        Transmission Response = new Transmission(Transmission.DiscourseID);
                        Response.Add("function_name", "response_register_machine");
                        Response.Add("function_flags", Flags);
                        Response.Add("machine_id", Machine.string_IdentificationID);
                        Transceiver.SendTransmission(Response, MachinePublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_get_machine_access_token")
                {
                    #region Import
                    string
                        MachineID = (string)Transmission["machine_id"],
                        MachineAccessToken = null; try { MachineAccessToken = (string)Transceiver.DataBoard["machine_access_token"]; } catch (KeyNotFoundException ex) { }

                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.List_Machine.Find(m => m.string_IdentificationID == MachineID);

                    bool
                        MachineExists = Machine != null,
                        MachineAccessTokenDoesNotExist = MachineAccessToken == null;

                    bool[] Flags = new bool[]
                    {
                    !MachineAccessTokenDoesNotExist
                    };

                    #endregion
                    #region Process
                    if (MachineExists)
                    {
                        if (Flags.Contains(true))
                        {
                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_get_machine_access_token");
                            Response.Add("function_flags", Flags);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                        }
                        if (!Flags.Contains(true))
                        {
                            MachineAccessToken = null;
                            byte[] EncryptedMachineAccessToken = null;
                            MainServer.Methods.void_GenerateAccessToken(Machine.string_AuthenticationPublicKey, out MachineAccessToken, out EncryptedMachineAccessToken);
                            Transceiver.DataBoard.Add("machine_id", Machine.string_IdentificationID);
                            Transceiver.DataBoard.Add("machine_access_token", MachineAccessToken);

                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_get_machine_access_token");
                            Response.Add("function_flags", Flags);
                            Response.Add("machine_access_token", EncryptedMachineAccessToken);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                        }
                    }
                    #endregion
                }
                if (FunctionName == "request_authenticate_machine")
                {
                    #region Import
                    string
                        MachineAccessTokenTest = (string)Transmission["machine_access_token"],
                        MachineID = (string)Transceiver.DataBoard["machine_id"],
                        MachineAccessTokenValid = (string)Transceiver.DataBoard["machine_access_token"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.List_Machine.Find(m => m.string_IdentificationID == MachineID);
                    bool
                        MachineAccessTokenTestExists = MachineAccessTokenTest != null,
                        MachineAccessTokenValidExists = MachineAccessTokenValid != null,
                        MachineIDExists = MachineID != null,
                        MachineExists = Machine != null,
                        isMachineAccessTokenValid = MachineAccessTokenTestExists && MachineAccessTokenValidExists && MachineAccessTokenTest == MachineAccessTokenValid;

                    bool[] Flags = new bool[]
                    {
                    !MachineAccessTokenTestExists,
                    !MachineAccessTokenValidExists,
                    !MachineIDExists,
                    !MachineExists,
                    !isMachineAccessTokenValid
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Response = new Transmission(Transmission.DiscourseID);
                        Response.Add("function_name", "response_authenticate_machine");
                        Response.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Transceiver.DataBoard.Remove("machine_access_token");
                        Transceiver.DataBoard.Remove("machine_id");

                        TransceiverAccess transceiverAccess = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver];
                        transceiverAccess.Machine = Machine;
                        MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver] = transceiverAccess;

                        Transmission Response = new Transmission(Transmission.DiscourseID);
                        Response.Add("function_name", "response_authenticate_machine");
                        Response.Add("function_flags", Flags);

                        Discourse.HandleTransmission = void_MACHINEAUTHPROCESS;

                        Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
            }
            public static void void_MACHINEAUTHPROCESS(Transceiver Transceiver, Discourse Discourse, Transmission Transmission)
            {
                string FunctionName = (string)Transmission["function_name"];
                if (FunctionName == "request_streamget_machine")
                {
                    #region Import
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;

                    bool[] Flags = new bool[]
                    {

                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_streamget_machine");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_streamget_machine");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);

                        DateTime LastStreamed = DateTime.Now;
                        Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("machine", Machine);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                        byte[] PreviousMachineByte = Machine.ToByte();
                        Action StreamOut = null;
                        StreamOut = () =>
                        {
                            if(Transceiver.Socket.Connected && DateTime.Now.Subtract(LastStreamed).TotalSeconds > 3)
                            {
                                byte[] MachineByte = Machine.ToByte();
                                if (!PreviousMachineByte.SequenceEqual(MachineByte))
                                {
                                    Result = new Transmission(Transmission.DiscourseID);
                                    Result.Add("machine", Machine);
                                    Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                                    PreviousMachineByte = MachineByte;
                                }
                                LastStreamed = DateTime.Now;
                            }
                            else if (!Transceiver.Socket.Connected)
                            {
                                MainServer.Vals.Action_TimerUpdateCallBack -= StreamOut;
                            }
                        };
                        MainServer.Vals.Action_TimerUpdateCallBack += StreamOut;
                    }
                    #endregion
                }
                if (FunctionName == "request_register_account")
                {
                    #region Import
                    string
                        AccountName = (string)Transmission["account_name"],
                        EmailAddress = (string)Transmission["email_address"],
                        IFAPublicKey = (string)Transmission["ifa_public_key"],
                        IFABackupPublicKey = (string)Transmission["ifa_backup_public_key"];
                    byte[]
                        IFAPrivateKey = (byte[])Transmission["ifa_private_key"],
                        IFABackupPrivateKey = (byte[])Transmission["ifa_backup_private_key"];
                    Machine
                        Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;

                    #endregion
                    #region Flags
                    Account AccountByName = MainServer.Vals.List_Account.Find(a => a.string_IdentificationName == AccountName);
                    Account AccountByEmail = MainServer.Vals.List_Account.Find(a => a.string_IdentificationEmailAddress == EmailAddress);
                    bool AccountNameNotTaken = AccountByName == null;
                    bool AccountEmailNotTaken = AccountByEmail == null;
                    bool MachineExists = Machine != null;

                    bool[] Flags = new bool[]
                    {
                    !AccountNameNotTaken,
                    !AccountEmailNotTaken,
                    !MachineExists
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true) && MachineExists)
                    {
                        Transmission Response = new Transmission(Transmission.DiscourseID);
                        Response.Add("function_name", "response_register_account");
                        Response.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Account Account = MainServer.Methods.Account_InitAccount(AccountName, EmailAddress, new IFASecurity(IFAPublicKey, IFAPrivateKey), new IFASecurity(IFABackupPublicKey, IFABackupPrivateKey), new Contract(true, true, Machine.string_IdentificationID, Machine.string_IdentificationName));
                        MainServer.Vals.List_Account.Add(Account);

                        Transmission Response = new Transmission(Transmission.DiscourseID);
                        Response.Add("function_name", "response_register_account");
                        Response.Add("function_flags", Flags);
                        Response.Add("account_id", Account.string_IdentificationName);
                        Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_get_account_access_token")
                {
                    #region Import
                    string
                        EmailAddress = (string)Transmission["account_email_address"],
                        AccessToken = null; try { AccessToken = (string)Transceiver.DataBoard["account_access_token"]; } catch (KeyNotFoundException ex) { }

                    #endregion
                    #region Flags
                    Account Account = MainServer.Vals.List_Account.Find(a => a.string_IdentificationEmailAddress == EmailAddress);
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    bool AccountExists = Account != null;
                    bool MachineExists = Machine != null;
                    bool TransceiverAccessTokenDoesNotExist = AccessToken == null;

                    bool[] Flags = new bool[]
                    {
                        !AccountExists,
                        !MachineExists,
                    };
                    #endregion
                    #region Process
                    if (MachineExists)
                    {
                        byte[] EncryptedAccessToken = null;
                        if (Flags.Contains(true))
                        {
                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_get_account_access_token");
                            Response.Add("function_flags", Flags);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                        }
                        if (!Flags.Contains(true))
                        {
                            MainServer.Methods.void_GenerateAccessToken(Account.List_IFASecurities[0].IFAPublicKey, out AccessToken, out EncryptedAccessToken);
                            if (!TransceiverAccessTokenDoesNotExist) 
                            {
                                Transceiver.DataBoard.Remove("account_access_token");
                                Transceiver.DataBoard.Remove("account_id");
                            }
                            Transceiver.DataBoard.Add("account_access_token", AccessToken);
                            Transceiver.DataBoard.Add("account_id", Account.string_IdentificationID);
                        }
                        if (!Flags.Contains(true) && !Account.List_ContractMachineAuth.Exists(x => x.ID == Machine.string_IdentificationID))
                        {
                            Account.List_ContractMachineAuth.Add(new Contract(false, false, Machine.string_IdentificationID, Machine.string_IdentificationName));
                        }
                        if (!Flags.Contains(true) && Account.List_ContractMachineAuth.Exists(x => x.ID == Machine.string_IdentificationID && x.Accepted))
                        {
                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_get_account_access_token");
                            Response.Add("function_flags", Flags);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                            Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_get_account_access_token");
                            Response.Add("account_access_token", EncryptedAccessToken);
                            Response.Add("account_ifa_private_key", Account.List_IFASecurities[0].IFAPrivateKey);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                        }
                        if (!Flags.Contains(true) && Account.List_ContractMachineAuth.Exists(x => x.ID == Machine.string_IdentificationID && !x.Accepted) && Account.bool_ConfigurationUseAssociation2FA)
                        {
                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_get_account_access_token");
                            Response.Add("function_flags", Flags);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);

                            Action SendWhenAuthenticated = () =>
                            {
                                while (Transceiver.Socket.Connected)
                                {
                                    if(Account.List_ContractMachineAuth.Exists(x => x.ID == Machine.string_IdentificationID && x.Accepted))
                                    {
                                        Response = new Transmission(Transmission.DiscourseID);
                                        Response.Add("function_name", "response_get_account_access_token");
                                        Response.Add("account_access_token", EncryptedAccessToken);
                                        Response.Add("account_ifa_private_key", Account.List_IFASecurities[0].IFAPrivateKey);
                                        Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                                        return;
                                    }
                                    Thread.Sleep(1000);
                                }
                            };
                            SendWhenAuthenticated.BeginInvoke(x => { }, null);
                        }
                        if (!Flags.Contains(true) && Account.List_ContractMachineAuth.Exists(x => x.ID == Machine.string_IdentificationID && !x.Accepted) && !Account.bool_ConfigurationUseAssociation2FA)
                        {
                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_get_account_access_token");
                            Response.Add("function_flags", Flags);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                            Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("account_access_token", EncryptedAccessToken);
                            Response.Add("account_ifa_private_key", Account.List_IFASecurities[0].IFAPrivateKey);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                        }
                    }
                    #endregion
                }
                if (FunctionName == "request_authenticate_account")
                {
                    #region Import
                    string
                        AccountAccessTokenTest = (string)Transmission["account_access_token"],
                        AccountID = (string)Transceiver.DataBoard["account_id"],
                        AccountAccessTokenValid = (string)Transceiver.DataBoard["account_access_token"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.List_Account.Find(x => x.string_IdentificationID == AccountID);
                    bool MachineExists = Machine != null;
                    bool AccountExists = Account != null;
                    bool AccountAccessTokenIsValid = AccountAccessTokenTest == AccountAccessTokenValid;

                    bool[] Flags = new bool[]
                    {
                    !MachineExists,
                    !AccountExists,
                    !AccountAccessTokenIsValid
                    };
                    #endregion
                    #region Process
                    if (MachineExists)
                    {
                        if (Flags.Contains(true))
                        {
                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_authenticate_account");
                            Response.Add("function_flags", Flags);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                        }
                        if (!Flags.Contains(true))
                        {
                            Transceiver.DataBoard.Remove("account_id");
                            Transceiver.DataBoard.Remove("account_access_token");

                            TransceiverAccess transceiverAccess = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver];
                            transceiverAccess.Account = Account;
                            MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver] = transceiverAccess;

                            Discourse.HandleTransmission = void_AUTHPROCESS;

                            Transmission Response = new Transmission(Transmission.DiscourseID);
                            Response.Add("function_name", "response_authenticate_account");
                            Response.Add("function_flags", Flags);
                            Transceiver.SendTransmission(Response, Machine.string_AuthenticationPublicKey);
                        }
                    }
                    #endregion
                }
            }
            public static void void_AUTHPROCESS(Transceiver Transceiver, Discourse Discourse, Transmission Transmission)
            {
                string FunctionName = (string)Transmission["function_name"];
                if (FunctionName == "request_streamget_account")
                {
                    #region Import
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;

                    bool[] Flags = new bool[]
                    {

                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_streamget_account");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_streamget_account");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);

                        DateTime LastStreamed = DateTime.Now;
                        Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("account", Account);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                        byte[] PreviousAccountByte = Account.ToByte();
                        Action StreamOut = null;
                        StreamOut = () =>
                        {
                            if (Transceiver.Socket.Connected && DateTime.Now.Subtract(LastStreamed).TotalSeconds > 3)
                            {
                                byte[] AccountByte = Account.ToByte();
                                if (!PreviousAccountByte.SequenceEqual(AccountByte))
                                {
                                    Result = new Transmission(Transmission.DiscourseID);
                                    Result.Add("account", Account);
                                    Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                                    PreviousAccountByte = AccountByte;
                                }
                                LastStreamed = DateTime.Now;
                            }
                            else if (!Transceiver.Socket.Connected)
                            {
                                MainServer.Vals.Action_TimerUpdateCallBack -= StreamOut;
                            }
                        };
                        MainServer.Vals.Action_TimerUpdateCallBack += StreamOut;
                    }
                    #endregion
                }
                if (FunctionName == "request_add_sacontract")
                {
                    #region Import
                    string ServerID = (string)Transmission["server_id"];
                    string RemoteAccountID = (string)Transmission["remote_account_id"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Server Server = Machine.List_Servers.Find(x => x.string_IdentificationID == ServerID);
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Account RemoteAccount = MainServer.Vals.List_Account.Find(x => x.string_IdentificationID == RemoteAccountID);
                    bool ServerExists = Server != null;
                    bool RemoteAccountExists = RemoteAccount != null;
                    bool RemoteAccountNotInvited = !Server.List_ContractsAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID && x.Initiated);
                    bool[] Flags = new bool[]
                    {
                        !RemoteAccountExists,
                        !RemoteAccountNotInvited,
                        !ServerExists,
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_sacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true) && !Server.List_ContractsAccountP2P.Exists(x => x.ID == RemoteAccountID))
                    {
                        Server.List_ContractsAccountP2P.Add(new Contract(false, true, RemoteAccount.string_IdentificationID, RemoteAccount.string_IdentificationName));
                        RemoteAccount.List_ContractServerP2P.Add(new Contract(false, false, Machine.string_IdentificationID + "." + Server.string_IdentificationID, Server.string_IdentificationName));
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_sacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if(!Flags.Contains(true) && Server.List_ContractsAccountP2P.Exists(x => x.ID == RemoteAccountID && !x.Initiated))
                    {
                        Server.List_ContractsAccountP2P.Find(x => x.ID == RemoteAccountID).Accepted = true;
                        RemoteAccount.List_ContractServerP2P.Find(x => x.ID == Server.string_IdentificationID).Accepted = true;
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_sacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_add_aacontract")
                {
                    #region Import
                    string RemoteAccountID = (string)Transmission["remote_account_id"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Account RemoteAccount = MainServer.Vals.List_Account.Find(x => x.string_IdentificationID == RemoteAccountID);
                    bool RemoteAccountExists = RemoteAccount != null;
                    bool RemoteAccountNotAsked = !Account.List_ContractAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID && x.Initiated);
                    bool RemoteAccountNotAdded = !Account.List_ContractAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID && x.Accepted);
                    bool[] Flags = new bool[]
                    {
                        !RemoteAccountExists,
                        !RemoteAccountNotAsked,
                        !RemoteAccountNotAdded,
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_aacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true) && !Account.List_ContractAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID))
                    {
                        Account.List_ContractAccountP2P.Add(new Contract(false, true, RemoteAccount.string_IdentificationID, RemoteAccount.string_IdentificationName));
                        RemoteAccount.List_ContractAccountP2P.Add(new Contract(false, false, Account.string_IdentificationID, Account.string_IdentificationName));
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_aacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true) && Account.List_ContractAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID && !x.Initiated))
                    {
                        Account.List_ContractAccountP2P.Find(x => x.ID == RemoteAccount.string_IdentificationID).Accepted = true;
                        RemoteAccount.List_ContractAccountP2P.Find(x => x.ID == Account.string_IdentificationID).Accepted = true;
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_aacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_add_ascontract")
                {
                    #region Import
                    string RemoteServerDirectory = (string)Transmission["remote_server_id"];
                    string[] RemoteServerDirectorySplit = RemoteServerDirectory.Split('.');
                    string RemoteMachineID = RemoteServerDirectorySplit[0];
                    string RemoteServerID = RemoteServerDirectorySplit[1];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Machine RemoteMachine = MainServer.Vals.List_Machine.Find(x => x.string_IdentificationID == RemoteMachineID);
                    bool RemoteMachineExists = RemoteMachine != null;
                    Server RemoteServer = RemoteMachineExists? RemoteMachine.List_Servers.Find(x => x.string_IdentificationID == RemoteServerID) : null;
                    bool RemoteServerExists = RemoteServer != null;
                    bool RemoteServerNotAdded = RemoteServerExists? !Account.List_ContractServerP2P.Exists(x => x.ID == RemoteServerDirectory && x.Initiated) : true;
                    bool[] Flags = new bool[]
                    {
                        !RemoteMachineExists,
                        !RemoteServerNotAdded,
                        !RemoteServerExists
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_ascontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true) && !Account.List_ContractServerP2P.Exists(x => x.ID == RemoteServerDirectory && !x.Initiated))
                    {
                        Account.List_ContractServerP2P.Add(new Contract(false, true, RemoteServerDirectory, RemoteServer.string_IdentificationName));
                        RemoteServer.List_ContractsAccountP2P.Add(new Contract(false, false, Account.string_IdentificationID, Account.string_IdentificationName));
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_ascontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true) && Account.List_ContractServerP2P.Exists(x => x.ID == RemoteServerDirectory && !x.Initiated))
                    {
                        Account.List_ContractServerP2P.Find(x => x.ID == RemoteServerDirectory).Accepted = true;
                        RemoteServer.List_ContractsAccountP2P.Find(x => x.ID == Account.string_IdentificationID).Accepted = true;
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_add_ascontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_remove_ascontract")
                {
                    #region Import
                    string RemoteServerDirectory = (string)Transmission["remote_server_id"];
                    string[] RemoteServerDirectorySplit = RemoteServerDirectory.Split('.');
                    string RemoteMachineID = RemoteServerDirectorySplit[0];
                    string RemoteServerID = RemoteServerDirectorySplit[1];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Machine RemoteMachine = MainServer.Vals.List_Machine.Find(x => x.string_IdentificationID == RemoteMachineID);
                    Server RemoteServer = RemoteMachine.List_Servers.Find(x => x.string_IdentificationID == RemoteServerID);
                    bool RemoteMachineExists = RemoteMachine != null;
                    bool RemoteServerExists = RemoteServer != null;
                    bool RemoteMachineAdded = RemoteMachineExists? Account.List_ContractServerP2P.Exists(x => x.ID == RemoteServerDirectory) : false;
                    bool[] Flags = new bool[]
                    {
                        !RemoteMachineExists,
                        !RemoteMachineAdded,
                        !RemoteServerExists,
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_remove_ascontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Account.List_ContractServerP2P.RemoveAt(x => x.ID == RemoteServerDirectory);
                        RemoteServer.List_ContractsAccountP2P.RemoveAt(x => x.ID == Account.string_IdentificationID);
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_remove_ascontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_remove_sacontract")
                {
                    #region Import
                    string ServerID = (string)Transmission["server_id"];
                    string RemoteAccountID = (string)Transmission["remote_account_id"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Server Server = Machine.List_Servers.Find(x => x.string_IdentificationID == ServerID);
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Account RemoteAccount = MainServer.Vals.List_Account.Find(x => x.string_IdentificationID == RemoteAccountID);
                    bool ServerExists = Server != null;
                    bool RemoteAccountExists = RemoteAccount != null;
                    bool RemoteAccountAdded = (RemoteAccountExists && ServerExists)? Server.List_ContractsAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID) : false;
                    bool[] Flags = new bool[]
                    {
                        !RemoteAccountExists,
                        !RemoteAccountAdded,
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_remove_sacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Server.List_ContractsAccountP2P.RemoveAt(x => x.ID == RemoteAccount.string_IdentificationID);
                        RemoteAccount.List_ContractServerP2P.RemoveAt(x => x.ID == Server.string_IdentificationID);
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_remove_sacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_remove_aacontract")
                {
                    #region Import
                    string RemoteAccountID = (string)Transmission["remote_account_id"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Account RemoteAccount = MainServer.Vals.List_Account.Find(x => x.string_IdentificationID == RemoteAccountID);
                    bool RemoteAccountExists = RemoteAccount != null;
                    bool RemoteAccountAdded = RemoteAccountExists? Account.List_ContractAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID) : false;
                    bool[] Flags = new bool[]
                    {
                        !RemoteAccountExists,
                        !RemoteAccountAdded,
                    };
                    #endregion
                    #region Process
                    if (Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_remove_aacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (!Flags.Contains(true))
                    {
                        Account.List_ContractAccountP2P.RemoveAt(x => x.ID == RemoteAccount.string_IdentificationID);
                        RemoteAccount.List_ContractAccountP2P.RemoveAt(x => x.ID == Account.string_IdentificationID);
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_remove_aacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_connect_ascontract")
                {
                    #region Import
                    string RemoteServerDirectory = (string)Transmission["remote_server_id"];
                    string[] RemoteServerDirectorySplit = RemoteServerDirectory.Split('.');
                    string RemoteMachineID = RemoteServerDirectorySplit[0];
                    string RemoteServerID = RemoteServerDirectorySplit[1];
                    IPEndPoint
                        IPEndPoint_External = (IPEndPoint)Transmission["ipendpoint_external"],
                        IPEndPoint_Internal = (IPEndPoint)Transmission["ipendpoint_internal"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Machine RemoteMachine = MainServer.Vals.List_Machine.Find(x => x.string_IdentificationID == RemoteMachineID);
                    Server RemoteServer = RemoteMachine.List_Servers.Find(x => x.string_IdentificationID == RemoteServerID);
                    bool MachineExists = Machine != null;
                    bool AccountExists = Account != null;
                    bool RemoteMachineExists = RemoteMachine != null;
                    bool RemoteServerExists = RemoteServer != null;
                    bool RemoteServerAdded = (RemoteMachineExists && RemoteServerExists)? Account.List_ContractServerP2P.Exists(x => x.ID == RemoteServer.string_IdentificationID && x.Accepted): false;
                    bool[] Flags = new bool[]
                    {
                        !AccountExists,
                        !RemoteMachineExists,
                        !RemoteServerAdded,
                        !RemoteServerExists,
                        !MachineExists,
                    };
                    #endregion
                    #region Process
                    if(MachineExists && Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_connect_ascontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if(MachineExists && !Flags.Contains(true))
                    {
                        string ContactToken = Crypt.RandomString_AlphaNumeric(10, MainServer.Vals.Random);
                        Predicate<P2P> PredicateMachineP2P = x => x.Tag.ID == Machine.string_IdentificationID;
                        if (RemoteServer.List_Connections.Exists(PredicateMachineP2P))
                        {
                            RemoteServer.List_Connections.RemoveAt(PredicateMachineP2P);
                        }

                        P2P MachineP2P = new P2P(new Tag(Machine.string_IdentificationID, Machine.string_IdentificationName), IPEndPoint_External, IPEndPoint_Internal);
                        RemoteServer.List_Connections.Add(MachineP2P);

                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_connect_ascontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_connect_sacontract")
                {
                    #region Import
                    string ServerID = (string)Transmission["server_id"];
                    string RemoteAccountID = (string)Transmission["remote_account_id"];
                    IPEndPoint
                        IPEndPoint_External = (IPEndPoint)Transmission["ipendpoint_external"],
                        IPEndPoint_Internal = (IPEndPoint)Transmission["ipendpoint_internal"];
                    #endregion
                    #region Flags
                    Machine Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine;
                    Account Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account;
                    Account RemoteAccount = MainServer.Vals.List_Account.Find(x => x.string_IdentificationID == RemoteAccountID);
                    Server Server = Machine.List_Servers.Find(x => x.string_IdentificationID == ServerID);
                    bool MachineExists = Machine != null;
                    bool AccountExists = Account != null;
                    bool RemoteAccountExists = RemoteAccount != null;
                    bool AccountFriended = RemoteAccountExists? Account.List_ContractAccountP2P.Exists(x => x.ID == RemoteAccount.string_IdentificationID && x.Accepted) : false;
                    bool[] Flags = new bool[]
                    {
                        !AccountExists,
                        !RemoteAccountExists,
                        !AccountFriended,
                    };
                    #endregion
                    #region Process
                    if (MachineExists && Flags.Contains(true))
                    {
                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_connect_sacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    if (MachineExists && !Flags.Contains(true))
                    {
                        Predicate<P2P> PredicateServerP2P = x => x.Tag.ID == Server.string_IdentificationID;
                        if (Account.List_ConnectionServers.Exists(PredicateServerP2P))
                        {
                            Account.List_ConnectionServers.RemoveAt(PredicateServerP2P);
                        }

                        P2P RemoteServerP2P = new P2P(new Tag(Server.string_IdentificationID, Server.string_IdentificationName), IPEndPoint_External, IPEndPoint_Internal);

                        Transmission Result = new Transmission(Transmission.DiscourseID);
                        Result.Add("function_name", "response_connect_sacontract");
                        Result.Add("function_flags", Flags);
                        Transceiver.SendTransmission(Result, Machine.string_AuthenticationPublicKey);
                    }
                    #endregion
                }
                if (FunctionName == "request_pull")
                {
                    #region Import
                    string
                        ItemName = (string)Transmission["item_name"];

                    #endregion
                    #region Flags
                    bool ItemNameExists = ItemName != null;

                    bool[] Flags = new bool[]
                    {
                    !ItemNameExists
                    };
                    #endregion
                    #region Process
                    if (!Flags.Contains(true))
                    {
                        void_PULLPROCESS(ItemName, Transceiver, Discourse, Transmission);
                    }
                    #endregion
                }
            }
            public static void void_PULLPROCESS(string ItemName, Transceiver Transceiver, Discourse Discourse, Transmission Transmission)
            {
                string FunctionName = (string)Transmission["function_name"];
                if (FunctionName == "account_name")
                {
                    #region Import
                    #endregion
                    #region Flags
                    #endregion
                    #region Process
                    #endregion
                }
            }
 
            public static byte[] byte_ExportTransmission(Transceiver Transceiver, Transmission Transmission, string OutPublicKey)
            {
                byte[] Result = null;

                Transmission.SKPString = Crypt.RandomString_AlphaNumeric(30, MainServer.Vals.Random);
                Transmission.SKPKey = Transceiver.Security.SeedKeyPair.GetKey(Transmission.SKPString);
                Transceiver.Security.UsedSKPStrings.Add(Transmission.SKPString);

                Action Encrypt = null;
                Action Serialize = () =>
                {
                    Result = Transmission.ToByte();
                };
                Action AddMainLayer = () =>
                {
                    string MessageKey = Crypt.RandomString_AlphaNumeric(100, MainServer.Vals.Random);
                    byte[] EncryptedMessageKey = Crypt.EncryptRSA(OutPublicKey, MessageKey.ToByte());
                    byte[] EncryptedMessage = Crypt.EncryptECB(Result, MessageKey.ToByte());
                    List<object> FinalExport = new List<object> { EncryptedMessageKey, EncryptedMessage };
                    Result = FinalExport.ToByte();
                };

                Encrypt += Serialize;
                Encrypt += AddMainLayer;
                Encrypt.Invoke();

                return Result;
            }
            public static Transmission Transmission_ImportTransmission(Transceiver Transceiver, byte[] data)
            {
                if (data.Length == 0)
                {
                    return null;
                }
                string InPrivateKey = MainServer.Vals.string_PrivateKey;
                Transmission Result = null;

                Action Decrypt = null;
                Action RemoveMainLayer = () =>
                {
                    List<object> FinalExport = data.To<List<object>>();
                    byte[] EncryptedMessageKey = (byte[])FinalExport[0];
                    byte[] EncryptedMessage = (byte[])FinalExport[1];
                    string MessageKey = Crypt.DecryptRSA(InPrivateKey, EncryptedMessageKey).To<string>();
                    data = Crypt.DecryptECB(EncryptedMessage, MessageKey.ToByte());
                };
                Action Deserialize = () =>
                {
                    Result = data.To<Transmission>();
                };

                Decrypt += RemoveMainLayer;
                Decrypt += Deserialize;
                Decrypt.Invoke();

                byte[] ValidKey = Transceiver.Security.SeedKeyPair.GetKey(Result.SKPString);
                if ((ValidKey == null && Result.SKPKey != null) || (Result.SKPKey == null && ValidKey != null) || (Result.SKPKey != null && ValidKey != null && !Result.SKPKey.SequenceEqual(ValidKey)))
                {
                    return null;
                }
                Transceiver.Security.UsedSKPStrings.Add(Result.SKPString);

                return Result;
            }
            public static void void_InitDiscourse(Transceiver Transceiver, Discourse Discourse)
            {
                Account Account = default(Account); try { Account = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Account; } catch (KeyNotFoundException ex) { }
                Machine Machine = default(Machine); try { Machine = MainServer.Vals.Dictionary_TransceiverAccesses[Transceiver].Machine; } catch (KeyNotFoundException ex) { }
                bool AccountExists = Account != null;
                bool MachineExists = Machine != null;

                if (!MachineExists && !AccountExists && Transceiver.Security.SeedKeyPair.Seed == null)
                {
                    Discourse.HandleTransmission = (transceiver, discourse, transmission) =>
                    {
                        if (transmission == null)
                        {
                            return;
                        }
                        MainServerProcessing.Methods.void_NOAUTHPROCESS(transceiver, discourse, transmission);
                    };
                }
                if (!MachineExists && !AccountExists && Transceiver.Security.SeedKeyPair.Seed != null)
                {
                    Discourse.HandleTransmission = (transceiver, discourse, transmission) =>
                    {
                        if (transmission == null)
                        {
                            return;
                        }
                        MainServerProcessing.Methods.void_SKPAUTHPROCESS(transceiver, discourse, transmission);
                    };
                }
                if (MachineExists && !AccountExists)
                {
                    Discourse.HandleTransmission = (transceiver, discourse, transmission) =>
                    {
                        if (transmission == null)
                        {
                            return;
                        }
                        MainServerProcessing.Methods.void_MACHINEAUTHPROCESS(transceiver, discourse, transmission);
                    };
                }
                if (MachineExists && AccountExists)
                {
                    Discourse.HandleTransmission = (transceiver, discourse, transmission) =>
                    {
                        if (transmission == null)
                        {
                            return;
                        }
                        MainServerProcessing.Methods.void_AUTHPROCESS(Transceiver, discourse, transmission);
                    };
                }
            }
            public static void void_InitTransceiver(Socket socket)
            {
                Transceiver Transceiver = new Transceiver(socket, new TransceiverCallBacks(void_InitDiscourse));
                Transceiver.Security = new TransceiverSecurity
                (
                    new SeedKeyPair(null, x => { return null; }),
                    MainServerProcessing.Methods.byte_ExportTransmission,
                    MainServerProcessing.Methods.Transmission_ImportTransmission
                );
                MainServer.Vals.List_ConnectedTransceivers.Add(Transceiver);
                MainServer.Vals.Dictionary_TransceiverAccesses.Add(Transceiver, new TransceiverAccess());
                Transceiver.Open();
            }
        }
    }
}

