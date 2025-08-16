using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMaxExp", menuName = "ScriptableObjects/PlayerMaxExpScriptableObject")]
public class PlayerMaxExpScriptableObject : ScriptableObject
{
    [Header("MaxEXP")]
    public List<int> MaxEXP;
}
