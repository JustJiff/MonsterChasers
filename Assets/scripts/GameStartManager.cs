using FishNet;
using FishNet.Object;
using UnityEngine;

public class GameStartManager : NetworkBehaviour
{
    public NetworkObject runnerPrefab;
    public NetworkObject chaserPrefab;

    private Transform[] runnerSpawns;
    private Transform[] chaserSpawns;

    private int runnerIndex = 0;
    private int chaserIndex = 0;

    public override void OnStartServer()
    {
        runnerSpawns = GetSpawnsWithTag("RunnerSpawn");
        chaserSpawns = GetSpawnsWithTag("ChaserSpawn");
    }

    Transform[] GetSpawnsWithTag(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        Transform[] result = new Transform[gos.Length];
        for (int i = 0; i < gos.Length; i++)
            result[i] = gos[i].transform;
        return result;
    }

    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc()
    {
        foreach (var player in FindObjectsOfType<PlayerData>())
        {
            NetworkObject prefab =
                player.SelectedTeam.Value == Team.Runner
                    ? runnerPrefab
                    : chaserPrefab;

            if (prefab == null || player.Owner == null)
                continue;

            Transform spawn =
                player.SelectedTeam.Value == Team.Runner
                    ? runnerSpawns[runnerIndex++ % runnerSpawns.Length]
                    : chaserSpawns[chaserIndex++ % chaserSpawns.Length];

            NetworkObject character =
                Instantiate(prefab, spawn.position, spawn.rotation);

            InstanceFinder.ServerManager.Spawn(character, player.Owner);
        }
    }
}