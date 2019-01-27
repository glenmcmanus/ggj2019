using SpriteGlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlinable : MonoBehaviour
{
    public int outlineWidth = 1;
    public bool available = true;
    public SpriteGlowEffect spriteGlow;

    public virtual void OnMouseEnter()
    {
        if (!available)
            return;

        spriteGlow.OutlineWidth = outlineWidth;
    }

    public virtual void OnMouseExit()
    {
        spriteGlow.OutlineWidth = 0;
    }
}
