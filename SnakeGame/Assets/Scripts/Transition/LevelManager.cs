using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public ItemCounter foodCounter;
    public GameObject portalPrefab;  // The portal prefab to spawn
    public Transform portalSpawnLocation;  // Where to spawn the portal

    private int totalFoodCount;
    private int foodLeft;

    void Start()
    {
        totalFoodCount = GameObject.FindGameObjectsWithTag("Food").Length; // Count all food objects
    }
    private void OnEnable()
    {
        Basic.BodyShouldGrow += CheckFoodAmount;
    }

    private void OnDisable()
    {
        Basic.BodyShouldGrow -= CheckFoodAmount;
    }
    // Call this when food is eaten
    public void CheckFoodAmount()
    {

        // If no food is left, instantiate the portal
        if (foodCounter.counter==totalFoodCount)
        {
            InstantiatePortal();
        }
    }

    private void InstantiatePortal()
    {
        if (portalPrefab != null && portalSpawnLocation != null)
        {
            Instantiate(portalPrefab, portalSpawnLocation.position, Quaternion.identity);
            Debug.Log("Portal created!");
        }
        else
        {
            Debug.LogError("Portal prefab or spawn location not assigned!");
        }
    }
}
