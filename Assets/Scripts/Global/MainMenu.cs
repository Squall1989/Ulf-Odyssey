using MsgPck;
using System;
using System.Collections.Generic;
using Ulf;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panelGame;
    [SerializeField] Button singleButton, multiplayerButton;
    [SerializeField] LobbyControl panelLobby;

    [Inject] GameOptions gameOptions;

    // Start is called before the first frame update
    void Start()
    {
        panelLobby.CreateButton.onClick.AddListener(CreateLobby);
        multiplayerButton.onClick.AddListener(SwitchToMultiplayer);
        singleButton.onClick.AddListener(SwitchToSingle);
    }

    private void SwitchToSingle()
    {
        ActivatePanel(false);
        gameOptions.SetGameType(GameType.single);
        SceneManager.LoadScene("GameScene");
    }

    private void SwitchToMultiplayer()
    {
        ActivatePanel(true);
        gameOptions.SetGameType(GameType.online);
    }

    private void CreateLobby()
    {
        panelLobby.CreateLobby();
    }

    public void ActivatePanel(bool isActivateLobby)
    {
        panelGame.SetActive(!isActivateLobby);
        panelLobby.gameObject.SetActive(isActivateLobby);
    }
}
