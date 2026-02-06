using FishNet.Object;
using UnityEngine;
public class GameStartManager : NetworkBehaviour
{
    public GameObject runnerPrefab;
    public GameObject chaserPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc()
    {
        foreach (var player in FindObjectsOfType<PlayerData>())
        {
            GameObject prefab = player.SelectedTeam.Value == Team.Runner
                ? runnerPrefab
                : chaserPrefab;

            Spawn(prefab, player.Owner);
        }
    }
}