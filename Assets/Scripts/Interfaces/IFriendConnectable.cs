using System;

public interface IFriendConnectable
{
    void RequestFriendsInGame();
    Action<string, ulong> OnFriendInGame {  get; set; }
}
