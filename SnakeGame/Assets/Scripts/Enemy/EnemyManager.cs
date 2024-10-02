using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyRespawningZone;
    [SerializeField] private float interval;
    private Collider2D mapCollider;
    private Bounds mapBounds;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float timeWaitUntilNextBatch;
    private int totalEnemyCount = 0;
    private int batchCounter = 0;
    private int totalBatches;
    public TextMeshProUGUI batchesInfo;
    public GameObject portal;



    [SerializeField] private int startingBatches;
    [SerializeField] private int endingBatches;
    [SerializeField] private int amountInBatch;
    [SerializeField] private float offset=2.0f;
    [SerializeField] private LayerMask layerCannotSpawnOn;
    [SerializeField] private int zoneRadius=2;
    

    void Start()
    {
        mapCollider = GetComponent<Collider2D>();
        mapBounds = mapCollider.bounds;
        timeWaitUntilNextBatch =interval;
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        minBounds = new Vector2(mapBounds.min.x + offset, mapBounds.min.y + offset);
        maxBounds = new Vector2(mapBounds.max.x - offset, mapBounds.max.y - offset);
        totalBatches=endingBatches-startingBatches+1;
        batchesInfo.text += batchCounter.ToString()+ " / " + totalBatches.ToString();
    }
    void Update()
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (startingBatches > endingBatches && totalEnemyCount == 0)
        {
            portal.SetActive(true);
        }
        else if (totalEnemyCount == 0 && startingBatches <= endingBatches)
        {
            timeWaitUntilNextBatch -= Time.deltaTime;

            if (timeWaitUntilNextBatch <= 0)
            {
                for (int i = 0; i < startingBatches; i++)
                {
                    SpawnEnemyZone();
                }
                timeWaitUntilNextBatch = interval;
                startingBatches++;
                if (startingBatches > endingBatches)
                {
                    
                }
                batchCounter++;
                batchesInfo.text = "WAVE:" + batchCounter.ToString() + " / " + totalBatches.ToString();

            }
        }
        
       
    }

    

    private Vector2 GetRandomZoneLocation()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        


        return new Vector2(randomX, randomY);
    }

    private Vector2 GetValidZoneLocation()
    {
        Vector2 spawnLocation = Vector2.zero;
        bool isSpawnPositionValid = false;

        int attemptCount = 0;
        int threshold = 200;

        while (!isSpawnPositionValid && attemptCount < threshold)
        {
            spawnLocation = GetRandomZoneLocation();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnLocation, zoneRadius);

            bool isInvalidCollision = false;
            foreach(Collider2D collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & layerCannotSpawnOn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnPositionValid=true;
            }

            attemptCount++;
        }

        if (!isSpawnPositionValid)
        {
            Debug.LogWarning("Couldnt find valid spawning location");
        }

        return spawnLocation;
    }

    private void SpawnEnemyZone()
    {
        
        Vector2 spawnLocation= GetValidZoneLocation();
        GameObject spawnedZone= Instantiate(enemyRespawningZone,spawnLocation,Quaternion.identity);
    }

}
