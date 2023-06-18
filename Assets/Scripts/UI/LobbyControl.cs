using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyControl : MonoBehaviour
{
    [SerializeField] protected GameObject playerListGO;
    private List<TextMeshProUGUI> playersTMPro;
    private Dictionary<int, string> playersDict;

    // Start is called before the first frame update
    void Start()
    {
        playersTMPro = playerListGO.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        playersTMPro.ForEach(x => x.enabled = false);
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
}
