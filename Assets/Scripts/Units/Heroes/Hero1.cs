using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1 : BaseHero
{
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        originalMovePoint.parent = null;
        originalMovement = movementRange;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
    }
}
