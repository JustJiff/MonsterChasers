using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    public readonly SyncVar<Team> SelectedTeam = new SyncVar<Team>(Team.None);
    public readonly SyncVar<string> PlayerName = new SyncVar<string>("Player");

    public override void OnStartClient()
    {
        base.OnStartClient();

        SelectedTeam.OnChange += OnTeamChanged;
        PlayerName.OnChange += OnNameChanged;
    }

    void OnTeamChanged(Team oldValue, Team newValue, bool asServer)
    {
        LobbyManagerUI.Instance?.RefreshLists();
    }

    void OnNameChanged(string oldValue, string newValue, bool asServer)
    {
        LobbyManagerUI.Instance?.RefreshLists();
    }


    [ServerRpc]
    public void SetTeamServerRpc(Team team)
    {
        SelectedTeam.Value = team;
    }

    [ServerRpc]
    public void SetNameServerRpc(string name)
    {
        PlayerName.Value = name;
        Debug.Log("Ustawiam nick: " + name);
    }

}