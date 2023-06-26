using System.Collections;
using System.Collections.Generic;
using Ulf;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHost : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI codeText;
    [SerializeField] private Button startHostButton;
    [SerializeField] private TMPro.TextMeshProUGUI logText;

    private async void Start()
    {
        Auth auth = new Auth();
        await auth.AuthenticatingAPlayer();

        startHostButton.onClick.AddListener(() => {
            HostRelay host = new HostRelay();
            host.OnLog += (log) => Debug.Log(log);
            host.OnCodeGenerate += (code) => codeText.text = code;
            host.StartAllocate();
            startHostButton.onClick.RemoveAllListeners();
        });
    }
}
