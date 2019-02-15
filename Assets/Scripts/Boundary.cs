using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boundary : MonoBehaviour
{
    public Image boundaryPanel;
    public GameObject boundsMessage;
    public float minX, maxX, minZ, maxZ;

    private void Update()
    {
        if(Player.instance.transform.position.x < minX || Player.instance.transform.position.x > maxX
            || Player.instance.transform.position.z < minZ || Player.instance.transform.position.z > maxZ)
        {
            if (boundsMessage.activeSelf)
                return;

            boundsMessage.SetActive(true);
            StartCoroutine(FadeWhite());
        }
        else
        {
            if (!boundsMessage.activeSelf)
                return;

            boundsMessage.SetActive(false);
            StartCoroutine(FadeWhite());
        }

    }

    IEnumerator FadeWhite()
    {
        float step = 0.05f;
        float t = 0;
        bool state = boundsMessage.activeSelf;
        Color target;
        float limit;

        if (state == true)
        {
            target = Color.white;
            limit = 0.3f;
        }
        else
        {
            target = Color.clear;
            limit = 1f;
        }
        
        while(state == boundsMessage.activeSelf)
        {
            t += step;
            boundaryPanel.color = Color.Lerp(boundaryPanel.color, target, t);

            if (t >= limit)
            {
                Debug.Log("break, t = " + t);
                if(state == false)
                    boundaryPanel.color = target;
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
