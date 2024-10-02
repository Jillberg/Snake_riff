using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour,Idamagable
{
    private GameObject player;
    private Vector2 direction=Vector2.zero;
    public float speed = 2f;
    public int health = 5;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction=(player.transform.position -transform.position).normalized;
        transform.position= Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    public void GettingHurt(int damage)
    {
        health -= damage;
        Debug.Log("Hurt");
        if (health <= 0) 
        {
            Destroy(gameObject);
        }
    }

    
}
