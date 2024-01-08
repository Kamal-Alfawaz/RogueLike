using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossDeath : MonoBehaviour
{

    public GameObject ObjectToEnable;
    public GameObject ObjectToEnable2;

    private void OnEnable()
    {
        Enemy.OnBossKilled += HandleBossKilled;
    }

    private void OnDisable()
    {
        Enemy.OnBossKilled -= HandleBossKilled;
    }

    private void HandleBossKilled()
    {
        if (!ObjectToEnable.activeSelf)
        {
            ObjectToEnable.SetActive(true);
            ObjectToEnable2.SetActive(true);
        }
    }

}
