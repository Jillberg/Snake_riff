using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawningZone : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider2D enemyRespawningZone;
    private Bounds enemyRespawningBound;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    public float offset=0f;
    [SerializeField] private float enemyRadius=0.5f;
    [SerializeField] private LayerMask layerCannotSpawnOn;
    public partyMembers EnemyBatch;
    private List<GameObject> enemies = new List<GameObject>();
    private float counter;
    public float waitUntilSpawning = 2f;
    void Start()
    {
        enemyRespawningZone = GetComponent<Collider2D>();
        enemyRespawningBound=enemyRespawningZone.bounds;
        minBounds= new Vector2(enemyRespawningBound.min.x+offset,enemyRespawningBound.min.y+offset);
        maxBounds = new Vector2(enemyRespawningBound.max.x - offset, enemyRespawningBound.max.y - offset);
        foreach(GameObject e in EnemyBatch.members)
        {
            enemies.Add(e);
        }
        SpawnEnemy();
        Destroy(gameObject);

    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        counter+=Time.deltaTime;
        if (counter > waitUntilSpawning) 
        {
            
        }
    }
    */
    private Vector2 GetRandomLocation()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX,randomY);
    }

     private Vector2 GetValidEnemyLocation()
    {
        Vector2 spawnLocation = Vector2.zero;
        bool isSpawnPositionValid = false;

        int attemptCount = 0;
        int threshold = 2000;

        while (!isSpawnPositionValid && attemptCount < threshold)
        {
            spawnLocation = GetRandomLocation();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnLocation, enemyRadius);

            bool isInvalidCollision = false;
            foreach (Collider2D collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & layerCannotSpawnOn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnPositionValid = true;
            }

            attemptCount++;
        }

        if (!isSpawnPositionValid)
        {
           // Debug.LogWarning("Couldnt find valid enemy location");
        }

        return spawnLocation;
    }

    private void SpawnEnemy()
    {
        
        foreach (GameObject enemy in enemies) 
        {
            Vector2 spawnLocation = GetValidEnemyLocation();
            GameObject spawnedEnemy = Instantiate(enemy, spawnLocation, Quaternion.identity);
        }
       
        
    }
}
