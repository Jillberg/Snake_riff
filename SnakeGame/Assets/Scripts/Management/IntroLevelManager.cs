using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IntroLevelManager : MonoBehaviour
{
    private Bounds mapBounds;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private Collider2D mapCollider;
    public GameObject food;
    public int amoutToLeave;
    private int foodCounter = 0;
    public GameObject portal;
    [SerializeField] private float offset = 2.0f;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        portal.SetActive(false);
        mapCollider = GetComponent<Collider2D>();
        mapBounds = mapCollider.bounds;
        minBounds = new Vector2(mapBounds.min.x + offset, mapBounds.min.y + offset);
        maxBounds = new Vector2(mapBounds.max.x - offset, mapBounds.max.y - offset);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void OnEnable()
    {
        Basic.BodyShouldGrow += CheckBodySize;
    }

    private void OnDisable()
    {
        Basic.BodyShouldGrow -= CheckBodySize;
    }

    private void CheckBodySize()
    {
        audioManager.PlaySFX(audioManager.growBody);
        foodCounter++;
        if (foodCounter < amoutToLeave)
        {
            SpawnFood();
        }
        else
        {
            EnablePortal();
        }
    }

    private Vector2 GetRandomFoodLocation()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);




        return new Vector2(randomX, randomY);
    }

    private void SpawnFood()
    {

        Vector2 spawnLocation = GetRandomFoodLocation();
        GameObject spawnedZone = Instantiate(food, spawnLocation, Quaternion.identity);
    }

    private void EnablePortal()
    {
        portal.SetActive(true);
    }
}
