using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteGlowEffect))]
public class Tree : Harvestable
{
    public int woodValue = 1;

    public UnityEvent OnTreeMined;

    public override void HarvestDone()
    {
        Player.instance.inventory.wood += woodValue;
        OnTreeMined.Invoke();
    }
}
