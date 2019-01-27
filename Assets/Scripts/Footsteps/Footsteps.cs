using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public Footstep prefab;
    public Tracks tracks;

    public void LayStep(Transform t)
    {
        Footstep f = Instantiate(prefab, t.position, t.rotation, transform);
        f.sprite = tracks.spriteStrip[0];
        f.StartCoroutine(Duration(f));
    }

    IEnumerator Duration(Footstep footstep)
    {
        int frame = 0;
        while (frame < tracks.spriteStrip.Length)
        {
            footstep.sprite = tracks.spriteStrip[frame++];
            yield return tracks.frameRate;
        }


    }
}
