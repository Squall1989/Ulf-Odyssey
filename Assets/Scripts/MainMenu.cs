using Ulf;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panelGame, panelLobby;
    [SerializeField] Button createButton;

    [Inject] MessageSender sender;

    // Start is called before the first frame update
    void Start()
    {
        createButton.onClick.AddListener(CreateLobby);
    }

    private void CreateLobby()
    {
        sender.CreateLobby();
    }

    public void ActivatePanel(bool isActivateLobby)
    {
        panelGame.SetActive(!isActivateLobby);
        panelLobby.SetActive(isActivateLobby);
    }
}
