using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject Camera;
    public bool GameIsOver = false;

    private void Start() {
        GameIsOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Camera.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void restartGame(){
        GameIsOver = false;
        SceneManager.LoadScene("mainMenu");
        Time.timeScale = 1;
    }
}
