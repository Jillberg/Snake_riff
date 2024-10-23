using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : MonoBehaviour
{
    public Attack attack;
    private int totalEnemyCount=0;
    public float attackTimer;
    public float attackCooldown = 2f;
    public AudioClip attackSound;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Food").Length;
        if(totalEnemyCount == 0 && attack.isPermanent)
        {
            attack.Activate(gameObject);
        }
       
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attack.isPermanent)
        {
            shouldAttack();
        }
        
    }

    private void shouldAttack()
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        attackTimer -= Time.deltaTime;
        if (totalEnemyCount != 0 && attackTimer <= 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int randomIndex = Random.Range(0, totalEnemyCount);
            GameObject targetEnemy = enemies[randomIndex];

            // Shoot projectile towards the selected enemy
            attack.Activate(gameObject,targetEnemy);
            audioManager.PlaySFX(attackSound);
            attackTimer = attackCooldown;
            //shoot projectile
        }
    }
}
