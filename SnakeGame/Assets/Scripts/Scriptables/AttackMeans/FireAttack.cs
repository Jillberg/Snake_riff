using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newFire", menuName = "FireAttack")]
public class FireAttack : Attack
{
    public GameObject hellFirePrefab;
    public override void Activate(GameObject parent, GameObject target)
    {

        GameObject hellFire = Instantiate(hellFirePrefab, target.transform.position, Quaternion.identity);



        HellFire fireUnderEnemy = hellFire.GetComponent<HellFire>();

        fireUnderEnemy.Initialize(target);
    }
}
