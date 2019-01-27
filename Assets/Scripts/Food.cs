using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteGlowEffect))]
public class Food : Harvestable
{
    public int foodValue = 1;

    public UnityEvent OnFoodChopped;

    public Sprite SpriteVariant1;
    public Sprite SpriteVariant2;

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Random.value > 0.5f ? SpriteVariant1 : SpriteVariant2;
    }

    public override void HarvestDone()
    {
        Player.instance.inventory.food += foodValue;
        OnFoodChopped.Invoke();
    }
}