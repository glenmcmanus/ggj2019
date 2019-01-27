using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Tracks", menuName = "Create new Tracks")]
public class Tracks : ScriptableObject
{
    public Sprite sprite;
    public float fadeRate = 1;
    public float fadeStep = 0.1f;
    public WaitForSeconds fadeWait;

    public void Initialize()
    {
        fadeWait = new WaitForSeconds(fadeRate);
    }
}
