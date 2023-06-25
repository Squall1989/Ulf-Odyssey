using MsgPck;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Ulf;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LobbyControl : MonoBehaviour
{
    [SerializeField] protected GameObject playerListGO;
    [SerializeField] protected Button createButton;
    [Inject] MessageSender sender;
    
    private List<TextMeshProUGUI> playersTMPro;
    private Dictionary<int, string> playersDict;

    public Button CreateButton => createButton;

    // Start is called before the first frame update
    void Start()
    {
        playersTMPro = playerListGO.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        playersTMPro.ForEach(x => x.enabled = false);
        sender.OnLobbyUpdate += UpdateLobby;
    }

    private void OnEnable()
    {
        sender.Init();
    }

    private void UpdateLobby(List<PlayerMsg> playerList)
    {
        EnterLobby(true);
        createButton.gameObject.SetActive(false);

        foreach (PlayerMsg playerMsg in playerList)
        {
            NewPlayer(playerMsg.Name, playerMsg.Id);
        }
    }

    public void EnterLobby(bool enter)
    {
        playerListGO.SetActive(enter);
        playersTMPro.ForEach(x => x.enabled = false);

        if (enter)
        {
            playersDict = new Dictionary<int, string>();

        }
    }

    public void NewPlayer(string name, int id)
    {
        if(playersDict.ContainsKey(id))
        {
            playersDict[id] = name;
        }
        else
        {
            var freeSlot = playersTMPro.FirstOrDefault(p => p.enabled == false);

            if(freeSlot == null)
            {
                freeSlot = Instantiate(playersTMPro[0], playerListGO.transform);
            }

            freeSlot.text = name;
            freeSlot.enabled = true; 

            playersDict.Add(id, freeSlot.text);

        }
    }

    internal void CreateLobby()
    {
        sender.CreateLobby();
    }
}
