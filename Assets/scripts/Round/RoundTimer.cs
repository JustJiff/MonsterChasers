using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using UnityEngine;

public class RoundTimer : NetworkBehaviour
{
    private readonly SyncVar<float> timeLeft = new();
    private readonly SyncVar<bool> timerRunning = new();

    private const float ROUND_TIME = 180f;

    
    [Server]
    public void StartRound()
    {
        if (timerRunning.Value) return;

        timeLeft.Value = ROUND_TIME;
        timerRunning.Value = true;

        StartCoroutine(TimerCoroutine());
    }

   
    [Server]
    private IEnumerator TimerCoroutine()
    {
        while (timeLeft.Value > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft.Value--;
        }

        timerRunning.Value = false;
        RoundEnded();
    }

   
    [Server]
    private void RoundEnded()
    {
        NotifyClients();
    }

   
    [ObserversRpc]
    private void NotifyClients()
    {
        Debug.Log("Czas min¹³ – monsters won");
    }

    
    public float GetTimeLeft()
    {
        return timeLeft.Value;
    }
}
