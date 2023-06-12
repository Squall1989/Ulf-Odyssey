using UnityEngine;
using UnityEngine.UI;

public class LobbyInfo : MonoBehaviour
{
    [SerializeField]
    protected Text lobbyText;

    public void SetLobbyName(string text)
    {
        lobbyText.text = text;  
    }
}
