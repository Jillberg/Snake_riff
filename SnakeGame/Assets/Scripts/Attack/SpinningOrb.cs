using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningOrb : MonoBehaviour
{
    private GameObject parentObject;
    private float rotationSpeed;
    private float distanceFromParent;
    private Vector3 rotationAxis = Vector3.forward; // Z-axis for 2D games
    public int damage;

    public void Initialize(GameObject parent, float speed, float distance)
    {
        parentObject = parent;
        rotationSpeed = speed;
        distanceFromParent = distance;


        // Set initial position at the correct distance from the parent
        transform.position = parentObject.transform.position + new Vector3(distanceFromParent, 0, 0);
        transform.SetParent(null);
    }

    void Update()
    {
       if(parentObject !=null)
            { // Rotate the orb around the parent object
        transform.RotateAround(parentObject.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);

            // Keep the orb at the same distance from the parent
            Vector3 direction = (transform.position - parentObject.transform.position).normalized;
            transform.position = parentObject.transform.position + direction * distanceFromParent;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy)
        {
            
            
            enemy.GettingHurt(damage);
        }
    }
}
