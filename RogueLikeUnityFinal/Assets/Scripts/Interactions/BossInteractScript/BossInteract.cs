using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossInteract : MonoBehaviour, IInteractable
{
    public GameObject bossSpawnUI;

    public void Interact()
    {
        Debug.Log("Interacting with Boss");
        LoadNextScene();
    }

    public void InRange()
    {
        bossSpawnUI.SetActive(true);
    }

    public void OutOfRange()
    {
        bossSpawnUI.SetActive(false);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Stage_2");
    }
}
