using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ScriptableObject
{
    public bool isPermanent = false;
    public virtual void Activate(GameObject parent) { }
    public virtual void Activate(GameObject parent,GameObject target) { }

}
