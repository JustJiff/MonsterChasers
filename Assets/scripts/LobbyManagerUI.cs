using System.Collections;
using FishNet;
using TMPro;
using UnityEngine;

public class LobbyManagerUI : MonoBehaviour
{
    public static LobbyManagerUI Instance;

    [Header("Team Lists")]
    public Transform runnerContent;
    public Transform chaserContent;
    public TMP_Text nameLabelPrefab;
    public GameObject startGameButton;
    
    void Awake()
    {
        Instance = this;
    }

    public void StartGameButton()
    {
        if (!InstanceFinder.IsServerStarted) return;

        FindObjectOfType<RoundTimer>().StartRound();
    }


    public void OnClickMonster()
    {
        var me = FindLocalPlayerData();
        if (me != null) me.SetTeamServerRpc(Team.Runner);
    }

    public void OnClickChaser()
    {
        var me = FindLocalPlayerData();
        if (me != null) me.SetTeamServerRpc(Team.Chaser);
    }

    public void RefreshLists()
    {
        ClearChildren(runnerContent);
        ClearChildren(chaserContent);

        var all = FindObjectsOfType<PlayerData>();

        foreach (var p in all)
        {
            if (p.SelectedTeam.Value == Team.None) continue;

            Transform parent = p.SelectedTeam.Value == Team.Runner ? runnerContent : chaserContent;
            var label = Instantiate(nameLabelPrefab, parent);
            label.text = p.PlayerName.Value;
        }
    }

    PlayerData FindLocalPlayerData()
    {
        foreach (var p in FindObjectsOfType<PlayerData>())
            if (p.IsOwner) return p;

        return null;
    }

    void ClearChildren(Transform t)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
            Destroy(t.GetChild(i).gameObject);
    }
   
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        bool isHost = FishNet.InstanceFinder.ServerManager != null &&
                      FishNet.InstanceFinder.ServerManager.Started;

        startGameButton.SetActive(isHost);

        Debug.Log("Czy host: " + isHost);
    }

}