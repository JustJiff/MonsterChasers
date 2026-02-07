using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public enum Team
{
    Monsters,
    Catchers
}

public class PlayerData : NetworkBehaviour
{
    public readonly SyncVar<Team> TeamSync = new SyncVar<Team>();

    public override void OnStartClient()
    {
        base.OnStartClient();
        TeamSync.OnChange += OnTeamChanged;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        // TEST: auto-assign team
        TeamSync.Value = Team.Monsters;
        Debug.Log($"[SERVER TEST] Auto team set for {Owner.ClientId}");
    }

    [ServerRpc]
    public void SetTeamServerRpc(Team team)
    {
        TeamSync.Value = team;
        Debug.Log($"[SERVER] Team set to {team} for {Owner.ClientId}");
    }

    private void OnTeamChanged(Team oldTeam, Team newTeam, bool asServer)
    {
        Debug.Log($"Team changed: {oldTeam} -> {newTeam}");
    }
}
