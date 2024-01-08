using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopInteract : MonoBehaviour, IInteractable
{
    public GameObject UI;
    public string nextScene;

    public void Interact()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void InRange()
    {
        UI.SetActive(true);
    }

    public void OutOfRange()
    {
        UI.SetActive(false);
    }
}
