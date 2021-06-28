using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBomb : Weapons
{
    protected override void OnEnable()
    {
        Invoke("DeactivateGameObject", deactivateTime);
    }

    protected override void Update()
    {
        InvokeRepeating("OffsetThenPush", 0, 5);
    }
}