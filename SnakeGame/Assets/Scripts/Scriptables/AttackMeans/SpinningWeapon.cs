using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSpinning", menuName = "spinningAttack")]
public class SpinningWeapon : Attack
{
    public GameObject spinningOrbPrefab; // Reference to the prefab (orb, book, etc.)
    public float rotationSpeed = 100f; // Speed of the spinning
    public float distanceFromParent = 2f; // Distance from the parent
   
    public override void Activate(GameObject parent)
    {
        // Instantiate the spinning orb as a child of the parent
        GameObject spinningOrb = Instantiate(spinningOrbPrefab, parent.transform);

        // Start the spinning behavior
        SpinningOrb orbComponent = spinningOrb.GetComponent<SpinningOrb>();
        orbComponent.Initialize(parent, rotationSpeed, distanceFromParent);
    }
}