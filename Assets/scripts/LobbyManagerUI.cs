using UnityEngine;

public class LobbyManagerUI : MonoBehaviour
{
    private PlayerData localPlayer;

    private void Start()
    {
        localPlayer = FindLocalPlayer();
    }

    private PlayerData FindLocalPlayer()
    {
        PlayerData[] players = FindObjectsOfType<PlayerData>();
        foreach (var p in players)
        {
            if (p.IsOwner)
                return p;
        }
        return null;
    }

    public void ChooseTeamMonsters()
    {
        Debug.Log("MONSTERS BUTTON CLICKED");
        if (localPlayer == null) return;

        localPlayer.SetTeamServerRpc(Team.Monsters);
        Debug.Log("Chose Monsters");
    }

    public void ChooseTeamCatchers()
    {
        Debug.Log("START GAME BUTTON CLICKED");
        if (localPlayer == null) return;

        localPlayer.SetTeamServerRpc(Team.Catchers);
        Debug.Log("Chose Catchers");
    }
}
