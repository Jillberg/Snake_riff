using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour,Idamagable
{
    private GameObject player;
    private Vector2 direction=Vector2.zero;
    public float speed = 2f;
    public int health = 5;
    public int EnemyDamage = 1;
    private Animator animator;
    private bool isDead = false;
    private bool isAttacking = false;

    //for boss UI
    public bool isBoss = false;
    public Image healthBar;
    public GameObject healthUI;
    private int healthAmount;
    private int maxHealth;

    public SpriteRenderer spriteRenderer;
    public Color hurtColor = Color.red; // Color to flash when hit
    public float flashDuration = 0.15f;  // How long to flash
    AudioManager audioManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();

        if (isBoss)
        {
            Debug.Log("bar");
            healthAmount=health;
            maxHealth=health;
            healthUI.SetActive(true);
            healthBar.fillAmount = healthAmount/maxHealth;
            
        }
      
  
    }
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnDisable()
    {
        if (isBoss)
        {
            audioManager.PlaySFX(audioManager.bossKilled);
        }
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction=(player.transform.position -transform.position).normalized;
        if (!isDead && !isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
       
    }

    public void GettingHurt(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            audioManager.PlaySFX(audioManager.enemyHit);
            if (isBoss) 
            {
                DepletingBossHealthBar(damage);
            }
            //Debug.Log("Hurt");
            StartCoroutine(HitEffect());
            if (health <= 0)
            {
                isDead = true;
                StartCoroutine(EnemyDead());
            }
        }
    }

    private IEnumerator EnemyDead()
    {
        animator.SetBool("isDead",true);
        gameObject.tag = "Untagged";
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private IEnumerator HitEffect()
    {
        // Save the original color
        Color originalColor = spriteRenderer.color;

        // Change the color to the hurt color
        spriteRenderer.color = hurtColor;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert to the original color
        spriteRenderer.color = originalColor;
    }

    public void SlowedDown(float effectDuration, float effectIntensity)
    {
        StartCoroutine(SlowingCoroutine(effectDuration, effectIntensity));
    }

    private IEnumerator SlowingCoroutine(float effectDuration, float effectIntensity)
    {
        Color originalColor = Color.white;
        float temp = speed;
        speed = effectIntensity;
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.color = originalColor;
        speed = temp;
    }

    private void DepletingBossHealthBar(int damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = (float)healthAmount / (float)maxHealth;
    }

    public void EnemyAttacking(float recoveringTime)
    {
        StartCoroutine(HandlingEnemyAttacking(recoveringTime));
    }

    private IEnumerator HandlingEnemyAttacking(float recoveringTime) 
    {
        isAttacking = true;
        yield return new WaitForSeconds(recoveringTime);
        isAttacking=false;
    }
}

    

