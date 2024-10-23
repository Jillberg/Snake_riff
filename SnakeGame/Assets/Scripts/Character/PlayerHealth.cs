using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private bool isInvinsible=false;
    public HealthUI healthUI;
    public GameObject gameOverScreen;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("EnemyManager").Length != 0)
        {
            currentHealth = maxHealth;
            healthUI.SetMaxHealth(maxHealth);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if(gameOverScreen!=null)
        {
            gameOverScreen.SetActive(false);
        }
        
        
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemy= collision.GetComponent<EnemyMovement>();
        if (enemy&&!isInvinsible)
        {
            StartCoroutine(GettingHit());
            TakingDamage(enemy.EnemyDamage);
        }
    }

    private void TakingDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.playerGettingHit);
        currentHealth-= damage;
        healthUI.UpdateHealth(currentHealth);

        if (currentHealth <= 0) 
        { 
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private IEnumerator GettingHit()
    {
        isInvinsible = true;
        float flashDuration = 0.2f;  // How fast to alternate colors
        int numberOfFlashes = 5;     // How many times to alternate

        for (int i = 0; i < numberOfFlashes; i++)
        {
            // Alternate between red and white
            spriteRenderer.color = (spriteRenderer.color == Color.red) ? Color.white : Color.red;

            // Wait for a short duration between flashes
            yield return new WaitForSeconds(flashDuration);
        }

        // Restore the original color and reset invincibility
        spriteRenderer.color = Color.white;
        isInvinsible = false;
    }

    public void ResetScene()
    {
        // Unpause the game before resetting
        Time.timeScale = 1;

        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
