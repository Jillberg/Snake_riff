using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
    public GameObject warningPrefab;
    AudioManager audioManager;



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

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Update()
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (startingBatches > endingBatches && totalEnemyCount == 0)
        {
            StartCoroutine(ActivatePortal());
        }
        else if (totalEnemyCount == 0 && startingBatches <= endingBatches)
        {
            timeWaitUntilNextBatch -= Time.deltaTime;

            if (timeWaitUntilNextBatch <= 0)
            {
                for (int i = 0; i < startingBatches; i++)
                {
                    StartCoroutine(WarningAndSpawn());
                    //SpawnEnemyZone();
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

    private IEnumerator WarningAndSpawn()
    {
       
            Vector2 spawnLocation = GetValidZoneLocation();
            
            // Instantiate the warning sign at the enemy spawn location
            GameObject warning = Instantiate(warningPrefab, spawnLocation, Quaternion.identity);
        audioManager.PlaySFX(audioManager.summoningGlyph);
            // Wait for the countdown
            yield return new WaitForSeconds(1);
            timeWaitUntilNextBatch = interval;
             yield return new WaitForSeconds(1);

        // Destroy the warning sign
        Destroy(warning);

        // Spawn the enemy at the location
            SpawnEnemyZone(spawnLocation);
             timeWaitUntilNextBatch = interval;


        // Optionally destroy the spawner object after enemies spawn

    }

    private IEnumerator ActivatePortal()
    {
        yield return new WaitForSeconds(3);
        yield return new WaitUntil(() => totalEnemyCount == 0);
        portal.SetActive(true);
    }

    public void SummoningEnemy(int batches)
    {
        for (int i = 0; i < batches; i++)
        {
            StartCoroutine(WarningAndSpawn());

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

    private void SpawnEnemyZone(Vector2 spawnLocation)
    {
        GameObject spawnedZone = Instantiate(enemyRespawningZone, spawnLocation, Quaternion.identity);
    }

}
