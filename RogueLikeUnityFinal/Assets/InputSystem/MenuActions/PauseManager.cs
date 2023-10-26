using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public bool paused = false;
    public GameObject pauseMenuUI;
    public GameObject Camera;
    PauseMenu action;

    private void Awake() 
    {
        action = new PauseMenu();
    }

    private void OnEnable(){
        action.Enable();
    }

    private void OnDisable(){
        action.Disable();
    }

    private void Start(){
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause(){
        if(paused){
            ResumeGame();
        }else{
            PauseGame();
        }
    }

    public void PauseGame(){
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Camera.SetActive(false);
        paused = true;
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Camera.SetActive(true);
        paused = false;
    }

    public void LoadMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame(){
        Debug.Log("Quitting!");
        Application.Quit();
    }
}
