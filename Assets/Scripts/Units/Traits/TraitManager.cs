using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    public List<Traits> traits = new List<Traits>();

    public void AddTrait(Traits trait)
    {
        traits.Add(trait);
    }
}
