using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPartyList",menuName ="party")]
public class partyMembers : ScriptableObject
{
    public List<GameObject> members= new List<GameObject>();

    public void ClearParty()
    {
        members.Clear();
    }

   
}
