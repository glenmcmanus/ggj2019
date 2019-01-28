using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FirePostProcessing : MonoBehaviour
{
    public PostProcessProfile home;

    public float flickerStep = 0.1f;
    public float flickerRate = .5f;
    WaitForSeconds flickerWait;

    public float minIntensity = 3f;
    public float maxIntensity = 6f;

    Bloom bloom;

    private void Awake()
    {
        bloom = home.GetSetting<Bloom>();
        flickerWait = new WaitForSeconds(flickerRate);
        StartCoroutine(FlickerBloom());
    }

    IEnumerator FlickerBloom()
    {
        bool increasing = true;
        float t = 0;
        while(true)
        {
            if (increasing)
            {
                if(t >= 1)
                {
                    t = 0;
                    increasing = false;
                }
                else
                {
                    Debug.Log("Increase");
                    bloom.intensity.Interp(maxIntensity, minIntensity, t);
                    t += flickerStep;
                }
            }
            else
            {
                if (t >= 1)
                {
                    t = 0;
                    increasing = true;
                }
                else
                {
                    Debug.Log("Decrease");
                    bloom.intensity.Interp(minIntensity, maxIntensity, t);
                    t += flickerStep;
                }
            }

            yield return flickerWait;
        }
    }
}
