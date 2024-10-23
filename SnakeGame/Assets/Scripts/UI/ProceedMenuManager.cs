using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProceedMenuManager : MonoBehaviour
{
    public int sceneIndex;
    public void OnProceedClick()
    {
      
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }
}
