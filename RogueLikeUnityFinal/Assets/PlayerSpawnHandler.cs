using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnHandler : MonoBehaviour
{
    public string playerTag = "Player";

    void Start()
    {
        GameObject player = GameObject.FindWithTag(playerTag);

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
