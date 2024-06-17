using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasToggle : MonoBehaviour
{
    public static CanvasToggle Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void EnableCanvas(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Canvas to enable is null.");
        }
    }

    public void DisableCanvas(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Canvas to disable is null.");
        }
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is null or empty.");
        }
    }

    public void ExitApplication()
    {
        Debug.Log("Exiting application...");
        Application.Quit();
    }
}
