using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteGlowEffect))]
public class Food : Harvestable
{
    public int foodValue = 1;

    public UnityEvent OnFoodChopped;

    public override void HarvestDone()
    {
        Player.instance.inventory.food += foodValue;
        OnFoodChopped.Invoke();
    }
}