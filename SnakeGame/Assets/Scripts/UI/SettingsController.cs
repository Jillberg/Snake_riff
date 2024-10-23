using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public GameObject menuCanvas;
    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnResumeClick()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnReloadClick()
    {
        // Unpause the game before resetting
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
