using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject enemy;
    public float speed = 5f;
    public int damage = 5;
    public float selfDestroyingThreshold=5f;
    private float timer;

 
    void FixedUpdate()
    {
        if (enemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        /*if (timer > selfDestroyingThreshold) 
        {
            Destroy(gameObject);
        }*/
       
    }
    // Start is called before the first frame update
    public void SetTarget(GameObject target)
    {
        enemy = target;
        Vector3 direction=enemy.transform.position-transform.position;
        float angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy)
        {
            
            Destroy(gameObject);
            enemy.GettingHurt(damage);
        }
    }
}
