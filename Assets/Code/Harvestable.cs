using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Harvestable : Outlinable
{
    public SpriteRenderer spriteRenderer;
    public Sprite afterHarvestSprite;

    public float minDistToHarvest = 3;

    public void OnMouseDown()
    {
        if (!available)
            return;

        if((Player.instance.transform.position - transform.position).magnitude > minDistToHarvest)
        {
            Player.instance.MoveTowards(this, transform.position, () => { Harvest(); });
        } else
        {
            Harvest();
        }
    }

    public void Harvest()
    {
        Player.instance.Harvest(this, () =>
        {
            available = false;
            spriteRenderer.sprite = afterHarvestSprite;
            HarvestDone();
        });
    }

    public abstract void HarvestDone();
}
