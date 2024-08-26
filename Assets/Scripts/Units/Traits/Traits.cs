using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terra {
[CreateAssetMenu(fileName = "New Trait", menuName = "Traits")]
public class Traits : ScriptableObject
{
    public string traitName;
    public string traitDescription;

    public int traitNumber;
    public string statToEffect;

    public bool isActive;
}
}
