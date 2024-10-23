using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Iceberg : MonoBehaviour
{
    public int damage;
    private Animator animator;
    public float effectRadius=1.5f;
    public float effectDuration = 1f;
    public float effectIntensity = 0.8f;

    public void Initialize(GameObject target)
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            Debug.LogError("haha");
        }
        //transform.SetParent(target.transform);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, effectRadius);
        foreach (Collider2D collider in colliders)
        {
            EnemyMovement enemy = collider.GetComponent<EnemyMovement>();
            if (collider.GetComponent<EnemyMovement>())
            {
               
                    enemy.SlowedDown(effectDuration, effectIntensity);
                    enemy.GettingHurt(damage);
            }
        }
   
        
        animator.SetTrigger("ice");
        StartCoroutine(IceThawing());
    }

    

    private IEnumerator IceThawing()
    {

        yield return new WaitForSeconds(1f);
        //animator.SetBool("isExtinguishing", false);
        //the fire dies out, play animation
        Destroy(gameObject);

    }
}
