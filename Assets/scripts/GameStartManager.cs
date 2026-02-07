using FishNet.Object;
using UnityEngine;

public class GameStartManager : NetworkBehaviour
{
    [Header("Character Prefabs")]
    [SerializeField] private NetworkObject runnerPrefab;
    [SerializeField] private NetworkObject chaserPrefab;

    public void StartGame()
    {
        Debug.Log("START GAME BUTTON CLICKED");
        StartGameServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void StartGameServerRpc()
    {
        Debug.Log("[SERVER] StartGameServerRpc");

        PlayerData[] players = FindObjectsOfType<PlayerData>();
        Debug.Log($"[SERVER] Players found: {players.Length}");

        foreach (PlayerData player in players)
        {
            NetworkObject prefab =
                player.TeamSync.Value == Team.Monsters
                ? runnerPrefab
                : chaserPrefab;

            NetworkObject character = Instantiate(prefab);
            ServerManager.Spawn(character, player.Owner);

            Debug.Log($"[SERVER] Spawned {player.TeamSync.Value} for {player.Owner.ClientId}");
        }
    }
}
