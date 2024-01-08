using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnInteract : MonoBehaviour, IInteractable
{
    public GameObject bossSpawnUI;
    public GameObject boss;

    public void Interact()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void InRange()
    {
        bossSpawnUI.SetActive(true);
    }

    public void OutOfRange()
    {
        bossSpawnUI.SetActive(true);
    }
}
