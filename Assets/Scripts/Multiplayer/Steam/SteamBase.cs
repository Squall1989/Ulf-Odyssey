using MessagePack;
using ModestTree;
using MsgPck;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using UlfServer;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

namespace Ulf
{
    public class SteamBase : MonoBehaviour
    {
        private Callback<P2PSessionRequest_t> p2pSessionRequest_t;

        protected delegate void UnionConnectDelegate(IUnionMsg message, IConnectWrapper connection);
        protected delegate void UnionDelegate(IUnionMsg message);
        protected Dictionary<Type, UnionConnectDelegate> callbacksConnectDict = new();
        protected Dictionary<Type, UnionDelegate> callbacksDict = new();

        protected const string pchName = "Ulf";
        protected const string pchCode = "1";
        protected CSteamID lobbySteamID = new CSteamID();
        protected CSteamID MySteamID => SteamUser.GetSteamID();


        public Action<string> OnReceive { get ; set; }

        protected virtual void Awake()
        {
            if(!SteamAPI.Init())
            {
                Debug.LogError("Steam not initialized!");
            }
            p2pSessionRequest_t = Callback<P2PSessionRequest_t>.Create(P2pRequested);
        }

        protected virtual void P2pRequested(P2PSessionRequest_t param)
        {
            if (SteamNetworking.AcceptP2PSessionWithUser(param.m_steamIDRemote))
            {

            }
        }

        protected virtual void OnLobbyEnter(LobbyEnter_t param)
        {
            lobbySteamID = (CSteamID)param.m_ulSteamIDLobby;
            Debug.Log("Enter: " + param.m_ulSteamIDLobby);
            var ownerId = SteamMatchmaking.GetLobbyOwner((CSteamID)param.m_ulSteamIDLobby);
        }

        protected IConnectWrapper WrapConnection(CSteamID connection)
        {
            return new SteamConnect() {  steamID = connection };
        }

        protected CSteamID UnwrapConnection(IConnectWrapper wrapper)
        {
            return ((SteamConnect)wrapper).steamID;
        }

        public void Send<T>(T message, IConnectWrapper connection) where T : IUnionMsg
        {
            var client = UnwrapConnection(connection);
            var bytes = Reader.Serialize<IUnionMsg>(message);
            SteamNetworking.SendP2PPacket(client, bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendUnreliableNoDelay);

        }

        public virtual void UnRegisterHandler<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (callbacksDict.ContainsKey(type))
            {
                callbacksDict[type] -= (msg) => callback((T)msg);
            }
        }

        public virtual void UnRegisterHandler<T>(Action<T, IConnectWrapper> callback)
        {
            var type = typeof(T);
            if (callbacksConnectDict.ContainsKey(type))
            {
                callbacksConnectDict[type] -= (msg, connect) => callback((T)msg, connect);
            }
        }

        public virtual void RegisterHandler<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (callbacksDict.ContainsKey(type))
            {
                callbacksDict[type] += (msg) => callback((T)msg);
            }
            else
            {
                callbacksDict.Add(type, (msg) => callback((T)msg));
            }
        }

        public virtual void RegisterHandler<T>(Action<T, IConnectWrapper> callback)
        {
            var type = typeof(T);
            if (callbacksConnectDict.ContainsKey(type))
            {
                callbacksConnectDict[type] += (msg, connect) => callback((T)msg, connect);
            }
            else
            {
                callbacksConnectDict.Add(type, (msg, connect) => callback((T)msg, connect));
            }
        }

        protected (IUnionMsg msg, CSteamID steamID) ReadP2pPacket(uint size)
        {
            var bytes = new byte[size];
            SteamNetworking.ReadP2PPacket(bytes, size, out var newSize, out CSteamID senderId);
            var msg = MessagePackSerializer.Deserialize<IUnionMsg>(bytes.ToArray());
            var key = msg.GetType();
            Debug.Log("Read: " + key);
            if (callbacksConnectDict.ContainsKey(key))
                callbacksConnectDict[msg.GetType()]?.Invoke(msg, WrapConnection(senderId));
            if (callbacksDict.ContainsKey(key))
                callbacksDict[msg.GetType()]?.Invoke(msg);

            return (msg, senderId);
        }

        protected void Update()
        {
            SteamAPI.RunCallbacks();
            if(SteamNetworking.IsP2PPacketAvailable(out uint size))
            {
                ReadP2pPacket(size);
            }
        }

        private void OnDestroy()
        {
            if (lobbySteamID != default(CSteamID))
            {
                SteamMatchmaking.LeaveLobby(lobbySteamID);
            }
        }
    }
}