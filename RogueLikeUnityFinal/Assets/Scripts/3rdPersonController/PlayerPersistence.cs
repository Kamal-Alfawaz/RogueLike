using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistObject : MonoBehaviour
{
    private static PersistObject Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (ShouldDestroyOnScene(scene.name))
        {
            Destroy(gameObject);
        }
    }

    private bool ShouldDestroyOnScene(string sceneName)
    {
        // Add other scene names if needed
        return sceneName == "MainMenu";
    }
}
