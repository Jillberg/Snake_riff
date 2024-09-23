using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportPosition;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            collision.transform.position=teleportPosition.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
