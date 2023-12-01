using MsgPck;
using Steamworks;
using System;
using Unity.Networking.Transport;
using UnityEngine;

namespace Ulf
{
    public class SteamBase : MonoBehaviour, INetworkable
    {
        protected const string pchName = "Ulf";

        public bool IsConnected => false;

        public Action<string> OnReceive { get ; set; }

        public void RegisterHandler<T>(Action<T> callback)
        {

        }

        public void RegisterHandler<T>(Action<T, NetworkConnection> callback)
        {

        }

        public void Send<T>(T message) where T : IUnionMsg
        {

        }

        public void Send<T>(T message, NetworkConnection connection) where T : IUnionMsg
        {

        }

        protected void Awake()
        {
            if(!SteamAPI.Init())
            {
                Debug.LogError("Steam not initialized!");
            }
        }

        protected void Update()
        {
            SteamAPI.RunCallbacks();
        }
    }
}