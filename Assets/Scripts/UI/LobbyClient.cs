using ENet;
using UnityEngine;
using UnityEngine.UI;

public class LobbyClient : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField JoinCodeInput;
    [SerializeField] private TMPro.TextMeshProUGUI logText;
    [SerializeField] private Button joinButton;

    ClientRelay clientRelay;

    private void Start()
    {
        clientRelay = new ClientRelay();
        clientRelay.OnLog += (log) => Debug.Log(log);
        joinButton.onClick.AddListener(() => clientRelay.Join(JoinCodeInput.text));
    }

    // Update is called once per frame
    void Update()
    {
        clientRelay.Update();
    }
}
