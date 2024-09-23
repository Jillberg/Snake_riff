using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour,IItem
{
    public ItemCounter foodCounter;
    public static event Action BodyShouldGrow;
    public void Collect()
    {
        Destroy(gameObject);
        //let the player know it shold grow
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            Collect();
            foodCounter.counter += 1;
            BodyShouldGrow?.Invoke();
        }
    }
}
