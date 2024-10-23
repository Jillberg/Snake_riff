using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HellFire : MonoBehaviour
{

    public int damage;
    private Animator animator;

    public void Initialize(GameObject target)
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            Debug.LogError("haha");
        }
        //transform.SetParent(target.transform);
        EnemyMovement enemy = target.GetComponent<EnemyMovement>();
        if (enemy)
        {
            enemy.GettingHurt(damage);
            
    

        }
        animator.SetTrigger("fire");
        StartCoroutine(FireExtinguishing());
    }

    private IEnumerator FireExtinguishing()
    {
        
        yield return new WaitForSeconds(1f);
        //animator.SetBool("isExtinguishing", false);
        //the fire dies out, play animation
        Destroy(gameObject);

    }
}
