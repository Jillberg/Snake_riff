using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturningHome : MonoBehaviour
{
    public int sceneIndex;
    public StateControl isSnakeMode;
    public ItemCounter foodCounter;
    public Vector2 respawningPosition;
    public VectorValue playerLoadingPosition;
    //public Vector2 positionToAppear;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerLoadingPosition.initialValue = respawningPosition;
            if (sceneIndex == 1)
            {
                isSnakeMode.isInState = false;
            }
            
            foodCounter.counter = 0;
            SceneManager.LoadScene(sceneIndex);
        };
    }
}
