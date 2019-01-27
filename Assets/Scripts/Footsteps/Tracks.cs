using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Tracks", menuName = "Create new Tracks")]
public class Tracks : ScriptableObject
{
    public Sprite[] spriteStrip;
    public float duration = 30f;
    public WaitForSeconds frameRate;

    public void Initialize()
    {
        frameRate = new WaitForSeconds(duration / spriteStrip.Length);
    }
}
