using ENet;
using MsgPck;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UlfServer;
using Unity.Entities.UniversalDelegates;

namespace Ulf
{
    public class SteamHost : SteamBase, INetworkable
    {
        private List<CSteamID> clientList = new();
        private HSteamListenSocket listenSocket;
        private HSteamNetPollGroup pollGroup;
        private Callback<SocketStatusCallback_t> socketCallback;
        private Callback<SteamNetConnectionStatusChangedCallback_t> connectCallback;

        public bool IsConnected => clientList.Count > 0;

        protected override void Awake()
        {
            base.Awake();
            SteamNetworkingUtils.InitRelayNetworkAccess();
            RegisterHandler<PlayerData>(OnPlayerData);
        }

        private void OnPlayerData(PlayerData data, IConnectWrapper connect)
        {
            if(!clientList.Contains(UnwrapConnection(connect)))
            {
                clientList.Add(UnwrapConnection(connect));
            }

        }

        private void Start()
        {
            CreateServer();
        }

        private void CreateServer()
        {
            pollGroup = SteamNetworkingSockets.CreatePollGroup();
            listenSocket = SteamNetworkingSockets.CreateListenSocketP2P(0, 0, null);

            UnityEngine.Debug.Log("Сервер запущен, ждём подключения...");
            CSteamID myID = SteamUser.GetSteamID();
            UnityEngine.Debug.Log($"Мой Steam ID: {myID}");
            // Подписываемся на события изменения статуса соединения
            connectCallback = Callback<SteamNetConnectionStatusChangedCallback_t>.Create(OnConnection);
        }

        private void OnConnection(SteamNetConnectionStatusChangedCallback_t callback)
        {
            UnityEngine.Debug.Log("Connect state changed");
        }

        protected override void P2pRequested(P2PSessionRequest_t param)
        {
            base.P2pRequested(param);
            clientList.Add(param.m_steamIDRemote);

        }

        private void OnDestroy()
        {
            
        }

        public void Send<T>(T message) where T : IUnionMsg
        {
            var bytes = Reader.Serialize<IUnionMsg>(message);
            foreach (var client in clientList)
            {
                SteamNetworking.SendP2PPacket(client, bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendUnreliableNoDelay);
            }
        }

        protected override void Update()
        {
            base.Update();
            IntPtr[] messagesPtr= new IntPtr[10];
            SteamNetworkingMessage_t[] messages = new SteamNetworkingMessage_t[10];
            int msgCount = SteamNetworkingSockets.ReceiveMessagesOnPollGroup(pollGroup, messagesPtr, 10);
            for (int i = 0; i < msgCount; i++)
            {
                byte[] buffer = new byte[messages[i].m_cbSize]; // Создаём массив нужного размера
                Marshal.Copy((IntPtr)messages[i].m_pData, buffer, 0, (int)messages[i].m_cbSize); // Копируем байты

                string receivedText = Encoding.UTF8.GetString(buffer); // Преобразуем в строку
                UnityEngine.Debug.Log($"Получено сообщение: {receivedText}");

                messages[i].Release(); // Освобождаем память
            }
        }
    }
}