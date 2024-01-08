using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour, IInteractable
{
    public GameObject bossSpawnUI;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    public void Interact()
    {
        Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        Destroy(this.gameObject);
    }

    public void InRange()
    {
        bossSpawnUI.SetActive(true);
    }

    public void OutOfRange()
    {
        bossSpawnUI.SetActive(false);
    }

}
