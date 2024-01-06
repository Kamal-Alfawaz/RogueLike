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
        SceneManager.LoadScene("mainMenu");
    }

    public void restartGame(){
        GameIsOver = false;
        SceneManager.LoadScene("Stage_1");
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
