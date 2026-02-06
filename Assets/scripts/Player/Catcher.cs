using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Catcher : MonoBehaviour
{
    public GameObject[] runners;
    public static event Action OnGameEnd;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if(other.tag == "Runner")
        {
            Destroy(other.gameObject);
        }
        runners = GameObject.FindGameObjectsWithTag("Runner");
            Debug.Log(runners.Length);
            if (runners.Length == 0)
            {
                Debug.Log(runners.Length);
                OnGameEnd?.Invoke();
            }
    }
}
