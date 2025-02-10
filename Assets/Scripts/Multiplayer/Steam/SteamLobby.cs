using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Steamworks;
using UnityEngine.SceneManagement;

public class LobbyScript : MonoBehaviour
{
    // Use this for initialization
    protected Callback<LobbyCreated_t> Callback_lobbyCreated;
    protected Callback<LobbyMatchList_t> Callback_lobbyList;
    protected Callback<LobbyEnter_t> Callback_lobbyEnter;
    protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;
    protected Callback<LobbyChatMsg_t> Callback_lobbyChat;
    protected Callback<LobbyChatUpdate_t> Callback_lobbyJoin;

    private int lobbyCount = 0;

    protected const string lobby_prefix = "u~l*f";


    public Text currentLobbyTitle;
    public Text lobbyTextEnterArea;
    public RectTransform lobbyButtonPrefab, Players;
    public Button startButton;

    public ulong current_lobbyID;
    List<CSteamID> lobbyIDS;
    List<GameObject> buttonsLobby;
    Dictionary<int, CSteamID> lobbyNums = new Dictionary<int, CSteamID>();

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }
    private void OnDisable()
    {
        if (current_lobbyID != 0)
        {
            SteamMatchmaking.LeaveLobby((CSteamID)current_lobbyID);
        }
    }

    // Use this for initialization
    void Start()
    {
        buttonsLobby = new List<GameObject>();
        lobbyIDS = new List<CSteamID>();
        Callback_lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        Callback_lobbyList = Callback<LobbyMatchList_t>.Create(OnGetLobbiesList);
        Callback_lobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        Callback_lobbyInfo = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);

        Callback_lobbyJoin = Callback<LobbyChatUpdate_t>.Create(OnLobbyJoin);
        Callback_lobbyChat = Callback<LobbyChatMsg_t>.Create(OnLobbyMessage);

        if (SteamAPI.Init())
            Debug.LogError("Steam API init -- SUCCESS!");
        else
            Debug.LogError("Steam API init -- failure ...");
    }

    public void StartGameCurrentLobby()
    {

        byte[] MsgBody = System.Text.Encoding.UTF8.GetBytes(lobby_prefix);

        SteamMatchmaking.SendLobbyChatMsg((CSteamID)current_lobbyID, MsgBody, MsgBody.Length);

        //startTheGame();
    }

    private void startTheGame()
    {
        GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Level_zero");
        int numPlayers = SteamMatchmaking.GetNumLobbyMembers((CSteamID)current_lobbyID);
        Debug.Log("\t Number of players currently in lobby : " + numPlayers);
        for (int i = 0; i < numPlayers; i++)
        {
            CSteamID lobbyPlayerID = SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)current_lobbyID, i);
            if (lobbyPlayerID == SteamUser.GetSteamID())
                continue;
            //PlayerNetSocket.playerNetSocket.AddPlayer(lobbyPlayerID);
        }
    }

    public void CreateLobby()
    {
        Debug.Log("Trying to create lobby ...");
        string str_ = lobbyTextEnterArea.text;
        str_.Trim();
        if (str_.Length == 0)
        {
            Debug.Log("Empty title of lobby");
            return;
        }
        SteamAPICall_t try_toHost = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 8);

    }

    public void GetLobbyList()
    {
        Debug.Log("Trying to get list of available lobbies ...");
        //usLobbyCount = 0;

        for (int i = 0; i < buttonsLobby.Count; i++)
        {
            Destroy(buttonsLobby[i].gameObject);
        }
        buttonsLobby.Clear();

        lobbyNums.Clear();
        SteamAPICall_t try_getList = SteamMatchmaking.RequestLobbyList();

    }

    public void TryJoinLobby(int num_)
    {
        Debug.Log("Num: " + num_);
        SteamAPICall_t try_joinLobby = SteamMatchmaking.JoinLobby(lobbyNums[num_]);

    }

    void Update()
    {
        SteamAPI.RunCallbacks();

    }

    void OnLobbyCreated(LobbyCreated_t result)
    {
        if (result.m_eResult == EResult.k_EResultOK)
            Debug.Log("Lobby created -- SUCCESS!");
        else
            Debug.Log("Lobby created -- failure ...");

        string personalName = SteamFriends.GetPersonaName();
        SteamMatchmaking.SetLobbyData((CSteamID)result.m_ulSteamIDLobby, "name", lobby_prefix + lobbyTextEnterArea.text);
    }

    void OnGetLobbiesList(LobbyMatchList_t result)
    {
        lobbyIDS.Clear();
        lobbyCount = 0;
        //Debug.Log("Found " + result.m_nLobbiesMatching + " lobbies!");
        for (int i = 0; i < result.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            lobbyIDS.Add(lobbyID);
            SteamMatchmaking.RequestLobbyData(lobbyID);
        }

    }

    void OnGetLobbyInfo(LobbyDataUpdate_t result)
    {
        for (int i = 0; i < lobbyIDS.Count; i++)
        {
            if (lobbyIDS[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                string gettingTitle_ = SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDS[i].m_SteamID, "name");

                if (gettingTitle_.Length <= 5)
                    return;

                string firstPart = gettingTitle_.Substring(0, 5);

                if (firstPart == lobby_prefix)
                {
                    int usLobbyCount = lobbyCount++;

                    lobbyNums.Add(usLobbyCount, (CSteamID)lobbyIDS[i].m_SteamID);

                    RectTransform newButton_ = Instantiate(lobbyButtonPrefab.gameObject).GetComponent<RectTransform>();
                    gettingTitle_ = gettingTitle_.Substring(5, gettingTitle_.Length - 5);
                    newButton_.GetChild(0).GetComponent<Text>().text = gettingTitle_;
                    newButton_.parent = lobbyButtonPrefab.parent;
                    newButton_.position = lobbyButtonPrefab.position;
                    newButton_.gameObject.SetActive(true);
                    newButton_.localPosition = new Vector2(newButton_.localPosition.x, newButton_.localPosition.y - newButton_.rect.height * usLobbyCount);

                    //UnityAction call = delegate { TryJoinLobby(usLobbyCount); };
                    newButton_.GetComponent<Button>().onClick.AddListener(delegate { TryJoinLobby(usLobbyCount); });
                    Debug.Log("usLobbyCount: " + usLobbyCount);
                    buttonsLobby.Add(newButton_.gameObject);
                }

                lobbyIDS.Remove((CSteamID)result.m_ulSteamIDLobby);
                return;
            }
        }
    }

    private void getPlayersOnLobby()
    {
        int numPlayers = SteamMatchmaking.GetNumLobbyMembers((CSteamID)current_lobbyID);

        Debug.Log("\t Number of players currently in lobby : " + numPlayers);
        for (int i = 0; i < numPlayers; i++)
        {
            string namePlayer = SteamFriends.GetFriendPersonaName(SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)current_lobbyID, i));
            Players.GetChild(i).GetComponent<Text>().text = namePlayer;
            //Debug.Log("\t Player(" + i + ") == " + namePlayer);
        }
    }


    void OnLobbyJoin(LobbyChatUpdate_t result)
    {
        getPlayersOnLobby();


    }

    void OnLobbyMessage(LobbyChatMsg_t result)
    {
        byte[] MsgBody = new byte[1024];
        CSteamID cSteam;
        EChatEntryType chatentry;

        SteamMatchmaking.GetLobbyChatEntry((CSteamID)current_lobbyID, (int)result.m_iChatID, out cSteam, MsgBody, MsgBody.Length, out chatentry);
        string chatMSG = System.Text.Encoding.Default.GetString(MsgBody);

        //if(chatMSG == lobby_prefix)
        {
            startTheGame();
        }
    }

    void OnLobbyEntered(LobbyEnter_t result)
    {
        current_lobbyID = result.m_ulSteamIDLobby;

        if (result.m_EChatRoomEnterResponse == 1)
        {
            Debug.Log("Lobby joined!");
            string tempTitle = SteamMatchmaking.GetLobbyData((CSteamID)current_lobbyID, "name");
            tempTitle = tempTitle.Substring(5, tempTitle.Length - 5);
            currentLobbyTitle.text = tempTitle;
            getPlayersOnLobby();

            if (SteamMatchmaking.GetLobbyOwner((CSteamID)current_lobbyID) == SteamUser.GetSteamID())
            {
                startButton.interactable = true;
            }
            else
                startButton.interactable = false;

        }
        else
            Debug.Log("Failed to join lobby.");
    }
}
