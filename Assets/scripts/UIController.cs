using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject gameoverText;
     private void OnEnable()
    {
       Catcher.OnGameEnd += GameEnd;
    }

    void GameEnd()
    {
        gameoverText.SetActive(true);
    }
}
