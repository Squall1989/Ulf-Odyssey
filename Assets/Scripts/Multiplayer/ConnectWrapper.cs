
using Steamworks;
using Unity.Networking.Transport;

namespace Ulf
{
    public interface IConnectWrapper
    {

    }

    public struct UnityConnect : IConnectWrapper
    {
        public NetworkConnection networkConnection;
    }

    public struct SteamConnect : IConnectWrapper
    {
        public CSteamID  steamID;
    }
}