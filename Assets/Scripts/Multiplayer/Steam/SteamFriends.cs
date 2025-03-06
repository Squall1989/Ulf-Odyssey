using Steamworks;
using System;
using UnityEngine;

namespace Ulf
{
    public class SteamLookFriends : MonoBehaviour, IFriendConnectable
    {
        public Action<string, ulong> OnFriendInGame { get; set; }

        public void RequestFriendsInGame()
        {
            GetFriendList();
        }

        public void Start()
        {

        }

        private void GetFriendList()
        {
            int friendsCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
            var myAppID = SteamUtils.GetAppID();
            for (int i = 0; i < friendsCount; i++)
            {
                CSteamID friendID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);

                if (SteamFriends.GetFriendGamePlayed(friendID, out var gameInfo))
                {
                    var friendAppId = gameInfo.m_gameID.AppID();
                    string friendName = SteamFriends.GetFriendPersonaName(friendID);
                    if (friendAppId == myAppID)
                    {
                        OnFriendInGame?.Invoke(friendName, friendID.m_SteamID);
                    }

                }
            }
        }
    }
}