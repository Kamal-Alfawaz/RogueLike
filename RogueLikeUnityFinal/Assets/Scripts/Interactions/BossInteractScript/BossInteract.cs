using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteract : MonoBehaviour, IInteractable
{
    public GameObject bossSpawnUI;

    public void Interact(){
        Debug.Log("Interacting with Boss");
    }

    public void InRange(){
        bossSpawnUI.SetActive(true);
    }

    public void OutOfRange(){
        bossSpawnUI.SetActive(false);
    }
}
