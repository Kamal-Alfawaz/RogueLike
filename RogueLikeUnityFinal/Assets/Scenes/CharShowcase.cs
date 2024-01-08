using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharShowcase : MonoBehaviour
{

    public void Play(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage_1");
    }

    public void Back(){
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame(){
        Debug.Log("Quit!");
        Application.Quit();
    }

}