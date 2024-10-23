using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMovement : MonoBehaviour
{
    public float attackCooldown;
    public float attackTimer;
    public EnemyManager enemyManager;
    private EnemyMovement enemyMovement;
    public int maxBatches;
    public float recoveringTime;
    AudioManager audioManager;

    private Shake shake;

    private void Start()
    {
       
        enemyMovement = GetComponent<EnemyMovement>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayBossMusic();
    }

    private void Update()
    {
        attackTimer-= Time.deltaTime;
        if(attackTimer < 0)
        {
            BossSummoning();
            attackTimer = attackCooldown;
            
        }
    }

    private void BossSummoning()
    {
        int randomIndex = Random.Range(1, maxBatches);
        enemyManager.SummoningEnemy(randomIndex);
        shake.CamShake();
        enemyMovement.EnemyAttacking(recoveringTime);
    }

}
