using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    public static Sled instance;

    public Sprite[] StageSprites;

    public int woodNeededForStage1 = 10;
    public int woodNeededForStage2 = 15;
    public int woodNeededForStage3 = 25;
    public int woodNeededForStage4 = 25;
    public int woodNeededForStage5 = 25;
    public int totalWoodNeeded { get { return woodNeededForStage1 + woodNeededForStage2 + woodNeededForStage3 + woodNeededForStage4 + woodNeededForStage5; } }

    public int totalWood = 0;
    public bool complete = false;

    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        instance = this;
    }

    public void GiveWood(int amount)
    {
        totalWood += amount;
        if(totalWood > woodNeededForStage5 + woodNeededForStage4+ woodNeededForStage3 + woodNeededForStage2+woodNeededForStage1)
        {
            spriteRenderer.sprite = StageSprites[4];
            complete = true;
        }
        else if (totalWood > woodNeededForStage4 + woodNeededForStage3+woodNeededForStage2+woodNeededForStage1)
        {
            spriteRenderer.sprite = StageSprites[3];
        }
        else if (totalWood > woodNeededForStage3 + woodNeededForStage2 + woodNeededForStage1)
        {
            spriteRenderer.sprite = StageSprites[2];
        }
        else if (totalWood > woodNeededForStage2 + woodNeededForStage1)
        {
            spriteRenderer.sprite = StageSprites[1];
        }
        else if(totalWood > woodNeededForStage1)
        {
            spriteRenderer.sprite = StageSprites[0];
        }
        else
        {
            
        }
    }
}
