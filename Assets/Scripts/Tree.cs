using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;

[RequireComponent(typeof(SpriteGlowEffect))]
public class Tree : Outlinable
{
    public int woodValue = 1;

    public SpriteRenderer spriteRenderer;
    public Sprite stump;

    public void OnMouseDown()
    {
        if (!available)
            return;

        Harvest();
    }

    public void Harvest()
    {
        Player.instance.inventory.wood += woodValue;
        available = false;
        spriteRenderer.sprite = stump;
    }
    
}
