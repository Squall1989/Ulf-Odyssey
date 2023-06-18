using MsgPck;
using System;
using System.Collections.Generic;
using Ulf;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panelGame;
    [SerializeField] LobbyControl panelLobby;
    [SerializeField] Button createButton, multiplayerButton;

    [Inject] MessageSender sender;
    [Inject] GameStarter starter;

    // Start is called before the first frame update
    void Start()
    {
        createButton.onClick.AddListener(CreateLobby);
        multiplayerButton.onClick.AddListener(SwitchToMultiplayer);
        sender.OnLobbyUpdate += UpdateLobby;
    }

    private void UpdateLobby(List<PlayerMsg> playerList)
    {
        panelLobby.EnterLobby(true);
        createButton.gameObject.SetActive(false);

        foreach (PlayerMsg playerMsg in playerList)
        {
            panelLobby.NewPlayer(playerMsg.Name, playerMsg.Id);
        }
    }

    private void SwitchToMultiplayer()
    {
        ActivatePanel(true);
        starter.SetupMultiplayer();
    }

    private void CreateLobby()
    {
        sender.CreateLobby();
    }

    public void ActivatePanel(bool isActivateLobby)
    {
        panelGame.SetActive(!isActivateLobby);
        panelLobby.gameObject.SetActive(isActivateLobby);
    }
}
