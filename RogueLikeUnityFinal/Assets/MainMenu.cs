using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("CharShowCase");
    }

    public void quitGame(){
        Debug.Log("Quit!");
        Application.Quit();
    }

}
