using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "newSlashing", menuName = "slashingAttack")]
public class SlashAttack : Attack
{
    public GameObject slashWavePrefab;
    public override void Activate(GameObject parent,GameObject target)
    {

        GameObject slashingWave = Instantiate(slashWavePrefab, parent.transform.position,Quaternion.identity);
        
        
    
        Slashing slashComponent = slashingWave.GetComponent<Slashing>();
        Vector3 direction= target.transform.position - slashingWave.transform.position;

        slashComponent.Initialize(target,direction.normalized,parent);
    }
}
