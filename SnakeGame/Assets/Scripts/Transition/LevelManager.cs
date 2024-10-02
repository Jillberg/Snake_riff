using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public partyMembers partyMembers;
    public GameObject newMember1;
    public ItemCounter foodCounter;
    public GameObject portal;  // The portal prefab to spawn
   // public Transform portalSpawnLocation;  // Where to spawn the portal
    public static event Action PartyMembersChanged;
    private int totalFoodCount;
    private int foodLeft;

    void Start()
    {
        portal.SetActive(false);
        totalFoodCount = GameObject.FindGameObjectsWithTag("Food").Length; // Count all food objects
        Debug.Log(totalFoodCount);
        foodCounter.counter = 0;
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
            //InstantiatePortal();
            enablePortal();
            if (newMember1 &&!partyMembers.members.Contains(newMember1))
            {
                partyMembers.members.Add(newMember1);
                PartyMembersChanged?.Invoke();

            }
        }
    }

    /*private void InstantiatePortal()
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
    }*/

    private void enablePortal()
    {
        portal.SetActive(true);
        Vector3 offset = new Vector3(3.2f, 0.4f, 0f);
        if (newMember1)
        {
            Instantiate(newMember1, offset, Quaternion.identity);
        }
        
    }
}
      