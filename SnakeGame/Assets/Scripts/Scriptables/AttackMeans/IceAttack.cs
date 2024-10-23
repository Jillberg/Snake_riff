using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIce", menuName = "IceAttack")]
public class IceAttack : Attack
{
    public GameObject IcebergPrefab;
    public override void Activate(GameObject parent, GameObject target)
    {

        GameObject iceberg = Instantiate(IcebergPrefab, target.transform.position, Quaternion.identity);



        Iceberg iceUnderEnemy = iceberg.GetComponent<Iceberg>();

        iceUnderEnemy.Initialize(target);
    }
}
