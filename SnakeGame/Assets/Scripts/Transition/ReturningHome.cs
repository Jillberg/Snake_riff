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
    public GameObject confirmationPanel;
    public GameObject newMember1;
    public partyMembers partyMembers;
    AudioManager audioManager;
    //public Vector2 positionToAppear;
    // Start is called before the first frame update
    /*public void OnTriggerEnter2D(Collider2D collision)
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
    }*/

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
      
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            audioManager.PlaySFX(audioManager.enterPortal);
            playerLoadingPosition.initialValue = respawningPosition;

            if (confirmationPanel != null)
            {
                confirmationPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                foodCounter.counter = 0;
                SceneManager.LoadScene(sceneIndex);
            }

            
            foodCounter.counter = 0;
            if (newMember1 && !partyMembers.members.Contains(newMember1))
            {
                partyMembers.members.Add(newMember1);
            }
            
        };
    }
}
