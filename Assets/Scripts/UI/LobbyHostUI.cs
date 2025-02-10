using System;
using System.Collections;
using System.Collections.Generic;
using Ulf;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LobbyHostUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI codeText;
    [SerializeField] private Button startHostButton;
    [SerializeField] private TMPro.TextMeshProUGUI logText;
    [Inject] private ConnectHandler connectHandler;

    private void Start()
    {
        startHostButton.onClick.AddListener(() => HostLobbyStart());

        
    }
    private void HostLobbyStart()
    {
        connectHandler.SetMode(true);
    }

}
