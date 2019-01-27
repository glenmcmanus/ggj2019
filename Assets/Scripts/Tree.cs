using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
using UnityEngine.Events;

public class Tree : Harvestable
{
    public SpriteGlowEffect topGlow;

    public int woodValue = 1;

    public UnityEvent OnTreeMined;

    public Transform trunk;
    public float fallEndAngle = 90f;
    public float fallStep = 1f;
    public float fallRate = 1f;
    WaitForSeconds fallWait;

    bool felled = false;

    private void Awake()
    {
        fallWait = new WaitForSeconds(fallRate);
    }

    public override void OnMouseEnter()
    {
        if (!available)
            return;

        spriteGlow.OutlineWidth = outlineWidth;

        if(topGlow != null)
            topGlow.OutlineWidth = outlineWidth;
    }

    public override void OnMouseExit()
    {
        spriteGlow.OutlineWidth = 0;

        if (topGlow != null)
            topGlow.OutlineWidth = 0;
    }

    public override void HarvestDone()
    {
        if(!felled)
            StartCoroutine(TreeFall());

        felled = true;
    }

    IEnumerator TreeFall()
    {
        int dir = Player.instance.transform.position.x > transform.position.x ? 1 : -1;
        float t = 0;
        while(t < fallEndAngle )
        {
            t += fallStep;
            trunk.transform.Rotate(Vector3.forward, fallStep * dir);
            yield return fallWait;
        }

        Destroy( trunk.gameObject );

        Player.instance.inventory.wood += woodValue;
        OnTreeMined.Invoke();
    }
}
