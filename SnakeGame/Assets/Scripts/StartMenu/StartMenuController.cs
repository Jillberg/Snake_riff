using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public int sceneIndex;
    public Vector2 respawningPosition;
    public VectorValue playerLoadingPosition;
    public partyMembers party;
    public StateControl isSnakeMode;
    public void OnStartClick()
    {
        playerLoadingPosition.initialValue = respawningPosition;
        party.ClearParty();
        SceneManager.LoadScene(sceneIndex);
        isSnakeMode.isInState = true;
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
