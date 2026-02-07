using FishNet.Managing;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    private static BootstrapManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private string menuName = "MainMenu";
    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private FishySteamworks.FishySteamworks _fishySteamworks;

    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;

    public static ulong CurrentLobbyID;

    private void Start()
    {
        Debug.Log("Bootstrap Start()");

        
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

        
       
        _fishySteamworks.StartConnection(true);   // server
        _fishySteamworks.StartConnection(false);  // client

        SceneManager.LoadScene(menuName);
    }

   

    public static void CreateLobby()
    {
        SteamMatchmaking.CreateLobby(
            ELobbyType.k_ELobbyTypeFriendsOnly,
            4
        );
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
            return;

        CurrentLobbyID = callback.m_ulSteamIDLobby;

        SteamMatchmaking.SetLobbyData(
            new CSteamID(CurrentLobbyID),
            "HostAddress",
            SteamUser.GetSteamID().ToString()
        );

        SteamMatchmaking.SetLobbyData(
            new CSteamID(CurrentLobbyID),
            "name",
            SteamFriends.GetPersonaName() + "'s lobby"
        );

        Debug.Log("Lobby creation was successful");
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        CurrentLobbyID = callback.m_ulSteamIDLobby;

        MainMenuManager.LobbyEntered(
            SteamMatchmaking.GetLobbyData(
                new CSteamID(CurrentLobbyID),
                "name"
            ),
            _networkManager.IsServerStarted   // zamiast IsServer
        );
    }

    public static void JoinByID(CSteamID steamID)
    {
        Debug.Log("Attempting to join lobby with ID: " + steamID.m_SteamID);
        SteamMatchmaking.JoinLobby(steamID);
    }

    public static void LeaveLobby()
    {
        
        SteamMatchmaking.LeaveLobby(new CSteamID(CurrentLobbyID));
        CurrentLobbyID = 0;
    }
}
