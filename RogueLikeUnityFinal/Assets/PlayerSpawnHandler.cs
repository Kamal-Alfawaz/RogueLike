using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnHandler : MonoBehaviour
{

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            player.transform.position = transform.position;
        }
        else
        {
            Debug.LogError("Player object not found");
        }
    }
}
