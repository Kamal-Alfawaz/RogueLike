using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public bool paused = false;
    public GameObject pauseMenu;
    public GameObject FollowCamera;

    PauseMenu action;

    private void Awake() {
        action = new PauseMenu();
    }

    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }

    private void Start() {
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
        pauseMenu.SetActive(true);
        FollowCamera.SetActive(false);
        Time.timeScale = 0;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame(){
        pauseMenu.SetActive(false);
        FollowCamera.SetActive(true);
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void QuitGame(){
        Debug.Log("Quit!");
        Application.Quit();

    }

}
