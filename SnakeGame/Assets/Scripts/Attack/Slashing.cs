using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Slashing : MonoBehaviour
{
    private GameObject targetObject;
    private GameObject parentObject;
    private Vector3 moveDirection;
    public float chargeDuration = 2f; // Time the wave stays with the character before launching
    public float speed = 5f;
    public int damage;
    private Collider2D slashCollider;

    public void Initialize(GameObject target,Vector3 direction,GameObject parent)
    {
        targetObject=target;
        moveDirection=direction;
        //moveDirection=target.transform.position;
        parentObject=parent;
        slashCollider = GetComponent<Collider2D>();
        if (slashCollider != null)
        {
           // slashCollider.enabled = false; // Disable the collider while attached to the parent
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //transform.SetParent(parentObject.transform);
       // StartCoroutine(ChargeAndRelease());
    }
    private IEnumerator ChargeAndRelease()
    {
        // Keep the wave attached to the character for the charge duration
       
        yield return new WaitForSeconds(1f);
   
        if (slashCollider != null)
        {
            slashCollider.enabled = true; // Disable the collider while attached to the parent
        }
      
        // Release the wave and let it move toward the target
        transform.SetParent(null);  // Detach from the character
    }
    void Update()
    {
            transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
            //transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hit an enemy
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy)
        {
      
            enemy.GettingHurt(damage);  // Deal damage to the enemy
               // Destroy the wave after hitting the enemy
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("slash hit wall");
            Destroy(gameObject);        // Destroy the wave if it hits a wall
        }
    }
}
